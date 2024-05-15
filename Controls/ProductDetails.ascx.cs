using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_ProductDetails : System.Web.UI.UserControl
{
    public DataRow dr, drCat;
    public DataTable dtNews, dtBannerRight;
    public int ID, CategoryID;
    public string purl, caturl, image, longDescription;
    protected void Page_Load(object sender, EventArgs e)
    {
        ProccessParameter();

        if (!IsPostBack)
        {
            BindData();
            AddToCart();
            SetSEO();
        }
    }

    protected void ProccessParameter()
    {
        purl = ConvertUtility.ToString(Page.RouteData.Values["purl"]);
        caturl = ConvertUtility.ToString(Page.RouteData.Values["caturl"]);
    }

    protected void BindData()
    {
        DataTable dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ParentIDList,HashTagUrlList", string.Format("FriendlyUrl='{0}'", caturl));
        if (Utils.CheckExist_DataTable(dtCat))
        {
            drCat = dtCat.Rows[0];
        }

        DataTable dt = SqlHelper.SQLToDataTable("tblProducts", "", string.Format("FriendlyUrl=N'{0}'", purl));
        if (Utils.CheckExist_DataTable(dt))
        {
            dr = dt.Rows[0];
            string domain = "https://dienmayhoanghai.vn";
            longDescription = AddDomainToRelativeUrls(dr["LongDescription"].ToString(), domain);

            ProductViewed();
            image = Utils.GetFirstImageInGallery_Json(ConvertUtility.ToString(dr["gallery"]));

            string[] cateList = ConvertUtility.ToString(dr["CategoryIDList"]).Trim(',').Split(',');
            if (cateList != null && cateList.Length > 0)
            {
                PageInfo.CategoryID = ConvertUtility.ToInt32(ConvertUtility.ToInt32(cateList[0]));
            }

            PageInfo.LinkEdit = "/admin/product/productupdate?id=" + dr["ID"];

        }

        dtNews = SqlHelper.SQLToDataTable("tblArticle", "", "", "ID DESC", 1, 10);

        string filterBannerRight = string.Format("Flags & {0} <> 0 AND CategoryIDList like N'%,{1},%'", (int)BannerPositionFlag.RightProductDetail, PageInfo.CategoryID);
        dtBannerRight = SqlHelper.SQLToDataTable("tblBanner", "", filterBannerRight, "Sort", 1, 10);
    }


    protected void ProductViewed()
    {
        string currentCookie = "0";

        if (CookieUtility.GetValueFromCookie("product_viewed") != null)
            currentCookie = CookieUtility.GetValueFromCookie("product_viewed");
        List<string> stringCookieList = Utils.ConvertStringToList(currentCookie);
        stringCookieList = Utils.AddTitemToArrayString(stringCookieList, ConvertUtility.ToString(dr["ID"]));
        currentCookie = Utils.ConvertArrayToString(stringCookieList);
        CookieUtility.SetValueToCookie("product_viewed", currentCookie);
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


    protected void AddToCart()
    {
        if (!String.IsNullOrEmpty(Request.Form["done_giohang"]) && Request.Form["done_giohang"] == "1")
        {
            int quantity = ConvertUtility.ToInt32(Request.Form["quantity"]);
            int pid = ConvertUtility.ToInt32(Request.Form["hdfProductID"]);

            ShoppingCart.AddToCart(pid, quantity);

            Response.Redirect(C.ROOT_URL + "/gio-hang.html");
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

        SEO.url_current = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(dr["FriendlyUrlCategory"]), ConvertUtility.ToString(dr["FriendlyUrl"]));
        SEO.canonical = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(dr["FriendlyUrlCategory"]), ConvertUtility.ToString(dr["FriendlyUrl"]));
        SEO.content_share_facebook = "<meta property='og:title' content='" + SEO.meta_title + "'/>";
        SEO.content_share_facebook += "<meta property='og:type' content='website'/>";
        SEO.content_share_facebook += "<meta property='og:url' content='" + SEO.url_current + "'/>";
        SEO.content_share_facebook += "<meta property='og:image' content='" + image + "'/>";
        SEO.content_share_facebook += "<meta property='og:site_name' content='" + SEO.url_current + "'/> ";
        SEO.content_share_facebook += "<meta property='og:description' content='" + SEO.meta_description + "'/>";

        SEO_Schema.Type = "Product";
        SEO_Schema.Title = SEO.meta_title;
        SEO_Schema.SKU = ConvertUtility.ToString(dr["ID"]);
        SEO_Schema.Description = SEO.meta_description;
        SEO_Schema.Image = image;
        decimal MinPrice = SqlHelper.GetPrice_Decimal(ConvertUtility.ToInt32(dr["ID"]), "Price", true);
        SEO_Schema.Url = SEO.canonical;
        SEO_Schema.Price = MinPrice.ToString();
        SEO_Schema.AuthorName = C.SITE_NAME;
        SEO_Schema.Publisher_Type = "Organization";
        SEO_Schema.Publisher_Name = C.SITE_NAME;
        SEO_Schema.Publisher_Logo = ConfigWeb.Logo;
        SEO_Schema.RatingCount = ConvertUtility.ToInt32(dr["SchemaRatingCount"]);
        SEO_Schema.RatingValue = ConvertUtility.ToInt32(dr["SchemaRatingValue"]);
        SEO_Schema.Brand = ConvertUtility.ToString(dr["Brand"]);
        if (SEO_Schema.RatingValue > 93)
            SEO_Schema.ReviewRatingValue = 5;
        else
            SEO_Schema.ReviewRatingValue = 4;

        PageInfo.CurrentControl = ControlCurrent.ProductDetails.ToString();
    }
}