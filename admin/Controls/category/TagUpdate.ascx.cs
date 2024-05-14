using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;

public partial class admin_Controls_category_TagUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblCategories";
    public string image_1 = C.NO_IMG_PATH, image_2 = C.NO_IMG_PATH, image_3 = C.NO_IMG_PATH, icon = C.NO_IMG_PATH;

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
                string sqlQuery = string.Format("SELECT * FROM tblCategories Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                    if (!string.IsNullOrEmpty(ds.Rows[0]["image_1"].ToString()))
                        image_1 = ds.Rows[0]["image_1"].ToString();
                    if (!string.IsNullOrEmpty(ds.Rows[0]["image_2"].ToString()))
                        image_2 = ds.Rows[0]["image_2"].ToString();
                    if (!string.IsNullOrEmpty(ds.Rows[0]["image_3"].ToString()))
                        image_3 = ds.Rows[0]["image_3"].ToString();
                    if (!string.IsNullOrEmpty(ds.Rows[0]["Icon"].ToString()))
                        icon = ds.Rows[0]["Icon"].ToString();
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

            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;


            CacheUtility.PurgeCacheItems(table);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["Link"] = Utils.KillChars(Request.Form["link"]);
            hashtable["ParentID"] = Utils.KillChars(Request.Form["parentid"]);
            hashtable["LevelNumber"] = Utils.KillChars(Request.Form["levelnumber"]);
            hashtable["ParentIDList"] = Utils.KillChars(Request.Form["parentidlist"]);
            hashtable["Image_1"] = Utils.KillChars(Request.Form["image_1"]);
            hashtable["Image_2"] = Utils.KillChars(Request.Form["image_2"]);
            hashtable["Image_3"] = Utils.KillChars(Request.Form["image_3"]);
            hashtable["Icon"] = Utils.KillChars(Request.Form["icon"]);
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Sort1"] = Utils.KillChars(Request.Form["sort1"]);
            hashtable["Sort2"] = Utils.KillChars(Request.Form["sort2"]);
            hashtable["Moduls"] = Utils.KillChars(Request.Form["moduls"]);

            hashtable["Description"] = Utils.KillChars(Request.Form["description"]);
            hashtable["LongDescription"] = Utils.KillChars(Request.Form["longdescription"]);

            hashtable["LinkTypeMenuFlag"] = "";
            hashtable["PositionMenuFlag"] = "";
            hashtable["AttrMenuFlag"] = "";

            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Position"] = Utils.KillChars(Request.Form["position"]);
            hashtable["AttributesIDList"] = Utils.KillChars(Request.Form["attributesidlist"]);
            hashtable["AttributesUrlList"] = Utils.KillChars(Request.Form["attributesurllist"]);
            hashtable["AttributeConfigIDList"] = Utils.CommaSQLAdd(Request.Form["attributeconfigidlist"]);
            hashtable["AttributeConfigUrlList"] = "";
            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);
            hashtable["MetaKeyword"] = Utils.KillChars(Request.Form["metakeyword"]);
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);
            hashtable["Temp1"] = Utils.KillChars(Request.Form["temp1"]);
            hashtable["Temp2"] = Utils.KillChars(Request.Form["temp2"]);
            hashtable["Temp3"] = Utils.KillChars(Request.Form["temp3"]);
            hashtable["Temp4"] = Utils.KillChars(Request.Form["temp4"]);
            hashtable["Temp5"] = Utils.KillChars(Request.Form["temp5"]);
            hashtable["Temp6"] = Utils.KillChars(Request.Form["temp6"]);
            hashtable["Temp7"] = Utils.KillChars(Request.Form["temp7"]);
            hashtable["TempModuls"] = Utils.KillChars(Request.Form["tempmoduls"]);
            CacheUtility.PurgeCacheItems(table);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblCategories] SET [Name]=@Name, [FriendlyUrl]=@FriendlyUrl, [Description]=@Description, [LongDescription]=@LongDescription, [Link]=@Link, [ParentID]=@ParentID, [ParentIDList]=@ParentIDList, [LevelNumber]=@LevelNumber, [Image_1]=@Image_1, [Image_2]=@Image_2, [Image_3]=@Image_3, [Icon]=@Icon, [Sort]=@Sort, [Sort1]=@Sort1, [Sort2]=@Sort2, [Moduls]=@Moduls, [LinkTypeMenuFlag]=@LinkTypeMenuFlag, [PositionMenuFlag]=@PositionMenuFlag, [AttrMenuFlag]=@AttrMenuFlag, [Hide]=@Hide, [AttributesIDList]=@AttributesIDList, [AttributesUrlList]=@AttributesUrlList, [AttributeConfigIDList]=@AttributeConfigIDList, [AttributeConfigUrlList]=@AttributeConfigUrlList, [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy, [Temp1]=@Temp1, [Temp2]=@Temp2, [Temp3]=@Temp3, [Temp4]=@Temp4, [Temp5]=@Temp5, [Temp6]=@Temp6, [Temp7]=@Temp7, [TempModuls]=@TempModuls WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblCategories] ([Name],[FriendlyUrl],[Description],[LongDescription],[Link],[ParentID],[ParentIDList],[LevelNumber],[Image_1],[Image_2],[Image_3],[Icon],[Sort],[Sort1],[Sort2],[Moduls],[LinkTypeMenuFlag],[PositionMenuFlag],[AttrMenuFlag],[Hide],[AttributesIDList],[AttributesUrlList],[AttributeConfigIDList],[AttributeConfigUrlList],[MetaTitle],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy],[Temp1],[Temp2],[Temp3],[Temp4],[Temp5],[Temp6],[Temp7],[TempModuls]) OUTPUT INSERTED.ID VALUES (@Name,@FriendlyUrl,@Description,@LongDescription,@Link,@ParentID,@ParentIDList,@LevelNumber,@Image_1,@Image_2,@Image_3,@Icon,@Sort,@Sort1,@Sort2,@Moduls,@LinkTypeMenuFlag,@PositionMenuFlag,@AttrMenuFlag,@Hide,@AttributesIDList,@AttributesUrlList,@AttributeConfigIDList,@AttributeConfigUrlList,@MetaTitle,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy,@Temp1,@Temp2,@Temp3,@Temp4,@Temp5,@Temp6,@Temp7,@TempModuls)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrl"].ToString());
                db.AddParameter("@Description", System.Data.SqlDbType.NVarChar, hashtable["Description"].ToString());
                db.AddParameter("@LongDescription", System.Data.SqlDbType.NVarChar, hashtable["LongDescription"].ToString());
                db.AddParameter("@Link", System.Data.SqlDbType.NVarChar, hashtable["Link"].ToString());
                db.AddParameter("@ParentID", System.Data.SqlDbType.Int, hashtable["ParentID"].ToString());
                db.AddParameter("@ParentIDList", System.Data.SqlDbType.NVarChar, hashtable["ParentIDList"].ToString());
                db.AddParameter("@LevelNumber", System.Data.SqlDbType.Int, hashtable["LevelNumber"].ToString());
                db.AddParameter("@Image_1", System.Data.SqlDbType.NVarChar, hashtable["Image_1"].ToString());
                db.AddParameter("@Image_2", System.Data.SqlDbType.NVarChar, hashtable["Image_2"].ToString());
                db.AddParameter("@Image_3", System.Data.SqlDbType.NVarChar, hashtable["Image_3"].ToString());
                db.AddParameter("@Icon", System.Data.SqlDbType.NVarChar, hashtable["Icon"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Sort1", System.Data.SqlDbType.Int, hashtable["Sort1"].ToString());
                db.AddParameter("@Sort2", System.Data.SqlDbType.Int, hashtable["Sort2"].ToString());
                db.AddParameter("@Moduls", System.Data.SqlDbType.NVarChar, hashtable["Moduls"].ToString());
                db.AddParameter("@LinkTypeMenuFlag", System.Data.SqlDbType.Int, hashtable["LinkTypeMenuFlag"].ToString());
                db.AddParameter("@PositionMenuFlag", System.Data.SqlDbType.Int, hashtable["PositionMenuFlag"].ToString());
                db.AddParameter("@AttrMenuFlag", System.Data.SqlDbType.Int, hashtable["AttrMenuFlag"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"].ToString());
                db.AddParameter("@AttributesIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributesIDList"].ToString());
                db.AddParameter("@AttributesUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributesUrlList"].ToString());
                db.AddParameter("@AttributeConfigIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigIDList"].ToString());
                db.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigUrlList"].ToString());
                db.AddParameter("@MetaTitle", System.Data.SqlDbType.NVarChar, hashtable["MetaTitle"].ToString());
                db.AddParameter("@MetaKeyword", System.Data.SqlDbType.NVarChar, hashtable["MetaKeyword"].ToString());
                db.AddParameter("@MetaDescription", System.Data.SqlDbType.NVarChar, hashtable["MetaDescription"].ToString());
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                db.AddParameter("@Temp1", System.Data.SqlDbType.Bit, hashtable["Temp1"].ToString());
                db.AddParameter("@Temp2", System.Data.SqlDbType.Bit, hashtable["Temp2"].ToString());
                db.AddParameter("@Temp3", System.Data.SqlDbType.Bit, hashtable["Temp3"].ToString());
                db.AddParameter("@Temp4", System.Data.SqlDbType.Bit, hashtable["Temp4"].ToString());
                db.AddParameter("@Temp5", System.Data.SqlDbType.Bit, hashtable["Temp5"].ToString());
                db.AddParameter("@Temp6", System.Data.SqlDbType.Bit, hashtable["Temp6"].ToString());
                db.AddParameter("@Temp7", System.Data.SqlDbType.Bit, hashtable["Temp7"].ToString());
                db.AddParameter("@TempModuls", System.Data.SqlDbType.NVarChar, hashtable["TempModuls"].ToString());

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

    protected void GetAttrByID(string IDList, string table, bool isSave, ref string UrlList, ref string NameList)
    {
        string UrlStr = string.Empty;
        string NameStr = string.Empty;

        if (!string.IsNullOrEmpty(IDList))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("Select Name, FriendlyUrl From {0} WHERE ID IN ({1})", table, IDList.Trim(','));
                DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UrlStr += dr["FriendlyUrl"].ToString() + ",";
                        NameStr += dr["Name"].ToString() + ",";
                    }
                }
                else
                {
                    UrlStr = string.Empty;
                    NameStr = string.Empty;
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
        else
        {
            UrlStr = ",";
            NameStr = ",";
        }
    }
}