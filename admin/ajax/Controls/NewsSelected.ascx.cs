using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_ajax_Controls_NewsSelected : System.Web.UI.UserControl
{
    public string articleIDList = "";
    public string btn = string.Empty;
    public string txt = string.Empty;

    protected void ProcessParameter()
    {
        articleIDList = RequestHelper.GetString("articleIDList", "");
        btn = RequestHelper.GetString("btn", "");
        txt = RequestHelper.GetString("txt", "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessParameter();
    }

}