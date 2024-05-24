using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

public class Utils
{
    private static Hashtable hashtable = new Hashtable();
    private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private static Cache cache = HttpContext.Current.Cache;
    private static readonly System.Random randNum = new System.Random();
    public static bool Valid_Error = true;
    public Utils()
    {

    }
    public static string KillChars(string strInput)
    {
        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            //string[] arrBadChars = new string[] {"--", "xp_", "sysobjects", "syscolumns", "'", "1=1", };
            //result = strInput.Trim().Replace("'", "''");
            //result = strInput.Replace("%20", " ");
            //for (int i = 0; i < arrBadChars.Length; i++)
            //{
            //    string strBadChar = arrBadChars[i].ToString();
            //    result = result.Replace(strBadChar, "");
            //}
            result = strInput.Trim();
        }
        return result;
    }

    public static string QuoteRemove(string strInput)
    {
        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            result = strInput.Trim().Replace(@"""", "");
        }
        return result;
    }

    public static string CommaSQLAdd(string strInput)
    {
        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            result = "," + strInput.Trim().Trim(',') + ",";
        }
        return result;
    }
    public static string CommaSQLRemove(string strInput)
    {
        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            result = strInput.Trim().Trim(',');
        }
        return result;
    }
    public static string DateTimeToSQLDate(DateTime datetime)
    {
        if (datetime == null)
            datetime = DateTime.Now;
        return datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
    public static DateTime DateTimeString_To_DateTime(string DateTimeString)
    {

        DateTime _dateTime = new DateTime();
        try
        {
            if (!string.IsNullOrEmpty(DateTimeString.Trim()))
                _dateTime = DateTime.ParseExact(DateTimeString, "dd/MM/yyyy HH:mm", null);
            else
                _dateTime = DateTime.Now;
        }
        catch
        {
            _dateTime = DateTime.Now;
        }

        return _dateTime;
    }
    public static string DateTimeString_To_DateTimeSql(string DateTimeString)
    {
        return DateTimeToSQLDate(DateTimeString_To_DateTime(DateTimeString));
    }
    public static string CommaTrim(string strInput)
    {
        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            result = strInput.Trim().Trim(',').Replace(",", ", ");
        }
        return result;
    }
    public static string GetControlAdmin()
    {
        var control = HttpContext.Current.Request.QueryString["control"];
        if (string.IsNullOrEmpty(control))
            control = ConvertUtility.ToString(HttpContext.Current.Request.RequestContext.RouteData.Values["control"]).ToLower();
        else
            control = control.ToLower();
        return control;
    }
    public static string GetFolderControlAdmin()
    {
        var folder = HttpContext.Current.Request.QueryString["folder"];
        if (string.IsNullOrEmpty(folder))
            folder = ConvertUtility.ToString(HttpContext.Current.Request.RequestContext.RouteData.Values["folder"]).ToLower();
        else
            folder = folder.ToLower();
        return folder;
    }
    public static string GetEditControl()
    {
        var control = GetControlAdmin();
        var folder = GetFolderControlAdmin();
        control = control.Replace("list", "update");
        string link = "/admin/" + folder + "/" + control + "";
        return link;
    }
    public static string GetViewControl()
    {
        var control = GetControlAdmin();
        var folder = GetFolderControlAdmin();
        control = control.Replace("update", "list");
        string link = "/admin/" + folder + "/" + control + "";
        return link;
    }
    public static string GetActionAdmin()
    {
        var control = HttpContext.Current.Request.QueryString["action"];
        if (string.IsNullOrEmpty(control))
            control = ConvertUtility.ToString(HttpContext.Current.Request.RequestContext.RouteData.Values["action"]).ToLower();
        else
            control = control.ToLower();
        return control;
    }
    public static string KillCharEmail(string strInput)
    {

        string result = "";
        if (!String.IsNullOrEmpty(strInput))
        {
            string[] arrBadChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_", "sysobjects", "syscolumns", "'", "1=1", "truncate", "table" };
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
    public static Int32 GetUserOnline()
    {
        Int32 userOnline = Membership.Provider.GetNumberOfUsersOnline();
        return userOnline;
    }
    public static bool DownloadFile(HttpResponse Response, string filePath)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(filePath);
        string fileName = file.Name;
        // filePath = d:/folder/fileName
        FileStream myFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

        //Reads file as binary values
        BinaryReader _BinaryReader = new BinaryReader(myFile);

        //Ckeck whether user is eligible to download the file
        if (IsEligibleUser())
        {
            //Check whether file exists in specified location
            if (file.Exists)
            {
                try
                {
                    long startBytes = 0;
                    int nDownloadBlock = 0;
                    string lastUpdateTiemStamp = File.GetLastWriteTimeUtc(filePath).ToString("r");
                    string _EncodedData = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTiemStamp;

                    Response.Clear();
                    Response.Buffer = false;
                    Response.AddHeader("Accept-Ranges", "bytes");
                    Response.AppendHeader("ETag", "\"" + _EncodedData + "\"");
                    Response.AppendHeader("Last-Modified", lastUpdateTiemStamp);
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
                    Response.AddHeader("Content-Length", (file.Length - startBytes).ToString());
                    Response.AddHeader("Connection", "Keep-Alive");
                    Response.ContentEncoding = Encoding.UTF8;

                    //Send data
                    _BinaryReader.BaseStream.Seek(startBytes, SeekOrigin.Begin);

                    //Dividing the data in 10000 bytes package
                    nDownloadBlock = 10000; // For small sized file set its file size in
                    int maxCount = (int)Math.Ceiling((file.Length - startBytes + 0.0) / nDownloadBlock);

                    //Download in block of 10000 bytes
                    int i;
                    for (i = 0; i < maxCount && Response.IsClientConnected; i++)
                    {
                        Response.BinaryWrite(_BinaryReader.ReadBytes(nDownloadBlock));
                        Response.Flush();
                    }
                    //if blocks transfered not equals total number of blocks
                    if (i < maxCount)
                        return false;
                    return true;
                }
                catch
                {

                    return false;
                }
                finally
                {
                    Response.End();
                    _BinaryReader.Close();
                    myFile.Close();
                }
            }
        }
        return false;

        //Ex : Utils.downloadFile(Response, Server.MapPath("Download/DOCINSIDE.rar"));
    }//End
    public static bool checkUploadFile(HtmlInputFile pControl, Page page, string strServerFolder, string strExtension, int maxSize)
    {

        string getExt = Path.GetExtension(pControl.Value).ToUpper().Trim();
        string strFiletmp = "";
        string strPathDesc = "";

        try
        {
            if (pControl.PostedFile.ContentLength < maxSize)
            {
                if (strExtension.ToUpper().Trim().IndexOf(getExt) == -1)
                    return false;

                strFiletmp = pControl.Value.Trim();
                strFiletmp = Path.GetFileName(strFiletmp);
                if (File.Exists(page.Server.MapPath(".") + strServerFolder + strFiletmp))
                    return false;


                strPathDesc = page.Server.MapPath(".") + "\\" + strServerFolder + "\\" + strFiletmp;
                pControl.PostedFile.SaveAs(strPathDesc);
                System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);
            }
            else
                return false;

        }

        catch (Exception ex)
        {
            return false;
        }



        return true;
    }
    public static void HitCounterWrite(string total)
    {
        try
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(HttpContext.Current.Server.MapPath("Visitor.txt"));
            if (fileInfo.IsReadOnly == true)
            {
                fileInfo.IsReadOnly = false;
            }
            StreamWriter sw = fileInfo.CreateText();
            sw.WriteLine(total);
            sw.Flush();
            sw.Close();
        }
        catch (Exception)
        {

        }
        /*
        TextWriter tw = new StreamWriter(HttpContext.Current.Server.MapPath("Visitor.txt"));
        tw.WriteLine(total);
        tw.Flush();
        tw.Close();
         * */
        // StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath("Visitor.txt"));
        // sw.WriteLine(total);
        // sw.Flush();
        // sw.Close();
    }
    public static string HitCounterRead()
    {
        string result = "0";
        try
        {
            //TextWriter tw = new StreamWriter("date.txt");
            StreamReader sr = File.OpenText(HttpContext.Current.Server.MapPath("Visitor.txt"));
            result = sr.ReadLine();
            sr.Close();
        }
        catch (Exception)
        {

        }
        return result;
    }
    public static string UploadFile(HtmlInputFile pControl, Page page, string strServerFolder, string strExtension)
    {
        if (Path.GetExtension(pControl.Value).ToUpper().Trim() != strExtension.ToUpper().Trim())
        {
            return "File phải được định dạng là file " + strExtension;
        }
        string strFiletmp = "";
        string strPathDesc = "";
        try
        {
            if (pControl.Value != "")
            {
                strFiletmp = pControl.Value.Trim();
                strFiletmp = Path.GetFileName(strFiletmp);
                if (File.Exists(page.Server.MapPath(".") + strServerFolder + strFiletmp))
                {
                    return "File đã được tải. Vui lòng chọn file khác!";
                }
                strPathDesc = page.Server.MapPath(".") + "\\" + strServerFolder + "\\" + strFiletmp;
                pControl.PostedFile.SaveAs(strPathDesc);
                System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);
            }
            else
            {
                return "Vui lòng chọn file!";
            }
        }
        catch (Exception exp)
        {
            string strErr = exp.Message;
            return "Đường dẫn chứa file không hợp lệ. Vui lòng xem lại.";
        }
        return "Thành công!";

        /*Ex: 
         * <input id="File1" style="width: 288px" type="file" runat="server" />
         * Utils.UploadFile(this.File1, this.Page, "Upload", ".xls");
         * */
    }
    public static string UploadFile(HtmlInputFile file, Page page, string strServerFolder, string strExtension, string UserName, int MaxSize) // Upload nhieu dinh dang va gioi han dung luong file return duong dan luu tren server
    {

        string[] arryExtension = strExtension.Split(';');
        string getExt = Path.GetExtension(file.Value).ToUpper().Trim();
        string strFiletmp = "";
        string strPathDesc = "";
        string strDate = DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year.ToString();
        if (file.PostedFile.ContentLength < MaxSize)
        {
            foreach (string word in arryExtension)
            {
                if (getExt == word.ToUpper().Trim())
                {
                    try
                    {
                        strFiletmp = file.Value.Trim();
                        strFiletmp = Path.GetFileName(UserName + strDate + strFiletmp);
                        strFiletmp = UserName + strDate + strFiletmp;
                        if (File.Exists(page.Server.MapPath(strFiletmp)))
                        {
                            return "";
                        }
                        //strFiletmp = strFiletmp;
                        /*
                        strPathDesc = page.Server.MapPath(strServerFolder + "/" + strFiletmp);
                        fUpload.SaveAs(strPathDesc);
                        System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);*/
                        strPathDesc = page.Server.MapPath(".") + "\\" + strServerFolder + "\\" + strFiletmp;
                        file.PostedFile.SaveAs(strPathDesc);
                        System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);
                    }
                    catch (Exception exp)
                    {
                        string strErr = exp.Message;
                        return "";
                    }
                }
            }
            return strFiletmp;
        }
        else
            return "";
        /*
        string[] arryExtension = strExtension.Split(';');
        //string getExt = Path.GetExtension(pControl.Value).ToUpper().Trim();
        string getExt = System.IO.Path.GetExtension(fUpload.FileName.ToUpper());
        string strFiletmp = "";
        string strPathDesc = "";
        if (fUpload.PostedFile.ContentLength < MaxSize)
        {
            foreach (string word in arryExtension)
            {
                if (getExt == word.ToUpper().Trim())
                {
                    try
                    {
                        strFiletmp = fUpload.FileName;
                        strFiletmp = Path.GetFileName(strFiletmp);
                        if (File.Exists(page.Server.MapPath(strFiletmp)))
                        {
                            return "";
                        }
                        strFiletmp = UserName + "_" + strFiletmp;
                        strPathDesc = page.Server.MapPath(strServerFolder+"/"+ strFiletmp);
                        fUpload.SaveAs(strPathDesc);
                        System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);
                    }
                    catch (Exception exp)
                    {
                        string strErr = exp.Message;
                        return "";
                    }
                }
            }
            return strFiletmp;
        }
        else
            return "";*/

    }
    public static string UploadFile(HtmlInputFile file, Page page, string strServerFolder, string strExtension, string UserName) // Upload nhieu dinh dang return duong dan luu tren server
    {

        string[] arryExtension = strExtension.Split(';');
        string getExt = Path.GetExtension(file.Value).ToUpper().Trim();
        string strFiletmp = "";
        string strPathDesc = "";
        string strDate = DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year.ToString();
        foreach (string word in arryExtension)
        {
            if (getExt == word.ToUpper().Trim())
            {
                try
                {
                    strFiletmp = file.Value.Trim();
                    strFiletmp = Path.GetFileName(strFiletmp);
                    strFiletmp = UserName.Substring(0, UserName.IndexOf("@")) + strDate + strFiletmp;
                    if (File.Exists(page.Server.MapPath(strFiletmp)))
                    {
                        return "";
                    }
                    //strFiletmp = strFiletmp;
                    /*
                    strPathDesc = page.Server.MapPath(strServerFolder + "/" + strFiletmp);
                    fUpload.SaveAs(strPathDesc);
                    System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);*/
                    strPathDesc = page.Server.MapPath(".") + "\\" + strServerFolder + "\\" + strFiletmp;
                    file.PostedFile.SaveAs(strPathDesc);
                    System.IO.File.SetAttributes(strPathDesc, System.IO.FileAttributes.Normal);
                }
                catch (Exception exp)
                {
                    string strErr = exp.Message;
                    return "";
                }
            }
        }
        return strFiletmp;
    }
    public static bool IsEligibleUser()
    {
        return true;
    }
    public static string ConvertDMYtoMDY(string pDDMMYY)
    {
        string temp_Date = pDDMMYY.Trim();
        string mstr_ShortDate = temp_Date.Substring(0, temp_Date.LastIndexOf("/") + 5);
        return mstr_ShortDate.Substring(mstr_ShortDate.IndexOf("/") + 1, mstr_ShortDate.LastIndexOf("/") - mstr_ShortDate.IndexOf("/") - 1) + "/" + mstr_ShortDate.Substring(0, mstr_ShortDate.IndexOf("/")) + "/" + mstr_ShortDate.Substring(mstr_ShortDate.LastIndexOf("/") + 1, mstr_ShortDate.Length - mstr_ShortDate.LastIndexOf("/") - 1);
    }
    public static string ConvertDMYtoMMMddyyyy(string pDDMMYY)
    {
        System.DateTime mDateID = new System.DateTime();
        mDateID = Convert.ToDateTime(ConvertDMYtoMDY(pDDMMYY));

        return mDateID.ToString("MMM dd yyyy");
    }
    public static string ConvertMDYtoDMY(object pMMDDYY)
    {
        if (pMMDDYY.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pMMDDYY.ToString().Trim()).ToString("dd/MM/yy");
        }
        else
            return "";
    }
    public static string ConvertMDYtoDDMMYYYY(object pMMDDYY)
    {
        if (pMMDDYY.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pMMDDYY.ToString().Trim()).ToString("dd/MM/yyyy");
        }
        else
            return "";
    }
    public static string ConvertDMYtoMDY(object pDDMMYY)
    {
        if (pDDMMYY.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("MM/dd/yy");
        }
        else
            return "";
    }
    public static string ConvertDMYtoMMDDYYYY(object pDDMMYY)
    {
        if (pDDMMYY.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("MM-dd-yyyy");
        }
        else
            return "";
    }
    public static string ConvertDMYtoMMDDYYYYHHMM(object pDDMMYY, object pHH, object pMM)
    {
        if (pDDMMYY.ToString().Trim() != "" && pHH.ToString().Trim() != "" && pMM.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("MM-dd-yyyy " + pHH.ToString().Trim() + ":" + pMM.ToString().Trim());
        }
        else
            return "";
    }
    public static string ConvertDMYtoDDMMYYYY(object pDDMMYY)
    {
        if (pDDMMYY.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pDDMMYY.ToString().Trim()).ToString("dd-MMM-yyyy");
        }
        else
            return "";
    }
    public static string ConvertDDMMYYYYtoDMY(object pDDMMMYYYY)
    {
        if (pDDMMMYYYY.ToString().Trim() != "")
        {
            return Convert.ToDateTime(pDDMMMYYYY.ToString().Trim()).ToString("dd/MM/yyyy");
        }
        else
            return "";
    }
    public static void LoadYearToList(DropDownList pCtrlName, int pMinYear, int pMaxYear)
    {
        for (int i = pMinYear; i <= pMaxYear; i++)
        {
            pCtrlName.Items.Add(new ListItem(Convert.ToString(i).Trim(), Convert.ToString(i).Trim()));
        }
    }
    public static void LoadYearToList(DropDownList pCtrlName, int pMinYear)
    {
        int pMaxYear = DateTime.Now.Year;
        for (int i = pMinYear; i <= pMaxYear; i++)
        {
            pCtrlName.Items.Add(new ListItem(Convert.ToString(i).Trim(), Convert.ToString(i).Trim()));
        }
    }
    public static void LoadMonthToList(DropDownList pCtrlName)
    {
        pCtrlName.Items.Add(new ListItem("01", "1"));
        pCtrlName.Items.Add(new ListItem("02", "2"));
        pCtrlName.Items.Add(new ListItem("03", "3"));
        pCtrlName.Items.Add(new ListItem("04", "4"));
        pCtrlName.Items.Add(new ListItem("05", "5"));
        pCtrlName.Items.Add(new ListItem("06", "6"));
        pCtrlName.Items.Add(new ListItem("07", "7"));
        pCtrlName.Items.Add(new ListItem("08", "8"));
        pCtrlName.Items.Add(new ListItem("09", "9"));
        pCtrlName.Items.Add(new ListItem("10", "10"));
        pCtrlName.Items.Add(new ListItem("11", "11"));
        pCtrlName.Items.Add(new ListItem("12", "12"));
    }
    public static void LoadDayToList(DropDownList pCtrlName)
    {
        for (int i = 1; i < 32; i++)
        {
            string temp = i < 10 ? "0" + i.ToString() : i.ToString();
            pCtrlName.Items.Add(new ListItem(temp, i.ToString()));
        }
    }
    public static void LoadWeekToList(DropDownList pCtrlName)
    {
        System.DateTime today = new System.DateTime(System.DateTime.Today.Ticks);
        System.DateTime Dautuan = new System.DateTime();
        System.DateTime Cuoituan = new System.DateTime();
        System.DateTime ThangTuan = new System.DateTime();


        int iDay = today.Day;
        ThangTuan = today.AddDays(7 * 4);
        string strDate;
        string DayOfWeek = today.DayOfWeek.ToString();
        int iDayofWeek = GetDayOfWeek(DayOfWeek);

        //			double i;
        //			i = iDay - 30;
        for (int i = 0; i < 60; i += 7)
        {
            Dautuan = ThangTuan.AddDays(-(iDayofWeek - 2) - i);
            Cuoituan = Dautuan.AddDays(6);

            strDate = Dautuan.Date.ToString("dd/MM/yyyy") + "->" + Cuoituan.Date.ToString("dd/MM/yyyy");
            pCtrlName.Items.Add(new ListItem(strDate, strDate));
        }
    }
    public static int GetDayOfWeek(string strinput)
    {
        switch (strinput)
        {
            case "Sunday":
                return 1;
            case "Monday":
                return 2;
            case "Tuesday":
                return 3;
            case "Wednesday":
                return 4;
            case "Thursday":
                return 5;
            case "Friday":
                return 6;
            case "Saturday":
                return 7;
        }
        return 0;
    }
    public static String AddSpace(int lenght)
    {
        String result = "";
        result = string.Concat(System.Collections.ArrayList.Repeat("&nbsp;", lenght).ToArray());
        return result;
    }
    public static String AddCharacterHtml(String Character, int lenght)
    {

        String result = "";
        result = string.Concat(System.Collections.ArrayList.Repeat(Character, lenght).ToArray());
        return result;
    }
    public static void ShowMessageBox(System.Web.UI.Page pPage, string pMessage)
    {

        string strScript = "<script language=JavaScript>";
        strScript += "alert('" + pMessage + "');";
        strScript += "</script>";

        if (!pPage.IsStartupScriptRegistered("clientScript"))
        {
            pPage.RegisterStartupScript("clientScript", strScript);
        }

        //Ex Utils.ShowMessageBox(this.Page, "Hello Honglk");
    }
    public static void ShowMessageBox(UpdatePanel updatePanel, string message)
    {
        string strScript = "alert('" + message + "');";
        ScriptManager.RegisterStartupScript(updatePanel, updatePanel.GetType(), "alert", strScript, true);
    }
    public static void GoBack(UpdatePanel updatePanel)
    {
        string strScript = "javascript:history.back();";
        ScriptManager.RegisterStartupScript(updatePanel, updatePanel.GetType(), "back", strScript, true);
    }
    public static void ShowMessageBoxAndRefresh(System.Web.UI.Page pPage, string pMessage)
    {
        string pageName = Path.GetFileNameWithoutExtension(pPage.AppRelativeVirtualPath);
        string pageNameFull = Path.GetFileName(pPage.AppRelativeVirtualPath);

        string strScript = "<script language=JavaScript>";
        strScript += "alert('" + pMessage + "');";
        strScript += "window.location ='" + pageNameFull + "'";
        strScript += "</script>";

        if (!pPage.IsStartupScriptRegistered("clientScript"))
        {
            pPage.RegisterStartupScript("clientScript", strScript);
        }

        //Ex Utils.ShowMessageBox(this.Page, "Hello Honglk");
    }
    public static void ShowMessageBoxAndRefresh(System.Web.UI.Page pPage, string pMessage, string url)
    {

        string strScript = "<script language=JavaScript>";
        strScript += "alert('" + pMessage + "');";
        strScript += "window.location ='" + url + "'";
        strScript += "</script>";

        if (!pPage.IsStartupScriptRegistered("clientScript"))
        {
            pPage.RegisterStartupScript("clientScript", strScript);
        }

        //Ex Utils.ShowMessageBox(this.Page, "Hello Honglk");
    }
    public static int SafeDataInteger(Object pValue)
    {
        if ((pValue is DBNull))
        {
            return 0;
        }
        else if (Convert.ToString(pValue) == "")
        {
            return 0;
        }
        return Convert.ToInt32(pValue);
    }
    public static string GetLanguage(HttpRequest Request, System.Web.SessionState.HttpSessionState Session)
    {
        string lang = "vi";
        if (string.IsNullOrEmpty(Request["Lang"]))
        {
            /*
            if (Session["Lang"] == null)
            {
                lang = "vi";
            }
            else
            {
                lang = Session["Lang"].ToString();
            }
            */
            lang = "vi";
        }
        else
        {
            lang = Request["Lang"];
        }
        return lang;

        //Ex Utils.GetLanguage(Request,Session);
    }
    public static string GetCulture(HttpRequest Request, System.Web.SessionState.HttpSessionState Session)
    {
        string culture = "vi-VN";
        string lang = "";
        if (string.IsNullOrEmpty(Request["Lang"]))
        {
            lang = "vi";
            /*
            if (Session["Lang"] == null)
            {
                lang = "vi";
            }
            else
            {
                lang = Session["Lang"].ToString();
            }
             */
        }
        else
        {
            lang = Request["Lang"];
        }
        culture = lang.Equals("vi") ? "vi-VN" : "en-US";
        return culture;
    }
    public static void LoadDataToList(DropDownList pCtrlName, string strProcedureName)
    {
        try
        {

            DatabaseHelper DB = new DatabaseHelper();
            DataTable dt = DB.ExecuteDataTable(strProcedureName);
            foreach (DataRow row in dt.Rows)
            {
                pCtrlName.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
            }
            dt.Dispose();
        }
        catch (Exception ex)
        {
            Utils.WriteLogError("Utils LoadDataToList", ex.Message);
        }
    }
    public static void LoadDataToRadioButtonList(RadioButtonList rblName, string strProcedureName)
    {
        try
        {

            DatabaseHelper DB = new DatabaseHelper();
            DataTable dt = DB.ExecuteDataTable(strProcedureName);
            foreach (DataRow row in dt.Rows)
            {
                rblName.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
            }
            dt.Dispose();
        }
        catch (Exception)
        {
        }
    }
    public static void LoadDataToListBox(ListBox pCtrlName, string strProcedureName)
    {
        try
        {

            DatabaseHelper DB = new DatabaseHelper();
            DataTable dt = DB.ExecuteDataTable(strProcedureName);
            foreach (DataRow row in dt.Rows)
            {
                pCtrlName.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
            }
            dt.Dispose();
        }
        catch (Exception)
        {
        }
    }
    public static void LoadDataToCheckBoxList(CheckBoxList cbList, string strProcedureName)
    {
        try
        {

            DatabaseHelper DB = new DatabaseHelper();
            DataTable dt = DB.ExecuteDataTable(strProcedureName);
            foreach (DataRow row in dt.Rows)
            {
                cbList.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
            }
            dt.Dispose();
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }
    public static string LoginSave(string CustomerID, bool isRemember)
    {
        if (isRemember)
        {
            HttpContext.Current.Response.Cookies.Clear();

            // Set the new expiry date - to thirty days from now
            DateTime expiryDate = DateTime.Now.AddDays(365);

            // Create a new forms auth ticket
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, CustomerID, DateTime.Now, expiryDate, true, String.Empty);

            // Encrypt the ticket
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // Create a new authentication cookie - and set its expiration date
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authenticationCookie.Expires = ticket.Expiration;

            // Add the cookie to the response.
            HttpContext.Current.Response.Cookies.Add(authenticationCookie);
        }
        return "";
    }
    public static void LoadDataToGrid(DataGrid dtgGrid, string strProcedureName)
    {
        DatabaseHelper DB = new DatabaseHelper();
        DataTable dt = DB.ExecuteDataTable(strProcedureName);
        DataView dv = new DataView(dt);
        dtgGrid.DataSource = dv;
        dtgGrid.DataBind();
        dtgGrid.Columns[0].Visible = false;
        // dtgGrid.Items[0].Visible = false;
    }
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public static string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }
    public static string RandomStringAndNumber(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static string RandomStringNumberANDCharacter(int length)
    {
        Random random = new Random();
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static string GetPassword()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomString(4, true));
        builder.Append(RandomNumber(1000, 9999));
        builder.Append(RandomString(2, false));
        return builder.ToString();
    }
    public static string ActiceCode(int size)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomString(size, true));
        builder.Append(RandomNumber(1000, 9999));
        builder.Append(RandomString(size, false));
        return builder.ToString();
    }
    public static bool IsValidEmailAddress(string sEmail)
    {
        if (sEmail == null)
        {
            return false;
        }

        int nFirstAT = sEmail.IndexOf('@');
        int nLastAT = sEmail.LastIndexOf('@');

        if ((nFirstAT > 0) && (nLastAT == nFirstAT) &&
        (nFirstAT < (sEmail.Length - 1)))
        {
            // address is ok regarding the single @ sign
            return (Regex.IsMatch(sEmail, @"(\w+)@(\w+)\.(\w+)"));
        }
        else
        {
            return false;
        }
    }
    public static void OpenNewWindow(System.Web.UI.Page pPage, string pURL, string pTitle)
    {
        string strScript = "<script language=JavaScript>";
        strScript += "window.open('" + pURL + "','" + pTitle + "')";
        strScript += "</script>";
        if (!pPage.IsStartupScriptRegistered("clientScript"))
            pPage.RegisterStartupScript("clientScript", strScript);
    }
    public static void OpenNewWindowPopup(System.Web.UI.Page pPage, string pURL, string pTitle)
    {
        string strScript = "<script language=JavaScript>";
        strScript += "ShowDialog('" + pURL + "','" + pTitle + "')";
        strScript += "</script>";
        if (!pPage.IsStartupScriptRegistered("clientScript"))
            pPage.RegisterStartupScript("clientScript", strScript);
    }
    public static string checkFileSize(FileUpload fUploadResume, int p)
    {
        throw new NotImplementedException();
    }
    public static string RemoveUnicode(string s)
    {
        string stFormD = s.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();

        for (int ich = 0; ich < stFormD.Length; ich++)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(stFormD[ich]);
            }
        }
        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }
    public static void getCookie(string strURl)
    {
        HttpCookie appCookie = new HttpCookie("URL");
        appCookie.Value = strURl;
        appCookie.Expires = DateTime.Now;
    }
    public static int dateDiff(String datePart, DateTime startDate, DateTime endDate)
    {
        int result = 0;
        TimeSpan timeSpan = endDate.Subtract(startDate);
        if (datePart.ToUpper().Equals("HOUR"))
        {
            result = timeSpan.Hours;
        }
        else if (datePart.ToUpper().Equals("MINUTE"))
        {
            result = timeSpan.Minutes;
        }
        else if (datePart.ToUpper().Equals("SECOND"))
        {
            result = timeSpan.Seconds;
        }
        return result;
    }
    public static double DatimeTounix(DateTime datetime)
    {
        double result = 0;
        TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0));
        try
        {
            result = (double)span.TotalSeconds;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
        return result;
    }
    public static DateTime unixToDatetime(double unix)
    {
        DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
        try
        {
            dateTime = dateTime.AddSeconds(unix);
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
        return dateTime;

    }
    public static void moveFile(string srcFile, String descFile)
    {
        try
        {
            if (File.Exists(descFile))
            {
                File.Delete(descFile);
            }
            File.Move(srcFile, descFile);
        }

        catch (FileNotFoundException ex)
        {
            logger.Error(ex);
            logger.Error("File " + srcFile + " khong ton tai");
        }

    }
    public static void copyFile(string srcFile, String descFile)
    {
        try
        {
            File.Copy(srcFile, descFile, true);
        }
        catch (FileNotFoundException ex)
        {
            logger.Error(ex);
            logger.Error("File " + srcFile + " khong ton tai");
        }

    }
    public static void createFolder(String pathFolder)
    {
        if (!Directory.Exists(pathFolder))
        {
            DirectoryInfo dir1 = new DirectoryInfo(pathFolder);
            dir1.Create();
        }
    }
    public static string getParameter(HttpRequest Request, int index)
    {
        string param = Request["param"];
        string parameter = "";
        try
        {
            //   Admin/Home/1 
            string[] arrParam = param.Split('/');
            parameter = arrParam[index];
        }
        catch (Exception)
        {
            parameter = "";
        }
        return parameter;
    }
    public static void insertCache(string cacheName, object value, int timeSpan = 10, bool force = false)
    {
        if (force == false)
        {
            if (cache[cacheName] != null)
            {
                cache.Insert(cacheName, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(timeSpan));
            }
        }
        else
        {
            cache.Insert(cacheName, value, null, DateTime.MaxValue, TimeSpan.FromMinutes(timeSpan));
        }
    }
    public static object getCache(string cacheName)
    {

        object result = cache[cacheName];
        if (result != null)
        {
            return result;
        }
        else
        {
            result = "";
        }
        return result;
    }
    public static void WriteLogError(string strTitle, string strException)
    {
        try
        {
            //if (logger.IsErrorEnabled)
            //{

            //logger.Error("--------------------------------");
            logger.Error(strTitle + ": " + strException);

            //logger.Error("--------------------------------");
            //}

        }
        catch (Exception ex)
        {

        }

    }
    public static void WriteLogInfo(string strTitle, string strInfo)
    {
        try
        {
            //if (logger.IsErrorEnabled)
            //{
            //   logger.Info("--------------------------------");
            logger.Info(strTitle + ": " + strInfo);
            //   logger.Info("--------------------------------");
            //}

        }
        catch (Exception ex)
        {
            Utils.WriteLogError("Utils WriteLogInfo ", ex.Message);
        }

    }
    public static void RedirectToPageError()
    {
        System.Web.HttpContext.Current.Response.Redirect("/ErrorPage/InternalServerError.htm", false);
    }
    public static String GetOSName(String AgentName)
    {
        String Name = "UnKnown";
        if (AgentName.IndexOf("Windows CE") > 0)
            Name = "Windows CE";
        else if (AgentName.IndexOf("Windows 95") > 0)
            Name = "Windows 95";
        else if (AgentName.IndexOf("Windows 98") > 0)
            Name = "Windows 98";
        else if (AgentName.IndexOf("Windows NT 4.0") > 0)
            Name = "Microsoft Windows NT 4.0";
        else if (AgentName.IndexOf("Windows NT 5.0") > 0)
            Name = "Windows 2000";
        else if (AgentName.IndexOf("Windows NT 5.01") > 0)
            Name = "Windows 2000";
        else if (AgentName.IndexOf("Windows NT 5.1") > 0)
            Name = "Windows XP";
        else if (AgentName.IndexOf("Windows NT 5.2") > 0)
            Name = "Windows Server 2003";
        else if (AgentName.IndexOf("Windows NT 6.0") > 0)
            Name = "Windows Vista";
        else if (AgentName.IndexOf("Windows NT 6.1") > 0)
            Name = "Windows 7";
        else if (AgentName.IndexOf("Windows NT 6.2") > 0)
            Name = "Windows 8";
        else if (AgentName.IndexOf("Windows NT 6.3") > 0)
            Name = "Windows 8.1";
        else if (AgentName.IndexOf("Linux") > 0)
            Name = "Linux";
        else if (AgentName.IndexOf("Mac OS") > 0)
            Name = "Mac OS";
        return Name;
    }
    #region SetViewStateMode

    public static void SetViewStateMode(ControlCollection Controls, bool ViewStateMode)
    {
        foreach (Control objControl in Controls)
        {
            objControl.ViewStateMode = ViewStateMode ? System.Web.UI.ViewStateMode.Enabled : System.Web.UI.ViewStateMode.Disabled;
            objControl.EnableViewState = ViewStateMode;
        }
    }

    #endregion
    public static void AddControls(ControlCollection page, ArrayList controlList)
    {
        foreach (Control c in page)
        {
            if (c.ID != null && c.GetType().ToString().Contains("Label"))
            {
                controlList.Add(c);
            }

            if (c.HasControls())
            {
                AddControls(c.Controls, controlList);
            }
        }
    }
    public static void SetLanguage(string ResourceLanguage, ControlCollection page)
    {
        ArrayList controlList = new ArrayList();
        Utils.AddControls(page, controlList);
        foreach (object ctr in controlList)
        {
            Label lbl = (Label)ctr;
            if (lbl.Attributes["K"] != null)
            {
                object Key = HttpContext.GetGlobalResourceObject(ResourceLanguage, lbl.Attributes["K"].ToString());
                if (Key != null)
                {
                    lbl.Text = Key.ToString();
                }
            }
        }
    }
    public static string GetLanguageCode()
    {
        string lg = "vn";
        string strLanguageCode = HttpContext.Current.Request.QueryString["lg"];
        if (!string.IsNullOrEmpty(strLanguageCode))
        {
            HttpCookie oCookieSet = new HttpCookie("LANGUAGE");
            oCookieSet.Value = strLanguageCode;
            HttpContext.Current.Response.SetCookie(oCookieSet);
            return strLanguageCode;
        }
        else
        {
            if (HttpContext.Current.Session["location"] != null)
            {
                return HttpContext.Current.Session["location"].ToString().ToLower();
            }
            else return "vn";
        }

    }
    public static void ResetTextBox(params TextBox[] txts)
    {
        foreach (TextBox txt in txts)
        {
            txt.Text = "";
        }
    }
    public static string IPAddress
    {
        get
        {
            //System.Net.IPHostEntry host;
            //string localIP = "";
            //host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            //foreach (System.Net.IPAddress ip in host.AddressList)
            //{
            //    if (ip.AddressFamily.ToString() == "InterNetwork")
            //    {
            //        localIP = ip.ToString();
            //    }
            //}
            //return localIP;
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
    public static string User_Agent
    {
        get
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();
        }
    }
    public static bool CheckCustmerLogin()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
            return true;
        else
            return false;
    }
    public static int CheckLogin(string username, string password, string remember = "0")
    {
        DataTable dtMember = new DataTable();
        DatabaseHelper db = new DatabaseHelper();

        string password_encry = Crypto.EncryptData(Crypto.KeyCrypto, Utils.KillChars(password)); // EncryptionUtil.Pass_Encrypt(password);
        try
        {
            dtMember = db.ExecuteDataTable("SELECT customerid,username,emailaddress,password,status  FROM CMRC_Customers where emailaddress='" + username + "'", CommandType.Text, ConnectionState.CloseOnExit);

        }
        catch (Exception ex)
        {


        }

        if (dtMember.Rows.Count > 0)
        {
            DataRow drMemeber = dtMember.Rows[0];

            if (drMemeber["password"].ToString() != password_encry)
            {
                HttpContext.Current.Session["login_error"] = "Sai mật khẩu, vui lòng kiểm tra lại ";
                return 0;
            }

            //if (drMemeber["status"].ToString() == "1")
            //{
            Hashtable hashtable = new Hashtable();
            hashtable.Add("iplastlogin", Utils.IPAddress);
            hashtable.Add("lastlogin", DateUtil.DateNow);
            bool Updated = db.Update(hashtable, "customerid", drMemeber["customerid"], "CMRC_Customers");

            //FormsAuthentication.RedirectFromLoginPage(drMemeber["customerid"].ToString(), ConvertUtility.ToBoolean(remember));
            FormsAuthentication.SetAuthCookie(drMemeber["customerid"].ToString(), ConvertUtility.ToBoolean(remember));

            return ConvertUtility.ToInt32(drMemeber["customerid"]);
            //}
            //else
            //{
            //    HttpContext.Current.Session["login_error"] = "Tài khoản chưa kích hoạt ";
            //    return 0;
            //}
        }
        else
        {
            HttpContext.Current.Session["login_error"] = "Tài khoản không tồn tại";
            return 0;
        }


    }
    public static void CheckAccount()
    {

        //using (DataDataContext ctx = new DataDataContext())
        //{
        //    Global.objAdmin = ctx.Administrators.FirstOrDefault(x => x.IDAdmin == Int16.Parse(HttpContext.Current.Session["alogin_" + Config.SESSION_KEY + "idadmin"].ToString()));
        //}

    }
    public static bool Permission_Menu(int idadmin, string permission)
    {
        //if (!String.IsNullOrEmpty(permission))
        //{
        //    string[] str = permission.Split('-');
        //    string controler = str[0];
        //    string mod = str[1];
        //    Administrator Admin = Global.objAdmin;

        //    if (Admin != null)
        //    {
        //        if (Admin.IDGroup == 1)
        //            return true;
        //        else
        //        {
        //            string sql = "SELECT G.permission from AdminGroup G left join administrator U on(G.idgroup=U.idgroup) WHERE U.idadmin='" + idadmin + "'";
        //            string group_permission = "";
        //            DataRow dr;

        //            //if (HttpContext.Current.Session["group_permission"] != null)
        //            //    //dr = ReflectorUtil.SetSession<DataRow>("drAdminGroup",dr);
        //            //    group_permission = HttpContext.Current.Session["group_permission"].ToString();
        //            //else
        //            //{
        //            //    dr = Global.db.ExecuteDataRow(sql);
        //            //    HttpContext.Current.Session["group_permission"] = dr["permission"];
        //            //}
        //            dr = Global.db.ExecuteDataRow(sql);

        //            //  string[] array = dr["permission"].ToString().Split(',');

        //            try
        //            {
        //                List<AdminGroupPermission> objs = Utils.Deserialize<List<AdminGroupPermission>>(dr["permission"].ToString().Trim());
        //                AdminGroupPermission obj = objs.FirstOrDefault(x => x.AGP_Controller == controler);
        //                if (obj == null)
        //                {
        //                    return false;
        //                }
        //                else
        //                {
        //                    Admin_Permission admin_pre = obj.AdminPermission.FirstOrDefault(x => x.AP_Model == mod);
        //                    if (admin_pre == null)
        //                    {
        //                        return false;
        //                    }
        //                    else
        //                    {
        //                        if (admin_pre.Checked)
        //                            return true;
        //                        else return false;
        //                    }

        //                }

        //            }
        //            catch (Exception ex)
        //            {

        //                return false;
        //            }
        //        }
        //    }
        //}
        return false;
    }
    public static string Checked(bool check)
    {
        if (check)
            return "checked";
        else return "";

    }
    public static string Serialize<T>(T value)
    {
        if (value == null)
        {
            return null;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(T));
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Encoding = Encoding.UTF8;
        settings.Indent = false;
        settings.OmitXmlDeclaration = false;

        using (StringWriter textWriter = new StringWriter())
        {

            using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
            {
                serializer.Serialize(xmlWriter, value);
            }
            return textWriter.ToString();
        }
    }
    public static T Deserialize<T>(string xml)
    {
        if (string.IsNullOrEmpty(xml))
        {
            return default(T);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(T));
        XmlReaderSettings settings = new XmlReaderSettings();

        using (StringReader textReader = new StringReader(xml))
        {
            using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
            {
                try
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
                catch (Exception ex)
                {
                    return default(T);
                }

            }
        }
    }
    public static bool Vaild_Field(List<Valid> objs)
    {
        List<Error_Field> error_list = new List<Error_Field>();
        bool Vaild = true;
        foreach (Valid obj in objs)
        {
            string[] rules = obj.Rule.Split('|');
            foreach (string rule in rules)
            {

                if (rule.Contains("required"))
                {

                    if (String.IsNullOrEmpty(obj.Value))
                    {
                        error_list.Add(new Error_Field(obj.Field, "Vui lòng nhập thông tin."));
                        Vaild = false;
                    }
                }
                else if (rule.Contains("min_length"))
                {
                    if (!String.IsNullOrEmpty(obj.Value))
                    {
                        string value = rule;
                        value = value.Replace("min_length", "");
                        value = value.Replace("[", "");
                        value = value.Replace("]", "");
                        int length = 0;
                        try
                        {
                            length = Int16.Parse(value);
                            if (obj.Value.Length < length)
                            {
                                error_list.Add(new Error_Field(obj.Field, "Vui lòng nhập ít nhất" + " " + value));
                                Vaild = false;
                            }

                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                else if (rule.Contains("isequal"))
                {
                    if (!String.IsNullOrEmpty(obj.Value))
                    {
                        string value = rule;
                        value = value.Replace("isequal", "");
                        value = value.Replace("[", "");
                        value = value.Replace("]", "");
                        Valid o = objs.FirstOrDefault(x => x.Field == value);
                        if (o != null)
                        {
                            if (obj.Value != o.Value)
                            {
                                error_list.Add(new Error_Field(obj.Field, "Dữ liệu nhập vào không giống nhau."));
                                Vaild = false;
                            }
                        }
                    }
                }
                else if (rule.Contains("isemail"))
                {
                    if (!String.IsNullOrEmpty(obj.Value))
                    {
                        if (!Utils.IsValidEmailAddress(obj.Value))
                        {
                            error_list.Add(new Error_Field(obj.Field, "Địa chỉ Email hợp lệ."));
                        }
                    }
                }

                else if (rule.Contains("compare"))
                {
                    if (!String.IsNullOrEmpty(obj.Value))
                    {
                        string value = rule;
                        value = value.Replace("compare", "");
                        value = value.Replace("[", "");
                        value = value.Replace("]", "");

                        if (!String.IsNullOrEmpty(value))
                        {
                            if (obj.Value != value)
                            {
                                error_list.Add(new Error_Field(obj.Field, "Dữ liệu nhập vào không giống nhau."));
                                Vaild = false;
                            }
                        }
                    }
                }


            }

        }
        HttpContext.Current.Session["error_" + Config.SESSION_KEY] = Serialize<List<Error_Field>>(error_list);
        return Vaild;
    }
    public static string Render_Error(string field)
    {
        string error = "";
        if (HttpContext.Current.Session["error_" + Config.SESSION_KEY] != null)
        {
            List<Error_Field> error_list = (List<Error_Field>)Deserialize<List<Error_Field>>(HttpContext.Current.Session["error_" + Config.SESSION_KEY].ToString());
            Error_Field obj = error_list.FirstOrDefault(x => x.Field == field);
            if (obj != null)
            {
                error = "<label for='" + obj.Field + "' class='error'>" + obj.Error + "</label>";
            }

        }
        return error;
        //
    }
    public static string HtmlEncode(object obj)
    {
        if (obj != null)
            return System.Web.HttpUtility.HtmlEncode(obj.ToString());
        else
            return "";
    }
    public static string HtmlDecode(object obj)
    {
        if (obj != null)
            return System.Web.HttpUtility.HtmlDecode(obj.ToString());
        else
            return "";
    }
    public static string ProductImageCrop(string Name, int Size)
    {
        return C.ROOT_URL + "/upload/img/" + Name + "?quality=100&mode=crop&width=" + Size + "&height=" + Size;
    }
    /// <summary>
    /// Hàm load ảnh sản phẩm
    /// </summary>
    /// <param name="Name">Tên ảnh</param>
    /// <param name="Size">Kích thước ngang</param>
    /// <param name="LayoutType">1 là vuông, 2 là tự do</param>
    /// <returns></returns>
    public static string ArticleImageCrop(string Name)
    {
        return C.ROOT_URL + C.DS + "upload/images/article/" + Name + "?quality=100&mode=crop&anchor=topleft&width=360&height=240";
    }
    public static string ProductLoading()
    {
        return C.TEMPLATE_PATH_FULL + "/img/blank.jpg";
    }
    public static string Show_Url_Link(object url)
    {
        //if (!String.IsNullOrEmpty(url))
        //{
        //    if ()
        //}
        return url.ToString();
    }

    public static string GetUrlInfo
    {
        get
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Uri uri = new Uri(url);
            string scheme = uri.Scheme; // http, https, etc.
            string host = uri.Host; // domain.com
            string path = uri.AbsolutePath; // /category/product.html
            string query = uri.Query; // ?id=123&page=2
            string urlInfo = string.Format("{0}://{1}{2}{3}", scheme, host, path, query);
            return urlInfo;
        }
    }

    public static string GetDomainName
    {
        get
        {
            string url = HttpContext.Current.Request.Url.ToString();
            Uri uri = new Uri(url);
            string host = uri.Host;
            return host;
        }
    }

    public static string Show_Price_Currency(object obj, string currency = "xeng", bool shownull = false)
    {
        if (obj != null)
        {
            try
            {
                Double value = Convert.ToDouble(obj.ToString());
                if (value > 0)
                {
                    string price = value.ToString("#,##0");
                    if (currency == "xeng")
                        price = price + " Xèng";
                    else if (currency == "vnd")
                        price = "$ " + price;
                    else if (currency == "won")
                        price = price + " WON";

                    return price;
                }
                else
                {
                    if (shownull)
                        return "0";
                    else
                        return "";
                }
            }
            catch (Exception)
            {
                if (shownull)
                    return "0";
                else
                    return "";
            }
        }
        else
        {
            if (shownull)
                return "0";
            else
                return "";
        }
    }
    public static bool Upload_Image(string folder, HttpPostedFile file, ref string filename, string _type = "", int thumb_width = 100, int thumb_height = 100)
    {

        int maxsize = 10000000; //10Mb
        string TargetLocation = HttpContext.Current.Server.MapPath("~/upload/" + folder);
        try
        {
            if (file.ContentLength > 0)
            {
                //Determining file name. You can format it as you wish.

                string fileExt = System.IO.Path.GetExtension(file.FileName);
                if (file.ContentType == "image/gif" || file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/pjpeg" || file.ContentType == "image/x-png" || file.ContentType == "image/png" || file.ContentType == "image/png" && file.ContentLength < maxsize)
                {
                    string FileName = StringUtil.GetStringDate() + "_" + StringUtil.CreateRandomString(10) + fileExt;
                    filename = FileName;
                    //Determining file size.
                    int FileSize = file.ContentLength;
                    //Creating a byte array corresponding to file size.
                    byte[] FileByteArray = new byte[FileSize];
                    //Posted file is being pushed into byte array.
                    file.InputStream.Read(FileByteArray, 0, FileSize);
                    //Uploading properly formatted file to server.
                    file.SaveAs(TargetLocation + C.DS + FileName);
                    if (_type == "resize")
                        ImageUitl.ResizeImage(thumb_width, thumb_height, "~/upload/" + folder + C.DS + FileName, "~/upload/" + folder + C.DS + "thumb_" + FileName);
                }

            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static bool Upload_Avatar(string folder, HttpPostedFile file, ref string filename, string MemberID)
    {

        int maxsize = 4000000; //4Mb
        string TargetLocation = HttpContext.Current.Server.MapPath("~/upload/" + folder);
        try
        {
            if (file.ContentLength > 0)
            {
                string fileExt = System.IO.Path.GetExtension(file.FileName);
                if (file.ContentType == "image/gif" || file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/pjpeg" || file.ContentType == "image/x-png" || file.ContentType == "image/png" || file.ContentType == "image/png" && file.ContentLength < maxsize)
                {
                    string FileName = MemberID + fileExt;
                    filename = FileName;
                    //Determining file size.
                    int FileSize = file.ContentLength;
                    //Creating a byte array corresponding to file size.
                    byte[] FileByteArray = new byte[FileSize];
                    //Posted file is being pushed into byte array.
                    file.InputStream.Read(FileByteArray, 0, FileSize);
                    //Uploading properly formatted file to server.
                    file.SaveAs(TargetLocation + C.DS + FileName);
                }

            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static bool Upload_Image_Order(string folder, HttpPostedFile file, ref string filename)
    {

        int maxsize = 4000000; //4Mb
        string TargetLocation = HttpContext.Current.Server.MapPath("~/upload/" + folder);
        try
        {
            if (file.ContentLength > 0)
            {
                string fileExt = System.IO.Path.GetExtension(file.FileName);
                if (file.ContentType == "image/gif" || file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/pjpeg" || file.ContentType == "image/x-png" || file.ContentType == "image/png" || file.ContentType == "image/png" && file.ContentLength < maxsize)
                {
                    string FileName = Guid.NewGuid() + fileExt;
                    filename = FileName;
                    //Determining file size.
                    int FileSize = file.ContentLength;
                    //Creating a byte array corresponding to file size.
                    byte[] FileByteArray = new byte[FileSize];
                    //Posted file is being pushed into byte array.
                    file.InputStream.Read(FileByteArray, 0, FileSize);
                    //Uploading properly formatted file to server.
                    file.SaveAs(TargetLocation + C.DS + FileName);
                }

            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static string Show_Value(Hashtable hashtable, string key)
    {
        if (hashtable != null)
        {
            if (hashtable[key] != null)
            {
                return hashtable[key].ToString();
            }
            else
                return "";
        }
        else
            return "";
    }
    public static string RandomString(int lenght = 6)
    {
        string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";

        char[] chars = new char[lenght];
        int allowedCharCount = _allowedChars.Length;

        for (int i = 0; i < lenght; i++)
        {
            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
        }
        return new string(chars);
    }
    public string Calc(string num1, string num2)
    {
        List<int[]> list1 = new List<int[]>();

        for (int i = 0; i < num1.Length; i++)
        {
            for (int j = 0; j < num2.Length; j++)
            {
                int n1 = int.Parse(num1.Substring(i, 1));
                int n2 = int.Parse(num2.Substring(j, 1));
                int ap = n1 * n2;
                if (ap > 9)
                    list1.Add(new int[] { int.Parse(ap.ToString().Substring(0, 1)), int.Parse(ap.ToString().Substring(1, 1)) });
                else
                    list1.Add(new int[] { 0, ap });
            }
        }

        return "";
    }
    public static int Page
    {
        get
        {
            int _Page = 1;
            try
            {
                _Page = HttpContext.Current.Request.QueryString["page"] == null ? 1 : Int16.Parse(HttpContext.Current.Request.QueryString["page"].ToString());
            }
            catch (Exception)
            {

                _Page = 1;
            }

            return _Page;
        }
    }
    public static string CheckVersion_NonTemplate(string path)
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
    public static string CheckImageVersion(string path)
    {
        if (!String.IsNullOrEmpty(path.Trim()))
        {
            string link = HttpContext.Current.Server.MapPath(path.Trim());
            if (System.IO.File.Exists(link))
            {
                string version = System.IO.File.GetLastWriteTime(link).Ticks.ToString();
                return C.ROOT_URL + path.Trim() + "?v=" + version;
            }
            else
                return C.ROOT_URL + "/upload/avatar/no-avatar.jpg";
        }
        return "";
    }
    public static bool IsNullOrEmpty(object obj)
    {
        try
        {
            if (obj == null)
                return true;
            else
            {
                return String.IsNullOrEmpty(obj.ToString());
            }

        }
        catch (Exception)
        {

            return true;
        }
    }
    public static string LoadUserControl(string url)
    {
        try
        {
            using (Page page = new Page())
            {
                UserControl userControl = (UserControl)page.LoadControl(url);

                page.Controls.Add(userControl);
                using (StringWriter writer = new StringWriter())
                {
                    //page.Controls.Add(userControl);
                    HttpContext.Current.Server.Execute(page, writer, false);
                    return writer.ToString();
                }
            }
        }
        catch (Exception ex)
        {

            return ex.Message;
        }
    }

    public static string LoadUserControl(string url, string Title, string ReadMore, string Filter, int CategoryID, bool IsHome, string Modul, int Index)
    {
        try
        {
            using (Page page = new Page())
            {
                UserControl userControl = (UserControl)page.LoadControl(url);
                userControl.GetType().GetProperty("Title").SetValue(userControl, Title);
                userControl.GetType().GetProperty("ReadMore").SetValue(userControl, ReadMore);
                userControl.GetType().GetProperty("Filter").SetValue(userControl, Filter);
                userControl.GetType().GetProperty("CategoryID").SetValue(userControl, CategoryID);
                userControl.GetType().GetProperty("IsHome").SetValue(userControl, IsHome);
                userControl.GetType().GetProperty("Modul").SetValue(userControl, Modul);
                userControl.GetType().GetProperty("Index").SetValue(userControl, Index);

                page.Controls.Add(userControl);
                using (StringWriter writer = new StringWriter())
                {
                    HttpContext.Current.Server.Execute(page, writer, false);
                    return writer.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string LoadMetaTags1(string url, string MetaTitle)
    {
        try
        {
            using (Page page = new Page())
            {
                UserControl userControl = (UserControl)page.LoadControl(url);
                userControl.GetType().GetProperty("meta_title").SetValue(userControl, MetaTitle);

                page.Controls.Add(userControl);
                using (StringWriter writer = new StringWriter())
                {
                    HttpContext.Current.Server.Execute(page, writer, false);
                    return writer.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string LoadUserControlBreadCrumb(string url, List<BreadCrumb> breadCrumbList)
    {
        try
        {
            using (Page page = new Page())
            {
                UserControl userControl = (UserControl)page.LoadControl(url);
                userControl.GetType().GetProperty("breadCrumbList").SetValue(userControl, breadCrumbList);

                page.Controls.Add(userControl);
                using (StringWriter writer = new StringWriter())
                {
                    HttpContext.Current.Server.Execute(page, writer, false);
                    return writer.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static string JavaSerialize<T>(T obj) where T : class
    {
        if (obj != null)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }
        else
            return "";
    }
    public static string JavaSerialize<T>(IList<T> objs) where T : class
    {
        if (objs != null)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();

            return js.Serialize(objs);
        }
        else
            return "";
    }
    public static string JavaSerialize(object obj)
    {
        if (obj != null)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }
        else
            return "";
    }
    public static T JavaDerialize<T>(string value) where T : class
    {
        if (!String.IsNullOrEmpty(value))
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Deserialize<T>(value);
        }
        else
            return null;
    }
    public static string GetToken(string controller)
    {
        try
        {
            return HttpContext.Current.Session["token_" + controller].ToString();
        }
        catch (Exception ex)
        {

            return "";
        }

    }
    public static void SetToken(string controller)
    {
        HttpContext.Current.Session["token_" + controller] = Utils.RandomString(50);

    }
    public static string DeviceInfo()
    {
        string userAgent = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];
        Regex OS = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        Regex device = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        string device_info = string.Empty;
        if (OS.IsMatch(userAgent))
        {
            device_info = OS.Match(userAgent).Groups[0].Value;
        }
        if (device.IsMatch(userAgent.Substring(0, 4)))
        {
            device_info += device.Match(userAgent).Groups[0].Value;
        }

        return device_info;
        return device.Match(userAgent).Groups[0].Value;


    }
    public static bool isMobileBrowser
    {
        get
        {

            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
    public static int GetRequestForm(string key, int _default)
    {

        try
        {
            if (Utils.IsNullOrEmpty(HttpContext.Current.Request.Form[key]))
                return _default;
            else
                return Convert.ToInt16(HttpContext.Current.Request.Form[key]);
        }
        catch (Exception)
        {
            return _default;
        }

    }
    public static string GetRequestForm(string key, string _default)
    {

        try
        {
            if (Utils.IsNullOrEmpty(HttpContext.Current.Request.Form[key]))
                return _default;
            else
                return HttpContext.Current.Request.Form[key].ToString();
        }
        catch (Exception)
        {
            return _default;
        }

    }
    public static string LoadPictrue(string path, int width = 0, int height = 0, bool _override = false)
    {
        string link = "";
        if (!Utils.IsNullOrEmpty(path))
        {
            string ext = Path.GetExtension(path);
            if (!Utils.IsNullOrEmpty(ext))
            {
                if (ext.ToLower() == ".jpg" || ext.ToLower() == ".png" || ext.ToLower() == ".jpeg")
                {
                    if (width == 0 && height == 0)
                        link = String.Format("{0}{1}", C.MAIN_URL + C.DS, path);
                    else if (width == 0 && height > 0)
                        link = String.Format("{0}{1}?height={2}&quality=100&bgcolor=000", C.MAIN_URL + C.DS, path, height);
                    else if (height == 0 && width > 0)
                        link = String.Format("{0}{1}?width={2}&quality=100&bgcolor=000", C.MAIN_URL + C.DS, path, width);
                    else
                        link = String.Format("{0}{1}?width={2}&height={3}&quality=100&mode=pad&bgcolor=f4f5f6", C.MAIN_URL + C.DS, path, width, height);
                }
                else if (ext.ToLower() == ".gif")
                {
                    if (_override)
                        link = String.Format("{0}{1}?width={2}&height={3}&quality=100", C.MAIN_URL + C.DS, path, width, height);
                    else
                        link = String.Format("{0}{1}", C.MAIN_URL + C.DS, path);

                }
            }
            else
                return link;
        }
        return link;
    }
    public static NameValueCollection ParseQueryString(string s)
    {
        NameValueCollection nvc = new NameValueCollection();

        // remove anything other than query string from url
        if (s.Contains("?"))
            s = s.Substring(s.IndexOf('?') + 1);

        foreach (string vp in Regex.Split(s, "&"))
        {
            string[] singlePair = Regex.Split(vp, "=");
            if (singlePair.Length == 2)
                nvc.Add(singlePair[0], singlePair[1]);
            else
                nvc.Add(singlePair[0], string.Empty);
        }
        return nvc;
    }
    public static string GetQueryString(string key, string _default)
    {
        NameValueCollection param = ParseQueryString(HttpContext.Current.Request.RawUrl);
        if (param != null)
        {
            if (param[key] != null)
                return param[key].ToString();
            else
                return _default;
        }
        else
            return _default;
    }
    public static int GetQueryString(string key, int _default)
    {
        NameValueCollection param = ParseQueryString(HttpContext.Current.Request.RawUrl);
        if (param != null)
        {
            if (param[key] != null)
            {
                try
                {
                    return Convert.ToInt32(param[key].ToString());
                }
                catch (Exception)
                {
                    return _default;
                }
            }

            else
                return _default;
        }
        else
            return _default;
    }
    public static string GetAppendedQueryString(string Url, string Key, string Value)
    {
        if (Url.Contains("?"))
            Url = string.Format("{0}&{1}={2}", Url, Key, Value);
        else
            Url = string.Format("{0}?{1}={2}", Url, Key, Value);

        return Url;
    }
    public static string GetAppendedQueryString(string Url, Hashtable array)
    {

        foreach (DictionaryEntry item in array)
        {
            if (Url.Contains("?"))
                Url = string.Format("{0}&{1}={2}", Url, item.Key.ToString(), item.Value);
            else
                Url = string.Format("{0}?{1}={2}", Url, item.Key.ToString(), item.Value);
        }

        return Url;
    }
    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }
    public static int ConvertToInt(object obj)
    {
        try
        {
            return Convert.ToInt16(obj);
        }
        catch (Exception)
        {
            return -1;
        }
    }
    public static bool LocalHostCheck
    {
        get
        {
            string host = HttpContext.Current.Request.Url.Host.ToLower();
            return (host == "localhost");
        }
    }
    public static string FirstCharToUpper(string input)
    {
        try
        {
            switch (input)
            {
                case null: return "";
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
        catch
        {
            return input;
        }
    }
    public static string Zip(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        string s = "";

        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                //msi.CopyTo(gs);
                CopyTo(msi, gs);
            }

            Byte[] temp = mso.ToArray();

            if (temp.Length > 0 && temp != null)
            {
                foreach (byte b in temp)
                {
                    s += b;
                }
            }
        }
        return s;
    }
    public static void CopyTo(Stream src, Stream dest)
    {
        byte[] bytes = new byte[4096];

        int cnt;

        while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
        {
            dest.Write(bytes, 0, cnt);
        }
    }
    public static string HighlightKeywords(string input, string keywords)
    {
        if (input == string.Empty || keywords == string.Empty)
        {
            return input;
        }

        string[] sKeywords = keywords.Split(' ');
        foreach (string sKeyword in sKeywords)
        {
            try
            {
                input = Regex.Replace(input, sKeyword, string.Format("<span class=\"highlight\">{0}</span>", "$0"), RegexOptions.IgnoreCase);
            }
            catch
            {
                //
            }
        }
        return input;
    }
    public static void SetValueToCookie(string name, string value)
    {
        SetValueToCookie(name, value, 30);
    }
    public static void SetValueToCookie(string name, string value, int days)
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
        context.Response.Cookies[name].Expires = DateTime.Now.AddDays(days);
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
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
    public static DateTime AbsoluteStart(DateTime dateTime)
    {
        return dateTime.Date;
    }
    public static DateTime AbsoluteEnd(DateTime dateTime)
    {
        return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
    }
    public static string TagUnsign(string TagHTML)
    {
        string ReturnString = ",";
        string[] tags = TagHTML.Split(',');
        foreach (string tag in tags)
        {
            string tagTrim = tag.Trim();
            tagTrim = TextChanger.Translate(tagTrim, " ");
            tagTrim += ",";
            ReturnString += tagTrim.ToLower();
        }
        return ReturnString;
    }
    public static string TrimText(string value, int textNumber)
    {
        if (textNumber == 0)
            textNumber = 32;

        string temp = value;
        if (temp.Length > textNumber)
        {
            temp = temp.Remove(textNumber);
            if (temp.LastIndexOf(" ") != -1)
            {
                temp = temp.Remove(temp.LastIndexOf(" "));
            }
            return temp + "...";
        }
        else
            return temp;
    }
    public static string PinterestPin(string url, string img, string description, string siteName)
    {
        return string.Format(@"https://www.pinterest.com/pin/create/button/?url={0}&media={1}&description={2}#{3}#", url, img, description, siteName);
    }

    public static Double RoundPrice(Double price)
    {
        price = Convert.ToDouble(price) / 1000;
        price = Math.Round(price, 0) * 1000;
        return price;
    }
    public static decimal RoundPrice(decimal price)
    {
        price = Convert.ToDecimal(price) / 1000;
        price = Math.Round(price, 0) * 1000;
        return price;
    }
    public static List<string> ConvertStringToList(string text)
    {
        List<string> result = new List<string>();
        if (!string.IsNullOrEmpty(text))
            result = text.Split(new char[] { ',' }).ToList();
        return result;
    }
    public static string ConvertArrayToString(List<string> strList)
    {
        return string.Join(",", strList.ToArray());
    }
    public static List<string> AddTitemToArrayString(List<string> strList, string itemAdd)
    {
        // nếu tồn tại thì bỏ đi
        for (int i = 0; i < strList.Count; i++)
        {
            if (strList[i].Equals(itemAdd))
                strList.RemoveAt(i);
        }

        if (strList.Contains(itemAdd) == false)
            strList.Add(itemAdd);

        if (strList.Count >= 100)
            strList.RemoveAt(0);

        return strList;
    }
    public static List<string> RemoveTitemToArrayString(List<string> strList, string itemRemove)
    {
        for (int i = 0; i < strList.Count; i++)
        {
            if (strList[i].Equals(itemRemove))
                strList.RemoveAt(i);
        }
        return strList;
    }
    public static bool CheckExistItemList(List<string> strList, string itemCheck)
    {
        bool Exist = false;
        for (int i = 0; i < strList.Count; i++)
        {
            if (strList[i].Equals(itemCheck))
                Exist = true;
        }
        return Exist;
    }
    public static bool Upload_Image(string folder, HttpPostedFile file, ref string filename)
    {
        int maxsize = 10000000; //10Mb
        string TargetLocation = HttpContext.Current.Server.MapPath("~/" + C.UPLOAD_DIR + folder);
        try
        {
            if (file.ContentLength > 0)
            {
                //Determining file name. You can format it as you wish.
                string fileExt = System.IO.Path.GetExtension(file.FileName);
                if (file.ContentType == "image/gif" || file.ContentType == "image/jpeg" || file.ContentType == "image/jpg" || file.ContentType == "image/pjpeg" || file.ContentType == "image/x-png" || file.ContentType == "image/png" || file.ContentType == "image/png" && file.ContentLength < maxsize)
                {
                    string FileName = StringUtil.GetStringDate() + "_" + StringUtil.CreateRandomString(10) + fileExt;
                    filename = FileName;
                    //Determining file size.
                    int FileSize = file.ContentLength;
                    //Creating a byte array corresponding to file size.
                    byte[] FileByteArray = new byte[FileSize];
                    //Posted file is being pushed into byte array.
                    file.InputStream.Read(FileByteArray, 0, FileSize);
                    //Uploading properly formatted file to server.
                    file.SaveAs(TargetLocation + C.DS + FileName);
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static bool Upload_Multi_Image(string folder, HttpFileCollection files, string name_file, string name_sort, ref string images_sort, string _type = "", int thumb_width = 100, int thumb_height = 100)
    {
        int maxsize = 10000000; //10Mb
        string TargetLocation = HttpContext.Current.Server.MapPath("~/Uploads/" + folder);
        List<Product_Image> list_product_image;
        if (String.IsNullOrEmpty(images_sort))
            list_product_image = new List<Product_Image>();
        else
            list_product_image = Utils.Deserialize<List<Product_Image>>(images_sort);
        try
        {
            if (files.Count > 0)
            {
                int k = 0;
                foreach (string key in files.Keys)
                {
                    if (key == name_file + "_" + k)
                    {
                        if (files[key].ContentLength > 0)
                        {
                            string fileExt = System.IO.Path.GetExtension(files[key].FileName);
                            if (files[key].ContentType == "image/gif" || files[key].ContentType == "image/jpeg" || files[key].ContentType == "image/jpg" || files[key].ContentType == "image/pjpeg" || files[key].ContentType == "image/x-png" || files[key].ContentType == "image/png" || files[key].ContentType == "image/png" && files[key].ContentLength < maxsize)
                            {
                                Utils utils = new Utils();
                                string FileName = StringUtil.GetStringDate() + "_" + StringUtil.CreateRandomString(10) + fileExt;
                                images_sort = FileName;
                                int FileSize = files[key].ContentLength;
                                byte[] FileByteArray = new byte[FileSize];
                                files[key].InputStream.Read(FileByteArray, 0, FileSize);
                                files[key].SaveAs(TargetLocation + C.DS + FileName);
                                if (_type == "resize")
                                    ImageUitl.ResizeImage(thumb_width, thumb_height, "~/Uploads/" + folder + C.DS + FileName, "~/Uploads/" + folder + C.DS + "thumb_" + FileName);
                                var d = HttpContext.Current.Request.Form[name_sort + "_" + (k).ToString()];
                                string s = name_sort + "_" + k;
                                list_product_image.Add(new Product_Image(FileName, d == null ? "0" : d.ToString()));
                            }
                        }
                        k++;
                    }
                }

                images_sort = Utils.Serialize<List<Product_Image>>(list_product_image);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }
    public static bool Upload_Multi_Image(string folder, HttpFileCollection files, string name_file, string name_sort, string name_note, ref string images_sort, string _type = "", int thumb_width = 100, int thumb_height = 100)
    {
        int maxsize = 10000000; //10Mb
        string TargetLocation = HttpContext.Current.Server.MapPath("~/upload/" + folder);
        List<Product_Image> list_product_image;
        if (String.IsNullOrEmpty(images_sort))
            list_product_image = new List<Product_Image>();
        else
            list_product_image = Utils.Deserialize<List<Product_Image>>(images_sort);
        try
        {
            if (files.Count > 0)
            {
                int k = 0;
                foreach (string key in files.Keys)
                {
                    if (key == name_file + "_" + k)
                    {
                        if (files[key].ContentLength > 0)
                        {
                            string fileExt = System.IO.Path.GetExtension(files[key].FileName);
                            if (files[key].ContentType == "image/gif" || files[key].ContentType == "image/jpeg" || files[key].ContentType == "image/jpg" || files[key].ContentType == "image/pjpeg" || files[key].ContentType == "image/x-png" || files[key].ContentType == "image/png" || files[key].ContentType == "image/png" && files[key].ContentLength < maxsize)
                            {
                                Utils utils = new Utils();
                                string FileName = StringUtil.GetStringDate() + "_" + StringUtil.CreateRandomString(10) + fileExt;
                                images_sort = FileName;
                                int FileSize = files[key].ContentLength;
                                byte[] FileByteArray = new byte[FileSize];
                                files[key].InputStream.Read(FileByteArray, 0, FileSize);
                                files[key].SaveAs(TargetLocation + C.DS + FileName);
                                if (_type == "resize")
                                    ImageUitl.ResizeImage(thumb_width, thumb_height, "~/upload/" + folder + C.DS + FileName, "~/Upload/" + folder + C.DS + "thumb_" + FileName, true);
                                var d = HttpContext.Current.Request.Form[name_sort + "_" + (k).ToString()];
                                var note = HttpContext.Current.Request.Form[name_note + "_" + (k).ToString()];
                                string s = name_sort + "_" + k;
                                list_product_image.Add(new Product_Image(FileName, d == null ? "0" : d.ToString(), note == null ? "" : note.ToString()));
                            }
                        }
                        k++;
                    }
                }

                images_sort = Utils.Serialize<List<Product_Image>>(list_product_image);
            }
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;

    }

    public static void GetAttrByID(string IDList, string table, bool isSave, ref string UrlList, ref string NameList, ref string IDListSort)
    {
        string UrlStr = string.Empty;
        string NameStr = string.Empty;
        string IDStr = string.Empty;

        if (!string.IsNullOrEmpty(IDList))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("Select ID, Name, FriendlyUrl From {0} WHERE ID IN ({1}) Order By ID", table, IDList.Trim(','));
                DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UrlStr += dr["FriendlyUrl"].ToString() + ",";
                        NameStr += dr["Name"].ToString() + ",";
                        IDStr += dr["ID"].ToString() + ",";
                    }
                }
            }

            if (isSave)
            {
                UrlList = Utils.CommaSQLAdd(UrlStr);
                NameList = Utils.CommaSQLAdd(NameStr);
                IDListSort = Utils.CommaSQLAdd(IDStr);
            }
            else
            {
                UrlList = Utils.CommaSQLRemove(UrlStr);
                NameList = Utils.CommaSQLRemove(NameStr);
                IDListSort = Utils.CommaSQLRemove(IDStr);
            }
        }
        else
        {
            UrlList = "";
            NameList = "";
            IDListSort = "";
        }
    }
    public static void GetAttrByID(string IDList, string table, bool isSave, ref string UrlList, ref string NameList)
    {
        string UrlStr = string.Empty;
        string NameStr = string.Empty;

        if (!string.IsNullOrEmpty(IDList))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("Select Name, FriendlyUrl From {0} WHERE ID IN ({1}) Order By ID", table, IDList.Trim(','));
                DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        UrlStr += dr["FriendlyUrl"].ToString() + ",";
                        NameStr += dr["Name"].ToString() + ",";
                    }
                }
            }

            if (isSave)
            {
                UrlList = Utils.CommaSQLAdd(UrlStr);
                NameList = Utils.CommaSQLAdd(NameStr);
            }
            else
            {
                UrlList = Utils.CommaSQLRemove(UrlStr);
                NameList = Utils.CommaSQLRemove(NameStr);
            }
        }
        else
        {

            UrlList = "";
            NameList = "";

        }
    }
    public static void GetAttrByUrl(string UrlList, string table, bool isSave, ref string IDList, ref string NameList)
    {
        string IDStr = string.Empty;
        string NameStr = string.Empty;

        if (!string.IsNullOrEmpty(UrlList))
        {
            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format("Select Name, ID From {0} WHERE {1}", table, string.Format("FriendlyUrl=N'{0}'", UrlList.Trim(',')));
                DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        IDStr += dr["ID"].ToString() + ",";
                        NameStr += dr["Name"].ToString() + ",";
                    }
                }
            }

            if (isSave)
            {
                IDList = Utils.CommaSQLAdd(IDStr);
                NameList = Utils.CommaSQLAdd(NameStr);
            }
            else
            {
                IDList = Utils.CommaSQLRemove(IDStr);
                NameList = Utils.CommaSQLRemove(NameStr);
            }
        }
        else
        {
            IDList = "";
            NameList = "";
        }
    }

    public static void GetTagByName(string NameList, string table, string Field, bool isSave, ref string Url, ref string ID)
    {
        string UrlStr = string.Empty;
        string IDStr = string.Empty;

        if (!string.IsNullOrEmpty(NameList))
        {
            string[] arr = NameList.Trim(',').Split(',');
            if (arr != null && arr.Length > 0)
            {
                string filter = string.Empty;
                int count = 0;
                foreach (string strName in arr)
                {
                    string filter1 = string.Format("Name=N'{0}' AND Moduls = N'{1}'", strName.Trim(), Field);
                    DataTable dt = SqlHelper.SQLToDataTable(table, "", filter1);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        UrlStr += dt.Rows[0]["FriendlyUrl"].ToString() + ",";
                        IDStr += dt.Rows[0]["ID"].ToString() + ",";
                    }
                    else
                    {
                        if (isSave)
                        {
                            int outID = 0;
                            AddTags(strName.Trim(), Field, ref outID);
                            UrlStr += TextChanger.Translate(strName.Trim().ToLower(), "-");
                            IDStr += outID.ToString() + ",";
                        }
                    }
                    count++;
                }
                if (isSave)
                {
                    Url = Utils.CommaSQLAdd(UrlStr);
                    ID = Utils.CommaSQLAdd(IDStr);
                }
                else
                {
                    Url = Utils.CommaSQLRemove(UrlStr);
                    ID = Utils.CommaSQLRemove(IDStr);
                }
            }
        }
        else
        {
            Url = "";
            ID = "";
        }
    }



    protected static void AddTags(string Name, string Type, ref int OUT_ID)
    {
        if (!string.IsNullOrEmpty(Name))
        {
            CacheUtility.PurgeCacheItems("tblCategories");

            using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string FriendlyUrl = TextChanger.Translate(Name.ToLower(), "-");

                string sqlQuery = "INSERT INTO tblCategories (ParentID, Name, FriendlyUrl, Moduls,CreatedDate,CreatedBy) OUTPUT INSERTED.ID Values (@ParentID, @Name, @FriendlyUrl, @Moduls, @CreatedDate, @CreatedBy)";
                dbx.AddParameter("@ParentID", System.Data.SqlDbType.Int, 0);
                dbx.AddParameter("@Name", System.Data.SqlDbType.NVarChar, Name);
                dbx.AddParameter("@FriendlyUrl", System.Data.SqlDbType.NVarChar, TextChanger.Translate(Name.Trim().ToLower(), "-"));
                dbx.AddParameter("@Moduls", System.Data.SqlDbType.NVarChar, Type);
                dbx.AddParameter("@CreatedDate", System.Data.SqlDbType.DateTime, DateTime.Now);
                dbx.AddParameter("@CreatedBy", System.Data.SqlDbType.Int, HttpContext.Current.User.Identity.Name);

                OUT_ID = dbx.ExecuteSqlScalar<int>(sqlQuery, 0);
            }
        }
    }

    public static string AccessPermission(string control)
    {
        string returnValue = "DENIED";
        List<AdminPermission> permissionList = new List<AdminPermission>();
        int id = ConvertToInt(HttpContext.Current.User.Identity.Name);
        using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
        {
            string sqlQuery = string.Format("Select u.ID, u.[Name], p.Name as 'PermissionName', p.[Role] from tblAdminUser as u inner join tblAdminPermission as p on u.Permission = p.ID AND u.ID={0}", id);
            DataTable dt = dbx.ExecuteSqlDataTable(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                permissionList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdminPermission>>(dt.Rows[0]["Role"].ToString());
                if (permissionList != null && permissionList.Count > 0)
                {
                    int count = 0;
                    foreach (AdminPermission ap in permissionList)
                    {
                        if (ap.Control.ToLower() == control.ToLower())
                        {
                            returnValue = ap.Access;
                            count++;
                        }
                    }
                    if (count == 0) // Chưa có cụ thể thì chuyển sang mặc định
                    {
                        foreach (AdminPermission ap in permissionList)
                        {
                            if (ap.Control == "default")
                            {
                                returnValue = ap.Access;
                                count++;
                            }
                        }
                    }
                }
            }
        }
        return returnValue;
    }

    public static string GetFirstImageInGallery_Json(string jSon, int width, int height)
    {
        string param = "";
        if (width > 0)
            param = "?width=" + width;
        if (height > 0)
            param += "&height=" + height;
        if (!string.IsNullOrEmpty(param))
            param += "&quality=100";

        return GetFirstImageInGallery_Json(jSon) + param;
    }
    public static string GetFirstImageInGallery_Json(string jSon)
    {
        string imgPath = C.NO_IMG_PATH;
        List<GalleryImage> galleryList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<GalleryImage>>(jSon);
        if (galleryList != null && galleryList.Count > 0)
        {
            imgPath = C.MAIN_URL + galleryList[0].Path;
        }

        return imgPath;
    }
    public static string GetAllImageInGallery_Json(string jSon)
    {
        string imgPath = string.Empty;
        List<GalleryImage> galleryList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<GalleryImage>>(jSon);
        if (galleryList != null && galleryList.Count > 0)
        {
            foreach (GalleryImage img in galleryList)
            {
                imgPath += C.MAIN_URL + img.Path + ",";
            }
            imgPath = CommaSQLRemove(imgPath);
        }

        return imgPath;
    }

    public static DataTable JsonToDataTable(string jsonString)
    {
        var jsonLinq = Newtonsoft.Json.Linq.JObject.Parse(jsonString);

        // Find the first array using Linq  
        var srcArray = jsonLinq.Descendants().Where(d => d is Newtonsoft.Json.Linq.JArray).FirstOrDefault();
        var trgArray = new Newtonsoft.Json.Linq.JArray();
        foreach (Newtonsoft.Json.Linq.JObject row in srcArray.Children<Newtonsoft.Json.Linq.JObject>())
        {
            var cleanRow = new Newtonsoft.Json.Linq.JObject();
            foreach (Newtonsoft.Json.Linq.JProperty column in row.Properties())
            {
                // Only include JValue types  
                if (column.Value is Newtonsoft.Json.Linq.JValue)
                {
                    cleanRow.Add(column.Name, column.Value);
                }
            }
            trgArray.Add(cleanRow);
        }

        return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
    }

    //public static DataTable JsonToDataTable(string jsonString)
    //{
    //    DataTable dt = new DataTable();
    //    //dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
    //    //dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(Convert.ToString(jsonObject));

    //    Object jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(jsonString);
    //    DataTable dsResult = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jObject.ToString());


    //    return dt;
    //}


    public static bool CheckExist_DataTable(DataTable dataTable)
    {
        if (dataTable != null && dataTable.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public static bool IS_LOCAL
    {
        get
        {
            if (C.ROOT_URL.Contains("http://localhost") || C.ROOT_URL.Contains("https://localhost"))
                return true;
            else
                return false;
        }
    }

    //public static int FlagHome
    //{
    //    get
    //    {
    //       FlagsAttribute()
    //    }
    //}


    //protected int Flags(bool Home, bool Priority, bool a, bool b, bool c, bool d, bool e, bool f, bool g, bool h, bool i, bool j, bool ak, bool l, bool m, bool n, bool o, bool p, bool q, bool z)
    //{
    //    int returnValue = 0;

    //    return returnValue;
    //}


    public static string SetChecked(bool Value)
    {
        string strReturn = string.Empty;
        if (Value)
            strReturn = "checked";
        return strReturn;
    }


    public static string CreateFilterFlags(object Flag, object Field)
    {
        int FlagInt = (int)Flag;
        return string.Format("{0} & {1} <> 0", Field, FlagInt);
    }




    public static string CreateFilterHide
    {
        get
        {
            return "(Hide is null OR Hide=0)";
        }
    }

    public static string CreateFilterDate
    {
        get
        {
            return "StartDate<DATEADD(DAY, DATEDIFF(day, 0, getdate()), 1) AND EndDate>DATEADD(DAY, DATEDIFF(day, 0, getdate()), 1)";
        }
    }



    public static string CreateCategoryLink(object Flags, object FriendlyUrl, object link)
    {
        int FlagInt = ConvertUtility.ToInt32(Flags);
        string UrlStr = ConvertUtility.ToString(FriendlyUrl);
        string Link = ConvertUtility.ToString(link);
        string strReturn = string.Empty;

        if (FlagInt == (int)LinkTypeMenuFlag.Article)
        {
            strReturn = TextChanger.GetLinkRewrite_CategoryArticle(UrlStr);
        }
        else if (FlagInt == (int)LinkTypeMenuFlag.Product || FlagInt == (int)LinkTypeMenuFlag.None)
        {
            strReturn = TextChanger.GetLinkRewrite_Category(UrlStr);
        }
        else if (FlagInt == (int)LinkTypeMenuFlag.Content)
        {
            strReturn = TextChanger.GetLinkRewrite_Menu(UrlStr);
        }
        else if (FlagInt == (int)LinkTypeMenuFlag.Link)
        {
            if (string.IsNullOrEmpty(Link))
                strReturn = HttpContext.Current.Request.RawUrl;
            else
            {
                if (Link.StartsWith("https://") || Link.StartsWith("http://"))
                    strReturn = Link;
                else
                    strReturn = C.ROOT_URL + Link;
            }
        }
        return strReturn;
    }

    public static string CreateCategory_Target(object Flags)
    {
        int FlagInt = ConvertUtility.ToInt32(Flags);
        AttrMenuFlag attrMenuFlag = (AttrMenuFlag)FlagInt;
        string strReturn = string.Empty;
        if (attrMenuFlag.HasFlag(AttrMenuFlag.OpenNewWindows))
        {
            strReturn = @" target=""_blank""";
        }
        return strReturn;
    }

    public static long Timestamp
    {
        get
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(2020, 9, 23, 0, 0, 0));
            return (long)timeSpan.TotalMilliseconds;
        }
    }


    public static string TimeAgo(DateTime dateTime)
    {
        string result = string.Empty;
        var timeSpan = DateTime.Now.Subtract(dateTime);

        if (timeSpan <= TimeSpan.FromSeconds(60))
        {
            result = string.Format("{0} giây trước", timeSpan.Seconds);
        }
        else if (timeSpan <= TimeSpan.FromMinutes(60))
        {
            result = timeSpan.Minutes > 1 ?
                String.Format("{0} phút trước", timeSpan.Minutes) :
                "khoảng 1 phút trước";
        }
        else if (timeSpan <= TimeSpan.FromHours(24))
        {
            result = timeSpan.Hours > 1 ?
                String.Format("{0} giờ trước", timeSpan.Hours) :
                "khoảng 1 giờ trước";
        }
        else if (timeSpan <= TimeSpan.FromDays(30))
        {
            result = timeSpan.Days > 1 ?
                String.Format("{0} ngày trước", timeSpan.Days) :
                "hôm qua";
        }
        else if (timeSpan <= TimeSpan.FromDays(365))
        {
            result = timeSpan.Days > 30 ?
                String.Format("{0} tháng trước", timeSpan.Days / 30) :
                "khoảng 1 tháng trước";
        }
        else
        {
            result = timeSpan.Days > 365 ?
                String.Format("{0} năm trước", timeSpan.Days / 365) :
                "năm ngoái";
        }

        return result;
    }

    public static DataTable SearchProduct(string keyword)
    {
        return SearchProduct(keyword, "ID,Name,Price,Price1, Gallery,FriendlyUrlCategory,FriendlyUrl", 100);
    }
    public static DataTable SearchProduct(string keyword, int pageSize)
    {
        return SearchProduct(keyword, "ID,Name,Price,Price1, Gallery,FriendlyUrlCategory,FriendlyUrl", pageSize);
    }


    public static DataTable SearchProduct(string keyword, string field, int pageSize)
    {
        DataTable dtProduct = new DataTable();
        dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, field, string.Format("(Name like N'%{0}%' OR NameUnsign like N'%{0}%') AND {1}", keyword, Utils.CreateFilterHide), "EditedDate DESC", 1, pageSize);
        if (!Utils.CheckExist_DataTable(dtProduct))
        {
            string keyword_new = keyword.Replace("-", " ");

            dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, field, string.Format("REPLACE(REPLACE(REPLACE(REPLACE(REPLACE([Name], '-', ''), '/', ''), '\', ''), '(', ''), ')', '') LIKE N'%{0}%'", keyword_new));
            if (!Utils.CheckExist_DataTable(dtProduct))
            {
                using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
                {
                    string sql = string.Format("SearchProducts");
                    db.AddParameter("@searchTerm", System.Data.SqlDbType.NVarChar, keyword);
                    db.AddParameter("@pageIndex", System.Data.SqlDbType.Int, 1);
                    db.AddParameter("@pageSize", System.Data.SqlDbType.Int, pageSize);
                    db.AddOutputParameter("@totalCount", SqlDbType.Int);
                    dtProduct = db.ExecuteSPDataTable(sql);

                    if (!Utils.CheckExist_DataTable(dtProduct))
                    {
                        string formattedSearchTerm = FormatSearchTerm(keyword);
                        string sql2 = string.Format("SearchProducts2");
                        db.AddParameter("@searchTerm", System.Data.SqlDbType.NVarChar, @"""" + formattedSearchTerm + @"""");
                        db.AddParameter("@pageIndex", System.Data.SqlDbType.Int, 1);
                        db.AddParameter("@pageSize", System.Data.SqlDbType.Int, pageSize);
                        db.AddOutputParameter("@totalCount", SqlDbType.Int);
                        dtProduct = db.ExecuteSPDataTable(sql2);
                    }
                }
            }
        }
        return dtProduct;
    }


    private static string FormatSearchTerm(string searchTerm)
    {
        // Tách từ khóa bằng khoảng trắng
        string[] keywords = searchTerm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Tạo danh sách từ khóa và từ gần giống
        List<string> keywordList = new List<string>();
        List<string> nearList = new List<string>();
        foreach (string keyword in keywords)
        {
            if (keyword.Length > 2) // từ gần phải có ít nhất 3 ký tự
            {
                nearList.Add(keyword);
            }
            keywordList.Add(string.Format("*{0}*", keyword));
        }

        // Tạo câu truy vấn
        string nearClause = "";
        if (nearList.Count > 0)
        {
            nearClause = string.Format("NEAR(({0})) AND ", string.Join(",", nearList));
        }
        return string.Format("{0}({1})", nearClause, string.Join(" OR ", keywordList));
    }


    public static string FixCapitalization(string inputString)
    {
        // Kiểm tra chuỗi inputString có null hoặc rỗng không
        if (string.IsNullOrEmpty(inputString))
        {
            return inputString;
        }

        // Đếm số ký tự viết hoa trong chuỗi
        int uppercaseCount = inputString.Count(c => char.IsUpper(c) && !char.IsDigit(c));

        // Tính tỷ lệ phần trăm ký tự viết hoa trong chuỗi
        double uppercasePercentage = (double)uppercaseCount / inputString.Length * 100;

        // Kiểm tra xem tỷ lệ phần trăm ký tự viết hoa có lớn hơn 50% hay không
        if (uppercasePercentage > 50)
        {
            // Chuyển đổi chuỗi thành chuỗi viết hoa mỗi ký tự của từng từ đầu tiên
            string[] words = inputString.ToLower().Split(' ');
            string outputString = "";
            foreach (string word in words)
            {
                string capitalizedWord = string.Empty;
                if (!string.IsNullOrEmpty(word))
                {
                    // Kiểm tra nếu là mã sản phẩm thì giữ nguyên viết hoa
                    if (word.All(c => char.IsUpper(c) || char.IsDigit(c)))
                    {
                        capitalizedWord = word;
                    }
                    else
                    {
                        capitalizedWord = char.ToUpper(word[0]) + word.Substring(1).ToLower();
                    }
                }
                outputString += capitalizedWord + " ";
            }
            outputString = outputString.TrimEnd();
            return outputString;
        }
        else
        {
            return inputString;
        }
    }


    public static string RemoveNonDigits(string input)
    {
        // Sử dụng biểu thức chính quy để trích xuất chuỗi có chứa các chữ số và dấu phân cách
        var match = Regex.Match(input, @"[\d.,]+");
        if (match.Success)
        {
            string extractedNumber = match.Value;

            // Thay thế dấu chấm bằng dấu phẩy (nếu có) và loại bỏ các dấu phân cách hàng nghìn
            extractedNumber = extractedNumber.Replace(".", "").Replace(",", ".");

            // Chuyển đổi chuỗi đã được làm sạch thành số thập phân
            decimal number;
            if (Decimal.TryParse(extractedNumber, NumberStyles.Any, CultureInfo.InvariantCulture, out number))
            {
                // Chuyển đổi số thập phân thành chuỗi nguyên số
                return ((int)number).ToString();
            }
        }

        return "";
    }




    public static string PriceConversion_Product(object Price)
    {
        //Price = Regex.Replace(ConvertUtility.ToString(Price), @"[^\d]", "");
        if (ConvertUtility.ToDecimal(Price) < 10000)
            Price = 1000000;
        string _return = string.Format("{0:0}", Convert.ToDecimal(Price));
        return _return;
    }





    public static string GetIPAddress()
    {
        try
        {

            string ipAddress = string.Empty;

            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                // Lấy địa chỉ IP của client khi web server đứng sau proxy server
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].Trim();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                // Lấy địa chỉ IP của client trực tiếp kết nối với web server
                ipAddress = HttpContext.Current.Request.UserHostAddress;
            }

            return ipAddress;
        }
        catch
        {
            return string.Empty;
        }
    }


    #region Save Image To Host


    public static string SaveImageToServer(string contentHTML)
    {
        string tempData = contentHTML;

        string pathToCreate = "/upload/img/" + DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/";
        if (!Directory.Exists(HttpContext.Current.Server.MapPath(pathToCreate)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(pathToCreate));
        }

        return ProcessHtmlData(tempData, pathToCreate);
    }
    private static string GetImageThumbnail(string htmlText, string saveToFolder)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(htmlText);

        // Tìm tất cả các tag img trong nội dung
        var images = doc.DocumentNode.SelectNodes("//img[@src]");
        if (images != null)
        {
            if (images.Any())
            {
                foreach (var img in images)
                {
                    // Lấy link hình trong thuộc tính 'src'
                    var attrSrc = img.Attributes["src"];

                    // Tải các file này về host
                    string file = DownloadFile(attrSrc.Value, saveToFolder);

                    // Thay thế link hình bằng các hình vừa tải (Các hình không tải được sẽ giữ nguyên link gốc)
                    if (!string.IsNullOrEmpty(file))
                    {
                        //attrSrc.Value = saveToFolder + file;
                        return saveToFolder + file;
                    }
                }
            }
        }
        return C.NO_IMG_PATH;
    }


    /// <summary>
    /// Xử lý dữ liệu html
    /// </summary>
    /// <param name="htmlText">Nội dung html</param>
    /// <param name="saveToFolder">Thư mục chứa hình ảnh</param>
    /// <returns></returns>
    private static string ProcessHtmlData(string htmlText, string saveToFolder)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(htmlText);

        // Tìm tất cả các tag img trong nội dung
        HtmlAgilityPack.HtmlNodeCollection images = doc.DocumentNode.SelectNodes("//img[@src]");

        if (images != null)
        {
            if (images.Any())
            {
                foreach (HtmlAgilityPack.HtmlNode img in images)
                {
                    if (img.Attributes["src"] == null)
                        continue;
                    HtmlAgilityPack.HtmlAttribute attrSrc1 = img.Attributes["src"];// Lấy link hình trong thuộc tính 'src'


                    string attrSrc = ConvertUtility.ToString(attrSrc1.Value);

                    bool isHttp = attrSrc.IndexOf("http:", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool isHttps = attrSrc.IndexOf("https:", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool isNotCurrentWeb = attrSrc.IndexOf(C.ROOT_URL, StringComparison.OrdinalIgnoreCase) < 0;


                    if ((isHttp || isHttps) && isNotCurrentWeb)
                    {
                        // Tải các file này về host
                        string file = DownloadFile(attrSrc, saveToFolder);

                        // Thay thế link hình bằng các hình vừa tải (Các hình không tải được sẽ giữ nguyên link gốc)
                        if (!string.IsNullOrEmpty(file))
                        {
                            attrSrc = saveToFolder + file;
                            htmlText = htmlText.Replace(attrSrc1.Value, attrSrc);
                        }
                    }
                }
            }
        }
        return htmlText;
    }

    /// <summary>
    /// Tải tập tin
    /// </summary>
    /// <param name="urlFile">Đường dẫn tập tin</param> //cai nay la image link ak anh?yes ok
    /// <param name="saveToFolder">Thư mục lưu tập tin</param>
    /// <returns>Đường dẫn tập tin trên host</returns>
    public static string DownloadFile(string urlFile, string saveToFolder)
    {
        try
        {
            int index = urlFile.IndexOf("?");
            if (index > 0)
                urlFile = urlFile.Substring(0, index);
            var webClient = new WebClient();
            string filename = string.Format("{0}", Path.GetFileName(urlFile));
            string duongdan = Path.Combine(HttpContext.Current.Server.MapPath(saveToFolder), filename);

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                   | SecurityProtocolType.Tls11
                   | SecurityProtocolType.Tls12
                   | SecurityProtocolType.Ssl3;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlFile);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
            img.Save(duongdan);
            return filename;
        }
        catch (Exception e)
        {
            return "";
        }
    }


    #endregion


    public static DataTable CheckCoupon(DataRow drProduct)
    {
        DataTable dtCoupon = new DataTable();
        //Check Product
        dtCoupon = SqlHelper.SQLToDataTable("tblCoupon", "ID,Name,ValueVND", string.Format("ProductIDList like N'%,{0},%' AND ExpireDate>= GETDATE() AND [Using]<[MaxUsing]", drProduct["ID"]));
        if (!Utils.CheckExist_DataTable(dtCoupon))
        {
            string Product_AttributesIDList = Utils.CommaSQLRemove(drProduct["AttributesIDList"].ToString());

            if (!string.IsNullOrEmpty(Product_AttributesIDList))
            {
                List<string> attributeIDs = Product_AttributesIDList.Split(',').ToList();

                string conditions = "";
                int count = 0;
                foreach (var id in attributeIDs)
                {
                    if (count > 0)
                        conditions += " OR ";
                    conditions += string.Format(@"AttributesIDList Like N'%,{0},%'", id);
                    count++;
                }

                //string filter = string.Join(" OR ", conditions);
                dtCoupon = SqlHelper.SQLToDataTable("tblCoupon", "ID,Name,ValueVND", string.Format("({0}) AND ExpireDate>= GETDATE() AND [Using]<[MaxUsing]", conditions));
            }
        }
        return dtCoupon;
    }

    public static string RemoveHtmlAndWhiteSpace(string html)
    {
        string plainText = Regex.Replace(html, "<.*?>", "");
        plainText = plainText.Replace("&nbsp;", " ");
        plainText = plainText.Replace("&nbsp", " ");
        plainText = plainText.Replace("&amp;", "&");
        plainText = plainText.Replace("&amp", "&");
        plainText = Regex.Replace(plainText, @"\s+", " ");
        return plainText.Trim();
    }


    public static string ValidJsonGallery(string JsonInput)
    {
        string validJsonString = string.Empty;
        try
        {
            List<GalleryItem> galleryResultList = new List<GalleryItem>();
            List<GalleryItem> galleryItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GalleryItem>>(JsonInput);
            foreach (GalleryItem image in galleryItems)
            {
                if (!image.Path.ToString().Contains(C.ROOT_URL))
                {
                    galleryResultList.Add(image);
                }
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            validJsonString = serializer.Serialize(galleryResultList);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return validJsonString;
    }

    public static bool CheckIsHomePage
    {
        get
        {
            string currentUrl = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
            string homePageUrl = VirtualPathUtility.ToAbsolute("~/default.aspx").ToLower();
            if (currentUrl == homePageUrl)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
public static class RateLimiter
{
    private static Dictionary<string, Tuple<int, DateTime>> requestCount = new Dictionary<string, Tuple<int, DateTime>>();
    private static readonly object lockObject = new object();
    private static readonly int MaxRequestsPerMinute;
    static RateLimiter()
    {
        MaxRequestsPerMinute = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MAX_REQUEST_IN_MINUTES"]);
    }
    public static bool IsAllowed(string ipAddress, HttpRequest request, out int requestCountForIp, out bool error)
    {
        error = false;
        if (request != null && IsMainRequest(request))
        {
            lock (lockObject)
            {
                if (!requestCount.ContainsKey(ipAddress))
                {
                    requestCount[ipAddress] = new Tuple<int, DateTime>(1, DateTime.Now.AddMinutes(1));
                    requestCountForIp = 1;
                    return true;
                }
                if (requestCount[ipAddress].Item2 < DateTime.Now)
                {
                    requestCount[ipAddress] = new Tuple<int, DateTime>(1, DateTime.Now.AddMinutes(1));
                    requestCountForIp = 1;
                    return true;
                }
                if (requestCount[ipAddress].Item1 < MaxRequestsPerMinute)
                {
                    requestCount[ipAddress] = new Tuple<int, DateTime>(requestCount[ipAddress].Item1 + 1, requestCount[ipAddress].Item2);
                    requestCountForIp = requestCount[ipAddress].Item1;
                    return true;
                }
                requestCountForIp = 0;
                return false;
            }
        }

        error = true;
        requestCountForIp = 0;
        return true;
    }

    private static bool IsSearchBot(string userAgent)
    {
        return !string.IsNullOrEmpty(userAgent) && userAgent.ToLower().Contains("bot");
    }

    private static bool IsMainRequest(HttpRequest request)
    {
        string requestedResource = request.Path.ToLower();
        if (requestedResource.EndsWith(".html") || requestedResource.EndsWith(".aspx") || requestedResource == "/")
        {
            return true;
        }
        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
        foreach (var extension in imageExtensions)
        {
            if (requestedResource.EndsWith(extension))
            {
                return false;
            }
        }
        string[] staticResourceExtensions = { ".css", ".js", ".ico" };
        foreach (var extension in staticResourceExtensions)
        {
            if (requestedResource.EndsWith(extension))
            {
                return false;
            }
        }
        return true;
    }




}