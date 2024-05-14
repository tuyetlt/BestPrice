using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_NewsDetail : System.Web.UI.UserControl
{
    public DataRow dr, drCategory;
    public DataTable dt;
    public int ID;
    public string seo_title, caturl, image;
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
        dt = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "", string.Format("FriendlyUrl=N'{0}'", seo_title));
        if (Utils.CheckExist_DataTable(dt))
        {
            dr = dt.Rows[0];
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
    
    protected void SetSEO()
    {
        SEO.meta_title = ConvertUtility.ToString(dr["MetaTitle"]);
        SEO.meta_keyword = ConvertUtility.ToString(dr["MetaKeyword"]);
        SEO.meta_description = ConvertUtility.ToString(dr["MetaDescription"]);

        if (SEO.meta_title.Length < 3)
            SEO.meta_title = ConvertUtility.ToString(dr["Name"]);
        if (SEO.meta_keyword.Length < 3)
        {
            string Meta_Keyword = "";
            //if (CategoryTagList != null && CategoryTagList.Count > 0)
            //{
            //    foreach (Category cat in CategoryTagList)
            //    {
            //        if (cat.Moduls != "Hashtag")
            //        {
            //            if (!string.IsNullOrEmpty(Meta_Keyword))
            //                Meta_Keyword += ",";
            //            Meta_Keyword += cat.CategoryName;
            //        }
            //    }
            //}

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
        SEO_Schema.PublisherDate = ConvertUtility.ToDateTime(dr["StartDate"]).ToString("yyyy-MM-dd");
        SEO_Schema.PublisherModify = ConvertUtility.ToDateTime(dr["EditedDate"]).ToString("yyyy-MM-dd");

        //SEO_Schema.RatingCount = Product.UnLikeVote;
        //SEO_Schema.RatingValue = Product.LikeVote;
    }

}