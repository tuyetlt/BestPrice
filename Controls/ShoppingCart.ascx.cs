using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class Controls_ShoppingCart : System.Web.UI.UserControl
{
    public string payment_method_mail = "";
    public string payment_status_mail = "";
    public string payment_bank = "";

    public decimal finalPrice = 0;

    public Hashtable hashtable = new Hashtable();
    log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Controls_ShoppingCart).Name);

    protected void Page_Load(object sender, EventArgs e)
    {
        PageInfo.ControlName = "Giỏ hàng của quý khách";
        if (!String.IsNullOrEmpty(Request.Form["done"]) && Request.Form["done"] == "1" && !Utils.IsNullOrEmpty(Request.Form["option_payment"]))
        {
            string option_payment = Request.Form["option_payment"].ToString();


            if (option_payment == "vnpay")
            {
                //////// VN Pay

                List<Valid> objsValid = new List<Valid>();


                /*if (Utils.Vaild_Field(objsValid))
                {

                    if (option_payment == "IB")
                    {
                        payment_method_mail = "Thanh toán bằng Internet Banking";
                        str_bankcode = option_payment;
                    }
                    else if (option_payment == "ATM")
                    {
                        payment_method_mail = "Thanh toán Online bằng thẻ ngân hàng nội địa ATM";
                        str_bankcode = Request.Form["bankcode"];
                    }
                    else if (option_payment == "VNPAYQR")
                    {
                        payment_method_mail = "Thanh toán qua QR CODE";
                        str_bankcode = option_payment;
                    }
                    else if (option_payment == "NL")
                        payment_method_mail = "Thanh toán bằng số dư Ví Ngân Lượng";
                    else if (option_payment == "VNMART")
                    {
                        payment_method_mail = "Thanh toán bằng số dư Ví VNMART";
                        str_bankcode = option_payment;
                        paymentHistory.BankCode = str_bankcode;
                        paymentHistory.BankName = Utils.GetBankNameByID(str_bankcode);
                    }
                    else if (option_payment == "INTCARD")
                    {
                        payment_method_mail = "Thanh toán bằng thẻ thanh toán quốc tế";
                        str_bankcode = option_payment;
                    } */


                //logger.Info("Chọn phương thức thanh toán =======>" + payment_method_mail);
                //string payment_method = "IB";
                //random = Utils.RandomString(50);

                //RequestInfo info = new RequestInfo();
                //info.Merchant_id = C.NganLuong_Merchant_id;
                //info.Merchant_password = C.NganLuong_Merchant_password;
                //info.Receiver_email = C.NganLuong_Receiver_email;
                //info.Buyer_address = "Email: " + customer.EmailAddress;
                //info.cur_code = "vnd";
                //info.bank_code = str_bankcode;

                //info.Total_amount = Utils.KillChars(Request.Form["totalamount"]);
                //info.fee_shipping = "0";
                //info.Discount_amount = "0";

                ////if (!string.IsNullOrEmpty(option_payment))
                ////{
                //info.return_url = C.ROOT_URL + "/hoan-tat-vnpay.html?payment-method=vnp";
                //info.cancel_url = C.ROOT_URL + "/hoan-tat-vnpay.html?mytoken=" + random;

                //info.Buyer_fullname = Utils.KillChars(customer.FullName);
                //info.Buyer_email = Utils.KillCharEmail(customer.EmailAddress);
                //info.Buyer_mobile = Utils.KillChars(customer.Phone);

                //saveTransactionHistory(info, payment_method);

                //info.order_description = "Nạp tiền số: " + info.Order_code.Replace("napxeng_", "");

                ////Get Config Info
                //string vnp_Returnurl = C.VNP_RETURN_URL; //URL nhan ket qua tra ve
                //string vnp_TmnCode = C.VNP_TMNCODE; //Ma website
                //string vnp_HashSecret = C.VNP_HASHSECRET; //Chuoi bi mat

                ////Build URL for VNPAY
                //VnPayLibrary vnpay = new VnPayLibrary();

                //vnpay.AddRequestData("vnp_Version", "2.0.0");
                //vnpay.AddRequestData("vnp_Command", "pay");
                //vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                //vnpay.AddRequestData("vnp_Locale", "vn");
                //vnpay.AddRequestData("vnp_CurrCode", "VND");
                //vnpay.AddRequestData("vnp_TxnRef", info.Order_code);
                //vnpay.AddRequestData("vnp_OrderInfo", info.order_description);
                //vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
                //vnpay.AddRequestData("vnp_Amount", (ConvertUtility.ToInt32(info.Total_amount) * 100).ToString());
                //vnpay.AddRequestData("vnp_ReturnUrl", info.return_url);
                //vnpay.AddRequestData("vnp_IpAddr", VnpUtils.GetIpAddress());
                //vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                //vnpay.AddRequestData("vnp_BankCode", payment_method);

                //string paymentUrl = vnpay.CreateRequestUrl(vnp_HashSecret);
                //logger.InfoFormat("VNPAY URL: {0}", paymentUrl);
                //Response.Redirect(paymentUrl);
                //}
                //}
                //}

            }
            else
            {
                hashtable["Name"] = Request.Form["name"];
                hashtable["Status"] = (int)OrderStatus.ProcessingInProgress;
                hashtable["Address"] = Request.Form["address"];
                hashtable["Tel"] = Request.Form["tel"];
                hashtable["Email"] = Request.Form["email"];
                hashtable["PaymentMethod"] = Request.Form["option_payment"];
                hashtable["NoteMember"] = Request.Form["note"];
                hashtable["TongTien"] = Request.Form["hdfTotalPrice"];
                hashtable["Note"] = Request.Form["note"];
                hashtable["MailTemplate"] = mail_body();
                UpdateDatabase();
                SendMail();
                Response.Redirect(string.Format("thong-tin-don-hang/{0}.html", hashtable["OrderID"]));
            }
        }

        SetSEO();
    }

    protected void SetSEO()
    {
        string Title = "Xem lại giỏ hàng và thanh toán";
        string MetaTitle = Title + " - " + ConfigWeb.SiteName;
        string MetaKeyword = Title + ", " + ConfigWeb.MetaKeyword;
        string MetaDescription = Title + ", " + ConfigWeb.MetaDescription;
        string url = C.ROOT_URL + Request.RawUrl;
        PageUtility.AddTitle(this.Page, MetaTitle);
        PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
        PageUtility.AddMetaTag(this.Page, "description", "Xem lại và cập nhật giỏ hàng theo ý bạn. Nếu tất cả đã đúng bạn có thể đặt hàng ngay tại đây");
        PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, C.ROOT_URL + ConfigWeb.Image, ConfigWeb.SiteName, MetaDescription);
        PageUtility.SetIndex(this.Page);
        PageUtility.AddDefaultMetaTag(this.Page);
    }

    protected void UpdateDatabase()
    {
        string token = ShoppingCart.CartToOrder(hashtable["Name"], hashtable["Tel"], hashtable["Address"], hashtable["Email"], hashtable["PaymentMethod"], hashtable["Note"], hashtable["MailTemplate"]);
        hashtable["OrderID"] = token;
    }

    #region Send Mail
    protected string mail_body()
    {
        string templateMail = GetFormatMail();
        templateMail = templateMail.Replace("{Name}", ConvertUtility.ToString(hashtable["Name"]));
        templateMail = templateMail.Replace("{Tel}", ConvertUtility.ToString(hashtable["Tel"]));
        templateMail = templateMail.Replace("{Email}", ConvertUtility.ToString(hashtable["Email"]));
        templateMail = templateMail.Replace("{Address}", ConvertUtility.ToString(hashtable["Address"]));
        templateMail = templateMail.Replace("{Date}", DateTime.Now.ToShortDateString());
        templateMail = templateMail.Replace("{payment_menthod}", ConvertUtility.ToString(hashtable["PaymentMethod"]));
        templateMail = templateMail.Replace("{TongTien}", string.Format("{0:N0} VNĐ", ConvertUtility.ToDecimal(Request.Form["hdfTotalPrice"])));
        templateMail = templateMail.Replace("{tel_web}", ConfigWeb.Hotline);
        templateMail = templateMail.Replace("{email_web}", ConfigWeb.Email_Display);
        templateMail = templateMail.Replace("{facebook_web}", "");
        templateMail = templateMail.Replace("{SiteLink}", ConfigWeb.SiteUrl);
        templateMail = templateMail.Replace("{SiteName}", ConfigWeb.SiteName);
        templateMail = templateMail.Replace("{Note}", ConvertUtility.ToString(hashtable["Note"]));
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
        string strProductList = string.Empty;
        finalPrice = 0;
        string Items = "[";
        List<OrderInfo> orderInfoList = ShoppingCart.GetOrderInfo(out finalPrice);
        if (orderInfoList.Count > 0)
        {
            foreach (OrderInfo orderInfo in orderInfoList)
            {
                DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrlCategory,FriendlyUrl", "ID=" + orderInfo.ProductID);
                if (Utils.CheckExist_DataTable(dtProduct))
                {
                    string link = TextChanger.GetLinkRewrite_Products(dtProduct.Rows[0]["FriendlyUrlCategory"], dtProduct.Rows[0]["FriendlyUrl"]);
                    strProductList += string.Format(@"<tr align=""center"">
                                                        <td>
                                                            <a href=""{0}""><img src=""{1}"" /></a>
                                                        </td>
                                                         <td align=""left"">
                                                            <a href=""{0}"">{2}</a>
                                                            {3}
                                                        </td>
                                                        <td>
                                                            {4}
                                                        </td>
                                                      
                                                        <td align=""right"">
                                                            <span style=""color: #FF0000"">{5} VNĐ</span>
                                                        </td>
                                                        <td align=""right"">
                                                            <span style=""color: #FF0000"">{6} VNĐ</span>
                                                        </td>
                                                    </tr>", link, orderInfo.Image + "?width=100&height=100&quality=100", orderInfo.Name, "", orderInfo.Quantity, string.Format("{0:N0}", orderInfo.Price), string.Format("{0:N0}", orderInfo.TotalPrice));
                }
            }
        }
        Items += "]";
        return strProductList;
    }

    protected void SendMail()
    {
        try
        {
            string from = C.SMTP_SENDER;
            string to = hashtable["Email"].ToString();
            string subject = "Đơn hàng của quý khách tại " + ConfigWeb.SiteName;
            string content = ConvertUtility.ToString(hashtable["MailTemplate"]);

            string[] Email_Receiving_List = ConfigWeb.Email_Receiving.Split(';');
            content = content.Replace("{OrderID}", ConvertUtility.ToString(hashtable["OrderID"]));

            string SMTPServer = C.SMTP_SERVER;
            string SMTPUser = C.SMTP_USERNAME;
            string SMTPPass = Crypto.DecryptData(Crypto.KeyCrypto, C.SMTP_PASSWORD);
            int SMTPPort = ConvertUtility.ToInt32(C.SMTP_PORT);

            using (MailMessage emailMessage = new MailMessage())
            {
                emailMessage.From = new MailAddress(from, ConfigWeb.SiteName);
                if (!string.IsNullOrEmpty(to))
                    emailMessage.To.Add(new MailAddress(to, hashtable["Name"].ToString()));

                if (Email_Receiving_List != null && Email_Receiving_List.Length > 0)
                {
                    foreach (string EmailReciving in Email_Receiving_List)
                    {
                        emailMessage.CC.Add(new MailAddress(EmailReciving));
                    }
                }

                emailMessage.IsBodyHtml = true;

                emailMessage.Subject = subject;
                emailMessage.Body = content;
                emailMessage.Priority = MailPriority.High;
                using (SmtpClient MailClient = new SmtpClient(SMTPServer, SMTPPort))
                {
                    MailClient.EnableSsl = ConvertUtility.ToBoolean(C.SMTP_SSL);
                    MailClient.Credentials = new System.Net.NetworkCredential(SMTPUser, SMTPPass);
                    MailClient.Send(emailMessage);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
    #endregion
}

