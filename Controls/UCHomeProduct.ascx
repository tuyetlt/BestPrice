<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCHomeProduct.ascx.cs" Inherits="Controls_UCHomeProduct" %>
<%@ Import Namespace="System.Data" %>

<%if (CategoryID > 0) //Nếu là danh mục
    {  %>
<% 
    string filterP = string.Format("AttrProductFlag & {0} <> 0", (int)AttrProductFlag.Home);
    DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,Price2,AttrProductFlag", string.Format("(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%') AND ({1})", CategoryID, filterP), ConfigWeb.SortProductHome, 1, 16);
    if (Utils.CheckExist_DataTable(dtProduct))
    {
%>
<%
    for (int i = 0; i < dtProduct.Rows.Count; i++)
    {
        DataRow drProduct = dtProduct.Rows[i];
        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
%>
<div class="product__item">
    <div class="product__inner">
        <div class="product__thumb">
            <%= SqlHelper.GetPercentLabel(drProduct) %>
            <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                <%= SqlHelper.GenFlashSaleFrame(drProduct) %>
            </a>
        </div>
        <div class="product__info">
            <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
            <div class="product__price d-flex mb-d-flex align-items-center justify-content-center">
                <div class="price"><%= SqlHelper.GetPrice(PriceReturn.Price, drProduct) %></div>
                <div class="old-price"><%= SqlHelper.GetPrice(PriceReturn.OriginalPrice, drProduct) %></div>
            </div>
        </div>
    </div>
</div>
<% } %><% } %>
<%
    }
    else
    {  %>

<%if (Modul == "Home1")
    {  %>
<section class="product product__special-topPage moduleProductSlideshow-specials mb-5">
    <div class="container">
        <div class="product__special-topPage__inner">
            <div class="heading heading-read-more">
                <div id="countdown">
                    <div id='tiles'><%= ConfigWeb.FlashSaleTimeDisplay %></div>
                </div>
                <h2 class="heading__title justify-content-center text-center text-white"><a href="/flash-sale/"><span class="text-title"><%= Title %></span></a></h2>
                <a href="/flash-sale/" target="_self" class="btn-more">Xem thêm</a>
            </div>
            <div class="product__special-topPage__content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_specials slider" module="specials">
                                <% 
                                    string FilterFlashSale = string.Format("(Hide is null OR Hide=0) AND AttrProductFlag & {0} <> 0 AND Price2>0", (int)AttrProductFlag.FlashSale);
                                    DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,Price2,AttrProductFlag,HashTagUrlList", FilterFlashSale, "Sort DESC", 1, 20);
                                    if (Utils.CheckExist_DataTable(dt))
                                    {
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            DataRow drProduct = dt.Rows[i];
                                            string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                                %>
                                <div class="product__item">
                                    <div class="product__inner">
                                        <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                            <div class="product__thumb">
                                                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                                                <%= SqlHelper.GenFlashSaleFrame(drProduct) %>
                                            </div>
                                        </a>
                                        <%= SqlHelper.GetTimeCountdownFlashSale(drProduct) %>
                                        <div class="product__info">
                                            <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                            <div class="product__price d-flex align-items-center justify-content-center">
                                                <div class="price"><%= SqlHelper.GetPrice(PriceReturn.Price, drProduct) %></div>
                                                <div class="old-price"><%= SqlHelper.GetPrice(PriceReturn.OriginalPrice, drProduct) %></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <% }
                                } %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<%}
    else if (Modul == "Home" || Modul == "Detail") // Dạng trang chủ hoặc chi tiết sp
    {  %>
<section class="product product__latest-top  moduleProductSlideshow-latest mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title text-center"><span><%= Title %></span></h2>
        </div>
        <div class="product__latest-top__inner">
            <div class="product__latest-content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_<%= Index %> slider" module="<%= Index %>">
                                <% 
                                    DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,Price2,AttrProductFlag,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", Filter), ConfigWeb.SortProductHome, 1, 20);
                                    if (Utils.CheckExist_DataTable(dt))
                                    {
                                %>
                                <%
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        DataRow drProduct = dt.Rows[i];
                                        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                                %>
                                <div class="product__item">
                                    <div class="product__inner">
                                        <div class="product__thumb">
                                            <%=SqlHelper.GetPercentLabel(drProduct)%>
                                            <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                                                <%=SqlHelper.GenFlashSaleFrame(drProduct)%>
                                            </a>
                                        </div>
                                        <div class="product__info">
                                            <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                            <div class="product__price d-flex align-items-center justify-content-center">
                                                <div class="price"><%= SqlHelper.GetPrice(PriceReturn.Price, drProduct) %></div>
                                                <div class="old-price"><%= SqlHelper.GetPrice(PriceReturn.OriginalPrice, drProduct) %></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <% } %><% } %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<% }
    else if (Modul == "FlashSale") // FlashSale Page
    {
%>
<section class="product product__special-topPage moduleProductSlideshow-specials mb-5">
    <div class="container">
        <div class="banner-sale">
            <img src="<%= ConfigWeb.FlashSaleHeader %>" alt="Flash Sale" />
        </div>
        <div class="moduleProductSlideshow__wrap">
            <div class="moduleProductSlideshow__section">
                <div class="moduleProductSlideshow__section-inner product-flash-sale">
                    <div class="product__grid d-grid createProductSlideshows_specials" module="specials">
                        <% 
                            string FilterFlashSale = string.Format("(Hide is null OR Hide=0) AND AttrProductFlag & {0} <> 0 AND Price2>0", (int)AttrProductFlag.FlashSale);
                            DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,Price2,AttrProductFlag,HashTagUrlList", FilterFlashSale, "Sort DESC, ID DESC", 1, 64);
                            if (Utils.CheckExist_DataTable(dt))
                            {
                        %>
                        <%
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow drProduct = dt.Rows[i];
                                string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                        %>
                        <div class="product__item">
                            <div class="product__inner">
                                <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                    <div class="product__thumb">
                                        <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                                        <%=SqlHelper.GenFlashSaleFrame(drProduct)%>
                                    </div>
                                </a>
                                <%= SqlHelper.GetTimeCountdownFlashSale(drProduct) %>
                                <div class="product__info">
                                    <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                    <div class="product__price d-flex align-items-center justify-content-center">
                                        <div class="price"><%= SqlHelper.GetPrice(PriceReturn.Price, drProduct) %></div>
                                        <div class="old-price"><%= SqlHelper.GetPrice(PriceReturn.OriginalPrice, drProduct) %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <% } %><% } %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<%
    }
    else if (Modul == "FlashSaleCategory") // FlashSale Category
    {
%>
<section class="product product__special-topPage moduleProductSlideshow-specials mb-5">
    <div class="container">
        <div class="banner-sale">
            <img src="<%= drCategory["FlashSaleHeader"].ToString() %>" alt="Flash Sale" />
        </div>
        <div class="moduleProductSlideshow__wrap">
            <div class="moduleProductSlideshow__section">
                <div class="moduleProductSlideshow__section-inner product-flash-sale">
                    <div class="product__grid d-grid createProductSlideshows_specials" module="specials">
                        <% 
                            string Category_ID = drCategory["ID"].ToString();
                            string filter = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%')", Category_ID);
                            DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,Price2,AttrProductFlag,HashTagUrlList", filter, "Sort DESC, ID DESC", 1, 64);
                            if (Utils.CheckExist_DataTable(dt))
                            {
                        %>
                        <%
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DataRow drProduct = dt.Rows[i];
                                string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                        %>
                        <div class="product__item">
                            <div class="product__inner">
                               
                                <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                    <div class="product__thumb">
                                         <%= SqlHelper.GetPercentLabel(drProduct) %>
                                        <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                                        <%=SqlHelper.GenFlashSaleFrameCategory(drCategory)%>
                                    </div>
                                </a>
                                <%= SqlHelper.GetTimeCountdownFlashSaleCategory(drCategory) %>
                                <div class="product__info">
                                    <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                    <div class="product__price d-flex align-items-center justify-content-center">
                                        <div class="price"><%= SqlHelper.GetPrice(PriceReturn.Price, drProduct) %></div>
                                        <div class="old-price"><%= SqlHelper.GetPrice(PriceReturn.OriginalPrice, drProduct) %></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <% } %><% } %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
<%
    }
%>

<% } %>