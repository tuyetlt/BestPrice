using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for StringUitl
/// </summary>
public static class StringUtil
{
    private static readonly System.Random randNum = new System.Random();
    private static readonly String[] VietNamChar = new String[]
    {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
    };

    public static String Removing(String str)
    {
        for (int i = 1; i < VietNamChar.Length; i++)
        {
            for (int j = 0; j < VietNamChar[i].Length; j++)
                str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
        }
        return str;
    }

    public static string CreateRandomString(int PasswordLength)
    {
        string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";

        char[] chars = new char[PasswordLength];
        int allowedCharCount = _allowedChars.Length;

        for (int i = 0; i < PasswordLength; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }
    public static string RandomNumberString(int Length)
    {
        string _allowedChars = "0123456789";

        char[] chars = new char[Length];
        int allowedCharCount = _allowedChars.Length;

        for (int i = 0; i < Length; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }

    public static string PostedDate(DateTime postedDate)
    {
        String[] dayArray = new String[] { "Chủ nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy" };
        String DayOfWeek = dayArray[((int)postedDate.DayOfWeek)];
        return DayOfWeek + "," + postedDate.ToString();
    }
    public static string HtmlEncode(string text)
    {
        char[] chars = HttpUtility.HtmlEncode(text).ToCharArray();
        StringBuilder result = new StringBuilder(text.Length + (int)(text.Length * 0.1));

        foreach (char c in chars)
        {
            int value = Convert.ToInt32(c);
            if (value > 127)
                result.AppendFormat("&#{0};", value);
            else
                result.Append(c);
        }

        return result.ToString();
    }

    public static string SplitSummary(string input, int length)
    {
        if (input.Length > length)
        {
            input = input.Substring(0, length);

            if (input.Contains(" "))
                input = input.Substring(0, input.LastIndexOf(' ')) + "...";
        }
        return input;
    }
    public static string GetStringDate()
    {
        string Date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        return Date;
    }
    public static string GetStringDate(string format = "MM/dd/yyyy H:M:SS")
    {
        string Date = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("en-US"));
        return Date;
    }
    public static string Prefix(int count)
    {
        if (count == 0)
            return "";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            sb.Append("&nbsp;");
        }
        return HttpContext.Current.Server.HtmlDecode(sb.ToString());
    }


    private static IDictionary<char, char> dic = new Dictionary<char, char>();

    public static String FormatCode(String str, int length)
    {
        if (length <= 0)
            return str;
        if (str == null)
            str = String.Empty;

        int pad = length - str.Length;
        if (pad > 0)
        {
            String str2 = FormatUtil.PadLeft(str, length, '0');
            return str2;
        }
        else if (pad < 0)
        {
            String str2 = str.Substring(0, length);
            return str2;
        }
        return str;
    }



    public static bool DigitalOnly(String str)
    {
        if (str == null || str.Length == 0)
            return false;
        foreach (char chr in str)
        {
            if (!char.IsDigit(chr))
                return false;
        }
        return true;
    }

