using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using log4net;

public partial class Controls_ProductCategory : System.Web.UI.UserControl
{
    public DataRow drCat, drProduct, drAttribute;
    public DataTable dtCat, dtProduct, dtAttribute;
    public int ID, RootID, RootChild, _totalProduct, _pageSize= C.ROWS_PRODUCTCATEGORY, _totalPage;
    public string caturl, thuonghieu, categoryTitle = "", thuonghieuParam = "";
    public List<string> RootList = new List<string>();
    protected static readonly ILog log = LogManager.GetLogger(typeof(Controls_ProductCategory));

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
        thuonghieu = RequestHelper.GetString("thuong-hieu", "");
        if (!string.IsNullOrEmpty(thuonghieu))
        {
            thuonghieuParam = "?thuong-hieu=" + thuonghieu;
        }
        else
        {
            thuonghieu = RequestHelper.GetString("thuong-hieu-may-lanh", "");
            if (!string.IsNullOrEmpty(thuonghieu))
                thuonghieuParam = "?thuong-hieu-may-lanh=" + thuonghieu;
        }

        //log.Info("Category:" + caturl);
    }

    protected void BindData()
    {
        dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("FriendlyUrl=N'{0}'", caturl));
        if (Utils.CheckExist_DataTable(dtCat))
        {
            drCat = dtCat.Rows[0];
            if (ConvertUtility.ToBoolean(drCat["Hide"]))
            {
                Response.Redirect(C.ROOT_URL);
                return;
            }
            PageInfo.CategoryID = ConvertUtility.ToInt32(drCat["ID"]);

          

            //Get Root ID
            DataRow drCatRoot = drCat;
            RootID = ConvertUtility.ToInt32(drCatRoot["ID"]);
            int count = 0;
            do
            {
                if (ConvertUtility.ToInt32(drCatRoot["ParentID"]) > 0)
                {
                    DataTable dtCatRoot = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, Name", string.Format("ID={0}", drCatRoot["ParentID"]));
                    if (Utils.CheckExist_DataTable(dtCatRoot))
                    {
                        drCatRoot = dtCatRoot.Rows[0];
                        RootID = ConvertUtility.ToInt32(drCatRoot["ID"]);
                        RootList.Add("aa");
                    }
                    count++;
                }
            }
            while (ConvertUtility.ToInt32(drCatRoot["ParentID"]) > 0);

            categoryTitle = drCat["Name"].ToString();

            if (!string.IsNullOrEmpty(thuonghieu))
            {
                dtAttribute = SqlHelper.SQLToDataTable("tblAttributes", "ID, Name, FriendlyUrl", string.Format("FriendlyUrl=N'{0}'", thuonghieu));
                if (Utils.CheckExist_DataTable(dtAttribute))
                {
                    drAttribute = dtAttribute.Rows[0];
                    categoryTitle += " " + drAttribute["Name"];
                }
            }

            string sort = ConfigWeb.SortProduct;
            if (Utils.CheckExist_DataTable(dtAttribute))
            {
                sort = string.Format("(CASE WHEN {0}=N'{1}' THEN 1 ELSE 0 END) DESC", "Brand", drAttribute["Name"]);
                if(!string.IsNullOrEmpty(ConfigWeb.SortProduct))
                    sort = string.Format("(CASE WHEN {0}=N'{1}' THEN 1 ELSE 0 END) DESC, {2}", "Brand", drAttribute["Name"], ConfigWeb.SortProduct);
            }
            string filterProduct = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%')", drCat["ID"]);
            dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", filterProduct, sort, 1, _pageSize, out _totalProduct);
            _totalPage = _totalProduct / _pageSize;

            if (_totalPage % _pageSize != 0)
                _totalPage++;

            CookieUtility.SetValueToCookie("pageIndex_Category", "2");
        }
    }


    protected void SetSeo()
    {
        if (!Utils.IsNullOrEmpty(drCat))
        {
            SEO.meta_title = ConvertUtility.ToString(drCat["MetaTitle"]);
            SEO.meta_keyword = ConvertUtility.ToString(drCat["MetaKeyword"]);
            SEO.meta_description = ConvertUtility.ToString(drCat["MetaDescription"]);

            SEO.url_current = TextChanger.GetLinkRewrite_Category(ConvertUtility.ToString(drCat["FriendlyUrl"])) + thuonghieuParam;
            SEO.canonical = TextChanger.GetLinkRewrite_Category(ConvertUtility.ToString(drCat["FriendlyUrl"])) + thuonghieuParam;
            if (SEO.meta_title.Length < 3)
                SEO.meta_title = categoryTitle;
            if (SEO.meta_keyword.Length < 3)
                SEO.meta_keyword = categoryTitle + ", " + ConfigWeb.MetaKeyword;

            if (SEO.meta_description.Length < 3)
                SEO.meta_description = categoryTitle + ", " + ConfigWeb.MetaDescription;

            SEO.content_share_facebook = "<meta property='og:title' content='" + SEO.meta_title + "'/>";
            SEO.content_share_facebook += "<meta property='og:type' content='website'/>";
            SEO.content_share_facebook += "<meta property='og:url' content='" + SEO.url_current + "'/>";

            string image = ConvertUtility.ToString(drCat["Image_1"]);
            if (string.IsNullOrEmpty(image))
                image = ConfigWeb.Image;
            SEO.content_share_facebook += "<meta property='og:image' content='" + C.ROOT_URL + image + "'/>";
            SEO.content_share_facebook += "<meta property='og:site_name' content='" + SEO.url_current + "'/> ";
            SEO.content_share_facebook += "<meta property='og:description' content='" + SEO.meta_description + "'/>";

            SEO_Schema.Type = "WebSite";
            SEO_Schema.Title = SEO.meta_title;
            SEO_Schema.Description = SEO.meta_description;
            SEO_Schema.Image = image;
            SEO_Schema.Url = SEO.canonical;
            SEO_Schema.AuthorName = C.SITE_NAME;
            SEO_Schema.Publisher_Type = "Organization";
            SEO_Schema.Publisher_Name = C.ROOT_URL.Replace("https://", "");
            SEO_Schema.Publisher_Logo = ConfigWeb.Logo;
            SEO_Schema.RatingCount = ConvertUtility.ToInt32(drCat["SchemaRatingCount"]);
            SEO_Schema.RatingValue = ConvertUtility.ToInt32(drCat["SchemaRatingValue"]);
            if (SEO_Schema.RatingValue > 93)
                SEO_Schema.ReviewRatingValue = 5;
            else
                SEO_Schema.ReviewRatingValue = 4;

            PageInfo.CurrentControl = ControlCurrent.ProductCategory.ToString();
        }
    }
}