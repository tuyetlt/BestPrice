using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _tool_test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strReturn = string.Empty;
        DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "FlashSaleTimeDisplay", "");
        if (dt != null && dt.Rows.Count > 0)
        {
            //string dateTimeStringSql = dt.Rows[0][0].ToString();
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
            //DateTime dateTime = Utils.ConvertStringToDateTime(dateTimeStringSql);
            DateTime dateTime = ConvertUtility.ToDateTime(dt.Rows[0][0]);
            strReturn = dateTime.ToString("MMMM dd, yyyy HH:mm:ss", culture);

            Response.Write(strReturn);
        }
    }


}