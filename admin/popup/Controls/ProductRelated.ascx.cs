using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class admin_popup_Controls_ProductRelated : System.Web.UI.UserControl
{
    public DataTable dtProducts;
    public string currentUrl, click_action, key, category, textbox, button, filter = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUrl = Request.RawUrl;
        click_action = Request.Form["done_search"];
        key = Request.Form["keyword"];
        category = Request.Form["category"];
        textbox = Request.Form["textbox"];
        button = Request.Form["button"];

        if (string.IsNullOrEmpty(click_action))
            click_action = RequestHelper.GetString("done_search", "");
        if (string.IsNullOrEmpty(key))
            key = RequestHelper.GetString("keyword", "");
        if (string.IsNullOrEmpty(category))
            category = RequestHelper.GetString("category", "");
        if (string.IsNullOrEmpty(textbox))
            textbox = RequestHelper.GetString("textbox", "");
        if (string.IsNullOrEmpty(button))
            button = RequestHelper.GetString("button", "");

        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(click_action) && click_action == "done_search")
            {

                Response.Redirect(string.Format("/admin/popup/popup.aspx?ctrl=productrelated&textbox={0}&button={1}&category={2}&keyword={3}", textbox, button, category, key));



                //dtProducts = Utils.SearchProduct(key);
            }

            filter = string.Format("");

            dtProducts = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,Price,Price1, Gallery,FriendlyUrlCategory,FriendlyUrl", string.Format("(Name like N'%{0}%' OR NameUnsign like N'%{0}%') AND {1}", key, Utils.CreateFilterHide), "EditedDate DESC", 1, 100);


            //dtProducts = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,FriendlyUrl, Name, Gallery", filter, "EditedDate DESC", 1, 100);

        }
    }

}