using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;


/// <summary>
/// Summary description for TextChanger
/// </summary>
public class TextChanger
{
    public TextChanger()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected static string GetRootUrl()
    {
        return C.ROOT_URL;
    }


    public static string Rewrite(string control, string friendlyUrl)
    {
        return Rewrite(control, friendlyUrl, string.Empty);
    }
    public static string Rewrite(string control, string friendlyUrl, string categoryUrl)
    {
        string retUrl = string.Empty;
        if (control == "article")
            GetLinkRewrite_Article(categoryUrl, friendlyUrl);
        if (control == "product")
            GetLinkRewrite_Products(categoryUrl, friendlyUrl);

        return retUrl;
    }


    public static string GetLinkRewrite_Article(string FriendlyUrl)
    {
        string retUrl = string.Format("{0}/tin/{1}" + C.SEO_EXTENSION, GetRootUrl(), FriendlyUrl.ToLower());
        return retUrl;
    }

    public static string GetLinkRewrite_Article(string catrul, string FriendlyUrl)
    {
        string retUrl = string.Format("{0}tin/{1}/{2}" + C.SEO_EXTENSION, C.ROOT_URL + C.DS, catrul.ToLower(), FriendlyUrl.ToLower());
        return retUrl;
    }
    
    public static string GetLinkRewrite_CategoryArticle(string FriendlyUrl)
    {
        string retUrl = string.Format("{0}tin/{1}/", C.ROOT_URL + C.DS, FriendlyUrl.ToLower());
        return retUrl;
    }

    public static string GetLinkRewrite_TagCloud(string FriendlyUrl)
    {
        string retUrl = string.Format("{0}tag/{1}.html", C.ROOT_URL + C.DS, FriendlyUrl.ToLower());
        return retUrl;
    }

    public static string GetLinkRewrite_CategoriesAttribute(string Cat, string FriendlyUrl)
    {
        string retUrl = string.Format("{0}{1}/{2}.html", C.ROOT_URL + C.DS, Cat, FriendlyUrl.ToLower());
        return retUrl;
    }

    public static string GetLinkRewrite_Products(object category, object FriendlyUrl)
    {
        if (string.IsNullOrEmpty(ConvertUtility.ToString(category)))
            category = "san-pham";
        string retUrl = string.Format("{0}{1}/{2}" + C.SEO_EXTENSION, C.ROOT_URL + C.DS, ConvertUtility.ToString(category).ToLower(), ConvertUtility.ToString(FriendlyUrl).ToLower());
        return retUrl;
    }

    public static string GetLinkRewrite_ChuDe(string FriendlyUrl)
    {
        string retUrl = string.Format("{0}chu-de/{1}" + C.SEO_EXTENSION, C.ROOT_URL + C.DS, FriendlyUrl.ToLower());
        return retUrl;
    }


    public static string GetLinkRewrite_Category(string FriendlyUrl)
    {
        string retUrl = string.Format("{0}{1}/", C.ROOT_URL + C.DS, FriendlyUrl.ToLower());
        return retUrl;
    }

    public static string GetLinkRewrite_Tags(string TagName)
    {
        string retUrl = string.Format("{0}tim-kiem.html?q={1}", C.ROOT_URL + C.DS, TagName);
        return retUrl;
    }

    public static string LoadImage_Category( string image)
    {
        string pathImage = "";
        if (!String.IsNullOrEmpty(image))
        {
            pathImage = StringUtil.Combine(C.ROOT_URL, image);
        }
        else
        {
            pathImage = StringUtil.Combine(C.TEMPLATE_PATH_FULL + "images/no-image-300x300.jpg");
        }
        return pathImage;
    }


    public static string GetLinkRewrite_Menu(string FriendlyUrl)
    {
        string retUrl = string.Format("{0}{1}", C.ROOT_URL + C.DS, FriendlyUrl + C.SEO_EXTENSION);
        return retUrl;
    }

    public static string GetLinkRewrite_Menu(int CategoryID, string Alias)
    {
        string RetModelName = TextChanger.Translate(Alias.ToLower(), "-");
        string retUrl = string.Format("{0}/noi-dung/{1}" + C.SEO_EXTENSION, GetRootUrl(), RetModelName);
        return retUrl;
    }



    public static string GetLinkRewrite_VideoDetails(int VideoID, string Alias)
    {
        string RetModelName = TextChanger.Translate(Alias, "-");
        string retUrl = string.Format("{0}video/{1}/{2}" + C.SEO_EXTENSION, GetRootUrl(), VideoID, RetModelName);
        return retUrl;
    }

    public static string GetLinkRewrite_Manufacturer(int StyleID, string Name)
    {
        string RetModelName = TextChanger.Translate(Name, "-");
        string retUrl = string.Format("{0}nsx/{1}/{2}"+C.SEO_EXTENSION, GetRootUrl(), StyleID, RetModelName);
        return retUrl;
    }




    public static string GetLinkRewrite_DonHangView()
    {
        string retUrl = string.Format("{0}{1}/", C.ROOT_URL + C.DS, "order/view");
        return retUrl;
    }


    static string[] aUniks = new string[] {"aáàảãạâấầẩẫậăắằẳẵặ","dđ","eéèẻẽẹêếềểễệ","iíìỉĩị","oóòỏõọôốồổỗộơớờởỡợ","yýỳỷỹỵ","uúùủũụưứừửữự",
				   "AÁẢÃẠÂẤẨẪẬĂẮẰẲẴẶ","DĐ","EÉÈẺẼẸÊẾỀỂỄỆ","IÍÌỈĨỊ","OÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢ","YÝỲỶỸỴ","UÚÙỦŨỤƯỨỪỬỮỰ",
				   @"_ -&?~!@#$%^*():'""/\+=.,;<>[]{}"};

    public static string Translate(string str, string hyphen = " ") //Kỷ niệm câu hỏi đầu tiên ChatGPT 3/2023
    {
        str = str.Replace("Đ", "D");
        str = str.Replace("đ", "d");

        // Remove Vietnamese signs
        string unsignedStr = string.Concat(str.Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            .Trim();

        // Replace non-letter, non-digit characters with hyphen
        unsignedStr = Regex.Replace(unsignedStr, @"[^a-zA-Z0-9\s]", hyphen, RegexOptions.Compiled);

        // Remove any leading, trailing, or repeated hyphens
        string urlFriendlyStr = Regex.Replace(unsignedStr, @"[\s" + hyphen + "]+", hyphen).Trim(hyphen.ToCharArray());

        return urlFriendlyStr;
    }


    public static string NiceCut(string input, int length)
    {
        const string endS = " .,;!";
        if (string.IsNullOrEmpty(input) || input.Length <= length)
            return input;
        else
        {
            string s = input.Substring(0, length);
            if (endS.Contains(input[length].ToString()))
                return s.Trim();
            else
            {
                int i = length - 1;
                while (!endS.Contains(s[i].ToString()))
                {
                    i--;
                }
                return s.Remove(i);

            }
        }
    }

}

