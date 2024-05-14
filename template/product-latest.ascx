<%@ Control Language="C#" AutoEventWireup="true" CodeFile="product-latest.ascx.cs" Inherits="template_product_latest" %>
<section class="product product__latest-top  moduleProductSlideshow-latest mb-5">
    <div class="container">
        <div class="heading">
          <h2 class="heading__title text-center"><span>HÀNG MỚI VỀ</span></h2>
        </div>
        <div class="product__latest-top__inner">
            <div class="product__latest-content">
                <div class="moduleProductSlideshow__wrap">
                    <div class="moduleProductSlideshow__section">
                        <div class="moduleProductSlideshow__section-inner">
                            <div class="product__grid productSliders createProductSlideshows_latest slider" module="latest">
                                <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>