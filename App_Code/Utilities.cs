using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Drawing;
using System.Reflection;
using log4net;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
public class Utilities
{
    public Utilities()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

public class ConvertUtility
{
    public static List<U> ConvertList<U, T>(List<T> listObject)
    {
        if (listObject == null) return null;
        List<U> uList = new List<U>();
        foreach (T t in listObject)
        {
            if (t == null) continue;
            U uDefault = default(U);
            U u = ConvertObject<U>(t, uDefault);
            uList.Add(u);
        }
        return uList;
    }

    public static object ConvertByType(Type type, object objConvert)
    {
        switch (type.Name)
        {
            case "String":
                return ConvertUtility.ToString(objConvert);

            case "Int":
                return ConvertUtility.ToInt32(objConvert);

            case "Double":
                return ConvertUtility.ToDouble(objConvert);

            case "DateTime":
                return ConvertUtility.ToDateTime(objConvert);

            case "Decimal":
                return ConvertUtility.ToDecimal(objConvert);

            case "Float":
                return ConvertUtility.ToInt64(objConvert);

            case "Guid":
                return ConvertUtility.ToGuid(objConvert.ToString());

            case "Bool":
                return ConvertUtility.ToBoolean(objConvert);

            case "Boolean":
                return ConvertUtility.ToBoolean(objConvert);

            case "Int16":
                return ConvertUtility.ToInt16(objConvert);

            case "Int32":
                return ConvertUtility.ToInt32(objConvert);

            case "Int64":
                return ConvertUtility.ToInt64(objConvert);

            case "Byte":
                return ConvertUtility.ToByte(objConvert);

        }
        return null;
    }

    public static T ConvertObject<T>(object t, T defaultValue)
    {
        T obj = default(T);
        try
        {
            obj = (T)t;
        }
        catch (Exception ex)
        {
            return defaultValue;
        }
        return obj;
    }

    public static Guid ToGuid(string obj, string defaultValue)
    {
        Guid retVal = new Guid(defaultValue);

        try
        {
            retVal = new Guid(obj);
        }
        catch (Exception ex)
        {
            try
            {
                retVal = new Guid(defaultValue);
            }
            catch (Exception ex1)
            {
                return new Guid();
            }
        }

        return retVal;
    }

    public static Guid ToGuid(object obj)
    {
        Guid retVal;
        try
        {
            retVal = new Guid(obj.ToString());
        }
        catch (Exception ex)
        {
            try
            {
                retVal = new Guid();
            }
            catch (Exception ex1)
            {
                return new Guid();
            }
        }

        return retVal;
    }

    public static string FormatTimeVn(DateTime dt, string defaultText)
    {
        if (ToDateTime(dt) != new DateTime(1900, 1, 1))
            return dt.ToString("dd-mm-yy");
        else
            return defaultText;
    }

