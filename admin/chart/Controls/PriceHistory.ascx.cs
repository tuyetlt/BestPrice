using System;
using System.Collections.Generic;
using Ebis.Utilities;
using MetaNET.DataHelper;
using System.Data;

public partial class admin_chart_Controls_PriceHistory : System.Web.UI.UserControl
{
    public DataTable dataTable;
    public string jsonName;
    public string jsonTien;


    protected void Page_Load(object sender, EventArgs e)
    {
        string PID = RequestHelper.GetString("pid", string.Empty);

        using (var dbx = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT CreatedDate, Price FROM [tblPriceHistory] Where ProductID = '{0}' order by ID ASC", PID);
            dataTable = dbx.ExecuteSqlDataTable(sqlQuery);
        }


        List<PriceHistory> topDaiLyList = new List<PriceHistory>();

        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                if (dr != dataTable.Rows[0])
                {
                    jsonName += ",";
                    jsonTien += ",";
                }

                jsonName += "'" + ConvertUtility.ToString(dr["CreatedDate"]) + "'";

                int tien = 0;
                try
                {
                    tien = ConvertUtility.ToInt32(dr["Price"]) / 1000;
                }
                catch { }


                jsonTien += string.Format("{0}", tien);
            }
        }


    }

}

public class PriceHistory
{
    public string Ngay { get; set; }
    public int SoDon { get; set; }

}