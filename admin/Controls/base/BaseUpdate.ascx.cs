using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
using System.Linq;

public partial class admin_Controls_base_BaseUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblBase";
    public string image_1 = C.NO_IMG_PATH, image_2 = C.NO_IMG_PATH, image_3 = C.NO_IMG_PATH, icon = C.NO_IMG_PATH;

    public BaseTableFlag Flag;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        ProccessParameter();

        if (!IsPostBack)
        {
            BindData();
            UpdateDatabase();
        }
    }

    protected void ProccessParameter()
    {
        ID = RequestHelper.GetInt("id", 0);
        IDCopy = RequestHelper.GetInt("idCopy", 0);
        control = ConvertUtility.ToString(Page.RouteData.Values["control"]).ToLower();
        click_action = Request.Form["done"];
    }

    protected void BindData()
    {
        using (var db = SqlService.GetSqlService())
        {
            dr = db.NewRow(table);
        }

        int SqlFilterID = 0;
        if (ID > 0 || IDCopy > 0)
        {
            if (ID > 0)
            {
                IsUpdate = true;
                SqlFilterID = ID;
            }
            else
            {
                SqlFilterID = IDCopy;
            }

        }

        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT * FROM {0} Where ID='{1}'", table, SqlFilterID);
            var ds = db.ExecuteSqlDataTable(sqlQuery);
            if (ds.Rows.Count > 0)
            {
                dr = ds.Rows[0];
            }
        }

        int FlagInt = 0;
        DataTable dtFlag = SqlHelper.SQLToDataTable(table, "Flags", string.Format("ID='{0}'", SqlFilterID), "ID", 1, 1);
        if (Utils.CheckExist_DataTable(dtFlag))
        {
            FlagInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["Flags"]);
        }
        Flag = (BaseTableFlag)FlagInt;
    }

    protected void UpdateDatabase()
    {
        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {
            #region Flags
            string flags = Request.Form["flag"];

            int Flag_INT = 0;

            if (!string.IsNullOrEmpty(flags))
            {
                if (flags == BaseTableFlag.Manufacturer.ToString())
                {
                    Flag_INT += (int)BaseTableFlag.Manufacturer;
                }
                if (flags == BaseTableFlag.Social.ToString())
                {
                    Flag_INT += (int)BaseTableFlag.Social;
                }
                if (flags == BaseTableFlag.Partner.ToString())
                {
                    Flag_INT += (int)BaseTableFlag.Partner;
                }
                if (flags == BaseTableFlag.Support.ToString())
                {
                    Flag_INT += (int)BaseTableFlag.Support;
                }
                if (flags == BaseTableFlag.Service.ToString())
                {
                    Flag_INT += (int)BaseTableFlag.Service;
                }
            }

            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;

            #endregion


            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["Phone"] = Utils.KillChars(Request.Form["phone"]);
            hashtable["NickChat"] = Utils.KillChars(Request.Form["nickchat"]);
            hashtable["Alt"] = Utils.KillChars(Request.Form["alt"]);
            hashtable["Gallery"] = Utils.KillChars(Request.Form["gallery"]);
            hashtable["Image_1"] = Utils.KillChars(Request.Form["image_1"]);
            hashtable["Image_2"] = Utils.KillChars(Request.Form["image_2"]);
            hashtable["Image_3"] = Utils.KillChars(Request.Form["image_3"]);
            hashtable["Icon"] = Utils.KillChars(Request.Form["icon"]);
            hashtable["ParentID"] = Utils.KillChars(Request.Form["parentid"]);
            hashtable["Sort"] = ConvertUtility.ToInt32(Request.Form["sort"]);
            hashtable["Flags"] = Flag_INT;
            hashtable["Description"] = Utils.KillChars(Request.Form["description"]);


            CacheUtility.PurgeCacheItems(table);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblBase] SET [Name]=@Name, [Phone]=@Phone, [NickChat]=@NickChat, [Alt]=@Alt, [Gallery]=@Gallery, [Image_1]=@Image_1, [Image_2]=@Image_2, [Image_3]=@Image_3, [Icon]=@Icon, [ParentID]=@ParentID, [Sort]=@Sort, [Flags]=@Flags, [Hide]=@Hide, [Description]=@Description, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblBase] ([Name],[Phone],[NickChat],[Alt],[Gallery],[Image_1],[Image_2],[Image_3],[Icon],[ParentID],[Sort],[Flags],[Hide],[Description],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@Phone,@NickChat,@Alt,@Gallery,@Image_1,@Image_2,@Image_3,@Icon,@ParentID,@Sort,@Flags,@Hide,@Description,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@Phone", System.Data.SqlDbType.NVarChar, hashtable["Phone"].ToString());
                db.AddParameter("@NickChat", System.Data.SqlDbType.NVarChar, hashtable["NickChat"].ToString());
                db.AddParameter("@Alt", System.Data.SqlDbType.NVarChar, hashtable["Alt"].ToString());
                db.AddParameter("@Gallery", System.Data.SqlDbType.NVarChar, hashtable["Gallery"].ToString());
                db.AddParameter("@Image_1", System.Data.SqlDbType.NVarChar, hashtable["Image_1"].ToString());
                db.AddParameter("@Image_2", System.Data.SqlDbType.NVarChar, hashtable["Image_2"].ToString());
                db.AddParameter("@Image_3", System.Data.SqlDbType.NVarChar, hashtable["Image_3"].ToString());
                db.AddParameter("@Icon", System.Data.SqlDbType.NVarChar, hashtable["Icon"].ToString());
                db.AddParameter("@ParentID", System.Data.SqlDbType.Int, hashtable["ParentID"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"]);
                db.AddParameter("@Description", System.Data.SqlDbType.NVarChar, hashtable["Description"].ToString());
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);


                if (IsUpdate)
                {
                    db.AddParameter("@ID", System.Data.SqlDbType.Int, ID);
                    db.ExecuteSql(sqlQuery);

                    if (click_action == "saveandcopy")
                        CookieUtility.SetValueToCookie("notice", "update_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "update_success");
                }
                else
                {
                    db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                    db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                    ID = db.ExecuteSqlScalar<int>(sqlQuery, 0);

                    if (click_action == "saveandcopy")
                        CookieUtility.SetValueToCookie("notice", "insert_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "insert_success");
                }

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(IsUpdate), Request.RawUrl);
                //UpdateTags(hashtable["Name"], "_tagType");
            }
        }
        else if (click_action == "delete")
        {
            CookieUtility.SetValueToCookie("notice", "delete_success");
        }


        ActionAfterUpdate();
    }


    protected void ActionAfterUpdate()
    {
        if (click_action == "saveandback" || click_action == "cancel" || click_action == "delete")
        {
            Response.Redirect(Utils.GetViewControl());
        }
        if (click_action == "saveandadd")
        {
            Response.Redirect(Utils.GetEditControl());
        }
        else if (click_action == "saveandcopy")
        {
            Response.Redirect(Utils.GetEditControl() + "?idCopy=" + ID);
        }
        else if (click_action == "save")
        {
            BindData();
        }
    }



    protected string GetCategoryIDParentList()
    {
        string returnValue = string.Empty;
        string CateofyIDList = Request.Form["categoryidlist"].Trim(',');
        if (string.IsNullOrEmpty(CateofyIDList))
            return returnValue;

        string[] NameArr = CateofyIDList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            string CategoryIDParentList = ",";
            foreach (string CatID in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    int count = 0;
                    int ParentID = ConvertUtility.ToInt32(CatID);
                    while (ParentID > 0 && count < 10)
                    {
                        string sqlQuery = string.Format("SELECT Top 1 * FROM tblCategories Where ID={0}", ParentID);
                        var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                        if (ds != null && ds.Rows.Count > 0)
                        {
                            ParentID = ConvertUtility.ToInt32(ds.Rows[0]["ParentID"]);

                            if (!CategoryIDParentList.Contains("," + ParentID + ",") && ParentID > 0)
                                CategoryIDParentList += ParentID + ",";
                        }
                        else
                        {
                            ParentID = 0;
                        }
                        count++;
                    }
                }
            }
            returnValue = Utils.CommaSQLAdd(CategoryIDParentList.Trim(','));
        }

        return returnValue;
    }





    #region thêm giá trị vào thuộc tính (chưa dùng)
    protected void UpdateAttribute(string NameList, string Type)
    {
        NameList = NameList.Trim();
        if (string.IsNullOrEmpty(NameList))
            return;

        string[] NameArr = NameList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            foreach (string name in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT * FROM tblAttributes Where Name=N'{0}' AND Type=N'{1}'", name.Trim(), Type);
                    var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (ds == null || ds.Rows.Count == 0)
                    {
                        AddAttribute(name, Type);
                    }
                }
            }
        }
    }

    protected void AddAttribute(string Name, string Type)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = "INSERT INTO tblAttributes (ParentID, Name, Type) Values (@ParentID, @Name, @Type)";
                dbx.AddParameter("@ParentID", System.Data.SqlDbType.Int, 0);
                dbx.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
                dbx.AddParameter("@Type", System.Data.SqlDbType.NVarChar, Type);
                dbx.ExecuteSql(sqlQuery);
            }
        }
    }
    #endregion




    #region thêm Tags
    protected void UpdateTags(string NameList, string Type)
    {
        NameList = NameList.Trim();
        if (string.IsNullOrEmpty(NameList))
            return;

        string[] NameArr = NameList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            foreach (string name in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT * FROM tblCategories Where Name=N'{0}' AND AttributeConfigUrlList like N'%,{1},%'", name.Trim(), Type);
                    var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (ds == null || ds.Rows.Count == 0)
                    {
                        AddTags(name, Type);
                    }
                }
            }
        }
    }

    protected void AddTags(string Name, string Type)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            CacheUtility.PurgeCacheItems("tblCategories");

            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = "INSERT INTO tblCategories (ParentID, Name, AttributeConfigUrlList) Values (@ParentID, @Name, @AttributeConfigUrlList)";
                dbx.AddParameter("@ParentID", System.Data.SqlDbType.Int, 0);
                dbx.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
                dbx.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, Type);
                dbx.ExecuteSql(sqlQuery);
            }
        }
    }
    #endregion



    //public string void GetCategory(string IDList)
    //{
    //    string table = "tblCategories";
    //    if (!string.IsNullOrEmpty(IDList))
    //    {
    //        using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
    //        {
    //            string sqlQuery = string.Format("Select Name, FriendlyUrl From {0} WHERE ID IN ({1})", table, IDList.Trim(','));
    //            DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
    //            if (dt.Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dt.Rows)
    //                {
    //                    UrlStr += dr["FriendlyUrl"].ToString() + ",";
    //                    NameStr += dr["Name"].ToString() + ",";
    //                }
    //            }
    //        }

    //        if (isSave)
    //        {
    //            UrlList = Utils.CommaSQLAdd(UrlStr);
    //            NameList = Utils.CommaSQLAdd(NameStr);
    //        }
    //        else
    //        {
    //            UrlList = Utils.CommaSQLRemove(UrlStr);
    //            NameList = Utils.CommaSQLRemove(NameStr);
    //        }
    //    }
    //}
}