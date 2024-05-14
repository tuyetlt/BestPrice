using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class admin_AdminControl_Controls_TopPanel : System.Web.UI.UserControl
{
    public DataTable dt;
    public DataRow dr;
    public string avatar = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string currentUrl = Request.RawUrl;
            if (Page.User.Identity.IsAuthenticated)
            {
                string click_action = Request.Form["done_login"];
                if (!String.IsNullOrEmpty(click_action) && click_action == "logout")
                {
                    FormsAuthentication.SignOut();
                    string CurrentUrl = Request.RawUrl;
                    string redirect = Globals.BaseUrl + "admin/login/?retUrl=" + CurrentUrl;
                    Response.Redirect(redirect);
                }
                else if (!String.IsNullOrEmpty(click_action) && click_action == "cache")
                {
                    string url = Request.RawUrl;
                    CacheUtility.ClearAllCache();
                    CookieUtility.SetValueToCookie("notice_success", "Xóa Cache (bộ nhớ đệm) thành công!");
                    Response.Redirect(url);
                }


                using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("Select u.ID, u.[Name], u.Gallery, p.Name as 'PermissionName', p.[Role] from tblAdminUser as u inner join tblAdminPermission as p on u.Permission = p.ID AND u.ID={0}", Page.User.Identity.Name);
                    dt = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (dt.Rows.Count > 0)
                    {
                        dr = dt.Rows[0];
                        List<GalleryImage> galleryList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<GalleryImage>>(dr["Gallery"].ToString());
                        
                        if (galleryList != null && galleryList.Count > 0)
                        {
                            avatar = galleryList[0].Path;
                        }
                        else
                        {
                            avatar = C.NO_IMG_PATH;
                        }

                        UpdateLogs(dr["Name"].ToString());
                    }
                    else
                    {   
                        Response.Redirect("/admin/login/?retUrl=" + currentUrl);
                    }
                }
            }
            else
            {
                Response.Redirect("/admin/login/?retUrl=" + currentUrl);
            }
        }
    }


    protected void UpdateLogs(string AdminName)
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LogSqlServer"].ConnectionString;
            using (var db = MetaNET.DataHelper.SqlService.GetSqlServiceFromConnectionString(connectionString))
            {
                string sqlQuery = @"INSERT INTO [tblLogs]([Name],[IP],[Url],[AdminName],[UserAgent],[CreatedDate]) VALUES (@Name,@IP,@Url,@AdminName,@UserAgent,@CreatedDate); SELECT SCOPE_IDENTITY();";

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Utils.GetDomainName);
                db.AddParameter("@IP", System.Data.SqlDbType.NVarChar, Utils.GetIPAddress());
                db.AddParameter("@Url", System.Data.SqlDbType.NVarChar, Utils.GetUrlInfo);
                db.AddParameter("@AdminName", System.Data.SqlDbType.NVarChar, AdminName);
                db.AddParameter("@UserAgent", System.Data.SqlDbType.NVarChar, Request.Headers["User-Agent"]);
                db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);

                object result = db.ExecuteSqlScalar<int>(sqlQuery, 0);
                int insertedId = Convert.ToInt32(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

}