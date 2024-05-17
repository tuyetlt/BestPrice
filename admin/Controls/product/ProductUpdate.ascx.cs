using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;
using System.Linq;
using System.Text;
public partial class admin_Controls_ProductUpdate : System.Web.UI.UserControl
{
    #region Variable
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    public bool IsUpdate = false;
    int ID = 0, IDCopy = 0;
    public string click_action, control, table = "tblProducts";
    public string
        AttributesUrlList = string.Empty,
        AttributesNameList = string.Empty,
        Attr_Config_UrlList = string.Empty,
        Attr_Config_NameList = string.Empty,
        CatUrlList = string.Empty,
        CatNameList = string.Empty,
         CatIDList = string.Empty,

        TagIDList = string.Empty,
        TagUrlList = string.Empty,
        TagNameList = string.Empty,

        HashTagIDList = string.Empty,
        HashTagUrlList = string.Empty,
        HashTagNameList = string.Empty,
        OldPrice = string.Empty,
        OldPrice1 = string.Empty;

    public AttrProductFlag attrProFlag;
    public ProductStatusFlag statusFlag;
    public ProductVATFlag vatFlag;

    string FriendlyUrlCategory = string.Empty;


    #endregion

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

        }

        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT * FROM tblProducts Where ID='{0}'", SqlFilterID);
            var ds = db.ExecuteSqlDataTable(sqlQuery);
            if (ds.Rows.Count > 0)
            {
                dr = ds.Rows[0];
                OldPrice = dr["Price"].ToString();
                OldPrice1 = dr["Price1"].ToString();
            }
        }

        int AttrInt = 0;
        int ProductStatusInt = 0;
        int ProductVATInt = 0;
        DataTable dtFlag = SqlHelper.SQLToDataTable(table, "AttrProductFlag, ProductStatusFlag, ProductVATFlag", string.Format("ID='{0}'", SqlFilterID), "ID", 1, 1);
        if (Utils.CheckExist_DataTable(dtFlag))
        {
            AttrInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["AttrProductFlag"]);
            ProductStatusInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["ProductStatusFlag"]);
            ProductVATInt = ConvertUtility.ToInt32(dtFlag.Rows[0]["ProductVATFlag"]);
        }

        attrProFlag = (AttrProductFlag)AttrInt;
        statusFlag = (ProductStatusFlag)ProductStatusInt;
        vatFlag = (ProductVATFlag)ProductVATInt;

        Utils.GetAttrByID(dr["AttributesIDList"].ToString(), "tblAttributes", false, ref AttributesUrlList, ref AttributesNameList);
        Utils.GetAttrByID(dr["CategoryIDList"].ToString(), "tblCategories", false, ref CatUrlList, ref CatNameList);
        Utils.GetAttrByID(dr["AttributeConfigIDList"].ToString(), "tblAttributeConfigs", false, ref Attr_Config_UrlList, ref Attr_Config_NameList);

        //Utils.GetTagByName(dr["TagNameList"].ToString(), "tblCategories", "tag", false, ref TagUrlList, ref TagIDList);
        //Utils.GetTagByName(dr["HashTagNameList"].ToString(), "tblCategories", "hashtag", false, ref HashTagUrlList, ref HashTagIDList);
    }

    protected void UpdateDatabase()
    {
        //Dictionary<string, string> q = Request.QueryString.AllKeys.ToDictionary(x => x, x => Request.Params[x]);

        if (!String.IsNullOrEmpty(click_action) && (click_action == "save" || click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd" || click_action == "view"))
        {
            #region Flags
            string home = ConvertUtility.ToString(Request.Form["trangchu"]);
            string home1 = ConvertUtility.ToString(Request.Form["home1"]);
            string priority = ConvertUtility.ToString(Request.Form["priority"]);
            string chkNew = ConvertUtility.ToString(Request.Form["chkNew"]);
            string chkFlashSale = ConvertUtility.ToString(Request.Form["chkFlashSale"]);
            string status = ConvertUtility.ToString(Request.Form["productstatusflag"]);
            string vat = ConvertUtility.ToString(Request.Form["productvatflag"]);
            string TextHome1 = ConvertUtility.ToString(Request.Form["TextHome1"]);
            string TextHome2 = ConvertUtility.ToString(Request.Form["TextHome2"]);
            string TextHome3 = ConvertUtility.ToString(Request.Form["TextHome3"]);
            string TextHome4 = ConvertUtility.ToString(Request.Form["TextHome4"]);
            string TextHome5 = ConvertUtility.ToString(Request.Form["TextHome5"]);

            int AttrProductFlag_INT = 0;
            AttrProductFlag attrProductFlag = AttrProductFlag.None;
           
            if (!string.IsNullOrEmpty(home) && home == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.Home;
                attrProductFlag |= AttrProductFlag.Home;
            }

            if (!string.IsNullOrEmpty(home1) && home1 == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.Home1;
                attrProductFlag |= AttrProductFlag.Home1;
            }
            if (!string.IsNullOrEmpty(priority) && priority == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.Priority;
                attrProductFlag |= AttrProductFlag.Priority;
            }
            if (!string.IsNullOrEmpty(chkNew) && chkNew == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.New;
                attrProductFlag |= AttrProductFlag.New;
            }
            if (!string.IsNullOrEmpty(TextHome1) && TextHome1 == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.TextHome1;
                attrProductFlag |= AttrProductFlag.TextHome1;
            }
            if (!string.IsNullOrEmpty(TextHome2) && TextHome2 == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.TextHome2;
                attrProductFlag |= AttrProductFlag.TextHome2;
            }
            if (!string.IsNullOrEmpty(TextHome3) && TextHome3 == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.TextHome3;
                attrProductFlag |= AttrProductFlag.TextHome3;
            }
            if (!string.IsNullOrEmpty(TextHome4) && TextHome4 == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.TextHome4;
                attrProductFlag |= AttrProductFlag.TextHome4;
            }
            if (!string.IsNullOrEmpty(TextHome5) && TextHome5 == "on")
            {
                AttrProductFlag_INT += (int)AttrProductFlag.TextHome5;
                attrProductFlag |= AttrProductFlag.TextHome5;
            }



            //if (!string.IsNullOrEmpty(chkFlashSale) && chkFlashSale == "on")
            //{
            //    AttrProductFlag_INT += (int)AttrProductFlag.FlashSale;
            //    attrProductFlag |= AttrProductFlag.FlashSale;
            //}

            int ProductStatusFlag_INT = 0;
            ProductStatusFlag productStatusFlag = ProductStatusFlag.None;
            if (!string.IsNullOrEmpty(status))
            {
                if (status == ProductStatusFlag.InStock.ToString())
                {
                    ProductStatusFlag_INT += (int)ProductStatusFlag.InStock;
                    productStatusFlag |= ProductStatusFlag.InStock;
                }
                if (status == ProductStatusFlag.OutStock.ToString())
                {
                    ProductStatusFlag_INT += (int)ProductStatusFlag.OutStock;
                    productStatusFlag |= ProductStatusFlag.OutStock;
                }
                if (status == ProductStatusFlag.Contact.ToString())
                {
                    ProductStatusFlag_INT += (int)ProductStatusFlag.Contact;
                    productStatusFlag |= ProductStatusFlag.Contact;
                }
            }

            int ProductVATFlag_INT = 0;
            ProductVATFlag productVATFlag = ProductVATFlag.None;
            if (!string.IsNullOrEmpty(vat))
            {
                if (vat == ProductVATFlag.Unown.ToString())
                {
                    ProductVATFlag_INT += (int)ProductVATFlag.Unown;
                    productVATFlag |= ProductVATFlag.Unown;
                }
                if (vat == ProductVATFlag.Yes.ToString())
                {
                    ProductVATFlag_INT += (int)ProductVATFlag.Yes;
                    productVATFlag |= ProductVATFlag.Yes;
                }
                if (vat == ProductVATFlag.No.ToString())
                {
                    ProductVATFlag_INT += (int)ProductVATFlag.No;
                    productVATFlag |= ProductVATFlag.No;
                }
            }
            if (!string.IsNullOrEmpty(Request.Form["hide"]) && Request.Form["hide"] == "on")
                hashtable["Hide"] = true;
            else
                hashtable["Hide"] = false;

            #endregion


            Utils.GetAttrByID(Request.Form["attributesidlist"], "tblAttributes", true, ref AttributesUrlList, ref AttributesNameList);
            Utils.GetAttrByID(Request.Form["categoryidlist"], "tblCategories", true, ref CatUrlList, ref CatNameList, ref CatIDList);
            Utils.GetAttrByID(Request.Form["attributeconfigidlist"], "tblAttributeConfigs", true, ref Attr_Config_UrlList, ref Attr_Config_NameList);

            Utils.GetTagByName(Request.Form["tag_name"], "tblCategories", "tag", true, ref TagUrlList, ref TagIDList);
            Utils.GetTagByName(Request.Form["hashtag_name"], "tblCategories", "hashtag", true, ref HashTagUrlList, ref HashTagIDList);

            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["ModelNumber"] = Utils.KillChars(Request.Form["modelnumber"]);
            hashtable["NameUnsign"] = TextChanger.Translate(Request.Form["name"], " ");
            hashtable["Warranty"] = Utils.KillChars(Request.Form["warranty"]);
            hashtable["FriendlyUrl"] = Utils.KillChars(Request.Form["friendlyurl"]);
            hashtable["Price"] = Utils.KillChars(Request.Form["price"]).Replace(",", "").Replace(".", "");
            hashtable["Price1"] = Utils.KillChars(Request.Form["price1"]).Replace(",", "").Replace(".", "");
            hashtable["Price2"] = Utils.KillChars(Request.Form["price2"]).Replace(",", "").Replace(".", "");


            string[] arr = CatUrlList.Trim(',').Split(',');
            if (arr != null && arr.Length > 0)
                FriendlyUrlCategory = arr[0];

            hashtable["FriendlyUrlCategory"] = FriendlyUrlCategory;

            hashtable["CategoryIDList"] = Utils.CommaSQLAdd(CatIDList);
            hashtable["CategoryNameList"] = Utils.CommaSQLAdd(CatNameList);
            hashtable["CategoryUrlList"] = Utils.CommaSQLAdd(CatUrlList);
            hashtable["CategoryIDParentList"] = GetCategoryIDParentList();

            hashtable["Sort"] = Utils.KillChars(Request.Form["sort"]);
            hashtable["Sort1"] = Utils.KillChars(Request.Form["sort1"]);
            hashtable["Sort2"] = Utils.KillChars(Request.Form["sort2"]);

            //Flags
            hashtable["AttrProductFlag"] = AttrProductFlag_INT;
            hashtable["ProductStatusFlag"] = ProductStatusFlag_INT;
            hashtable["ProductVATFlag"] = ProductVATFlag_INT;


            hashtable["AttributesIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributesidlist"]));
            hashtable["AttributesUrlList"] = Utils.KillChars(AttributesUrlList);

            hashtable["AttributeConfigIDList"] = Utils.KillChars(Utils.CommaSQLAdd(Request.Form["attributeconfigidlist"]));
            hashtable["AttributeConfigUrlList"] = Utils.KillChars(Attr_Config_UrlList);

            hashtable["TagIDList"] = TagIDList;
            hashtable["TagNameList"] = Utils.CommaSQLAdd(Request.Form["tag_name"]);
            hashtable["TagUrlList"] = TagUrlList;

            hashtable["HashTagIDList"] = HashTagIDList;
            hashtable["HashTagNameList"] = Utils.CommaSQLAdd(Request.Form["hashtag_name"]);
            hashtable["HashTagUrlList"] = HashTagUrlList;

            hashtable["ManufacturerID"] = Utils.KillChars(Request.Form["manufacturerid"]);
            hashtable["ManufacturerName"] = Utils.KillChars(Request.Form["manufacturername"]);

            hashtable["Gift"] = Utils.KillChars(Request.Form["gift"]);
            hashtable["Tab1"] = Utils.KillChars(Request.Form["tab1"]);
            hashtable["Tab2"] = Utils.KillChars(Request.Form["tab2"]);
            hashtable["Tab3"] = Utils.KillChars(Request.Form["tab3"]);

            string description = Utils.SaveImageToServer(Request.Form["description"]);
            hashtable["Description"] = description;

            string longDescription = Utils.SaveImageToServer(Request.Form["longdescription"]);
            hashtable["LongDescription"] = longDescription;

            hashtable["Tags"] = Utils.KillChars(Request.Form["tags"]);
            hashtable["Image"] = Utils.KillChars(Request.Form["image"]);
            hashtable["Gallery"] = Utils.ValidJsonGallery(Request.Form["gallery"]);

            hashtable["VideoGallery"] = Utils.KillChars(Request.Form["videogallery"]);
            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);

            string metakeyword = Utils.CommaSQLRemove(Request.Form["metakeyword"]);
            metakeyword = metakeyword.Replace(System.Environment.NewLine, ",");
            hashtable["MetaKeyword"] = metakeyword;

            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);
            hashtable["Temp_CategoryID"] = Utils.KillChars(Request.Form["temp_categoryid"]);
            hashtable["Temp_ParentCategoryID"] = Utils.KillChars(Request.Form["temp_parentcategoryid"]);
            hashtable["Temp_GrandFatherCategoryID"] = Utils.KillChars(Request.Form["temp_grandfathercategoryid"]);
            hashtable["SchemaBestRating"] = Utils.KillChars(Request.Form["schemabestrating"]);
            hashtable["SchemaRatingValue"] = Utils.KillChars(Request.Form["schemaratingvalue"]);
            hashtable["SchemaRatingCount"] = Utils.KillChars(Request.Form["schemaratingcount"]);

            CacheUtility.PurgeCacheItems(table);

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblProducts] SET [Name]=@Name, [NameUnsign]=@NameUnsign, [ModelNumber]=@ModelNumber, [FriendlyUrl]=@FriendlyUrl, [Price]=@Price, [Price1]=@Price1,[Price2]=@Price2, [Warranty]=@Warranty, [Sort]=@Sort, [Sort1]=@Sort1, [Sort2]=@Sort2, [AttrProductFlag]=@AttrProductFlag, [ProductStatusFlag]=@ProductStatusFlag, [ProductVATFlag]=@ProductVATFlag, [Hide]=@Hide, [FriendlyUrlCategory]=@FriendlyUrlCategory, [CategoryIDList]=@CategoryIDList, [CategoryNameList]=@CategoryNameList, [CategoryUrlList]=@CategoryUrlList, [CategoryIDParentList]=@CategoryIDParentList, [AttributesIDList]=@AttributesIDList, [AttributesUrlList]=@AttributesUrlList, [AttributeConfigIDList]=@AttributeConfigIDList, [AttributeConfigUrlList]=@AttributeConfigUrlList, [TagIDList]=@TagIDList, [TagNameList]=@TagNameList, [TagUrlList]=@TagUrlList, [HashTagIDList]=@HashTagIDList, [HashTagNameList]=@HashTagNameList, [HashTagUrlList]=@HashTagUrlList, [ManufacturerID]=@ManufacturerID, [ManufacturerName]=@ManufacturerName, [Description]=@Description, [LongDescription]=@LongDescription, [Gift]=@Gift, [Tab1]=@Tab1, [Tab2]=@Tab2, [Tab3]=@Tab3, [Image]=@Image, [Gallery]=@Gallery, [VideoGallery]=@VideoGallery, [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [EditedDate]=@EditedDate, [EditedBy]=@EditedBy, [Temp_CategoryID]=@Temp_CategoryID, [Temp_ParentCategoryID]=@Temp_ParentCategoryID, [Temp_GrandFatherCategoryID]=@Temp_GrandFatherCategoryID,[SchemaBestRating]=@SchemaBestRating, [SchemaRatingValue]=@SchemaRatingValue, [SchemaRatingCount]=@SchemaRatingCount WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblProducts] ([Name],[NameUnsign],[ModelNumber],[FriendlyUrl],[Price],[Price1],[Price2],[Warranty],[Sort],[Sort1],[Sort2],[AttrProductFlag],[ProductStatusFlag],[ProductVATFlag],[Hide],[FriendlyUrlCategory],[CategoryIDList],[CategoryNameList],[CategoryUrlList],[CategoryIDParentList],[AttributesIDList],[AttributesUrlList],[AttributeConfigIDList],[AttributeConfigUrlList],[TagIDList],[TagNameList],[TagUrlList],[HashTagIDList],[HashTagNameList],[HashTagUrlList],[ManufacturerID],[ManufacturerName],[Description],[LongDescription],[Gift],[Tab1],[Tab2],[Tab3],[Image],[Gallery],[VideoGallery],[MetaTitle],[MetaKeyword],[MetaDescription],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy],[Temp_CategoryID],[Temp_ParentCategoryID],[Temp_GrandFatherCategoryID],[SchemaBestRating],[SchemaRatingValue],[SchemaRatingCount]) OUTPUT INSERTED.ID VALUES (@Name,@NameUnsign,@ModelNumber,@FriendlyUrl,@Price,@Price1,@Price2,@Warranty,@Sort,@Sort1,@Sort2,@AttrProductFlag,@ProductStatusFlag,@ProductVATFlag,@Hide,@FriendlyUrlCategory,@CategoryIDList,@CategoryNameList,@CategoryUrlList,@CategoryIDParentList,@AttributesIDList,@AttributesUrlList,@AttributeConfigIDList,@AttributeConfigUrlList,@TagIDList,@TagNameList,@TagUrlList,@HashTagIDList,@HashTagNameList,@HashTagUrlList,@ManufacturerID,@ManufacturerName,@Description,@LongDescription,@Gift,@Tab1,@Tab2,@Tab3,@Image,@Gallery,@VideoGallery,@MetaTitle,@MetaKeyword,@MetaDescription,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy,@Temp_CategoryID,@Temp_ParentCategoryID,@Temp_GrandFatherCategoryID,@SchemaBestRating,@SchemaRatingValue,@SchemaRatingCount)";
                }

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@NameUnsign", System.Data.SqlDbType.NVarChar, hashtable["NameUnsign"].ToString());
                db.AddParameter("@ModelNumber", System.Data.SqlDbType.NVarChar, hashtable["ModelNumber"].ToString());
                db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrl"].ToString());
                db.AddParameter("@Price", System.Data.SqlDbType.Money, hashtable["Price"].ToString());
                db.AddParameter("@Price1", System.Data.SqlDbType.Money, hashtable["Price1"].ToString());
                db.AddParameter("@Price2", System.Data.SqlDbType.Money, hashtable["Price2"].ToString());
                db.AddParameter("@Warranty", System.Data.SqlDbType.NVarChar, hashtable["Warranty"].ToString());
                db.AddParameter("@Sort", System.Data.SqlDbType.Int, hashtable["Sort"].ToString());
                db.AddParameter("@Sort1", System.Data.SqlDbType.Int, hashtable["Sort1"].ToString());
                db.AddParameter("@Sort2", System.Data.SqlDbType.Int, hashtable["Sort2"].ToString());
                db.AddParameter("@AttrProductFlag", System.Data.SqlDbType.Int, hashtable["AttrProductFlag"].ToString());
                db.AddParameter("@ProductStatusFlag", System.Data.SqlDbType.Int, hashtable["ProductStatusFlag"].ToString());
                db.AddParameter("@ProductVATFlag", System.Data.SqlDbType.Int, hashtable["ProductVATFlag"].ToString());
                db.AddParameter("@Hide", System.Data.SqlDbType.Bit, hashtable["Hide"].ToString());
                db.AddParameter("@FriendlyUrlCategory", System.Data.SqlDbType.NVarChar, hashtable["FriendlyUrlCategory"].ToString());
                db.AddParameter("@CategoryIDList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDList"].ToString());
                db.AddParameter("@CategoryNameList", System.Data.SqlDbType.NVarChar, hashtable["CategoryNameList"].ToString());
                db.AddParameter("@CategoryUrlList", System.Data.SqlDbType.NVarChar, hashtable["CategoryUrlList"].ToString());
                db.AddParameter("@CategoryIDParentList", System.Data.SqlDbType.NVarChar, hashtable["CategoryIDParentList"].ToString());
                db.AddParameter("@AttributesIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributesIDList"].ToString());
                db.AddParameter("@AttributesUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributesUrlList"].ToString());
                db.AddParameter("@AttributeConfigIDList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigIDList"].ToString());
                db.AddParameter("@AttributeConfigUrlList", System.Data.SqlDbType.NVarChar, hashtable["AttributeConfigUrlList"].ToString());
                db.AddParameter("@TagIDList", System.Data.SqlDbType.NVarChar, hashtable["TagIDList"].ToString());
                db.AddParameter("@TagNameList", System.Data.SqlDbType.NVarChar, hashtable["TagNameList"].ToString());
                db.AddParameter("@TagUrlList", System.Data.SqlDbType.NVarChar, hashtable["TagUrlList"].ToString());
                db.AddParameter("@HashTagIDList", System.Data.SqlDbType.NVarChar, hashtable["HashTagIDList"].ToString());
                db.AddParameter("@HashTagNameList", System.Data.SqlDbType.NVarChar, hashtable["HashTagNameList"].ToString());
                db.AddParameter("@HashTagUrlList", System.Data.SqlDbType.NVarChar, hashtable["HashTagUrlList"].ToString());
                db.AddParameter("@ManufacturerID", System.Data.SqlDbType.NVarChar, hashtable["ManufacturerID"].ToString());
                db.AddParameter("@ManufacturerName", System.Data.SqlDbType.NVarChar, hashtable["ManufacturerName"].ToString());
                db.AddParameter("@Description", System.Data.SqlDbType.NVarChar, hashtable["Description"].ToString());
                db.AddParameter("@LongDescription", System.Data.SqlDbType.NVarChar, hashtable["LongDescription"].ToString());
                db.AddParameter("@Gift", System.Data.SqlDbType.NVarChar, hashtable["Gift"].ToString());
                db.AddParameter("@Tab1", System.Data.SqlDbType.NVarChar, hashtable["Tab1"].ToString());
                db.AddParameter("@Tab2", System.Data.SqlDbType.NVarChar, hashtable["Tab2"].ToString());
                db.AddParameter("@Tab3", System.Data.SqlDbType.NVarChar, hashtable["Tab3"].ToString());
                db.AddParameter("@Image", System.Data.SqlDbType.NVarChar, hashtable["Image"].ToString());
                db.AddParameter("@Gallery", System.Data.SqlDbType.NVarChar, hashtable["Gallery"].ToString());
                db.AddParameter("@VideoGallery", System.Data.SqlDbType.NVarChar, hashtable["VideoGallery"].ToString());
                db.AddParameter("@MetaTitle", System.Data.SqlDbType.NVarChar, hashtable["MetaTitle"].ToString());
                db.AddParameter("@MetaKeyword", System.Data.SqlDbType.NVarChar, hashtable["MetaKeyword"].ToString());
                db.AddParameter("@MetaDescription", System.Data.SqlDbType.NVarChar, hashtable["MetaDescription"].ToString());
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                db.AddParameter("@Temp_CategoryID", System.Data.SqlDbType.Int, hashtable["Temp_CategoryID"].ToString());
                db.AddParameter("@Temp_ParentCategoryID", System.Data.SqlDbType.Int, hashtable["Temp_ParentCategoryID"].ToString());
                db.AddParameter("@Temp_GrandFatherCategoryID", System.Data.SqlDbType.Int, hashtable["Temp_GrandFatherCategoryID"].ToString());
                db.AddParameter("@SchemaBestRating", System.Data.SqlDbType.Int, hashtable["SchemaBestRating"].ToString());
                db.AddParameter("@SchemaRatingValue", System.Data.SqlDbType.Int, hashtable["SchemaRatingValue"].ToString());
                db.AddParameter("@SchemaRatingCount", System.Data.SqlDbType.Int, hashtable["SchemaRatingCount"].ToString());

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

                    if (click_action == "saveandcopy" || click_action == "save")
                        CookieUtility.SetValueToCookie("notice", "insert_copy_success");
                    else
                        CookieUtility.SetValueToCookie("notice", "insert_success");
                }

                SqlHelper.LogsToDatabase_ByID(ID, table, Utils.GetFolderControlAdmin(), ControlAdminInfo.ShortName, ConvertUtility.ToInt32(IsUpdate), Request.RawUrl);
                //UpdateTags(hashtable["Name"], "_tagType");

                UpdateGGShopping(ID);
                UpdateBrand(hashtable["AttributesIDList"].ToString(), hashtable["CategoryNameList"].ToString(), ID.ToString());
                UpdatePriceHistory(ID.ToString());
            }

            SqlHelper.Update_Url_Table(IsUpdate, "product_detail", ID, hashtable["Name"].ToString(), hashtable["FriendlyUrl"].ToString());

        }
        else if (click_action == "delete")
        {
            CookieUtility.SetValueToCookie("notice", "delete_success");
        }


        ActionAfterUpdate();
    }


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
        else if (click_action == "view")
        {
            string Url = TextChanger.GetLinkRewrite_Products(hashtable["FriendlyUrlCategory"].ToString(), hashtable["FriendlyUrl"].ToString());
            Response.Redirect(Url);
        }
    }



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





    #region thêm giá trị vào thuộc tính (chưa dùng)
    protected void UpdateAttribute(string NameList, string Type)
    {
        NameList = NameList.Trim();
        if (string.IsNullOrEmpty(NameList))
            return;

        string[] NameArr = NameList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            foreach (string name in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT * FROM tblAttributes Where Name=N'{0}' AND Type=N'{1}'", name.Trim(), Type);
                    var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (ds == null || ds.Rows.Count == 0)
                    {
                        AddAttribute(name, Type);
                    }
                }
            }
        }
    }

    protected void AddAttribute(string Name, string Type)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = "INSERT INTO tblAttributes (ParentID, Name, Type) Values (@ParentID, @Name, @Type)";
                dbx.AddParameter("@ParentID", System.Data.SqlDbType.Int, 0);
                dbx.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
                dbx.AddParameter("@Type", System.Data.SqlDbType.NVarChar, Type);
                dbx.ExecuteSql(sqlQuery);
            }
        }
    }
    #endregion




    #region thêm Tags
    protected void UpdateTags(string NameList, string Type, ref string TagIDList)
    {
        string _TagIDList = "";
        NameList = NameList.Trim();
        if (string.IsNullOrEmpty(NameList))
            return;

        string[] NameArr = NameList.Split(',');
        if (NameArr != null && NameArr.Length > 0)
        {
            foreach (string name in NameArr)
            {
                using (var dbx = SqlService.GetSqlService())
                {
                    string sqlQuery = string.Format("SELECT * FROM tblCategories Where Name=N'{0}' AND Moduls=N'%,{1},%'", name.Trim(), Type);
                    var ds = dbx.ExecuteSqlDataTable(sqlQuery);
                    if (ds == null || ds.Rows.Count == 0)
                    {
                        int TagID = 0;
                        AddTags(name, Type, ref TagID);
                        _TagIDList += TagID + ",";
                    }
                }
            }

            TagIDList = _TagIDList.Trim(',');
        }
    }

    protected void AddTags(string Name, string Type, ref int TagID)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            CacheUtility.PurgeCacheItems("tblCategories");

            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = "INSERT INTO tblCategories (ParentID, Name, Moduls) OUTPUT INSERTED.ID Values (@ParentID, @Name, @Type)";
                dbx.AddParameter("@ParentID", System.Data.SqlDbType.Int, 0);
                dbx.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
                dbx.AddParameter("@Type", System.Data.SqlDbType.NVarChar, Type);
                TagID = dbx.ExecuteSqlScalar<int>(sqlQuery, 0);
            }
        }
    }
    #endregion


    protected void UpdateBrand(string AttributesIDList, string CategoryNameList, string ID)
    {
        List<string> attrList = GenSitemap.GenAttribute(AttributesIDList, CategoryNameList);
        if (attrList.Count > 0)
        {
            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = @"UPDATE [dbo].[tblProducts] SET [Brand]=@Brand WHERE [ID] = @ID";
                db.AddParameter("@ID", System.Data.SqlDbType.Int, ID);
                db.AddParameter("@Brand", System.Data.SqlDbType.NVarChar, attrList[0].ToString());
                db.ExecuteSql(sqlQuery);
            }
        }
    }


    protected void UpdateGGShopping(int ID)
    {
        DataTable dtP = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", "ID=" + ID, "", 1, 1);
        if (Utils.CheckExist_DataTable(dtP))
        {
            DataRow drP = dtP.Rows[0];
            int CategoryID = 0;
            string[] cateList = ConvertUtility.ToString(drP["CategoryIDList"]).Trim(',').Split(',');
            if (cateList != null && cateList.Length > 0)
            {
                CategoryID = ConvertUtility.ToInt32(ConvertUtility.ToInt32(cateList[0]));
            }

            if (CategoryID > 0)
            {
                Stack<BreadCrumb> bcList = new Stack<BreadCrumb>();

                DataTable dt = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, LinkTypeMenuFlag, FriendlyUrl, Name, Link", string.Format("ID='{0}' AND {1}", CategoryID, Utils.CreateFilterHide), "Sort");
                if (Utils.CheckExist_DataTable(dt))
                {
                    dr = dt.Rows[0];

                    //add thêm thằng hiện tại
                    BreadCrumb bcCurrent = new BreadCrumb();
                    bcCurrent.Link = Utils.CreateCategoryLink(dr["LinkTypeMenuFlag"], dr["FriendlyUrl"], dr["Link"]);
                    bcCurrent.Name = ConvertUtility.ToString(dr["Name"]);
                    bcCurrent.ID = ConvertUtility.ToString(dr["ID"]);
                    bcList.Push(bcCurrent);


                    int countP = 0;
                    DataRow drRoot = dr;
                    int RootID = ConvertUtility.ToInt32(drRoot["ID"]);
                    do
                    {

                        if (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0)
                        {
                            countP = countP + 1;
                            DataTable dtRoot = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, LinkTypeMenuFlag, FriendlyUrl, Name, Link", string.Format("ID={0}", drRoot["ParentID"]), "Sort");
                            if (Utils.CheckExist_DataTable(dtRoot))
                            {
                                drRoot = dtRoot.Rows[0];
                                RootID = ConvertUtility.ToInt32(drRoot["ID"]);

                                BreadCrumb bc = new BreadCrumb();
                                bc.Link = Utils.CreateCategoryLink(drRoot["LinkTypeMenuFlag"], drRoot["FriendlyUrl"], drRoot["Link"]);
                                bc.Name = ConvertUtility.ToString(drRoot["Name"]);
                                bc.ID = ConvertUtility.ToString(drRoot["ID"]);
                                bcList.Push(bc);
                            }
                            else
                                break;
                        }
                    }
                    while (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0 && countP <= 5);
                }

                // Product type GoogleShopping

                StringBuilder sb = new StringBuilder();

                if (bcList != null && bcList.Count > 0)
                {
                    int countPT = 0;

                    foreach (BreadCrumb bc in bcList)
                    {
                        countPT++;
                        sb.Append(bc.Name);

                        if (countPT < bcList.Count)
                            sb.Append(" > ");
                    }
                }

                using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
                {
                    string sqlQuery = @"UPDATE [dbo].[tblProducts] SET [ProductType]=@ProductType WHERE [ID] = @ID";
                    db.AddParameter("@ID", System.Data.SqlDbType.NVarChar, drP["ID"].ToString());
                    db.AddParameter("@ProductType", System.Data.SqlDbType.NVarChar, sb.ToString());
                    db.ExecuteSql(sqlQuery);
                }
            }
        }
    }


    protected void UpdatePriceHistory(string ProductID)
    {
        decimal _oldPrice = ConvertUtility.ToDecimal(Request.Form["hdfOldPrice"]);
        decimal _oldPrice1 = ConvertUtility.ToDecimal(Request.Form["hdfOldPrice1"]);
        decimal _newPrice = ConvertUtility.ToDecimal(Utils.KillChars(Request.Form["price"]).Replace(",", "").Replace(".", ""));
        decimal _newPrice1 = ConvertUtility.ToDecimal(Utils.KillChars(Request.Form["price1"]).Replace(",", "").Replace(".", ""));

        if (_newPrice != _oldPrice)
        {
            string _newPriceString = ConvertUtility.ToString(_newPrice);
            string _newPrice1String = ConvertUtility.ToString(_newPrice1);
            hashtable["Name"] = Utils.KillChars(Request.Form["name"]);
            hashtable["ProductID"] = ProductID;
            hashtable["Price"] = Utils.KillChars(Request.Form["price"]).Replace(",", "").Replace(".", "");
            hashtable["Price1"] = Utils.KillChars(Request.Form["price1"]).Replace(",", "").Replace(".", "");

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = @"INSERT INTO [dbo].[tblPriceHistory] ([Name],[ProductID],[Price],[Price1],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@ProductID,@Price,@Price1,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";

                db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
                db.AddParameter("@ProductID", System.Data.SqlDbType.Int, hashtable["ProductID"].ToString());
                db.AddParameter("@Price", System.Data.SqlDbType.Money, hashtable["Price"]);
                db.AddParameter("@Price1", System.Data.SqlDbType.Money, hashtable["Price1"]);
                db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);
                db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, Page.User.Identity.Name);

                db.ExecuteSqlScalar<int>(sqlQuery, 0);
            }
        }
    }

}