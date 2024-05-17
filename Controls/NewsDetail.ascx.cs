using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_NewsDetail : System.Web.UI.UserControl
{
    public DataRow dr, drCategory;
    public DataTable dt, dtRef;
    public int ID;
    public string seo_title, caturl, image, longDescription;
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
        seo_title = ConvertUtility.ToString(Page.RouteData.Values["seo_title"]);
    }

    protected void BindData()
    {
        DataTable dt = new DataTable();
        if (Utils.CheckExist_DataTable(dtRef))
            dt = dtRef;
        else
            dt = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "", string.Format("FriendlyUrl=N'{0}'", seo_title));

        if (Utils.CheckExist_DataTable(dt))
        {
            dr = dt.Rows[0];

            string domain = "https://dienmayhoanghai.vn";
            longDescription = AddDomainToRelativeUrls(dr["LongDescription"].ToString(), domain);


            image = Utils.GetFirstImageInGallery_Json(ConvertUtility.ToString(dr["gallery"]));
            string[] cateList = ConvertUtility.ToString(dr["CategoryIDList"]).Trim(',').Split(',');
            foreach (string categoryid in cateList)
            {
                PageInfo.CategoryID = ConvertUtility.ToInt32(categoryid);
            }

            DataTable dtCategory = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "Name,BreadCrumbJson", string.Format("ID={0}", PageInfo.CategoryID));
            if (Utils.CheckExist_DataTable(dtCategory))
                drCategory = dtCategory.Rows[0];

            PageInfo.LinkEdit = "/admin/article/articleupdate?id=" + dr["ID"];

            //tăng 1 lên view
            using (var dbx = new MetaNET.DataHelper.SqlService())
            {
                int curenView = ConvertUtility.ToInt32(dr["Viewed"]) + 1;
                string sqlUpdateView = "Update tblArticle SET Viewed=@totalview WHERE ID=@articleID";
                dbx.AddParameter("@articleID", SqlDbType.Int, dr["ID"]);
                dbx.AddParameter("@totalview", SqlDbType.Int, curenView);
                dbx.ExecuteSql(sqlUpdateView);
            }
        }
        
    }

    static string AddDomainToRelativeUrls(string html, string domain)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var imgNodes = doc.DocumentNode.SelectNodes("//img[@src]");

        if (imgNodes != null)
        {
            foreach (var imgNode in imgNodes)
            {
                var srcValue = imgNode.GetAttributeValue("src", "");
                if (!string.IsNullOrEmpty(srcValue) && Uri.IsWellFormedUriString(srcValue, UriKind.Relative))
                {
                    imgNode.SetAttributeValue("src", domain + srcValue);
                }
            }
        }

        return doc.DocumentNode.OuterHtml;
    }

    protected void SetSEO()
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

        string url = TextChanger.GetLinkRewrite_Article(ConvertUtility.ToString(dr["FriendlyUrl"]));

        PageUtility.AddTitle(this.Page, MetaTitle);
        PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
        PageUtility.AddMetaTag(this.Page, "description", MetaDescription);
        PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, image, ConfigWeb.SiteName, MetaDescription);
        PageUtility.AddCanonicalLink(this.Page, url);

        PageUtility.AddDefaultMetaTag(this.Page);

        SEO_Schema.Type = "NewsArticle";
        SEO_Schema.Url = SEO.canonical;
        SEO_Schema.Title = SEO.meta_title;
        SEO_Schema.Description = Utils.QuoteRemove(SEO.meta_description);
        SEO_Schema.Image = image;
        SEO_Schema.AuthorType = "Organization";
        SEO_Schema.AuthorName = C.SITE_NAME;
        SEO_Schema.Publisher_Type = "Organization";
        SEO_Schema.Publisher_Name = C.SITE_NAME;
        SEO_Schema.Publisher_Logo = ConfigWeb.Logo;
        SEO_Schema.PublisherDate = ConvertUtility.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd");
        SEO_Schema.PublisherModify = ConvertUtility.ToDateTime(dr["EditedDate"]).ToString("yyyy-MM-dd");

        //SEO_Schema.RatingCount = ConvertUtility.ToInt32(dr["SchemaRatingCount"]);
        //SEO_Schema.RatingValue = ConvertUtility.ToInt32(dr["SchemaRatingValue"]);
        //if (SEO_Schema.RatingValue > 93)
        //    SEO_Schema.ReviewRatingValue = 5;
        //else
        //    SEO_Schema.ReviewRatingValue = 4;

        PageInfo.CurrentControl = ControlCurrent.NewsDetail.ToString();
    }

}