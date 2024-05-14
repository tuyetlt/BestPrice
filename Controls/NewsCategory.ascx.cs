using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_NewsCategory : System.Web.UI.UserControl
{
    public DataRow drCat, drNews;
    public DataTable dtCat, dtNews;
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
        dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("FriendlyUrl=N'{0}'", caturl));
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
            SEO.meta_title = ConvertUtility.ToString(drCat["MetaTitle"]);
            SEO.meta_keyword = ConvertUtility.ToString(drCat["MetaKeyword"]);
            SEO.meta_description = ConvertUtility.ToString(drCat["MetaDescription"]);

            SEO.url_current = TextChanger.GetLinkRewrite_Category(ConvertUtility.ToString(drCat["FriendlyUrl"]));
            SEO.canonical = TextChanger.GetLinkRewrite_Category(ConvertUtility.ToString(drCat["FriendlyUrl"]));
            if (SEO.meta_title.Length < 3)
                SEO.meta_title = ConvertUtility.ToString(drCat["Name"]);
            if (SEO.meta_keyword.Length < 3)
                SEO.meta_keyword = ConvertUtility.ToString(drCat["Name"]) + ", " + ConfigWeb.MetaKeyword;

            if (SEO.meta_description.Length < 3)
                SEO.meta_description = ConvertUtility.ToString(drCat["Name"]) + ", " + ConfigWeb.MetaDescription;

            SEO.content_share_facebook = "<meta property='og:title' content='" + SEO.meta_title + "'/>";
            SEO.content_share_facebook += "<meta property='og:type' content='website'/>";
            SEO.content_share_facebook += "<meta property='og:url' content='" + SEO.url_current + "'/>";

            string image = ConvertUtility.ToString(drCat["Image_1"]);
            if (string.IsNullOrEmpty(image))
                image = ConfigWeb.Image;
            SEO.content_share_facebook += "<meta property='og:image' content='" + C.ROOT_URL + image + "'/>";
            SEO.content_share_facebook += "<meta property='og:site_name' content='" + SEO.url_current + "'/> ";
            SEO.content_share_facebook += "<meta property='og:description' content='" + SEO.meta_description + "'/>";
        }
    }

}