using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
public partial class admin_Controls_coupon_CouponUpdate : System.Web.UI.UserControl
{

    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblCoupon", AttributesNameList = string.Empty, AttributesUrlList = string.Empty, CatUrlList = string.Empty, CatNameList = string.Empty,CatIDList = string.Empty;

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
            dr = db.NewRow("tblCoupon");
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
                string sqlQuery = string.Format("SELECT * FROM tblCoupon Where ID='{0}'", SqlFilterID);
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
            Utils.GetAttrByID(Request.Form["attributesidlist"], "tblAttributes", true, ref AttributesUrlList, ref AttributesNameList);
            Utils.GetAttrByID(Request.Form["categoryidlist"], "tblCategories", true, ref CatUrlList, ref CatNameList, ref CatIDList);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["ValueVND"] = Utils.KillChars(Request.Form["valuevnd"].Replace(",", "").Replace(".", ""));
            hashtable["ValuePercent"] = Utils.KillChars(Request.Form["valuepercent"]);
            hashtable["MaxUsing"] = Utils.KillChars(Request.Form["maxusing"]);
            hashtable["Using"] = Utils.KillChars(Request.Form["using"]);
            hashtable["ExpireDate"] = Utils.KillChars(Request.Form["expiredate"]);
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Type"] = Utils.KillChars(Request.Form["type"]);
            hashtable["ProductID"] = Utils.KillChars(Request.Form["productid"]);
            hashtable["CategoryID"] = Utils.KillChars(Request.Form["categoryid"]);
            hashtable["ProductIDList"] = Utils.KillChars(Request.Form["productidlist"]);
            hashtable["FilterJson"] = Utils.KillChars(Request.Form["filterjson"]);
            hashtable["CategoryIDList"] = Utils.KillChars(Request.Form["categoryidlist"]);

            hashtable["AttributesIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributesidlist"]));
            hashtable["AttributesUrlList"] = Utils.KillChars(AttributesUrlList);
            
            hashtable["CategoryIDList"] = Utils.CommaSQLAdd(CatIDList);
            hashtable["CategoryNameList"] = Utils.CommaSQLAdd(CatNameList);
            hashtable["CategoryUrlList"] = Utils.CommaSQLAdd(CatUrlList);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblCoupon] SET [Name]=@Name, [ValueVND]=@ValueVND, [ValuePercent]=@ValuePercent, [MaxUsing]=@MaxUsing, [Using]=@Using, [ExpireDate]=@ExpireDate, [Sort]=@Sort, [Type]=@Type, [ProductID]=@ProductID, [CategoryID]=@CategoryID, [ProductIDList]=@ProductIDList, [FilterJson]=@FilterJson, [CategoryIDList]=@CategoryIDList,[CategoryUrlList]=@CategoryUrlList,[CategoryNameList]=@CategoryNameList, [AttributesIDList]=@AttributesIDList, [AttributesUrlList]=@AttributesUrlList, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblCoupon] ([Name],[ValueVND],[ValuePercent],[MaxUsing],[Using],[ExpireDate],[Sort],[Type],[ProductID],[CategoryID],[ProductIDList],[FilterJson],[CategoryIDList],[CategoryNameList],[CategoryUrlList],[AttributesIDList],[AttributesUrlList],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@ValueVND,@ValuePercent,@MaxUsing,@Using,@ExpireDate,@Sort,@Type,@ProductID,@CategoryID,@ProductIDList,@FilterJson,@CategoryIDList,@CategoryNameList,@CategoryUrlList,@AttributesIDList,@AttributesUrlList,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@ValueVND", System.Data.SqlDbType.Int, hashtable["ValueVND"].ToString());
                db.AddParameter("@ValuePercent", System.Data.SqlDbType.Decimal, hashtable["ValuePercent"].ToString());
                db.AddParameter("@MaxUsing", System.Data.SqlDbType.Int, hashtable["MaxUsing"].ToString());
                db.AddParameter("@Using", System.Data.SqlDbType.Int, hashtable["Using"].ToString());
                db.AddParameter("@ExpireDate", System.Data.SqlDbType.DateTime, hashtable["ExpireDate"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Type", System.Data.SqlDbType.NVarChar, hashtable["Type"].ToString());
                db.AddParameter("@ProductID", System.Data.SqlDbType.Int, hashtable["ProductID"].ToString());
                db.AddParameter("@CategoryID", System.Data.SqlDbType.Int, hashtable["CategoryID"].ToString());

                db.AddParameter("@ProductIDList", System.Data.SqlDbType.NVarChar, hashtable["ProductIDList"].ToString());
                db.AddParameter("@CategoryNameList", System.Data.SqlDbType.NVarChar, hashtable["CategoryNameList"].ToString());
                db.AddParameter("@CategoryUrlList", System.Data.SqlDbType.NVarChar, hashtable["CategoryUrlList"].ToString());

                db.AddParameter("@FilterJson", System.Data.SqlDbType.NVarChar, hashtable["FilterJson"].ToString());
                db.AddParameter("@CategoryIDList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDList"].ToString());
                db.AddParameter("@AttributesIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributesIDList"].ToString());
                db.AddParameter("@AttributesUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributesUrlList"].ToString());
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
        else
        {
            Utils.GetAttrByID(dr["AttributesIDList"].ToString(), "tblAttributes", false, ref AttributesUrlList, ref AttributesNameList);
            Utils.GetAttrByID(dr["CategoryIDList"].ToString(), "tblCategories", false, ref CatUrlList, ref CatNameList);
        }

        ActionAfterUpdate();
    }


    protected void DeleteRecord()
    {
        using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("DELETE FROM tblCoupon WHERE ID={0}", ID);
            dbx.ExecuteSql(sqlQuery);
        }
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



    protected string GetCategoryIDParentList()
    {
        string returnValue = string.Empty;
        string CateofyIDList = Request.Form["categoryidlist"].Trim(',');
        if (string.IsNullOrEmpty(CateofyIDList))
            return returnValue;

        string[] NameArr = CateofyIDList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            string CategoryIDParentList = ",";
            foreach (string CatID in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    int count = 0;
                    int ParentID = ConvertUtility.ToInt32(CatID);
                    while (ParentID > 0 && count < 10)
                    {
                        string sqlQuery = string.Format("SELECT Top 1 * FROM tblCategories Where ID={0}", ParentID);
                        var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                        if (ds != null && ds.Rows.Count > 0)
                        {
                            ParentID = ConvertUtility.ToInt32(ds.Rows[0]["ParentID"]);

                            if (!CategoryIDParentList.Contains("," + ParentID + ",") && ParentID > 0)
                                CategoryIDParentList += ParentID + ",";
                        }
                        else
                        {
                            ParentID = 0;
                        }
                        count++;
                    }
                }
            }
            returnValue = Utils.CommaSQLAdd(CategoryIDParentList.Trim(','));
        }

        return returnValue;
    }


}