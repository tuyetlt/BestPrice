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




<script>
    var product = jQuery('#productDetail').length == 1;
    var cart = window.location.href.indexOf('/gio-hang') != -1;
    var purchased = window.location.href.indexOf('/thong-tin-don-hang') != -1;
    var dr_items = [];
    var dr_value = '';

    if (product) {
        var itemId = jQuery('[name*="hdfProductID"]').attr('value').trim();

        item = { "id": itemId, "google_business_vertical": 'retail' };
        dr_items.push(item);

        dr_value = parseFloat(jQuery('#GG_Price').val());

        dataLayer.push({
            'dr_event_type': 'view_item',
            'dr_items': dr_items,
            'dr_value': dr_value,
            'event': 'dynamic_remarketing'
        });

        fbq('track', 'ViewContent', {
            content_ids: [itemId.toString()],
            content_type: 'product',
            value: dr_value,
            currency: 'VND'
        });


    } else if (cart) {
        var itemIds = [];
        jQuery('.del_cart').each(function () {
            var id = jQuery(this).attr('data-id').trim();
            itemIds.push(id);
        })
        localStorage.setItem("idtemIds_Cart", JSON.stringify(itemIds));
        dr_value = parseFloat(jQuery('.cart_total_price').text().replace(/[VNĐ.]/g, "").trim());

        itemIds.forEach(function (itemId) {
            item = { "id": itemId, "google_business_vertical": 'retail' };
            dr_items.push(item);
        });

        dataLayer.push({
            'dr_event_type': 'add_to_cart',
            'dr_items': dr_items,
            'dr_value': dr_value,
            'event': 'dynamic_remarketing'
        });
    }
    else if (purchased) {
        var itemIds = JSON.parse(localStorage.getItem("idtemIds_Cart"));
        itemIds.forEach(function (itemId) {
            item = { "id": itemId, "google_business_vertical": 'retail' };
            dr_items.push(item);
        });
        dr_value = parseFloat(jQuery('table tbody tr td p:nth-child(12) span').text().replace(/[VNĐ.]/g, "").trim());

        dataLayer.push({
            'dr_event_type': 'purchase',
            'dr_items': dr_items,
            'dr_value': dr_value,
            'event': 'dynamic_remarketing'
        });

        fbq('track', 'Purchase', { currency: "VND", value: dr_value });
    }

    $("#button-cart").click(function () {
        fbq('track', 'AddToCart', {
            content_ids: [itemId.toString()],
            content_type: 'product',
            value: dr_value,
            currency: 'VND'
        });
    });
</script>