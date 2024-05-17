using System;
using System.Data;
using System.Collections.Generic;
using System.Web.UI;
using log4net;

public partial class Controls_ProductCategory : System.Web.UI.UserControl
{
    public DataRow drCat, drProduct, drAttribute;
    public DataTable dtCat, dtProduct, dtAttribute, dtRef;
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
        if (Utils.CheckExist_DataTable(dtRef))
            dtCat = dtRef;
        else
            dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,Name,FriendlyUrl,Image_1,Hide,ParentID,AttributesIDList,LongDescription,TagIDList,MetaTitle,MetaKeyword,MetaDescription,SchemaRatingCount,SchemaRatingValue,Moduls", string.Format("{0} AND Moduls=N'{1}' AND FriendlyUrl=N'{2}'", Utils.CreateFilterHide, "category", caturl));

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
        }
        PageUtility.AddDefaultMetaTag(this.Page);
        PageInfo.CurrentControl = ControlCurrent.ProductCategory.ToString();
    }

}