    public static String SimplifyString(String str)
    {
        if (str == null || str.Length == 0)
            return String.Empty;

        char[] chrs = str.ToCharArray();
        char[] chrs2 = new char[chrs.Length];

        if (dic == null)
            dic = new Dictionary<char, char>();

        #region dictionary
        if (dic.Count == 0)
        {
            dic.Add('Đ', 'D');

            dic.Add('Á', 'A');
            dic.Add('À', 'A');
            dic.Add('Ả', 'A');
            dic.Add('Ã', 'A');
            dic.Add('Ạ', 'A');

            dic.Add('Â', 'A');
            dic.Add('Ấ', 'A');
            dic.Add('Ầ', 'A');
            dic.Add('Ẩ', 'A');
            dic.Add('Ẫ', 'A');
            dic.Add('Ậ', 'A');

            dic.Add('É', 'E');
            dic.Add('È', 'E');
            dic.Add('Ẻ', 'E');
            dic.Add('Ẽ', 'E');
            dic.Add('Ẹ', 'E');

            dic.Add('Ê', 'E');
            dic.Add('Ế', 'E');
            dic.Add('Ề', 'E');
            dic.Add('Ể', 'E');
            dic.Add('Ễ', 'E');
            dic.Add('Ệ', 'E');

            dic.Add('Í', 'I');
            dic.Add('Ì', 'I');
            dic.Add('Ỉ', 'I');
            dic.Add('Ĩ', 'I');
            dic.Add('Ị', 'I');

            dic.Add('Ó', 'O');
            dic.Add('Ò', 'O');
            dic.Add('Ỏ', 'O');
            dic.Add('Õ', 'O');
            dic.Add('Ọ', 'O');

            dic.Add('Ô', 'O');
            dic.Add('Ố', 'O');
            dic.Add('Ồ', 'O');
            dic.Add('Ổ', 'O');
            dic.Add('Ỗ', 'O');
            dic.Add('Ộ', 'O');

            dic.Add('Ơ', 'O');
            dic.Add('Ớ', 'O');
            dic.Add('Ờ', 'O');
            dic.Add('Ở', 'O');
            dic.Add('Ỡ', 'O');
            dic.Add('Ợ', 'O');

            dic.Add('Ú', 'U');
            dic.Add('Ù', 'U');
            dic.Add('Ủ', 'U');
            dic.Add('Ũ', 'U');
            dic.Add('Ụ', 'U');

            dic.Add('Ư', 'U');
            dic.Add('Ứ', 'U');
            dic.Add('Ừ', 'U');
            dic.Add('Ữ', 'U');
            dic.Add('Ử', 'U');
            dic.Add('Ự', 'U');

            dic.Add('Ý', 'Y');
            dic.Add('Ỳ', 'Y');
            dic.Add('Ỷ', 'Y');
            dic.Add('Ỹ', 'Y');
            dic.Add('Ỵ', 'Y');

            dic.Add('đ', 'd');

            dic.Add('á', 'a');
            dic.Add('à', 'a');
            dic.Add('ả', 'a');
            dic.Add('ã', 'a');
            dic.Add('ạ', 'a');

            dic.Add('â', 'a');
            dic.Add('ấ', 'a');
            dic.Add('ầ', 'a');
            dic.Add('ẩ', 'a');
            dic.Add('ẫ', 'a');
            dic.Add('ậ', 'a');

            dic.Add('é', 'e');
            dic.Add('è', 'e');
            dic.Add('ẻ', 'e');
            dic.Add('ẽ', 'e');
            dic.Add('ẹ', 'e');

            dic.Add('ê', 'e');
            dic.Add('ế', 'e');
            dic.Add('ề', 'e');
            dic.Add('ể', 'e');
            dic.Add('ễ', 'e');
            dic.Add('ệ', 'e');

            dic.Add('í', 'i');
            dic.Add('ì', 'i');
            dic.Add('ỉ', 'i');
            dic.Add('ĩ', 'i');
            dic.Add('ị', 'i');

            dic.Add('ó', 'o');
            dic.Add('ò', 'o');
            dic.Add('ỏ', 'o');
            dic.Add('õ', 'o');
            dic.Add('ọ', 'o');

            dic.Add('ô', 'o');
            dic.Add('ố', 'o');
            dic.Add('ồ', 'o');
            dic.Add('ổ', 'o');
            dic.Add('ỗ', 'o');
            dic.Add('ộ', 'o');

            dic.Add('ơ', 'o');
            dic.Add('ớ', 'o');
            dic.Add('ờ', 'o');
            dic.Add('ở', 'o');
            dic.Add('ỡ', 'o');
            dic.Add('ợ', 'o');

            dic.Add('ú', 'u');
            dic.Add('ù', 'u');
            dic.Add('ủ', 'u');
            dic.Add('ũ', 'u');
            dic.Add('ụ', 'u');

            dic.Add('ư', 'u');
            dic.Add('ứ', 'u');
            dic.Add('ừ', 'u');
            dic.Add('ử', 'u');
            dic.Add('ữ', 'u');
            dic.Add('ự', 'u');

            dic.Add('ý', 'y');
            dic.Add('ỳ', 'y');
            dic.Add('ỷ', 'y');
            dic.Add('ỹ', 'y');
            dic.Add('ỵ', 'y');
        }
        #endregion dictionary

        int len = chrs.Length;
        char chr;
        for (int i = 0; i < len; i++)
        {
            if (dic.TryGetValue(chrs[i], out chr))
            {
                chrs2[i] = chr;
            }
            else
            {
                chrs2[i] = chrs[i];
            }
        }
        return new String(chrs2);
    }

