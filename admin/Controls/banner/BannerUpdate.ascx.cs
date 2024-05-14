
using System;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
public partial class admin_Controls_banner_BannerUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table= "tblBanner";
    public string 
        AttributesUrlList = string.Empty, 
        AttributesNameList = string.Empty,
        Attr_Config_IDList = string.Empty,
        Attr_Config_UrlList = string.Empty,
        Attr_Config_NameList = string.Empty, 
        CatUrlList = string.Empty, 
        CatNameList = string.Empty;

    public string image_1 = C.NO_IMG_PATH, image_2 = C.NO_IMG_PATH, image_3 = C.NO_IMG_PATH;
    public BannerPositionFlag bannerPositionFlag;


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
            dr = db.NewRow(table);
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
                string sqlQuery = string.Format("SELECT * FROM tblBanner Where ID='{0}'", SqlFilterID);
                var ds = db.ExecuteSqlDataTable(sqlQuery);
                if (ds.Rows.Count > 0)
                {
                    dr = ds.Rows[0];
                    image_1 = ds.Rows[0]["Image_1"].ToString();
                }
            }

            int BannerPositionINT = 0;

            DataTable dtFlag = SqlHelper.SQLToDataTable(table, "Flags", string.Format("ID='{0}'", SqlFilterID), "ID", 1, 1);
            if (Utils.CheckExist_DataTable(dtFlag))
            {
                BannerPositionINT = ConvertUtility.ToInt32(dtFlag.Rows[0]["Flags"]);
            }
            bannerPositionFlag = (BannerPositionFlag)BannerPositionINT;


        }

        Utils.GetAttrByID(dr["CategoryIDList"].ToString(), "tblCategories", false, ref CatUrlList, ref CatNameList);
        Utils.GetAttrByUrl(dr["AttributeConfigUrlList"].ToString(), "tblAttributeConfigs", false, ref Attr_Config_IDList, ref Attr_Config_NameList);

    }

    #endregion

    #region Update Database
    protected void UpdateDatabase()
    {
        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {
            #region Flags
            string HomeSlider = Request.Form["HomeSlider"];
            string RightSlider = Request.Form["RightSlider"];
            string ByCategory = Request.Form["ByCategory"];
            string OpenNewWindows = Request.Form["OpenNewWindows"];
            string RightProductDetail = Request.Form["RightProductDetail"];
            string TopBanner = Request.Form["TopBanner"];
            string Popup = Request.Form["Popup"];
            string BottomSlider = Request.Form["BottomSlider"];

            int Position_INT = 0;

            if (!string.IsNullOrEmpty(HomeSlider) && HomeSlider == "on")
                Position_INT += (int)BannerPositionFlag.HomeSlider;
            if (!string.IsNullOrEmpty(RightSlider) && RightSlider == "on")
                Position_INT += (int)BannerPositionFlag.RightSlider;
            if (!string.IsNullOrEmpty(ByCategory) && ByCategory == "on")
                Position_INT += (int)BannerPositionFlag.ByCategory;
            if (!string.IsNullOrEmpty(HomeSlider) && HomeSlider == "on")
                Position_INT += (int)BannerPositionFlag.OpenNewWindows;
            if (!string.IsNullOrEmpty(RightProductDetail) && RightProductDetail == "on")
                Position_INT += (int)BannerPositionFlag.RightProductDetail;
            if (!string.IsNullOrEmpty(TopBanner) && TopBanner == "on")
                Position_INT += (int)BannerPositionFlag.TopBanner;
            if (!string.IsNullOrEmpty(Popup) && Popup == "on")
                Position_INT += (int)BannerPositionFlag.Popup;
            if (!string.IsNullOrEmpty(BottomSlider) && BottomSlider == "on")
                Position_INT += (int)BannerPositionFlag.BottomSlider;

            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;

            #endregion

            CacheUtility.PurgeCacheItems(table);

            Utils.GetAttrByID(Request.Form["categoryidlist"], "tblCategories", true, ref CatUrlList, ref CatNameList);
            Utils.GetAttrByUrl(Request.Form["attributeconfigidlist"], "tblAttributeConfigs", true, ref Attr_Config_IDList, ref Attr_Config_NameList);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["Link"] = Utils.KillChars(Request.Form["link"]);
            hashtable["Alt"] = Utils.KillChars(Request.Form["alt"]);
            hashtable["FriendlyUrlCategory"] = Utils.KillChars(Request.Form["friendlyurlcategory"]);

            hashtable["CategoryIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["categoryidlist"]));
            hashtable["CategoryNameList"] = Utils.CommaSQLAdd(CatNameList);
            hashtable["CategoryUrlList"] = Utils.CommaSQLAdd(CatUrlList);
            hashtable["CategoryIDParentList"] = GetCategoryIDParentList();

            hashtable["AttributesIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributesidlist"]));
            hashtable["AttributesUrlList"] = Utils.KillChars(AttributesUrlList);

            hashtable["AttributeConfigIDList"] = Utils.KillChars(Attr_Config_IDList);
            hashtable["AttributeConfigUrlList"] = Utils.KillChars(Request.Form["attributeconfigidlist"]);

            hashtable["StartDate"] = Utils.KillChars(Request.Form["startdate"]);
            hashtable["EndDate"] = DateUtil.GetMaxDateTime_IfNull(Request.Form["enddate"]);

            hashtable["Image_1"] = Utils.KillChars(Request.Form["image_1"]);
            hashtable["Image_2"] = Utils.KillChars(Request.Form["image_2"]);
            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);
            hashtable["MetaKeyword"] = Utils.KillChars(Request.Form["metakeyword"]);
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);
            hashtable["Flags"] = Position_INT;
            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblBanner] SET [Name]=@Name, [Link]=@Link, [Alt]=@Alt, [FriendlyUrlCategory]=@FriendlyUrlCategory, [CategoryIDList]=@CategoryIDList, [CategoryNameList]=@CategoryNameList, [AttributesIDList]=@AttributesIDList, [AttributesUrlList]=@AttributesUrlList, [CategoryUrlList]=@CategoryUrlList, [CategoryIDParentList]=@CategoryIDParentList, [AttributeConfigIDList]=@AttributeConfigIDList, [AttributeConfigUrlList]=@AttributeConfigUrlList, [StartDate]=@StartDate, [EndDate]=@EndDate, [Flags]=@Flags, [Hide]=@Hide, [Sort]=@Sort, [Image_1]=@Image_1, [Image_2]=@Image_2, [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblBanner] ([Name],[Link],[Alt],[FriendlyUrlCategory],[CategoryIDList],[CategoryNameList],[AttributesIDList],[AttributesUrlList],[CategoryUrlList],[CategoryIDParentList],[AttributeConfigIDList],[AttributeConfigUrlList],[StartDate],[EndDate],[Flags],[Hide],[Sort],[Image_1],[Image_2],[MetaTitle],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@Link,@Alt,@FriendlyUrlCategory,@CategoryIDList,@CategoryNameList,@AttributesIDList,@AttributesUrlList,@CategoryUrlList,@CategoryIDParentList,@AttributeConfigIDList,@AttributeConfigUrlList,@StartDate,@EndDate,@Flags,@Hide,@Sort,@Image_1,@Image_2,@MetaTitle,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@Link", System.Data.SqlDbType.NVarChar, hashtable["Link"].ToString());
                db.AddParameter("@Alt", System.Data.SqlDbType.NVarChar, hashtable["Alt"].ToString());
                db.AddParameter("@FriendlyUrlCategory", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrlCategory"].ToString());
                db.AddParameter("@CategoryIDList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDList"].ToString());
                db.AddParameter("@CategoryNameList", System.Data.SqlDbType.NVarChar, hashtable["CategoryNameList"].ToString());
                db.AddParameter("@AttributesIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributesIDList"].ToString());
                db.AddParameter("@AttributesUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributesUrlList"].ToString());
                db.AddParameter("@CategoryUrlList", System.Data.SqlDbType.NVarChar, hashtable["CategoryUrlList"].ToString());
                db.AddParameter("@CategoryIDParentList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDParentList"].ToString());
                db.AddParameter("@AttributeConfigIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigIDList"].ToString());
                db.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigUrlList"].ToString());
                db.AddParameter("@StartDate", System.Data.SqlDbType.DateTime, hashtable["StartDate"].ToString());
                db.AddParameter("@EndDate", System.Data.SqlDbType.DateTime, hashtable["EndDate"].ToString());
                db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Image_1", System.Data.SqlDbType.NVarChar, hashtable["Image_1"].ToString());
                db.AddParameter("@Image_2", System.Data.SqlDbType.NVarChar, hashtable["Image_2"].ToString());
                db.AddParameter("@MetaTitle", System.Data.SqlDbType.NVarChar, hashtable["MetaTitle"].ToString());
                db.AddParameter("@MetaKeyword", System.Data.SqlDbType.NVarChar, hashtable["MetaKeyword"].ToString());
                db.AddParameter("@MetaDescription", System.Data.SqlDbType.NVarChar, hashtable["MetaDescription"].ToString());
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