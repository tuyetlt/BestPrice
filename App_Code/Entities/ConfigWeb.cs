using System;
using System.Collections;
using System.Data;
using MetaNET.DataHelper;

public class ConfigWeb
{



    public static string MetaTitle
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MetaTitle", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MetaKeyword
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MetaKeyword", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MetaDescription
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MetaDescription", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SiteName
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SiteName", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SiteUrl
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SiteUrl", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Hotline
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Hotline", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Address
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Address", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Email
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Email", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Logo
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Logo", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Icon
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Icon", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string IconAdmin
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "IconAdmin", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Slogan
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Slogan", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Image
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Image", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Color
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Color", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Style
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Style", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string HeaderText
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "HeaderText", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string FooterText
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "FooterText", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Footer_Address
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Footer_Address", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Footer_Phone
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Footer_Phone", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Footer_Social
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Footer_Social", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Footer1
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Footer1", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Footer2
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Footer2", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string CodeHeader
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "CodeHeader", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string CodeBody
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "CodeBody", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TextContact
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextContact", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MapLocation
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MapLocation", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MapLocation1
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MapLocation1", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MapLocation2
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MapLocation2", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Email_Display
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Email_Display", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Email_Receiving
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Email_Receiving", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string RemoteUrl
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "RemoteUrl", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string LogoFooter
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "LogoFooter", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string LogoAdmin
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "LogoAdmin", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Mail_SMTP
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Mail_SMTP", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Mail_Username
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Mail_Username", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MailPassword
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MailPassword", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MailPort
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MailPort", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Mail_SSL
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Mail_SSL", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string Mail_SecurityMethod
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "Mail_SecurityMethod", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string CacheTimeMinutes
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "CacheTimeMinutes", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string RowsPagingProduct
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "RowsPagingProduct", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string RowsPagingArticle
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "RowsPagingArticle", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SortProduct
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SortProduct", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SortProductHome
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SortProductHome", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SortArticle
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SortArticle", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string OAZalo
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "OAZalo", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string FacebookID
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "FacebookID", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string HotlineBH
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "HotlineBH", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string MapPage
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "MapPage", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaLatitude
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaLatitude", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaLongitude
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaLongitude", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaSameAs
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaSameAs", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaStreetAddress
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaStreetAddress", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaAddressLocality
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaAddressLocality", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaPostalCode
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaPostalCode", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaTelephone
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaTelephone", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaBestRating
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaBestRating", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaRatingValue
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaRatingValue", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string SchemaRatingCount
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "SchemaRatingCount", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string AdressFunction
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "AdressFunction", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string ContactFunction
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "ContactFunction", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TraGop1
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TraGop1", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TraGop2
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TraGop2", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TextHome1
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome1", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TextHome2
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome2", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TextHome3
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome3", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TextHome4
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome4", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string TextHome5
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome5", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }
    public static string BoCongThuongLink
    {
        get
        {
            string strReturn = string.Empty;
            DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "BoCongThuongLink", "");
            if (dt != null && dt.Rows.Count > 0)
                strReturn = dt.Rows[0][0].ToString();
            return strReturn;
        }
    }


}


