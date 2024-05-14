using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
using System.Linq;

public partial class admin_Controls_video_VideoUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblVideo";
    public string
        AttributesUrlList = string.Empty,
        AttributesNameList = string.Empty,
        Attr_Config_UrlList = string.Empty,
        Attr_Config_NameList = string.Empty,
        CatUrlList = string.Empty,
        CatNameList = string.Empty,

        TagIDList = string.Empty,
        TagUrlList = string.Empty,
        TagNameList = string.Empty,

        HashTagIDList = string.Empty,
        HashTagUrlList = string.Empty,
        HashTagNameList = string.Empty;

    public ArticleFlag _flag;
    string FriendlyUrlCategory = string.Empty;
    public string thumbnail = C.NO_IMG_PATH;
    
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
                if (!string.IsNullOrEmpty(ds.Rows[0]["Thumbnail"].ToString()))
                    thumbnail = ds.Rows[0]["Thumbnail"].ToString();
            }
        }

        int FlagINT = 0;

        DataTable dtFlag = SqlHelper.SQLToDataTable(table, "Flags", string.Format("ID='{0}'", SqlFilterID), "ID", 1, 1);
        if (Utils.CheckExist_DataTable(dtFlag))
        {
            FlagINT = ConvertUtility.ToInt32(dtFlag.Rows[0]["Flags"]);
        }
        _flag = (ArticleFlag)FlagINT;

        Utils.GetAttrByID(dr["CategoryIDList"].ToString(), "tblCategories", false, ref CatUrlList, ref CatNameList);

    }

    protected void UpdateDatabase()
    {

        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd" || click_action == "view"))
        {
            Utils.GetAttrByID(Request.Form["categoryidlist"], "tblCategories", true, ref CatUrlList, ref CatNameList);

            Utils.GetTagByName(Request.Form["tag_name"], "tblCategories", "tag", true, ref TagUrlList, ref TagIDList);
            Utils.GetTagByName(Request.Form["hashtag_name"], "tblCategories", "hashtag", true, ref HashTagUrlList, ref HashTagIDList);

            /////////////////

            #region Flags
            string HomeArticle = Request.Form["HomeArticle"];

            int Flag_INT = 0;

            if (!string.IsNullOrEmpty(HomeArticle) && HomeArticle == "on")
                Flag_INT += (int)ArticleFlag.HomeArticle;


            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;

            #endregion

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["Link"] = Utils.KillChars(Request.Form["link"]);
            hashtable["Description"] = Utils.KillChars(Request.Form["description"]);
            hashtable["Thumbnail"] = Utils.KillChars(Request.Form["thumbnail"]);
            hashtable["Embed"] = Utils.KillChars(Request.Form["embed"]);
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Flags"] = Flag_INT;

            hashtable["CategoryIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["categoryidlist"]));
            hashtable["CategoryNameList"] = Utils.CommaSQLAdd(CatNameList);
            hashtable["CategoryUrlList"] = Utils.CommaSQLAdd(CatUrlList);
            hashtable["CategoryIDParentList"] = GetCategoryIDParentList();


            hashtable["TagIDList"] = TagIDList;
            hashtable["TagNameList"] = Utils.CommaSQLAdd(Request.Form["tag_name"]);
            hashtable["TagUrlList"] = TagUrlList;

            hashtable["HashTagIDList"] = HashTagIDList;
            hashtable["HashTagNameList"] = Utils.CommaSQLAdd(Request.Form["hashtag_name"]);
            hashtable["HashTagUrlList"] = HashTagUrlList;

            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);
            hashtable["MetaKeyword"] = Utils.KillChars(Request.Form["metakeyword"]);
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);


            CacheUtility.PurgeCacheItems(table);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblVideo] SET [Name]=@Name, [FriendlyUrl]=@FriendlyUrl, [Link]=@Link, [Description]=@Description, [Thumbnail]=@Thumbnail, [Embed]=@Embed, [CategoryIDList]=@CategoryIDList, [CategoryNameList]=@CategoryNameList, [CategoryUrlList]=@CategoryUrlList, [CategoryIDParentList]=@CategoryIDParentList, [Sort]=@Sort, [Flags]=@Flags, [Hide]=@Hide, [TagIDList]=@TagIDList, [TagNameList]=@TagNameList, [TagUrlList]=@TagUrlList, [HashTagIDList]=@HashTagIDList, [HashTagNameList]=@HashTagNameList, [HashTagUrlList]=@HashTagUrlList, [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblVideo] ([Name],[FriendlyUrl],[Link],[Description],[Thumbnail],[Embed],[CategoryIDList],[CategoryNameList],[CategoryUrlList],[CategoryIDParentList],[Sort],[Flags],[Hide],[TagIDList],[TagNameList],[TagUrlList],[HashTagIDList],[HashTagNameList],[HashTagUrlList],[MetaTitle],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@FriendlyUrl,@Link,@Description,@Thumbnail,@Embed,@CategoryIDList,@CategoryNameList,@CategoryUrlList,@CategoryIDParentList,@Sort,@Flags,@Hide,@TagIDList,@TagNameList,@TagUrlList,@HashTagIDList,@HashTagNameList,@HashTagUrlList,@MetaTitle,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrl"].ToString());
                db.AddParameter("@Link", System.Data.SqlDbType.NVarChar, hashtable["Link"].ToString());
                db.AddParameter("@Description", System.Data.SqlDbType.NVarChar, hashtable["Description"].ToString());
                db.AddParameter("@Thumbnail", System.Data.SqlDbType.NVarChar, hashtable["Thumbnail"].ToString());
                db.AddParameter("@Embed", System.Data.SqlDbType.NVarChar, hashtable["Embed"].ToString());
                db.AddParameter("@CategoryIDList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDList"].ToString());
                db.AddParameter("@CategoryNameList", System.Data.SqlDbType.NVarChar, hashtable["CategoryNameList"].ToString());
                db.AddParameter("@CategoryUrlList", System.Data.SqlDbType.NVarChar, hashtable["CategoryUrlList"].ToString());
                db.AddParameter("@CategoryIDParentList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDParentList"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"].ToString());
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

                    if (click_action == "saveandcopy" || click_action == "save")
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
        else if (click_action == "view")
        {
            string Url = TextChanger.GetLinkRewrite_Products(hashtable["FriendlyUrlCategory"].ToString(), hashtable["FriendlyUrl"].ToString());
            Response.Redirect(Url);
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


    #region thêm Tags
    protected void UpdateTags(string NameList, string Type, ref string TagIDList)
    {
        string _TagIDList = "";
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
                    string sqlQuery = string.Format("SELECT * FROM tblCategories Where Name=N'{0}' AND Moduls=N'%,{1},%'", name.Trim(), Type);
                    var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (ds == null || ds.Rows.Count == 0)
                    {
                        int TagID = 0;
                        AddTags(name, Type, ref TagID);
                        _TagIDList += TagID + ",";
                    }
                }
            }

            TagIDList = _TagIDList.Trim(',');
        }
    }

    protected void AddTags(string Name, string Type, ref int TagID)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            CacheUtility.PurgeCacheItems("tblCategories");

            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = "INSERT INTO tblCategories (ParentID, Name, Moduls) OUTPUT INSERTED.ID Values (@ParentID, @Name, @Type)";
                dbx.AddParameter("@ParentID", System.Data.SqlDbType.Int, 0);
                dbx.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
                dbx.AddParameter("@Type", System.Data.SqlDbType.NVarChar, Type);
                TagID = dbx.ExecuteSqlScalar<int>(sqlQuery, 0);
            }
        }
    }
    #endregion

    

}