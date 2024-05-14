using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public partial class ajax_Controls_Dynamic : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool ajax = RequestHelper.GetBoolean("ajax", false);
        string services = RequestHelper.GetString("services", string.Empty);
        int RecordID = RequestHelper.GetInt("id", 0);
        int Quantity = RequestHelper.GetInt("quantity", 0);
        Response.Clear();
        Response.Headers.Add("Content-type", "application/json");
        Hashtable hashtable = new Hashtable();
        try
        {
            if (!string.IsNullOrEmpty(services))
            {
                var arrayService = services.Split(',');
                if (arrayService.Count() > 0)
                {
                    foreach (var service in arrayService)
                    {
                        if (service.Trim() == "cart_count")
                        {
                            hashtable.Add(service, ShoppingCart.CartCount);
                        }
                        if (service.Trim() == "product_viewed")
                        {
                            string currentCookie = string.Empty;
                            if (CookieUtility.GetValueFromCookie("product_viewed") != null)
                                currentCookie = CookieUtility.GetValueFromCookie("product_viewed");
                            List<string> stringCookieList = Utils.ConvertStringToList(currentCookie);
                            int total_product_viewed = stringCookieList.Count;

                            hashtable.Add(service, total_product_viewed);
                        }

                        if (service.Trim() == "product_fav_add")
                        {
                            string currentCookie = string.Empty;
                            if (CookieUtility.GetValueFromCookie("product_fav") != null)
                                currentCookie = CookieUtility.GetValueFromCookie("product_fav");

                            List<string> stringCookieList = Utils.ConvertStringToList(currentCookie);
                            stringCookieList = Utils.AddTitemToArrayString(stringCookieList, RecordID.ToString());
                            currentCookie = Utils.ConvertArrayToString(stringCookieList);
                            CookieUtility.SetValueToCookie("product_fav", currentCookie);

                            int total_product_viewed = stringCookieList.Count;
                            hashtable.Add("product_fav", total_product_viewed);
                        }

                        if (service.Trim() == "product_fav_remove")
                        {
                            string currentCookie = string.Empty;
                            if (CookieUtility.GetValueFromCookie("product_fav") != null)
                                currentCookie = CookieUtility.GetValueFromCookie("product_fav");

                            List<string> stringCookieList = Utils.ConvertStringToList(currentCookie);
                            stringCookieList = Utils.RemoveTitemToArrayString(stringCookieList, RecordID.ToString());
                            currentCookie = Utils.ConvertArrayToString(stringCookieList);
                            CookieUtility.SetValueToCookie("product_fav", currentCookie);

                            int total_product_viewed = stringCookieList.Count;
                            hashtable.Add("product_fav", total_product_viewed);
                        }


                        if (service.Trim() == "product_fav")
                        {
                            string currentCookie = "";
                            if (CookieUtility.GetValueFromCookie("product_fav") != null)
                                currentCookie = CookieUtility.GetValueFromCookie("product_fav");
                            List<string> stringCookieList = Utils.ConvertStringToList(currentCookie);
                            int total_product_viewed = stringCookieList.Count;

                            hashtable.Add(service, total_product_viewed);
                        }

                        if (service == "cart_update")
                        {
                            if (RecordID > 0)
                            {
                                if (Quantity > 0)
                                    ShoppingCart.AddToCart(RecordID, Quantity);
                                else
                                    ShoppingCart.DeleteProduct(RecordID);
                            }


                            decimal finalPrice = 0;
                            List<OrderInfo> orderInfoList = ShoppingCart.GetOrderInfo(out finalPrice);

                            int count = 0;
                            string Items = "[";
                            if (orderInfoList.Count > 0)
                            {
                                foreach (OrderInfo orderInfo in orderInfoList)
                                {
                                    count++;
                                    string dau_phay = ",";
                                    if (count == orderInfoList.Count)
                                        dau_phay = string.Empty;

                                    Items += string.Format(@"{{""id"":""{0}"",""productName"":""{1}"",""quantity"":""{2}"", ""price"":""{3}"", ""voucher_quantity"":""{4}""}}{5}", orderInfo.ProductID, orderInfo.Name, orderInfo.Quantity, orderInfo.Price, orderInfo.NumberApplyVoucher, dau_phay);
                                }
                            }
                            Items += "]";

                            hashtable.Add(service, RecordID);
                            hashtable.Add("quantity", count);
                            hashtable.Add("finalPrice", string.Format("{0:0}", finalPrice));
                            hashtable.Add("finalPriceVND", string.Format("{0:N0} VNĐ", finalPrice));
                            hashtable.Add("jsonProduct", Items);
                            hashtable.Add("cart_count", ShoppingCart.CartCount);

                        }
                        if (service == "cart_total")
                        {

                        }
                        if (service == "cart_del")
                        {
                            ShoppingCart.DeleteProduct(RecordID);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }

        Response.Write(JSONHelper.ToJSON(hashtable));
        Response.End();
    }

    private void _productSearch()
    {
        Response.Clear();
        Response.Headers.Add("Content-type", "application/json");
        Hashtable hashtable = new Hashtable();


        Response.Write(JSONHelper.ToJSON(hashtable));
        Response.End();
    }
}