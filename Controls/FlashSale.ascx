<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlashSale.ascx.cs" Inherits="Controls_FlashSale" %>
<%@ Import Namespace="System.Data" %>
<div class="main main__wrapper" style="background-color:#00beb4">
    <div class="container">
       <%-- <div class="heading d-flex align-items-end">
            <div class="col flash-sale-Breadcrumb">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>--%>
        <div class="column__main col-12">
            <% for (int i = 1; i <= 1; i++)
               {
                    DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome1", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        string Modul = "FlashSale";

                        string ReadMore = "";

                        if (!string.IsNullOrEmpty(dr[string.Format("TextHome{0}", i)].ToString()))
                        {
                            string filter = "";
                            Response.Write(Utils.LoadUserControl("~/Controls/UCHomeProduct.ascx", dr[string.Format("TextHome{0}", i)].ToString(), ReadMore, filter, 0, true, Modul, i));
                        }
                    }
               } %>
        </div>
    </div>
</div>
