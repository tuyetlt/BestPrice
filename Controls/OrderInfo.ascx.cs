using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_OrderInfo : System.Web.UI.UserControl
{
    public DataTable dt;
    public DataRow dr;
    public string content, token;
    public int countProduct = 0;
    public string totalValue = "0";
    public List<OrderInfo> orderInfoList;
    protected void Page_Load(object sender, EventArgs e)
    {
        token = ConvertUtility.ToString(Page.RouteData.Values["token"]);
        dt = SqlHelper.SQLToDataTable("tblOrder", "", string.Format("Token=N'{0}'", token), "ID", 1, 1);
        if (Utils.CheckExist_DataTable(dt))
        {
            dr = dt.Rows[0];
            totalValue = Utils.RemoveNonDigits(dr["PriceFinal"].ToString());
            content = mail_body();
            SetSEO();
        }
    }

    protected string mail_body()
    {
        string templateMail = GetFormatMail();
        templateMail = templateMail.Replace("{OrderID}", token);
        templateMail = templateMail.Replace("{Name}", ConvertUtility.ToString(dr["Name"]));
        templateMail = templateMail.Replace("{Tel}", ConvertUtility.ToString(dr["Phone"]));
        templateMail = templateMail.Replace("{Email}", ConvertUtility.ToString(dr["Email"]));
        templateMail = templateMail.Replace("{Address}", ConvertUtility.ToString(dr["Address"]));
        templateMail = templateMail.Replace("{Date}", ConvertUtility.ToDateTime(dr["CreatedDate"]).ToString("hh:mm:ss - dd/MM/yyyy"));
        templateMail = templateMail.Replace("{payment_menthod}", ConvertUtility.ToString(dr["PaymentMethod"]));
        templateMail = templateMail.Replace("{TongTien}", string.Format("{0:N0} VNĐ", dr["PriceFinal"]));
        templateMail = templateMail.Replace("{tel_web}", ConfigWeb.Hotline);
        templateMail = templateMail.Replace("{email_web}", ConfigWeb.Email_Display);
        templateMail = templateMail.Replace("{facebook_web}", "");
        templateMail = templateMail.Replace("{SiteLink}", ConfigWeb.SiteUrl);
        templateMail = templateMail.Replace("{SiteName}", ConfigWeb.SiteName);
        templateMail = templateMail.Replace("{Note}", ConvertUtility.ToString(dr["Name"]));
        templateMail = templateMail.Replace("{ProductList}", GetProductList());
        return templateMail;
    }
    protected string GetFormatMail()
    {
        string templatePath = HttpContext.Current.Server.MapPath(Globals.BaseUrl + "/assets/MailForm/order.html");
        string mailFormat = String.Empty;
        if (!System.IO.File.Exists(templatePath))
            return String.Empty;
        else
            return System.IO.File.ReadAllText(templatePath);
    }
    protected string GetProductList()
    {
        PageInfo.ControlName = string.Format("Thông tin đơn hàng số <b>{0}</b>", token);

        string strProductList = string.Empty;
        orderInfoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderInfo>>(ConvertUtility.ToString(dr["Json"]));
        if (orderInfoList.Count > 0)
        {
            foreach (OrderInfo orderInfo in orderInfoList)
            {
                string Coupon = "";
                if (orderInfo.NumberApplyVoucher > 0)
                    Coupon = string.Format(@"<p style=""color:#0045a3;font-style:italic"">(đã áp dụng mã giảm giá <b class=""coupon_code"">{0}</b> cho <b class=""coupon_quantity_apply"">{1}</b> sản phẩm)<p>", orderInfo.CouponCode, orderInfo.NumberApplyVoucher);

                DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", "ID=" + orderInfo.ProductID);
                if (Utils.CheckExist_DataTable(dtProduct))
                {
                    string link = TextChanger.GetLinkRewrite_Products(dtProduct.Rows[0]["FriendlyUrlCategory"], dtProduct.Rows[0]["FriendlyUrl"]);

                    strProductList += string.Format(@"<tr align=""center"">
                                                        <td>
                                                            <a href=""{0}""><img src=""{1}"" /></a>
                                                            
                                                        </td>
                                                         <td align=""left"">
                                                            <a href=""{0}"">{3}</a>
                                                            {2}
                                                            {4}
                                                        </td>
                                                        <td>
                                                            {5}
                                                        </td>
                                                      
                                                        <td align=""right"">
                                                            <span style=""color: #FF0000"">{6} VNĐ</span>
                                                        </td>
                                                        <td align=""right"">
                                                            <span style=""color: #FF0000"">{7} VNĐ</span>
                                                        </td>
                                                    </tr>", link, orderInfo.Image + "?width=100&height=100&quality=100", Coupon, orderInfo.Name, "", orderInfo.Quantity, string.Format("{0:N0}", orderInfo.Price), string.Format("{0:N0}", orderInfo.TotalPrice));
                }
            }
        }

        return strProductList;
    }

    protected void SetSEO()
    {
        PageInfo.CategoryID = 0;
        string Title = "Thông tin đơn hàng";
        string MetaTitle = Title + " - " + ConfigWeb.SiteName;
        string MetaKeyword = Title + ", " + ConfigWeb.MetaKeyword;
        string MetaDescription = Title + ", " + ConfigWeb.MetaDescription;
        string url = C.ROOT_URL + Request.RawUrl;
        PageUtility.AddTitle(this.Page, MetaTitle);
        PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
        PageUtility.AddMetaTag(this.Page, "description", MetaDescription);
        PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, ConfigWeb.Image, ConfigWeb.SiteName, MetaDescription);
        PageUtility.SetIndex(this.Page);
        PageUtility.AddDefaultMetaTag(this.Page);
    }

}