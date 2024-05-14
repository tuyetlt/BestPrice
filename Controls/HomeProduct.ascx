<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeProduct.ascx.cs" Inherits="Controls_HomeProduct" %>
<%@ Import Namespace="System.Data" %>

<% for (int i = 1; i <= 5; i++)
    {
        DataTable dt = SqlHelper.SQLToDataTable("dbo.tblConfigs", "TextHome1, TextHome2, TextHome3, TextHome4, TextHome5", "");
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

            string filter = string.Format("AttrProductFlag & {0} <> 0", intValueFlag);
            Response.Write(Utils.LoadUserControl("~/Controls/UCHomeProduct.ascx", dr[string.Format("TextHome{0}", i)].ToString(), intValueFlag.ToString(), filter, i, true, HightLight));
%>

<%}

    } %>



<%--<section class="product product__special-topPage moduleProductSlideshow-specials mb-5">
    <div class="container">
        <div class="product__special-topPage__inner">
            <div class="heading">
                <h2 class="heading__title justify-content-center text-center text-white"><span>XẢ HÀNG - GIẢM KỊCH SÀN</span></h2>
            </div>
            <div class="product__special-topPage__content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_specials slider" module="specials">
                                <% 
                                    string filterP = string.Format("AttrProductFlag & {0} <> 0", (int)AttrProductFlag.Home);
                                    DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", filterP), ConfigWeb.SortProductHome, 1, 20);
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
                                            <label class="on-sale"><span>-26%</span></label>
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
</section>--%>




