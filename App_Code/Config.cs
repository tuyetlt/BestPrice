using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for Config
/// </summary>
public class Config
{
    //Email
    public static String _SMTP_SERVER = "";
    public static String _SMTP_USERNAME = "";
    public static String _SMTP_PASSWORD = "";
    public static String _SMTP_SENDER = "";
    public static String _SMTP_SSL = "";
    public static String _SMTP_PORT = "";


    //EMAIL SEND

    public static String SMTP_SERVER
    {
        get { return _SMTP_SERVER = ConfigurationSettings.AppSettings["SMTP_SERVER"].ToString(); }
        set { _SMTP_SERVER = value; }
    }

    public static String SMTP_USERNAME
    {
        get { return _SMTP_USERNAME = ConfigurationSettings.AppSettings["SMTP_USERNAME"].ToString(); }
        set { _SMTP_USERNAME = value; }
    }

    public static String SMTP_PASSWORD
    {
        get { return _SMTP_PASSWORD = ConfigurationSettings.AppSettings["SMTP_PASSWORD"].ToString(); }
        set { _SMTP_PASSWORD = value; }
    }

    public static String SMTP_SENDER
    {
        get { return _SMTP_SENDER = ConfigurationSettings.AppSettings["SMTP_SENDER"].ToString(); }
        set { _SMTP_SENDER = value; }
    }
    public static String SMTP_PORT
    {
        get { return _SMTP_PORT = ConfigurationSettings.AppSettings["SMTP_PORT"].ToString(); }
        set { _SMTP_PORT = value; }
    }
    public static String SMTP_SSL
    {
        get { return _SMTP_SSL = ConfigurationSettings.AppSettings["SMTP_SSL"].ToString(); }
        set { _SMTP_SSL = value; }
    }



    public static string ROOT_URL
    {
        get
        {
            string ret = ConfigurationSettings.AppSettings["ROOT_URL"].ToString();
            string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            if (!domainName.Contains("dienmayhoanghai.vn"))
            {
                ret = domainName;
            }

            return ret;
        }
    }
    public static String SITE_NAME
    {
        get { return ConfigurationSettings.AppSettings["SITE_NAME"].ToString(); }
    }
    public static int MAX_ITEM_MENU
    {
        get { return ConvertUtility.ToInt32(ConfigurationSettings.AppSettings["MAX_ITEM_MENU"].ToString()); }
    }
    public static int MAX_ITEM_CATEGORY_HOME
    {
        get { return ConvertUtility.ToInt32(ConfigurationSettings.AppSettings["MAX_ITEM_CATEGORY_HOME"].ToString()); }
    }

    public static String DOMAIN
    {
        get
        {
            return ConfigurationSettings.AppSettings["ROOT_URL"].ToString().Replace("http://", "").Replace("https://", "");
        }
    }


    public static String MAIN_URL
    {
        get { return ConfigurationSettings.AppSettings["MAIN_URL"].ToString(); }
    }

    public static String CATEGORY_TABLE
    {
        get { return ConfigurationSettings.AppSettings["CATEGORY_TABLE"].ToString(); }
    }

    public static String PRODUCT_TABLE
    {
        get { return ConfigurationSettings.AppSettings["PRODUCT_TABLE"].ToString(); }
    }

    public static String ARTICLE_TABLE
    {
        get { return ConfigurationSettings.AppSettings["ARTICLE_TABLE"].ToString(); }
    }

    public static String BANNER_TABLE
    {
        get { return ConfigurationSettings.AppSettings["BANNER_TABLE"].ToString(); }
    }



    public static String CACHE_TIME_MINUTES
    {
        get { return ConfigurationSettings.AppSettings["CACHE_TIME_MINUTES"].ToString(); }
    }

    public static String MAX_REQUEST_IN_MINUTES
    {
        get { return ConfigurationSettings.AppSettings["MAX_REQUEST_IN_MINUTES"].ToString(); }
    }

    public static int ROWS_PRODUCTCATEGORY { get { return Int16.Parse(ConfigurationSettings.AppSettings["ROWS_PRODUCTCATEGORY"].ToString()); } }
    public static string SORT_PRODUCTCATEGORY { get { return ConfigurationSettings.AppSettings["SORT_PRODUCTCATEGORY"].ToString(); } }


    public static string SESSION_KEY { get { return ConfigurationSettings.AppSettings["SESSION_KEY"].ToString(); } }
    public static string COOKIE_KEY { get { return ConfigurationSettings.AppSettings["COOKIE_KEY"].ToString(); } }
    public static string ROOT_IMAGE_URL { get { return ConfigurationSettings.AppSettings["ROOT_IMAGE_URL"].ToString(); } }
    public static string UPLOAD_DIR { get { return ConfigurationSettings.AppSettings["UPLOAD_DIR"].ToString(); } }
    public static string PAGING_ADMIN { get { return ConfigurationSettings.AppSettings["PAGING_ADMIN"].ToString(); } }
    public static string NO_IMG_PATH { get { return ConfigurationSettings.AppSettings["NO_IMG_PATH"].ToString(); } }



    public static string DS { get { return ConfigurationSettings.AppSettings["DS"].ToString(); } }

    public static string TEMPLATE_NAME { get { return ConfigurationSettings.AppSettings["TEMPLATE_NAME"].ToString(); } }

    public static string TEMPLATE_PATH { get { return String.Concat("templates", DS, TEMPLATE_NAME); } }

    public static string TEMPLATE_PATH_FULL
    {
        get
        {
            return String.Concat(C.ROOT_URL, C.DS, C.TEMPLATE_PATH, C.DS);

        }
    }

    public static string SEO_EXTENSION { get { return ConfigurationSettings.AppSettings["SEO_EXTENSION"].ToString(); } }


    public static string NganLuong_Url { get { return ConfigurationSettings.AppSettings["NganLuong_Url"].ToString(); } }
    public static string VNP_SANDBOX_URL { get { return ConfigurationSettings.AppSettings["VNP_SANDBOX_URL"].ToString(); } }
    public static string VNP_RETURN_URL { get { return ConfigurationSettings.AppSettings["VNP_RETURN_URL"].ToString(); } }
    public static string VNPAY_API_URL { get { return ConfigurationSettings.AppSettings["VNPAY_API_URL"].ToString(); } }
    public static string VNP_TMNCODE { get { return ConfigurationSettings.AppSettings["VNP_TMNCODE"].ToString(); } }
    public static string VNP_HASHSECRET { get { return ConfigurationSettings.AppSettings["VNP_HASHSECRET"].ToString(); } }
    public static string NganLuong_Merchant_id { get { return ConfigurationSettings.AppSettings["NganLuong_Merchant_id"].ToString(); } }
    public static string NganLuong_Merchant_password { get { return ConfigurationSettings.AppSettings["NganLuong_Merchant_password"].ToString(); } }
    public static string NganLuong_Receiver_email { get { return ConfigurationSettings.AppSettings["NganLuong_Receiver_email"].ToString(); } }
    public static string GoogleLogin_ClientID { get { return ConfigurationSettings.AppSettings["GoogleLogin_ClientID"].ToString(); } }
    public static bool SandBoxMode { get { return Ebis.Utilities.ConvertUtility.ToBoolean(ConfigurationSettings.AppSettings["SANDBOX"]); } }



    public Config()
    {

    }
}
public class C : Config
{

}