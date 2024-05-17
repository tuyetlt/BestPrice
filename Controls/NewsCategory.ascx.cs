using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_NewsCategory : System.Web.UI.UserControl
{
    public DataRow drCat, drNews;
    public DataTable dtCat, dtNews, dtRef;
    public int ID, RootID, _totalArticle;
    public string caturl;

    protected void Page_Load(object sender, EventArgs e)
    {
        ProccessParameter();
        if (!IsPostBack)
        {
            BindData();
            SetSeo();
        }
    }

    protected void ProccessParameter()
    {
        caturl = ConvertUtility.ToString(Page.RouteData.Values["caturl"]);
    }

    protected void BindData()
    {
        if (Utils.CheckExist_DataTable(dtRef))
            dtCat = dtRef;
        else
            dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, Name,FriendlyUrl,Image_1,MetaTitle,MetaKeyword,MetaDescription,SchemaRatingCount,SchemaRatingValue,SeoFlags,Canonical", string.Format("FriendlyUrl=N'{0}' AND {1}", caturl, Utils.CreateFilterHide));

        if (Utils.CheckExist_DataTable(dtCat))
        {
            drCat = dtCat.Rows[0];
            PageInfo.CategoryID = ConvertUtility.ToInt32(drCat["ID"]);
            string filterNews = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryaIDParentList Like N'%,{0},%') AND {1}", drCat["ID"], Utils.CreateFilterDate);
            dtNews = SqlHelper.SQLToDataTable("tblArticle", "Name,Gallery,Description,FriendlyUrl", filterNews, ConfigWeb.SortArticle, 1, C.ROWS_PRODUCTCATEGORY,out _totalArticle);
            CookieUtility.SetValueToCookie("pageIndex_Category", "2");
        }
    }



    protected void SetSeo()
    {
        if (Utils.CheckExist_DataTable(dtCat))
        {

            string MetaTitle = ConvertUtility.ToString(drCat["MetaTitle"]);
            string MetaKeyword = ConvertUtility.ToString(drCat["MetaKeyword"]);
            string MetaDescription = ConvertUtility.ToString(drCat["MetaDescription"]);

            if (MetaTitle.Length < 3)
                MetaTitle = ConvertUtility.ToString(drCat["Name"]);
            if (MetaKeyword.Length < 3)
                MetaKeyword = MetaTitle + ", " + ConfigWeb.MetaKeyword;
            if (MetaDescription.Length < 3)
                MetaDescription = MetaTitle + ", " + ConfigWeb.MetaDescription;

            string url = TextChanger.GetLinkRewrite_Category(ConvertUtility.ToString(drCat["FriendlyUrl"]));
 
            PageUtility.AddTitle(this.Page, MetaTitle);
            PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
            PageUtility.AddMetaTag(this.Page, "description", MetaDescription);
            string image = ConvertUtility.ToString(drCat["Image_1"]);
            if (string.IsNullOrEmpty(image))
                image = ConfigWeb.Image;
            PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, C.ROOT_URL + image, ConfigWeb.SiteName, MetaDescription);
            PageUtility.AddCanonicalLink(this.Page, url);

            SEO_Schema.Type = "WebSite";
            SEO_Schema.Title = SEO.meta_title;
            SEO_Schema.Description = SEO.meta_description;
            SEO_Schema.Image = image;
            SEO_Schema.Url = SEO.canonical;
            SEO_Schema.AuthorName = C.SITE_NAME;
            SEO_Schema.Publisher_Type = "Organization";
            SEO_Schema.Publisher_Name = C.ROOT_URL.Replace("https://", "");
            SEO_Schema.Publisher_Logo = ConfigWeb.LogoAdmin;
            SEO_Schema.RatingCount = ConvertUtility.ToInt32(drCat["SchemaRatingCount"]);
            SEO_Schema.RatingValue = ConvertUtility.ToInt32(drCat["SchemaRatingValue"]);
            if (SEO_Schema.RatingValue > 93)
                SEO_Schema.ReviewRatingValue = 5;
            else
                SEO_Schema.ReviewRatingValue = 4;

            PageInfo.CurrentControl = ControlCurrent.NewsCategory.ToString();
        }
        else
        {
            string MetaTitle = "";
            string MetaKeyword = "";
            string MetaDescription = "";

            if (MetaTitle.Length < 3)
                MetaTitle = "Tin tức";
            if (MetaKeyword.Length < 3)
                MetaKeyword = MetaTitle + ", " + ConfigWeb.MetaKeyword;
            if (MetaDescription.Length < 3)
                MetaDescription = MetaTitle + ", " + ConfigWeb.MetaDescription;

            string url = C.ROOT_URL + "/tin-tuc/";
            PageUtility.AddTitle(this.Page, MetaTitle);
            PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
            PageUtility.AddMetaTag(this.Page, "description", MetaDescription);
            string image = ConfigWeb.Image;
            PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, image, ConfigWeb.SiteName, MetaDescription);
            PageUtility.AddCanonicalLink(this.Page, url);
            PageUtility.SetIndex(this.Page);

        }

        PageUtility.AddDefaultMetaTag(this.Page);
    }
}