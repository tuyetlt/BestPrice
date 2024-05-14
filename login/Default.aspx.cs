using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ebis.Utilities;


public partial class login_Default : System.Web.UI.Page
{
    #region Variable
    Control mainControl;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        CacheUtility.ClearAllCache();
        PlaceHolder.Controls.Clear();
        if (!IsPostBack)
        {
            var control = Request.QueryString["control"];
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

                    case "forgotpassword":
                        {
                            mainControl = LoadControl("~/login/controls/ForgotPassword.ascx");
                            break;
                        }
                    case "resetpassword":
                        {
                            mainControl = LoadControl("~/login/controls/SetPassword.ascx");
                            break;
                        }
                 
                    default:
                        {
                            mainControl = LoadControl("~/login/controls/Login.ascx");
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