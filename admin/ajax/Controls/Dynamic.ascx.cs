using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ebis.Utilities;
using System.Collections.Specialized;
using System.Data;
public partial class admin_ajax_Controls_Dynamic : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Dictionary<string, string> q = Request.QueryString.AllKeys.ToDictionary(x => x, x => Request.Params[x]);

        if (q["Action"] == "text-unsign")
        {
            if (q.ContainsKey("key"))
            {
                string table = "tblUrl";

                string id = "0";
                if (q.ContainsKey("id"))
                    id = q["id"];

                Hashtable hashtable = new Hashtable();
                string key = TextChanger.Translate(q["key"], "-").ToLower();
                hashtable.Add("url", key);

                if (!string.IsNullOrEmpty(key))
                {
                    if (!string.IsNullOrEmpty(table))
                    {
                        string filter = string.Format("FriendlyUrl=N'{0}' AND ContentID<>'{1}'", key, id);
                        DataTable dt = SqlHelper.SQLToDataTable("tblUrl", "ID,Moduls", filter, "ID", 1, 1);
                        if (Utils.CheckExist_DataTable(dt))
                        {
                            if (dt.Rows[0]["Moduls"].ToString() == "category_link") // nếu dạng link thì bỏ qua
                                hashtable.Add("esixt", "0");
                            else
                                hashtable.Add("esixt", "1");
                        }
                        else
                        {
                            hashtable.Add("esixt", "0");
                        }
                    }
                }
                else
                {
                    hashtable.Add("esixt", "1");
                }

                Response.Write(JSONHelper.ToJSON(hashtable));
                Response.End();
            }
        }
        else if (q["Action"] == "update-price")
        {
            CacheUtility.PurgeCacheItems(q["table"]);

            SqlHelper.LogsToDatabase_ByID(ConvertUtility.ToInt32(q["pid"]), q["table"], "productlist", "Giá", 1, Request.RawUrl);


            // Lấy ra giá cũ:
            DataTable dtPrice = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Price,Price1,Name", "ID=" + q["pid"]);
            if (Utils.CheckExist_DataTable(dtPrice))
            {
                decimal oldPrice = ConvertUtility.ToDecimal(dtPrice.Rows[0]["Price"]);
                decimal newPrice = ConvertUtility.ToDecimal(q["newPrice"]);
                if (oldPrice != newPrice)
                {
                    using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
                    {
                        string sqlQuery = @"INSERT INTO [dbo].[tblPriceHistory] ([Name],[ProductID],[Price],[Price1],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@ProductID,@Price,@Price1,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";

                        db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, dtPrice.Rows[0]["Name"]);
                        db.AddParameter("@ProductID", System.Data.SqlDbType.Int, dtPrice.Rows[0]["ID"]);
                        db.AddParameter("@Price", System.Data.SqlDbType.Money, q["newPrice"]);
                        db.AddParameter("@Price1", System.Data.SqlDbType.Money, 0); //Giá chưa giảm tạm thời không cần
                        db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                        db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                        db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                        db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                        db.ExecuteSqlScalar<int>(sqlQuery, 0);
                    }
                }

                using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("Update {0} set {1}={2} WHERE ID={3}", q["table"], q["field"], q["newPrice"], q["pid"]);
                    dbx.ExecuteSql(sqlQuery);
                }
            }
        }
        else if (q["Action"] == "update-sort")
        {
            CacheUtility.PurgeCacheItems(q["table"]);
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("Update {0} set Sort={1} WHERE ID={2}", q["table"], q["newSort"], q["pid"]);
                dbx.ExecuteSql(sqlQuery);
            }
        }
        else if (q["Action"] == "setPriceTemp")
        {
            CacheUtility.PurgeCacheItems("tblProducts");
            CacheUtility.PurgeCacheItems("tblPrice");

            bool IsUpdate = false;
            int IDPrice = 0;
            DataTable checkExist = SqlHelper.SQLToDataTable("tblPrice", "ID", "ProductID=" + q["pid"], "");
            if (checkExist != null && checkExist.Rows.Count > 0)
                IsUpdate = true;


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblPrice] SET [ProductID]=@ProductID, [Price]=@Price, [Price1]=@Price1, [StartDate]=@StartDate, [EndDate]=@EndDate, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                    db.AddParameter("@ID", System.Data.SqlDbType.Int, ConvertUtility.ToInt32(q["priceid"]));
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblPrice] ([ProductID],[Price],[Price1],[StartDate],[EndDate],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@ProductID,@Price,@Price1,@StartDate,@EndDate,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@ProductID", System.Data.SqlDbType.Int, ConvertUtility.ToInt32(q["pid"]));
                db.AddParameter("@Price", System.Data.SqlDbType.Money, ConvertUtility.ToDecimal(q["price"].ToString().Replace(",", "").Replace(".", "")));
                db.AddParameter("@Price1", System.Data.SqlDbType.Money, ConvertUtility.ToDecimal(q["price1"].ToString().Replace(",", "").Replace(".", "")));
                db.AddParameter("@StartDate", System.Data.SqlDbType.DateTime, Utils.DateTimeString_To_DateTimeSql(q["startdate"]));
                db.AddParameter("@EndDate", System.Data.SqlDbType.DateTime, DateUtil.GetMaxDateTime_IfNull(q["enddate"]));
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);


                if (IsUpdate)
                    db.ExecuteSql(sqlQuery);
                else
                    IDPrice = db.ExecuteSqlScalar<int>(sqlQuery, 0);

                SqlHelper.LogsToDatabase_ByID(IDPrice, "tblPrice", "update price", "Giá tạm", 1, Request.RawUrl);

            }

            Response.Clear();
            Response.Write("Giá đã được cập nhật");
            Response.End();
        }
        else if (q["Action"] == "delPriceTemp")
        {
            CacheUtility.PurgeCacheItems("tblProducts");
            CacheUtility.PurgeCacheItems("tblPrice");

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = @"DELETE FROM tblPrice WHERE ID=@priceid";
                db.AddParameter("@priceid", System.Data.SqlDbType.Int, ConvertUtility.ToInt32(q["priceid"]));
                db.ExecuteSql(sqlQuery);
            }

            Response.Clear();
            Response.Write("Đã xóa giá tạm thời");
            Response.End();
        }
        else if (q["Action"] == "getPriceTemp")
        {
            string productName = "";
            string productImage = "";
            DataTable dtP = SqlHelper.SQLToDataTable("tblProducts", "Name,Gallery", "ID=" + q["pid"], "");
            if (dtP != null && dtP.Rows.Count > 0)
            {
                productName = dtP.Rows[0]["Name"].ToString();
                productImage = Utils.GetFirstImageInGallery_Json(dtP.Rows[0]["Gallery"].ToString());
            }
            DataTable dt = SqlHelper.SQLToDataTable("tblPrice", "", "ProductID=" + q["pid"], "");
            if (dt != null && dt.Rows.Count > 0)
            {
                PriceTemp price = new PriceTemp();
                price.ProductName = productName;
                price.ProductID = ConvertUtility.ToInt32(dt.Rows[0]["ProductID"]);
                price.Image = productImage;
                price.Price = ConvertUtility.ToDecimal(dt.Rows[0]["Price"]);
                price.Price1 = ConvertUtility.ToDecimal(dt.Rows[0]["Price1"]);
                price.StartDate = ConvertUtility.ToDateTime(dt.Rows[0]["StartDate"]).ToString("dd/MM/yyyy HH:mm:ss");
                price.EndDate = ConvertUtility.ToDateTime(dt.Rows[0]["EndDate"]).ToString("dd/MM/yyyy HH:mm:ss");
                price.PriceID = ConvertUtility.ToInt32(dt.Rows[0]["ID"]);

                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(price, Newtonsoft.Json.Formatting.Indented);
                Response.Clear();
                Response.Write(jsonString);
                Response.End();
            }
            else
            {
                PriceTemp price = new PriceTemp();
                price.ProductID = ConvertUtility.ToInt32(q["pid"]);
                price.ProductName = productName;
                price.Image = productImage;
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(price, Newtonsoft.Json.Formatting.Indented);
                Response.Clear();
                Response.Write(jsonString);
                Response.End();

            }

        }
        else if (q["Action"] == "setFlag")
        {

            CacheUtility.PurgeCacheItems(q["table"]);



            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format(@"Update {0} set {1}={2} Where ID={3}", q["table"], q["field"], q["total_flags"], q["pid"]);
                db.ExecuteSql(sqlQuery);
            }


        }


        //for (int index = 0; index < queryString.Count; index++)
        //{
        //    var item = queryString.ElementAt(index);
        //    string Key = item.Key;
        //    string Value = item.Value;
        //    //Response.Write(itemKey + "-" + itemValue + "<br />");
        //}





    }
}