    public static int GetLength(String str)
    {
        if (IsEmpty(str))
            return 0;
        else
            return str.Length;
    }
    /// <summary>
    /// Check whether this string is empty or not
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsEmpty(String str)
    {
        if (str == null)
            return true;
        else
            return (str.Trim().Length == 0);
    }

    /// <summary>
    /// Check whether Str1 equals with Str2
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public static bool Equal(String str1, String str2)
    {
        if (str1 == null && str2 == null)
            return true;
        else if (str1 != null && str2 == null)
            return false;
        else if (str1 == null && str2 != null)
            return false;
        else
            return str2.Equals(str2);
    }

    /// <summary>
    /// Compare str1 to str2
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public static int Compare(String str1, String str2)
    {
        if (str1 == null && str2 == null)
            return 0;
        else if (str1 != null && str2 == null)
            return 1;
        else if (str1 == null && str2 != null)
            return -1;
        else
            return str1.CompareTo(str2);
    }

    public static String WordWrap(String strGetFromDB, int maxCharOfLine)
    {
        String[] strSplit = strGetFromDB.Split();
        StringBuilder strReturn = new StringBuilder();
        StringBuilder strTemp = new StringBuilder();
        int i;

        foreach (String strArr in strSplit)
        {
            if (strArr.Length > maxCharOfLine)
            {
                for (i = 0; strArr.Length - i > maxCharOfLine; i += maxCharOfLine)
                {
                    strTemp = strTemp.Append(strArr.Substring(i, maxCharOfLine) + " ");
                }
                strTemp = strTemp.Append(strArr.Substring(i, strArr.Length - i));
            }
            else
            {
                strTemp = strTemp.Append(strArr);
            }
            strReturn = strReturn.Append(strTemp + " ");
            strTemp.Remove(0, strTemp.Length);
        }
        return strReturn.Remove(strReturn.Length - 1, 1).ToString();
    }

