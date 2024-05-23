<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlashSale.ascx.cs" Inherits="Controls_FlashSale" %>
<%@ Import Namespace="System.Data" %>
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>
        <div class="column__main col-12">
            <% for (int i = 1; i <= 1; i++)
               {
                    DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome1", "");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        bool HightLight = false;
                        if (i == 1)
                            HightLight = true;

                        AttrProductFlag result;
                        int intValueFlag = 0;
                        if (Enum.TryParse("TextHome" + i, out result))
                            intValueFlag = (int)result;



                        string ReadMore = "";

                        if (!string.IsNullOrEmpty(dr[string.Format("TextHome{0}", i)].ToString()))
                        {
                            string filter = string.Format("AttrProductFlag & {0} <> 0", intValueFlag);
                            Response.Write(Utils.LoadUserControl("~/Controls/UCHomeProductFlash.ascx", dr[string.Format("TextHome{0}", i)].ToString(), ReadMore, filter, 0, true, HightLight, i));
                        }
                    }
               } %>
        </div>
    </div>
</div>
