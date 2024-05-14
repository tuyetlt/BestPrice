using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetaNET.DataHelper;

public partial class login_Controls_SetPassword : System.Web.UI.UserControl
{
    DataRow dr = null;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    public string table = "tblAdminUser";
    int ID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["token"] != null)
        {
            string token = Request.QueryString["token"].ToString().Trim();
            using (var db = SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("SELECT * FROM tblAdminUser Where Token=N'{0}'", token);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds != null && ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                    ID = ConvertUtility.ToInt32(ds.Rows[0]["ID"]);
                }
                else
                {
                    CookieUtility.SetValueToCookie("notice_error", "Link đặt lại mật khẩu đã hết hạn hoặc không chính xác");
                    Response.Redirect(C.ROOT_URL);
                }
            }
        }
        else
        {
            Response.Redirect(C.ROOT_URL);
            CookieUtility.SetValueToCookie("notice_error", "Link đặt lại mật khẩu đã hết hạn hoặc không chính xác");
        }

        if (!String.IsNullOrEmpty(Request.Form["done"]) && Request.Form["done"] == "1")
        {
            hashtable["Password"] = Crypto.EncryptData(Crypto.KeyCrypto, Request.Form["password1"].Trim());
            hashtable["Token"] = Utils.KillChars(Request.Form["token"]);
            CacheUtility.PurgeCacheItems(table);
            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = @"UPDATE [dbo].[tblAdminUser] SET [Password]=@Password,[Token]=@Token WHERE [ID]=@ID";
                db.AddParameter("@Password", System.Data.SqlDbType.NVarChar, hashtable["Password"].ToString());
                db.AddParameter("@Token", System.Data.SqlDbType.NVarChar, Utils.RandomStringNumberANDCharacter(20));
                db.AddParameter("@ID", System.Data.SqlDbType.Int, ID);
                db.ExecuteSql(sqlQuery);

                CookieUtility.SetValueToCookie("notice", "update_success");
                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), "Set password", 1, Request.RawUrl);
                CookieUtility.SetValueToCookie("notice_success", "Mật khẩu đã được đặt thành công, vui lòng đăng nhập để sử dụng hệ thống quản trị");

                Response.Redirect("/admin/login/");
            }
        }
    }
}