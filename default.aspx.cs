using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class _default : System.Web.UI.Page
{
    #region Variable
    Control mainControl;
    DateTime time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //string ipAddress = HttpContext.Current.Request.UserHostAddress;

        //int requestCountForIp = 0;
        //if (!RateLimiter.IsAllowed(ipAddress, HttpContext.Current.Request, out requestCountForIp))
        //{
        //    HttpContext.Current.Response.StatusCode = 429;
        //    HttpContext.Current.Response.End();
        //}

        //Response.Write(requestCountForIp);


        PlaceHolder.Controls.Clear();
        if (!IsPostBack)
        {
            var controler = Request.QueryString["id"];
            try
            {
                var caturl = ConvertUtility.ToString(Page.RouteData.Values["caturl"]);
                var purl = ConvertUtility.ToString(Page.RouteData.Values["purl"]);
                var m = ConvertUtility.ToString(Page.RouteData.Values["m"]);
                var ajax = ConvertUtility.ToString(Page.RouteData.Values["ajax"]);
                var newsdetail = ConvertUtility.ToString(Page.RouteData.Values["newsdetail"]);
                var content = ConvertUtility.ToString(Page.RouteData.Values["content"]);

                if (!Utils.IsNullOrEmpty(m) && m == "contentdetail")
                {
                    mainControl = LoadControl("~/controls/ContentDetail.ascx");
                }
                else if (!Utils.IsNullOrEmpty(m) && m == "productcategory")
                {
                    mainControl = LoadControl("~/controls/ProductCategory.ascx");
                    PageInfo.CurrentControl = ControlCurrent.ProductCategory.ToString();
                }
                else if (!Utils.IsNullOrEmpty(m) && m == "productdetails")
                {
                    mainControl = LoadControl("~/controls/ProductDetails.ascx");
                    PageInfo.CurrentControl = ControlCurrent.ProductDetails.ToString();
                }
                else if (!Utils.IsNullOrEmpty(m))
                    mainControl = LoadControl("~/controls/" + m + ".ascx");
                else if (!Utils.IsNullOrEmpty(ajax))
                    mainControl = LoadControl("~/ajax/" + ajax + ".ascx");
                else
                {
                    mainControl = LoadControl("~/controls/Home.ascx");
                }

            }
            catch (Exception ex)
            {
                mainControl = LoadControl("~/controls/Home.ascx");
                Response.Write(ex.Message);

            }
            PlaceHolder.Controls.Add(mainControl);

            //UpdateLogs();
        }
    }


    //protected void UpdateLogs()
    //{
    //    try
    //    {
    //        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LogSqlServer"].ConnectionString;
    //        using (var db = MetaNET.DataHelper.SqlService.GetSqlServiceFromConnectionString(connectionString))
    //        {
    //            string sqlQuery = @"INSERT INTO [tblLogs]([ID],[Name],[IP],[Url],[UserAgent]) VALUES (@ID,@Name,@IP,@Url,@UserAgent)";

    //            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, "");
    //            db.AddParameter("@IP", System.Data.SqlDbType.NVarChar, Utils.GetIPAddress());
    //            db.AddParameter("@Url", System.Data.SqlDbType.NVarChar, Utils.GetUrlInfo);
    //            db.AddParameter("@UserAgent", System.Data.SqlDbType.NVarChar, "");

    //            db.ExecuteSql(sqlQuery);
    //        }
    //    }
    //    catch
    //    {

    //    }
    //}

    protected void UpdateLogs()
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LogSqlServer"].ConnectionString;
            using (var db = MetaNET.DataHelper.SqlService.GetSqlServiceFromConnectionString(connectionString))
            {
                string sqlQuery = @"INSERT INTO [tblLogs]([Name],[IP],[Url],[UserAgent],[CreatedDate]) VALUES (@Name,@IP,@Url,@UserAgent,@CreatedDate); SELECT SCOPE_IDENTITY();";

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Utils.GetDomainName);
                db.AddParameter("@IP", System.Data.SqlDbType.NVarChar, Utils.GetIPAddress());
                db.AddParameter("@Url", System.Data.SqlDbType.NVarChar, Utils.GetUrlInfo);
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