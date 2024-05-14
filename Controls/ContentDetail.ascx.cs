using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_ContentDetail : System.Web.UI.UserControl
{
    public DataRow dr;
    public DataTable dt;
    public int ID;
    public string seo_title, image;
    protected void Page_Load(object sender, EventArgs e)
    {
        ProccessParameter();

        if (!IsPostBack)
        {
            BindData();
            SetSEO();
        }
    }

    protected void ProccessParameter()
    {
        seo_title = ConvertUtility.ToString(Page.RouteData.Values["caturl"]);
    }

    protected void BindData()
    {
        dt = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("FriendlyUrl=N'{0}'", seo_title));
        if (Utils.CheckExist_DataTable(dt))
        {
            dr = dt.Rows[0];
            image = ConvertUtility.ToString(dr["Image_1"]);
           
                PageInfo.CategoryID = ConvertUtility.ToInt32(dr["ID"]);
           
            PageInfo.LinkEdit = "/admin/category/categoryupdate?id=" + dr["ID"];
        }

    }

    protected void SetSEO()
    {
        if (Utils.CheckExist_DataTable(dt))
        {
            SEO.meta_title = ConvertUtility.ToString(dr["MetaTitle"]);
            SEO.meta_keyword = ConvertUtility.ToString(dr["MetaKeyword"]);
            SEO.meta_description = ConvertUtility.ToString(dr["MetaDescription"]);

            if (SEO.meta_title.Length < 3)
                SEO.meta_title = ConvertUtility.ToString(dr["Name"]);
            if (SEO.meta_keyword.Length < 3)
            {
                string Meta_Keyword = "";
                if (Meta_Keyword.Length > 0)
                    SEO.meta_keyword = Meta_Keyword;
                else
                    SEO.meta_keyword = SEO.meta_title + ", " + ConfigWeb.MetaKeyword;
            }

            if (SEO.meta_description.Length < 3)
                SEO.meta_description = ConvertUtility.ToString(dr["Name"]) + ", " + ConfigWeb.MetaDescription;

            string linkDetail = TextChanger.GetLinkRewrite_Article(dr["FriendlyUrl"].ToString());
            SEO.url_current = linkDetail;
            SEO.canonical = linkDetail;
            SEO.content_share_facebook = "<meta property='og:title' content='" + SEO.meta_title + "'/>";
            SEO.content_share_facebook += "<meta property='og:type' content='website'/>";
            SEO.content_share_facebook += "<meta property='og:url' content='" + SEO.url_current + "'/>";
            SEO.content_share_facebook += "<meta property='og:image' content='" + image + "'/>";
            SEO.content_share_facebook += "<meta property='og:site_name' content='" + SEO.url_current + "'/> ";
            SEO.content_share_facebook += "<meta property='og:description' content='" + SEO.meta_description + "'/>";

            SEO_Schema.Type = "NewsArticle";
            SEO_Schema.Url = SEO.canonical;
            SEO_Schema.Title = SEO.meta_title;
            SEO_Schema.Description = SEO.meta_description;
            SEO_Schema.Image = image;
            SEO_Schema.AuthorType = "Organization";
            SEO_Schema.AuthorName = C.SITE_NAME;
            SEO_Schema.Publisher_Type = "Organization";
            SEO_Schema.Publisher_Name = C.SITE_NAME;
            SEO_Schema.Publisher_Logo = ConfigWeb.Logo;
        }
    }
}