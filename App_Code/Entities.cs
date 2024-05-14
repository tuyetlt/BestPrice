using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ebis.Utilities;
using System.Data;
using System.Reflection;

/// <summary>
/// Summary description for Entities
/// </summary>
public class Entities
{
    public Entities()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// Add thêm vào Category
    /// </summary>
    /// <param name="TagsHTML">tagsid list</param>
    /// 
    /// 

    static int _outID;

    public static List<T> ConvertTo<T>(DataTable table) where T : new()
    {
        List<T> list = new List<T>();
        try
        {
            List<String> columnNames = new List<string>();
            foreach (DataColumn Column in table.Columns)
                columnNames.Add(Column.ColumnName);
            list = table.AsEnumerable().ToList().ConvertAll<T>(row => getObject<T>(row, columnNames));
            return list;
        }
        catch
        {
            throw;
        }
    }

    private static T getObject<T>(DataRow row, List<string> columnsName) where T : new()
    {
        T obj = new T();
        try
        {
            string columnname = "";
            string value = "";
            PropertyInfo[] Properties;
            Properties = typeof(T).GetProperties();
            foreach (PropertyInfo objProperty in Properties)
            {
                columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                if (!string.IsNullOrEmpty(columnname))
                {
                    value = row[columnname].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                        {
                            value = row[columnname].ToString().Replace("$", "").Replace(",", "");
                            objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                        }
                        else
                        {
                            value = row[columnname].ToString().Replace("%", "");
                            objProperty.SetValue(obj, Convert.ChangeType(value, Type.GetType(objProperty.PropertyType.ToString())), null);
                        }
                    }
                }
            }
            return obj;
        }
        catch
        {
            return obj;
        }
    }

}


public class Search
{

    public string Key { get; set; }
    public string Value { get; set; }
    public Search()
    { }
    public Search(string key, string value)
    {
        this.Key = key;
        this.Value = value;
        //
        // TODO: Add constructor logic here
        //
    }
}
public class Valid
{
    public string Field { get; set; }
    public string Value { get; set; }

    public string Rule { get; set; }




    public Valid() { }
    public Valid(string _field, string _value, string _rule)
    {
        this.Field = _field;
        this.Value = _value;
        this.Rule = _rule;
    }
}
public class Error_Field
{

    public string Field { get; set; }

    public string Error { get; set; }

    public Error_Field() { }

    public Error_Field(string _field, string _error)
    {
        this.Field = _field;
        this.Error = _error;
    }
}


public static partial class SEO
{

    public static string meta_title { get; set; }

    public static string meta_keyword { get; set; }
    public static string meta_description { get; set; }

    public static string content_share_facebook { get; set; }

    public static string url_current { get; set; }
    public static string canonical { get; set; }

    public static string breadcrumb { get; set; }
    public static string control { get; set; }
    public static string control_name { get; set; }
}


public static partial class ControlAdminInfo
{
    public static string Name { get; set; }
    public static string ShortName { get; set; }
    public static string Control { get; set; }
    public static string SQLNameTable { get; set; }
    public static string FieldSql { get; set; }
    public static string Icon { get; set; }
    public static string Sort { get; set; }
    public static string Hide { get; set; }
}






public static partial class SEO_Schema
{
    public static string Type { get; set; }
    public static string Url { get; set; }
    public static string Image { get; set; }
    public static string Title { get; set; }
    public static string Description { get; set; }

    public static string AuthorType { get; set; }
    public static string AuthorName { get; set; }
    public static string Publisher_Type { get; set; }
    public static string Publisher_Name { get; set; }
    public static string Publisher_Logo { get; set; }
    public static string PublisherDate { get; set; }
    public static string PublisherModify { get; set; }
    public static string SKU { get; set; }
    public static string Brand { get; set; }
    public static string Price { get; set; }
    public static int RatingCount { get; set; }
    public static int RatingValue { get; set; }
    public static int ReviewRatingCount { get; set; }
    public static int ReviewRatingValue { get; set; }
}





public static partial class UserInfo
{
    public static int ID { get; set; }
    public static string FullName { get; set; }
    public static string Email { get; set; }
    public static string AccountType { get; set; }
    public static string Avatar { get; set; }
    public static int Xu { get; set; }
    public static bool IsAuthenticated { get; set; }
    public static bool IsAdmin { get; set; }

}

public class Product_Image
{
    public string Image { get; set; }

    public string Orinal { get; set; }

    public string Note { get; set; }

    public Product_Image() { }

    public Product_Image(string _Image, string _Orinal)
    {
        this.Image = _Image;
        this.Orinal = _Orinal;
    }
    public Product_Image(string _Image, string _Orinal, string _Note)
    {
        this.Image = _Image;
        this.Orinal = _Orinal;
        this.Note = _Note;
    }
}

public class Select2Data
{
    private string _id = String.Empty;
    private String _name = String.Empty;

    public Select2Data()
    {
    }
    public string id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string text
    {
        get { return _name; }
        set { _name = value; }
    }
}


public class GalleryImage
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Size { get; set; }
}


public class ColumnTableAdmin
{
    public string Field { get; set; }
    public string Text { get; set; }
    public string Show { get; set; }
}


public class AdminPermission
{
    public string Control { get; set; }
    public string Action { get; set; }
    public string Access { get; set; }
}


enum Access
{
    _access,
    _denied,
    _owner
}



