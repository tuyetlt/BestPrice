using System;
using System.Collections.Generic;
using Ebis.Utilities;
using MetaNET.DataHelper;
using System.Data;

public partial class admin_chart_Controls_Article : System.Web.UI.UserControl
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
            string sqlQuery = string.Format("SELECT convert(varchar(10), CreatedDate, 120) as CreatedDate, SUM(1) as 'SoDon' FROM tblArticle Where CreatedDate >= '{0}' AND CreatedDate < DATEADD(day,1,'{1}') Group by convert(varchar(10), CreatedDate, 120) order by CreatedDate ASC", FromDate, ToDate);
            dataTable = dbx.ExecuteSqlDataTable(sqlQuery);
        }


        List<BaiViet> topDaiLyList = new List<BaiViet>();

        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                if (dr != dataTable.Rows[0])
                {
                    jsonName += ",";
                    //jsonTien += ",";
                    jsonDon += ",";
                }

                jsonName += "'" + ConvertUtility.ToString(dr["CreatedDate"]) + "'";
               
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

public class BaiViet
{
    public string Ngay { get; set; }
    public int SoDon { get; set; }
}