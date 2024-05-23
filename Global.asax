<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="Ebis.Utilities" %>

<script RunAt="server">

    //private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(Globals));

    void Application_BeginRequest(Object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        context.Items["RequestStartTime"] = DateTime.Now;

        //CheckRequest();

        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN");

        if (!Utils.IS_LOCAL)
        {
            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://www." + C.DOMAIN))
            {
                HttpContext.Current.Response.Status =
                    "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location",
                    Request.Url.ToString().ToLower().Replace(
                        "http://www." + C.DOMAIN,
                        "https://" + C.DOMAIN));
            }

            if (HttpContext.Current.Request.Url.ToString().ToLower().Contains("http://" + C.DOMAIN))
            {
                HttpContext.Current.Response.Status =
                    "301 Moved Permanently";
                HttpContext.Current.Response.AddHeader("Location",
                    Request.Url.ToString().ToLower().Replace(
                        "http://" + C.DOMAIN,
                        "https://" + C.DOMAIN));
            }
        }
    }

    void CheckRequest()
    {
        string ipAddress = HttpContext.Current.Request.UserHostAddress;

        int requestCountForIp = 0;
        bool error = false;
        bool check = RateLimiter.IsAllowed(ipAddress, HttpContext.Current.Request, out requestCountForIp, out error);
        if (!check)
        {
            Save(requestCountForIp, "[block] ");
            HttpContext.Current.Response.StatusCode = 429;
            HttpContext.Current.Response.End();
        }
        else
            Save(requestCountForIp, "");

    }

    private static void Save(int requestCountForIp, string block)
    {
        try
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LogSqlServer"].ConnectionString;
            using (var db = MetaNET.DataHelper.SqlService.GetSqlServiceFromConnectionString(connectionString))
            {
                string sqlQuery = @"INSERT INTO [tblLogs]([Name],[IP],[Url],[UserAgent],[CreatedDate]) VALUES (@Name,@IP,@Url,@UserAgent,@CreatedDate); SELECT SCOPE_IDENTITY();";

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Utils.GetDomainName);
                db.AddParameter("@IP", System.Data.SqlDbType.NVarChar, ConvertUtility.ToString(requestCountForIp) + " - " + Utils.GetIPAddress());
                db.AddParameter("@Url", System.Data.SqlDbType.NVarChar, Utils.GetUrlInfo);
                db.AddParameter("@UserAgent", System.Data.SqlDbType.NVarChar, block + HttpContext.Current.Request.Headers["User-Agent"]);
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



    void RegisterRoutes(RouteCollection routes)
    {
        routes.Ignore("{resource}.axd/{*pathInfo}");
        routes.Ignore("{resource}.ashx/{*pathInfo}");


        //Admin
        routes.MapPageRoute("admin_dashboard", "admin/{folder}/{control}", "~/admin/default.aspx");
        routes.MapPageRoute("ajax1", "ajax/", "~/ajax/");
        routes.MapPageRoute("websosanh", "websosanh/{page}/", "~/Websosanh/datafeed.aspx");
        routes.MapPageRoute("flashsale", "flash-sale/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "flashsale" } });

        routes.MapPageRoute("search", "tim-kiem/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "searchresult" } });
        routes.MapPageRoute("loi 404", "404/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "404" } });

        routes.MapPageRoute("checkout", "checkout/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "checkout" } });
        routes.MapPageRoute("product history", "san-pham-da-xem/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "widgetproducthistory" } });

        routes.MapPageRoute("gio hang", "gio-hang/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "shoppingcart" } });
        routes.MapPageRoute("hoan tat don hang", "thong-tin-don-hang/{token}/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "orderinfo" } });

        routes.MapPageRoute("content", "{caturl}.html", "~/Default.aspx", true, new RouteValueDictionary { { "m", "contentdetail" } });
        routes.MapPageRoute("sp", "{caturl}/{purl}.html", "~/Default.aspx", true, new RouteValueDictionary { { "m", "productdetails" } });

        routes.MapPageRoute("ajax_shoppingcart", "ajax/shoppingcart", "~/Default.aspx", true, new RouteValueDictionary { { "ajax", "shoppingcart" } });
        routes.MapPageRoute("ajax_search", "ajax/search", "~/Default.aspx", true, new RouteValueDictionary { { "ajax", "product_search" } });
        routes.MapPageRoute("ajax", "ajax/{control}/", "~/Default.aspx");


        routes.MapPageRoute("url chung", "{url}/", "~/Default.aspx", true, new RouteValueDictionary { { "m", "urlchung" } });



    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        DateTime startTime = (DateTime)context.Items["RequestStartTime"];
        TimeSpan requestDuration = DateTime.Now - startTime;
        string durationString = string.Format("{0:00}:{1:00}:{2:00}:{3:000}", requestDuration.Hours, requestDuration.Minutes, requestDuration.Seconds, requestDuration.Milliseconds);



    }

    void Application_Start(object sender, EventArgs e)
    {

        //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/Web.config")));
        RegisterRoutes(RouteTable.Routes);


    }

    void Application_End(object sender, EventArgs e)
    {
    }

    void Application_Error(object sender, EventArgs e)
    {
        //_logger.Info("Err: " + Utils.IPAddress);

    }

    void Session_Start(object sender, EventArgs e)
    {

    }

    void Session_End(object sender, EventArgs e)
    {

    }
</script>
