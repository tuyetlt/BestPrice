using System;
using System.Data;
using System.Security.Policy;
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

            PageInfo.CategoryID = 0;
            PageInfo.LinkEdit = string.Empty;
            PageInfo.ControlName = string.Empty;

            var caturl = ConvertUtility.ToString(Page.RouteData.Values["caturl"]);
            var purl = ConvertUtility.ToString(Page.RouteData.Values["purl"]);
            var m = ConvertUtility.ToString(Page.RouteData.Values["m"]);
            var ajax = ConvertUtility.ToString(Page.RouteData.Values["ajax"]);
            var newsdetail = ConvertUtility.ToString(Page.RouteData.Values["newsdetail"]);
            var content = ConvertUtility.ToString(Page.RouteData.Values["content"]);

            try
            {
                if (m == "urlchung")
                {
                    string friendlyUrl = ConvertUtility.ToString(Page.RouteData.Values["url"]).Trim();
                    DataTable dtUrl = SqlHelper.SQLToDataTable("tblUrl", "Moduls,ContentID", string.Format("FriendlyUrl=N'{0}'", friendlyUrl), "ID DESC", 1, 1);
                    if (Utils.CheckExist_DataTable(dtUrl))
                    {
                        DataRow drUrl = dtUrl.Rows[0];
                        string moduls = ConvertUtility.ToString(drUrl["Moduls"]);
                        if (moduls == "product_detail")
                        {
                            DataTable dt = SqlHelper.SQLToDataTable("tblProducts", "", string.Format("ID=N'{0}'", drUrl["ContentID"]));
                            mainControl = LoadControl("~/Controls/ProductDetails.ascx");
                            Controls_ProductDetails contentDetailControl = mainControl as Controls_ProductDetails;
                            if (contentDetailControl != null)
                            {
                                contentDetailControl.dtRef = dt;
                            }
                        }
                        else if (moduls == "article_detail")
                        {
                            DataTable dt = SqlHelper.SQLToDataTable("tblArticle", "", string.Format("ID=N'{0}'", drUrl["ContentID"]));
                            mainControl = LoadControl("~/Controls/NewsDetail.ascx");
                            Controls_NewsDetail contentDetailControl = mainControl as Controls_NewsDetail;
                            if (contentDetailControl != null)
                            {
                                contentDetailControl.dtRef = dt;
                            }
                        }
                        else if (moduls == "category_article")
                        {
                            DataTable dt = SqlHelper.SQLToDataTable("tblCategories", "ID, Name,FriendlyUrl,Image_1,MetaTitle,MetaKeyword,MetaDescription,SchemaRatingCount,SchemaRatingValue", string.Format("ID=N'{0}'", drUrl["ContentID"]));
                            mainControl = LoadControl("~/Controls/NewsCategory.ascx");
                            Controls_NewsCategory contentDetailControl = mainControl as Controls_NewsCategory;
                            if (contentDetailControl != null)
                            {
                                contentDetailControl.dtRef = dt;
                            }
                        }
                        else if (moduls == "category_product")
                        {
                            DataTable dt = SqlHelper.SQLToDataTable("tblCategories", "ID,Name,FriendlyUrl,Image_1,Hide,ParentID,AttributesIDList,LongDescription,TagIDList,MetaTitle,MetaKeyword,MetaDescription,SchemaRatingCount,SchemaRatingValue,Moduls", string.Format("ID=N'{0}'", drUrl["ContentID"]));
                            mainControl = LoadControl("~/Controls/ProductCategory.ascx");
                            Controls_ProductCategory contentDetailControl = mainControl as Controls_ProductCategory;
                            if (contentDetailControl != null)
                            {
                                contentDetailControl.dtRef = dt;
                            }
                        }
                        else if (moduls == "category_content")
                        {
                            DataTable dt = SqlHelper.SQLToDataTable("tblCategories", "", string.Format("ID=N'{0}'", drUrl["ContentID"]));
                            mainControl = LoadControl("~/Controls/ContentDetail.ascx");
                            Controls_ContentDetail contentDetailControl = mainControl as Controls_ContentDetail;
                            if (contentDetailControl != null)
                            {
                                contentDetailControl.dtRef = dt;
                            }
                        }
                    }
                }
                else if (!Utils.IsNullOrEmpty(m))
                {
                    mainControl = LoadControl("~/controls/" + m + ".ascx");
                }
                else
                {
                    mainControl = LoadControl("~/controls/Home.ascx");
                    PageInfo.CategoryID = 0;
                    string url = C.ROOT_URL;
                    PageUtility.AddTitle(this.Page, ConfigWeb.MetaTitle);
                    PageUtility.AddMetaTag(this.Page, "keywords", ConfigWeb.MetaKeyword);
                    PageUtility.AddMetaTag(this.Page, "description", ConfigWeb.MetaDescription);
                    PageUtility.OpenGraph(this.Page, ConfigWeb.MetaTitle, "website", url, C.ROOT_URL + ConfigWeb.Image, ConfigWeb.SiteName, ConfigWeb.MetaDescription);
                    PageUtility.AddCanonicalLink(this.Page, url);
                    PageUtility.SetIndex(this.Page);
                    PageUtility.AddDefaultMetaTag(this.Page);
                }
            }
            catch (Exception ex)
            {
                mainControl = LoadControl("~/controls/Home.ascx");
                Response.Write(ex.Message);

            }
            try {
                PlaceHolder.Controls.Add(mainControl);
            } catch { }
            

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