<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCHomeProduct.ascx.cs" Inherits="Controls_UCHomeProduct" %>
<%@ Import Namespace="System.Data" %>
<section class="product product__latest-top  moduleProductSlideshow-latest mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title text-center"><span><%= Title %> - <%= ReadMore %></span></h2>
        </div>
        <div class="product__latest-top__inner">
            <div class="product__latest-content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_<%= CategoryID %> slider" module="<%= CategoryID %>">
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
