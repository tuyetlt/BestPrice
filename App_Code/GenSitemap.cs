using System;
using System.Data;
using System.Web;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for GenSitemap
/// </summary>
public class GenSitemap
{
    public static void SitemapUpdate()
    {
        try
        {
            string fileName = HttpContext.Current.Request.MapPath("/sitemap.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc = GetSitemapDocument();
            xmlDoc.Save(fileName);
        }
        catch { }
    }


    #region Build sitemap document methods
    private static XmlDocument GetSitemapDocument()
    {
        XmlDocument sitemapDocument = new XmlDocument();
        XmlDeclaration xmlDeclaration = sitemapDocument.CreateXmlDeclaration("1.0", "UTF-8", string.Empty);
        sitemapDocument.AppendChild(xmlDeclaration);

        XmlElement urlset = sitemapDocument.CreateElement("urlset");
        urlset.SetAttribute("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        urlset.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        urlset.SetAttribute("schemaLocation", "http://www.w3.org/2001/XMLSchema-instance", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

        sitemapDocument.AppendChild(urlset);

        List<SitemapPage> urls = GetSitemapPages();

        foreach (SitemapPage sitemapPage in urls)
        {
            XmlElement url = CreateUrlElement(sitemapDocument, sitemapPage);
            urlset.AppendChild(url);
        }
        return sitemapDocument;
    }

    private static XmlElement CreateUrlElement(XmlDocument sitemapDocument, SitemapPage sitemapPage)
    {
        XmlElement url = sitemapDocument.CreateElement("url");
        XmlElement loc = CreateElementWithText(sitemapDocument, "loc", sitemapPage.Location);
        url.AppendChild(loc);
        string lastModValue = sitemapPage.LastModificationDate.ToString("yyyy-MM-ddTHH:mm:ss+07:00");
        XmlElement lastmod = CreateElementWithText(sitemapDocument, "lastmod", lastModValue);
        url.AppendChild(lastmod);
        if (!string.IsNullOrEmpty(sitemapPage.ChangeFrequency))
        {
            XmlElement changefreq = CreateElementWithText(sitemapDocument, "changefreq", sitemapPage.ChangeFrequency);
            url.AppendChild(changefreq);
        }
        XmlElement priority = CreateElementWithText(sitemapDocument, "priority", sitemapPage.Priority);
        url.AppendChild(priority);
        return url;
    }

    private static XmlElement CreateElementWithText(XmlDocument document, string elementName, string text)
    {
        XmlElement element = document.CreateElement(elementName);
        XmlText elementValue = document.CreateTextNode(text);
        element.AppendChild(elementValue);
        return element;
    }
    #endregion
    private static List<SitemapPage> GetSitemapPages()
    {
        string SiteUrl = C.MAIN_URL + C.DS;

        List<SitemapPage> sitemapPages = new List<SitemapPage>();

        sitemapPages.Add(new SitemapPage(SiteUrl, DateTime.Now, "always", "1.0"));
        sitemapPages.Add(new SitemapPage(SiteUrl, new DateTime(2017, 4, 15), "yearly", "0.4"));
        sitemapPages.Add(new SitemapPage(SiteUrl, DateTime.Now, "monthly", "1.0"));

        int count = 3;



        DataTable dtArticle = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "", Utils.CreateFilterHide + " AND StartDate<=getdate()", "ID DESC");
        foreach (DataRow drNews in dtArticle.Rows)
        {
            sitemapPages.Add(new SitemapPage(TextChanger.GetLinkRewrite_Article(drNews["FriendlyUrl"].ToString()), ConvertUtility.ToDateTime(drNews["CreatedDate"]), "monthly", "0.5"));
            count++;
        }


        DataTable dt = SqlHelper.SQLToDataTable("tblProducts", "", Utils.CreateFilterHide, "ID DESC");
        if (Utils.CheckExist_DataTable(dt))
        {
            foreach (DataRow drProduct in dt.Rows)
            {
                sitemapPages.Add(new SitemapPage(TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString()), ConvertUtility.ToDateTime(drProduct["CreatedDate"]), "monthly", "0.5"));
                count++;
            }
        }



        DataTable dtCategory = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", "Moduls='category' and (Hide=null or hide=0)", "ID DESC");
        foreach (DataRow drCategory in dtCategory.Rows)
        {
            sitemapPages.Add(new SitemapPage(TextChanger.GetLinkRewrite_Category(drCategory["FriendlyUrl"].ToString()), ConvertUtility.ToDateTime(drCategory["EditedDate"]), "monthly", "0.5"));
            count++;
        }

        //ltr.Text = "Lập được: " + count + " chỉ mục  -  <a href=\"" + Globals.BaseUrl + "sitemap.xml\"> Xem Sitemap </a>";
        return sitemapPages;
    }