    public static Int16 ToInt16(object obj)
    {
        Int16 retVal = 0;

        try
        {
            retVal = Convert.ToInt16(obj);
        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    public static byte ToByte(object obj, byte defaultValue)
    {
        byte retVal = defaultValue;

        try
        {
            retVal = Convert.ToByte(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static byte ToByte(object obj)
    {
        return ConvertUtility.ToByte(obj, byte.MaxValue);
    }

    public static Int16 ToInt16(object obj, Int16 defaultValue)
    {
        Int16 retVal = defaultValue;

        try
        {
            retVal = Convert.ToInt16(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static int ToInt32(object obj)
    {
        int retVal = 0;

        try
        {
            retVal = Convert.ToInt32(obj);
        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    public static int ToInt32(object obj, int defaultValue)
    {
        int retVal = defaultValue;

        try
        {
            retVal = Convert.ToInt32(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static Int64 ToInt64(object obj)
    {
        Int64 retVal = 0;

        try
        {
            retVal = Convert.ToInt64(obj);
        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    public static Int64 ToInt64(object obj, Int64 defaultValue)
    {
        Int64 retVal = defaultValue;

        try
        {
            retVal = Convert.ToInt64(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static decimal ToDecimal(object obj)
    {
        decimal retVal = 0;

        try
        {
            retVal = Convert.ToDecimal(obj);
        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    public static decimal ToDecimal(object obj, decimal defaultValue)
    {
        decimal retVal = defaultValue;

        try
        {
            retVal = Convert.ToDecimal(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static string ToString(object obj)
    {
        string retVal;

        try
        {
            retVal = Convert.ToString(obj);
        }
        catch (Exception ex)
        {
            retVal = String.Empty;
        }

        return retVal;
    }

    public static string ToString(object obj, string defaultValue)
    {
        string retVal;

        try
        {
            retVal = Convert.ToString(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static DateTime ToDateTime(object obj)
    {
        DateTime retVal;
        try
        {
            retVal = Convert.ToDateTime(obj);
        }
        catch (Exception ex)
        {
            retVal = DateTime.Now;
        }
        if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

        return retVal;
    }

    public static DateTime ToDateTime(object obj, DateTime defaultValue)
    {
        DateTime retVal;
        try
        {
            retVal = Convert.ToDateTime(obj);
        }
        catch (Exception ex)
        {
            retVal = DateTime.Now;
        }
        if (retVal == new DateTime(1, 1, 1)) return defaultValue;

        return retVal;
    }

    public static bool ToBoolean(object obj)
    {
        bool retVal;

        try
        {
            retVal = Convert.ToBoolean(obj);
        }
        catch (Exception ex)
        {
            retVal = false;
        }

        return retVal;
    }

    public static double ToDouble(object obj)
    {
        double retVal;

        try
        {
            retVal = Convert.ToDouble(obj);
        }
        catch (Exception ex)
        {
            retVal = 0;
        }

        return retVal;
    }

    public static double ToDouble(object obj, double defaultValue)
    {
        double retVal;

        try
        {
            retVal = Convert.ToDouble(obj);
        }
        catch (Exception ex)
        {
            retVal = defaultValue;
        }

        return retVal;
    }

    public static string FormatDateTime(object o, string coutry)
    {
        DateTime retVal;
        string strVal = "";
        try
        {
            retVal = Convert.ToDateTime(o);
        }
        catch (Exception ex)
        {
            retVal = new DateTime(1900, 1, 1);
        }
        if (coutry == "vi-VN")
            strVal = retVal.Day.ToString() + "-" + retVal.Month.ToString() + "-" + retVal.Year.ToString();
        else
            strVal = retVal.Month.ToString() + "-" + retVal.Day.ToString() + "-" + retVal.Year.ToString();

        return strVal;
    }


}

public class CookieUtility
{
    public static void SetValueToCookie(string name, string value)
    {
        HttpContext context = HttpContext.Current;
        HttpCookie ckie;
        if (context.Request.Cookies[name] == null)
        {
            ckie = new HttpCookie(name);
        }
        else
        {
            ckie = context.Request.Cookies[name];
        }
        context.Response.Cookies[name].Value = value;
        context.Response.Cookies[name].Expires = DateTime.Now.AddDays(360);
    }

    public static string GetValueFromCookie(string Name)
    {
        HttpContext context = HttpContext.Current;
        if (context.Request.Cookies[Name] == null)
        {
            return null;
        }
        else
        {
            return context.Request.Cookies[Name].Value;
        }
    }
}

public class CacheUtility
{
    private HttpContext context;

    public HttpContext Context
    {
        get { return context; }
        set { context = value; }
    }
    public CacheUtility()
    {
        context = HttpContext.Current;
    }

    public static Object GetValueFromCache(string key)
    {
        HttpContext context = HttpContext.Current;
        if (context.Cache[key] == null)
        {
            return null;
        }
        return context.Cache.Get(key);
    }

    public static void SetValueToCache(string key, object value, int expireTimeMinutes)
    {
        HttpContext context = HttpContext.Current;
        if (value != null)
        {
            context.Cache.Insert(key, value, null,
            DateTime.Now.AddMinutes(expireTimeMinutes), TimeSpan.Zero);
        }
    }

    public static void SetValueToCache(string key, object value)
    {
        SetValueToCache(key, value, 10);
    }

    public static void RemoveValueFromCache(string key)
    {
        HttpContext context = HttpContext.Current;
        context.Cache.Remove(key);
    }

    public static void ClearAllCache()
    {
        HttpContext context = HttpContext.Current;
        IDictionaryEnumerator enumerator = context.Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            context.Cache.Remove(enumerator.Key.ToString());
        }
    }

    public static void PurgeCacheItems(string prefix)
    {
        HttpContext context = HttpContext.Current;
        prefix = prefix.ToLower();
        List<string> itemsToRemove = new List<string>();

        IDictionaryEnumerator enumerator = context.Cache.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Key.ToString().ToLower().Contains(prefix))
            {
                itemsToRemove.Add(enumerator.Key.ToString());
            }
        }

        foreach (string itemToRemove in itemsToRemove)
        {
            context.Cache.Remove(itemToRemove);
        }
    }
}

public class SessionUtility
{
    public static void SetValueToSession(string name, object value)
    {
        HttpContext context = HttpContext.Current;
        if (context.Session[name] == null)
        {
            context.Session.Add(name, value);
        }
        else
        {
            context.Session[name] = value;
        }
    }

    public static object GetValueFromSession(string name)
    {
        HttpContext context = HttpContext.Current;
        if (context.Session[name] == null)
        {
            return null;
        }
        else
        {
            return context.Session[name];
        }

    }

    public static void Remove(string name)
    {
        HttpContext context = HttpContext.Current;
        context.Session.Remove(name);
    }
}

public class UnicodeUtility
{
    private const string uniChars =
        "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

    private const string KoDauChars =
        "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

    public static int UnicodeToUTF8(byte[] dest, int maxDestBytes, string source, int sourceChars)
    {
        int i, count;
        int c, result;

        result = 0;
        if ((source != null && source.Length == 0))
            return result;
        count = 0;
        i = 0;
        if (dest != null)
        {
            while ((i < sourceChars) && (count < maxDestBytes))
            {
                c = (int)source[i++];
                if (c <= 0x7F)
                    dest[count++] = (byte)c;
                else if (c > 0x7FF)
                {
                    if ((count + 3) > maxDestBytes)
                        break;
                    dest[count++] = (byte)(0xE0 | (c >> 12));
                    dest[count++] = (byte)(0x80 | ((c >> 6) & 0x3F));
                    dest[count++] = (byte)(0x80 | (c & 0x3F));
                }
                else
                {
                    //  0x7F < source[i] <= 0x7FF
                    if ((count + 2) > maxDestBytes)
                        break;
                    dest[count++] = (byte)(0xC0 | (c >> 6));
                    dest[count++] = (byte)(0x80 | (c & 0x3F));
                }
            }
            if (count >= maxDestBytes)
                count = maxDestBytes - 1;
            dest[count] = (byte)(0);
        }
        else
        {
            while (i < sourceChars)
            {
                c = (int)(source[i++]);
                if (c > 0x7F)
                {
                    if (c > 0x7FF)
                        count++;
                    count++;
                }
                count++;
            }
        }
        result = count + 1;
        return result;
    }


    public static int UTF8ToUnicode(char[] dest, int maxDestChars, byte[] source, int sourceBytes)
    {
        int i, count;
        int c, result;
        int wc;

        if (source == null)
        {
            result = 0;
            return result;
        }
        result = (int)(-1);
        count = 0;
        i = 0;
        if (dest != null)
        {
            while ((i < sourceBytes) && (count < maxDestChars))
            {
                wc = (int)(source[i++]);
                if ((wc & 0x80) != 0)
                {
                    if (i >= sourceBytes)
                        return result;
                    wc = wc & 0x3F;
                    if ((wc & 0x20) != 0)
                    {
                        c = (byte)(source[i++]);
                        if ((c & 0xC0) != 0x80)
                            return result;
                        if (i >= sourceBytes)
                            return result;
                        wc = (wc << 6) | (c & 0x3F);
                    }
                    c = (byte)(source[i++]);
                    if ((c & 0xC0) != 0x80)
                        return result;
                    dest[count] = (char)((wc << 6) | (c & 0x3F));
                }
                else
                    dest[count] = (char)wc;
                count++;
            }
            if (count > maxDestChars)
                count = maxDestChars - 1;
            dest[count] = (char)(0);
        }
        else
        {
            while (i < sourceBytes)
            {
                c = (byte)(source[i++]);
                if ((c & 0x80) != 0)
                {
                    if (i >= sourceBytes)
                        return result;
                    c = c & 0x3F;
                    if ((c & 0x20) != 0)
                    {
                        c = (byte)(source[i++]);
                        if ((c & 0xC0) != 0x80)
                            return result;
                        if (i >= sourceBytes)
                            return result;
                    }
                    c = (byte)(source[i++]);
                    if ((c & 0xC0) != 0x80)
                        return result;
                }
                count++;
            }
        }
        result = count + 1;
        return result;
    }


    public static byte[] UTF8Encode(string ws)
    {
        int l;
        byte[] temp, result;

        result = null;
        if ((ws != null && ws.Length == 0))
            return result;
        temp = new byte[ws.Length * 3];
        l = UnicodeToUTF8(temp, temp.Length + 1, ws, ws.Length);
        if (l > 0)
        {
            result = new byte[l - 1];
            Array.Copy(temp, 0, result, 0, l - 1);
        }
        else
        {
            result = new byte[ws.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = (byte)(ws[i]);
        }
        return result;
    }


    public static string UTF8Decode(byte[] s)
    {
        int l;
        char[] temp;
        string result;
        result = String.Empty;
        if (s == null)
            return result;
        temp = new char[s.Length + 1];
        l = UTF8ToUnicode(temp, temp.Length, s, s.Length);
        if (l > 0)
        {
            result = "";
            for (int i = 0; i < l - 1; i++)
                result += temp[i];
        }
        else
        {
            result = "";
            for (int i = 0; i < s.Length; i++)
                result += (char)(s[i]);
        }
        return result;
    }

    public static string UnicodeToKoDau(string s)
    {
        string retVal = String.Empty;
        if (s == null)
            return retVal;
        int pos;
        for (int i = 0; i < s.Length; i++)
        {
            pos = uniChars.IndexOf(s[i].ToString());
            if (pos >= 0)
                retVal += KoDauChars[pos];
            else
                retVal += s[i];
        }
        return retVal;
    }

    public static string UnicodeToWindows1252(string s)
    {
        string retVal = String.Empty;
        for (int i = 0; i < s.Length; i++)
        {
            int ord = (int)s[i];
            if (ord > 191)
                retVal += "&#" + ord.ToString() + ";";
            else
                retVal += s[i];
        }
        return retVal;
    }

    public static string UnicodeToISO8859(string src)
    {
        Encoding iso = Encoding.GetEncoding("iso8859-1");
        Encoding unicode = Encoding.UTF8;
        byte[] unicodeBytes = unicode.GetBytes(src);
        return iso.GetString(unicodeBytes);
    }

    public static string ISO8859ToUnicode(string src)
    {
        Encoding iso = Encoding.GetEncoding("iso8859-1");
        Encoding unicode = Encoding.UTF8;
        byte[] isoBytes = iso.GetBytes(src);
        return unicode.GetString(isoBytes);
    }
}

public class MultimediaUtility
{
    private static bool ThumbnailCallback()
    {
        return false;
    }

    public static bool SetThumbnail(string filePath, string newPath, int iThumbWidth, int iThumbHeight)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists) return false;
        try
        {
            if (!Directory.Exists(newPath)) Directory.CreateDirectory(newPath);

            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
            Bitmap myBitmap = new Bitmap(fileInfo.FullName);

            if ((iThumbHeight == 0) && (iThumbWidth == 0)) return false;
            else if ((iThumbHeight != 0) && (iThumbWidth == 0))
                iThumbWidth = (int)(iThumbHeight * myBitmap.Width) / myBitmap.Height;
            else if ((iThumbHeight == 0) && (iThumbWidth != 0))
                iThumbHeight = (int)(iThumbWidth * myBitmap.Height) / myBitmap.Width;

            System.Drawing.Image myThumbnail = myBitmap.GetThumbnailImage(iThumbWidth, iThumbHeight, myCallback, IntPtr.Zero);
            myThumbnail.Save(newPath + fileInfo.Name, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public static bool SetAvatarThumbnail(string filePath, int iThumbWidth, int iThumbHeight)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        if (!fileInfo.Exists) return false;
        return SetThumbnail(filePath, fileInfo.Directory + "\\Avatar\\", iThumbWidth, iThumbHeight);
    }

    public static string GetAvatar(string avatar)
    {
        int splitIndex = avatar.LastIndexOf("/");
        if (splitIndex != 0)
            return avatar.Substring(0, splitIndex) + "/Avatar" + avatar.Substring(splitIndex, avatar.Length - splitIndex);
        else return string.Empty;

    }
    public static string strInitImage(string image, int width, int height)
    {
        string retVal = "<img src=\"" + image + "\"";
        if (width > 0) retVal += " width=\"" + width + "px\" ";
        if (height > 0) retVal += " height=\"" + height + "px\" ";
        retVal += " border=\"0\">";
        return retVal;
    }
    public static string strInitFlash(string flashURL, int width, int height)
    {
        string strMedia = "<div style='z-index:0;'>";
        strMedia += "<object codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\"";
        strMedia += " width=\"" + width.ToString() + "\" height=\"" + height.ToString() + "\">";
        strMedia += " <param name=\"movie\" value=\"" + flashURL + "\"/>";
        strMedia += " <param name=\"quality\" value=\"high\" />";
        strMedia += " <param name=\"wmode\" value=\"transparent\">";
        strMedia += " <embed src=\"" + flashURL + "\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\"";
        strMedia += " width=\"" + width.ToString() + "\" height=\"" + height.ToString() + "\" wmode=\"transparent\"></embed></object>";
        strMedia += "</div>";

        //string retVal = "<EMBED align=baseline src='" + flashURL + "'";
        //if (width != 0) retVal += " width=" + width;
        //if (height != 0) retVal += " height=" + height;

        //retVal += " type=audio/x-pn-realaudio-plugin autostart=\"true\" controls=\"ControlPanel\" console=\"Clip1\" border=\"0\">";
        return strMedia;
    }



    public static string strInitMultimedia(string mediaPath, int width, int height)
    {
        string retVal = "<EMBED pluginspage='http://www.microsoft.com/Windows/Downloads/Contents/Products/MediaPlayer/' ";
        if (width != 0) retVal += " width=" + width;
        if (height != 0) retVal += " height=" + height;

        retVal += " src='" + mediaPath + "' type='application/x-mplayer2' ShowStatusBar='1' AutoStart='true' ShowControls='1'></embed>";
        return retVal;
    }
}

public class ObjectHelper
{
    public static object CreateObject(Type type, IDataReader dr)
    {
        object objTarget = null;
        try
        {
            if (dr.Read())
            {
                objTarget = Activator.CreateInstance(type);
                PropertyInfo[] objProperties = type.GetProperties();
                for (int i = 0; i < objProperties.Length; i++)
                {
                    PropertyInfo property = objProperties[i];
                    if (property.CanWrite)
                    {
                        property.SetValue(objTarget, Convert.ChangeType(dr[property.Name], property.PropertyType), null);
                    }
                }

            }
        }
        finally
        {
            dr.Close();
        }
        return objTarget;
    }

    public static object CreateObject(Type type, DataRow row)
    {
        object objTarget = null;
        try
        {
            if (row != null)
            {
                objTarget = Activator.CreateInstance(type);
                PropertyInfo[] objProperties = type.GetProperties();
                for (int i = 0; i < objProperties.Length; i++)
                {
                    PropertyInfo property = objProperties[i];
                    if (property.CanWrite)
                    {
                        property.SetValue(objTarget, Convert.ChangeType(row[property.Name], property.PropertyType), null);
                    }
                }

            }
        }
        finally
        {

        }
        return objTarget;
    }

    public static T FillObject<T>(IDataReader _dr)
    {
        T objTarget = default(T);
        try
        {
            if (_dr.Read())
            {
                objTarget = Activator.CreateInstance<T>();
                PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                for (int i = 0; i < objProperties.Length; i++)
                {
                    PropertyInfo property = objProperties[i];

                    if (property.CanWrite && !Convert.IsDBNull(_dr[property.Name]))
                    {
                        property.SetValue(objTarget, Convert.ChangeType(_dr[property.Name], property.PropertyType), null);
                    }
                }
            }
        }
        finally
        {
            _dr.Close();
        }
        return objTarget;
    }

    public static T FillObject<T>(IDataReader _dr, string propertyNames)
    {
        if (string.IsNullOrEmpty(propertyNames))
            return FillObject<T>(_dr);
        T objTarget = default(T);
        try
        {
            if (_dr.Read())
            {
                objTarget = Activator.CreateInstance<T>();
                PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                string[] fieldList = propertyNames.Split(',');
                for (int i = 0; i < fieldList.Length; i++)
                {
                    PropertyInfo property = objTarget.GetType().GetProperty(fieldList[i]);

                    if (property.CanWrite && !Convert.IsDBNull(_dr[property.Name]))
                    {
                        property.SetValue(objTarget, Convert.ChangeType(_dr[property.Name], property.PropertyType), null);
                    }
                }
            }
        }
        finally
        {
            _dr.Close();
        }
        return objTarget;
    }

    public static T FillObject<T>(DataRow row)
    {
        T objTarget = default(T);
        try
        {
            if (row != null)
            {
                objTarget = Activator.CreateInstance<T>();
                PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                for (int i = 0; i < objProperties.Length; i++)
                {
                    PropertyInfo property = objProperties[i];

                    if (property.CanWrite && !Convert.IsDBNull(row[property.Name]))
                    {
                        property.SetValue(objTarget, Convert.ChangeType(row[property.Name], property.PropertyType), null);
                    }
                }
            }
        }
        finally
        {

        }
        return objTarget;
    }

    public static T FillObject<T>(DataRow row, string propertyNames)
    {
        if (string.IsNullOrEmpty(propertyNames))
            return FillObject<T>(row);
        T objTarget = default(T);
        try
        {
            if (row != null)
            {
                objTarget = Activator.CreateInstance<T>();
                PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                string[] fieldList = propertyNames.Split(',');
                for (int i = 0; i < fieldList.Length; i++)
                {
                    PropertyInfo property = objTarget.GetType().GetProperty(fieldList[i]);
                    if (property.CanWrite && !Convert.IsDBNull(row[property.Name]))
                    {
                        property.SetValue(objTarget, Convert.ChangeType(row[property.Name], property.PropertyType), null);
                    }
                }
            }
        }
        finally
        {

        }
        return objTarget;
    }

    public static List<T> FillCollection<T>(IDataReader _dr)
    {
        List<T> _list = new List<T>();
        try
        {
            while (_dr.Read())
            {
                T objTarget = (T)Activator.CreateInstance<T>();
                PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                for (int i = 0; i < objProperties.Length; i++)
                {
                    PropertyInfo property = objProperties[i];
                    int index = _dr.GetOrdinal(property.Name);
                    if (index < 0) continue;
                    if (property.CanWrite && !Convert.IsDBNull(_dr[property.Name]))
                    {
                        property.SetValue(objTarget, Convert.ChangeType(_dr[property.Name], property.PropertyType), null);

                    }
                }
                if (_list.IndexOf(objTarget) < 0)
                    _list.Add(objTarget);
            }
        }
        finally
        {
            _dr.Close();
        }
        return _list;
    }

    public static List<T> FillCollection<T>(IDataReader _dr, string propertyNames)
    {
        List<T> _list = new List<T>();
        try
        {
            while (_dr.Read())
            {
                T objTarget = (T)Activator.CreateInstance<T>();
                PropertyInfo[] objProperties = objTarget.GetType().GetProperties();
                string[] proNames = propertyNames.Split(',');
                for (int i = 0; i < proNames.Length; i++)
                {
                    PropertyInfo property = objTarget.GetType().GetProperty(proNames[i]);
                    int index = _dr.GetOrdinal(property.Name);
                    if (index < 0) continue;
                    if (property.CanWrite && !Convert.IsDBNull(_dr[property.Name]))
                    {
                        property.SetValue(objTarget, Convert.ChangeType(_dr[property.Name], property.PropertyType), null);

                    }
                }
                if (_list.IndexOf(objTarget) < 0)
                    _list.Add(objTarget);
            }
        }
        finally
        {
            _dr.Close();
        }
        return _list;
    }

    public static List<T> FillCollection<T>(DataTable dt)
    {
        return FillCollection<T>(dt, string.Empty);
    }

    public static List<T> FillCollection<T>(DataTable dt, string propertyNames)
    {
        List<T> _list = new List<T>();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    T objTarget = FillObject<T>(row, propertyNames);
                    if (_list.IndexOf(objTarget) < 0)
                        _list.Add(objTarget);
                }
            }
        }
        finally
        {

        }
        return _list;
    }

    public static List<T> FillCollection<T>(DataSet ds)
    {
        return FillCollection<T>(ds, string.Empty);
    }

    public static List<T> FillCollection<T>(DataSet ds, string propertyNames)
    {
        List<T> _list = new List<T>();
        try
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                _list = FillCollection<T>(ds.Tables[0], propertyNames);
            }
        }
        finally
        {

        }
        return _list;
    }
}

public class RequestHelper
{
    public RequestHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static Guid GetGuid(string key, Guid defaultvalue)
    {
        string hc = HttpContext.Current.Request.QueryString.Get(key);
        if (!string.IsNullOrEmpty(hc))
        {
            try
            {
                Guid guidTmp = new Guid(hc);
                return guidTmp;
            }
            catch
            {
                return defaultvalue;
            }
        }
        else
        {
            return defaultvalue;
        }
    }

    public static T GetValueParameter<T>(string key, T defaultvalue)
    {
        try
        {
            if (HttpContext.Current.Request[key] != null)
            {
                return ConvertUtility.ConvertObject<T>(HttpContext.Current.Request[key], defaultvalue);
            }
            else
            {
                return defaultvalue;
            }
        }
        catch (Exception ex)
        {
            return defaultvalue;
        }
    }

    public static string GetString(string key, string defaultvalue)
    {
        try
        {
            if (HttpContext.Current.Request[key] != null)
            {
                return ConvertUtility.ToString(HttpContext.Current.Request[key]);
            }
            else
            {
                return defaultvalue;
            }
        }
        catch (Exception ex)
        {
            return defaultvalue;
        }
    }
    public static bool GetBoolean(string key, bool defaultvalue)
    {
        string hc = HttpContext.Current.Request.QueryString.Get(key);
        if (!string.IsNullOrEmpty(hc))
        {
            return hc.ToLower() == "true";
        }
        else
        {
            return defaultvalue;
        }
    }
    public static int GetInt(string key, int defaultvalue)
    {
        string hc = HttpContext.Current.Request.QueryString.Get(key);
        if (!string.IsNullOrEmpty(hc))
        {
            int intTmp;
            if (int.TryParse(hc, out intTmp))
            {
                return intTmp;
            }
            else
                return defaultvalue;
        }
        else
        {
            return defaultvalue;
        }
    }
    public static decimal GetDecimal(string key, decimal defaultvalue)
    {
        string hc = HttpContext.Current.Request.QueryString.Get(key);
        if (!string.IsNullOrEmpty(hc))
        {
            decimal intTmp;
            if (decimal.TryParse(hc, out intTmp))
            {
                return intTmp;
            }
            else
                return defaultvalue;
        }
        else
        {
            return defaultvalue;
        }
    }
    public static long GetLong(string key, long defaultvalue)
    {
        string hc = HttpContext.Current.Request.QueryString.Get(key);
        if (!string.IsNullOrEmpty(hc))
        {
            long intTmp;
            if (long.TryParse(hc, out intTmp))
            {
                return intTmp;
            }
            else
                return defaultvalue;
        }
        else
        {
            return defaultvalue;
        }
    }
}

public class UrlHelper
{
    public static string CombineParaToUrl(string para, string url)
    {
        int index = para.IndexOf("=");
        if (index <= 0) throw new System.ComponentModel.InvalidEnumArgumentException("Không đúng tham số para");
        string pPara = para.Substring(0, index);
        string sPara = para.Substring(index + 1);
        if (url.Contains("?"))
        {
            if (url.Contains(pPara))
            {
                int i = url.IndexOf(pPara);
                string start = url.Substring(0, i + pPara.Length);
                string end = url.Substring(i + pPara.Length + 1);
                int t = end.IndexOf("&");
                if (t >= 0)
                {
                    end = end.Substring(t);
                    url = start + "=" + sPara + "&" + end;
                }
                else
                {
                    end = string.Empty;
                    url = start + "=" + sPara;
                }
            }
            else
            {
                url = url + "&" + para;
            }
        }
        else
        {
            url = url + "?" + para;
        }
        return url;
    }

    public static string AddParasToUrl(string oldUrl, string paras)
    {
        oldUrl = ApproveURL(oldUrl);
        if (oldUrl.Contains("?"))
        {
            if (oldUrl.EndsWith("?"))
            {
                oldUrl = oldUrl + "&" + paras;
            }
            else
            {
                string[] paraList = paras.Split('&');
                if (paraList != null && paraList.Length > 0)
                {
                    foreach (string s in paraList)
                    {
                        if (!oldUrl.Contains(s))
                        {
                            oldUrl = oldUrl + "&" + s;
                        }
                    }
                }
            }
        }
        else
        {
            oldUrl = oldUrl + "?" + paras;
        }
        return oldUrl;
    }

    public static string GetQuery(string url)
    {
        string query = string.Empty;
        int index = url.IndexOf("?");
        if (index >= 0)
        {
            query = url.Substring(index + 1);
        }
        return query;
    }

    public static string AddParameterToUrl(string oldUrl, string para, string value)
    {
        oldUrl = ApproveURL(oldUrl);
        string[] urlPart = oldUrl.Split('?');
        string path = string.Empty;
        string query = string.Empty;
        if (urlPart != null)
        {
            path = urlPart[0];
            if (urlPart.Length > 1)
            {
                query = ConvertUtility.ToString(urlPart[1]);
            }
        }
        para = ApprovePara(para);
        value = ApproveParaValue(value);
        if (query.Contains(para + "="))
        {
            string[] queryList = query.Split('&');
            string newQuery = string.Empty;
            if (queryList != null)
            {
                foreach (string s in queryList)
                {
                    string tmp = s;
                    if (tmp.Substring(0, tmp.IndexOf("=")).Trim().Equals(para))
                    {
                        tmp = para + "=" + value;
                    }
                    if (!newQuery.Contains(tmp))
                    {
                        newQuery = newQuery + tmp + "&";
                    }

                }
            }
            query = ApproveURL(newQuery);
        }
        else
        {
            if (string.IsNullOrEmpty(query))
            {
                query = para + "=" + value;
            }
            else
            {
                query = ApproveURL(query);
                query = query + "&" + para + "=" + value;
            }
        }

        return path + "?" + query;
    }

    public static string RemoveParameterToUrl(string oldUrl, string para)
    {
        para = ApprovePara(para);
        if (oldUrl.Contains(para))
        {
            string[] queryList = oldUrl.Split('&');
            string newUrl = string.Empty;
            if (queryList != null)
            {
                foreach (string s in queryList)
                {
                    if (s.Contains(para))
                    {
                        continue;
                    }
                    newUrl = newUrl + s + "&";

                }
            }
            if (newUrl.EndsWith("&"))
            {
                newUrl = newUrl.Substring(0, newUrl.Length - 1);
            }
            oldUrl = newUrl;
        }
        return oldUrl;
    }

    protected static string ApprovePara(string para)
    {
        para = para.Replace("=", "");
        para = para.Replace("&", "");
        para = para.Replace("?", "");
        return para;
    }

    protected static string ApproveParaValue(string value)
    {
        value = value.Replace("=", "");
        value = value.Replace("&", "");
        value = value.Replace("?", "");
        return value;
    }

    public static string ApproveURL(string url)
    {
        if (url.EndsWith("&"))
        {
            url = url.Substring(0, url.Length - 1);
        }
        return url;
    }
}

public class HtmlToText
{
    public HtmlToText()
    {
    }

    public string Convert(string path)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.Load(path);

        StringWriter sw = new StringWriter();
        ConvertTo(doc.DocumentNode, sw);
        sw.Flush();
        return sw.ToString();
    }

    public string ConvertHtml(string html)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);

        StringWriter sw = new StringWriter();
        ConvertTo(doc.DocumentNode, sw);
        sw.Flush();
        return sw.ToString();
    }

    private void ConvertContentTo(HtmlNode node, TextWriter outText)
    {
        foreach (HtmlNode subnode in node.ChildNodes)
        {
            ConvertTo(subnode, outText);
        }
    }




    public void ConvertTo(HtmlNode node, TextWriter outText)
    {
        string html;
        switch (node.NodeType)
        {
            case HtmlNodeType.Comment:
                // don't output comments
                break;

            case HtmlNodeType.Document:
                ConvertContentTo(node, outText);
                break;

            case HtmlNodeType.Text:
                // script and style must not be output
                string parentName = node.ParentNode.Name;
                if ((parentName == "script") || (parentName == "style"))
                    break;

                // get text
                html = ((HtmlTextNode)node).Text;

                // is it in fact a special closing node output as text?
                if (HtmlNode.IsOverlappedClosingElement(html))
                    break;

                // check the text is meaningful and not a bunch of whitespaces
                if (html.Trim().Length > 0)
                {
                    outText.Write(HtmlEntity.DeEntitize(html));
                }
                break;

            case HtmlNodeType.Element:
                switch (node.Name)
                {
                    case "p":
                        // treat paragraphs as crlf
                        outText.Write("\r\n");
                        break;
                }

                if (node.HasChildNodes)
                {
                    ConvertContentTo(node, outText);
                }
                break;
        }
    }
}

public static class JSONHelper
{
    public static string ToJSON(this object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }

    public static string ToJSON(this object obj, int recursionDepth)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        serializer.RecursionLimit = recursionDepth;
        return serializer.Serialize(obj);
    }
}

public class SqlHelper
{
    static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static DataTable SQLToDataTable(string table)
    {
        return SQLToDataTable(table, string.Empty, string.Empty);
    }

    public static DataTable SQLToDataTable(string table, string fields, string where)
    {
        return SQLToDataTable(table, fields, where, string.Empty);
    }

    public static DataTable SQLToDataTable(string table, string fields, string where, string orderby)
    {
        return SQLToDataTable(table, fields, where, orderby, 0, 0);
    }

    public static DataTable SQLToDataTable(string table, string fields, string where, string orderby, int pageIndex, int pageSize)
    {
        int totalRows = 0;
        return SQLToDataTable(table, fields, where, orderby, pageIndex, pageSize, out totalRows);
    }

    public static DataTable SQLToDataTable(string table, string fields, string where, string orderby, int pageIndex, int pageSize, out int totalRows)
    {
        totalRows = 0;
        string sqlQuery = string.Empty;

        if (string.IsNullOrEmpty(fields.Trim()))
            fields = "*";
        if (!string.IsNullOrEmpty(where.Trim()))
            where = " WHERE " + where;
        if (!string.IsNullOrEmpty(orderby.Trim()))
            orderby = " ORDER BY " + orderby;


        if (pageIndex < 1 || pageSize < 1)
        {
            sqlQuery = string.Format("SELECT {0} FROM {1}{2}{3}", fields, table, where, orderby);
        }
        else
        {
            if (string.IsNullOrEmpty(orderby.Trim())) // Offset phải có order by
                orderby = " ORDER BY ID DESC";

            string pagingSql = string.Format(@" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", pageSize * (pageIndex - 1), pageSize);
            sqlQuery = string.Format("SELECT {0} FROM {1}{2}{3}{4}", fields, table, where, orderby, pagingSql);
        }

        DataTable dtReturn = null;
        string key = table + "_" + fields + "_" + where + "_" + orderby + "_" + pageIndex + "_" + pageSize;
        int CACHE_TIME_MINUTES = ConvertUtility.ToInt16(C.CACHE_TIME_MINUTES);

        if (CACHE_TIME_MINUTES > 0)
        {
            if (CacheUtility.GetValueFromCache(key) == null)
            {
                dtReturn = Exe_SQLToDataTable(table, where, sqlQuery, out totalRows);

                CacheUtility.SetValueToCache(key, dtReturn, CACHE_TIME_MINUTES);
            }
            else
            {
                dtReturn = (DataTable)CacheUtility.GetValueFromCache(key);
            }

            //Count Total Rows
            if (CacheUtility.GetValueFromCache(key + "_totalRows") == null)
            {
                string query_count = string.Format("Select COUNT(ID) FROM {0}{1}", table, where);
                using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
                {
                    dtReturn = dbx.ExecuteSqlDataTable(sqlQuery);
                    DataTable dtCount = dbx.ExecuteSqlDataTable(query_count);
                    totalRows = ConvertUtility.ToInt32(dtCount.Rows[0][0]);
                }

                CacheUtility.SetValueToCache(key + "_totalRows", totalRows);
            }
            else
            {
                totalRows = ConvertUtility.ToInt32(CacheUtility.GetValueFromCache(key + "_totalRows"));
            }
        }
        else
        {
            dtReturn = Exe_SQLToDataTable(table, where, sqlQuery, out totalRows);
        }

        return dtReturn;
    }

    public static DataTable Exe_SQLToDataTable(string table, string where, string sqlQuery, out int totalRows)
    {
        DataTable dtReturn = null;
        totalRows = 0;
        try
        {
            string query_count = string.Format("Select COUNT(ID) FROM {0}{1}", table, where);
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                dtReturn = dbx.ExecuteSqlDataTable(sqlQuery);
                DataTable dtCount = dbx.ExecuteSqlDataTable(query_count);
                totalRows = ConvertUtility.ToInt32(dtCount.Rows[0][0]);
            }
        }
        catch
        {

        }
        return dtReturn;
    }



    private static void AttachParameters(SqlCommand cmd, SqlParameter[] parameters)
    {
        foreach (SqlParameter p in parameters)
        {
            if (p.Direction == ParameterDirection.InputOutput && p.Value == null)
            {
                p.Value = DBNull.Value;
            }
            cmd.Parameters.Add(p);
        }
    }


    public static int GetCount(string table, string where)
    {
        int totalRows = 0;
        if (!string.IsNullOrEmpty(where.Trim()))
            where = " WHERE " + where;
        string query_count = string.Format("Select COUNT(ID) FROM {0} {1}", table, where);
        using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            DataTable dtCount = dbx.ExecuteSqlDataTable(query_count);
            totalRows = ConvertUtility.ToInt32(dtCount.Rows[0][0]);
        }

        return totalRows;
    }

    public static bool IsLeafCategory(object ID, string table)
    {
        bool _return = false;
        try
        {
            DataTable dt = SQLToDataTable(table, "ID", "ParentID=" + ID, "ID", 1, 1);
            if (Utils.CheckExist_DataTable(dt))
            {
                _return = true;
            }
            return _return;
        }
        catch
        {
            _return = true;
        }

        return _return;
    }

    private Dictionary<string, int> keyPairs = new Dictionary<string, int>();




    public static void LogsToDatabase_ByID(int ID, string table, string control, string Name, int action, string link)
    {
        DataTable dataTable = SqlHelper.SQLToDataTable(table, "", "ID=" + ID);
        if (dataTable != null && dataTable.Rows.Count > 0)
            LogsToDatabase(dataTable, table, control, Name, action, link);
    }


    /// <summary>
    /// Lưu logs hệ thống theo DataTable
    /// </summary>
    /// <param name="dataTable">dataTable</param>
    /// <param name="table">tên bảng</param>
    /// <param name="control">Ví dụ product</param>
    /// <param name="Name">Ví dụ Sản phẩm</param>
    /// <param name="action">0: thêm mới, 1: update, 2: Xóa, 3: Khôi phục</param>
    /// <param name="link">Link hiện tại</param>
    /// 
    public static void LogsToDatabase(DataTable dataTable, string table, string control, string Name, int action, string link)
    {
        List<JsonObjectByField> jsonObjList = new List<JsonObjectByField>();
        if (dataTable != null && dataTable.Rows.Count > 0)
        {
            foreach (DataColumn dc in dataTable.Columns)
            {
                JsonObjectByField jsonObj = new JsonObjectByField();
                jsonObj.Field = dc.ColumnName;
                jsonObj.Value = ConvertUtility.ToString(dataTable.Rows[0][dc.ColumnName]);
                jsonObjList.Add(jsonObj);
            }
        }


        string strAction = "";
        if (action == 0)
            strAction = "Thêm mới";
        else if (action == 1)
            strAction = "Cập nhật";
        else if (action == 2)
            strAction = "Xóa";
        else if (action == 3)
            strAction = "Khôi phục";



        string Json_Content = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObjList, Newtonsoft.Json.Formatting.Indented);

        CacheUtility.PurgeCacheItems("tblLogs");

        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = @"INSERT INTO [dbo].[tblLogs] ([Name],[Action],[Control],[Json_Content],[TableSql],[Link],[CreatedDate],[CreatedBy]) OUTPUT INSERTED.ID VALUES (@Name,@Action,@Control,@Json_Content,@TableSql,@Link,@CreatedDate,@CreatedBy)";

            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
            db.AddParameter("@Action", System.Data.SqlDbType.NVarChar, strAction);
            db.AddParameter("@Control", System.Data.SqlDbType.NVarChar, control);
            db.AddParameter("@Json_Content", System.Data.SqlDbType.NVarChar, Json_Content);
            db.AddParameter("@TableSql", System.Data.SqlDbType.NVarChar, table);
            db.AddParameter("@Link", System.Data.SqlDbType.NVarChar, link);
            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
            db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, ConvertUtility.ToInt32(HttpContext.Current.User.Identity.Name));

            db.ExecuteSql(sqlQuery);
        }


    }

    public static Hashtable JsonToHashTable(string jsonString)
    {
        Hashtable hashTable = new Hashtable();
        hashTable = Newtonsoft.Json.JsonConvert.DeserializeObject<Hashtable>(jsonString);
        //foreach (DictionaryEntry entry in ht)
        //{
        //    log.Info(entry.Value + "<br />");
        //}

        return hashTable;
    }

    public static List<JsonObjectByField> GetJsonObjectByField(string jsonString)
    {
        List<JsonObjectByField> jsonObjectByField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonObjectByField>>(jsonString);
        return jsonObjectByField;
    }

    //public static string GetJsonObjectByField(List<JsonObjectByField>)
    //{
    //    List<JsonObjectByField> jsonObjectByField = Newtonsoft.Json.JsonConvert.DeserializeObject<List<JsonObjectByField>>(jsonString);
    //    return jsonObjectByField;
    //}


    public static string GetPricePercent(int ProductID)
    {
        string strReturn = string.Empty;
        bool isTemporary = false;
        decimal Price = GetPrice_Decimal(ProductID, "Price", true, out isTemporary);
        decimal Price1 = GetPrice_Decimal(ProductID, "Price1", true, out isTemporary);

        int percentComplete = 0;
        if (Price1 > Price && Price > 0)
        {
            percentComplete = 100 - (int)Math.Round((decimal)(100 * Price) / Price1);
            strReturn = "-" + percentComplete + " %";
        }
        return strReturn;
    }


    public static string GetPrice(int ProductID, string PriceField)
    {
        bool isTemporary;
        return GetPrice(ProductID, PriceField, "VNĐ", true, out isTemporary);
    }
    public static string GetPrice(int ProductID, string PriceField, bool ByCurrentDate)
    {
        bool isTemporary;
        return GetPrice(ProductID, PriceField, "VNĐ", ByCurrentDate, out isTemporary);
    }



    public static string GetPrice(int ProductID, string PriceField, string CurrencySymbol, bool ByCurrentDate, out bool isTemporary)
    {
        isTemporary = false;
        string strReturn = string.Empty;

        decimal price = GetPrice_Decimal(ProductID, PriceField, ByCurrentDate, out isTemporary);
        if (price > 0)
        {
            strReturn = string.Format("{0:N0} {1}", price, CurrencySymbol);
        }
        else if (PriceField == "Price")
        {
            strReturn = "Vui lòng liên hệ";
        }
        return strReturn;
    }


    public static decimal GetPrice_Decimal(int ProductID, string PriceField, bool ByCurrentDate)
    {
        bool isTemporary = false;
        return GetPrice_Decimal(ProductID, PriceField, ByCurrentDate, out isTemporary);
    }

    public static decimal GetPrice_Decimal(int ProductID, string PriceField, bool ByCurrentDate, out bool isTemporary)
    {
        decimal Return = 0;
        string FilterDate = "";
        if (ByCurrentDate)
            FilterDate = " AND StartDate<DATEADD(DAY, DATEDIFF(day, 0, getdate()), 1) AND EndDate>DATEADD(DAY, DATEDIFF(day, 0, getdate()), 1)";
        DataTable dtTemporaryPrice = SqlHelper.SQLToDataTable("tblPrice", PriceField, string.Format("ProductID={0}{1}", ProductID, FilterDate), "ID DESC", 1, 1);
        if (dtTemporaryPrice != null && dtTemporaryPrice.Rows.Count > 0)
        {
            isTemporary = true;
            Return = ConvertUtility.ToDecimal(dtTemporaryPrice.Rows[0][PriceField]);
        }
        else
        {
            isTemporary = false;
            DataTable dtPrice = SQLToDataTable("tblProducts", PriceField, "ID=" + ProductID);
            if (dtPrice != null && dtPrice.Rows.Count > 0 && ConvertUtility.ToInt32(dtPrice.Rows[0][PriceField]) > 0)
            {
                Return = ConvertUtility.ToDecimal(dtPrice.Rows[0][PriceField]);
            }
        }
        return Return;
    }


    public static bool GetForeignTable_Check(string Field)
    {
        bool Check = false;
        GetForeignTable(Field, string.Empty, out Check);
        return Check;
    }

    public static string GetForeignTable(string Field, string Value)
    {
        bool Check = false;
        return GetForeignTable(Field, Value, out Check);
    }

    public static string GetForeignTable(string Field, string Value, out bool Check)
    {
        Check = false;

        string Return = string.Empty;
        if (Field == "ProductID")
        {
            Check = true;
            if (!string.IsNullOrEmpty(Value))
            {
                DataTable dt = SQLToDataTable("tblProducts", "Name", "ID=" + Value);
                if (dt != null && dt.Rows.Count > 0)
                    Return = ConvertUtility.ToString(dt.Rows[0]["Name"]);
            }
        }
        else if (Field == "AttributesIDList")
        {
            Check = true;
            if (!string.IsNullOrEmpty(Value))
            {
                DataTable dt = SQLToDataTable("tblAttributes", "Name", string.Format("ID IN ({0})", Value.Trim(',')));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Return += string.Format(@"<a class=""tag"" href=""javascript:;"">{0}</a>", dr["Name"].ToString());
                    }

                    Return = Utils.CommaSQLRemove(Return);
                }
            }
        }

        else if (Field == "AttributeConfigIDList")
        {
            Check = true;
            if (!string.IsNullOrEmpty(Value))
            {
                DataTable dt = SQLToDataTable("tblAttributeConfigs", "Name", string.Format("ID IN ({0})", Value.Trim(',')));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Return += string.Format(@"<a class=""tag"" href=""javascript:;"">{0}</a>", dr["Name"].ToString());
                    }

                    Return = Utils.CommaSQLRemove(Return);
                }
            }
        }

        else if (Field == "CategoryIDParentList")
        {
            if (!string.IsNullOrEmpty(Value))
            {
                DataTable dt = SQLToDataTable("tblCategories", "Name", string.Format("ID IN ({0})", Value.Trim(',')));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Return += string.Format(@"<a class=""tag"" href=""javascript:;"">{0}</a>", dr["Name"].ToString());
                    }

                    Return = Utils.CommaSQLRemove(Return);
                }
            }
            Check = true;
        }
        else if (Field == "CategoryIDList")
        {
            if (!string.IsNullOrEmpty(Value))
            {
                DataTable dt = SQLToDataTable("tblCategories", "Name", string.Format("ID IN ({0})", Value.Trim(',')));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Return += string.Format(@"<a class=""tag"" href=""javascript:;"">{0}</a>", dr["Name"].ToString());
                    }

                    Return = Utils.CommaSQLRemove(Return);
                }
            }
            Check = true;
        }
        else if (Field == "CreatedBy" || Field == "EditedBy")
        {
            if (!string.IsNullOrEmpty(Value))
            {
                DataTable dt = SQLToDataTable("tblAdminUser", "Name", string.Format("ID IN ({0})", Value.Trim(',')));
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Return += string.Format(@"<a class=""tag"" href=""javascript:;"">{0}</a>", dr["Name"].ToString());
                    }

                    Return = Utils.CommaSQLRemove(Return);
                }

            }
            Check = true;
        }

        return Return;
    }


