using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Input;

public partial class Controls_FlashSale : System.Web.UI.UserControl
{
    public DataTable dtProduct;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string sort = "case ID ";
            string where = "ID in(";
            string currentCookie = "";

            if (CookieUtility.GetValueFromCookie("product_viewed") != null)
                currentCookie = CookieUtility.GetValueFromCookie("product_viewed");
            List<string> stringCookieList = Utils.ConvertStringToList(currentCookie);
            if (stringCookieList.Count > 0)
            {
                int count = 0;
                foreach (string str in stringCookieList)
                {
                    if (ConvertUtility.ToInt32(str) > 0)
                    {
                        sort += "when " + str + " then " + count + " ";
                        if (count > 0)
                            where += ",";
                        where += str;
                        count++;
                    }
                }
                sort += "end DESC";
                where += ")";
            }

            dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", where, sort, 1, 100);
        }
        catch
        {

        }

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