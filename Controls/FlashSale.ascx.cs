using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;

public partial class Controls_FlashSale : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Set_CSS_and_SEO();
    }

    protected void Set_CSS_and_SEO()
    {
        string Title = "Flash Sale";
        string MetaTitle = Title + " - " + ConfigWeb.SiteName;
        string MetaKeyword = Title + ", " + ConfigWeb.MetaKeyword;
        string MetaDescription = Title + ", " + ConfigWeb.MetaDescription;
        string url = C.ROOT_URL + Request.RawUrl;
        PageUtility.AddTitle(this.Page, MetaTitle);
        PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
        PageUtility.AddMetaTag(this.Page, "description", MetaDescription);
        PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, ConfigWeb.Image, ConfigWeb.SiteName, MetaDescription);
        PageUtility.SetIndex(this.Page);
        PageUtility.AddDefaultMetaTag(this.Page);
        PageInfo.ControlName = Title;
    }
}