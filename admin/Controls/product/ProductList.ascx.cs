using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using System.Net.Mail;
using MetaNET.DataHelper;
public partial class admin_Controls_ProductList : System.Web.UI.UserControl
{
    public DataTable dtFilter;
    public DataTable dtProducts;
    public int _totalRows, _totalPage;

    protected void Page_Load(object sender, EventArgs e)
    {
        //using (var dbx = SqlService.GetSqlService())
        //{
        //    string sqlQuery = string.Format("SELECT * FROM tblAttributes Where Type='Tags'");
        //    dtFilter = dbx.ExecuteSqlDataTable(sqlQuery);
        //}

        //using (var dbx = SqlService.GetSqlService())
        //{
        //    string sqlQuery = string.Format("SELECT * FROM tblProducts");
        //    dtProducts = dbx.ExecuteSqlDataTable(sqlQuery);
        //    _totalRows = dtProducts.Rows.Count;

        //    int Sodu = _totalRows % ConvertUtility.ToInt16(C.PAGING_ADMIN);
        //    _totalPage = _totalRows / ConvertUtility.ToInt16(C.PAGING_ADMIN);
        //    if (Sodu > 0)
        //        _totalPage += 1;
            
        //}
    }
}