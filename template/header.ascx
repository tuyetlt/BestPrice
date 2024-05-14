<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="template_header" %>
<header class="header">
    <div class="header__top container">
        <button class="btn btnNavigationMenu d-md-none" role="button"><span class="icon-fa-menu"></span></button>
        <div class="header__top-inner d-md-flex  align-items-sm-center">
            <div class="header__logo">
                <a href="/" title="Công Ty Điện Lạnh Võ Gia Tiến" class="header__logo-link">
                    <img src="/html/images/logo.jpg" title="Công Ty Điện Lạnh Võ Gia Tiến" alt="Công Ty Điện Lạnh Võ Gia Tiến" class="img-responsive" />
                </a>
            </div>
            <div class="header__right col pr-0">
                <div class="header__right-inner d-md-flex align-items-center justify-content-end">
                    <div class="header__search">
                        <div class="header__search-inner">
                            <div class="header__search-field">
                                <input type="text" name="search" value="" placeholder="Tìm kiếm" class="form-control" />
                            </div>
                            <button type="button" class="btn btn-primary btn-top-search"><i class="icon-search"></i></button>
                        </div>
                    </div>
                    <div class="header__col header__nav">
                        <ul class="nav">
                            <li class="nav-item"><a href="#" class="nav-link">Hàng thanh lý</a></li>
                            <li class="nav-item"><a href="#" class="nav-link">Tin hay</a></li>
                            <li class="nav-item"><a href="#" class="nav-link">Liên hệ báo giá</a></li>
                        </ul>
                    </div>
                    <div class="header__col header__contact">
                        <div class="d-flex align-items-center">
                            <div class="contact-no">
                                <a href="tel:0967773668">096-777-3668</a>
                            </div>
                        </div>
                    </div>
                    <div class="header__cart block-top-cart" id="cart">
                        <a href="#" data-loading-text="text_loading" class="btn btn-cart">
                            <i class="icon-shipping-cart"></i>
                            <span id="cart-total">0</span>
                        </a>
                        <ul id="loadCart" class="dropdown-menu list-item-cart">
                            <li class="item">
                                <a href="javascript:void(0)" onclick="cart.close('.block-top-cart');" class="btn-close-cart"><span class="icon-close"></span></a>
                                <div class="cart-inform">
                                    <div class="add-success"><span class="fa fa-check"></span></div>
                                    <p class="text-center">text_success</p>
                                </div>
                                <div class="buttons-set">
                                    <a href="#" class="btn btn-primary">text_cart</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</header>
