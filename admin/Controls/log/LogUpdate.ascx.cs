using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
public partial class admin_Controls_log_LogUpdate : System.Web.UI.UserControl
{


    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table= "tblLogs";
    #endregion

    #region BindData

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

            using (var db = SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("SELECT * FROM tblLogs Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                }
            }
        }
    }

    #endregion

    #region Update Database
    protected void UpdateDatabase()
    {
        if (!String.IsNullOrEmpty(click_action) && (click_action == "restore"))
        {
            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["Control"] = Utils.KillChars(Request.Form["control"]);
            hashtable["Json_Content"] = Utils.KillChars(Request.Form["json_content"]);
            hashtable["TableSql"] = Utils.KillChars(Request.Form["tablesql"]);
            hashtable["Link"] = Utils.KillChars(Request.Form["link"]);

            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblLogs] SET [Name]=@Name, [Control]=@Control, [Json_Content]=@Json_Content, [TableSql]=@TableSql, [Link]=@Link,  WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblLogs] ([Name],[Control],[Json_Content],[TableSql],[Link],[CreatedDate],[CreatedBy]) OUTPUT INSERTED.ID VALUES (@Name,@Control,@Json_Content,@TableSql,@Link,@CreatedDate,@CreatedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@Control", System.Data.SqlDbType.NVarChar, hashtable["Control"].ToString());
                db.AddParameter("@Json_Content", System.Data.SqlDbType.NVarChar, hashtable["Json_Content"].ToString());
                db.AddParameter("@TableSql", System.Data.SqlDbType.NVarChar, hashtable["TableSql"].ToString());
                db.AddParameter("@Link", System.Data.SqlDbType.NVarChar, hashtable["Link"].ToString());

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
                    CookieUtility.SetValueToCookie("notice", "insert_success");
                }

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(IsUpdate), Request.RawUrl);

            }
        }
      
        ActionAfterUpdate();
    }
    #endregion

    #region Orther Action
    protected void ActionAfterUpdate()
    {
        if (click_action == "cancel")
        {
            Response.Redirect("/admin/" + control + "/view/");
        }
        else if (click_action == "restore")
        {
            BindData();
        }
    }


    #endregion
}