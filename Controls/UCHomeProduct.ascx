<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCHomeProduct.ascx.cs" Inherits="Controls_UCHomeProduct" %>
<%@ Import Namespace="System.Data" %>

<%if (CategoryID > 0) //Nếu là danh mục
    {  %>
<% 
    string filterP = string.Format("AttrProductFlag & {0} <> 0", (int)AttrProductFlag.Home);
    DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1", string.Format("(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%') AND ({1})", CategoryID, filterP), ConfigWeb.SortProductHome, 1, 16);
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
            <% if (!string.IsNullOrEmpty(SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"]))))
                { %>
            <label class="on-sale"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"])) %></span></label>
            <% } %>
            <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" /></a>
        </div>
        <div class="product__info">
            <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
            <div class="product__price d-flex mb-d-flex align-items-center justify-content-center">
                <div class="price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></div>
                <div class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></div>
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
                    <div id='tiles'></div>
                </div>
                <h2 class="heading__title justify-content-center text-center text-white"><a href="/"><span class="text-title"><%= Title %></span></a></h2>
                <a href="/flash-sale/" target="_self" class="btn-more"></a>
            </div>
            <div class="product__special-topPage__content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_specials slider" module="specials">
                                <% 
                                    DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", Filter), ConfigWeb.SortProductHome, 1, 20);
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
                                            <% if (!string.IsNullOrEmpty(SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"]))))
                                                { %>
                                            <label class="on-sale"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"])) %></span></label>
                                            <% } %>
                                            <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" /></a>
                                        <div class="frame-flash-sale">
                                            <img src="/themes/images/summer.png" alt="Alternate Text" />
                                        </div>
                                        </div>
                                        <div class="timeCountdown" data-date="December 24, 2024 21:14:01">
                                            <span class="hours"></span>
                                            <b>:</b>
                                            <span class="minutes"></span>
                                            <b>:</b>
                                            <span class="seconds"></span>
                                        </div>
                                        <div class="product__info">
                                            <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                            <div class="product__price d-flex align-items-center justify-content-center">
                                                <div class="price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></div>
                                                <div class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></div>
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

<%}
    else if (Modul == "Home" || Modul=="Detail") // Dạng trang chủ hoặc chi tiết sp
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
                                    DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", Filter), ConfigWeb.SortProductHome, 1, 20);
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
                                            <% if (!string.IsNullOrEmpty(SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"]))))
                                                { %>
                                            <label class="on-sale"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"])) %></span></label>
                                            <% } %>
                                            <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" /></a>
                                            
                                        </div>
                                        <div class="product__info">
                                            <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                            <div class="product__price d-flex align-items-center justify-content-center">
                                                <div class="price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></div>
                                                <div class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></div>
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
            <img src="/themes/images/cate.webp" alt="banner cate flash sale" />
        </div>
        <div class="moduleProductSlideshow__wrap">
                <div class="moduleProductSlideshow__section">
                    <div class="moduleProductSlideshow__section-inner product-flash-sale">
                        <div class="product__grid d-grid createProductSlideshows_specials" module="specials">
                            <% 
                                DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", Filter), ConfigWeb.SortProductHome, 1, 32);
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
                                        <% if (!string.IsNullOrEmpty(SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"]))))
                                            { %>
                                        <label class="on-sale"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"])) %></span></label>
                                        <% } %>
                                        <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                            <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                                        </a>
                                        <div class="frame-flash-sale">
                                            <img src="/themes/images/summer.png" alt="Alternate Text" />
                                        </div>
                                         <div class="frame-label-sale">
                                             <img src="/themes/images/sale.webp" alt="Sale" />
                                         </div>
                                        <div class="icon-flash-sale">
                                            <img src="/themes/images/icon-flash-sale.png" alt="Alternate Text" />
                                        </div>
                                    </div>
                                    <div class="timeCountdown" data-date="December 24, 2024 21:14:01">
                                        <span class="hours"></span>
                                        <b>:</b>
                                        <span class="minutes"></span>
                                        <b>:</b>
                                        <span class="seconds"></span>
                                    </div>
                                    <div class="product__info">
                                        <h3 class="product__name"><a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>"><%= drProduct["Name"].ToString() %></a></h3>
                                        <div class="product__price d-flex align-items-center justify-content-center">
                                            <div class="price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></div>
                                            <div class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></div>
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