<%--<section class="product product__bestseller-top moduleProductSlideshow-bestseller mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title text-center"><span>TOP GIÁ TỐT BÁN CHẠY</span></h2>
        </div>
        <div class="product__bestseller-top__inner">
            <div class="product__bestseller-content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_bestseller slider" module="bestseller">
                                <% 
                                    string filter2 = string.Format("AttrProductFlag & {0} <> 0", (int)AttrProductFlag.Priority);
                                    DataTable dtProduct2 = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", filter2), ConfigWeb.SortProductHome, 1, 20);
                                    if (Utils.CheckExist_DataTable(dtProduct2))
                                    {
                                %>
                                <%
                                    for (int i = 0; i < dtProduct2.Rows.Count; i++)
                                    {
                                        DataRow drProduct = dtProduct2.Rows[i];
                                        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                                %>
                                <div class="product__item">
                                    <div class="product__inner">
                                        <div class="product__thumb">
                                            <label class="on-sale"><span>-26%</span></label>
                                            <a href="<%= linkDetail %>" title="Máy nước nóng Panasonic DH-4RP1" class="product__image">
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
</section>--%>



<%--<section class="product product__houseware">
    <div class="container">
        <div class="heading">
            <h3 class="heading__title"><span>Tốp sản phẩm gia dụng</span></h3>
        </div>
        <div class="product__houseware-inner">
            <div class="tabs mb-4">
                <ul id="productHouseWareTabs" class="tabs__nav d-flex justify-content-center">
                    <li class="tabs__nav-item"><a href="#tabs-1" class="tabs__nav-link">Máy lọc nước</a></li>
                    <li class="tabs__nav-item"><a href="#tabs-2" class="tabs__nav-link">Máy lọc không khí</a></li>
                    <li class="tabs__nav-item"><a href="#tabs-3" class="tabs__nav-link">Bếp điện từ, hồng ngoại</a></li>
                    <li class="tabs__nav-item"><a href="#tabs-4" class="tabs__nav-link">Máy nước nóng</a></li>
                    <li class="tabs__nav-item"><a href="#tabs-5" class="tabs__nav-link">Bàn ủi, bàn là</a></li>
                    <li class="tabs__nav-item"><a href="#tabs-6" class="tabs__nav-link">Quạt điện</a></li>
                </ul>
                <div class="tabs__content">
                    <div id="tabs-1" class="tabs__pane moduleProductSlideshow-tabs-1">
                        <div class="moduleProductSlideshow__wrap">
                            <div class="moduleProductSlideshow__section">
                                <div class="moduleProductSlideshow__section-inner">
                                    <div class="product__grid productSliders createProductSlideshows_tabs-1 slider" module="tabs-1">
                                        <% 
                                            string filter3 = string.Format("AttrProductFlag & {0} <> 0", (int)AttrProductFlag.Home);
                                            DataTable dtProduct3 = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name,FriendlyUrl,FriendlyUrlCategory,Gallery,Price,Price1,HashTagUrlList", string.Format("(Hide is null OR Hide=0) AND {0}", filter1), ConfigWeb.SortProductHome, 1, 20);
                                            if (Utils.CheckExist_DataTable(dtProduct1))
                                            {
                                        %>
                                        <%
                                            for (int i = 0; i < dtProduct3.Rows.Count; i++)
                                            {
                                                DataRow drProduct = dtProduct3.Rows[i];
                                                string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                                        %>
                                        <div class="product__item">
                                            <div class="product__inner">
                                                <div class="product__thumb">
                                                    <label class="on-sale"><span>-26%</span></label>
                                                    <a href="#" title="Máy nước nóng Panasonic DH-4RP1" class="product__image">
                                                        <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 400, 400) %>" alt="<%= drProduct["Name"].ToString() %>" />
                                                </div>
                                                <div class="product__info">
                                                    <h3 class="product__name"><a href="#" title="Máy nước nóng Panasonic DH-4RP1"><%= drProduct["Name"].ToString() %></a></h3>
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
                    <div id="tabs-2" class="tabs__pane moduleProductSlideshow-tabs-2">
                        <div class="moduleProductSlideshow__wrap">
                            <div class="moduleProductSlideshow__section">
                                <div class="moduleProductSlideshow__section-inner">
                                    <div class="product__grid productSliders createProductSlideshows_tabs-2 slider" module="tabs-2">
                                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-3" class="tabs__pane moduleProductSlideshow-tabs-3">
                        <div class="moduleProductSlideshow__wrap">
                            <div class="moduleProductSlideshow__section">
                                <div class="moduleProductSlideshow__section-inner">
                                    <div class="product__grid productSliders createProductSlideshows_tabs-3 slider" module="tabs-3">
                                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-4" class="tabs__pane moduleProductSlideshow-tabs-4">
                        <div class="moduleProductSlideshow__wrap">
                            <div class="moduleProductSlideshow__section">
                                <div class="moduleProductSlideshow__section-inner">
                                    <div class="product__grid productSliders createProductSlideshows_tabs-4 slider" module="tabs-4">
                                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-5" class="tabs__pane moduleProductSlideshow-tabs-5">
                        <div class="moduleProductSlideshow__wrap">
                            <div class="moduleProductSlideshow__section">
                                <div class="moduleProductSlideshow__section-inner">
                                    <div class="product__grid productSliders createProductSlideshows_tabs-5 slider" module="tabs-5">
                                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-6" class="tabs__pane moduleProductSlideshow-tabs-6">
                        <div class="moduleProductSlideshow__wrap">
                            <div class="moduleProductSlideshow__section">
                                <div class="moduleProductSlideshow__section-inner">
                                    <div class="product__grid productSliders createProductSlideshows_tabs-6 slider" module="tabs-6">
                                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="buttons text-center">
                <a href="#" class="btn btn-primary min-w200">Xem them</a>
            </div>
        </div>
    </div>
    <script>
        // Show the first tab and hide the rest
        $('#productHouseWareTabs li:first-child').addClass('active');
        $('.tabs__content .tabs__pane').hide();
        $('.tabs__content .tabs__pane:first').show();

        $('#productHouseWareTabs > li').on('click', function (e) {
            e.preventDefault();

            const productSliders = document.querySelectorAll('.productSliders');

            if (productSliders) {
                for (let i = 0; i < productSliders.length; i++) {
                    var module_id = productSliders[i].getAttribute('module');
                    const sliders = document.querySelector('.createProductSlideshows_' + module_id);

                    if ($(sliders).hasClass('slick-initialized')) {
                        sliders.slick.destroy();
                    }
                }
            }

            $('#productHouseWareTabs > li').removeClass('active');
            $(this).addClass('active');
            $('.tabs__content .tabs__pane').hide();

            var activeTab = $(this).find('a').attr('href');
            $(activeTab).fadeIn(300);
            QHGraphic.Module.createProductSlideshow();
            return false;
        });
    </script>
</section>--%>
