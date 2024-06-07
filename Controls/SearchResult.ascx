<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchResult.ascx.cs" Inherits="Controls_SearchResult" %>
<%@ Import Namespace="System.Data" %>

<input type="hidden" id="keyword" value="<%= keyword %>" />
<input type="hidden" id="pageIndex" value="1" />
<input type="hidden" id="pageSize" value="<%= C.ROWS_PRODUCTCATEGORY %>" />
<input type="hidden" id="totalProduct" value="<%= _totalProduct %>" />
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>
        <div class="column__main col-12 col-md-9">
            <div class="product__wrapper">
                <div class="product__grid row mb-5 product-list" id="products-container">
                    <% 
                        if (Utils.CheckExist_DataTable(dtProduct))
                        {
                            foreach (DataRow drProduct in dtProduct.Rows)
                            {
                                string linkDetail = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(drProduct["FriendlyUrlCategory"]), ConvertUtility.ToString(drProduct["FriendlyUrl"]));

                    %>
                    <div class="product__item col-6 col-sm-4 col-md-4 col-lg-3">
                        <div class="product__inner">
                            <div class="product__thumb">
                               <%= SqlHelper.GetPercentLabel(drProduct) %>
                                <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                    <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 300, 300) %>" alt="<%= drProduct["Name"].ToString() %>" width="350" height="400" />
                                   <%= SqlHelper.GenFlashSaleFrame(drProduct) %>
                                </a>
                            </div>
                            <div class="product__info">
                                <h3 class="product__name"><a href="<%= linkDetail %>"><%= drProduct["Name"].ToString() %></a></h3>
                                <div class="product__price d-flex align-items-center">
                                    <div class="price"><%= SqlHelper.GetPrice(PriceReturn.Price, drProduct) %></div>
                                    <div class="old-price"><%= SqlHelper.GetPrice(PriceReturn.OriginalPrice, drProduct) %></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%}
                        } %>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
</div>




