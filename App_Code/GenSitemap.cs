using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml.Linq;


/// <summary>
/// Summary description for GenSitemap
/// </summary>
public class GenSitemap
{
    public static void GenGoogleShopping(HttpContext context, string web)
    {
        string Uu_Tien = "NO";
        string filterCat = "1=1";
        int countErr = 0;
        int count = 0;
        string filter = string.Format("(Hide is null OR Hide=0) AND Len(FriendlyUrlCategory)>0 AND Price>0 AND " + filterCat); //(ID<= 14153 AND ID>=14180) AND 

        DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,Price,Price1,Gallery, Hide,FriendlyUrlCategory,Brand,ProductType,AttributesIDList,CategoryNameList,CategoryIDList,CategoryIDParentList", filter, "ID DESC", 1, 10000);
        if (Utils.CheckExist_DataTable(dt))
        {
            if (web == "hoanghai" || web == "bestprice")
            {
                List<ProductFeed> products = new List<ProductFeed>();

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        string dk1 = GenerateFriendlyUrlCondition(dr["CategoryIDList"].ToString());
                        string dk2 = GenerateFriendlyUrlCondition(dr["CategoryIDParentList"].ToString());

                        string dk = "";
                        if (!string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND ({0} OR {1})", dk1, dk2);
                        }
                        else if (string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk2);
                        }
                        else if (!string.IsNullOrEmpty(dk1) && string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk1);
                        }

                        DataTable dtCate = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID", string.Format("(Hide is null OR Hide=0) AND AttrMenuFlag & {0}<> 0 {1}", (int)AttrMenuFlag.PriorityAds, dk), "ID DESC");
                        {
                            if (Utils.CheckExist_DataTable(dtCate))
                            {
                                Uu_Tien = "YES";
                            }
                            else
                            {
                                Uu_Tien = "NO";
                            }
                        }

                        string CategoryIDList = Utils.CommaSQLRemove(dr["CategoryIDList"].ToString());

                        DataTable dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,AttrMenuFlag", Utils.CreateFilterHide + " AND ID IN (" + CategoryIDList + ")", "ID DESC");
                        if (Utils.CheckExist_DataTable(dtCat))
                        {
                            int Price = ConvertUtility.ToInt32(dr["Price"]);
                            string imgPath = Utils.GetFirstImageInGallery_Json(dr["Gallery"].ToString());
                            if (Price > 0 && !imgPath.Contains("no-img"))
                            {
                                string type = dr["ProductType"].ToString().Replace(">", "-");
                                if (string.IsNullOrEmpty(type))
                                    type = "0";

                                List<string> attrList = GenAttribute(dr["AttributesIDList"].ToString(), dr["CategoryNameList"].ToString());
                                string attr = "0";
                                if (attrList.Count > 0)
                                    attr = attrList[0];

                                string brand = attr;
                                if (brand == "0")
                                    brand = ConfigWeb.SiteName;

                                string categoryName = "0";
                                string CategoryNameList = dr["CategoryNameList"].ToString().Trim(',');
                                if (!string.IsNullOrEmpty(CategoryNameList))
                                    categoryName = CategoryNameList;

                                DateTime date = DateTime.Now;
                                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                                var lastDayOfMonth = firstDayOfMonth.AddMonths(2).AddDays(-1);
                                string NgayDau = firstDayOfMonth.ToString("yyyy-MM-dd") + "T08:00+0700";
                                string NgayCuoi = lastDayOfMonth.ToString("yyyy-MM-dd") + "T23:00+0700";
                                string NgayUuDai = NgayDau + "/" + NgayCuoi;

                                //writer.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}",
                                //    dr["ID"].ToString(),
                                //    Utils.FixCapitalization(dr["Name"].ToString()),
                                //    dr["Name"] + ", " + ConfigWeb.SiteName,
                                //    TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]),
                                //    "mới",
                                //    Price.ToString().Replace(",0000", "") + " VND",
                                //    "còn hàng",
                                //    imgPath,
                                //    brand, dr["ProductType"].ToString(), type, type + "_" + attr, categoryName, Uu_Tien),
                                //    System.Text.Encoding.Unicode);


                                count++;
                                ProductFeed product = new ProductFeed
                                {
                                    Id = ConvertUtility.ToInt32(dr["ID"]),
                                    Title = Utils.FixCapitalization(dr["Name"].ToString()),
                                    Description = dr["Name"] + ", " + ConfigWeb.SiteName,
                                    Link = TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]),
                                    ImageLink = imgPath,
                                    Price = Price.ToString().Replace(",0000", "") + " VND",
                                    Availability = "in stock",
                                    Condition = "new",
                                    Brand = brand,
                                    Gtin = "",
                                    Mpn = "",
                                    ProductType = dr["ProductType"].ToString(),
                                    CustomLabel0 = type,
                                    CustomLabel1 = type + "_" + attr,
                                    CustomLabel2 = categoryName,
                                    CustomLabel3 = Uu_Tien
                                };

                                products.Add(product);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        countErr++;
                        //HttpResponse response = context.Response;
                        //response.Write(string.Format("Lỗi GG: {0} - {1} \n<br />", dr["ID"], dr["Name"]));
                    }
                }

                HttpResponse res = context.Response;
                res.Write(count +" sản phẩm, " + countErr + " lỗi!");


                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/upload/ProductFeed.xml");
                using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("rss");
                    writer.WriteAttributeString("version", "2.0");
                    writer.WriteAttributeString("xmlns", "g", null, "http://base.google.com/ns/1.0");

                    writer.WriteStartElement("channel");
                    writer.WriteElementString("title", ConfigWeb.MetaTitle);
                    writer.WriteElementString("link", C.ROOT_URL);
                    writer.WriteElementString("description", ConfigWeb.MetaDescription);

                    foreach (var product in products)
                    {
                        writer.WriteStartElement("item");

                        writer.WriteElementString("g", "id", null, product.Id.ToString());
                        writer.WriteElementString("g", "title", null, product.Title);
                        writer.WriteElementString("g", "description", null, product.Description);
                        writer.WriteElementString("g", "link", null, product.Link);
                        writer.WriteElementString("g", "image_link", null, product.ImageLink);
                        writer.WriteElementString("g", "price", null, product.Price);
                        writer.WriteElementString("g", "availability", null, product.Availability);
                        writer.WriteElementString("g", "condition", null, product.Condition);
                        writer.WriteElementString("g", "brand", null, product.Brand);
                        writer.WriteElementString("g", "gtin", null, product.Gtin);
                        writer.WriteElementString("g", "mpn", null, product.Mpn);
                        writer.WriteElementString("g", "product_type", null, product.ProductType);
                        writer.WriteElementString("g", "custom_label_0", null, product.CustomLabel0);
                        writer.WriteElementString("g", "custom_label_1", null, product.CustomLabel1);
                        writer.WriteElementString("g", "custom_label_2", null, product.CustomLabel2);
                        writer.WriteElementString("g", "custom_label_3", null, product.CustomLabel3);

                        writer.WriteEndElement(); // item
                    }

                    writer.WriteEndElement(); // channel
                    writer.WriteEndElement(); // rss
                    writer.WriteEndDocument();
                }
            }

            if (web == "duclong")
            {
                string link1 = "/upload/gg-shopping-list-duc-long.txt";
                string savepath_dl = HttpContext.Current.Request.MapPath("~" + link1);
                TextWriter writer_dl = new StreamWriter(savepath_dl);
                writer_dl.WriteLine("id\ttiêu đề\tmô tả\tliên kết\ttình trạng\tgiá\tcòn hàng\tliên kết hình ảnh\tnhãn hiệu\tloại sản phẩm\tcustom_label_0\tcustom_label_1\tcustom_label_2\tcustom_label_3", System.Text.Encoding.Unicode);
                List<ProductFeed> products = new List<ProductFeed>();

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {

                        string dk1 = GenerateFriendlyUrlCondition(dr["CategoryIDList"].ToString());
                        string dk2 = GenerateFriendlyUrlCondition(dr["CategoryIDParentList"].ToString());

                        string dk = "";
                        if (!string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND ({0} OR {1})", dk1, dk2);
                        }
                        else if (string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk2);
                        }
                        else if (!string.IsNullOrEmpty(dk1) && string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk1);
                        }

                        DataTable dtCate = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID", string.Format("(Hide is null OR Hide=0) AND AttrMenuFlag & {0}<> 0 {1}", (int)AttrMenuFlag.PriorityAds, dk), "ID DESC");
                        {
                            if (Utils.CheckExist_DataTable(dtCate))
                            {
                                Uu_Tien = "YES";
                            }
                            else
                            {
                                Uu_Tien = "NO";
                            }
                        }

                        string CategoryIDList = Utils.CommaSQLRemove(dr["CategoryIDList"].ToString());

                        DataTable dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,AttrMenuFlag", Utils.CreateFilterHide + " AND ID IN (" + CategoryIDList + ")", "ID DESC");
                        if (Utils.CheckExist_DataTable(dtCat))
                        {
                            int Price = ConvertUtility.ToInt32(dr["Price"]);
                            string imgPath = Utils.GetFirstImageInGallery_Json(dr["Gallery"].ToString());
                            if (Price > 0 && !imgPath.Contains("no-img"))
                            {
                                string type = dr["ProductType"].ToString().Replace(">", "-");
                                if (string.IsNullOrEmpty(type))
                                    type = "0";

                                List<string> attrList = GenAttribute(dr["AttributesIDList"].ToString(), dr["CategoryNameList"].ToString());
                                string attr = "0";
                                if (attrList.Count > 0)
                                    attr = attrList[0];

                                string brand = attr;
                                if (brand == "0")
                                    brand = ConfigWeb.SiteName;

                                string categoryName = "0";
                                string CategoryNameList = dr["CategoryNameList"].ToString().Trim(',');
                                if (!string.IsNullOrEmpty(CategoryNameList))
                                    categoryName = CategoryNameList;

                                DateTime date = DateTime.Now;
                                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                                var lastDayOfMonth = firstDayOfMonth.AddMonths(2).AddDays(-1);
                                string NgayDau = firstDayOfMonth.ToString("yyyy-MM-dd") + "T08:00+0700";
                                string NgayCuoi = lastDayOfMonth.ToString("yyyy-MM-dd") + "T23:00+0700";
                                string NgayUuDai = NgayDau + "/" + NgayCuoi;


                                //writer_dl.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}",
                                //    dr["ID"].ToString(),
                                //    Utils.FixCapitalization(dr["Name"].ToString()),
                                //    dr["Name"] + ", " + "Đức Long",
                                //    TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]).Replace("dienmayhoanghai.vn", "dienmayduclong.vn"),
                                //    "mới",
                                //    Price.ToString().Replace(",0000", "") + " VND",
                                //    "còn hàng",
                                //    imgPath.Replace("dienmayhoanghai.vn", "img.dienmayduclong.vn"),
                                //    brand, dr["ProductType"].ToString(), type, type + "_" + attr, categoryName, Uu_Tien),
                                //    System.Text.Encoding.Unicode);



                                ProductFeed product = new ProductFeed
                                {
                                    Id = ConvertUtility.ToInt32(dr["ID"]),
                                    Title = Utils.FixCapitalization(dr["Name"].ToString()),
                                    Description = dr["Name"] + ", " + "Đức Long",
                                    Link = TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]).Replace("dienmayhoanghai.vn", "dienmayduclong.vn"),
                                    ImageLink = imgPath.Replace("dienmayhoanghai.vn", "img.dienmayduclong.vn"),
                                    Price = Price.ToString().Replace(",0000", "") + " VND",
                                    Availability = "in stock",
                                    Condition = "new",
                                    Brand = brand,
                                    Gtin = "",
                                    Mpn = "",
                                    ProductType = dr["ProductType"].ToString(),
                                    CustomLabel0 = type,
                                    CustomLabel1 = type + "_" + attr,
                                    CustomLabel2 = categoryName,
                                    CustomLabel3 = Uu_Tien
                                };

                                products.Add(product);

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        countErr++;
                        HttpResponse response = context.Response;
                        //response.Write(string.Format("Lỗi GG: {0} - {1} \n<br />", dr["ID"], dr["Name"]));
                    }
                }


                HttpResponse res = context.Response;
                res.Write(countErr);

                string filePath = System.Web.HttpContext.Current.Server.MapPath("~/upload/ProductFeedDucLong.xml");
                using (XmlWriter writer = XmlWriter.Create(filePath, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("rss");
                    writer.WriteAttributeString("version", "2.0");
                    writer.WriteAttributeString("xmlns", "g", null, "http://base.google.com/ns/1.0");

                    writer.WriteStartElement("channel");
                    writer.WriteElementString("title", "Product Feed");
                    writer.WriteElementString("link", "https://dienmayduclong.vn");
                    writer.WriteElementString("description", "Điện máy Đức Long Data Feed");

                    foreach (var product in products)
                    {
                        writer.WriteStartElement("item");

                        writer.WriteElementString("g", "id", null, product.Id.ToString());
                        writer.WriteElementString("g", "title", null, product.Title);
                        writer.WriteElementString("g", "description", null, product.Description);
                        writer.WriteElementString("g", "link", null, product.Link);
                        writer.WriteElementString("g", "image_link", null, product.ImageLink);
                        writer.WriteElementString("g", "price", null, product.Price);
                        writer.WriteElementString("g", "availability", null, product.Availability);
                        writer.WriteElementString("g", "condition", null, product.Condition);
                        writer.WriteElementString("g", "brand", null, product.Brand);
                        writer.WriteElementString("g", "gtin", null, product.Gtin);
                        writer.WriteElementString("g", "mpn", null, product.Mpn);
                        writer.WriteElementString("g", "product_type", null, product.ProductType);
                        writer.WriteElementString("g", "custom_label_0", null, product.CustomLabel0);
                        writer.WriteElementString("g", "custom_label_1", null, product.CustomLabel1);
                        writer.WriteElementString("g", "custom_label_2", null, product.CustomLabel2);
                        writer.WriteElementString("g", "custom_label_3", null, product.CustomLabel3);

                        writer.WriteEndElement(); // item
                    }

                    writer.WriteEndElement(); // channel
                    writer.WriteEndElement(); // rss
                    writer.WriteEndDocument();
                }

            }



            if (web == "facebook")
            {
                StringBuilder writerFB = new StringBuilder();
                writerFB.AppendLine("id\ttitle\tdescription\tavailability\tcondition\tprice\tsale_price\tlink\timage_link\tbrand\tcustom_label_0\tcustom_label_1\tcustom_label_2\tcustom_label_3");

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        string dk1 = GenerateFriendlyUrlCondition(dr["CategoryIDList"].ToString());
                        string dk2 = GenerateFriendlyUrlCondition(dr["CategoryIDParentList"].ToString());

                        string dk = "";
                        if (!string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND ({0} OR {1})", dk1, dk2);
                        }
                        else if (string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk2);
                        }
                        else if (!string.IsNullOrEmpty(dk1) && string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk1);
                        }

                        DataTable dtCate = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID", string.Format("(Hide is null OR Hide=0) AND AttrMenuFlag & {0}<> 0 {1}", (int)AttrMenuFlag.PriorityAds, dk), "ID DESC");
                        {
                            if (Utils.CheckExist_DataTable(dtCate))
                            {
                                Uu_Tien = "YES";
                            }
                            else
                            {
                                Uu_Tien = "NO";
                            }
                        }

                        string CategoryIDList = Utils.CommaSQLRemove(dr["CategoryIDList"].ToString());

                        DataTable dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,AttrMenuFlag", Utils.CreateFilterHide + " AND ID IN (" + CategoryIDList + ")", "ID DESC");
                        if (Utils.CheckExist_DataTable(dtCat))
                        {
                            int Price = ConvertUtility.ToInt32(dr["Price"]);
                            string imgPath = Utils.GetFirstImageInGallery_Json(dr["Gallery"].ToString());
                            if (Price > 0 && !imgPath.Contains("no-img"))
                            {
                                string type = dr["ProductType"].ToString().Replace(">", "-");
                                if (string.IsNullOrEmpty(type))
                                    type = "0";

                                List<string> attrList = GenAttribute(dr["AttributesIDList"].ToString(), dr["CategoryNameList"].ToString());
                                string attr = "0";
                                if (attrList.Count > 0)
                                    attr = attrList[0];

                                string brand = attr;
                                if (brand == "0")
                                    brand = ConfigWeb.SiteName;

                                string categoryName = "0";
                                string CategoryNameList = dr["CategoryNameList"].ToString().Trim(',');
                                if (!string.IsNullOrEmpty(CategoryNameList))
                                    categoryName = CategoryNameList;

                                int Price1 = ConvertUtility.ToInt32(dr["Price1"]);
                                if (Price1 > 0 && Price < Price1)
                                {
                                    Price = ConvertUtility.ToInt32(dr["Price1"]);
                                    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                }
                                else
                                {
                                    Price = ConvertUtility.ToInt32(dr["Price"]);
                                    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                }

                                DateTime date = DateTime.Now;
                                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                                string NgayDau = firstDayOfMonth.ToString("yyyy-MM-dd") + "T08:00+0700";
                                string NgayCuoi = lastDayOfMonth.ToString("yyyy-MM-dd") + "T23:00+0700";
                                string NgayUuDai = NgayDau + "/" + NgayCuoi;

                                writerFB.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}",
                                    dr["ID"].ToString(), //0 id
                                    Utils.FixCapitalization(dr["Name"].ToString()), //1 title
                                    dr["Name"] + " " + ConfigWeb.SiteName, //2 description
                                    "in stock", //3 availability
                                    "new", //4 condition
                                    Price.ToString().Replace(",0000", "") + " VND", //5 price
                                     Price1.ToString().Replace(",0000", "") + " VND", //6 sale price
                                    TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]), //7 link
                                    imgPath, //image_link
                                    brand, //brand
                                    dr["ProductType"].ToString(), //custom_label_0
                                    type, //custom_label_1 
                                    type + "_" + attr, //custom_label_2
                                    categoryName, //custom_label_3
                                    Uu_Tien //custom_label_4
                                    ));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HttpResponse response = context.Response;
                        response.Write(string.Format("Lỗi FB: {0} - {1} \n<br />", dr["ID"], dr["Name"]));
                    }
                }

                string savepath1 = HttpContext.Current.Request.MapPath("~/upload/");

                string filePath = Path.Combine(savepath1, "products.tsv");
                using (StreamWriter writer1 = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer1.Write(writerFB.ToString());
                }
            }

        }



        if (web == "facebook_sales")
        {
            //Giảm giá sập sàn
            string filterSale = string.Format("(Hide is null OR Hide=0) AND Len(FriendlyUrlCategory)>0 AND Price>0 AND (CategoryIDList like N'%,2166,%' OR CategoryIDParentList like N'%,2166,%')");
            DataTable dtSale = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,Price,Price1,Gallery, Hide,FriendlyUrlCategory,Brand,ProductType,AttributesIDList,CategoryNameList,CategoryIDList,CategoryIDParentList", filterSale, "ID DESC");
            if (Utils.CheckExist_DataTable(dtSale))
            {
                StringBuilder writerFBSale = new StringBuilder();
                writerFBSale.AppendLine("id\ttitle\tdescription\tavailability\tcondition\tprice\tsale_price\tlink\timage_link\tbrand\tcustom_label_0\tcustom_label_1\tcustom_label_2\tcustom_label_3");

                foreach (DataRow dr in dtSale.Rows)
                {
                    try
                    {
                        string dk1 = GenerateFriendlyUrlCondition(dr["CategoryIDList"].ToString());
                        string dk2 = GenerateFriendlyUrlCondition(dr["CategoryIDParentList"].ToString());

                        string dk = "";
                        if (!string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND ({0} OR {1})", dk1, dk2);
                        }
                        else if (string.IsNullOrEmpty(dk1) && !string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk2);
                        }
                        else if (!string.IsNullOrEmpty(dk1) && string.IsNullOrEmpty(dk2))
                        {
                            dk = string.Format("AND {0}", dk1);
                        }

                        DataTable dtCate = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID", string.Format("(Hide is null OR Hide=0) AND AttrMenuFlag & {0}<> 0 {1}", (int)AttrMenuFlag.PriorityAds, dk), "ID DESC");
                        {
                            if (Utils.CheckExist_DataTable(dtCate))
                            {
                                Uu_Tien = "YES";
                            }
                            else
                            {
                                Uu_Tien = "NO";
                            }
                        }

                        string CategoryIDList = Utils.CommaSQLRemove(dr["CategoryIDList"].ToString());

                        DataTable dtCat = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,AttrMenuFlag", Utils.CreateFilterHide + " AND ID IN (" + CategoryIDList + ")", "ID DESC");
                        if (Utils.CheckExist_DataTable(dtCat))
                        {
                            int Price = ConvertUtility.ToInt32(dr["Price"]);
                            string imgPath = Utils.GetFirstImageInGallery_Json(dr["Gallery"].ToString());
                            if (Price > 0 && !imgPath.Contains("no-img"))
                            {
                                string type = dr["ProductType"].ToString().Replace(">", "-");
                                if (string.IsNullOrEmpty(type))
                                    type = "0";

                                List<string> attrList = GenAttribute(dr["AttributesIDList"].ToString(), dr["CategoryNameList"].ToString());
                                string attr = "0";
                                if (attrList.Count > 0)
                                    attr = attrList[0];

                                string brand = attr;
                                if (brand == "0")
                                    brand = ConfigWeb.SiteName;

                                string categoryName = "0";
                                string CategoryNameList = dr["CategoryNameList"].ToString().Trim(',');
                                if (!string.IsNullOrEmpty(CategoryNameList))
                                    categoryName = CategoryNameList;

                                int Price1 = ConvertUtility.ToInt32(dr["Price1"]);
                                if (Price1 > 0 && Price < Price1)
                                {
                                    Price = ConvertUtility.ToInt32(dr["Price1"]);
                                    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                }
                                else
                                {
                                    Price = ConvertUtility.ToInt32(dr["Price"]);
                                    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                }

                                DateTime date = DateTime.Now;
                                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                                var lastDayOfMonth = firstDayOfMonth.AddMonths(2).AddDays(-1);
                                string NgayDau = firstDayOfMonth.ToString("yyyy-MM-dd") + "T08:00+0700";
                                string NgayCuoi = lastDayOfMonth.ToString("yyyy-MM-dd") + "T23:00+0700";
                                string NgayUuDai = NgayDau + "/" + NgayCuoi;

                                writerFBSale.AppendLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}",
                                    dr["ID"].ToString(), //0 id
                                    Utils.FixCapitalization(dr["Name"].ToString()), //1 title
                                    dr["Name"] + " " + ConfigWeb.SiteName, //2 description
                                    "in stock", //3 availability
                                    "new", //4 condition
                                    Price.ToString().Replace(",0000", "") + " VND", //5 price
                                     Price1.ToString().Replace(",0000", "") + " VND", //6 sale price
                                    TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]), //7 link
                                    imgPath, //image_link
                                    brand, //brand
                                    dr["ProductType"].ToString(), //custom_label_0
                                    type, //custom_label_1 
                                    type + "_" + attr, //custom_label_2
                                    categoryName, //custom_label_3
                                    Uu_Tien //custom_label_4
                                    ));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HttpResponse response = context.Response;
                        response.Write(string.Format("Lỗi FB: {0} - {1} \n<br />", dr["ID"], dr["Name"]));
                    }
                }

                string savepath1 = HttpContext.Current.Request.MapPath("~/upload/");

                string filePath = Path.Combine(savepath1, "sale_products.tsv");
                using (StreamWriter writer1 = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer1.Write(writerFBSale.ToString());
                }

            }
        }
    }

    public static string GenerateFriendlyUrlCondition(string inputString)
    {
        if (!string.IsNullOrEmpty(inputString))
        {
            string[] values = inputString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder conditionBuilder = new StringBuilder();
            conditionBuilder.Append("(");

            for (int i = 0; i < values.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(values[i]))
                {
                    conditionBuilder.AppendFormat("ID={0}", values[i].Trim());

                    if (i < values.Length - 1)
                    {
                        conditionBuilder.Append(" OR ");
                    }
                }
            }

            conditionBuilder.Append(")");

            return conditionBuilder.ToString();
        }
        else
            return "";
    }

    public static List<string> GenAttribute(string Value, string CategoryName)
    {
        List<string> Return = new List<string>();
        if (!string.IsNullOrEmpty(Value) || !string.IsNullOrEmpty(CategoryName))
        {
            DataTable dt = SqlHelper.SQLToDataTable("tblAttributes", "Name", string.Format("ID IN ({0}) AND ParentID IN (5225, 28)", Value.Trim(',')));
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    Return.Add(dr["Name"].ToString());
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(CategoryName))
                {
                    Return.Add(Utils.CommaSQLRemove(CategoryName));
                }
            }
        }
        return Return;
    }


    #region SiteMap
    public static void SitemapUpdate()
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        XElement sitemapIndex = new XElement(ns + "sitemapindex",
            new XElement(ns + "sitemap",
                new XElement(ns + "loc", C.ROOT_URL + "/sitemap/danh-muc-san-pham.xml")),
            new XElement(ns + "sitemap",
                new XElement(ns + "loc", C.ROOT_URL + "/sitemap/danh-muc-tin-tuc.xml")),
            new XElement(ns + "sitemap",
                new XElement(ns + "loc", C.ROOT_URL + "/sitemap/noi-dung.xml")),
            new XElement(ns + "sitemap",
                new XElement(ns + "loc", C.ROOT_URL + "/sitemap/san-pham.xml")),
            new XElement(ns + "sitemap",
                new XElement(ns + "loc", C.ROOT_URL + "/sitemap/tin-tuc.xml"))
        );

        XDocument sitemapDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            sitemapIndex
        );

        string fileName = HttpContext.Current.Request.MapPath("/sitemap.xml");
        sitemapDocument.Save(fileName);

        Article_Sitemap();
        Product_Sitemap();
        Article_Category_Sitemap();
        Product_Category_Sitemap();
        Content_Category_Sitemap();
    }

    public static void Article_Sitemap()
    {
        try
        {
            DataTable dataTable = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "FriendlyUrl,EditedDate", string.Format(@"{0} AND StartDate<=getdate() AND {1}", Utils.CreateFilterDate, Utils.CreateFilterHide), "ID DESC");
            if (Utils.CheckExist_DataTable(dataTable))
            {
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XElement urlSet = new XElement(ns + "urlset");

                foreach (DataRow dr in dataTable.Rows)
                {
                    string formattedDate = string.Empty;
                    DateTime editedDate;
                    if (dr["EditedDate"] != DBNull.Value && DateTime.TryParse(dr["EditedDate"].ToString(), out editedDate))
                    {
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }
                    else
                    {
                        editedDate = new DateTime(2010, 1, 1);
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }

                    XElement urlElement = new XElement(ns + "url",
                        new XElement(ns + "loc", TextChanger.GetLinkRewrite_Article(dr["FriendlyUrl"].ToString())),
                        new XElement(ns + "lastmod", formattedDate),
                        new XElement(ns + "changefreq", "daily"),
                        new XElement(ns + "priority", "0.6")
                    );
                    urlSet.Add(urlElement);
                }

                XDocument sitemapDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    urlSet
                );

                sitemapDocument.Save(HttpContext.Current.Request.MapPath("/sitemap/tin-tuc.xml"));
            }
        }
        catch { }
    }

    public static void Product_Sitemap()
    {
        try
        {
            DataTable dataTable = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "FriendlyUrl,EditedDate,FriendlyUrlCategory", Utils.CreateFilterHide, ConfigWeb.SortArticle);
            if (Utils.CheckExist_DataTable(dataTable))
            {
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XElement urlSet = new XElement(ns + "urlset");

                foreach (DataRow dr in dataTable.Rows)
                {

                    string formattedDate = string.Empty;
                    DateTime editedDate;
                    if (dr["EditedDate"] != DBNull.Value && DateTime.TryParse(dr["EditedDate"].ToString(), out editedDate))
                    {
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }
                    else
                    {
                        editedDate = new DateTime(2010, 1, 1);
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }


                    XElement urlElement = new XElement(ns + "url",
                        new XElement(ns + "loc", TextChanger.GetLinkRewrite_Products(dr["FriendlyUrlCategory"].ToString(), dr["FriendlyUrl"].ToString())),
                        new XElement(ns + "lastmod", formattedDate),
                        new XElement(ns + "changefreq", "daily"),
                        new XElement(ns + "priority", "0.5")
                    );
                    urlSet.Add(urlElement);
                }

                XDocument sitemapDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    urlSet
                );

                sitemapDocument.Save(HttpContext.Current.Request.MapPath("/sitemap/san-pham.xml"));
            }
        }
        catch { }
    }


    public static void Article_Category_Sitemap()
    {
        try
        {
            string filter = string.Format("(Hide is null OR Hide=0) AND LinkTypeMenuFlag & {0} <> 0 and PositionMenuFlag & {1} = 0", (int)LinkTypeMenuFlag.Article, (int)PositionMenuFlag.Bottom);
            DataTable dataTable = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,EditedDate", string.Format("{0}", filter), "Sort");
            if (Utils.CheckExist_DataTable(dataTable))
            {
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XElement urlSet = new XElement(ns + "urlset");

                foreach (DataRow dr in dataTable.Rows)
                {
                    string formattedDate = string.Empty;
                    DateTime editedDate;
                    if (dr["EditedDate"] != DBNull.Value && DateTime.TryParse(dr["EditedDate"].ToString(), out editedDate))
                    {
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }
                    else
                    {
                        editedDate = new DateTime(2010, 1, 1);
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }

                    XElement urlElement = new XElement(ns + "url",
                        new XElement(ns + "loc", TextChanger.GetLinkRewrite_CategoryArticle(dr["FriendlyUrl"].ToString())),
                        new XElement(ns + "lastmod", formattedDate),
                        new XElement(ns + "changefreq", "daily"),
                        new XElement(ns + "priority", "0.8")
                    );
                    urlSet.Add(urlElement);
                }

                XDocument sitemapDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    urlSet
                );

                sitemapDocument.Save(HttpContext.Current.Request.MapPath("/sitemap/danh-muc-tin-tuc.xml"));
            }
        }
        catch { }
    }


    public static void Product_Category_Sitemap()
    {
        try
        {
            string filter = string.Format("{0} AND {1}", Utils.CreateFilterHide, Utils.CreateFilterFlags(PositionMenuFlag.Main, "PositionMenuFlag"));
            DataTable dataTable = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,EditedDate", string.Format("{0}", filter), "Sort");
            if (Utils.CheckExist_DataTable(dataTable))
            {
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XElement urlSet = new XElement(ns + "urlset");

                foreach (DataRow dr in dataTable.Rows)
                {
                    string formattedDate = string.Empty;
                    DateTime editedDate;
                    if (dr["EditedDate"] != DBNull.Value && DateTime.TryParse(dr["EditedDate"].ToString(), out editedDate))
                    {
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }
                    else
                    {
                        editedDate = new DateTime(2010, 1, 1);
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }

                    XElement urlElement = new XElement(ns + "url",
                        new XElement(ns + "loc", TextChanger.GetLinkRewrite_Category(dr["FriendlyUrl"].ToString())),
                        new XElement(ns + "lastmod", formattedDate),
                        new XElement(ns + "changefreq", "daily"),
                        new XElement(ns + "priority", "0.9")
                    );
                    urlSet.Add(urlElement);
                }

                XDocument sitemapDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    urlSet
                );

                sitemapDocument.Save(HttpContext.Current.Request.MapPath("/sitemap/danh-muc-san-pham.xml"));
            }
        }
        catch { }
    }

    public static void Content_Category_Sitemap()
    {
        try
        {
            string filter = string.Format("{0} AND {1}", Utils.CreateFilterHide, Utils.CreateFilterFlags(LinkTypeMenuFlag.Content, "LinkTypeMenuFlag"));
            DataTable dataTable = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "FriendlyUrl,EditedDate", string.Format("{0}", filter), "Sort");
            if (Utils.CheckExist_DataTable(dataTable))
            {
                XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                XElement urlSet = new XElement(ns + "urlset");

                foreach (DataRow dr in dataTable.Rows)
                {
                    string formattedDate = string.Empty;
                    DateTime editedDate;
                    if (dr["EditedDate"] != DBNull.Value && DateTime.TryParse(dr["EditedDate"].ToString(), out editedDate))
                    {
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }
                    else
                    {
                        editedDate = new DateTime(2010, 1, 1);
                        formattedDate = editedDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
                    }

                    XElement urlElement = new XElement(ns + "url",
                        new XElement(ns + "loc", TextChanger.GetLinkRewrite_Menu(dr["FriendlyUrl"].ToString())),
                        new XElement(ns + "lastmod", formattedDate),
                        new XElement(ns + "changefreq", "daily"),
                        new XElement(ns + "priority", "0.5")
                    );
                    urlSet.Add(urlElement);
                }

                XDocument sitemapDocument = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    urlSet
                );

                sitemapDocument.Save(HttpContext.Current.Request.MapPath("/sitemap/noi-dung.xml"));
            }
        }
        catch { }
    }
    #endregion

}

public class ProductFeed
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public string ImageLink { get; set; }
    public string Price { get; set; }
    public string Availability { get; set; }
    public string Condition { get; set; }
    public string Brand { get; set; }
    public string Gtin { get; set; }
    public string Mpn { get; set; }
    public string ProductType { get; set; }
    public string CustomLabel0 { get; set; }
    public string CustomLabel1 { get; set; }
    public string CustomLabel2 { get; set; }
    public string CustomLabel3 { get; set; }
}