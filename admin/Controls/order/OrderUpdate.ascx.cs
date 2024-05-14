using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;

public partial class admin_Controls_order_OrderUpdate : System.Web.UI.UserControl
{

    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblAttributes";
    public string image_1 = C.NO_IMG_PATH, ProductList;
    #endregion

    #region BindData

    protected void Page_Load(object sender, EventArgs e)
    {
        ProccessParameter();
        if (!IsPostBack)
        {
            BindData();
            UpdateDatabase();
        }
    }

    protected void ProccessParameter()
    {
        ID = RequestHelper.GetInt("id", 0);
        IDCopy = RequestHelper.GetInt("idCopy", 0);
        control = ConvertUtility.ToString(Page.RouteData.Values["control"]).ToLower();
        click_action = Request.Form["done"];
    }

    protected void BindData()
    {
        using (var db = SqlService.GetSqlService())
        {
            dr = db.NewRow("tblAttributes");
        }
        int SqlFilterID = 0;
        if (ID > 0 || IDCopy > 0)
        {
            if (ID > 0)
            {
                IsUpdate = true;
                SqlFilterID = ID;
            }
            else
            {
                SqlFilterID = IDCopy;
            }

            using (var db = SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("SELECT * FROM tblOrder Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                    ProductList = GetProductList();
                }
            }

           
        }
    }

    #endregion

    #region Update Database
    protected void UpdateDatabase()
    {
        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {
            CacheUtility.PurgeCacheItems(table);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["MemberID"] = Utils.KillChars(Request.Form["memberid"]);
            hashtable["Status"] = Utils.KillChars(Request.Form["status"]);
            hashtable["Address"] = Utils.KillChars(Request.Form["address"]);
            hashtable["Phone"] = Utils.KillChars(Request.Form["phone"]);
            hashtable["Email"] = Utils.KillChars(Request.Form["email"]);
            hashtable["Json"] = Utils.KillChars(Request.Form["json"]);
            hashtable["PriceFinal"] = Utils.KillChars(Request.Form["pricefinal"]);
            hashtable["Flags"] = Utils.KillChars(Request.Form["flags"]);
            hashtable["PaymentMethod"] = Utils.KillChars(Request.Form["paymentmethod"]);
            hashtable["ShippingMethod"] = Utils.KillChars(Request.Form["shippingmethod"]);
            hashtable["NoteMember"] = Utils.KillChars(Request.Form["notemember"]);
            hashtable["NoteAdmin"] = Utils.KillChars(Request.Form["noteadmin"]);
            hashtable["ShipDate"] = Utils.KillChars(Request.Form["shipdate"]);
            hashtable["Coupon"] = Utils.KillChars(Request.Form["coupon"]);
            hashtable["Token"] = Utils.KillChars(Request.Form["token"]);
            hashtable["MailTemplate"] = Utils.KillChars(Request.Form["mailtemplate"]);
            hashtable["Website"] = Utils.KillChars(Request.Form["website"]);

            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE[dbo].[tblOrder] SET [Name]=@Name,[Status]=@Status,[Address]=@Address,[Phone]=@Phone,[Email]=@Email,[PriceFinal]=@PriceFinal,[PaymentMethod]=@PaymentMethod,[ShippingMethod]=@ShippingMethod,[NoteMember]=@NoteMember,[NoteAdmin]=@NoteAdmin,[ShipDate]=@ShipDate,[Coupon]=@Coupon,[Token]=@Token,[EdittedDate]=@EdittedDate,[EditedBy]=@EditedBy,[Website]=@Website WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblOrder] ([Name],[MemberID],[Status],[Address],[Phone],[Email],[Website],[PriceFinal],[Flags],[PaymentMethod],[ShippingMethod],[NoteMember],[NoteAdmin],[ShipDate],[CreatedDate],[CreatedBy],[Coupon],[Token],[MailTemplate]) OUTPUT INSERTED.ID VALUES (@Name,@MemberID,@Status,@Address,@Phone,@Email,@Website,@PriceFinal,@Flags,@PaymentMethod,@ShippingMethod,@NoteMember,@NoteAdmin,@ShipDate,@CreatedDate,@CreatedBy,@Coupon,@Token,@MailTemplate)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@MemberID", System.Data.SqlDbType.NVarChar, hashtable["MemberID"].ToString());
                db.AddParameter("@Status", System.Data.SqlDbType.Int, hashtable["Status"].ToString());
                db.AddParameter("@Address", System.Data.SqlDbType.NVarChar, hashtable["Address"].ToString());
                db.AddParameter("@Phone", System.Data.SqlDbType.NVarChar, hashtable["Phone"].ToString());
                db.AddParameter("@Email", System.Data.SqlDbType.NVarChar, hashtable["Email"].ToString());
                db.AddParameter("@Website", System.Data.SqlDbType.NVarChar, hashtable["Website"].ToString());
                db.AddParameter("@PriceFinal", System.Data.SqlDbType.Money, ConvertUtility.ToDecimal(hashtable["PriceFinal"].ToString()));
                db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
                db.AddParameter("@PaymentMethod", System.Data.SqlDbType.NVarChar, hashtable["PaymentMethod"].ToString());
                db.AddParameter("@ShippingMethod", System.Data.SqlDbType.NVarChar, hashtable["ShippingMethod"].ToString());
                db.AddParameter("@NoteMember", System.Data.SqlDbType.NVarChar, hashtable["NoteMember"].ToString());
                db.AddParameter("@NoteAdmin", System.Data.SqlDbType.NVarChar, hashtable["NoteAdmin"].ToString());
                db.AddParameter("@ShipDate", System.Data.SqlDbType.DateTime, hashtable["ShipDate"].ToString());
                db.AddParameter("@Coupon", System.Data.SqlDbType.NVarChar, hashtable["Coupon"].ToString());
                db.AddParameter("@Token", System.Data.SqlDbType.NVarChar, hashtable["Token"].ToString());
                db.AddParameter("@MailTemplate", System.Data.SqlDbType.NVarChar, hashtable["MailTemplate"].ToString());
                db.AddParameter("@EdittedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);

                if (IsUpdate)
                {
                    db.AddParameter("@ID", System.Data.SqlDbType.Int, ID);
                    db.ExecuteSql(sqlQuery);

                    if (click_action == "saveandcopy")
                        CookieUtility.SetValueToCookie("notice", "update_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "update_success");
                }
                else
                {
                   
                    ID = db.ExecuteSqlScalar<int>(sqlQuery, 0);

                    if (click_action == "saveandcopy")
                        CookieUtility.SetValueToCookie("notice", "insert_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "insert_success");
                }

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(IsUpdate), Request.RawUrl);
            }
        }
        else if (click_action == "delete")
        {
            CookieUtility.SetValueToCookie("notice", "delete_success");
        }
        ActionAfterUpdate();
    }
    #endregion

