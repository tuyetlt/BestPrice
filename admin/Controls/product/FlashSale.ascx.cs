using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
public partial class admin_Controls_product_FlashSale : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    int ID = 1;
    public string click_action, controlName, table = "tblConfigs";
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
            
            if (!string.IsNullOrEmpty(Request.Form["AutoRenewal"]) && Request.Form["AutoRenewal"] == "on")
                hashtable["AutoRenewal"] = 1;

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = @"UPDATE [dbo].[tblConfigs] SET [FlashSaleTimeDisplay]=@FlashSaleTimeDisplay,[FlashSaleTimeReal]=@FlashSaleTimeReal,[AutoRenewal]=@AutoRenewal WHERE [ID] = @ID";
                db.AddParameter("@FlashSaleTimeDisplay", System.Data.SqlDbType.DateTime, hashtable["FlashSaleTimeDisplay"].ToString());
                db.AddParameter("@FlashSaleTimeReal", System.Data.SqlDbType.DateTime, hashtable["FlashSaleTimeReal"].ToString());
                db.AddParameter("@AutoRenewal", System.Data.SqlDbType.Int, hashtable["AutoRenewal"].ToString());
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