    public static void Update_Url_Table(bool IsUpdate, string modul, int ID, string Name, string FriendlyUrl)
    {
        DataTable tblCheck = SQLToDataTable("tblUrl", "ID", string.Format("ContentID='{0}' AND Moduls='{1}'", ID, modul));
        if (!Utils.CheckExist_DataTable(tblCheck))
            IsUpdate = false;

        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Empty;
            if (IsUpdate)
                sqlQuery = @"UPDATE[dbo].[tblUrl] SET [Name]=@Name,[FriendlyUrl]=@FriendlyUrl,[EditedDate]=@EditedDate,[EditedBy]=@EditedBy WHERE [ContentID] = @ID AND [Moduls]=@Moduls";
            else
                sqlQuery = @"INSERT INTO [dbo].[tblUrl]([Name],[FriendlyUrl],[Moduls],[ContentID],[CreatedDate],[EditedDate],[CreatedBy],[EditedBy]) OUTPUT INSERTED.ID VALUES (@Name,@FriendlyUrl,@Moduls,@ContentID,@CreatedDate,@EditedDate,@CreatedBy,@EditedBy)";

            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
            db.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, FriendlyUrl);
            db.AddParameter("@Moduls", System.Data.SqlDbType.NVarChar, modul);
            db.AddParameter("@ContentID", System.Data.SqlDbType.Int, ID);
            db.AddParameter("@EditedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
            db.AddParameter("@EditedBy", System.Data.SqlDbType.Int, HttpContext.Current.User.Identity.Name);

            if (IsUpdate)
            {
                db.AddParameter("@ID", System.Data.SqlDbType.Int, ID);
            }
            else
            {
                db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                db.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, HttpContext.Current.User.Identity.Name);
            }
            db.ExecuteSql(sqlQuery);
        }
    }
}

