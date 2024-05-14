using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_popup_popup : System.Web.UI.Page
{
    #region Variable
    Control mainControl;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PlaceHolder.Controls.Clear();
        if (!IsPostBack)
        {
            var control = Request.QueryString["ctrl"];
            if (string.IsNullOrEmpty(control))
            {
                control = ConvertUtility.ToString(Page.RouteData.Values["ctrl"]).ToLower();
            }
            else
            {
                control = control.ToLower();
            }


            try
            {
                switch (control)
                {

                    case "newsrelated":
                        {
                            mainControl = LoadControl("~/admin/popup/controls/ArticleRelated.ascx");
                            break;
                        }
                    case "productrelated":
                        {
                            mainControl = LoadControl("~/admin/popup/controls/ProductRelated.ascx");
                            break;
                        }
                    default:
                        {
                            mainControl = LoadControl("~/admin/popup/controls/ArticleRelated.ascx");
                            break;
                        }
                }
                PlaceHolder.Controls.Add(mainControl);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
    
}