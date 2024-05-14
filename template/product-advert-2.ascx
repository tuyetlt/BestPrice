<%@ Control Language="C#" AutoEventWireup="true" CodeFile="product-advert-2.ascx.cs" Inherits="template_product_advert_2" %>
<section class="banner banner__productAdvert banner__productAdvert-slideshow2 mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title justify-content-center"><span>Tivi</span></h2>
        </div>
    </div>
    <div class="banner__productAdvert-section d-md-flex">
        <div class="col-12 col-md-6">
            <div class="banner__productAdvert-inner d-flex align-items-center">
                <div class="banner__productAdvert-slide__inner col-12">
                    <div class="slider product__grid createProductAdvertSlideshow mb-3" module="2">
                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                    </div>
                    <div class="tags d-flex flex-wrap justify-content-center">
                        <a href="#" target="_self" class="btn tag"><span>Tất cả tivi</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Samsung</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Sony</span></a>
                        <a href="#" target="_self" class="btn tag"><span>LG</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Casper</span></a>
                    </div>
                </div>
            </div>
        </div>    
        <div class="col-12 col-md-6">
            <div class="slider createBannerProductAdvertSliders">
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="/html/images/GR-X257MC-1024x512.jpg" alt="" />
                    </div>
                </div>
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="/html/images/banner-HTP.jpg" alt="" />
                    </div>
                </div>    
            </div>
        </div>
        
    </div>
</section>