public class ShoppingCart
{
    public static string CartID
    {
        get
        {
            string CartID = CookieUtility.GetValueFromCookie("CartID");
            if (string.IsNullOrEmpty(CartID))
            {
                CartID = Utils.RandomStringNumberANDCharacter(50);
                CookieUtility.SetValueToCookie("CartID", CartID);
            }
            return CartID;
        }
    }

    public static void AddToCart(object ProductID, int Quantity)
    {
        Hashtable hashtable = new Hashtable();
        string CouponCode = "";
        int PID = ConvertUtility.ToInt32(ProductID);
        CacheUtility.PurgeCacheItems("tblCart");
        hashtable["CartID"] = CartID;
        hashtable["Quantity"] = Quantity;
        hashtable["ProductID"] = PID;
        hashtable["Token"] = Utils.IPAddress;

        int VoucheLeft = 0;
        int PriceDiscount = 0;
        int Price = 0;
        int TotalPrice = 0;
        int QuantityDiscount = 0;

        DataTable dtProduct = SqlHelper.SQLToDataTable("tblProducts", "Price", "ID=" + PID);
        if (Utils.CheckExist_DataTable(dtProduct))
        {
            Price = ConvertUtility.ToInt32(dtProduct.Rows[0]["Price"]);
            TotalPrice = Price * Quantity;

            //Lưu ý: chỉ đúng khi số lượng thêm vào giỏ hàng = 1

            DataTable dtCoupon = SqlHelper.SQLToDataTable("tblCoupon", "ValueVND,MaxUsing,Using,Name", string.Format("ProductIDList like N'%,{0},%' AND ExpireDate>= GETDATE() AND [Using]<[MaxUsing]", PID));
            if (Utils.CheckExist_DataTable(dtCoupon))
            {
                CouponCode = dtCoupon.Rows[0]["Name"].ToString();
                VoucheLeft = ConvertUtility.ToInt32(dtCoupon.Rows[0]["MaxUsing"]) - ConvertUtility.ToInt32(dtCoupon.Rows[0]["Using"]);
                PriceDiscount = Price - ConvertUtility.ToInt32(dtCoupon.Rows[0]["ValueVND"]);
                TotalPrice = PriceDiscount * Quantity;
                QuantityDiscount = VoucheLeft;

                if (VoucheLeft > 0 && VoucheLeft > Quantity)
                    QuantityDiscount = Quantity;
            }
        }



        bool IsUpdate = false;
        DataTable dt = SqlHelper.SQLToDataTable("tblCart", "ID,Quantity", string.Format("CartID=N'{0}' AND ProductID={1}", CartID, ProductID));
        if (Utils.CheckExist_DataTable(dt)) //Nếu đã tồn tại trong giỏ hàng thì cộng thêm số lượng
        {
            //Quantity += ConvertUtility.ToInt32(dt.Rows[0]["Quantity"]);
            if (VoucheLeft > 0)
            {
                if (VoucheLeft <= Quantity)
                {
                    int TotalPriceDiscount = VoucheLeft * PriceDiscount; // Lấy toàn bộ số voucher còn lại * giá sản phẩm
                    int NomalQuantity = Quantity - VoucheLeft;
                    int TotalPriceNomal = NomalQuantity * Price;

                    TotalPrice = TotalPriceDiscount + TotalPriceNomal;
                    QuantityDiscount = VoucheLeft;
                }
                else
                {
                    TotalPrice = Quantity * PriceDiscount;
                    QuantityDiscount = Quantity;
                }
            }

            IsUpdate = true;
            hashtable["ID"] = ConvertUtility.ToInt32(dt.Rows[0]["ID"]);
        }


        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Empty;
            if (IsUpdate)
            {
                sqlQuery = @"UPDATE [dbo].[tblCart] SET [CartID]=@CartID, [Quantity]=@Quantity, [QuantityDiscount]=@QuantityDiscount, [CouponCode]=@CouponCode, [ProductID]=@ProductID, [Token]=@Token, [Price]=@Price WHERE [ID] = @ID";
                db.AddParameter("@ID", System.Data.SqlDbType.Int, hashtable["ID"].ToString());
            }
            else
            {
                sqlQuery = @"INSERT INTO [dbo].[tblCart] ([CartID],[Quantity],[QuantityDiscount],[CouponCode],[ProductID],[Price],[CreatedDate],[Token]) OUTPUT INSERTED.ID VALUES (@CartID,@Quantity,@QuantityDiscount,@CouponCode,@ProductID,@Price,@CreatedDate,@Token)";
            }

            db.AddParameter("@Price", System.Data.SqlDbType.Int, TotalPrice);
            db.AddParameter("@CartID", System.Data.SqlDbType.NVarChar, hashtable["CartID"].ToString());
            db.AddParameter("@Quantity", System.Data.SqlDbType.Int, hashtable["Quantity"].ToString());
            db.AddParameter("@ProductID", System.Data.SqlDbType.Int, hashtable["ProductID"].ToString());
            db.AddParameter("@QuantityDiscount", System.Data.SqlDbType.Int, QuantityDiscount);
            db.AddParameter("@CouponCode", System.Data.SqlDbType.NVarChar, CouponCode);
            db.AddParameter("@Token", System.Data.SqlDbType.NVarChar, hashtable["Token"].ToString());
            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
            db.ExecuteSql(sqlQuery);
        }
    }

    //public static void UpdateCart(object ProductID, int Quantity)
    //{
    //    Hashtable hashtable = new Hashtable();
    //    int PID = ConvertUtility.ToInt32(ProductID);
    //    CacheUtility.PurgeCacheItems("tblCart");
    //    hashtable["CartID"] = CartID;
    //    hashtable["Quantity"] = Quantity;
    //    hashtable["ProductID"] = PID;

    //    if (Quantity > 0)
    //    {
    //        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
    //        {
    //            string sqlQuery = @"UPDATE [dbo].[tblCart] SET [Quantity]=@Quantity WHERE [CartID]=@CartID AND [ProductID] = @ProductID";

    //            db.AddParameter("@CartID", System.Data.SqlDbType.NVarChar, hashtable["CartID"].ToString());
    //            db.AddParameter("@Quantity", System.Data.SqlDbType.Int, hashtable["Quantity"].ToString());
    //            db.AddParameter("@ProductID", System.Data.SqlDbType.Int, hashtable["ProductID"].ToString());
    //            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
    //            db.ExecuteSql(sqlQuery);
    //        }
    //    }
    //    else
    //    {
    //        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
    //        {
    //            string sqlQuery = string.Format(@"Delete From tblCart where CartID=N'{0}' AND ProductID={1}", CartID, PID);
    //            db.ExecuteSql(sqlQuery);
    //        }
    //    }
    //}

    public static void DeleteProduct(object ProductID)
    {
        CacheUtility.PurgeCacheItems("tblCart");
        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Format(@"Delete From tblCart where CartID=N'{0}' AND ProductID={1}", CartID, ProductID.ToString());
            db.ExecuteSql(sqlQuery);
        }
    }

    public static void ClearCart()
    {
        CacheUtility.PurgeCacheItems("tblCart");

        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Format(@"Delete From tblCart where CartID=N'{0}'", CartID);
            db.ExecuteSql(sqlQuery);
        }
    }
    public static int CartCount
    {
        get
        {
            int count = 0;
            string sqlQuery = string.Format(@"Select COUNT(ID) from tblCart where CartID=N'{0}''", CartID);
            DataTable cart = SqlHelper.SQLToDataTable("tblCart", "ID", string.Format(@"CartID=N'{0}'", CartID));
            if (Utils.CheckExist_DataTable(cart))
            {
                count = cart.Rows.Count;
            }
            return count;
        }
    }

    public static string CartToOrder(object Name, object Phone, object Address, object Email, object PaymentMethod, object NoteMember, object MailTemplate)
    {
        decimal PriceFinal = 0;
        List<OrderInfo> jsonObjList = GetOrderInfo(out PriceFinal);

        //Cập nhật lại số lượng mã Coupon
        if (jsonObjList.Count > 0)
        {
            foreach (OrderInfo orderInfo in jsonObjList)
            {
                if (orderInfo.NumberApplyVoucher > 0)
                {
                    using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
                    {
                        string sqlQuery = string.Format(@"Update tblCoupon SET [Using]=[Using] + {0} WHERE Name=N'{1}'", orderInfo.NumberApplyVoucher, orderInfo.CouponCode);
                        db.ExecuteSql(sqlQuery);
                    }
                }
            }
        }

        string Json_Content = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObjList, Newtonsoft.Json.Formatting.Indented);

        string Token = ConvertUtility.ToString(Utils.Timestamp);
        Hashtable hashtable = new Hashtable();
        hashtable["Name"] = ConvertUtility.ToString(Name);
        hashtable["MemberID"] = CartID;
        hashtable["Status"] = (int)OrderStatus.ProcessingInProgress;
        hashtable["Address"] = ConvertUtility.ToString(Address);
        hashtable["Email"] = ConvertUtility.ToString(Email);
        hashtable["Phone"] = ConvertUtility.ToString(Phone);
        hashtable["Json"] = Json_Content;
        hashtable["PriceFinal"] = PriceFinal;
        hashtable["Flags"] = "0";
        hashtable["PaymentMethod"] = PaymentMethod;
        hashtable["ShippingMethod"] = "";
        hashtable["NoteMember"] = NoteMember;
        hashtable["NoteAdmin"] = "";
        hashtable["ShipDate"] = "";
        hashtable["Coupon"] = "";
        hashtable["Token"] = Token;
        hashtable["MailTemplate"] = MailTemplate;
        hashtable["utm_id"] = "";
        hashtable["utm_source"] = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_source"));
        hashtable["utm_medium"] = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_medium"));
        hashtable["utm_campaign"] = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_campaign"));
        hashtable["utm_adgroup"] = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_adgroup"));
        hashtable["utm_term"] = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_term"));
        hashtable["utm_content"] = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("utm_content"));
        hashtable["IPAddress"] = Utils.GetIPAddress();


        using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = @"INSERT INTO [dbo].[tblOrder] ([Name],[MemberID],[Status],[Address],[Phone],[Email],[Json],[PriceFinal],[Flags],[PaymentMethod],[ShippingMethod],[NoteMember],[NoteAdmin],[OrderDate],[ShipDate],[CreatedDate],[Coupon],[Token],[MailTemplate],[Website],[utm_id],[utm_source],[utm_medium],[utm_campaign],[utm_adgroup],[utm_term],[utm_content],[IPAddress]) OUTPUT INSERTED.ID VALUES (@Name,@MemberID,@Status,@Address,@Phone,@Email,@Json,@PriceFinal,@Flags,@PaymentMethod,@ShippingMethod,@NoteMember,@NoteAdmin,@OrderDate,@ShipDate,@CreatedDate,@Coupon,@Token,@MailTemplate,@Website,@utm_id,@utm_source,@utm_medium,@utm_campaign,@utm_adgroup,@utm_term,@utm_content,@IPAddress)";

            db.AddParameter("@Name", System.Data.SqlDbType.NVarChar, hashtable["Name"].ToString());
            db.AddParameter("@MemberID", System.Data.SqlDbType.NVarChar, hashtable["MemberID"].ToString());
            db.AddParameter("@Status", System.Data.SqlDbType.Int, hashtable["Status"].ToString());
            db.AddParameter("@Address", System.Data.SqlDbType.NVarChar, hashtable["Address"].ToString());
            db.AddParameter("@Phone", System.Data.SqlDbType.NVarChar, hashtable["Phone"].ToString());
            db.AddParameter("@Email", System.Data.SqlDbType.NVarChar, hashtable["Email"].ToString());
            db.AddParameter("@Json", System.Data.SqlDbType.NVarChar, hashtable["Json"].ToString());
            db.AddParameter("@PriceFinal", System.Data.SqlDbType.Money, hashtable["PriceFinal"].ToString());
            db.AddParameter("@Flags", System.Data.SqlDbType.Int, hashtable["Flags"].ToString());
            db.AddParameter("@PaymentMethod", System.Data.SqlDbType.NVarChar, hashtable["PaymentMethod"].ToString());
            db.AddParameter("@ShippingMethod", System.Data.SqlDbType.NVarChar, hashtable["ShippingMethod"].ToString());
            db.AddParameter("@NoteMember", System.Data.SqlDbType.NVarChar, hashtable["NoteMember"].ToString());
            db.AddParameter("@NoteAdmin", System.Data.SqlDbType.NVarChar, hashtable["NoteAdmin"].ToString());
            db.AddParameter("@OrderDate", System.Data.SqlDbType.DateTime, DateTime.Now);
            db.AddParameter("@ShipDate", System.Data.SqlDbType.DateTime, hashtable["ShipDate"].ToString());
            db.AddParameter("@Coupon", System.Data.SqlDbType.NVarChar, hashtable["Coupon"].ToString());
            db.AddParameter("@Token", System.Data.SqlDbType.NVarChar, hashtable["Token"].ToString());
            db.AddParameter("@MailTemplate", System.Data.SqlDbType.NVarChar, hashtable["MailTemplate"].ToString());
            db.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
            db.AddParameter("@Website", System.Data.SqlDbType.NVarChar, C.ROOT_URL);
            db.AddParameter("@utm_id", System.Data.SqlDbType.NVarChar, hashtable["utm_id"].ToString());
            db.AddParameter("@utm_source", System.Data.SqlDbType.NVarChar, hashtable["utm_source"].ToString());
            db.AddParameter("@utm_medium", System.Data.SqlDbType.NVarChar, hashtable["utm_medium"].ToString());
            db.AddParameter("@utm_campaign", System.Data.SqlDbType.NVarChar, hashtable["utm_campaign"].ToString());
            db.AddParameter("@utm_adgroup", System.Data.SqlDbType.NVarChar, hashtable["utm_adgroup"].ToString());
            db.AddParameter("@utm_term", System.Data.SqlDbType.NVarChar, hashtable["utm_term"].ToString());
            db.AddParameter("@utm_content", System.Data.SqlDbType.NVarChar, hashtable["utm_content"].ToString());
            db.AddParameter("@IPAddress", System.Data.SqlDbType.NVarChar, hashtable["IPAddress"].ToString());

            db.ExecuteSql(sqlQuery);
        }







        CacheUtility.PurgeCacheItems("tblOrder");
        ClearCart();

        return Token;
    }

    public static List<OrderInfo> GetOrderInfo(out decimal PriceFinal)
    {
        List<OrderInfo> jsonObjList = new List<OrderInfo>();
        PriceFinal = 0;
        DataTable dtShoppingCart = SqlHelper.SQLToDataTable("tblCart", "", string.Format("CartID=N'{0}'", CartID));
        if (Utils.CheckExist_DataTable(dtShoppingCart))
        {
            foreach (DataRow drCart in dtShoppingCart.Rows)
            {
                DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", "ID=" + ConvertUtility.ToString(drCart["ProductID"]));
                if (Utils.CheckExist_DataTable(dtProduct))
                {
                    OrderInfo jsonObj = new OrderInfo();
                    decimal Price = ConvertUtility.ToDecimal(drCart["Price"]);

                    decimal TotalPrice = Price;
                    PriceFinal += TotalPrice;
                    jsonObj.ProductID = ConvertUtility.ToInt32(drCart["ProductID"]);
                    jsonObj.Quantity = ConvertUtility.ToInt32(drCart["Quantity"]);
                    jsonObj.Price = Price;
                    jsonObj.TotalPrice = TotalPrice;
                    jsonObj.NumberApplyVoucher = ConvertUtility.ToInt32(drCart["QuantityDiscount"]);
                    jsonObj.CouponCode = drCart["CouponCode"].ToString();
                    jsonObj.Name = ConvertUtility.ToString(dtProduct.Rows[0]["Name"]);
                    jsonObj.Image = Utils.GetFirstImageInGallery_Json(ConvertUtility.ToString(dtProduct.Rows[0]["Gallery"]));
                    jsonObjList.Add(jsonObj);
                }
            }
        }
        return jsonObjList;
    }

    //public static List<OrderInfo> GetOrderInfo(out decimal PriceFinal)
    //{
    //    List<OrderInfo> jsonObjList = new List<OrderInfo>();
    //    PriceFinal = 0;
    //    DataTable dtShoppingCart = SqlHelper.SQLToDataTable("tblCart", "", string.Format("CartID=N'{0}'", CartID));
    //    if (Utils.CheckExist_DataTable(dtShoppingCart))
    //    {
    //        foreach (DataRow drCart in dtShoppingCart.Rows)
    //        {
    //            DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", "ID=" + ConvertUtility.ToString(drCart["ProductID"]));
    //            if (Utils.CheckExist_DataTable(dtProduct))
    //            {
    //                OrderInfo jsonObj = new OrderInfo();
    //                decimal Price = SqlHelper.GetPrice_Decimal(ConvertUtility.ToInt32(drCart["ProductID"]), "Price", true);
    //                if (ConvertUtility.ToInt32(drCart["Price"]) > 0) //nếu có mã voucher
    //                {
    //                    Price = ConvertUtility.ToInt32(drCart["Price"]);
    //                    jsonObj.NumberApplyVoucher = 1;
    //                }
    //                decimal TotalPrice = Price * ConvertUtility.ToInt32(drCart["Quantity"]);
    //                PriceFinal += TotalPrice;
    //                jsonObj.ProductID = ConvertUtility.ToInt32(drCart["ProductID"]);
    //                jsonObj.Quantity = ConvertUtility.ToInt32(drCart["Quantity"]);
    //                    jsonObj.Price = Price;
    //                jsonObj.TotalPrice = TotalPrice;
    //                jsonObj.Name = ConvertUtility.ToString(dtProduct.Rows[0]["Name"]);
    //                jsonObj.Image = Utils.GetFirstImageInGallery_Json(ConvertUtility.ToString(dtProduct.Rows[0]["Gallery"]));
    //                jsonObjList.Add(jsonObj);
    //            }
    //        }
    //    }
    //    return jsonObjList;
    //}
}


