using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ebis.Utilities;

public partial class admin_ajax_Controls_Paging : System.Web.UI.UserControl
{
    public int totalRecord, totalPage, pageIndex; 
    protected void Page_Load(object sender, EventArgs e)
    {
        totalRecord = RequestHelper.GetInt("totalRecord", 0);
        pageIndex = RequestHelper.GetInt("pageIndex", 1);
        
        int Sodu = totalRecord % ConvertUtility.ToInt16(C.PAGING_ADMIN);
        totalPage = totalRecord / ConvertUtility.ToInt16(C.PAGING_ADMIN);
        if (Sodu > 0)
            totalPage += 1;


    }
}