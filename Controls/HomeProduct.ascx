<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeProduct.ascx.cs" Inherits="Controls_HomeProduct" %>
<%@ Import Namespace="System.Data" %>

<% for (int i = 1; i <= 5; i++)
    {
        DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome1, TextHome2, TextHome3, TextHome4, TextHome5", "");
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string Modul = "Home";
            if (i == 1)
                Modul = "Home1";

            AttrProductFlag result;
            int intValueFlag = 0;
            if (Enum.TryParse("TextHome" + i, out result))
                intValueFlag = (int)result;



            string ReadMore = "";

            if (!string.IsNullOrEmpty(dr[string.Format("TextHome{0}", i)].ToString()))
            {
                string filter = string.Format("AttrProductFlag & {0} <> 0", intValueFlag);
                Response.Write(Utils.LoadUserControl("~/Controls/UCHomeProduct.ascx", dr[string.Format("TextHome{0}", i)].ToString(), ReadMore, filter, 0, true, Modul, i));
            }
        }
    } %>