public static class PageUtility
{
    public static void AddTitle(Page page, string title)
    {
        LiteralControl metaTag = new LiteralControl();
        metaTag.Text = string.Format("\n<title>{0}</title>", title);
        page.Header.Controls.Add(metaTag);
    }

    public static void AddMetaTag(Page page, string name, string content)
    {
        LiteralControl metaTag = new LiteralControl();
        metaTag.Text = string.Format("\n<meta name=\"{0}\" content=\"{1}\" />", name, content);
        page.Header.Controls.Add(metaTag);
    }

    public static void AddCssLink(Page page, string href, bool checkVersion)
    {
        string versionedHref = checkVersion ? CheckVersion(href) : href;

        if (!string.IsNullOrEmpty(versionedHref))
        {
            string cssLinkTag = string.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />", versionedHref);
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(cssLinkTag));
        }
    }

    public static void AddScriptLink(Page page, string src, bool checkVersion)
    {
        string versionedSrc = checkVersion ? CheckVersion(src) : src;

        if (!string.IsNullOrEmpty(versionedSrc))
        {
            string scriptTag = string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", versionedSrc);
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(scriptTag));
        }
    }

    public static void Charset(Page page)
    {
        string metaCharsetTag = "<meta charset=\"utf-8\" />";
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(metaCharsetTag));
    }

    public static void X_UA_Compatible(Page page)
    {
        string metaCompatibleTag = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />";
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(metaCompatibleTag));
    }

    public static void Viewport(Page page)
    {
        string metaViewportTag = "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />";
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(metaViewportTag));
    }

    public static void OpenGraph(Page page, string title, string type, string url, string image, string site_name, string description)
    {
        string ogTitleTag = string.Format("<meta property=\"og:title\" content=\"{0}\" />", title);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(ogTitleTag));

        string ogImageTag = string.Format("<meta property=\"og:image\" content=\"{0}\" />", image);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(ogImageTag));

        string ogDescriptionTag = string.Format("<meta property=\"og:description\" content=\"{0}\" />", description);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(ogDescriptionTag));

        string ogUrlTag = string.Format("<meta property=\"og:url\" content=\"{0}\" />", url);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(ogUrlTag));

        string ogTypeTag = string.Format("<meta property=\"og:type\" content=\"{0}\" />", type);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(ogTypeTag));

        string ogSiteNameTag = string.Format("<meta property=\"og:site_name\" content=\"{0}\" />", site_name);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(ogSiteNameTag));

        // Twitter

        string twitter_card = string.Format("<meta name=\"twitter:card\" content=\"summary_large_image\">");
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(twitter_card));

        string twitter_TitleTag = string.Format("<meta name=\"twitter:title\" content=\"{0}\" />", title);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(twitter_TitleTag));

        string twitter_ImageTag = string.Format("<meta name=\"twitter:image\" content=\"{0}\" />", image);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(twitter_ImageTag));

        string twitter_DescriptionTag = string.Format("<meta name=\"twitter:description\" content=\"{0}\" />", description);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(twitter_DescriptionTag));

        // Itemprop

        string Itemprop_TitleTag = string.Format("<meta itemprop=\"name\" content=\"{0}\" />", title);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(Itemprop_TitleTag));

        string Itemprop_ImageTag = string.Format("<meta itemprop=\"image\" content=\"{0}\" />", image);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(Itemprop_ImageTag));

        string Itemprop_DescriptionTag = string.Format("<meta itemprop=\"description\" content=\"{0}\" />", description);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(Itemprop_DescriptionTag));
    }

    public static void AddCanonicalLink(Page page, string href)
    {
        string canonicalLinkTag = string.Format("<link rel=\"canonical\" href=\"{0}\" />", href);
        page.Header.Controls.Add(new LiteralControl("\n"));
        page.Header.Controls.Add(new LiteralControl(canonicalLinkTag));
    }

    public static void AddIconLink(Page page, string href)
    {
        if (Utils.IS_LOCAL)
        {
            string iconLinkTag = string.Format("<link rel=\"shortcut icon\" href=\"{0}\" />", C.ROOT_URL + "/themes/image/code.svg");
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(iconLinkTag));
        }
        else
        {
            string ico = ConfigWeb.Icon;
            string xType = string.Empty;
            string regexPattern = @"\.([^.]+)$";
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(ico, regexPattern);
            if (match.Success)
            {
                string extension = match.Groups[1].Value;
                if (extension == "png")
                    xType = " type=\"image/png\"";
                else if (extension == "jpg")
                    xType = " type=\"image/jpg\"";
                else if (extension == "ico")
                    xType = " type=\"image/x-icon\"";
            }

            string iconLinkTag = string.Format("<link rel=\"shortcut icon\" href=\"{0}\"{1} />", C.ROOT_URL + ConfigWeb.Icon, xType);
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(iconLinkTag));

            iconLinkTag = string.Format("<link rel=\"icon\" href=\"{0}\"{1} />", C.ROOT_URL + ConfigWeb.Icon, xType);
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(iconLinkTag));

            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"57x57\" href=\"{0}apple-icon-57x57.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"60x60\" href=\"{0}apple-icon-60x60.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"72x72\" href=\"{0}apple-icon-72x72.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"76x76\" href=\"{0}apple-icon-76x76.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"114x114\" href=\"{0}apple-icon-114x114.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"120x120\" href=\"{0}apple-icon-120x120.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"144x144\" href=\"{0}apple-icon-144x144.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"152x152\" href=\"{0}apple-icon-152x152.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"apple-touch-icon\" sizes=\"180x180\" href=\"{0}apple-icon-180x180.png\">", href)));

            // Tạo các dòng cho biểu tượng favicon và manifest
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"icon\" type=\"image/png\" sizes=\"192x192\" href=\"{0}android-icon-192x192.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"icon\" type=\"image/png\" sizes=\"32x32\" href=\"{0}favicon-32x32.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"icon\" type=\"image/png\" sizes=\"96x96\" href=\"{0}favicon-96x96.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"icon\" type=\"image/png\" sizes=\"16x16\" href=\"{0}favicon-16x16.png\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl(string.Format("<link rel=\"mask-icon\" href=\"{0}safari-pinned-tab.svg\" color=\"#5bbad5\">", href)));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl("<link rel=\"manifest\" href=\"/manifest.json\">"));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl("<meta name=\"msapplication-TileColor\" content=\"#ffffff\">"));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl("<meta name=\"msapplication-TileImage\" content=\"/ms-icon-144x144.png\">"));
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(new LiteralControl("<meta name=\"theme-color\" content=\"#ffffff\">"));

            // kết hợp https://www.favicon-generator.org/ và https://realfavicongenerator.net/
        }
    }


    public static void AddAlternateLink(Page page)
    {
        string alternateTag = string.Format("<link rel=\"alternate\" href=\"{0}\" hreflang=\"{1}\" />", C.ROOT_URL, "vi-vn");
        LiteralControl alternateLink = new LiteralControl(alternateTag);
        if (page.Header != null && page.Header.Controls != null)
        {
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(alternateLink);
        }
    }

    public static string CheckVersion(string path)
    {
        if (!String.IsNullOrEmpty(path.Trim()))
        {
            string link = HttpContext.Current.Server.MapPath(path.Trim());
            if (System.IO.File.Exists(link))
            {
                string version = System.IO.File.GetLastWriteTime(link).Ticks.ToString();
                return path.Trim() + "?v=" + version;
            }
        }
        return "";
    }

    public static void SetIndex(Page page)
    {
        AddMetaTag(page, "robots", "follow, index, max-snippet:-1, max-video-preview:-1, max-image-preview:large");
    }
    public static void SetNoIndex(Page page)
    {
        AddMetaTag(page, "robots", "noindex");
    }

    public static void SetScriptHeaderContent(Page page, string scriptContent)
    {
        LiteralControl ltrContent = new LiteralControl(scriptContent);
        if (page.Header != null && page.Header.Controls != null)
        {
            page.Header.Controls.Add(new LiteralControl("\n"));
            page.Header.Controls.Add(ltrContent);
        }
    }


    public static void AddDefaultMetaTag(Page page)
    {
        PageUtility.AddIconLink(page, "/");
        PageUtility.Charset(page);
        PageUtility.X_UA_Compatible(page);
        PageUtility.Viewport(page);
        PageUtility.AddCssLink(page, "/themes/assets/css/bootstrap.min.css", false);
        PageUtility.AddCssLink(page, "/themes/assets/css/custom.css", true);
        if (!string.IsNullOrEmpty(ConfigWeb.Style))
            PageUtility.AddCssLink(page, "/themes/assets/css/" + ConfigWeb.Style, false);
        PageUtility.AddScriptLink(page, "/themes/assets/js/jquery/jquery-3.6.3.min.js", false);
        PageUtility.AddScriptLink(page, "/themes/assets/js/jquery/jquery-ui.min.js", false);
        PageUtility.AddScriptLink(page, "/themes/assets/js/bootstrap/popper.min.js", false);
        PageUtility.AddScriptLink(page, "/themes/assets/js/bootstrap/bootstrap.min.js", false);
        PageUtility.SetScriptHeaderContent(page, ConfigWeb.CodeHeader);
    }
}
