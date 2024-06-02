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
    }
}