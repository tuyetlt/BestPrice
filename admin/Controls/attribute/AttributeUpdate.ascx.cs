using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;


public partial class admin_Controls_AttributeUpdate : System.Web.UI.UserControl
{


    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table= "tblAttributes";
    public string image_1 = C.NO_IMG_PATH;
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
            dr = db.NewRow("tblAttributes");
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
                string sqlQuery = string.Format("SELECT * FROM tblAttributes Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];

                    if(IDCopy>0)
                    {
                        dr["FriendlyUrl"] = "";
                    }
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
            hashtable["NameDisplay"] = Utils.KillChars(Request.Form["namedisplay"]);
            hashtable["Description"] = Utils.KillChars(Request.Form["description"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["ParentID"] = Utils.KillChars(Request.Form["parentid"]);
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Flags"] = Utils.KillChars(Request.Form["flags"]);
            hashtable["Image_1"] = Utils.KillChars(Request.Form["image_1"]);
            hashtable["Image_2"] = Utils.KillChars(Request.Form["image_2"]);
            hashtable["Image_3"] = Utils.KillChars(Request.Form["image_3"]);
            hashtable["Icon"] = Utils.KillChars(Request.Form["icon"]);

            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblAttributes] SET [Name]=@Name, [NameDisplay]=@NameDisplay, [Description]=@Description, [FriendlyUrl]=@FriendlyUrl, [ParentID]=@ParentID, [Sort]=@Sort, [Flags]=@Flags, [Image_1]=@Image_1, [Image_2]=@Image_2, [Image_3]=@Image_3, [Icon]=@Icon, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblAttributes] ([Name],[NameDisplay],[Description],[FriendlyUrl],[ParentID],[Sort],[Flags],[Image_1],[Image_2],[Image_3],[Icon],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@NameDisplay,@Description,@FriendlyUrl,@ParentID,@Sort,@Flags,@Image_1,@Image_2,@Image_3,@Icon,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@NameDisplay", System.Data.SqlDbType.NVarChar, hashtable["NameDisplay"].ToString());
                db.AddParameter("@Description", System.Data.SqlDbType.NVarChar, hashtable["Description"].ToString());
                db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrl"].ToString());
                db.AddParameter("@ParentID", System.Data.SqlDbType.Int, hashtable["ParentID"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
                db.AddParameter("@Image_1", System.Data.SqlDbType.NVarChar, hashtable["Image_1"].ToString());
                db.AddParameter("@Image_2", System.Data.SqlDbType.NVarChar, hashtable["Image_2"].ToString());
                db.AddParameter("@Image_3", System.Data.SqlDbType.NVarChar, hashtable["Image_3"].ToString());
                db.AddParameter("@Icon", System.Data.SqlDbType.NVarChar, hashtable["Icon"].ToString());
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


}