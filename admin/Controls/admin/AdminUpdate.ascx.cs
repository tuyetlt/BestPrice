using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;

public partial class admin_Controls_AdminUpdate : System.Web.UI.UserControl
{

    #region Variable
    public DataRow dr;
    public DataTable dtPermission;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table= "tblAdminUser";
    public string Attr_Config_UrlList = string.Empty, Attr_Config_NameList = string.Empty;

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
        GetPermission();
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
                string sqlQuery = string.Format("SELECT * FROM tblAdminUser Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                    Utils.GetAttrByID(dr["AttributeConfigIDList"].ToString(), "tblAttributeConfigs", false, ref Attr_Config_UrlList, ref Attr_Config_NameList);
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

            Utils.GetAttrByID(dr["AttributeConfigIDList"].ToString(), "tblAttributeConfigs", true, ref Attr_Config_UrlList, ref Attr_Config_NameList);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["UserName"] = Utils.KillChars(Request.Form["username"]);
            hashtable["Password"] = Crypto.EncryptData(Crypto.KeyCrypto, Request.Form["password"].Trim());
            hashtable["Email"] = Utils.KillChars(Request.Form["email"]);
            hashtable["Gallery"] = Utils.KillChars(Request.Form["gallery"]);
            hashtable["Permission"] = Utils.KillChars(Request.Form["permission"]);
            hashtable["Token"] = Utils.KillChars(Request.Form["token"]);
            hashtable["AttributeConfigIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributeconfigidlist"]));
            hashtable["AttributeConfigUrlList"] = Attr_Config_UrlList;
            hashtable["MetaKeyword"] = Utils.KillChars(Request.Form["metakeyword"]);
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);

            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblAdminUser] SET [Name]=@Name, [UserName]=@UserName, [Password]=@Password, [Email]=@Email, [Permission]=@Permission, [Token]=@Token, [AttributeConfigIDList]=@AttributeConfigIDList, [AttributeConfigUrlList]=@AttributeConfigUrlList, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy, [Gallery]=@Gallery WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblAdminUser] ([Name],[UserName],[Password],[Email],[Permission],[Token],[AttributeConfigIDList],[AttributeConfigUrlList],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy],[Gallery]) OUTPUT INSERTED.ID VALUES (@Name,@UserName,@Password,@Email,@Permission,@Token,@AttributeConfigIDList,@AttributeConfigUrlList,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy,@Gallery)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@UserName", System.Data.SqlDbType.NVarChar, hashtable["UserName"].ToString());
                db.AddParameter("@Password", System.Data.SqlDbType.NVarChar, hashtable["Password"].ToString());
                db.AddParameter("@Email", System.Data.SqlDbType.NVarChar, hashtable["Email"].ToString());
                db.AddParameter("@Permission", System.Data.SqlDbType.NVarChar, hashtable["Permission"].ToString());
                db.AddParameter("@Token", System.Data.SqlDbType.NVarChar, hashtable["Token"].ToString());
                db.AddParameter("@AttributeConfigIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigIDList"].ToString());
                db.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigUrlList"].ToString());
                db.AddParameter("@MetaKeyword", System.Data.SqlDbType.NVarChar, hashtable["MetaKeyword"].ToString());
                db.AddParameter("@MetaDescription", System.Data.SqlDbType.NVarChar, hashtable["MetaDescription"].ToString());
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                db.AddParameter("@Gallery", System.Data.SqlDbType.NVarChar, hashtable["Gallery"].ToString());

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(IsUpdate), Request.RawUrl);


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


    protected void GetPermission()
    {
        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT * FROM tblAdminPermission");
            dtPermission = db.ExecuteSqlDataTable(sqlQuery);
        }
    }
}