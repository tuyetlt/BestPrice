using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_ContentDetail : System.Web.UI.UserControl
{
    public DataRow dr;
    public DataTable dt, dtRef;
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
        if (Utils.CheckExist_DataTable(dtRef))
            dt = dtRef;
        else
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
            string MetaTitle = ConvertUtility.ToString(dr["MetaTitle"]);
            string MetaKeyword = ConvertUtility.ToString(dr["MetaKeyword"]);
            string MetaDescription = ConvertUtility.ToString(dr["MetaDescription"]);

            if (MetaTitle.Length < 3)
                MetaTitle = ConvertUtility.ToString(dr["Name"]);
            if (MetaKeyword.Length < 3)
                MetaKeyword = MetaTitle + ", " + ConfigWeb.MetaKeyword;
            if (MetaDescription.Length < 3)
                MetaDescription = MetaTitle + ", " + ConfigWeb.MetaDescription;

            string url = TextChanger.GetLinkRewrite_Menu(ConvertUtility.ToString(dr["FriendlyUrl"]));
            
            PageUtility.AddTitle(this.Page, MetaTitle);
            PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
            PageUtility.AddMetaTag(this.Page, "description", MetaDescription);
            PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, image, ConfigWeb.SiteName, MetaDescription);
            PageUtility.AddCanonicalLink(this.Page, url);
            int SEOFlags = ConvertUtility.ToInt32(dr["SeoFlags"]);
  
            PageUtility.AddDefaultMetaTag(this.Page);

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

        PageInfo.CurrentControl = ControlCurrent.ContentDetail.ToString();
    }

}