    public static void GenGoogleShopping(HttpContext context, string web)
    {
        string Uu_Tien = "NO";
        string filterCat = "1=1";

        string filter = string.Format("(Hide is null OR Hide=0) AND Len(FriendlyUrlCategory)>0 AND Price>0 AND " + filterCat); //(ID<= 14153 AND ID>=14180) AND 

        DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,Price,Price1,Gallery, Hide,FriendlyUrlCategory,Brand,ProductType,AttributesIDList,CategoryNameList,CategoryIDList,CategoryIDParentList", filter, "ID DESC", 1, 5000);
        if (Utils.CheckExist_DataTable(dt))
        {
            if (web == "hoanghai")
            {
                string link = "/upload/gg-shopping-list.txt";
                string savepath = HttpContext.Current.Request.MapPath("~" + link);
                TextWriter writer = new StreamWriter(savepath);
                writer.WriteLine("id\ttiêu đề\tmô tả\tliên kết\ttình trạng\tgiá\tcòn hàng\tliên kết hình ảnh\tnhãn hiệu\tloại sản phẩm\tcustom_label_0\tcustom_label_1\tcustom_label_2\tcustom_label_3", System.Text.Encoding.Unicode);

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

                                //int Price1 = ConvertUtility.ToInt32(dr["Price1"]);
                                //if (Price1 > 0 && Price < Price1)
                                //{
                                //    Price = ConvertUtility.ToInt32(dr["Price1"]);
                                //    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                //}
                                //else
                                //{
                                //    Price = ConvertUtility.ToInt32(dr["Price"]);
                                //    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                //}

                                DateTime date = DateTime.Now;
                                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                                var lastDayOfMonth = firstDayOfMonth.AddMonths(2).AddDays(-1);
                                string NgayDau = firstDayOfMonth.ToString("yyyy-MM-dd") + "T08:00+0700";
                                string NgayCuoi = lastDayOfMonth.ToString("yyyy-MM-dd") + "T23:00+0700";
                                string NgayUuDai = NgayDau + "/" + NgayCuoi;

                                writer.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}",
                                    dr["ID"].ToString(),
                                    Utils.FixCapitalization(dr["Name"].ToString()),
                                    dr["Name"] + ", " + ConfigWeb.SiteName,
                                    TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]),
                                    "mới",
                                    Price.ToString().Replace(",0000", "") + " VND",
                                    "còn hàng",
                                    imgPath,
                                    brand, dr["ProductType"].ToString(), type, type + "_" + attr, categoryName, Uu_Tien),
                                    System.Text.Encoding.Unicode);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HttpResponse response = context.Response;
                        response.Write(string.Format("Lỗi GG: {0} - {1} \n<br />", dr["ID"], dr["Name"]));
                    }
                }
                writer.Close();
            }

            if (web == "duclong")
            {
                string link1 = "/upload/gg-shopping-list-duc-long.txt";
                string savepath_dl = HttpContext.Current.Request.MapPath("~" + link1);
                TextWriter writer_dl = new StreamWriter(savepath_dl);
                writer_dl.WriteLine("id\ttiêu đề\tmô tả\tliên kết\ttình trạng\tgiá\tcòn hàng\tliên kết hình ảnh\tnhãn hiệu\tloại sản phẩm\tcustom_label_0\tcustom_label_1\tcustom_label_2\tcustom_label_3", System.Text.Encoding.Unicode);

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

                                //int Price1 = ConvertUtility.ToInt32(dr["Price1"]);
                                //if (Price1 > 0 && Price < Price1)
                                //{
                                //    Price = ConvertUtility.ToInt32(dr["Price1"]);
                                //    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                //}
                                //else
                                //{
                                //    Price = ConvertUtility.ToInt32(dr["Price"]);
                                //    Price1 = ConvertUtility.ToInt32(dr["Price"]);
                                //}

                                DateTime date = DateTime.Now;
                                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                                var lastDayOfMonth = firstDayOfMonth.AddMonths(2).AddDays(-1);
                                string NgayDau = firstDayOfMonth.ToString("yyyy-MM-dd") + "T08:00+0700";
                                string NgayCuoi = lastDayOfMonth.ToString("yyyy-MM-dd") + "T23:00+0700";
                                string NgayUuDai = NgayDau + "/" + NgayCuoi;


                                writer_dl.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}",
                                    dr["ID"].ToString(),
                                    Utils.FixCapitalization(dr["Name"].ToString()),
                                    dr["Name"] + ", " + "Đức Long",
                                    TextChanger.GetLinkRewrite_Products(dtCat.Rows[0]["FriendlyUrl"].ToString(), dr["FriendlyUrl"]).Replace("dienmayhoanghai.vn", "dienmayduclong.vn"),
                                    "mới",
                                    Price.ToString().Replace(",0000", "") + " VND",
                                    "còn hàng",
                                    imgPath.Replace("dienmayhoanghai.vn", "img.dienmayduclong.vn"),
                                    brand, dr["ProductType"].ToString(), type, type + "_" + attr, categoryName, Uu_Tien),
                                    System.Text.Encoding.Unicode);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        HttpResponse response = context.Response;
                        response.Write(string.Format("Lỗi GG: {0} - {1} \n<br />", dr["ID"], dr["Name"]));
                    }
                }
                writer_dl.Close();
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
}

public class SitemapPage
{
    public SitemapPage(string location, DateTime lastModificationDate, string changeFrequency, string priority)
    {
        Location = location;
        LastModificationDate = lastModificationDate;
        ChangeFrequency = changeFrequency;
        Priority = priority;
    }

    public string Location;
    public DateTime LastModificationDate;
    public string ChangeFrequency;
    public string Priority;
}