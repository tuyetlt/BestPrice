using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

public partial class ajax_Controls_Paging : System.Web.UI.UserControl
{
    int pageIndex = 1, pageSize = 20, categoryID = 0;
    string sort = string.Empty, modul=string.Empty, keyword=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessParameter();
        if (!Page.IsPostBack)
            Paging();
    }

    protected void ProcessParameter()
    {
        pageIndex = RequestHelper.GetInt("page", 1);
        pageSize = RequestHelper.GetInt("pageSize", C.ROWS_PRODUCTCATEGORY);
        sort = RequestHelper.GetString("sort", ConfigWeb.SortProduct);
        modul = RequestHelper.GetString("modul", "product");
        keyword = RequestHelper.GetString("keyword", string.Empty);
        categoryID = RequestHelper.GetInt("catid", 0);
    }

    protected void Paging()
    {
        Response.Clear();
        Response.Headers.Add("Content-type", "application/json");
        List<Product> productList = new List<Product>();
        if (modul == "product")
        {
            string filterProduct = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%')", categoryID);

            DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", filterProduct, ConfigWeb.SortProduct, pageIndex, pageSize);
            if(Utils.CheckExist_DataTable(dt))
            {
                foreach(DataRow dr in dt.Rows)
                {
                    string image = Utils.GetFirstImageInGallery_Json(ConvertUtility.ToString(dr["Gallery"]), 300, 300);
                    Product product = new Product();
                    product.ID = ConvertUtility.ToInt32(dr["ID"]);
                    product.Name = ConvertUtility.ToString(dr["Name"]);
                    product.FriendlyUrl = ConvertUtility.ToString(dr["FriendlyUrl"]);
                    product.FriendlyUrlCategory = ConvertUtility.ToString(dr["FriendlyUrlCategory"]);
                    product.Image = image;
                    product.Price = ConvertUtility.ToInt32(dr["Price"]);
                    product.Price1 = ConvertUtility.ToInt32(dr["Price1"]);
                    product.HashTagUrlList = ConvertUtility.ToString(dr["HashTagUrlList"]);
                    product.PricePercent = SqlHelper.GetPricePercent(ConvertUtility.ToInt32(dr["ID"]));
                    product.Link = TextChanger.GetLinkRewrite_Products(dr["FriendlyUrlCategory"], dr["FriendlyUrl"]);
                    productList.Add(product);
                }
            }
        }
        else if(modul=="article")
        {

        }    
        Response.Write(JSONHelper.ToJSON(productList));
        Response.End();
    }
}