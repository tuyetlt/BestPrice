using System;
using System.Web;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Collections;

public partial class Websosanh_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int page = RequestHelper.GetInt("page", 0);
        if (page < 1)
            page = ConvertUtility.ToInt32(Page.RouteData.Values["page"]);

        int pageSize = 100;

        if (page == 0)
        {
            DefaultPage();
        }
        else
        {
            DataTable dtWss = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "Name,Price,Price1,CategoryNameList,Description,Gallery,FriendlyUrlCategory,FriendlyUrl", string.Format("(Hide is null OR Hide=0) AND Gallery not like N'%{0}%'", C.ROOT_URL), "ID DESC", page, pageSize);
            if (Utils.CheckExist_DataTable(dtWss))
            {
                List<Product> products = new List<Product>();
                foreach (DataRow dr in dtWss.Rows)
                {
                    List<GalleryItem> galleryItems = JsonConvert.DeserializeObject<List<GalleryItem>>(dr["Gallery"].ToString());
                    //List<string> imageUrls = galleryItems.ConvertAll(item => C.ROOT_URL + item.Path);

                    //List<GalleryItem> galleryItems = JsonConvert.DeserializeObject<List<GalleryItem>>(dr["Gallery"].ToString());
                    List<string> imageUrls = new List<string>();

                    if (!Utils.IsNullOrEmpty(galleryItems))
                        imageUrls = galleryItems.ConvertAll(item => C.MAIN_URL + item.Path);

                    Product product = new Product
                    {
                        name = dr["Name"].ToString(),
                        url = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(dr["FriendlyUrlCategory"]), ConvertUtility.ToString(dr["FriendlyUrl"])),
                        price = dr["Price"] != DBNull.Value ? Convert.ToDecimal(dr["Price"]) : (decimal?)null,
                        originPrice = dr["Price1"] != DBNull.Value ? Convert.ToDecimal(dr["Price1"]) : (decimal?)null,
                        category = Utils.CommaSQLRemove(dr["CategoryNameList"].ToString()),
                        shortDescription = Utils.RemoveHtmlAndWhiteSpace(dr["Description"].ToString()),
                        imageUrls = imageUrls,
                        instock = 1,
                        promotion = ""
                    };

                    products.Add(product);
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonResult = serializer.Serialize(products);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "UTF-8";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/json";
                Response.Write(jsonResult);
                Response.End();
            }
        }
    }

    protected void DefaultPage()
    {
        Hashtable hashtable = new Hashtable();

        int TotalRecord = SqlHelper.GetCount(C.PRODUCT_TABLE, "Hide is null OR Hide=0");
        int PageSize = 100;

        int totalPage = TotalRecord / PageSize;

        if (TotalRecord % PageSize > 0)
            totalPage += 1;

        hashtable.Add("feed_url", C.ROOT_URL + "/websosanh/");
        hashtable.Add("total_page", totalPage);
        hashtable.Add("page_param", "page");

        Response.Clear();
        Response.Headers.Add("Content-type", "application/json");
        Response.Write(JSONHelper.ToJSON(hashtable));
        Response.End();

    }

}

public class Product
{

    public string name { get; set; }
    public string url { get; set; }
    public decimal? price { get; set; }
    public decimal? originPrice { get; set; }
    public List<string> imageUrls { get; set; }
    public string category { get; set; }
    public int instock { get; set; }
    public string shortDescription { get; set; }
    public string promotion { get; set; }

}

