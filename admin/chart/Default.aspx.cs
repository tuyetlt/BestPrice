using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_chart_Default : System.Web.UI.Page
{
    #region Variable
    Control mainControl;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        PlaceHolder.Controls.Clear();
        if (!IsPostBack)
        {
            var control = RequestHelper.GetString("control", "");
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
                    case "order":
                        {
                            mainControl = LoadControl("~/admin/chart/Controls/Order.ascx");
                            break;
                        }

                    case "article":
                        {
                            mainControl = LoadControl("~/admin/chart/Controls/Article.ascx");
                            break;
                        }

                    case "pricehistory":
                        {
                            mainControl = LoadControl("~/admin/chart/Controls/PriceHistory.ascx");
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