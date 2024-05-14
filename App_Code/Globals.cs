using System;
using System.Web;
using System.Web.UI;
using System.Data;
/// <summary>
/// Summary description for Globals
/// </summary>
public class Globals
{
    private static string _basePath = string.Empty;
    public static string ThemesSelectorID = "";
    public Globals()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string WEB_PATH
    {
        get
        {
            string _basePath = HttpContext.Current.Request.ApplicationPath;
            if (!_basePath.EndsWith("/"))
            {
                _basePath += "/";
            }
            return _basePath;
        }
    }
    public static string FullURL(string url)
    {
        if (url.StartsWith("http://"))
            return url;
        else
            return BaseUrl + url;

    }
    public static string RemoteUrl
    {
        get
        {
            return "http://" + HttpContext.Current.Request.Url.Host + BaseUrl;
        }
    }
    public static string BaseUrl
    {
        get
        {
            if (string.IsNullOrEmpty(_basePath))
            {
                _basePath = HttpContext.Current.Request.ApplicationPath;
                if (!_basePath.EndsWith("/"))
                {
                    _basePath += "/";
                }
            }
            return _basePath;
        }
    }
    public static string RootUrl
    {
        get
        {
            if (string.IsNullOrEmpty(_basePath))
            {
                _basePath = HttpContext.Current.Request.ApplicationPath;
                if (!_basePath.EndsWith("/"))
                {
                    _basePath += "/";
                }
            }
            return _basePath;
        }
    }
    public static string ThemePath(Page page, string path)
    {
        if (path.StartsWith("/") || path.StartsWith("http://"))
            return path;
        else
            return BaseUrl + "/App_Themes/" + path;
    }

    public static string NoPrice
    {
        get
        {
            return "Xin liên hệ";
        }
    }

    public static string LayNgayDang(DateTime ngaydang)
    {
        DateTime giohientai = DateTime.Now;
        TimeSpan span = new TimeSpan(giohientai.Ticks - ngaydang.Ticks);

        string strTime = "";
        if (span.Hours < 6 && span.Days < 1)
        {
            if (span.Hours > 0)
            {
                strTime = span.Hours + " giờ " + span.Minutes + " phút";
            }
            else if (span.Minutes > 1)
            {
                strTime = span.Minutes + " phút";
            }
            else
            {
                strTime = "vài giây";
            }
            strTime = "cách đây " + strTime;
        }
        else if (ngaydang.Day == giohientai.Day)
        {
            strTime = "hôm nay, lúc " + ngaydang.Hour + ":" + ngaydang.Minute;
        }
        else if (span.Days == 1)
        { strTime = "hôm qua"; }
        else if (span.Days > 1 && span.Days <= 10)
        { strTime = span.Days + " ngày trước"; }
        else
        {
            strTime = ngaydang.ToString("d/M/yyyy - H:m tt");
        }

        return strTime;
    }

    public static string LayNgayDang(DateTime ngaydang, bool chilaykhoangthoigian)
    {
        if (ngaydang == null)
            return "";

        if(chilaykhoangthoigian == false)
        {
            LayNgayDang(ngaydang);
        }
        DateTime giohientai = DateTime.Now;
        TimeSpan span = new TimeSpan(giohientai.Ticks - ngaydang.Ticks);

        string strTime = "";
        if (span.Hours < 6 && span.Days < 1)
        {
            if (span.Hours > 0)
            {
                strTime = span.Hours + " giờ " + span.Minutes + " phút";
            }
            else if (span.Minutes > 1)
            {
                strTime = span.Minutes + " phút";
            }
            else
            {
                strTime = "vài giây";
            }
            strTime = "cách đây " + strTime;
        }
        else if (ngaydang.Day == giohientai.Day)
        {
            strTime = "hôm nay, lúc " + ngaydang.Hour + ":" + ngaydang.Minute + ngaydang.ToString("tt");
        }
        else if (span.Days == 1)
        { strTime = "hôm qua"; }
        else if (span.Days > 1)
        { strTime = span.Days + " ngày trước"; }

        return strTime;
    }


    public static DataTable GetDataPositionMenu()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");

