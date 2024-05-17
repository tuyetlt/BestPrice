using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;

public partial class admin_Controls_ArticleUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblArticle";
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
        click_action = Request.Form["done"];


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
                string sqlQuery = string.Format("SELECT * FROM tblArticle Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];

                    Utils.GetAttrByID(dr["AttributesIDList"].ToString(), "tblAttributes", false, ref AttributesUrlList, ref AttributesNameList);
                    Utils.GetAttrByID(dr["CategoryIDList"].ToString(), "tblCategories", false, ref CatUrlList, ref CatNameList);
                    Utils.GetAttrByID(dr["AttributeConfigIDList"].ToString(), "tblAttributeConfigs", false, ref Attr_Config_UrlList, ref Attr_Config_NameList);
                }
            }

            int FlagINT = 0;

            DataTable dtFlag = SqlHelper.SQLToDataTable(table, "Flags", string.Format("ID='{0}'", SqlFilterID), "ID", 1, 1);
            if (Utils.CheckExist_DataTable(dtFlag))
            {
                FlagINT = ConvertUtility.ToInt32(dtFlag.Rows[0]["Flags"]);
            }
            _flag = (ArticleFlag)FlagINT;

        }
    }

    protected void UpdateDatabase()
    {

        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {
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


            Utils.GetAttrByID(Request.Form["attributesidlist"], "tblAttributes", true, ref AttributesUrlList, ref AttributesNameList);
            Utils.GetAttrByID(Request.Form["categoryidlist"], "tblCategories", true, ref CatUrlList, ref CatNameList);
            Utils.GetAttrByID(Request.Form["attributeconfigidlist"], "tblAttributeConfigs", true, ref Attr_Config_UrlList, ref Attr_Config_NameList);

            Utils.GetTagByName(Request.Form["tag_name"], "tblCategories", "tag", true, ref TagUrlList, ref TagIDList);
            Utils.GetTagByName(Request.Form["hashtag_name"], "tblCategories", "hashtag", true, ref HashTagUrlList, ref HashTagIDList);


            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["FriendlyUrlCategory"] = Utils.KillChars(Request.Form["friendlyurlcategory"]);
            hashtable["NameUsign"] = Utils.KillChars(Request.Form["nameusign"]);
            hashtable["CategoryIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["categoryidlist"]));
            hashtable["CategoryNameList"] = Utils.CommaSQLAdd(CatNameList);
            hashtable["CategoryUrlList"] = Utils.CommaSQLAdd(CatUrlList);
            hashtable["CategoryaIDParentList"] = GetCategoryIDParentList();

            hashtable["AttributesIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributesidlist"]));
            hashtable["AttributesUrlList"] = Utils.KillChars(AttributesUrlList);
            hashtable["AttributeConfigIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributeconfigidlist"]));
            hashtable["AttributeConfigUrlList"] = Utils.KillChars(Attr_Config_UrlList);

            hashtable["TagIDList"] = TagIDList;
            hashtable["TagNameList"] = Utils.CommaSQLAdd(Request.Form["tag_name"]);
            hashtable["TagUrlList"] = TagUrlList;

            hashtable["HashTagIDList"] = HashTagIDList;
            hashtable["HashTagNameList"] = Utils.CommaSQLAdd(Request.Form["hashtag_name"]);
            hashtable["HashTagUrlList"] = HashTagUrlList;

            hashtable["Description"] = Utils.KillChars(Request.Form["description"]);
            hashtable["LongDescription"] = Utils.KillChars(Request.Form["longdescription"]);
            hashtable["Tags"] = Utils.KillChars(Request.Form["tags"]);
            hashtable["Image"] = Utils.KillChars(Request.Form["image"]);
            hashtable["Gallery"] = Utils.KillChars(Request.Form["gallery"]);
            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);
            hashtable["MetaKeyword"] = Utils.KillChars(Request.Form["metakeyword"]);
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);

            hashtable["StartDate"] = Utils.DateTimeString_To_DateTimeSql(Request.Form["startdate"]);
            hashtable["EndDate"] = DateUtil.GetMaxDateTime_IfNull(Request.Form["enddate"]);
            hashtable["Flags"] = Flag_INT;
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);

            hashtable["Viewed"] = Utils.KillChars(Request.Form["viewed"]);
            hashtable["NewsRelatedIDList"] = Utils.KillChars(Request.Form["newsrelatedidlist"]);

            CacheUtility.PurgeCacheItems(table);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblArticle] SET [Name]=@Name, [NameUsign]=@NameUsign, [FriendlyUrl]=@FriendlyUrl, [FriendlyUrlCategory]=@FriendlyUrlCategory, [CategoryIDList]=@CategoryIDList, [CategoryNameList]=@CategoryNameList, [CategoryUrlList]=@CategoryUrlList, [CategoryaIDParentList]=@CategoryaIDParentList, [AttributesIDList]=@AttributesIDList, [AttributesUrlList]=@AttributesUrlList, [AttributeConfigIDList]=@AttributeConfigIDList, [AttributeConfigUrlList]=@AttributeConfigUrlList, [Description]=@Description, [LongDescription]=@LongDescription, [TagIDList]=@TagIDList, [TagNameList]=@TagNameList, [TagUrlList]=@TagUrlList, [HashTagIDList]=@HashTagIDList, [HashTagNameList]=@HashTagNameList, [HashTagUrlList]=@HashTagUrlList, [NewsRelatedIDList]=@NewsRelatedIDList, [StartDate]=@StartDate, [EndDate]=@EndDate, [Flags]=@Flags, [Hide]=@Hide, [Sort]=@Sort, [Viewed]=@Viewed, [Tags]=@Tags, [Image]=@Image, [Gallery]=@Gallery, [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblArticle] ([Name],[NameUsign],[FriendlyUrl],[FriendlyUrlCategory],[CategoryIDList],[CategoryNameList],[CategoryUrlList],[CategoryaIDParentList],[AttributesIDList],[AttributesUrlList],[AttributeConfigIDList],[AttributeConfigUrlList],[Description],[LongDescription],[TagIDList],[TagNameList],[TagUrlList],[HashTagIDList],[HashTagNameList],[HashTagUrlList],[NewsRelatedIDList],[StartDate],[EndDate],[Flags],[Hide],[Sort],[Viewed],[Tags],[Image],[Gallery],[MetaTitle],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@NameUsign,@FriendlyUrl,@FriendlyUrlCategory,@CategoryIDList,@CategoryNameList,@CategoryUrlList,@CategoryaIDParentList,@AttributesIDList,@AttributesUrlList,@AttributeConfigIDList,@AttributeConfigUrlList,@Description,@LongDescription,@TagIDList,@TagNameList,@TagUrlList,@HashTagIDList,@HashTagNameList,@HashTagUrlList,@NewsRelatedIDList,@StartDate,@EndDate,@Flags,@Hide,@Sort,@Viewed,@Tags,@Image,@Gallery,@MetaTitle,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@NameUsign", System.Data.SqlDbType.NVarChar, hashtable["NameUsign"].ToString());
                db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrl"].ToString());
                db.AddParameter("@FriendlyUrlCategory", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrlCategory"].ToString());
                db.AddParameter("@CategoryIDList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDList"].ToString());
                db.AddParameter("@CategoryNameList", System.Data.SqlDbType.NVarChar, hashtable["CategoryNameList"].ToString());
                db.AddParameter("@CategoryUrlList", System.Data.SqlDbType.NVarChar, hashtable["CategoryUrlList"].ToString());
                db.AddParameter("@CategoryaIDParentList", System.Data.SqlDbType.NVarChar, hashtable["CategoryaIDParentList"].ToString());
                db.AddParameter("@AttributesIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributesIDList"].ToString());
                db.AddParameter("@AttributesUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributesUrlList"].ToString());
                db.AddParameter("@AttributeConfigIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigIDList"].ToString());
                db.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigUrlList"].ToString());
                db.AddParameter("@Description", System.Data.SqlDbType.NVarChar, hashtable["Description"].ToString());
                db.AddParameter("@LongDescription", System.Data.SqlDbType.NVarChar, hashtable["LongDescription"].ToString());
                db.AddParameter("@TagIDList", System.Data.SqlDbType.NVarChar, hashtable["TagIDList"].ToString());
                db.AddParameter("@TagNameList", System.Data.SqlDbType.NVarChar, hashtable["TagNameList"].ToString());
                db.AddParameter("@TagUrlList", System.Data.SqlDbType.NVarChar, hashtable["TagUrlList"].ToString());
                db.AddParameter("@HashTagIDList", System.Data.SqlDbType.NVarChar, hashtable["HashTagIDList"].ToString());
                db.AddParameter("@HashTagNameList", System.Data.SqlDbType.NVarChar, hashtable["HashTagNameList"].ToString());
                db.AddParameter("@HashTagUrlList", System.Data.SqlDbType.NVarChar, hashtable["HashTagUrlList"].ToString());
                db.AddParameter("@NewsRelatedIDList", System.Data.SqlDbType.NVarChar, hashtable["NewsRelatedIDList"].ToString());
                db.AddParameter("@StartDate", System.Data.SqlDbType.DateTime, hashtable["StartDate"].ToString());
                db.AddParameter("@EndDate", System.Data.SqlDbType.DateTime, hashtable["EndDate"].ToString());
                db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Viewed", System.Data.SqlDbType.Int, hashtable["Viewed"].ToString());
                db.AddParameter("@Tags", System.Data.SqlDbType.NVarChar, hashtable["Tags"].ToString());
                db.AddParameter("@Image", System.Data.SqlDbType.NVarChar, hashtable["Image"].ToString());
                db.AddParameter("@Gallery", System.Data.SqlDbType.NVarChar, hashtable["Gallery"].ToString());
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

                    if (click_action == "saveandcopy" || click_action == "save")
                        CookieUtility.SetValueToCookie("notice", "insert_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "insert_success");
                }

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(IsUpdate), Request.RawUrl);

            }

            SqlHelper.Update_Url_Table(IsUpdate, "article_detail", ID, hashtable["Name"].ToString(), hashtable["FriendlyUrl"].ToString());
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