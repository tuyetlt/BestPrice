using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaNET.DataHelper;
using Ebis.Utilities;
using System.Net.Mail;
public partial class login_Controls_ForgotPassword : System.Web.UI.UserControl
{
    public bool ResetOK = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CookieUtility.SetValueToCookie("notice_success", "Trình tự gene nCoV tại Hải Dương tương tự chủng ở Đà Nẵng, cho thấy nguồn lây nhiễm Covid-19 Hải Dương liên quan tới ổ dịch Đà Nẵng.");

            Update();
        }
    }

    protected void Update()
    {
        string click_action = Request.Form["done"];
        if (!String.IsNullOrEmpty(click_action) && click_action == "1")
        {
            using (var db = SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("SELECT * FROM tblAdminUser WHERE Email=N'{0}'", Utils.KillCharEmail(Request["email"]).ToString().Trim());
                DataTable dtPermission = db.ExecuteSqlDataTable(sqlQuery);
                if (dtPermission != null && dtPermission.Rows.Count > 0)
                {
                    using (var dbx = SqlService.GetSqlService())
                    {
                        string random = Utils.RandomStringNumberANDCharacter(20);
                        string sqlQueryUpdate = string.Format("Update tblAdminUser set Token=N'{0}' where Email=N'{1}'", random, Utils.KillCharEmail(Request["email"]).ToString().Trim());
                        db.ExecuteSql(sqlQueryUpdate);

                        SendMail(dtPermission, random);

                        CookieUtility.SetValueToCookie("notice", "forgotpass_sendmail_success");

                        //Response.Redirect(Request.RawUrl);
                        ResetOK = true;
                    }
                }
                else
                {
                    CookieUtility.SetValueToCookie("notice", "forgotpass_sendmail_error");
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }


    #region Send Mail


    protected void SendMail(DataTable dt, string token)
    {
        try
        {
            string from = C.SMTP_SENDER;
            string to = dt.Rows[0]["Email"].ToString();
            string subject = "Đặt lại mật khẩu tại " + ConfigWeb.SiteName;
            string content = mail_body(dt, token);

            string SMTPServer = C.SMTP_SERVER;
            string SMTPUser = C.SMTP_USERNAME;
            string SMTPPass = Crypto.DecryptData(Crypto.KeyCrypto, C.SMTP_PASSWORD);
            int SMTPPort = ConvertUtility.ToInt32(C.SMTP_PORT);

            using (MailMessage emailMessage = new MailMessage())
            {
                emailMessage.From = new MailAddress(from, ConfigWeb.SiteName);
                if (!string.IsNullOrEmpty(to))
                    emailMessage.To.Add(new MailAddress(to, dt.Rows[0]["Name"].ToString()));
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


    protected string GetFormatMail()
    {
        string templatePath = HttpContext.Current.Server.MapPath(Globals.BaseUrl + "/assets/MailForm/forgot-pass.html");
        string mailFormat = String.Empty;
        if (!System.IO.File.Exists(templatePath))
            return String.Empty;
        else
            return System.IO.File.ReadAllText(templatePath);
    }

    protected string mail_body(DataTable dt, string token)
    {
        string templateMail = GetFormatMail();
        templateMail = templateMail.Replace("{website}", ConfigWeb.SiteName);
        templateMail = templateMail.Replace("{name}", dt.Rows[0]["Name"].ToString());
        templateMail = templateMail.Replace("{link}", C.ROOT_URL + "/admin/login/default.aspx?control=resetpassword&token=" + token);

        return templateMail;
    }



    #endregion

}