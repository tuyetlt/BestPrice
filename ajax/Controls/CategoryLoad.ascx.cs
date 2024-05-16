using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_Controls_CategoryLoad : System.Web.UI.UserControl
{
    public DataRow drCat, drProduct, drArticle;
    public DataTable dtCat, dtProduct, dtArticle;
    public string action = RequestHelper.GetString("action", "product_list");

    protected void Page_Load(object sender, EventArgs e)
    {
        string CategoryIDList = RequestHelper.GetString("CategoryIDList", string.Empty);
        string CategoryIDParentList = RequestHelper.GetString("CategoryIDParentList", string.Empty);
        int categoryID = RequestHelper.GetInt("categoryID", 0);
        string keyword = RequestHelper.GetString("keyword", string.Empty);
        int pageIndex = RequestHelper.GetInt("pageIndex", 1);
        int mainCategory = RequestHelper.GetInt("mainCategory", 1);
        //string rootFilterCategoryID = ""; // RequestHelper.GetString("rootFilterCategoryID", ""); //Tạm bỏ Root đi để lấy đúng theo CategoryID
        string attributeIDList = RequestHelper.GetString("attributeIDList", "");

        int _totalProduct = 0;

        if (action == "product_list")
        {
            string sort = ConfigWeb.SortProduct;
            dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", "ID=" + categoryID);
            if (Utils.CheckExist_DataTable(dtCat))
            {
                drCat = dtCat.Rows[0];
            }


            if (!string.IsNullOrEmpty(attributeIDList))
            {
                string filterAttr = string.Empty;
                string[] attributeID_Array = attributeIDList.Trim(',').Split(',');
                if (attributeID_Array != null && attributeID_Array.Length > 0)
                {
                    int count = 0;
                    foreach (string fil in attributeID_Array)
                    {
                        if (count > 0)
                            filterAttr += " OR ";
                        filterAttr += string.Format("(Hide is null OR Hide=0) AND AttributesIDList LIKE '%,{0},%' AND (CategoryIDList Like '%,{1},%' OR CategoryIDParentList Like '%,{1},%')", fil, categoryID);
                        count++;
                    }
                }

                //Tạo điều kiện lọc cho Attr
                string filterAttrParent = "";

                DataTable dtAttr = SqlHelper.SQLToDataTable("tblAttributes", "ID, ParentID", "ID in (" + attributeIDList.Trim(',') + ")", "ParentID");


                if (Utils.CheckExist_DataTable(dtAttr))
                {
                    int ParentID = 0;
                    int countAND = 0;
                    int countOR = 0;

                    //filterAttrParent = "(";
                    foreach (DataRow drAtrr in dtAttr.Rows)
                    {
                        if (ParentID == 0)
                            ParentID = ConvertUtility.ToInt32(drAtrr["ParentID"]);

                        if (ParentID != ConvertUtility.ToInt32(drAtrr["ParentID"]))
                        {
                            //if (countAND > 0)
                            filterAttrParent += ") AND (";
                            countAND++;
                            ParentID = ConvertUtility.ToInt32(drAtrr["ParentID"]);
                        }
                        else
                        {
                            if (countOR == 0)
                                filterAttrParent += " (";
                            else
                                filterAttrParent += " OR ";

                            countOR++;
                        }

                        filterAttrParent += string.Format("AttributesIDList LIKE '%,{0},%'", drAtrr["ID"]);
                    }

                    if (!filterAttrParent.EndsWith(")"))
                        filterAttrParent += ")";
                }

                if (!string.IsNullOrEmpty(filterAttrParent))
                    filterAttrParent = string.Format(" AND {0}", filterAttrParent);


                string filterProduct = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDParentList LIKE '%,{0},%' OR CategoryIDList LIKE '%,{0},%' OR TagIDList Like N'%,{0},%'){1}", categoryID, filterAttrParent);
                dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", filterProduct, ConfigWeb.SortProduct, pageIndex, C.ROWS_PRODUCTCATEGORY, out _totalProduct);

                //if (Utils.CheckExist_DataTable(dtProduct))
                //{
                //    Response.Clear();
                //    Response.Headers.Add("Content-type", "application/json");
                //    List<Product> productList = new List<Product>();

                //    if (Utils.CheckExist_DataTable(dtProduct))
                //    {
                //        foreach (DataRow dr in dtProduct.Rows)
                //        {
                //            string image = Utils.GetFirstImageInGallery_Json(ConvertUtility.ToString(dr["Gallery"]), 300, 300);
                //            Product product = new Product();
                //            product.ID = ConvertUtility.ToInt32(dr["ID"]);
                //            product.Name = ConvertUtility.ToString(dr["Name"]);
                //            product.FriendlyUrl = ConvertUtility.ToString(dr["FriendlyUrl"]);
                //            product.FriendlyUrlCategory = ConvertUtility.ToString(dr["FriendlyUrlCategory"]);
                //            product.Image = image;
                //            product.Price = ConvertUtility.ToInt32(dr["Price"]);
                //            product.Price1 = ConvertUtility.ToInt32(dr["Price1"]);
                //            product.HashTagUrlList = ConvertUtility.ToString(dr["HashTagUrlList"]);
                //            product.PricePercent = SqlHelper.GetPricePercent(ConvertUtility.ToInt32(dr["ID"]));
                //            product.Link = TextChanger.GetLinkRewrite_Products(dr["FriendlyUrlCategory"], dr["FriendlyUrl"]);
                //            productList.Add(product);
                //        }
                //    }

                //    Response.Write(JSONHelper.ToJSON(productList));
                //    Response.End();

                //}


                CookieUtility.SetValueToCookie("TotalProduct", _totalProduct.ToString());
                pageIndex += 1;
                CookieUtility.SetValueToCookie("pageIndex_Category", pageIndex.ToString());
            }
            else if (categoryID > 0 || !string.IsNullOrEmpty(keyword))
            {
                string filterProduct = "";
                if (categoryID > 0)
                {
                    string thuonghieuParam = "";
                    string thuonghieu = RequestHelper.GetString("thuonghieu", "");
                    if (string.IsNullOrEmpty(thuonghieu))
                    {
                        thuonghieu = RequestHelper.GetString("thuong-hieu-may-lanh", "");
                        if (string.IsNullOrEmpty(thuonghieu))
                            thuonghieuParam = "?thuong-hieu-may-lanh=" + thuonghieu;
                    }
                    else
                    {
                        thuonghieuParam = "?thuong-hieu=" + thuonghieu;
                    }
                    DataRow drAttribute = null;
                    DataTable dtAttribute = null;
                    if (!string.IsNullOrEmpty(thuonghieu))
                    {
                        dtAttribute = SqlHelper.SQLToDataTable("tblAttributes", "ID, Name, FriendlyUrl", string.Format("FriendlyUrl=N'{0}'", thuonghieu));
                        if (Utils.CheckExist_DataTable(dtAttribute))
                        {
                            drAttribute = dtAttribute.Rows[0];
                        }
                    }


                    if (Utils.CheckExist_DataTable(dtAttribute))
                    {
                        sort = string.Format("(CASE WHEN {0}=N'{1}' THEN 1 ELSE 0 END) DESC", "Brand", drAttribute["Name"]);
                        if (!string.IsNullOrEmpty(ConfigWeb.SortProduct))
                            sort = string.Format("(CASE WHEN {0}=N'{1}' THEN 1 ELSE 0 END) DESC, {2}", "Brand", drAttribute["Name"], ConfigWeb.SortProduct);
                    }

                    filterProduct = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%')", categoryID);
                }
                else
                    filterProduct = string.Format(@"Name Like N'%{0}%' OR NameUnsign Like N'%{0}%'", keyword);
                dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", filterProduct, sort, pageIndex, C.ROWS_PRODUCTCATEGORY, out _totalProduct);
                CookieUtility.SetValueToCookie("TotalProduct", _totalProduct.ToString());
                pageIndex += 1;
                CookieUtility.SetValueToCookie("pageIndex_Category", pageIndex.ToString());
            }
        }
        else if (action == "article_list")
        {
            string filterProduct = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryaIDParentList Like N'%,{0},%')", categoryID);
            dtArticle = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "", filterProduct, ConfigWeb.SortArticle, pageIndex, C.ROWS_PRODUCTCATEGORY, out _totalProduct);
            CookieUtility.SetValueToCookie("TotalArticle", _totalProduct.ToString());
            pageIndex += 1;
            CookieUtility.SetValueToCookie("pageIndex_Category", pageIndex.ToString());
        }
    }
}