<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WidgetProductHistory.ascx.cs" Inherits="Controls_WidgetProductHistory" %>
<%@ Import Namespace="System.Data" %>
<div class="main main__wrapper">
    <div class="container">
        <div class="row">
            <div class="column__main col-12 col-sm-12">
                <section class="product product__latest-top  moduleProductSlideshow-latest mb-5">
                    <div class="container">
                        <div class="heading">
                            <h2 class="heading__title text-center"><span>Sản phẩm bạn vừa xem</span></h2>
                        </div>
                        <div class="product__latest-top__inner">
                            <div class="product__latest-content">
                                <div class="moduleProductSlideshow__wrap">
                                    <div class="moduleProductSlideshow__section">
                                        <div class="moduleProductSlideshow__section-inner">
                                            <div class="product__grid productSliders createProductSlideshows_99999 slider" module="99999">
                                               <% 
                                                        string filterP = string.Format("AttrProductFlag & {0} <> 0", (int)AttrProductFlag.Home);
                                                        if (Utils.CheckExist_DataTable(dtProduct))
                                                        {
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
            </div>
        </div>
    </div>
</div>
