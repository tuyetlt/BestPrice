using System;
using System.Web.UI;

public partial class assets_ckfinder_ckfinder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.Page.User.Identity.IsAuthenticated)
        {
            Response.Clear();
            Response.Write("access denied");
            Response.End();
        }
    }
}