using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using System.Net.Mail;
using MetaNET.DataHelper;
public partial class admin_ajax_Controls_ObjectListAjax : System.Web.UI.UserControl
{
    public DataRow dr;
    public DataTable dataTable, dtMenu;
    public string FromDate, ToDate, Keyword, Filter, SortOrder, pagingSql, tableSql, fieldSql, action, control, folder, categoryid, controlName, filterJson, fieldJson, dataFieldSort, dataSort, flag, Level;
    string PositionMenuFlag, Hide, AttrProductFlag, filter = "";
    public int RemoveID, pageIndex, pageSize, totalWidthTable = 0;
    public List<MenuAdminJson> jsonHeaderField = new List<MenuAdminJson>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ProccessParameter();
        BindData();
    }

    protected void ProccessParameter()
    {
        FromDate = RequestHelper.GetString("startDate", string.Empty);
        ToDate = RequestHelper.GetString("endDate", string.Empty);
        Keyword = RequestHelper.GetString("key", string.Empty);
        dataFieldSort = RequestHelper.GetString("fieldSort", string.Empty);
        dataSort = RequestHelper.GetString("sort", string.Empty);
        Filter = RequestHelper.GetString("filter", string.Empty);
        pageIndex = RequestHelper.GetInt("pageIndex", 1);
        pageSize = RequestHelper.GetInt("pageSize", 50);

        tableSql = RequestHelper.GetString("table", "");
        fieldJson = RequestHelper.GetString("field", "");
        action = RequestHelper.GetString("action", "");
        control = RequestHelper.GetString("control", "");
        folder = RequestHelper.GetString("folder", "");
        controlName = RequestHelper.GetString("ControlName", "");
        categoryid = RequestHelper.GetString("categoryid", "");

        filterJson = RequestHelper.GetString("filterJson", "");
        flag = RequestHelper.GetString("flag", "");
        PositionMenuFlag = RequestHelper.GetString("PositionMenuFlag", "");
        Hide = RequestHelper.GetString("hide", "");
        AttrProductFlag = RequestHelper.GetString("AttrProductFlag", "");
        Level = RequestHelper.GetString("level", "");
        Filter = RequestHelper.GetString("filter", string.Empty);

        if (Filter == "all" || Filter == "null")
            Filter = "";

        SortOrder = dataFieldSort + " " + dataSort;

        if (action != "getTotalRecord" && RemoveID == 0) // Nếu là get dữ liệu
        {
            if (dataSort == "asc" || string.IsNullOrEmpty(dataSort))
                dataSort = "desc";
            else
                dataSort = "asc";
        }

        dtMenu = SqlHelper.SQLToDataTable("tblAdminMenu", "FieldSql", string.Format("Control=N'{0}'", Utils.GetControlAdmin()));

        if (dtMenu != null && dtMenu.Rows.Count > 0)
        {
            try
            {
                fieldJson = dtMenu.Rows[0]["FieldSql"].ToString();
                if (!string.IsNullOrEmpty(fieldJson))
                {
                    jsonHeaderField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MenuAdminJson>>(fieldJson);

                    foreach (MenuAdminJson json in jsonHeaderField)
                    {
                        if (json.Show)
                        {
                            fieldSql += json.Field + ",";

                            int WidthColumn = ConvertUtility.ToInt32(json.Width);
                            if (WidthColumn == 0)
                                WidthColumn = 200;

                            totalWidthTable += WidthColumn;
                        }
                    }

                    totalWidthTable += 300; // thêm cột action

                    fieldSql = fieldSql.Trim(',');
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }


        if (string.IsNullOrEmpty(dataFieldSort))
        {
            if (Utils.CommaSQLAdd(fieldSql).ToLower().Contains(",sort,"))
                SortOrder = "Sort ASC";
            else
                SortOrder = "ID DESC";
        }

        SortOrder = "ORDER BY " + SortOrder;

        RemoveID = RequestHelper.GetInt("removeID", 0);

        if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrEmpty(ToDate))
        {
            if(tableSql=="tblOrder")
                filter += string.Format("WHERE OrderDate >= '{0}' AND OrderDate < DATEADD(day,1,'{1}')", FromDate, ToDate);
            else
                filter += string.Format("WHERE EditedDate >= '{0}' AND EditedDate < DATEADD(day,1,'{1}')", FromDate, ToDate);
        }    
            



        if (!string.IsNullOrEmpty(Keyword))
        {
            if (tableSql == C.PRODUCT_TABLE)
            {
                filter = string.Format("WHERE REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([Name], '-', ''), '/', ''), '\', ''), '(', ''), ')', '') LIKE N'%{0}%'", Keyword);
            }
            else
            {
                filter += GenPrefixFilter(filter);
                filter += "(";
                if (ConvertUtility.ToInt32(Keyword, 0) > 0)
                {
                    filter += string.Format("ID='{0}'", Keyword);
                    filter += " OR ";
                }
                filter += string.Format("Name Like N'%{0}%'", Keyword);
                filter += ")";
            }
        }


        if (!string.IsNullOrEmpty(filterJson))
        {
            string decodeUrl = System.Text.RegularExpressions.Regex.Replace(filterJson, @"\\", "");
            List<JsonListPageAjax> jsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonListPageAjax>>(decodeUrl);
            if (jsonList != null && jsonList.Count > 0)
            {
                if (!string.IsNullOrEmpty(filter))
                    filter += " AND ";
                else
                    filter += " WHERE ";
                
                

                foreach (JsonListPageAjax json in jsonList)
                {
                    if (json != jsonList[0])
                        filter += " OR ";

                    filter += "(";
                    int count = 0;
                    string Key = json.Field;
                    string Value = json.Value;
                    string Type = json.Type;
                    string[] IDList = Value.Trim(',').Split(',');

                    if (IDList != null && IDList.Length > 0)
                    {
                        if (Type == "equal")
                        {
                            foreach (string item in IDList)
                            {
                                count++;
                                filter += string.Format("{0}=N'{1}'", Key, item);
                                if (count < IDList.Length)
                                    filter += " OR ";
                            }
                        }
                        else
                        {
                            foreach (string item in IDList)
                            {
                                count++;
                                filter += string.Format("{0} LIKE N'%,{1},%'", Key, item);
                                if (count < IDList.Length)
                                    filter += " OR ";
                            }
                        }
                    }
                    filter += ")";
                }  
            }
        }

        if (ConvertUtility.ToInt32(PositionMenuFlag) > 0)
        {
            filter += GenPrefixFilter(filter);
            filter += string.Format("PositionMenuFlag & {0} <> 0", PositionMenuFlag);
        }

        if (ConvertUtility.ToInt32(AttrProductFlag) > 0)
        {
            filter += GenPrefixFilter(filter);
            filter += string.Format("AttrProductFlag & {0} <> 0", AttrProductFlag);
        }

        if (ConvertUtility.ToInt32(flag) > 0)
        {
            filter += GenPrefixFilter(filter);
            filter += string.Format("Flags & {0} = 0", flag);
        }



        if (Hide == "true" || Hide == "false")
        {
            filter += GenPrefixFilter(filter);

            if (Hide == "true")
                filter += string.Format("(Hide is null OR Hide=1)");
            else
                filter += string.Format("(Hide is null OR Hide=0)");
        }
        //if (!string.IsNullOrEmpty(Filter))
        //{
        //    if (!string.IsNullOrEmpty(filter))
        //        filter += " AND ";
        //    else
        //        filter += " WHERE ";

        //    filter += "(";
        //    filter += string.Format("Tags Like N'%,{0},%'", Filter);
        //    filter += ")";
        //}

        if (!string.IsNullOrEmpty(categoryid))
        {
            if (!string.IsNullOrEmpty(filter))
                filter += " AND ";
            else
                filter += " WHERE ";

            filter += "(";
            filter += string.Format("CategoryIDList Like N'%,{0},%'", categoryid);
            filter += ")";
        }

        pagingSql = string.Format(@" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", pageSize * (pageIndex - 1), pageSize);



    }

    protected void BindData()
    {
        if (RemoveID > 0)
        {
            CacheUtility.PurgeCacheItems(tableSql);

            using (var db = SqlService.GetSqlService())
            {
                DataTable dataTable = SqlHelper.SQLToDataTable(tableSql, "", "ID=" + RemoveID);
                if (dataTable != null && dataTable.Rows.Count > 0)
                    SqlHelper.LogsToDatabase(dataTable, tableSql, Utils.GetFolderControlAdmin(), controlName, 2, Request.RawUrl);

                string sqlQuery = string.Format(" DELETE FROM {0} WHERE ID={1}", tableSql, RemoveID);
                db.ExecuteSql(sqlQuery);

                if (tableSql == "tblCategories" || tableSql == "tblArticle" || tableSql == "tblProducts") // Xoá Trong Bảng URL
                {
                    string filter = "ContentID='blabla'";
                    if (tableSql == "tblArticle")
                        filter = string.Format("ContentID='{0}' AND Moduls=N'{1}'", RemoveID, "article_detail");
                    else if (tableSql == "tblProducts")
                        filter = string.Format("ContentID='{0}' AND Moduls=N'{1}'", RemoveID, "product_detail");
                    else if (tableSql == "tblCategories")
                        filter = string.Format("ContentID='{0}' AND (Moduls=N'{1}' OR Moduls=N'{2}' OR Moduls=N'{3}')", RemoveID, "category_product", "category_article", "category_content");
                    string sqlQueryUrl = string.Format(" DELETE FROM {0} WHERE {1}", "tblUrl", filter);
                    db.ExecuteSql(sqlQueryUrl);
                }
            }
        }
        else
        {


            if (action == "getTotalRecord")
            {
                if (Utils.CommaSQLAdd(fieldSql).ToLower().Contains(",parentid,"))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " AND ";
                    else
                        filter += " WHERE ";
                    filter += "ParentID=0";
                }

                int count = 0;
                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT COUNT(ID) as Tong FROM {0} {1}", tableSql, filter);
                    DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
                    count = ConvertUtility.ToInt32(dt.Rows[0]["Tong"]);
                }

                Response.Clear();
                Response.Write(count);
                Response.End();
            }
            else if (Level.ToLower() == "true")
            {
                filter += GenPrefixFilter(filter);
                filter += "ParentID=0";

                using (var dbx = SqlService.GetSqlService())
                {
                    DataTable dt = new DataTable();
                    string sqlQuery = string.Format("SELECT {0} FROM {1} {2} {3} {4}", fieldSql, tableSql, filter, SortOrder, pagingSql);
                    dt = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataTable = dt.Clone();
                        dataTable.Columns.Add("Level_Bullet", typeof(String));
                        dt.Columns.Add("Level_Bullet", typeof(String));

                        foreach (DataRow dr0 in dt.Rows)
                        {
                            foreach (DataColumn column in dt.Columns)
                            {
                                if (column.ColumnName == "Level_Bullet")
                                    dr0.SetField(column, "0");
                            }

                            dataTable.ImportRow(dr0);

                            string sqlQuery1 = string.Format("SELECT {0} FROM {1} Where ParentID={2} {3}", fieldSql, tableSql, dr0["ID"].ToString(), SortOrder);
                            DataTable dt1 = new DataTable();
                            dt1 = dbx.ExecuteSqlDataTable(sqlQuery1);

                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                dt1.Columns.Add("Level_Bullet", typeof(String));

                                foreach (DataRow dr1 in dt1.Rows)
                                {
                                    foreach (DataColumn column1 in dt1.Columns)
                                    {
                                        if (column1.ColumnName == "Level_Bullet")
                                            dr1.SetField(column1, "1");
                                    }
                                    dataTable.ImportRow(dr1);

                                    string sqlQuery2 = string.Format("SELECT {0} FROM {1} Where ParentID={2} {3}", fieldSql, tableSql, dr1["ID"].ToString(), SortOrder);
                                    DataTable dt2 = new DataTable();
                                    dt2 = dbx.ExecuteSqlDataTable(sqlQuery2);
                                    if (dt2 != null && dt2.Rows.Count > 0)
                                    {
                                        dt2.Columns.Add("Level_Bullet", typeof(String));
                                        foreach (DataRow dr2 in dt2.Rows)
                                        {
                                            foreach (DataColumn column2 in dt2.Columns)
                                            {
                                                if (column2.ColumnName == "Level_Bullet")
                                                    dr2.SetField(column2, "2");
                                            }
                                            dataTable.ImportRow(dr2);



                                            string sqlQuery3 = string.Format("SELECT {0} FROM {1} Where ParentID={2} {3}", fieldSql, tableSql, dr2["ID"].ToString(), SortOrder);
                                            DataTable dt3 = new DataTable();
                                            dt3 = dbx.ExecuteSqlDataTable(sqlQuery3);
                                            if (dt3 != null && dt3.Rows.Count > 0)
                                            {
                                                dt3.Columns.Add("Level_Bullet", typeof(String));

                                                foreach (DataRow dr3 in dt3.Rows)
                                                {
                                                    foreach (DataColumn column3 in dt3.Columns)
                                                    {
                                                        if (column3.ColumnName == "Level_Bullet")
                                                            dr3.SetField(column3, "3");
                                                    }
                                                    dataTable.ImportRow(dr3);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (Level.ToLower() == "false")
            {

                filter += GenPrefixFilter(filter);
                string FolderCategory = CookieUtility.GetValueFromCookie("FolderCategory" + tableSql);// RequestHelper.GetString("FolderCategory", "0");
                if (!string.IsNullOrEmpty(FolderCategory))
                    filter += "ParentID=" + FolderCategory;
                else
                    filter += "ParentID=0";
                //Response.Write("FolderCategory: " + FolderCategory + "<br />");
                //CookieUtility.SetValueToCookie("FolderCategory", null);
                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT {0} FROM {1} {2} {3} {4}", fieldSql, tableSql, filter, SortOrder, pagingSql);
                    dataTable = dbx.ExecuteSqlDataTable(sqlQuery);
                }
            }
            else if (tableSql == C.PRODUCT_TABLE && !Utils.IsNullOrEmpty(Keyword))
            {
                dataTable = Utils.SearchProduct(Keyword, fieldSql, 100);
            }
            else
            {

                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT {0} FROM {1} {2} {3} {4}", fieldSql, tableSql, filter, SortOrder, pagingSql);
                    dataTable = dbx.ExecuteSqlDataTable(sqlQuery);
                }
            }

        }
    }


    protected string GenPrefixFilter(string filter)
    {
        string returnStr = "";
        if (!string.IsNullOrEmpty(filter))
            returnStr = " AND ";
        else
            returnStr = " WHERE ";
        return returnStr;
    }
}