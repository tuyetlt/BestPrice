using System;
using System.Globalization;
using System.Runtime.Serialization;
public static class DateUtil
{
    public static int GetWeekOfYear(DateTime date)
    {
        System.Globalization.CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("no");
        System.Globalization.Calendar cal = cult_info.Calendar;
        int weekNo = cal.GetWeekOfYear(date, cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
        return weekNo;
    }
    public static DateTime Get4WeeksAgo()
    {
        DateTime date = DateTime.Today;
        return AddDay(date, -28);
    }
    public static DateTime GetTomorrow()
    {
        return AddDay(DateTime.Today, 1);
    }
    public static DateTime GetToday()
    {
        return DateTime.Today;
    }
    public static bool CheckDate(DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate == null || toDate == null)
            return true;
        if (fromDate.Value <= toDate.Value)
            return true;
        else
            return false;
        //bool result = true;
        //if (toDate != null && toDate.Value.Date < DateTime.Now.Date)
        //{
        //    result = false;
        //}
        //else if (fromDate != null && toDate != null && fromDate.Value.Date > toDate.Value.Date)
        //{
        //    result = false;
        //}
        //return result;
    }
    /// <summary>
    /// Get Date by String.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime Parse(String strDate, String format)
    {
        return DateTime.ParseExact(strDate.Trim(), format, null);
    }
    /// <summary>
    /// Get Date by String.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime? ParseNullable(String strDate, String format)
    {
        if (StringUtil.IsEmpty(strDate))
            return null;
        else
            return DateTime.ParseExact(strDate.Trim(), format, null);
    }
    public static DateTime NowByTimeZone(int timezoneOffset)
    {
        return AddHour(DateTime.Now.ToUniversalTime(), timezoneOffset);
    }
    public static String NowByTimeZone(int timezoneOffset, String format)
    {
        DateTime dt = AddHour(DateTime.Now.ToUniversalTime(), timezoneOffset);
        if (format == null || StringUtil.IsEmpty(format))
            return dt.ToString();
        else
            return Format(dt, format);
    }
    /// <summary>
    /// Get Date by String.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool TryParse(String strDate, String format)
    {
        try
        {
            DateTime date = DateTime.ParseExact(strDate.Trim(), format, null);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public static String Format(Object date, String format)
    {
        if (date == null)
            return String.Empty;
        if (date is DateTime)
            return Format((DateTime)date, format);
        else if (date is DateTime?)
            return Format((DateTime?)date, format);
        else
            return String.Empty;
    }
    public static String Format(DateTime? date, String format)
    {
        if (date == null || !date.HasValue)
            return String.Empty;
        return date.Value.ToString(format);
    }
    public static String Format(DateTime date, String format)
    {
        if (date == DateTime.MinValue)
            return String.Empty;
        return date.ToString(format);
    }
    public static String Format(DateTime date, int days, String format)
    {
        if (date == DateTime.MinValue)
            return String.Empty;
        return DateUtil.AddDay(date, days).ToString(format);
    }
    public static DateTime? AddDay(DateTime? date, int days)
    {
        if (date == null)
            return null;
        TimeSpan timespan = new TimeSpan(days, 0, 0, 0);
        return new DateTime?(date.Value.Add(timespan));
    }
    public static DateTime AddDay(DateTime date, int days)
    {
        TimeSpan timespan = new TimeSpan(days, 0, 0, 0);
        return date.Add(timespan);
    }
    public static DateTime AddHour(DateTime date, int hours)
    {
        TimeSpan timespan = new TimeSpan(hours, 0, 0);
        return date.Add(timespan);
    }
    public static DateTime AddMinute(DateTime date, int minutes)
    {
        TimeSpan timespan = new TimeSpan(0, minutes, 0);
        return date.Add(timespan);
    }
    public static DateTime AddSecond(DateTime date, int second)
    {
        TimeSpan timespan = new TimeSpan(0, 0, second);
        return date.Add(timespan);
    }
    public static String AddDay(String strDate, int days, String inputformat, String outputformat)
    {
        DateTime date = Parse(strDate, inputformat);
        DateTime outputdate = AddDay(date, days);
        return Format(outputdate, outputformat);
    }
    public static void ReFormatDate(Object obj, String[] properties, String inputDateFormat, String outDateFormat)
    {
    }
    public static DateTime GetTodayTime(DateTime? datetime0)
    {
        DateTime datetime = datetime0.Value;
        DateTime today = DateTime.Today;
        DateTime time = new DateTime(today.Year, today.Month, today.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        return time;
    }
    public static DateTime GetTodayTime(DateTime datetime)
    {
        DateTime today = DateTime.Today;
        DateTime time = new DateTime(today.Year, today.Month, today.Day, datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);
        return time;
    }
    public static void CountWeekBetweenTwoDateTime(DateTime startDate, DateTime endDate, out int numberOfWeek, out int startNumber, out int endNumber)
    {
        int startWeek = DateUtil.GetWeekOfYear(startDate);
        int endWeek = DateUtil.GetWeekOfYear(endDate);
        numberOfWeek = 0;
        startNumber = 0;
        endNumber = 0;
        for (int i = startWeek; i <= endWeek; i++)
            numberOfWeek++;
        startNumber = (int)startDate.DayOfWeek;
        if (startNumber == 0)
            startNumber = 7;
        endNumber = (int)endDate.DayOfWeek;
        if (endNumber == 0)
            endNumber = 7;
    }
    public static DateTime GetNext4Weeks()
    {
        DateTime date = DateTime.Today;
        return AddDay(date, 28);
    }
    public static int GetDaysBetweenTwoDateTime(DateTime startDate, DateTime endDate)
    {
        int days = 0;
        if (startDate.Year == endDate.Year)
            days = endDate.DayOfYear - startDate.DayOfYear;
        else if ((startDate.Year + 1) == endDate.Year)
        {
            days += GetDaysInYear(startDate.Year) - startDate.DayOfYear;
            days += endDate.DayOfYear;
        }
        else if ((endDate.Year - startDate.Year) > 1)
        {
            days += GetDaysInYear(startDate.Year) - startDate.DayOfYear;
            for (int i = startDate.Year + 1; i < endDate.Year; i++)
                days += GetDaysInYear(i);
            days += endDate.DayOfYear;
        }
        return days;
    }
    public static int GetDaysInYear(int year)
    {
        int days = 0;
        for (int i = 1; i < 13; i++)
            days += DateTime.DaysInMonth(year, i);
        return days;
    }

    public static string DateNow
    {

        get
        { return DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US")); }
    }
    public static string DateNowMMDDYYY
    {
        get
        { return DateTime.Now.ToString("MM/dd/yyyy", System.Globalization.CultureInfo.GetCultureInfo("en-US")); }

    }

    public static DateTime Now
    {

        get
        {
            TimeZoneInfo timeZoneInfo;
            DateTime dateTime;
            //Set the time zone information to US Mountain Standard Time 
            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            //Get date and time in US Mountain Standard Time 
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            return dateTime;
        }
    }
    public static DateTime ConvertDateTime(string datetime, string format = "MM/dd/yyyy HH:mm:ss tt")
    {
        try
        {
            return DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.GetCultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
        }
        catch (Exception ex)
        {

            return DateTime.Now;
        }

    }


    public static string ConvertDateTimeToSql(DateTime datetime)
    {
        try
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch (Exception ex)
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }


    public static DateTime ConvertDate(string datetime, string format = "MM/dd/yyyy")
    {
        try
        {
            //return DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.GetCultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
             DateTime objDT;
             DateTime.TryParseExact(datetime, format, System.Globalization.CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out objDT);
             return objDT;
        }
        catch (Exception ex)
        {

            return DateTime.MinValue;
        }

    }
    public static string ConvertDateToString(string datetime, string format = "MM/dd/yyyy")
    {
        try
        {
            return DateUtil.Format(DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None), "MM/dd/yyy HH:mm:ss tt");
        }
        catch (Exception ex)
        {

            return DateUtil.DateNow;
        }
    }
    public static string ConvertDateToString(string datetime, string format = "MM/dd/yyyy", string output = "MM/dd/yyyy")
    {
        try
        {
            return DateUtil.Format(DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None), output);
        }
        catch (Exception ex)
        {

            return DateUtil.DateNow;
        }
    }


    public static string GetMaxDateTime_IfNull(string datetime)
    {
        string retunValue = datetime;
        try
        {
            if(string.IsNullOrEmpty(datetime))
            {
                retunValue = "01/01/3000 00:00";
            }
        }
        catch (Exception ex)
        {

            return DateUtil.DateNow;
        }

        return retunValue;
    }




}