public class PriceTemp
{
    public int ProductID { get; set; }
    public int PriceID { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public decimal Price1 { get; set; }
    public string ProductName { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
}



public class JsonObjectByField
{
    public string Field { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
}



public class JsonListPageAjax
{
    public string Field { get; set; }
    public string Value { get; set; }
    public string Type { get; set; }
}



public class MenuAdminJson
{
    public string Field { get; set; }
    public string Text { get; set; }
    public string Sort { get; set; }
    public string Width { get; set; }
    public bool Show { get; set; }
}

public enum AttrConfig
{
    _hide,
    _isHome,
    _productStatus,
    _productStatus_coHang,
    _productStatus_hetHang,
    _productStatus_dangNhap,
    attributeconfig,
    product,
    category,
    article,
    _all,
    _categoryProduct,
    _categoryArticle,
    _categoryDetail,
    _categoryLink,
    _permission,
    _adminPermission,
    _readOnly,
    _full,
    _addOnly,
    _editOwnerOnly,
    _supperAdmin,
    _bannerPosition,
    _bannerPositionTop,
    _bannerPositionSlider,
    _bannerPositionByCategory,
    _bannerPositionRightSlider,
    _categoryType,
    _tagType,
    _hashTagType,
    _categoryPosition,
    _mainCategoryPosition,
    _articlePosition,
    _footerPosition,
    _homePosition,
}


[Flags]
public enum AttrProductFlag
{
    None = 0,
    Home = 1,
    Priority = 2,
    New = 4,
    Best = 8,
    SaleOff = 16,
    Discount = 32,
    Selling = 64,
    Home1 = 128,
    TextHome1 = 256,
    TextHome2 = 512,
    TextHome3 = 1024,
    TextHome4 = 2048,
    TextHome5 = 4096,
}



[Flags]
public enum ProductStatusFlag
{
    None = 0,
    InStock = 1,
    OutStock = 2,
    Importing = 4,
    Contact = 8
}

[Flags]
public enum ProductVATFlag
{
    None = 0,
    Unown = 1,
    Yes = 2,
    No = 4,
}

[Flags]
public enum PositionMenuFlag
{
    None = 0,
    Main = 1,
    Top = 2,
    Bottom = 4,
    MenuSub = 8,
    MenuSubMainHome = 16,
    MenuSubMainHome2 = 32,
    Style1 = 64,
    Style2 = 128,
    SupperTop = 256
}

[Flags]
public enum LinkTypeMenuFlag
{
    None = 0,
    Link = 1,
    Content = 2,
    Product = 4,
    Article = 8
}


public enum AttrMenuFlag
{
    None = 0,
    MenuHome = 1,
    MenuPriority = 2,
    MenuHotIcon = 4,
    OpenNewWindows = 8,
    PriorityAds = 16
}


public enum BannerPositionFlag
{
    None = 0,
    HomeSlider = 1,
    RightSlider = 2,
    ByCategory = 4,
    TopBanner = 8,
    OpenNewWindows = 16,
    RightProductDetail = 32,
    Popup = 64,
    BottomSlider = 128
}

public enum ArticleFlag
{
    None = 0,
    HomeArticle = 1,
    HotCategoryHome = 2,
    HotCategory = 4
}
public enum BaseTableFlag
{
    None = 0,
    Manufacturer = 1,
    Social = 2,
    Partner = 4,
    Support = 8,
    Service = 16
}

public enum OrderStatus
{
    ProcessingInProgress = 1,
    Completed = 2,
    Canceled = 4
}

public class OrderInfo
{
    public int ProductID { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string CouponCode { get; set; }
    public decimal Price { get; set; }
    public int NumberApplyVoucher { get; set; }
    public decimal TotalPrice { get; set; }
}

public class VideoGallery
{
    public int ID { get; set; }
    public string IDYoutube { get; set; }
}



public enum ControlCurrent
{
    Home,
    ProductDetails,
    ShoppingCart,
    NewsDetail,
    ProductCategory,
    NewsCategory,
    SearchResult,
    ContentDetail,
    Checkout,
    Page404
}


public static partial class PageInfo
{
    public static string CurrentControl { get; set; }
    public static string ControlName { get; set; }
    public static int CategoryID { get; set; }
    public static string LinkEdit { get; set; }

}

public class BreadCrumb
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string FriendlyUrl { get; set; }
    public string Link { get; set; }
    public string LinkTypeMenuFlag { get; set; }
    public List<BreadCrumbChild> Child { get; set; }
}

public class BreadCrumbChild
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string FriendlyUrl { get; set; }
    public string Link { get; set; }
    public string LinkTypeMenuFlag { get; set; }
}


public class AttributeProduct
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string NameDisplay { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string FriendlyUrl { get; set; }

    public List<AttributeProductChild> attributeProductChild { get; set; }
}
public class AttributeProductChild
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string NameDisplay { get; set; }
    public string FriendlyUrl { get; set; }
    public string FriendlyUrlParent { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
}


public class GalleryItem
{
    public string Name { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
}

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string FriendlyUrl { get; set; }
    public string FriendlyUrlCategory { get; set; }
    public string Image { get; set; }
    public int Price { get; set; }
    public int Price1 { get; set; }
    public string PricePercent { get; set; }
    public string HashTagUrlList { get; set; }
    public string Link { get; set; }
}


