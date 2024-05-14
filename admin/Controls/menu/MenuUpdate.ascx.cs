using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
public partial class admin_Controls_MenuUpdate : System.Web.UI.UserControl
{
    #region Variable
    public string arr_img_js;
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    DatabaseHelper db1 = new DatabaseHelper();
    public bool IsUpdate = false;
    int outID = 0;
    public string jsonCategory = "";
    int ID = 0, IDCopy = 0;
    public string control = "", icon="", field="";
    public string click_action, controlName, table= "tblAdminMenu";
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
    }

    protected void BindData()
    {
        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT Top 1 * FROM {0} Where Control=N'{1}'", table, control);
            var ds = db.ExecuteSqlDataTable(sqlQuery);
            if (ds.Rows.Count > 0)
            {
                dr = ds.Rows[0];
            }
        }

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

            using (var db = SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("SELECT * FROM {0} Where ID='{1}'", table, SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                }
            }
        }
    }

    protected void UpdateDatabase()
    {
        click_action = Request.Form["done"];

        if (!String.IsNullOrEmpty(click_action) && (click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd" || click_action == "save"))
        {
            CacheUtility.PurgeCacheItems(table);

            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;

            hashtable["id"] = Utils.KillChars(Request.Form["id"]);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["ShortName"] = Utils.KillChars(Request.Form["shortname"]);
            hashtable["ParentID"] = Utils.KillChars(Request.Form["parentid"]);
            hashtable["Control"] = Utils.KillChars(Request.Form["control"]);
            hashtable["SQLNameTable"] = Utils.KillChars(Request.Form["sqlnametable"]);
            hashtable["FieldSql"] = Request.Form["fieldsql"];
            hashtable["ModulFilter"] = Request.Form["modulfilter"];
            hashtable["Icon"] = Request.Form["icon"];
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);

            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblAdminMenu] SET [Name]=@Name, [ShortName]=@ShortName, [ParentID]=@ParentID, [Control]=@Control, [SQLNameTable]=@SQLNameTable, [FieldSql]=@FieldSql, [ModulFilter]=@ModulFilter, [Icon]=@Icon, [Sort]=@Sort, [Hide]=@Hide, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblAdminMenu] ([Name],[ShortName],[ParentID],[Control],[SQLNameTable],[FieldSql],[ModulFilter],[Icon],[Sort],[Hide],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@ShortName,@ParentID,@Control,@SQLNameTable,@FieldSql,@ModulFilter,@Icon,@Sort,@Hide,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }


                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@ShortName", System.Data.SqlDbType.NVarChar, hashtable["ShortName"].ToString());
                db.AddParameter("@ParentID", System.Data.SqlDbType.Int, hashtable["ParentID"].ToString());
                db.AddParameter("@Control", System.Data.SqlDbType.NVarChar, hashtable["Control"].ToString());
                db.AddParameter("@SQLNameTable", System.Data.SqlDbType.NVarChar, hashtable["SQLNameTable"].ToString());
                db.AddParameter("@FieldSql", System.Data.SqlDbType.NVarChar, hashtable["FieldSql"].ToString());
                db.AddParameter("@ModulFilter", System.Data.SqlDbType.NVarChar, hashtable["ModulFilter"].ToString());
                db.AddParameter("@Icon", System.Data.SqlDbType.NVarChar, hashtable["Icon"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"]);

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


}