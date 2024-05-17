using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
using System.Collections.Generic;

public partial class admin_Controls_CategoryUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblCategories";
    public string image_1 = C.NO_IMG_PATH, image_2 = C.NO_IMG_PATH, image_3 = C.NO_IMG_PATH, image_4 = C.NO_IMG_PATH, icon = C.NO_IMG_PATH;
    public string AttributesUrlList = string.Empty, AttributesNameList = string.Empty, Attr_Config_UrlList = string.Empty, Attr_Config_NameList = string.Empty, CatUrlList = string.Empty, CatNameList = string.Empty;
    public PositionMenuFlag positionFlag;
    public LinkTypeMenuFlag linkTypeFlag;
    public AttrMenuFlag attrMenuFlag;

    public string TagIDList = string.Empty,
        TagUrlList = string.Empty,
        TagNameList = string.Empty,

        HashTagIDList = string.Empty,
        HashTagUrlList = string.Empty,
        HashTagNameList = string.Empty;



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
                    if (!string.IsNullOrEmpty(ds.Rows[0]["image_4"].ToString()))
                        image_4 = ds.Rows[0]["image_4"].ToString();
                    if (!string.IsNullOrEmpty(ds.Rows[0]["Icon"].ToString()))
                        icon = ds.Rows[0]["Icon"].ToString();
                }
            }

            int LinkTypeMenuInt = 0;
            int PositionMenuInt = 0;
            int AttrMenuInt = 0;
            DataTable dtFlag = SqlHelper.SQLToDataTable(table, "LinkTypeMenuFlag, PositionMenuFlag, AttrMenuFlag", string.Format("ID='{0}'", SqlFilterID), "ID", 1, 1);
            if (Utils.CheckExist_DataTable(dtFlag))
            {
                LinkTypeMenuInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["LinkTypeMenuFlag"]);
                PositionMenuInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["PositionMenuFlag"]);
                AttrMenuInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["AttrMenuFlag"]);
            }

            positionFlag = (PositionMenuFlag)PositionMenuInt;
            linkTypeFlag = (LinkTypeMenuFlag)LinkTypeMenuInt;
            attrMenuFlag = (AttrMenuFlag)AttrMenuInt;
            if (linkTypeFlag == LinkTypeMenuFlag.None)
                linkTypeFlag = LinkTypeMenuFlag.Product;
        }
    }

    #endregion

    #region Update Database
    protected void UpdateDatabase()
    {
        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {
            #region Flags
            string MenuHome = Request.Form["MenuHome"];
            string MenuPriority = Request.Form["MenuPriority"];
            string MenuHotIcon = Request.Form["MenuHotIcon"];

            string Main = Request.Form["Main"];
            string Top = Request.Form["Top"];
            string SupperTop = Request.Form["SupperTop"];
            string Bottom = Request.Form["Bottom"];
            string MenuSub = Request.Form["MenuSub"];
            string MenuSubMainHome = Request.Form["MenuSubMainHome"];
            string MenuSubMainHome2 = Request.Form["MenuSubMainHome2"];
            string Style1 = Request.Form["Style1"];
            string Style2 = Request.Form["Style2"];
            string OpenNewWindows = Request.Form["OpenNewWindows"];
            string PriorityAds = Request.Form["PriorityAds"];


            string linktype = Request.Form["linktypeflag"].ToString();
            string position = Request.Form["positionmenuflag"].ToString();


            int PositionMenuFlag_INT = 0;
            PositionMenuFlag positionMenuFlag = PositionMenuFlag.None;
            if (!string.IsNullOrEmpty(Main) && Main == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.Main;
                positionMenuFlag |= PositionMenuFlag.Main;
            }
            if (!string.IsNullOrEmpty(Top) && Top == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.Top;
                positionMenuFlag |= PositionMenuFlag.Top;
            }
            if (!string.IsNullOrEmpty(SupperTop) && SupperTop == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.SupperTop;
                positionMenuFlag |= PositionMenuFlag.SupperTop;
            }
            if (!string.IsNullOrEmpty(Bottom) && Bottom == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.Bottom;
                positionMenuFlag |= PositionMenuFlag.Bottom;
            }
            if (!string.IsNullOrEmpty(MenuSub) && MenuSub == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.MenuSub;
                positionMenuFlag |= PositionMenuFlag.MenuSub;
            }
            if (!string.IsNullOrEmpty(MenuSubMainHome) && MenuSubMainHome == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.MenuSubMainHome;
                positionMenuFlag |= PositionMenuFlag.MenuSubMainHome;
            }
            if (!string.IsNullOrEmpty(MenuSubMainHome2) && MenuSubMainHome2 == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.MenuSubMainHome2;
                positionMenuFlag |= PositionMenuFlag.MenuSubMainHome2;
            }
            if (!string.IsNullOrEmpty(Style1) && Style1 == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.Style1;
                positionMenuFlag |= PositionMenuFlag.Style1;
            }
            if (!string.IsNullOrEmpty(Style2) && Style2 == "on")
            {
                PositionMenuFlag_INT += (int)PositionMenuFlag.Style2;
                positionMenuFlag |= PositionMenuFlag.Style2;
            }


            int AttrMenuFlag_INT = 0;
            AttrMenuFlag attrMenuFlag = AttrMenuFlag.None;
            if (!string.IsNullOrEmpty(MenuHome) && MenuHome == "on")
            {
                AttrMenuFlag_INT += (int)AttrMenuFlag.MenuHome;
                attrMenuFlag |= AttrMenuFlag.MenuHome;
            }
            if (!string.IsNullOrEmpty(MenuPriority) && MenuPriority == "on")
            {
                AttrMenuFlag_INT += (int)AttrMenuFlag.MenuPriority;
                attrMenuFlag |= AttrMenuFlag.MenuPriority;
            }
            if (!string.IsNullOrEmpty(MenuHotIcon) && MenuHotIcon == "on")
            {
                AttrMenuFlag_INT += (int)AttrMenuFlag.MenuHotIcon;
                attrMenuFlag |= AttrMenuFlag.MenuHotIcon;
            }
            if (!string.IsNullOrEmpty(OpenNewWindows) && OpenNewWindows == "on")
            {
                AttrMenuFlag_INT += (int)AttrMenuFlag.OpenNewWindows;
                attrMenuFlag |= AttrMenuFlag.OpenNewWindows;
            }

            if (!string.IsNullOrEmpty(PriorityAds) && PriorityAds == "on")
            {
                AttrMenuFlag_INT += (int)AttrMenuFlag.PriorityAds;
                attrMenuFlag |= AttrMenuFlag.PriorityAds;
            }

            int LinkTypeMenuFlag_INT = 0;
            LinkTypeMenuFlag linkTypeMenuFlag = LinkTypeMenuFlag.None;
            if (!string.IsNullOrEmpty(linktype))
            {
                if (linktype == LinkTypeMenuFlag.Product.ToString())
                {
                    LinkTypeMenuFlag_INT += (int)LinkTypeMenuFlag.Product;
                    linkTypeMenuFlag |= LinkTypeMenuFlag.Product;
                }
                if (linktype == LinkTypeMenuFlag.Article.ToString())
                {
                    LinkTypeMenuFlag_INT += (int)LinkTypeMenuFlag.Article;
                    linkTypeMenuFlag |= LinkTypeMenuFlag.Article;
                }
                if (linktype == LinkTypeMenuFlag.Content.ToString())
                {
                    LinkTypeMenuFlag_INT += (int)LinkTypeMenuFlag.Content;
                    linkTypeMenuFlag |= LinkTypeMenuFlag.Content;
                }
                if (linktype == LinkTypeMenuFlag.Link.ToString())
                {
                    LinkTypeMenuFlag_INT += (int)LinkTypeMenuFlag.Link;
                    linkTypeMenuFlag |= LinkTypeMenuFlag.Link;
                }
            }


            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;

            #endregion

            CacheUtility.PurgeCacheItems(table);

            GetAttrByID(Request.Form["attributesidlist"], "tblAttributes", true, ref AttributesUrlList, ref AttributesNameList);
            GetAttrByID(Request.Form["ParentID"], table, true, ref CatUrlList, ref CatNameList);
            GetAttrByID(Request.Form["attributeconfigidlist"], "tblAttributeConfigs", true, ref Attr_Config_UrlList, ref Attr_Config_NameList);

            string tag_name = Request.Form["tag_name"];
            string tag_id = Request.Form["tag_id"];
            string tagName = Utils.CommaSQLAdd(tag_name);
            tagName = tagName.Replace(System.Environment.NewLine, ",");

            Utils.GetTagByName(tagName, "tblCategories", "tag", true, ref TagUrlList, ref TagIDList);
            Utils.GetTagByName(Request.Form["hashtag_name"], "tblCategories", "hashtag", true, ref HashTagUrlList, ref HashTagIDList);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["Link"] = Utils.KillChars(Request.Form["link"]);
            hashtable["ParentID"] = Utils.KillChars(Request.Form["parentid"]);
            hashtable["LevelNumber"] = Utils.KillChars(Request.Form["levelnumber"]);
            hashtable["ParentIDList"] = Utils.KillChars(Request.Form["parentidlist"]);
            hashtable["Image_1"] = Utils.KillChars(Request.Form["image_1"]);
            hashtable["Image_2"] = Utils.KillChars(Request.Form["image_2"]);
            hashtable["Image_3"] = Utils.KillChars(Request.Form["image_3"]);
            hashtable["Image_4"] = Utils.KillChars(Request.Form["image_4"]);
            hashtable["Icon"] = Utils.KillChars(Request.Form["icon"]);
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Sort1"] = Utils.KillChars(Request.Form["sort1"]);
            hashtable["Sort2"] = Utils.KillChars(Request.Form["sort2"]);
            hashtable["Moduls"] = Utils.KillChars(Request.Form["moduls"]);
            hashtable["FilterJson"] = Utils.KillChars(Request.Form["filterjson"]);
            hashtable["Description"] = Utils.KillChars(Request.Form["description"]);
            hashtable["LongDescription"] = Utils.KillChars(Request.Form["longdescription"]);

            hashtable["LinkTypeMenuFlag"] = LinkTypeMenuFlag_INT;
            hashtable["PositionMenuFlag"] = PositionMenuFlag_INT;
            hashtable["AttrMenuFlag"] = AttrMenuFlag_INT;


            hashtable["TagIDList"] = TagIDList;
            hashtable["TagNameList"] = tagName;
            hashtable["TagUrlList"] = TagUrlList;

            hashtable["HashTagIDList"] = HashTagIDList;
            hashtable["HashTagNameList"] = Utils.CommaSQLAdd(Request.Form["hashtag_name"]);
            hashtable["HashTagUrlList"] = HashTagUrlList;



            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Moduls"] = "category";
            hashtable["Position"] = Utils.KillChars(Request.Form["position"]);
            hashtable["AttributesIDList"] = Utils.KillChars(Request.Form["attributesidlist"]);
            hashtable["AttributesUrlList"] = Utils.KillChars(Request.Form["attributesurllist"]);
            hashtable["AttributeConfigIDList"] = Utils.CommaSQLAdd(Request.Form["attributeconfigidlist"]);
            hashtable["AttributeConfigUrlList"] = Attr_Config_UrlList;
            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);
            string metakeyword = Utils.CommaSQLRemove(Request.Form["metakeyword"]);
            metakeyword = metakeyword.Replace(System.Environment.NewLine, ",");

            hashtable["MetaKeyword"] = metakeyword;
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);
            hashtable["Temp1"] = Utils.KillChars(Request.Form["temp1"]);
            hashtable["Temp2"] = Utils.KillChars(Request.Form["temp2"]);
            hashtable["Temp3"] = Utils.KillChars(Request.Form["temp3"]);
            hashtable["Temp4"] = Utils.KillChars(Request.Form["temp4"]);
            hashtable["Temp5"] = Utils.KillChars(Request.Form["temp5"]);
            hashtable["Temp6"] = Utils.KillChars(Request.Form["temp6"]);
            hashtable["Temp7"] = Utils.KillChars(Request.Form["temp7"]);
            hashtable["TempModuls"] = Utils.KillChars(Request.Form["tempmoduls"]);
            hashtable["SchemaBestRating"] = Utils.KillChars(Request.Form["schemabestrating"]);
            hashtable["SchemaRatingValue"] = Utils.KillChars(Request.Form["schemaratingvalue"]);
            hashtable["SchemaRatingCount"] = Utils.KillChars(Request.Form["schemaratingcount"]);


            CacheUtility.PurgeCacheItems(table);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblCategories] SET [Name]=@Name, [FriendlyUrl]=@FriendlyUrl, [Description]=@Description, [LongDescription]=@LongDescription, [Link]=@Link, [ParentID]=@ParentID, [ParentIDList]=@ParentIDList, [LevelNumber]=@LevelNumber, [Image_1]=@Image_1, [Image_2]=@Image_2, [Image_3]=@Image_3,[Image_4]=@Image_4, [Icon]=@Icon, [Sort]=@Sort, [Sort1]=@Sort1, [Sort2]=@Sort2, [Moduls]=@Moduls, [LinkTypeMenuFlag]=@LinkTypeMenuFlag, [PositionMenuFlag]=@PositionMenuFlag, [AttrMenuFlag]=@AttrMenuFlag, [Hide]=@Hide, [FilterJson]=@FilterJson, [AttributesIDList]=@AttributesIDList, [AttributesUrlList]=@AttributesUrlList, [AttributeConfigIDList]=@AttributeConfigIDList, [AttributeConfigUrlList]=@AttributeConfigUrlList, [TagIDList]=@TagIDList, [TagNameList]=@TagNameList, [TagUrlList]=@TagUrlList, [HashTagIDList]=@HashTagIDList, [HashTagNameList]=@HashTagNameList, [HashTagUrlList]=@HashTagUrlList, [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy, [Temp1]=@Temp1, [Temp2]=@Temp2, [Temp3]=@Temp3, [Temp4]=@Temp4, [Temp5]=@Temp5, [Temp6]=@Temp6, [Temp7]=@Temp7, [TempModuls]=@TempModuls,[SchemaBestRating]=@SchemaBestRating, [SchemaRatingValue]=@SchemaRatingValue, [SchemaRatingCount]=@SchemaRatingCount WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblCategories] ([Name],[FriendlyUrl],[Description],[LongDescription],[Link],[ParentID],[ParentIDList],[LevelNumber],[Image_1],[Image_2],[Image_3],[Image_4],[Icon],[Sort],[Sort1],[Sort2],[Moduls],[LinkTypeMenuFlag],[PositionMenuFlag],[AttrMenuFlag],[Hide],[FilterJson],[AttributesIDList],[AttributesUrlList],[AttributeConfigIDList],[AttributeConfigUrlList],[TagIDList],[TagNameList],[TagUrlList],[HashTagIDList],[HashTagNameList],[HashTagUrlList],[MetaTitle],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy],[Temp1],[Temp2],[Temp3],[Temp4],[Temp5],[Temp6],[Temp7],[TempModuls],[SchemaBestRating],[SchemaRatingValue],[SchemaRatingCount]) OUTPUT INSERTED.ID VALUES (@Name,@FriendlyUrl,@Description,@LongDescription,@Link,@ParentID,@ParentIDList,@LevelNumber,@Image_1,@Image_2,@Image_3,@Image_4,@Icon,@Sort,@Sort1,@Sort2,@Moduls,@LinkTypeMenuFlag,@PositionMenuFlag,@AttrMenuFlag,@Hide,@FilterJson,@AttributesIDList,@AttributesUrlList,@AttributeConfigIDList,@AttributeConfigUrlList,@TagIDList,@TagNameList,@TagUrlList,@HashTagIDList,@HashTagNameList,@HashTagUrlList,@MetaTitle,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy,@Temp1,@Temp2,@Temp3,@Temp4,@Temp5,@Temp6,@Temp7,@TempModuls,@SchemaBestRating,@SchemaRatingValue,@SchemaRatingCount)";
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
                db.AddParameter("@Image_4", System.Data.SqlDbType.NVarChar, hashtable["Image_4"].ToString());
                db.AddParameter("@Icon", System.Data.SqlDbType.NVarChar, hashtable["Icon"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Sort1", System.Data.SqlDbType.Int, hashtable["Sort1"].ToString());
                db.AddParameter("@Sort2", System.Data.SqlDbType.Int, hashtable["Sort2"].ToString());
                db.AddParameter("@Moduls", System.Data.SqlDbType.NVarChar, hashtable["Moduls"].ToString());
                db.AddParameter("@LinkTypeMenuFlag", System.Data.SqlDbType.Int, hashtable["LinkTypeMenuFlag"].ToString());
                db.AddParameter("@PositionMenuFlag", System.Data.SqlDbType.Int, hashtable["PositionMenuFlag"].ToString());
                db.AddParameter("@AttrMenuFlag", System.Data.SqlDbType.Int, hashtable["AttrMenuFlag"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"].ToString());
                db.AddParameter("@FilterJson", System.Data.SqlDbType.NVarChar, hashtable["FilterJson"].ToString());
                db.AddParameter("@AttributesIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributesIDList"].ToString());
                db.AddParameter("@AttributesUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributesUrlList"].ToString());
                db.AddParameter("@AttributeConfigIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigIDList"].ToString());
                db.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigUrlList"].ToString());
                db.AddParameter("@TagIDList", System.Data.SqlDbType.NVarChar, hashtable["TagIDList"].ToString());
                db.AddParameter("@TagNameList", System.Data.SqlDbType.NVarChar, hashtable["TagNameList"].ToString());
                db.AddParameter("@TagUrlList", System.Data.SqlDbType.NVarChar, hashtable["TagUrlList"].ToString());
                db.AddParameter("@HashTagIDList", System.Data.SqlDbType.NVarChar, hashtable["HashTagIDList"].ToString());
                db.AddParameter("@HashTagNameList", System.Data.SqlDbType.NVarChar, hashtable["HashTagNameList"].ToString());
                db.AddParameter("@HashTagUrlList", System.Data.SqlDbType.NVarChar, hashtable["HashTagUrlList"].ToString());
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
                db.AddParameter("@SchemaBestRating", System.Data.SqlDbType.Int, hashtable["SchemaBestRating"].ToString());
                db.AddParameter("@SchemaRatingValue", System.Data.SqlDbType.Int, hashtable["SchemaRatingValue"].ToString());
                db.AddParameter("@SchemaRatingCount", System.Data.SqlDbType.Int, hashtable["SchemaRatingCount"].ToString());

                db.AddParameter("@TempModuls", System.Data.SqlDbType.NVarChar, hashtable["TempModuls"].ToString());


                if (IsUpdate)
                {
                    db.AddParameter("@ID", System.Data.SqlDbType.Int, ID);
                    db.ExecuteSql(sqlQuery);

                    if (click_action == "saveandcopy")
                        CookieUtility.SetValueToCookie("notice", "update_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "update_success");

                    //Cập nhật lại bài viết nếu đổi danh mục
                    int old_parent_id = ConvertUtility.ToInt32(Request.Form["old_parent_id"]);
                    int new_parent_id = ConvertUtility.ToInt32(Request.Form["parentid"]);
                    UpdateArticleAndProductByNewCategory(old_parent_id, new_parent_id);
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

            UpdateBreadCrumb();
            string ModulsUrl = "category_product";
            if (!string.IsNullOrEmpty(linktype))
            {
                if (linktype == LinkTypeMenuFlag.Product.ToString())
                {
                    ModulsUrl = "category_product";
                }
                if (linktype == LinkTypeMenuFlag.Article.ToString())
                {
                    ModulsUrl = "category_article";
                }
                if (linktype == LinkTypeMenuFlag.Content.ToString())
                {
                    ModulsUrl = "category_content";
                }
                if (linktype == LinkTypeMenuFlag.Link.ToString())
                {
                    ModulsUrl = "category_link";
                }
            }
            SqlHelper.Update_Url_Table(IsUpdate, ModulsUrl, ID, hashtable["Name"].ToString(), hashtable["FriendlyUrl"].ToString());
        }
        else if (click_action == "delete")
        {
            DeleteCategory();

            CookieUtility.SetValueToCookie("notice", "delete_success");
        }
        else
        {
            GetAttrByID(dr["AttributesIDList"].ToString(), "tblAttributes", false, ref AttributesUrlList, ref AttributesNameList);
            GetAttrByID(dr["ParentID"].ToString(), table, false, ref CatUrlList, ref CatNameList);
            GetAttrByID(dr["AttributeConfigIDList"].ToString(), "tblAttributeConfigs", false, ref Attr_Config_UrlList, ref Attr_Config_NameList);
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
            IDList = IDList.Trim(',');

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

    #region UpdateChangeCategory
    //Thay đổi lại danh mục trong Article, Product khi update lại danh mục
    protected void UpdateArticleAndProductByNewCategory(int old_parent_id, int new_parent_id)
    {
        if (old_parent_id != new_parent_id)
        {
            //Lấy tất cả các bài viết đang sử dụng danh mục cũ để cập nhật lại danh mục CHA
            using (var db = SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("SELECT * FROM {0} Where CategoryIDList LIKE N'%,{1},%' OR CategoryIDParentList LIKE N'%,{1},%'", C.ARTICLE_TABLE, ID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Rows)
                    {
                        string CategoryIDParentList = GetCategoryIDParentList(dr["CategoryIDList"].ToString());
                        string sqlQuery1 = string.Format(@"UPDATE tblArticle SET CategoryIDParentList=N'{0}' WHERE ID={1}", CategoryIDParentList, dr["ID"]);
                        using (var dbx = SqlService.GetSqlService())
                        {
                            db.ExecuteSql(sqlQuery1);
                        }
                    }
                }
            }
            try
            {
                //Lấy tất cả các sản phẩm đang sử dụng danh mục cũ để cập nhật lại danh mục CHA
                using (var db = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT * FROM {0} Where CategoryIDList LIKE N'%,{1},%' OR CategoryIDParentList LIKE N'%,{1},%'", C.PRODUCT_TABLE, ID);
                    var ds = db.ExecuteSqlDataTable(sqlQuery);
                    if (ds.Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Rows)
                        {
                            string CategoryIDParentList = GetCategoryIDParentList(dr["CategoryIDList"].ToString());
                            string sqlQuery1 = string.Format(@"UPDATE tblProducts SET CategoryIDParentList=N'{0}' WHERE ID={1}", CategoryIDParentList, dr["ID"]);
                            using (var dbx = SqlService.GetSqlService())
                            {
                                db.ExecuteSql(sqlQuery1);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
    protected string GetCategoryIDParentList(string CategoryIDList)
    {
        string returnValue = string.Empty;
        CategoryIDList = Utils.CommaSQLRemove(CategoryIDList);

        if (string.IsNullOrEmpty(CategoryIDList))
            return returnValue;

        string[] NameArr = CategoryIDList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            string CategoryIDParentList = ",";
            foreach (string CatID in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    int count = 0;
                    int ParentID = ConvertUtility.ToInt32(CatID);
                    while (ParentID > 0 && count < 5)
                    {
                        string sqlQuery = string.Format("SELECT Top 1 ParentID FROM tblCategories Where ID={0}", ParentID);
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

    #endregion


    #region UpdateBreadCrumb
    protected void UpdateBreadCrumb()
    {
        string AllRelatedCategory = GetAllRelatedCategory();
        DataTable dtRelatedCategories = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("ID IN ({0})", AllRelatedCategory));
        if (Utils.CheckExist_DataTable(dtRelatedCategories))
        {
            foreach (DataRow drRelatedCategories in dtRelatedCategories.Rows)
            {
                bool IsLeaf = true;
                int Level = 1;
                int RootID = 0;
                string ParentIDList = "";
                Stack<BreadCrumb> bcList = new Stack<BreadCrumb>();
                DataTable dtCurrent = dtRelatedCategories;
                DataRow drCurrent = drRelatedCategories;
                List<BreadCrumbChild> bcChildList = new List<BreadCrumbChild>();
                DataTable dtChild = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", "ParentID=" + drCurrent["ID"], "Sort");
                if (Utils.CheckExist_DataTable(dtChild))
                {
                    IsLeaf = false;
                    bcChildList = new List<BreadCrumbChild>();
                    foreach (DataRow drChild in dtChild.Rows)
                    {
                        bcChildList.Add(CreateBreadCrumbChild(drChild));
                    }
                }
                else
                {
                    IsLeaf = true;
                }
                bcList.Push(CreateBreadCrumb(drCurrent, bcChildList)); //Add chính nó
                do
                {
                    if (!Utils.IsNullOrEmpty(drCurrent["ParentID"]) && ConvertUtility.ToInt32(drCurrent["ParentID"]) > 0)
                    {
                        dtCurrent = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, LinkTypeMenuFlag, FriendlyUrl, Name, Link", string.Format("ID={0}", drCurrent["ParentID"]), "Sort");
                        if (Utils.CheckExist_DataTable(dtCurrent))
                        {
                            Level++;
                            drCurrent = dtCurrent.Rows[0];
                            RootID = ConvertUtility.ToInt32(drCurrent["ID"]);
                            dtChild = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", "ParentID=" + drCurrent["ID"], "Sort");
                            if (Utils.CheckExist_DataTable(dtChild))
                            {
                                bcChildList = new List<BreadCrumbChild>();
                                foreach (DataRow drChild in dtChild.Rows)
                                {
                                    bcChildList.Add(CreateBreadCrumbChild(drChild));
                                }
                            }

                            bcList.Push(CreateBreadCrumb(drCurrent, bcChildList));
                            if (Level == 3)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                while (!Utils.IsNullOrEmpty(drCurrent["ParentID"]) && ConvertUtility.ToInt32(drCurrent["ParentID"]) > 0 && Level <= 3);

                foreach (BreadCrumb bc in bcList)
                {
                    if (bc.ID != dr["ID"].ToString())
                        ParentIDList += bc.ID + ",";
                }

                ParentIDList = Utils.CommaSQLAdd(ParentIDList);

                using (var db = SqlService.GetSqlService())
                {
                    string query = string.Format("update tblCategories set BreadCrumbJson=N'{0}', IsLeaf='{1}', LevelNumber={2}, RootID={3}, ParentIDList='{4}' where ID={5}", bcList.ToJSON(), IsLeaf, Level, RootID, ParentIDList, drRelatedCategories["ID"].ToString());
                    //Response.Write(query + "<br />");
                    db.ExecuteSql(query);
                }
            }
        }
    }

    protected BreadCrumb CreateBreadCrumb(DataRow dr, List<BreadCrumbChild> child)
    {
        BreadCrumb bc = new BreadCrumb();
        bc.ID = ConvertUtility.ToString(dr["ID"]);
        bc.Name = ConvertUtility.ToString(dr["Name"]);
        bc.FriendlyUrl = dr["FriendlyUrl"].ToString();
        bc.LinkTypeMenuFlag = dr["LinkTypeMenuFlag"].ToString();
        bc.Link = dr["Link"].ToString();
        bc.Child = child;
        return bc;
    }
    protected BreadCrumbChild CreateBreadCrumbChild(DataRow dr)
    {
        BreadCrumbChild bc = new BreadCrumbChild();
        bc.ID = ConvertUtility.ToString(dr["ID"]);
        bc.Name = ConvertUtility.ToString(dr["Name"]);
        bc.FriendlyUrl = dr["FriendlyUrl"].ToString();
        bc.LinkTypeMenuFlag = dr["LinkTypeMenuFlag"].ToString();
        bc.Link = dr["Link"].ToString();
        return bc;
    }

    protected string GetAllRelatedCategory() // Lấy tất cả các danh mục có liên quan
    {
        List<int> IDCategoryRelated = new List<int>();
        int CategoryID = ID;
        string returnValue = string.Empty;

        //Add chính nó
        IDCategoryRelated.Add(CategoryID);
        // Lấy tất cả mục con
        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT ID FROM tblCategories Where ParentID={0}", CategoryID);
            DataTable ds = db.ExecuteSqlDataTable(sqlQuery);
            if (Utils.CheckExist_DataTable(ds))
            {
                foreach (DataRow dr in ds.Rows)
                {
                    IDCategoryRelated.Add(ConvertUtility.ToInt32(dr["ID"]));
                }
            }
        }
        // Lấy tất cả mục cha
        DataTable dtCurrent = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ParentID,ParentID", string.Format("ID={0}", CategoryID), "Sort");
        if (Utils.CheckExist_DataTable(dtCurrent))
        {
            DataTable dt1 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,ParentID", string.Format("ID={0}", dtCurrent.Rows[0]["ParentID"]), "Sort");
            if (Utils.CheckExist_DataTable(dt1))
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    IDCategoryRelated.Add(ConvertUtility.ToInt32(dr1["ID"]));
                    //Cấp 2
                    DataTable dt2 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,ParentID", string.Format("ID={0}", dr1["ParentID"]), "Sort");
                    if (Utils.CheckExist_DataTable(dt2))
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            IDCategoryRelated.Add(ConvertUtility.ToInt32(dr2["ID"]));
                            //Cấp 3
                            DataTable dt3 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,ParentID", string.Format("ID={0}", dr2["ParentID"]), "Sort");
                            if (Utils.CheckExist_DataTable(dt3))
                            {
                                foreach (DataRow dr3 in dt3.Rows)
                                {
                                    IDCategoryRelated.Add(ConvertUtility.ToInt32(dr3["ID"]));
                                    //Cấp 4
                                    DataTable dt4 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,ParentID", string.Format("ID={0}", dr3["ParentID"]), "Sort");
                                    if (Utils.CheckExist_DataTable(dt4))
                                    {
                                        foreach (DataRow dr4 in dt4.Rows)
                                        {
                                            IDCategoryRelated.Add(ConvertUtility.ToInt32(dr4["ID"]));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (int IDCat in IDCategoryRelated)
        {
            returnValue += IDCat + ",";
        }

        returnValue = Utils.CommaSQLRemove(returnValue);

        return returnValue;
    }


    #endregion



    #region DeleteCategory
    protected void DeleteCategory()
    {
        // Cập nhật tất cả mục con nối vào danh mục cha của category bị xóa
        int old_parent_id = ConvertUtility.ToInt32(Request.Form["old_parent_id"]);
        int old_id = ConvertUtility.ToInt32(Request.Form["id"]);
        //if (old_parent_id > 0)
        //{
        using (var db = SqlService.GetSqlService())
        {
            //Chuyển tất cả bài viết sang mục cha
            string sqlQuery = string.Format(@"Update tblArticle SET {0} = REPLACE({0}, ',{1},', ',{2},') Where {0} like N'%,{1},%'
                                              Update tblArticle SET {3} = REPLACE({3}, ',{1},', ',{2},') Where {3} like N'%,{1},%'", "CategoryIDList", old_id, old_parent_id, "CategoryIDParentList");
            sqlQuery += string.Format(@"Update tblCategories set ParentID={0} where ParentID={1}
                                        ", old_parent_id, old_id);
            sqlQuery += string.Format("Delete From tblCategories where ID={0}", old_id);
            db.ExecuteSql(sqlQuery);

            // xoá trong bảng Url

            string filter = string.Format("ContentID='{0}' AND (Moduls=N'{1}' OR Moduls=N'{2}' OR Moduls=N'{3}')", old_id, "category_product", "category_article", "category_content");
            string sqlQueryUrl = string.Format("DELETE FROM {0} WHERE {1}", "tblUrl", filter);
            db.ExecuteSql(sqlQueryUrl);
            //Response.Write(sqlQueryUrl);
        }
        //}
    }
    #endregion


}