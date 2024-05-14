using System;
using System.Collections.Generic;
using Ebis.Utilities;
using MetaNET.DataHelper;
using System.Data;
public partial class admin_chart_Controls_Order : System.Web.UI.UserControl
{
    public DataTable dataTable;
    public string jsonName;
    public string jsonDon;
    public string jsonTien;

    public string jsonKhung;
    public string jsonKhungValue;
    public string jsonChatLieu;

    protected void Page_Load(object sender, EventArgs e)
    {
        string FromDate = RequestHelper.GetString("startDate", string.Empty);
        string ToDate = RequestHelper.GetString("endDate", string.Empty);

        using (var dbx = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT convert(varchar(10), OrderDate, 120) as OrderDate, SUM(1) as 'SoDon', sum(PriceFinal) as 'Tien' FROM tblOrder Where OrderDate >= '{0}' AND OrderDate < DATEADD(day,1,'{1}') Group by convert(varchar(10), OrderDate, 120) order by OrderDate ASC", FromDate, ToDate);
            dataTable = dbx.ExecuteSqlDataTable(sqlQuery);
        }


        List<DonHang> topDaiLyList = new List<DonHang>();

        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                if (dr != dataTable.Rows[0])
                {
                    jsonName += ",";
                    jsonTien += ",";
                    jsonDon += ",";
                }

                jsonName += "'" + ConvertUtility.ToString(dr["OrderDate"]) + "'";
                jsonDon += ConvertUtility.ToString(dr["SoDon"]);

                int tien = 0;
                try
                {
                    tien = ConvertUtility.ToInt32(dr["Tien"]) / 1000;
                }
                catch { }


                jsonTien += string.Format("{0}", tien);
            }
        }


    }


}

public class DonHang
{
    public string Ngay { get; set; }
    public int SoDon { get; set; }

}