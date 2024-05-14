using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ebis.Utilities;

public partial class admin_DonHang_ajax_ajax : System.Web.UI.Page
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

                    case "select2":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/Select2Control.ascx");
                            break;
                        }
                    case "objlist":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/ObjectListAjax.ascx");
                            break;
                        }
                    case "dynamic":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/Dynamic.ascx");
                            break;
                        }
                    case "paging":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/Paging.ascx");
                            break;
                        }
                    case "newsrelated":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/NewsRelated.ascx");
                            break;
                        }
                    case "newsselected":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/NewsSelected.ascx");
                            break;
                        }
                    case "productselected":
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/ProductSelected.ascx");
                            break;
                        }
                    default:
                        {
                            mainControl = LoadControl("~/admin/ajax/controls/Select2Control.ascx");
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