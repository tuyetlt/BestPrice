using System;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    string utm_source = "", utm_medium = "", utm_campaign = "", utm_term = "", utm_content = "", utm_adgroup = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        PageInfo.CategoryID = 0;
        PageInfo.ControlName = string.Empty;
        PageInfo.LinkEdit = string.Empty;

        SEO.meta_title = ConfigWeb.MetaTitle;
        SEO.meta_keyword = ConfigWeb.MetaKeyword;
        SEO.meta_description = ConfigWeb.MetaDescription;
        SEO.url_current = C.ROOT_URL;
        SEO.canonical = C.MAIN_URL;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<meta property='og:title' content='" + SEO.meta_title + "' />");
        sb.AppendLine("<meta property='og:type' content='website'/>");
        sb.AppendLine("<meta property='og:url' content='" + SEO.url_current + "'/>");
        sb.AppendLine("<meta property='og:image' content='" + ConfigWeb.Image + "'/>");
        sb.AppendLine("<meta property='og:site_name' content='" + SEO.url_current + "'/>");
        sb.AppendLine("<meta property='og:description' content='" + SEO.meta_description + "'/>");
        SEO.content_share_facebook = ConvertUtility.ToString(sb);

        if (!IsPostBack)
        {
            utm_source = RequestHelper.GetString("utm_source", string.Empty);
            utm_medium = RequestHelper.GetString("utm_medium", string.Empty);
            utm_campaign = RequestHelper.GetString("utm_campaign", string.Empty);
            utm_term = RequestHelper.GetString("utm_term", string.Empty);
            utm_content = RequestHelper.GetString("utm_content", string.Empty);
            utm_adgroup = RequestHelper.GetString("utm_adgroup", string.Empty);


            CookieUtility.SetValueToCookie("utm_source", utm_source);
            CookieUtility.SetValueToCookie("utm_medium", utm_medium);
            CookieUtility.SetValueToCookie("utm_campaign", utm_campaign);
            CookieUtility.SetValueToCookie("utm_term", utm_term);
            CookieUtility.SetValueToCookie("utm_content", utm_content);
            CookieUtility.SetValueToCookie("utm_adgroup", utm_adgroup);


            //Response.Write(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_source")) + "<br />");
            //Response.Write(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_medium")) + "<br />");
            //Response.Write(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_campaign")) + "<br />");
            //Response.Write(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_adgroup")) + "<br />");
            //Response.Write(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_term")) + "<br />");
            //Response.Write(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_content")) + "<br />");

        }

        if (string.IsNullOrEmpty(CookieUtility.GetValueFromCookie("StartOnWeb")))
        {
            CookieUtility.SetValueToCookie("StartOnWeb", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
        }
    }
}
