<%@ Control Language="C#" AutoEventWireup="true" CodeFile="product-houseware.ascx.cs" Inherits="template_product_houseware" %>
<section class="product product__houseware">
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
                                         <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
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
</section>