using System;
using System.Data;
public class Category
{
    public Category()
    {
    }


    public static DataTable GetByFriendlyUrl(string FriendlyUrl)
    {
        return SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("FriendlyUrl = N'{0}'", FriendlyUrl.Trim()));
    }

    public static DataTable GetByFriendlyUrl(string field, string FriendlyUrl)
    {
        DataTable dtReturn = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE,field, string.Format("FriendlyUrl = N'{0}'", FriendlyUrl.Trim()));
        return dtReturn;
    }
    public static bool GetByFriendlyUrl_CheckExist(string FriendlyUrl)
    {
        DataTable dtReturn = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID", string.Format("FriendlyUrl = N'{0}'", FriendlyUrl.Trim()));
        if (dtReturn != null && dtReturn.Rows.Count > 0)
            return true;
        else
            return false;
    }
}