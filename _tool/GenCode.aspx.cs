using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _tool_GenCode : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;
    public DataTable dtTable = new DataTable();
    public DataTable dtColumn = new DataTable();
    public string tableName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        tableName = RequestHelper.GetString("tableName", "");
        if (!string.IsNullOrEmpty(tableName))
        {
            using (var dbColumn = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQueryColumn = string.Format("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'{0}'", tableName);
                dtColumn = dbColumn.ExecuteSqlDataTable(sqlQueryColumn);
            }
        }


        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            dtTable = db.ExecuteSqlDataTable(sqlQuery);
        }




    }
}