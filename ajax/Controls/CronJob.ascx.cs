using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_Controls_CronJob : System.Web.UI.UserControl
{
    public int TotalProducts = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string web = RequestHelper.GetString("web", "bestprice");

        if (web == "sitemap")
            GenSitemap.SitemapUpdate();
        else
            GenSitemap.GenGoogleShopping(HttpContext.Current, web);

        DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID", "");
        TotalProducts = dt.Rows.Count;

    }
}