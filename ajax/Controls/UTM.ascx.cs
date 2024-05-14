using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
using System.Globalization;

public partial class ajax_Controls_UTM : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CacheUtility.PurgeCacheItems("tblConversion");

        int countByIP = 1;

        //Check đã chuyển đổi trong ngày chưa
        DataTable dtCheck = SqlUtil.SQLToDataTable("tblConversion", "ID", string.Format("(CreatedDate >= CONVERT(date, GETDATE()) AND CreatedDate < DATEADD(day, 1, CONVERT(date, GETDATE()))) AND IPAddress=N'{0}'", ConvertUtility.ToString(Utils.GetIPAddress())));
        if (Utils.CheckExist_DataTable(dtCheck))
        {
            countByIP = dtCheck.Rows.Count + 1;
        }


        bool IsUpdate = false;
        System.Collections.Hashtable hashtable = new System.Collections.Hashtable();

        hashtable["ID"] = Utils.KillChars(Request.Form["ID"]);
        hashtable["Name"] = Utils.KillChars(Request.Form["Name"]);
        hashtable["utm_id"] = Utils.KillChars(Request.Form["utm_id"]);
        hashtable["utm_source"] = Utils.KillChars(Request.Form["utm_source"]);
        hashtable["utm_medium"] = Utils.KillChars(Request.Form["utm_medium"]);
        hashtable["utm_campaign"] = Utils.KillChars(Request.Form["utm_campaign"]);
        hashtable["utm_adgroup"] = Utils.KillChars(Request.Form["utm_adgroup"]);
        hashtable["utm_term"] = Utils.KillChars(Request.Form["utm_term"]);
        hashtable["utm_content"] = Utils.KillChars(Request.Form["utm_content"]);
        hashtable["CountByIP"] = countByIP;
        hashtable["IPAddress"] = ConvertUtility.ToString(Utils.GetIPAddress());
        hashtable["IsAdmin"] = Utils.KillChars(Request.Form["IsAdmin"]);
      
        hashtable["Event"] = Utils.KillChars(Request.Form["ConversionType"]);
        hashtable["Param"] = Utils.KillChars(Request.Form["phoneNumber"]);
        hashtable["Class"] = Utils.KillChars(Request.Form["Class"]);
        hashtable["Url"] = Utils.KillChars(Request.Form["Url"]);
        hashtable["DeviceInfo"] = Request.UserAgent;
        hashtable["StartOnWeb"] = CookieUtility.GetValueFromCookie("StartOnWeb");
        hashtable["CreatedDate"] = DateTime.Now;
        hashtable["EditedDate"] = DateTime.Now;
        hashtable["CreatedBy"] = Page.User.Identity.Name;
        hashtable["EditedBy"] = Page.User.Identity.Name;

        string StartOnWeb = CookieUtility.GetValueFromCookie("StartOnWeb");
        DateTime startDate = DateTime.ParseExact(StartOnWeb, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        DateTime endDate = DateTime.Now;
        TimeSpan span = endDate - startDate;
        int second = (int)span.TotalSeconds;


        hashtable["TimeActive"] = startDate;
        hashtable["TimeActiveBySecond"] = second;

        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Empty;
            if (IsUpdate)
                sqlQuery = @"UPDATE[dbo].[tblConversion] SET [ID]=@ID,[Name]=@Name,[utm_id]=@utm_id,[utm_source]=@utm_source,[utm_medium]=@utm_medium,[utm_campaign]=@utm_campaign,[utm_adgroup]=@utm_adgroup,[utm_term]=@utm_term,[utm_content]=@utm_content,[CountByIP]=@CountByIP,[IPAddress]=@IPAddress,[IsAdmin]=@IsAdmin,[TimeActive]=@TimeActive,[TimeActiveBySecond]=@TimeActiveBySecond,[Event]=@Event,[Param]=@Param,[Class]=@Class,[DeviceInfo]=@DeviceInfo,[Url]=@Url,[StartOnWeb]=@StartOnWeb,[CreatedDate]=@CreatedDate,[EditedDate]=@EditedDate,[CreatedBy]=@CreatedBy,[EditedBy]=@EditedBy WHERE [ID] = @ID";
            else
                sqlQuery = @"INSERT INTO [dbo].[tblConversion]([Name],[utm_id],[utm_source],[utm_medium],[utm_campaign],[utm_adgroup],[utm_term],[utm_content],[CountByIP],[IPAddress],[IsAdmin],[TimeActive],[TimeActiveBySecond],[Event],[Param],[Class],[Url],[DeviceInfo],[StartOnWeb],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@utm_id,@utm_source,@utm_medium,@utm_campaign,@utm_adgroup,@utm_term,@utm_content,@CountByIP,@IPAddress,@IsAdmin,@TimeActive,@TimeActiveBySecond,@Event,@Param,@Class,@Url,@DeviceInfo,@StartOnWeb,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";

            db.AddParameter("@ID", System.Data.SqlDbType.Int, hashtable["ID"].ToString());
            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
            db.AddParameter("@utm_id", System.Data.SqlDbType.NVarChar, hashtable["utm_id"].ToString());
            db.AddParameter("@utm_source", System.Data.SqlDbType.NVarChar, hashtable["utm_source"].ToString());
            db.AddParameter("@utm_medium", System.Data.SqlDbType.NVarChar, hashtable["utm_medium"].ToString());
            db.AddParameter("@utm_campaign", System.Data.SqlDbType.NVarChar, hashtable["utm_campaign"].ToString());
            db.AddParameter("@utm_adgroup", System.Data.SqlDbType.NVarChar, hashtable["utm_adgroup"].ToString());
            db.AddParameter("@utm_term", System.Data.SqlDbType.NVarChar, hashtable["utm_term"].ToString());
            db.AddParameter("@utm_content", System.Data.SqlDbType.NVarChar, hashtable["utm_content"].ToString());
            db.AddParameter("@CountByIP", System.Data.SqlDbType.Int, hashtable["CountByIP"].ToString());
            db.AddParameter("@IPAddress", System.Data.SqlDbType.NVarChar, hashtable["IPAddress"].ToString());
            db.AddParameter("@IsAdmin", System.Data.SqlDbType.Bit, hashtable["IsAdmin"].ToString());
            db.AddParameter("@TimeActive", System.Data.SqlDbType.DateTime, hashtable["TimeActive"].ToString());
            db.AddParameter("@TimeActiveBySecond", System.Data.SqlDbType.Int, hashtable["TimeActiveBySecond"].ToString());
            db.AddParameter("@Event", System.Data.SqlDbType.NVarChar, hashtable["Event"].ToString());
            db.AddParameter("@Param", System.Data.SqlDbType.NVarChar, hashtable["Param"].ToString());
            db.AddParameter("@Class", System.Data.SqlDbType.NVarChar, hashtable["Class"].ToString());
            db.AddParameter("@DeviceInfo", System.Data.SqlDbType.NVarChar, hashtable["DeviceInfo"].ToString());
            db.AddParameter("@Url", System.Data.SqlDbType.NVarChar, hashtable["Url"].ToString());
            db.AddParameter("@StartOnWeb", System.Data.SqlDbType.DateTime, hashtable["StartOnWeb"].ToString());
            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, hashtable["CreatedDate"].ToString());
            db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, hashtable["EditedDate"].ToString());
            db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, hashtable["CreatedBy"].ToString());
            db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, hashtable["EditedBy"].ToString());

            db.ExecuteSql(sqlQuery);
        }
    }
}