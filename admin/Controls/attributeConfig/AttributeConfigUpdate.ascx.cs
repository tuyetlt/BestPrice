using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;

public partial class admin_Controls_AttributeConfigUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table= "tblAttributeConfigs";
    public string ModulsName = string.Empty, ModulsUrl = string.Empty;

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
                string sqlQuery = string.Format("SELECT * FROM tblAttributeConfigs Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                    GetAttrByID(dr["Moduls"].ToString(), table, false, ref ModulsUrl, ref ModulsName);
                }
            }
        }
    }

    #endregion

    #region Update Database
    protected void UpdateDatabase()
    {
        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {
            CacheUtility.PurgeCacheItems(table);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["ParentID"] = Utils.KillChars(Request.Form["parentid"]);
            hashtable["Moduls"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["moduls"]));

            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblAttributeConfigs] SET [Name]=@Name, [FriendlyUrl]=@FriendlyUrl, [ParentID]=@ParentID, [Moduls]=@Moduls WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblAttributeConfigs] ([Name],[FriendlyUrl],[ParentID],[Moduls]) OUTPUT INSERTED.ID VALUES (@Name,@FriendlyUrl,@ParentID,@Moduls)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrl"].ToString());
                db.AddParameter("@ParentID", System.Data.SqlDbType.Int, hashtable["ParentID"].ToString());
                db.AddParameter("@Moduls", System.Data.SqlDbType.NVarChar,hashtable["Moduls"].ToString());

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
    #endregion

    #region Orther Action
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
  

    #endregion


    protected void GetAttrByID(string IDList, string table, bool isSave, ref string UrlList, ref string NameList)
    {
        string UrlStr = string.Empty;
        string NameStr = string.Empty;

        if (!string.IsNullOrEmpty(IDList))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string filter = "";
                string[] arr = IDList.Trim(',').Split(',');
                int count = 0;
                if(arr != null && arr.Length > 0)
                {
                    foreach(string str in arr)
                    {
                        if (count == 0)
                            filter = "Moduls='all'";
                            filter += " OR ";
                        filter += "FriendlyUrl = N'" + str + "'";
                        count++;
                    }
                }

                string sqlQuery = string.Format("Select Name, FriendlyUrl From {0} WHERE {1}", table, filter);
                DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UrlStr += dr["FriendlyUrl"].ToString() + ",";
                        NameStr += dr["Name"].ToString() + ",";
                    }
                }
            }

            if (isSave)
            {
                UrlList = Utils.CommaSQLAdd(UrlStr);
                NameList = Utils.CommaSQLAdd(NameStr);
            }
            else
            {
                UrlList = Utils.CommaSQLRemove(UrlStr);
                NameList = Utils.CommaSQLRemove(NameStr);
            }
        }
    }

}