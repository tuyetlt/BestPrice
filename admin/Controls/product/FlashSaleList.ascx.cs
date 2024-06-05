using MetaNET.DataHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Controls_product_FlashSaleList : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    int ID = 1;
    public string click_action, controlName, table = "tblConfigs", FlashSaleHeader=C.NO_IMG_PATH, FlashSaleFrame1 = C.NO_IMG_PATH, FlashSaleFrame2 = C.NO_IMG_PATH, FlashSaleFrame3 = C.NO_IMG_PATH;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            UpdateDatabase();
        }
    }
    protected void BindData()
    {
        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT Top 1 * FROM tblConfigs");
            var ds = db.ExecuteSqlDataTable(sqlQuery);
            if (ds.Rows.Count > 0)
            {
                dr = ds.Rows[0];
                if (!string.IsNullOrEmpty(dr["FlashSaleHeader"].ToString()))
                    FlashSaleHeader = ds.Rows[0]["FlashSaleHeader"].ToString();
                if (!string.IsNullOrEmpty(dr["FlashSaleFrame1"].ToString()))
                    FlashSaleFrame1 = ds.Rows[0]["FlashSaleFrame1"].ToString();
                if (!string.IsNullOrEmpty(dr["FlashSaleFrame2"].ToString()))
                    FlashSaleFrame2 = ds.Rows[0]["FlashSaleFrame2"].ToString();
                if (!string.IsNullOrEmpty(dr["FlashSaleFrame3"].ToString()))
                    FlashSaleFrame3 = ds.Rows[0]["FlashSaleFrame3"].ToString();
            }
        }
    }
    protected void UpdateDatabase()
    {
        click_action = Request.Form["done"];
        if (!String.IsNullOrEmpty(click_action) && (click_action == "save"))
        {
            hashtable["FlashSaleTimeDisplay"] = Utils.DateTimeString_To_DateTimeSql(Request.Form["FlashSaleTimeDisplay"]);
            hashtable["FlashSaleTimeReal"] = Utils.DateTimeString_To_DateTimeSql(Request.Form["FlashSaleTimeReal"]);
            hashtable["AutoRenewal"] = 0;
            hashtable["FlashSaleHeader"] = Utils.KillChars(Request.Form["FlashSaleHeader"]);
            hashtable["FlashSaleBackgroundColor"] = Utils.KillChars(Request.Form["FlashSaleBackgroundColor"]);
            hashtable["FlashSaleFrame1"] = Utils.KillChars(Request.Form["FlashSaleFrame1"]);
            hashtable["FlashSaleFrame2"] = Utils.KillChars(Request.Form["FlashSaleFrame2"]);
            hashtable["FlashSaleFrame3"] = Utils.KillChars(Request.Form["FlashSaleFrame3"]);

            if (!string.IsNullOrEmpty(Request.Form["AutoRenewal"]) && Request.Form["AutoRenewal"] == "on")
                hashtable["AutoRenewal"] = 1;

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = @"UPDATE [dbo].[tblConfigs] SET [FlashSaleTimeDisplay]=@FlashSaleTimeDisplay,[FlashSaleTimeReal]=@FlashSaleTimeReal,[AutoRenewal]=@AutoRenewal,[FlashSaleHeader]=@FlashSaleHeader,[FlashSaleBackgroundColor]=@FlashSaleBackgroundColor,[FlashSaleFrame1]=@FlashSaleFrame1,[FlashSaleFrame2]=@FlashSaleFrame2,[FlashSaleFrame3]=@FlashSaleFrame3 WHERE [ID] = @ID";
                db.AddParameter("@FlashSaleTimeDisplay", System.Data.SqlDbType.DateTime, hashtable["FlashSaleTimeDisplay"].ToString());
                db.AddParameter("@FlashSaleTimeReal", System.Data.SqlDbType.DateTime, hashtable["FlashSaleTimeReal"].ToString());
                db.AddParameter("@AutoRenewal", System.Data.SqlDbType.Int, hashtable["AutoRenewal"].ToString());
                db.AddParameter("@FlashSaleHeader", System.Data.SqlDbType.NVarChar, hashtable["FlashSaleHeader"].ToString());
                db.AddParameter("@FlashSaleBackgroundColor", System.Data.SqlDbType.NVarChar, hashtable["FlashSaleBackgroundColor"].ToString());
                db.AddParameter("@FlashSaleFrame1", System.Data.SqlDbType.NVarChar, hashtable["FlashSaleFrame1"].ToString());
                db.AddParameter("@FlashSaleFrame2", System.Data.SqlDbType.NVarChar, hashtable["FlashSaleFrame2"].ToString());
                db.AddParameter("@FlashSaleFrame3", System.Data.SqlDbType.NVarChar, hashtable["FlashSaleFrame3"].ToString());
                db.AddParameter("@ID", System.Data.SqlDbType.Int, 1);
                db.ExecuteSql(sqlQuery);

                if (click_action == "saveandcopy")
                    CookieUtility.SetValueToCookie("notice", "update_copy_success");
                else
                    CookieUtility.SetValueToCookie("notice", "update_success");

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(1), Request.RawUrl);
                BindData();
            }

            if (!string.IsNullOrEmpty(Request.Form["sitemap"]) && Request.Form["sitemap"] == "on")
                GenSitemap.SitemapUpdate();
            if (!string.IsNullOrEmpty(Request.Form["ggshopping"]) && Request.Form["ggshopping"] == "on")
                GenSitemap.GenGoogleShopping(System.Web.HttpContext.Current, "hoanghai");
        }
    }
}