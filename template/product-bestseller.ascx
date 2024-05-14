<%@ Control Language="C#" AutoEventWireup="true" CodeFile="product-bestseller.ascx.cs" Inherits="template_product_bestseller" %>
<section class="product product__bestseller-top moduleProductSlideshow-bestseller mb-5">
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
                                <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