    public static String Concat<T>(IList<T> objs, String seperator)
    {
        if (objs == null || objs.Count == 0)
            return String.Empty;
        int max = objs.Count - 1;
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < max; i++)
        {
            sb.Append(objs[i]);
            sb.Append(seperator);
        }
        sb.Append(objs[max]);
        return sb.ToString();
    }

    public static String AutoWildCard(String str)
    {
        if (IsEmpty(str))
            return String.Empty;
        else
            return "%" + str + "%";
    }

    public static String TrimWildCard(String str)
    {
        if (IsEmpty(str))
            return String.Empty;
        else
            return str.Trim('%');
    }

    public static String TrimStart(String str)
    {
        if (IsEmpty(str))
            return String.Empty;
        else
            return str.TrimStart();
    }

    public static String TrimEnd(String str)
    {
        if (IsEmpty(str))
            return String.Empty;
        else
            return str.TrimEnd();
    }

    public static String Trim(String str)
    {
        if (IsEmpty(str))
            return String.Empty;
        else
            return str.Trim();
    }

    public static String GetLastChar(String str)
    {
        if (IsEmpty(str))
            return String.Empty;
        else if (str.Length <= 1)
            return str;
        else
        {
            str = str.Substring(str.Length - 2);
            return str;
        }
    }
    public static string KillChars(string strInput)
    {

        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            string[] arrBadChars = new string[] { "select", "SELECT", "drop", "DROP", ";", "--", "insert", "INSERT", "delete", "DELETE", "xp_", "XP_", "sysobjects", "SYSOBJECTS", "syscolumns", "SYSCOLUMNS", "or", "OR", "'", "1=1", "truncate", "TRUNCATE", "table", "TABLE", "is null", "IS NULL" };
            result = strInput.Trim().Replace("'", "''");
            result = strInput.Replace("%20", " ");
            //result = result.ToLower();
            for (int i = 0; i < arrBadChars.Length; i++)
            {
                string strBadChar = arrBadChars[i].ToString();
                result = result.Replace(strBadChar, "");
            }
        }
        return result;
    }
    public static string KillCharEmail(string strInput)
    {

        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            string[] arrBadChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_", "sysobjects", "syscolumns", "'", "1=1", "truncate", "table", "is null" };
            result = strInput.Trim().Replace("'", "''");
            result = strInput.Replace("%20", " ");
            result = result.ToLower();
            for (int i = 0; i < arrBadChars.Length; i++)
            {
                string strBadChar = arrBadChars[i].ToString();
                result = result.Replace(strBadChar, "");
            }
        }
        return result;
    }

    public static String NullValue(object obj)
    {
        String result = "";
        if (obj == null)
            result = "";
        else
            result = obj.ToString();
        return result;
    }

    public static String Combine(params string[] arr)
    {
        string result = "";
        if (arr == null || arr.Length == 0)
        {
            return "";
        }
        else
        {
            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i];
            }
        }
        return result;
    }
    public static string HTMLTextToUTF8(string str)
    {
        var bytes = Encoding.Default.GetBytes(str);
        var result = Encoding.UTF8.GetString(bytes);
        return result;
     }
}
public static class FormatUtil
{
    public static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        return char.ToUpper(s[0]) + s.Substring(1);
    }

    public static string NumberToWords1(Double num)
    {
        string s = NumberToWords(num) + "đồng";
        return UppercaseFirst(s);
    }

    public static string NumberToWords(Double num)
    {
        Int64 number = Convert.ToInt64(num);
        if (number == 0)
            return "không";

        if (number < 0)
            return "âm " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000000) > 0)
        {
            words += NumberToWords(number / 1000000000) + " tỉ ";
            number %= 1000000000;
        }

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " triệu ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " ngàn ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " trăm ";
            number %= 100;
        }

        if (number > 0)
        {
            var unitsMap = new[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám",
                                       "chín", "mười", "mười một", "mười hai", "mười ba", "mười bốn", "mười lăm",
                                       "mười sáu", "mười bảy", "mười tám", "mười chín" };
            var tensMap = new[] { "zero", "mười", "hai mươi", "ba mươi", "bốn mươi",
                                      "năm mươi", "sáu mươi", "bảy mươi", "tám mươi", "chín mươi" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }

        return words;
    }

    public static String GetNextSequence(String str)
    {
        long l = long.Parse(str);
        l++;
        String tmp = l.ToString();
        if (tmp.Length < str.Length)
        {
            return PadLeft(tmp, str.Length, '0');
        }
        else
        {
            return tmp;
        }
    }

    public static String PadLeft(String str, int len, char pad)
    {
        if (str.Length >= len)
            return str;
        int max = len - str.Length;
        char[] chrs = new char[max];
        for (int i = 0; i < max; i++)
            chrs[i] = pad;
        return (new String(chrs)) + str;
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(byte n)
    {
        return n.ToString("#,##0");
    }

    public static String Format(object obj)
    {
        if (obj != null)
        {
            try
            {
                Double value = Convert.ToDouble(obj.ToString());
                return value.ToString("#,##0");

            }
            catch (Exception)
            {
                return "0";
            }
        }
        else
        { return "0"; }

    }
    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(short n)
    {
        return n.ToString("#,##0");
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(int n)
    {
        return n.ToString("#,##0");
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(long n)
    {
        return n.ToString("#,##0");
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(float n)
    {
        return n.ToString("#,##0.00");
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(double n)
    {
        return n.ToString("#,##0.00");
        //return n.ToString("#,##0.00");

        //String result = String.Format("{0:0,000.00}", n);
        //return result;

    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(decimal n)
    {
        return n.ToString("#,##0.00");
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(byte? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(short? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(int? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(long? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(float? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(double? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(decimal? n)
    {
        return (n != null && n.HasValue ? n.Value.ToString("#,##0") : String.Empty);
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(float n, int scale)
    {
        if (scale <= 0)
            return n.ToString("#,##0");
        char chr0 = '0';
        char[] chrs = new char[scale];
        for (int i = 0; i < chrs.Length; i++)
            chrs[i] = chr0;
        return n.ToString("#,##0." + new String(chrs));

    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(double n, int scale)
    {
        if (scale <= 0)
            return n.ToString("#,##0");
        char chr0 = '0';
        char[] chrs = new char[scale];
        for (int i = 0; i < chrs.Length; i++)
            chrs[i] = chr0;
        return n.ToString("#,##0." + new String(chrs));
    }

    /// <summary>
    /// Return a string of this number with Format "#,##0.00".
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static String Format(decimal n, int scale)
    {
        if (scale <= 0)
            return n.ToString("#,##0");
        char chr0 = '0';
        char[] chrs = new char[scale];
        for (int i = 0; i < chrs.Length; i++)
            chrs[i] = chr0;
        return n.ToString("#,##0." + new String(chrs));
    }

   
}

