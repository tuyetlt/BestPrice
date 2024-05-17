using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Checkout : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SetSEO()
    {
        PageInfo.CategoryID = 0;
        string Title = "Kết quả tìm kiếm: " + keyword;
        string MetaTitle = Title + " - " + ConfigWeb.SiteName;
        string MetaKeyword = Title + ", " + ConfigWeb.MetaKeyword;
        string MetaDescription = Title + ", " + ConfigWeb.MetaDescription;
        string url = C.ROOT_URL + Request.RawUrl;
        PageUtility.AddTitle(this.Page, MetaTitle);
        PageUtility.AddMetaTag(this.Page, "keywords", MetaKeyword);
        PageUtility.AddMetaTag(this.Page, "description", "Trang không tìm thấy. Vui lòng kiểm tra lại URL hoặc quay lại trang chính.");
        PageUtility.OpenGraph(this.Page, MetaTitle, "website", url, C.ROOT_URL + ConfigWeb.Image, ConfigWeb.SiteName, MetaDescription);
        PageUtility.SetIndex(this.Page);
        PageUtility.AddDefaultMetaTag(this.Page);
    }
}