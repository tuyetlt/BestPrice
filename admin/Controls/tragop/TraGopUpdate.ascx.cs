using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;


public partial class admin_Controls_tragop_TraGopUpdate : System.Web.UI.UserControl
{

    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control;
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
            dr = db.NewRow("tblTraGop");
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
                string sqlQuery = string.Format("SELECT * FROM tblTraGop Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
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
            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["Phone"] = Utils.KillChars(Request.Form["phone"]);
            hashtable["Email"] = Utils.KillChars(Request.Form["email"]);
            hashtable["Address"] = Utils.KillChars(Request.Form["address"]);
            hashtable["Product"] = Utils.KillChars(Request.Form["product"]);
            hashtable["Info"] = Utils.KillChars(Request.Form["info"]);
            hashtable["BankOrCard"] = Utils.KillChars(Request.Form["bankorcard"]);
            hashtable["AdminNote"] = Utils.KillChars(Request.Form["adminnote"]);
            hashtable["Status"] = Utils.KillChars(Request.Form["status"]);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblTraGop] SET [Name]=@Name, [Phone]=@Phone, [Email]=@Email, [Address]=@Address, [Product]=@Product, [Info]=@Info, [BankOrCard]=@BankOrCard, [AdminNote]=@AdminNote, [Status]=@Status, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblTraGop] ([Name],[Phone],[Email],[Address],[Product],[Info],[BankOrCard],[AdminNote],[Status],[CreatedDate],[CreatedBy],[EditedDate],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@Phone,@Email,@Address,@Product,@Info,@BankOrCard,@AdminNote,@Status,@CreatedDate,@CreatedBy,@EditedDate,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@Phone", System.Data.SqlDbType.NVarChar, hashtable["Phone"].ToString());
                db.AddParameter("@Email", System.Data.SqlDbType.NVarChar, hashtable["Email"].ToString());
                db.AddParameter("@Address", System.Data.SqlDbType.NVarChar, hashtable["Address"].ToString());
                db.AddParameter("@Product", System.Data.SqlDbType.NVarChar, hashtable["Product"].ToString());
                db.AddParameter("@Info", System.Data.SqlDbType.NVarChar, hashtable["Info"].ToString());
                db.AddParameter("@BankOrCard", System.Data.SqlDbType.NVarChar, hashtable["BankOrCard"].ToString());
                db.AddParameter("@AdminNote", System.Data.SqlDbType.NVarChar, hashtable["AdminNote"].ToString());
                db.AddParameter("@Status", System.Data.SqlDbType.NVarChar, hashtable["Status"].ToString());
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);

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
                    db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                    db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                    ID = db.ExecuteSqlScalar<int>(sqlQuery, 0);

                    if (click_action == "saveandcopy")
                        CookieUtility.SetValueToCookie("notice", "insert_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "insert_success");
                }
            }
        }
       
        else if (click_action == "delete")
        {
            DeleteRecord();
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
            Response.Redirect("/admin/" + control + "/view/");
        }
        if (click_action == "saveandadd")
        {
            Response.Redirect("/admin/" + control + "/add/");
        }
        else if (click_action == "saveandcopy")
        {
            Response.Redirect("/admin/" + control + "/add/?idCopy=" + ID);
        }
        else if (click_action == "save")
        {
            BindData();
        }
    }

    protected void DeleteRecord()
    {
        using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("DELETE FROM tblTraGop WHERE ID={0}", ID);
            dbx.ExecuteSql(sqlQuery);
        }
    }

    #endregion

}