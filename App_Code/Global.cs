using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Global
/// </summary>
public static class Global
{
    public static readonly DatabaseHelper db = new DatabaseHelper();


    public static string Lang_validate = "vn";

    //public static Administrator objAdmin { get; set; }

    private static System.Data.DataRow _dr;
    public static System.Data.DataRow rowAdmin
    {
        get
        {
            return _dr;
        }
        set
        {
            _dr = value;
        }

    }
    public static DataTable dtLanguage()
    {
        return db.ExecuteDataTable("SELECT IDLanguage,Title,images,Code,Ordinal,CShow_Admin FROM dbo.Language WHERE CShow =1");
    }
    //public static string administrator(int idadmin)
    //{
    //    string username = "root";
    //    using (DataDataContext ctx = new DataDataContext())
    //    {
    //        Administrator obj = ctx.Administrators.FirstOrDefault(x => x.IDAdmin == idadmin);
    //        if (obj != null)
    //            username = obj.UserName;
    //    }
    //    return username;
    //}
    //public static List<ComboItem> permission_menu { get; set; }

    //public static string Language
    //{
    //    get
    //    {
    //        if (HttpContext.Current.Session["alogin_" + Config.SESSION_KEY + "language"] != null)
    //            return HttpContext.Current.Session["alogin_" + Config.SESSION_KEY + "language"].ToString();
    //        else
    //            return C.DEFAULT_LANGUAGE;
    //    }
    //}

    //public static string Configure(string MKey, string language = "")
    //{
    //    if (Utils.IsNullOrEmpty(language))
    //        language = Global.Language;
    //    if (!CacheUtitl.CheckCache("ConfigureWebiste_"+language) )
    //    {

           
    //        DataTable dt = db.ExecuteDataTable("SELECT idConfigure,mkey,title,value_" + language + " as value FROM Configure");
    //        CacheUtitl.Set<DataTable>("ConfigureWebiste_" + language, dt);
    //        DataRow row = dt.AsEnumerable().Where(r => r.Field<string>("mkey") == MKey).FirstOrDefault();
    //        if (row != null)
    //            return row["value"].ToString();
    //        return "";
    //    }
    //    else
    //    {
    //        DataTable dt = CacheUtitl.Get<DataTable>("ConfigureWebiste_" + language);
    //        if (dt != null)
    //        {
    //            DataRow row = dt.AsEnumerable().Where(r => r.Field<string>("mkey") == MKey).FirstOrDefault();
    //            if (row != null)
    //                return row["value"].ToString();
    //            return "";
    //        }
    //        return "";
    //    }
    //}
    //public static DataRow drWeb
    //{
    //    get
    //    {
    //        DataRow drWeb;
    //        if (!Utils.IsNullOrEmpty(HttpContext.Current.Session["url"]))
    //        {
    //            drWeb = Global.db.ExecuteDataRow("SELECT TOP(1)* FROM  dbo.Website WHERE Title LIKE	'%" + Utils.GetDomain(HttpContext.Current.Session["url"].ToString()) + "%' and status='1'");
    //            if (drWeb != null)
    //                return drWeb;
    //            else
    //            { drWeb = Global.db.ExecuteDataRow("SELECT TOP(1)* FROM  dbo.Website WHERE IDWebsite='1'"); return drWeb; }
    //        }
    //        else
    //        { drWeb = Global.db.ExecuteDataRow("SELECT TOP(1)* FROM  dbo.Website WHERE IDWebsite='1'"); return drWeb; }
    //    }

    //}

    public static DataRow Member
    {
        get
        {

            if (HttpContext.Current.Session["alogin_" + Config.SESSION_KEY + "idmember"] != null)
            {

                DataRow drMember = Global.db.ExecuteDataRow("SELECT * FROM member where idmember='" + HttpContext.Current.Session["alogin_" + Config.SESSION_KEY + "idmember"] + "' and status='1'");
                return drMember;
            }
            else return null;
            
        }
    }
}