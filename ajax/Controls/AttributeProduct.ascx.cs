using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_Controls_AttributeProduct : System.Web.UI.UserControl
{
    string[] filterArray;
    string filter = string.Empty;
    string AttributesIDList = string.Empty;
    string jsonString = string.Empty;
    int RootID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string categoryIDList = RequestHelper.GetString("categoryList", "");
        string attrIDList = RequestHelper.GetString("attrIDList", "");
        string bindTo = RequestHelper.GetString("bindto", "");
        if (bindTo == "category")
        {
            AttributesIDList = attrIDList.Trim(',');
            //filter = "ParentID=0";
            BindData();
        }
        else
        {
            string[] CategoryArr = categoryIDList.Trim(',').Split(',');
            if (CategoryArr != null && CategoryArr.Length > 0)
            {
                foreach (string CategoryID in CategoryArr)
                {
                    DataRow dr;
                    DataTable dt;

                    dt = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, FilterJson", string.Format("(Hide is null OR Hide=0) AND ID='{0}'", CategoryID),"Sort");
                    if (Utils.CheckExist_DataTable(dt))
                    {
                        dr = dt.Rows[0];
                        RootID = ConvertUtility.ToInt32(dr["ID"]);
                        DataRow drRoot;
                        drRoot = dr;
                        jsonString = dr["FilterJson"].ToString();
                        do
                        {
                            if (ConvertUtility.ToInt32(dr["ParentID"]) > 0)
                            {
                                 DataTable dtRoot = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, FilterJson", string.Format("ID={0}", drRoot["ParentID"]));
                                if (Utils.CheckExist_DataTable(dtRoot))
                                {
                                    drRoot = dtRoot.Rows[0];
                                    RootID = ConvertUtility.ToInt32(drRoot["ID"]);

                                    if (drRoot["FilterJson"].ToString().Length>10)
                                    {
                                        jsonString = drRoot["FilterJson"].ToString();
                                    }
                                }
                            }
                        }
                        while (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0 && jsonString.Length>10);
                    }
                }
            }  
        }

        try
        {

            // ADD thêm ID Root
            List<AttributeProduct> attrList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AttributeProduct>>(jsonString);

            if (attrList != null && attrList.Count > 0)
            {
                AttributeProduct attrRoot = new AttributeProduct();
                attrRoot.ID = ConvertUtility.ToString(RootID);
                attrRoot.Name = "RootID";
                attrList.Add(attrRoot);
            }

            jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(attrList, Newtonsoft.Json.Formatting.Indented);
            Response.Write(jsonString);
        }
        catch { }

       
    }

    protected void BindData()
    {
        //Response.Write(filter);
        filterArray = AttributesIDList.Trim(',').Split(',');
        if (filterArray != null && filterArray.Length > 0 && !string.IsNullOrEmpty(AttributesIDList))
        {
            int count = 0;
            foreach (string a in filterArray)
            {
                if (count > 0)
                    filter += " OR ";
                else
                    filter = "ParentID=0 AND (";
                filter += string.Format("ID={0}", a);
                if (count + 1 == filterArray.Length)
                    filter += ")";
                count++;
            }
            List<AttributeProduct> attrProductList = new List<AttributeProduct>();

            DataTable dtAttr = SqlHelper.SQLToDataTable("tblAttributes", "ID, Name, NameDisplay, Description, Image_1, ParentID,FriendlyUrl", filter, "Sort");
            if (Utils.CheckExist_DataTable(dtAttr))
            {
                foreach (DataRow drAttr in dtAttr.Rows)
                {
                    AttributeProduct attrProduct = new AttributeProduct();
                    attrProduct.ID = ConvertUtility.ToString(drAttr["ID"]);
                    attrProduct.Name = ConvertUtility.ToString(drAttr["Name"]);
                    attrProduct.Image = ConvertUtility.ToString(drAttr["Image_1"]);
                    attrProduct.FriendlyUrl = ConvertUtility.ToString(drAttr["FriendlyUrl"]);

                    List<AttributeProductChild> attrProductListChild = new List<AttributeProductChild>();
                    DataTable dtChild = SqlHelper.SQLToDataTable("tblAttributes", "ID, Name, NameDisplay, Description, Image_1,FriendlyUrl", "ParentID=" + drAttr["ID"], "Sort");
                    if (Utils.CheckExist_DataTable(dtChild))
                    {
                        foreach (DataRow drChild in dtChild.Rows)
                        {
                            AttributeProductChild attrProductChild = new AttributeProductChild();
                            attrProductChild.ID = ConvertUtility.ToString(drChild["ID"]);
                            attrProductChild.Name = ConvertUtility.ToString(drChild["Name"]);
                            attrProductChild.Image = ConvertUtility.ToString(drChild["Image_1"]);
                            attrProductChild.FriendlyUrl = ConvertUtility.ToString(drChild["FriendlyUrl"]);
                            attrProductChild.FriendlyUrlParent = ConvertUtility.ToString(drAttr["FriendlyUrl"]);
                            attrProductListChild.Add(attrProductChild);
                        }
                    }

                    attrProduct.attributeProductChild = attrProductListChild;
                    attrProductList.Add(attrProduct);
                }


                jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(attrProductList, Newtonsoft.Json.Formatting.Indented);
            }
        }
    }
}