    #region Orther Action
    protected void ActionAfterUpdate()
    {
        if (click_action == "saveandback" || click_action == "cancel" || click_action == "delete")
        {
            Response.Redirect(Utils.GetViewControl());
        }
        if (click_action == "saveandadd")
        {
            Response.Redirect(Utils.GetEditControl());
        }
        else if (click_action == "saveandcopy")
        {
            Response.Redirect(Utils.GetEditControl() + "?idCopy=" + ID);
        }
        else if (click_action == "save")
        {
            BindData();
        }
    }

    #endregion


    protected string GetProductList()
    {
        string strProductList = string.Empty;
        string json = ConvertUtility.ToString(dr["Json"]);

        if (!string.IsNullOrEmpty(json))
        {
            List<OrderInfo> orderInfoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderInfo>>(json);
            if (orderInfoList.Count > 0)
            {
                foreach (OrderInfo orderInfo in orderInfoList)
                {
                    string Coupon = "";
                    if (orderInfo.NumberApplyVoucher > 0)
                        Coupon = string.Format(@"<p style=""color:#0045a3;font-style:italic"">(đã áp dụng mã giảm giá <b class=""coupon_code"">{0}</b> cho <b class=""coupon_quantity_apply"">{1}</b> sản phẩm)<p>", orderInfo.CouponCode, orderInfo.NumberApplyVoucher);

                    DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", "ID=" + orderInfo.ProductID);
                    if (Utils.CheckExist_DataTable(dtProduct))
                    {
                        string link = TextChanger.GetLinkRewrite_Products(dtProduct.Rows[0]["FriendlyUrlCategory"], dtProduct.Rows[0]["FriendlyUrl"]);

                        strProductList += string.Format(@"<tr align=""center"">
                                                        <td>
                                                            <a href=""{0}""><img src=""{1}"" /></a>
                                                            
                                                        </td>
                                                         <td align=""left"">
                                                            <a href=""{0}"">{3}</a>
                                                            {2}
                                                            {4}
                                                        </td>
                                                        <td>
                                                            {5}
                                                        </td>
                                                      
                                                        <td align=""right"">
                                                            <span style=""color: #FF0000"">{6} VNĐ</span>
                                                        </td>
                                                        <td align=""right"">
                                                            <span style=""color: #FF0000"">{7} VNĐ</span>
                                                        </td>
                                                    </tr>", link, orderInfo.Image + "?width=100&height=100&quality=100", Coupon, orderInfo.Name, "", orderInfo.Quantity, string.Format("{0:N0}", orderInfo.Price), string.Format("{0:N0}", orderInfo.TotalPrice));
                    }
                }
            }
        }
        return strProductList;
    }


}