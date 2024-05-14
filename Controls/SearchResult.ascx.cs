using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Threading;

public partial class Controls_SearchResult : System.Web.UI.UserControl
{
    //public DataRow drCat, drProduct;
    //public DataTable dtCat, dtProduct;
    //public int ID, RootID, _totalProduct;
    //public string keyword;

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    ProccessParameter();
    //    if (!IsPostBack)
    //    {
    //        BindData();

    //    }
    //}

    //protected void ProccessParameter()
    //{
    //    keyword = RequestHelper.GetString("key", string.Empty);
    //}

    //protected string GetIPAddress()
    //{
    //    System.Web.HttpContext context = System.Web.HttpContext.Current;
    //    string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

    //    if (!string.IsNullOrEmpty(ipAddress))
    //    {
    //        string[] addresses = ipAddress.Split(',');
    //        if (addresses.Length != 0)
    //        {
    //            return addresses[0];
    //        }
    //    }

    //    return context.Request.ServerVariables["REMOTE_ADDR"];
    //}

    //protected void BindData()
    //{
    //    string IPAdd = GetIPAddress();

    //    DataTable dtS = SqlHelper.SQLToDataTable("tblKeySearch", "ID,Count", string.Format("IP = N'{0}' AND Block=1", IPAdd));
    //    if (Utils.CheckExist_DataTable(dtS))
    //    {
    //        Hashtable htBlock = new Hashtable();

    //        htBlock["Name"] = keyword;
    //        htBlock["IP"] = GetIPAddress();

    //        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
    //        {
    //            string sqlQuery = @"INSERT INTO [dbo].[tblKeySearchBlockByIP] ([Name],[CreatedDate],[IP],[Block],[Count]) OUTPUT INSERTED.ID VALUES (@Name,@CreatedDate,@IP,@Block,@Count)";

    //            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, htBlock["Name"]);
    //            db.AddParameter("@IP", System.Data.SqlDbType.NVarChar, htBlock["IP"].ToString());
    //            db.AddParameter("@Block", System.Data.SqlDbType.Bit, true);
    //            db.AddParameter("@Count", System.Data.SqlDbType.Int, dtS.Rows.Count);
    //            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
    //            db.ExecuteSql(sqlQuery);
    //        }

    //        //Thread.Sleep(100000);
    //        Response.Redirect(C.ROOT_URL);
    //    }
    //    else
    //    {
    //        Hashtable hashtable = new Hashtable();
    //        hashtable["Name"] = keyword;
    //        hashtable["Count"] = Utils.KillChars(Request.Form["count"]);
    //        hashtable["IP"] = GetIPAddress();

    //        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
    //        {
    //            string sqlQuery = @"INSERT INTO [dbo].[tblKeySearch] ([Name],[Count],[CreatedDate],[IP]) OUTPUT INSERTED.ID VALUES (@Name,@Count,@CreatedDate,@IP)";

    //            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
    //            db.AddParameter("@IP", System.Data.SqlDbType.NVarChar, hashtable["IP"].ToString());
    //            db.AddParameter("@Count", System.Data.SqlDbType.Int, hashtable["Count"].ToString());
    //            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);

    //            db.ExecuteSql(sqlQuery);
    //        }

    //        string[] tubay = { "sex", "porn", "幼色情", "本子库", "gay", ".com", "】", "【", "hx.ag", "xổ số", "xbet" };
    //        bool loai = false;

    //        foreach (string tu in tubay)
    //        {
    //            if (keyword.Contains(tu))
    //                loai = true;
    //        }

    //        if (loai)
    //            Response.Redirect(C.ROOT_URL);

    //        //dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,Price,Price1, Gallery,FriendlyUrlCategory,FriendlyUrl", string.Format("(Name like N'%{0}%' OR NameUnsign like N'%{0}%') AND {1}", keyword, Utils.CreateFilterHide), "EditedDate DESC", 1, C.ROWS_PRODUCTCATEGORY, out _totalProduct);

    //        dtProduct = Utils.SearchProduct(keyword);
    //        if (dtProduct.Rows.Count > 0)
    //        {
    //            SetSeo();
    //            PageInfo.ControlName = string.Format("Kết quả tìm kiếm <b>'{0}'</b>", keyword);
    //        }

    //        CookieUtility.SetValueToCookie("pageIndex_Category", "2");
    //    }
    //}


    //protected void SetSeo()
    //{
    //    SEO.meta_title = keyword + " - " + ConfigWeb.MetaTitle;
    //    SEO.meta_keyword = keyword + ", " + ConfigWeb.MetaKeyword;
    //    SEO.meta_description = keyword + ", " + ConfigWeb.MetaDescription;
    //}

}