<%@ Control Language="C#" AutoEventWireup="true" CodeFile="product-special.ascx.cs" Inherits="template_product_special" %>
<section class="product product__special-topPage moduleProductSlideshow-specials mb-5">
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
                            <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
    </div>
</section>