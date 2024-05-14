using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;


namespace WebSoSanh
{
    [Serializable]
    [XmlRoot("Product")]
    public class ProductWSS
    {
        [XmlIgnore]
        public string SimpleSku { get; set; }//NOT
        [XmlElement("simple_sku")]//SKU sản phẩm
        public string VAT { get; set; }//NOT
        [XmlElement("vat")]//Thuế VAT




        public System.Xml.XmlCDataSection VATData
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(VAT);
            }
            set
            {
                VAT = value.Value;
            }
        }
        [XmlIgnore]



        public string ParentSku { get; set; }//NOT
        [XmlElement("parent_sku")]//SKU sản phẩm cha
        public System.Xml.XmlCDataSection ParentSkuCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentSku);
            }
            set
            {
                ParentSku = value.Value;
            }
        }
        [XmlIgnore]
        public string AvailabilityInstock { get; set; }
        [XmlElement("availability_instock")]//Còn hàng hay hết hàng (not null)
        public System.Xml.XmlCDataSection AvailabilityInstockCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(AvailabilityInstock);
            }
            set
            {
                AvailabilityInstock = value.Value;
            }
        }
        [XmlIgnore]
        public string Brand { get; set; }
        [XmlElement("brand")]//Tên hãng sản xuất
        public System.Xml.XmlCDataSection BrandCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Brand);
            }
            set
            {
                Brand = value.Value;
            }
        }
        [XmlIgnore]
        public string ProductName { get; set; }
        [XmlElement("product_name")]//Tên sản phẩm (not null)
        public System.Xml.XmlCDataSection ProductNameCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ProductName);
            }
            set
            {
                ProductName = value.Value;
            }
        }
        [XmlIgnore]
        public string Description { get; set; }
        [XmlElement("description")]//Mô tả sản phẩm (not null) (~220 words, plain text)
        public System.Xml.XmlCDataSection DescriptionCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Description);
            }
            set
            {
                Description = value.Value;
            }
        }
        [XmlIgnore]
        public string Currency { get; set; }
        [XmlElement("currency")]//Tiền tệ (not null) (VND or USD)
        public System.Xml.XmlCDataSection CurrencyCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Currency);
            }
            set
            {
                Currency = value.Value;
            }
        }
        [XmlIgnore]
        public string Price { get; set; }
        [XmlElement("price")] //Giá sản phẩm (not null) (format theo định dạng xxx,xxx.xxx, VD: 180,000)
        public System.Xml.XmlCDataSection PriceCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Price);
            }
            set
            {
                Price = value.Value;
            }
        }
        [XmlIgnore]
        public string Discount { get; set; }//NOT
        [XmlElement("discount")]//Số tiền khuyến mãi (default = 0)
        public System.Xml.XmlCDataSection DiscountCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Discount);
            }
            set
            {
                Discount = value.Value;
            }
        }
        [XmlIgnore]
        public string DiscountedPrice { get; set; }
        [XmlElement("discounted_price")]//Giá khuyến mãi (default = Price)
        public System.Xml.XmlCDataSection DiscountedPriceCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(DiscountedPrice);
            }
            set
            {
                DiscountedPrice = value.Value;
            }
        }
        [XmlIgnore]
        public string ParentOfParentOfCat1 { get; set; }//NOT
        [XmlElement("parent_of_parent_of_cat1")]//Tên danh mục cha của cha 1
        public System.Xml.XmlCDataSection ParentOfParentOfCat1CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentOfParentOfCat1);
            }
            set
            {
                ParentOfParentOfCat1 = value.Value;
            }
        }
        [XmlIgnore]
        public string ParentOfCat1 { get; set; }
        [XmlElement("parent_of_cat_1")]//Tên danh mục cha 1
        public System.Xml.XmlCDataSection ParentOfCat1CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentOfCat1);
            }
            set
            {
                ParentOfCat1 = value.Value;
            }
        }
        [XmlIgnore]
        public string Category1 { get; set; }
        [XmlElement("category_1")]//Tên danh mục 1 (not null)
        public System.Xml.XmlCDataSection Category1CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Category1);
            }
            set
            {
                Category1 = value.Value;
            }
        }
        [XmlIgnore]
        public string ParentOfParentOfCat2 { get; set; }//NOT
        [XmlElement("parent_of_parent_of_cat2")]//Tên danh mục cha của cha 2
        public System.Xml.XmlCDataSection ParentOfParentOfCat2CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentOfParentOfCat2);
            }
            set
            {
                ParentOfParentOfCat2 = value.Value;
            }
        }
        [XmlIgnore]
        public string ParentOfCat2 { get; set; }
        [XmlElement("parent_of_cat_2")]//Tên danh mục cha 2
        public System.Xml.XmlCDataSection ParentOfCat2CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentOfCat2);
            }
            set
            {
                ParentOfCat2 = value.Value;
            }
        }
        [XmlIgnore]
        public string Category2 { get; set; }
        [XmlElement("category_2")]//Tên danh mục 2
        public System.Xml.XmlCDataSection Category2CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Category2);
            }
            set
            {
                Category2 = value.Value;
            }
        }
        [XmlIgnore]
        public string ParentOfParentOfCat3 { get; set; }//NOT
        [XmlElement("parent_of_parent_of_cat3")]//Tên danh mục cha của cha 3
        public System.Xml.XmlCDataSection ParentOfParentOfCat3CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentOfParentOfCat3);
            }
            set
            {
                ParentOfParentOfCat3 = value.Value;
            }
        }
        [XmlIgnore]
        public string ParentOfCat3 { get; set; }
        [XmlElement("parent_of_cat3")]//Tên danh mục cha 3
        public System.Xml.XmlCDataSection ParentOfCat3CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(ParentOfCat3);
            }
            set
            {
                ParentOfCat3 = value.Value;
            }
        }
        [XmlIgnore]
        public string Category3 { get; set; }
        [XmlElement("category_3")]//Tên danh mục 3
        public System.Xml.XmlCDataSection Category3CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Category3);
            }
            set
            {
                Category3 = value.Value;
            }
        }
        [XmlIgnore]
        public string PictureUrl { get; set; }
        [XmlElement("picture_url")]//Anh đại diện của sản phẩm (not null)
        public System.Xml.XmlCDataSection PictureUrlCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(PictureUrl);
            }
            set
            {
                PictureUrl = value.Value;
            }
        }
        [XmlIgnore]
        public string PictureUrl2 { get; set; }
        [XmlElement("picture_url2")]//Anh liên quan, ảnh mô tả sản phẩm
        public System.Xml.XmlCDataSection PictureUrl2CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(PictureUrl2);
            }
            set
            {
                PictureUrl2 = value.Value;
            }
        }
        [XmlIgnore]
        public string PictureUrl3 { get; set; }
        [XmlElement("picture_url3")]
        public System.Xml.XmlCDataSection PictureUrl3CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(PictureUrl3);
            }
            set
            {
                PictureUrl3 = value.Value;
            }
        }
        [XmlIgnore]
        public string PictureUrl4 { get; set; }
        [XmlElement("picture_url4")]
        public System.Xml.XmlCDataSection PictureUrl4CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(PictureUrl4);
            }
            set
            {
                PictureUrl4 = value.Value;
            }
        }
        [XmlIgnore]
        public string PictureUrl5 { get; set; }
        [XmlElement("picture_url5")]
        public System.Xml.XmlCDataSection PictureUrl5CDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(PictureUrl5);
            }
            set
            {
                PictureUrl5 = value.Value;
            }
        }
        [XmlIgnore]
        public string URL { get; set; }//NOT
        [XmlElement("URL")]//Đường dẫn đến sản phẩm (not null)
        public System.Xml.XmlCDataSection URLCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(URL);
            }
            set
            {
                URL = value.Value;
            }
        }
        [XmlIgnore]
        public string Promotion { get; set; }
        [XmlElement("promotion")]//Thông tin khuyến mãi (plain text có dấu)
        public System.Xml.XmlCDataSection PromotionCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(Promotion);
            }
            set
            {
                Promotion = value.Value;
            }
        }
        [XmlIgnore]
        public string DeliveryPeriod { get; set; }//NOT
        [XmlElement("delivery_period")]//Thời gian giao hàng
        public System.Xml.XmlCDataSection DeliveryPeriodCDATA
        {
            get
            {
                return new System.Xml.XmlDocument().CreateCDataSection(DeliveryPeriod);
            }
            set
            {
                DeliveryPeriod = value.Value;
            }
        }
        public ProductWSS()
        {
            SimpleSku = "";
            ParentSku = "";
            AvailabilityInstock = "false";
            Brand = "";
            ProductName = "";
            Description = "";
            Currency = "";
            Price = "0";
            Discount = "0";
            DiscountedPrice = "0";
            ParentOfParentOfCat1 = "";
            ParentOfCat1 = "";
            Category1 = "";
            ParentOfParentOfCat2 = "";
            ParentOfCat2 = "";
            Category2 = "";
            ParentOfParentOfCat3 = "";
            ParentOfCat3 = "";
            Category3 = "";
            PictureUrl = "";
            PictureUrl2 = "";
            PictureUrl3 = "";
            PictureUrl4 = "";
            PictureUrl5 = "";
            URL = "";
            Promotion = "";
            DeliveryPeriod = "";
            VAT = "0";
        }
    }

    public class MappingData
    {
        public static string ImgStore = ConfigurationManager.AppSettings["ImageStore"].ToString();
        /// <summary>
        /// get list product from DB
        /// </summary>
        /// <returns></returns>
        public static DataTable GetProductsFromDb()
        {
            DataTable data = new DataTable();
            var query =
                "SELECT SP.[TINHTRANG],HA.[TENLOAI] AS TENHANG,SP.[TENSP],SP.[MOTA],SP.[DONVI],SP.[GIA],SP.[GIAKM],LO2.[TENLOAI] AS CATECHA,LO.[TENLOAI] AS CTAECON,SP.[HINHNHO],SP.[HINHLON],SP.[KHUYENMAI],SP.[IDSP],LO.[IDLOAI] AS IDCATECON, LO2.[IDLOAI] AS IDCATECHA " +
                "FROM [mykim_dtb2013].[dbo].[SANPHAM] AS SP LEFT JOIN [mykim_dtb2013].[dbo].[SANPHAM_HANG] AS HA ON SP.[IDH] = HA.[IDH]" +
                "LEFT JOIN [mykim_dtb2013].[dbo].[SANPHAM_LOAI] AS LO ON SP.[IDLOAI] = LO.[IDLOAI]" +
                "LEFT JOIN [mykim_dtb2013].[dbo].[SANPHAM_LOAI] AS LO2 ON LO.[IDCHA] = LO2.[IDLOAI]" +
                "WHERE SP.[HIEULUC]=1 AND HA.[HIEULUC]=1 AND LO.[HIEULUC]=1 AND SP.[GIA]>0";
            try
            {
                data = ConnectDB.ExecuteQuery(query);
            }
            catch (Exception)
            {
            }
            return data;
        }
        /// <summary>
        /// mapping product from data row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static ProductWSS MapProduct(DataRow row)
        {
            ProductWSS product = new ProductWSS();
            product.AvailabilityInstock = (row["TINHTRANG"] != DBNull.Value && Functions.Object2Boolean(row["TINHTRANG"])).ToString();
            product.Brand = row["TENHANG"] != DBNull.Value ? row["TENHANG"].ToString() : string.Empty;
            product.ProductName = row["TENSP"] != DBNull.Value ? row["TENSP"].ToString() : string.Empty;
            product.Description = row["MOTA"] != DBNull.Value ? Functions.StripHTML(row["MOTA"].ToString()).Trim() : string.Empty;
            product.Currency = row["DONVI"] != DBNull.Value ? row["DONVI"].ToString() : string.Empty;
            var oldPrice = row["GIA"] != DBNull.Value ? Functions.Object2Long(row["GIA"]) : 0;
            var newPrice = row["GIAKM"] != DBNull.Value ? Functions.Object2Long(row["GIAKM"]) : 0;
            newPrice = newPrice > 0 ? newPrice : oldPrice;
            product.Price = Functions.FormatPriceVN(oldPrice);
            product.Discount = Functions.FormatPriceVN(oldPrice - newPrice);
            product.DiscountedPrice = Functions.FormatPriceVN(newPrice);
            product.ParentOfCat1 = row["CATECHA"] != DBNull.Value ? row["CATECHA"].ToString() : string.Empty;
            product.Category1 = row["CTAECON"] != DBNull.Value ? row["CTAECON"].ToString() : string.Empty;
            product.PictureUrl = row["HINHNHO"] != DBNull.Value ? (ImgStore + row["HINHNHO"].ToString()).Replace("../", "") : string.Empty;
            product.PictureUrl2 = row["HINHLON"] != DBNull.Value ? (ImgStore + row["HINHLON"].ToString()).Replace("../", "") : string.Empty;
            product.Promotion = row["KHUYENMAI"] != DBNull.Value ? Functions.StripHTML(row["KHUYENMAI"].ToString()).Trim() : string.Empty;
            var idSp = row["IDSP"] != DBNull.Value ? Functions.Object2Long(row["IDSP"]) : 0;
            var idCateCon = row["IDCATECON"] != DBNull.Value ? Functions.Object2Integer(row["IDCATECON"]) : 0;
            var idCateCha = row["IDCATECHA"] != DBNull.Value ? Functions.Object2Integer(row["IDCATECHA"]) : 0;
            product.URL = Functions.GetDetailProductUrl(product.ProductName, idSp, idCateCon, idCateCha);
            return product;
        }
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        public static List<ProductWSS> GetProducts()
        {
            List<ProductWSS> products = new List<ProductWSS>();
            var data = GetProductsFromDb();
            if (data != null)
            {
                foreach (DataRow row in data.Rows)
                {
                    products.Add(MapProduct(row));
                }
            }
            return products;
        }
    }

    public class Functions
    {
        static string extension = ".html";

        public static int Object2Integer(object value)
        {
            if (null == value) return int.MinValue;
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return int.MinValue;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Long
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Long, nếu lỗi return long.MinValue</returns>
        public static long Object2Long(object value)
        {
            if (null == value) return long.MinValue;
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return long.MinValue;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu Double
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu Double, nếu lỗi return double.NaN</returns>
        public static double Object2Double(object value)
        {
            if (null == value) return 0;
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu float
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>Số kiểu float, nếu lỗi return float.NaN</returns>
        public static float Object2Float(object value)
        {
            if (null == value) return float.NaN;
            try
            {
                return float.Parse(value.ToString());
            }
            catch
            {
                return float.NaN;
            }
        }
        public static Decimal Object2Decimal(object value)
        {
            if (null == value) return Decimal.Zero;
            try
            {
                return Decimal.Parse(value.ToString());
            }
            catch
            {
                return Decimal.Zero;
            }
        }
        /// <summary>
        /// Chuyển đổi 1 giá trị sang kiểu boolean
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <returns>giá trị kiểu boolean, nếu lỗi return false</returns>
        public static bool Object2Boolean(object value)
        {
            if (null == value) return false;
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Price"></param>
        /// <returns></returns>
        public static string FormatPriceVN(object Price)
        {
            CultureInfo ci = new CultureInfo("en-US"); // us
            long price = Object2Long(Price);
            return price.ToString("n0", ci);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlToPlainText(string html)
        {
            const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
            const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
            const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
            var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
            var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
            var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

            var text = html;
            //Decode html specific characters
            //text = System.Net.WebUtility.HtmlDecode(text);
            text = System.Web.HttpUtility.HtmlDecode(text);
            //Remove tag whitespace/line breaks
            text = tagWhiteSpaceRegex.Replace(text, "><");
            //Replace <br /> with line breaks
            text = lineBreakRegex.Replace(text, Environment.NewLine);
            //Strip formatting
            text = stripFormattingRegex.Replace(text, string.Empty);

            return text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HTMLText"></param>
        /// <returns></returns>
        public static string StripHTML(string HTMLText)
        {
            var reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            //return System.Net.WebUtility.HtmlDecode(reg.Replace(HTMLText, ""));
            return System.Web.HttpUtility.HtmlDecode(reg.Replace(HTMLText, ""));
        }

        public static string GetDetailProductUrl(string Name, long Id, int CateId, int ParentCateId)
        {
            var parentCate = ParentCateId > 0 ? ParentCateId.ToString() : "";
            var cate = CateId > 0 ? CateId.ToString() : "";
            return ConfigurationManager.AppSettings["Domain"] + "chi-tiet/" + UnicodeToKoDauAndGach(Name) + "-" + parentCate + "-" + cate + "-" + Id + extension;
        }


        #region Chuyen doi xau dang unicode co dau sang dang khong dau
        private const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        private const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            s = s.Trim();
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
        public static string UnicodeToKoDauAndGach(string s)
        {
            string strChar = "-abcdefghijklmnopqrstxyzuvxw0123456789 ";
            //string retVal = UnicodeToKoDau(s);
            //s = s.Replace("-", " ");
            //s = s.Replace("–", "");
            s = s.Replace("  ", " ");
            //s = s.Replace("  ", " ");
            s = s.Replace("+", "-");
            s = UnicodeToKoDau(s.ToLower().Trim());
            string sReturn = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (strChar.IndexOf(s[i]) > -1)
                {
                    if (s[i] != ' ')
                        sReturn += s[i];
                    else if (i > 0 && s[i - 1] != '-')
                        sReturn += "-";
                }
            }
            sReturn = sReturn.Replace("--", "-");
            return sReturn;
        }
        #endregion
    }

    public class ConnectDB
    {
        public static DataTable ExecuteQuery(string query)
        {
            //SELECT SP.[TINHTRANG],HA.[TENLOAI] AS TENHANG,SP.[TENSP],SP.[MOTA],SP.[DONVI],SP.[GIA],SP.[GIAKM],LO2.[TENLOAI] AS CATECHA,LO.[TENLOAI] AS CTAECON,SP.[HINHNHO],SP.[HINHLON],SP.[KHUYENMAI]
            //FROM [mykim_dtb2013].[dbo].[SANPHAM] AS SP LEFT JOIN [mykim_dtb2013].[dbo].[SANPHAM_HANG] AS HA ON SP.[IDH] = HA.[IDH]
            //LEFT JOIN [mykim_dtb2013].[dbo].[SANPHAM_LOAI] AS LO ON SP.[IDLOAI] = LO.[IDLOAI]
            //LEFT JOIN [mykim_dtb2013].[dbo].[SANPHAM_LOAI] AS LO2 ON LO.[IDCHA] = LO2.[IDLOAI]
            //WHERE SP.[HIEULUC]=1 AND HA.[HIEULUC]=1 AND LO.[HIEULUC]=1 AND SP.[GIA]>0
            DataTable dataTable = new DataTable();
            string connetionString = ConnectionString.MainConnection;
            SqlConnection connection = new SqlConnection(connetionString);
            //SqlDataReader dataReader;
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                //dataReader = command.ExecuteReader();
                new SqlDataAdapter(command).Fill(dataTable);
                //while (dataReader.Read())
                //{
                //}
                //dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                return dataTable;
            }
            return dataTable;
        }
    }

    public class ConnectionString
    {
        public static String MainConnection
        {
            get { return ConfigurationManager.AppSettings["MainConnectionString"].ToString(); }
        }
    }

    public class XmlSerialize
    {
        public static string Serialize(List<ProductWSS> list)//, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<ProductWSS>), new XmlRootAttribute("Products"));
            //using (TextWriter writer = new StreamWriter(HttpContext.Current.Server.MapPath(filePath), false, System.Text.Encoding.UTF8))
            using (StringWriter writer = new StringWriterUtf8())
            {
                serializer.Serialize(writer, list);
                return writer.ToString();
            }
        }
    }
    public class StringWriterUtf8 : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

}