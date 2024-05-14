using System;
using System.Collections.Generic;
using System.Data;
using Ebis.Utilities;
using System.Collections;
using MetaNET.DataHelper;

public partial class admin_Controls_ConfigUpdate : System.Web.UI.UserControl
{
    #region Variable
    public string arr_img_js;
    public DataRow dr;
    public Hashtable hashtable = new Hashtable();
    DatabaseHelper db1 = new DatabaseHelper();
    public bool IsUpdate = false;
    int outID = 0;
    public string jsonCategory = "";
    int ID = 1, IDCopy = 0;
    public string click_action, controlName, table = "tblConfigs";
    public string logo = C.NO_IMG_PATH, logoadmin = C.NO_IMG_PATH, image = C.NO_IMG_PATH, icon = C.NO_IMG_PATH;
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
            dr = db.NewRow(table);
        }

        using (var db = SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("SELECT Top 1 * FROM tblConfigs");
            var ds = db.ExecuteSqlDataTable(sqlQuery);
            if (ds.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(ds.Rows[0]["Logo"].ToString()))
                    logo = ds.Rows[0]["Logo"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["LogoAdmin"].ToString()))
                    logoadmin = ds.Rows[0]["LogoAdmin"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["Image"].ToString()))
                    image = ds.Rows[0]["Image"].ToString();
                if (!string.IsNullOrEmpty(ds.Rows[0]["Icon"].ToString()))
                    icon = ds.Rows[0]["Icon"].ToString();

                dr = ds.Rows[0];
                IsUpdate = true;
            }
        }
    }

    protected void UpdateDatabase()
    {
        click_action = Request.Form["done"];

        if (!String.IsNullOrEmpty(click_action) && (click_action == "saveandback" || click_action == "saveandcopy" || click_action == "saveandadd"))
        {

            hashtable["MetaTitle"] = Utils.KillChars(Request.Form["metatitle"]);
            hashtable["MetaKeyword"] = Utils.KillChars(Request.Form["metakeyword"]);
            hashtable["MetaDescription"] = Utils.KillChars(Request.Form["metadescription"]);
            hashtable["SiteName"] = Utils.KillChars(Request.Form["sitename"]);
            hashtable["SiteUrl"] = Utils.KillChars(Request.Form["siteurl"]);
            hashtable["RemoteUrl"] = Utils.KillChars(Request.Form["remoteurl"]);
            hashtable["Hotline"] = Utils.KillChars(Request.Form["hotline"]);
            hashtable["HotlineBH"] = Utils.KillChars(Request.Form["hotlinebh"]);
            hashtable["Address"] = Utils.KillChars(Request.Form["address"]);
            hashtable["Email"] = Utils.KillChars(Request.Form["email"]);
            hashtable["Logo"] = Utils.KillChars(Request.Form["logo"]);
            hashtable["LogoFooter"] = Utils.KillChars(Request.Form["logofooter"]);
            hashtable["Icon"] = Utils.KillChars(Request.Form["icon"]);
            hashtable["IconAdmin"] = Utils.KillChars(Request.Form["iconadmin"]);
            hashtable["LogoAdmin"] = Utils.KillChars(Request.Form["logoadmin"]);
            hashtable["Slogan"] = Utils.KillChars(Request.Form["slogan"]);
            hashtable["Image"] = Utils.KillChars(Request.Form["image"]);
            hashtable["ProductNumberInCategory"] = Utils.KillChars(Request.Form["productnumberincategory"]);
            hashtable["Color"] = Utils.KillChars(Request.Form["color"]);
            hashtable["Style"] = Utils.KillChars(Request.Form["style"]);
            hashtable["HeaderText"] = Utils.KillChars(Request.Form["headertext"]);
            hashtable["FooterText"] = Utils.KillChars(Request.Form["footertext"]);
            hashtable["Footer1"] = Utils.KillChars(Request.Form["footer1"]);
            hashtable["Footer2"] = Utils.KillChars(Request.Form["footer2"]);
            hashtable["Footer_Address"] = Utils.KillChars(Request.Form["footer_address"]);
            hashtable["Footer_Phone"] = Utils.KillChars(Request.Form["footer_phone"]);
            hashtable["Footer_Social"] = Utils.KillChars(Request.Form["footer_social"]);
            hashtable["CodeHeader"] = Utils.KillChars(Request.Form["codeheader"]);
            hashtable["CodeBody"] = Utils.KillChars(Request.Form["codebody"]);
            hashtable["TextContact"] = Utils.KillChars(Request.Form["textcontact"]);
            hashtable["MapLocation"] = Utils.KillChars(Request.Form["maplocation"]);
            hashtable["MapLocation1"] = Utils.KillChars(Request.Form["maplocation1"]);
            hashtable["MapLocation2"] = Utils.KillChars(Request.Form["maplocation2"]);
            hashtable["Email_Display"] = Utils.KillChars(Request.Form["email_display"]);
            hashtable["Email_Receiving"] = Utils.KillChars(Request.Form["email_receiving"]);
            hashtable["Mail_SMTP"] = Utils.KillChars(Request.Form["mail_smtp"]);
            hashtable["Mail_Username"] = Utils.KillChars(Request.Form["mail_username"]);
            hashtable["MailPassword"] = Utils.KillChars(Request.Form["mailpassword"]);
            hashtable["MailPort"] = Utils.KillChars(Request.Form["mailport"]);
            hashtable["Mail_SSL"] = Utils.KillChars(Request.Form["mail_ssl"]);
            hashtable["Mail_SecurityMethod"] = Utils.KillChars(Request.Form["mail_securitymethod"]);
            hashtable["CacheTimeMinutes"] = Utils.KillChars(Request.Form["cachetimeminutes"]);
            hashtable["RowsPagingProduct"] = Utils.KillChars(Request.Form["rowspagingproduct"]);
            hashtable["RowsPagingArticle"] = Utils.KillChars(Request.Form["rowspagingarticle"]);
            hashtable["SortProduct"] = Utils.KillChars(Request.Form["sortproduct"]);
            hashtable["SortProductHome"] = Utils.KillChars(Request.Form["sortproducthome"]);
            hashtable["SortArticle"] = Utils.KillChars(Request.Form["sortarticle"]);
            hashtable["OAZalo"] = Utils.KillChars(Request.Form["oazalo"]);
            hashtable["FacebookID"] = Utils.KillChars(Request.Form["facebookid"]);
            hashtable["MapPage"] = Utils.KillChars(Request.Form["mappage"]);
            hashtable["SchemaLatitude"] = Utils.KillChars(Request.Form["schemalatitude"]);
            hashtable["SchemaLongitude"] = Utils.KillChars(Request.Form["schemalongitude"]);
            hashtable["SchemaSameAs"] = Utils.KillChars(Request.Form["schemasameas"]);
            hashtable["SchemaStreetAddress"] = Utils.KillChars(Request.Form["schemastreetaddress"]);
            hashtable["SchemaAddressLocality"] = Utils.KillChars(Request.Form["schemaaddresslocality"]);
            hashtable["SchemaPostalCode"] = Utils.KillChars(Request.Form["schemapostalcode"]);
            hashtable["SchemaTelephone"] = Utils.KillChars(Request.Form["schematelephone"]);
            hashtable["SchemaBestRating"] = Utils.KillChars(Request.Form["schemabestrating"]);
            hashtable["SchemaRatingValue"] = Utils.KillChars(Request.Form["schemaratingvalue"]);
            hashtable["SchemaRatingCount"] = Utils.KillChars(Request.Form["schemaratingcount"]);
            hashtable["AdressFunction"] = Utils.KillChars(Request.Form["adressfunction"]);
            hashtable["ContactFunction"] = Utils.KillChars(Request.Form["contactfunction"]);
            hashtable["TraGop1"] = Utils.KillChars(Request.Form["TraGop1"]);
            hashtable["TraGop2"] = Utils.KillChars(Request.Form["TraGop2"]);
            hashtable["TextHome1"] = Utils.KillChars(Request.Form["TextHome1"]);
            hashtable["TextHome2"] = Utils.KillChars(Request.Form["TextHome2"]);
            hashtable["TextHome3"] = Utils.KillChars(Request.Form["TextHome3"]);
            hashtable["TextHome4"] = Utils.KillChars(Request.Form["TextHome4"]);
            hashtable["TextHome5"] = Utils.KillChars(Request.Form["TextHome5"]);
            hashtable["BoCongThuongLink"] = Utils.KillChars(Request.Form["bocongthuonglink"]);
            CacheUtility.PurgeCacheItems(table);


            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Empty;
                if (IsUpdate)
                {
                    sqlQuery = @"UPDATE [dbo].[tblConfigs] SET [MetaTitle]=@MetaTitle, [MetaKeyword]=@MetaKeyword, [MetaDescription]=@MetaDescription, [SiteName]=@SiteName, [SiteUrl]=@SiteUrl, [Hotline]=@Hotline, [Address]=@Address, [Email]=@Email, [Logo]=@Logo, [Icon]=@Icon, [IconAdmin]=@IconAdmin, [Slogan]=@Slogan, [Image]=@Image, [Color]=@Color, [Style]=@Style, [HeaderText]=@HeaderText, [FooterText]=@FooterText, [Footer_Address]=@Footer_Address, [Footer_Phone]=@Footer_Phone, [Footer_Social]=@Footer_Social, [Footer1]=@Footer1, [Footer2]=@Footer2, [CodeHeader]=@CodeHeader, [CodeBody]=@CodeBody, [TextContact]=@TextContact, [MapLocation]=@MapLocation, [MapLocation1]=@MapLocation1, [MapLocation2]=@MapLocation2, [Email_Display]=@Email_Display, [Email_Receiving]=@Email_Receiving, [RemoteUrl]=@RemoteUrl, [LogoFooter]=@LogoFooter, [LogoAdmin]=@LogoAdmin, [Mail_SMTP]=@Mail_SMTP, [Mail_Username]=@Mail_Username, [MailPassword]=@MailPassword, [MailPort]=@MailPort, [Mail_SSL]=@Mail_SSL, [Mail_SecurityMethod]=@Mail_SecurityMethod, [CacheTimeMinutes]=@CacheTimeMinutes, [RowsPagingProduct]=@RowsPagingProduct, [RowsPagingArticle]=@RowsPagingArticle, [SortProduct]=@SortProduct, [SortProductHome]=@SortProductHome, [SortArticle]=@SortArticle, [OAZalo]=@OAZalo, [FacebookID]=@FacebookID, [HotlineBH]=@HotlineBH, [MapPage]=@MapPage, [SchemaLatitude]=@SchemaLatitude, [SchemaLongitude]=@SchemaLongitude, [SchemaSameAs]=@SchemaSameAs, [SchemaStreetAddress]=@SchemaStreetAddress, [SchemaAddressLocality]=@SchemaAddressLocality, [SchemaPostalCode]=@SchemaPostalCode, [SchemaTelephone]=@SchemaTelephone, [SchemaBestRating]=@SchemaBestRating, [SchemaRatingValue]=@SchemaRatingValue, [SchemaRatingCount]=@SchemaRatingCount, [AdressFunction]=@AdressFunction, [ContactFunction]=@ContactFunction, [TraGop1]=@TraGop1, [TraGop2]=@TraGop2, [TextHome1]=@TextHome1, [TextHome2]=@TextHome2, [TextHome3]=@TextHome3, [TextHome4]=@TextHome4, [TextHome5]=@TextHome5, [BoCongThuongLink]=@BoCongThuongLink WHERE [ID] = @ID";
                }
                else
                {
                    sqlQuery = @"INSERT INTO [dbo].[tblConfigs] ([MetaTitle],[MetaKeyword],[MetaDescription],[SiteName],[SiteUrl],[Hotline],[Address],[Email],[Logo],[Icon],[IconAdmin],[Slogan],[Image],[Color],[Style],[HeaderText],[FooterText],[Footer_Address],[Footer_Phone],[Footer_Social],[Footer1],[Footer2],[CodeHeader],[CodeBody],[TextContact],[MapLocation],[MapLocation1],[MapLocation2],[Email_Display],[Email_Receiving],[RemoteUrl],[LogoFooter],[LogoAdmin],[Mail_SMTP],[Mail_Username],[MailPassword],[MailPort],[Mail_SSL],[Mail_SecurityMethod],[CacheTimeMinutes],[RowsPagingProduct],[RowsPagingArticle],[SortProduct],[SortProductHome],[SortArticle],[OAZalo],[FacebookID],[HotlineBH],[MapPage],[SchemaLatitude],[SchemaLongitude],[SchemaSameAs],[SchemaStreetAddress],[SchemaAddressLocality],[SchemaPostalCode],[SchemaTelephone],[SchemaBestRating],[SchemaRatingValue],[SchemaRatingCount],[AdressFunction],[ContactFunction],[TraGop1],[TraGop2],[TextHome1],[TextHome2],[TextHome3],[TextHome4],[TextHome5],[BoCongThuongLink]) OUTPUT INSERTED.ID VALUES (@MetaTitle,@MetaKeyword,@MetaDescription,@SiteName,@SiteUrl,@Hotline,@Address,@Email,@Logo,@Icon,@IconAdmin,@Slogan,@Image,@Color,@Style,@HeaderText,@FooterText,@Footer_Address,@Footer_Phone,@Footer_Social,@Footer1,@Footer2,@CodeHeader,@CodeBody,@TextContact,@MapLocation,@MapLocation1,@MapLocation2,@Email_Display,@Email_Receiving,@RemoteUrl,@LogoFooter,@LogoAdmin,@Mail_SMTP,@Mail_Username,@MailPassword,@MailPort,@Mail_SSL,@Mail_SecurityMethod,@CacheTimeMinutes,@RowsPagingProduct,@RowsPagingArticle,@SortProduct,@SortProductHome,@SortArticle,@OAZalo,@FacebookID,@HotlineBH,@MapPage,@SchemaLatitude,@SchemaLongitude,@SchemaSameAs,@SchemaStreetAddress,@SchemaAddressLocality,@SchemaPostalCode,@SchemaTelephone,@SchemaBestRating,@SchemaRatingValue,@SchemaRatingCount,@AdressFunction,@ContactFunction,@TraGop1,@TraGop2,@TextHome1,@TextHome2,@TextHome3,@TextHome4,@TextHome5,@BoCongThuongLink)";
                }

                db.AddParameter("@MetaTitle", System.Data.SqlDbType.NVarChar, hashtable["MetaTitle"].ToString());
                db.AddParameter("@MetaKeyword", System.Data.SqlDbType.NVarChar, hashtable["MetaKeyword"].ToString());
                db.AddParameter("@MetaDescription", System.Data.SqlDbType.NVarChar, hashtable["MetaDescription"].ToString());
                db.AddParameter("@SiteName", System.Data.SqlDbType.NVarChar, hashtable["SiteName"].ToString());
                db.AddParameter("@SiteUrl", System.Data.SqlDbType.NVarChar, hashtable["SiteUrl"].ToString());
                db.AddParameter("@Hotline", System.Data.SqlDbType.NVarChar, hashtable["Hotline"].ToString());
                db.AddParameter("@HotlineBH", System.Data.SqlDbType.NVarChar, hashtable["HotlineBH"].ToString());
                db.AddParameter("@Address", System.Data.SqlDbType.NVarChar, hashtable["Address"].ToString());
                db.AddParameter("@Email", System.Data.SqlDbType.NVarChar, hashtable["Email"].ToString());
                db.AddParameter("@Logo", System.Data.SqlDbType.NVarChar, hashtable["Logo"].ToString());
                db.AddParameter("@Icon", System.Data.SqlDbType.NVarChar, hashtable["Icon"].ToString());
                db.AddParameter("@IconAdmin", System.Data.SqlDbType.NVarChar, hashtable["IconAdmin"].ToString());
                db.AddParameter("@Slogan", System.Data.SqlDbType.NVarChar, hashtable["Slogan"].ToString());
                db.AddParameter("@Image", System.Data.SqlDbType.NVarChar, hashtable["Image"].ToString());
                db.AddParameter("@Color", System.Data.SqlDbType.NVarChar, hashtable["Color"].ToString());
                db.AddParameter("@Style", System.Data.SqlDbType.NVarChar, hashtable["Style"].ToString());
                db.AddParameter("@HeaderText", System.Data.SqlDbType.NVarChar, hashtable["HeaderText"].ToString());
                db.AddParameter("@FooterText", System.Data.SqlDbType.NVarChar, hashtable["FooterText"].ToString());
                db.AddParameter("@Footer_Address", System.Data.SqlDbType.NVarChar, hashtable["Footer_Address"].ToString());
                db.AddParameter("@Footer_Phone", System.Data.SqlDbType.NVarChar, hashtable["Footer_Phone"].ToString());
                db.AddParameter("@Footer_Social", System.Data.SqlDbType.NVarChar, hashtable["Footer_Social"].ToString());
                db.AddParameter("@CodeHeader", System.Data.SqlDbType.NVarChar, hashtable["CodeHeader"].ToString());
                db.AddParameter("@CodeBody", System.Data.SqlDbType.NVarChar, hashtable["CodeBody"].ToString());
                db.AddParameter("@TextContact", System.Data.SqlDbType.NVarChar, hashtable["TextContact"].ToString());
                db.AddParameter("@MapLocation", System.Data.SqlDbType.NVarChar, hashtable["MapLocation"].ToString());
                db.AddParameter("@MapLocation1", System.Data.SqlDbType.NVarChar, hashtable["MapLocation1"].ToString());
                db.AddParameter("@MapLocation2", System.Data.SqlDbType.NVarChar, hashtable["MapLocation2"].ToString());
                db.AddParameter("@Email_Display", System.Data.SqlDbType.NVarChar, hashtable["Email_Display"].ToString());
                db.AddParameter("@Email_Receiving", System.Data.SqlDbType.NVarChar, hashtable["Email_Receiving"].ToString());
                db.AddParameter("@RemoteUrl", System.Data.SqlDbType.NVarChar, hashtable["RemoteUrl"].ToString());
                db.AddParameter("@LogoFooter", System.Data.SqlDbType.NVarChar, hashtable["LogoFooter"].ToString());
                db.AddParameter("@LogoAdmin", System.Data.SqlDbType.NVarChar, hashtable["LogoAdmin"].ToString());
                db.AddParameter("@Mail_SMTP", System.Data.SqlDbType.NVarChar, hashtable["Mail_SMTP"].ToString());
                db.AddParameter("@Mail_Username", System.Data.SqlDbType.NVarChar, hashtable["Mail_Username"].ToString());
                db.AddParameter("@MailPassword", System.Data.SqlDbType.NVarChar, hashtable["MailPassword"].ToString());
                db.AddParameter("@MailPort", System.Data.SqlDbType.NVarChar, hashtable["MailPort"].ToString());
                db.AddParameter("@Mail_SSL", System.Data.SqlDbType.NVarChar, hashtable["Mail_SSL"].ToString());
                db.AddParameter("@Mail_SecurityMethod", System.Data.SqlDbType.NVarChar, hashtable["Mail_SecurityMethod"].ToString());
                db.AddParameter("@CacheTimeMinutes", System.Data.SqlDbType.NVarChar, hashtable["CacheTimeMinutes"].ToString());
                db.AddParameter("@RowsPagingProduct", System.Data.SqlDbType.NVarChar, hashtable["RowsPagingProduct"].ToString());
                db.AddParameter("@RowsPagingArticle", System.Data.SqlDbType.NVarChar, hashtable["RowsPagingArticle"].ToString());
                db.AddParameter("@SortProduct", System.Data.SqlDbType.NVarChar, hashtable["SortProduct"].ToString());
                db.AddParameter("@SortProductHome", System.Data.SqlDbType.NVarChar, hashtable["SortProductHome"].ToString());
                db.AddParameter("@SortArticle", System.Data.SqlDbType.NVarChar, hashtable["SortArticle"].ToString());
                db.AddParameter("@OAZalo", System.Data.SqlDbType.NVarChar, hashtable["OAZalo"].ToString());
                db.AddParameter("@FacebookID", System.Data.SqlDbType.NVarChar, hashtable["FacebookID"].ToString());
                db.AddParameter("@MapPage", System.Data.SqlDbType.NVarChar, hashtable["MapPage"].ToString());
                db.AddParameter("@SchemaLatitude", System.Data.SqlDbType.NVarChar, hashtable["SchemaLatitude"].ToString());
                db.AddParameter("@SchemaLongitude", System.Data.SqlDbType.NVarChar, hashtable["SchemaLongitude"].ToString());
                db.AddParameter("@SchemaSameAs", System.Data.SqlDbType.NVarChar, hashtable["SchemaSameAs"].ToString());
                db.AddParameter("@SchemaStreetAddress", System.Data.SqlDbType.NVarChar, hashtable["SchemaStreetAddress"].ToString());
                db.AddParameter("@SchemaAddressLocality", System.Data.SqlDbType.NVarChar, hashtable["SchemaAddressLocality"].ToString());
                db.AddParameter("@SchemaPostalCode", System.Data.SqlDbType.NVarChar, hashtable["SchemaPostalCode"].ToString());
                db.AddParameter("@SchemaTelephone", System.Data.SqlDbType.NVarChar, hashtable["SchemaTelephone"].ToString());
                db.AddParameter("@SchemaBestRating", System.Data.SqlDbType.Int, hashtable["SchemaBestRating"].ToString());
                db.AddParameter("@SchemaRatingValue", System.Data.SqlDbType.Int, hashtable["SchemaRatingValue"].ToString());
                db.AddParameter("@SchemaRatingCount", System.Data.SqlDbType.Int, hashtable["SchemaRatingCount"].ToString());
                db.AddParameter("@AdressFunction", System.Data.SqlDbType.NVarChar, hashtable["AdressFunction"].ToString());
                db.AddParameter("@ContactFunction", System.Data.SqlDbType.NVarChar, hashtable["ContactFunction"].ToString());
                db.AddParameter("@TraGop1", System.Data.SqlDbType.NVarChar, hashtable["TraGop1"].ToString());
                db.AddParameter("@TraGop2", System.Data.SqlDbType.NVarChar, hashtable["TraGop2"].ToString());
                db.AddParameter("@Footer1", System.Data.SqlDbType.NVarChar, hashtable["Footer1"].ToString());
                db.AddParameter("@Footer2", System.Data.SqlDbType.NVarChar, hashtable["Footer2"].ToString());
                db.AddParameter("@TextHome1", System.Data.SqlDbType.NVarChar, hashtable["TextHome1"].ToString());
                db.AddParameter("@TextHome2", System.Data.SqlDbType.NVarChar, hashtable["TextHome2"].ToString());
                db.AddParameter("@TextHome3", System.Data.SqlDbType.NVarChar, hashtable["TextHome3"].ToString());
                db.AddParameter("@TextHome4", System.Data.SqlDbType.NVarChar, hashtable["TextHome4"].ToString());
                db.AddParameter("@TextHome5", System.Data.SqlDbType.NVarChar, hashtable["TextHome5"].ToString());
                db.AddParameter("@BoCongThuongLink", System.Data.SqlDbType.NVarChar, hashtable["BoCongThuongLink"].ToString());

                if (IsUpdate)
                {
                    db.AddParameter("@ID", System.Data.SqlDbType.Int, 1);
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


                BindData();
            }

            if (!string.IsNullOrEmpty(Request.Form["sitemap"]) && Request.Form["sitemap"] == "on")
                GenSitemap.SitemapUpdate();
            if (!string.IsNullOrEmpty(Request.Form["ggshopping"]) && Request.Form["ggshopping"] == "on")
                GenSitemap.GenGoogleShopping(System.Web.HttpContext.Current, "hoanghai");
        }
    }
}