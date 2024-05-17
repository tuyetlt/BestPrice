<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderInfo.ascx.cs" Inherits="Controls_OrderInfo" %>
<%@ Import Namespace="System.Data" %>
<input type="hidden" id="countProduct" value="<%= countProduct %>" />
<input type="hidden" id="totalValue" value="<%= totalValue %>" />

<%
    if (Utils.CheckExist_DataTable(dt))
    {
%>
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>
        <%= content %>
    </div>
</div>




<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        SaveUTMData('ThankForOrder', "<%= string.Format("{0:N0} VNĐ", dr["PriceFinal"]) %>");
    });
</script>

<%
    int count = 0;
    string Items = "[";
    if (orderInfoList.Count > 0)
    {
        foreach (OrderInfo orderInfo in orderInfoList)
        {
            count++;
            string dau_phay = ",";
            if (count == orderInfoList.Count)
                dau_phay = string.Empty;

            Items += string.Format(@"{{""id"":""{0}"",""productName"":""{1}"",""quantity"":""{2}""}}{3}", orderInfo.ProductID, orderInfo.Name, orderInfo.Quantity, dau_phay);
        }
    }
    Items += "]";

%>
<input type="hidden" value="purchase" id="GG_Page" />
<input type="hidden" value="<%= token %>" id="GG_ID" />
<input type="hidden" value="<%= Utils.PriceConversion_Product(totalValue) %>" id="GG_Price" />
<input type="hidden" value='<%= Items %>' id="GG_Items" />
<input type="hidden" value="<%= countProduct %>" id="GG_CountItems" />

<% }
    else
    {%>
<div class="cart-empty">
    <div class="container">
        <h2>Đơn hàng không tồn tại!</h2>
        <img src="/themes/assets/images/cart-empty.png" />
    </div>
</div>
<% } %>

