using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_ajax : System.Web.UI.Page
{
    #region Variable
    Control mainControl;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PlaceHolder.Controls.Clear();
        if (!IsPostBack)
        {
            var control = RequestHelper.GetString("control",""); 
            if (string.IsNullOrEmpty(control))
            {
                control = ConvertUtility.ToString(Page.RouteData.Values["control"]).ToLower();
            }
            else
            {
                control = control.ToLower();
            }
            try
            {
                switch (control)
                {
                    case "search":
                        {
                            mainControl = LoadControl("~/ajax/Controls/SearchSuggestion.ascx");
                            break;
                        }
                    case "dynamic":
                        {
                            mainControl = LoadControl("~/ajax/Controls/Dynamic.ascx");
                            break;
                        }
                    case "categoryload":
                        {
                            mainControl = LoadControl("~/ajax/Controls/CategoryLoad.ascx");
                            break;
                        }
                    case "homeproduct":
                        {
                            mainControl = LoadControl("~/ajax/Controls/HomeProduct.ascx");
                            break;
                        }
                    case "attributeproduct":
                        {
                            mainControl = LoadControl("~/ajax/Controls/AttributeProduct.ascx");
                            break;
                        }
                    case "cronjob":
                        {
                            mainControl = LoadControl("~/ajax/Controls/CronJob.ascx");
                            break;
                        }
                    case "utm":
                        {
                            mainControl = LoadControl("~/ajax/Controls/UTM.ascx");
                            break;
                        }
                    case "coupon":
                        {
                            mainControl = LoadControl("~/ajax/Controls/Coupon.ascx");
                            break;
                        }
                    case "paging":
                        {
                            mainControl = LoadControl("~/ajax/Controls/Paging.ascx");
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