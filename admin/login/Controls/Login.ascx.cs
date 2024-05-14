using System;
using System.Data;
using System.Web.Security;
using Ebis.Utilities;
using MetaNET.DataHelper;

public partial class admin_login_Controls_Login : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                bool remember = Ebis.Utilities.ConvertUtility.ToBoolean(Request["remember"]);
                string sqlQuery = string.Format("SELECT * FROM tblAdminUser WHERE Email=N'{0}' AND Password='{1}'", Utils.KillCharEmail(Request["email"]).ToString().Trim(), Crypto.EncryptData(Crypto.KeyCrypto, Request.Form["password"].ToString().Trim()));
                DataTable dtPermission = db.ExecuteSqlDataTable(sqlQuery);
                if (dtPermission != null && dtPermission.Rows.Count > 0)
                {
                    CookieUtility.SetValueToCookie("notice", "login_success");
                    FormsAuthentication.RedirectFromLoginPage(dtPermission.Rows[0]["ID"].ToString(), remember);
                    Utils.LoginSave(dtPermission.Rows[0]["ID"].ToString(), remember);
                    string retUrl = Ebis.Utilities.RequestHelper.GetString("retUrl", string.Empty);

                    if (string.IsNullOrEmpty(retUrl))
                    {
                        Response.Redirect(C.ROOT_URL + "/admin/");
                    }
                    else
                    {
                        Response.Redirect(retUrl);
                    }
                }
                else
                {
                    CookieUtility.SetValueToCookie("notice", "login_error");
                    Response.Redirect(Request.RawUrl);
                }
            }
        }
    }
}