        table.Rows.Add(new String[] { "1", "Menu trên" });
        table.Rows.Add(new String[] { "2", "Menu dưới" });
        //table.Rows.Add(new String[] { "3", "Box trang chủ" });
        //table.Rows.Add(new String[] { "4", "Menu dưới" });
        //table.Rows.Add(new String[] { "5", "Tin tức" });
        //table.Rows.Add(new String[] { "6", "Hot Category" });
        //table.Rows.Add(new String[] { "7", "Menu trên cùng" });
        return table;
    }
    public static DataTable GetDataModulsCategory()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");

        table.Rows.Add(new String[] { "category", "Danh mục" });
        table.Rows.Add(new String[] { "details", "Chi tiết" });
        table.Rows.Add(new String[] { "link", "Liên kết" });
        table.Rows.Add(new String[] { "article", "Bài viết" });
        table.Rows.Add(new String[] { "Tags", "Tags" });
        table.Rows.Add(new String[] { "Hashtag", "Hash Tag" });
        return table;
    }


    public static DataTable Sap_Xep_Don_Hang()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");
        table.Rows.Add(new String[] { "NgayDatHang DESC", "Ngày đặt hàng (mới)" });
        table.Rows.Add(new String[] { "NgayDatHang ASC", "Ngày đặt hàng (cũ)" });
        table.Rows.Add(new String[] { "NgayThem DESC", "Ngày lên đơn (mới)" });
        table.Rows.Add(new String[] { "NgayThem ASC", "Ngày lên đơn (cũ)" });
        table.Rows.Add(new String[] { "DonGia DESC", "Giá tiền (cao)" });
        table.Rows.Add(new String[] { "DonGia ASC", "Giá tiền (thấp)" });
        return table;
    }


    public static DataTable Sap_Xep_Thu_Chi()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");
        table.Rows.Add(new String[] { "NgayThuChi DESC", "Ngày chi (mới)" });
        table.Rows.Add(new String[] { "NgayThuChi ASC", "Ngày chi (cũ)" });
        table.Rows.Add(new String[] { "NgayThem DESC", "Ngày nhập (mới)" });
        table.Rows.Add(new String[] { "NgayThem ASC", "Ngày nhập (cũ)" });
        table.Rows.Add(new String[] { "SoTien DESC", "Giá tiền (cao)" });
        table.Rows.Add(new String[] { "SoTien ASC", "Giá tiền (thấp)" });
        return table;
    }



    public static DataTable GetAdsPositionMenu()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");

        table.Rows.Add(new String[] { "0", "-Tất cả-" });
        table.Rows.Add(new String[] { "1", "Slider trang chủ" });
        table.Rows.Add(new String[] { "2", "Trang chủ (Theo category)" });
        table.Rows.Add(new String[] { "3", "Trang blog" });
        //table.Rows.Add(new String[] { "3", "Bên trái" });
        //table.Rows.Add(new String[] { "4", "Phía dưới slider trang chủ" });
        //table.Rows.Add(new String[] { "5", "Slider trang chủ" });
        //table.Rows.Add(new String[] { "6", "Logo dưới cùng" });
        return table;
    }
    public static DataTable NguonKhachHang()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");
        table.Rows.Add(new String[] { "0", "-Chọn nguồn khách-" });
        table.Rows.Add(new String[] { "Trên Web", "Trên Web" });
        table.Rows.Add(new String[] { "Đại lý", "Đại lý" });
        table.Rows.Add(new String[] { "Facebook", "Facebook" });
        table.Rows.Add(new String[] { "Đến xưởng", "Đến xưởng" });
        table.Rows.Add(new String[] { "Người quen", "Người quen" });
        table.Rows.Add(new String[] { "Khác", "Khác" });
        return table;
    }

    public static DataTable TrangThaiDonHang()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value");
        table.Columns.Add("Text");
        table.Rows.Add(new String[] { "Hoàn thành", "Trạng thái: Hoàn thành" });
        table.Rows.Add(new String[] { "Chờ chốt", "Trạng thái: Chờ chốt" });
        table.Rows.Add(new String[] { "Huỷ đơn", "Trạng thái: Huỷ đơn" });
        return table;
    }

    public static string CatChuoi(string value, int sokytu)
    {
        string temp = value;
        if (temp.Length > sokytu)
        {
            temp = temp.Remove(sokytu);
            if (temp.LastIndexOf(" ") != -1)
            {
                temp = temp.Remove(temp.LastIndexOf(" "));
            }
            return temp + "...";
        }
        else
            return temp;
    }
}

public class FaceBookUser
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string PictureUrl { get; set; }
    public string Email { get; set; }
}
