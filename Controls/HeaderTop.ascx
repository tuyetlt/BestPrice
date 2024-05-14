<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderTop.ascx.cs" Inherits="Controls_HeaderTop" %>
<%@ Import Namespace="System.Data" %>
<header class="header">
    <div class="header__top container">
        <button class="btn btnNavigationMenu d-md-none" role="button"><span class="icon-fa-menu"></span></button>
        <div class="header__top-inner d-md-flex  align-items-sm-center">
            <div class="header__logo">
                <a href="<%= C.ROOT_URL %>" class="header__logo-link">
                    <img src="<%= C.ROOT_URL %><%= ConfigWeb.Logo %>" alt="<%= ConfigWeb.MetaTitle %>" class="img-responsive"></a>
            </div>
            <div class="header__right col pr-0">
                <div class="header__right-inner d-md-flex align-items-center justify-content-end">
                    <div class="header__search col">
                        <div class="header__search-inner">
                            <form method="GET" action="<%=C.ROOT_URL %>/tim-kiem.html" data-search="internal">
                                <div class="header__search-field dropdown">
                                    <input type="text" name="search" id="input-search" data-bs-toggle="dropdown" aria-expanded="false" autocomplete="off" value="" placeholder="Tìm kiếm" class="form-control" />
                                    <div class="dropdown-menu">
                                        <div class="dropdown-inner">
                                            <div id="#search-suggestions">
                                                <ul></ul>
                                            </div>
                                            <ul>
                                                <li>
                                                    <div class="d-flex align-items-center">
                                                        <div class="col d-flex align-items-center">
                                                            <div class="img">
                                                                <img src="https://meta.vn/api/cateico.aspx?id=1015" />
                                                            </div>
                                                            <div class="product__name">Tủ lạnh Samsung</div>
                                                        </div>
                                                        <a href="#" class="btn-remove-search"><i class="icon-close"></i></a>
                                                    </div>
                                                </li>
                                              
                                            </ul>
                                            <hr>
                                            <div class="category__latest">
                                                <p class="fs-14"><strong>Danh mục nổi bật</strong></p>
                                                <div class="search-cat-list d-grid">
                                                    <div class="search-cat-item">
                                                        <a href="#">
                                                            <div class="img">
                                                                <img src="https://meta.vn/icons/cateico/c3256-168x168.jpg" />
                                                            </div>
                                                            <div class="search-cat__name">Tivi</div>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="text-center mt-3"><a href="#" class="fs-14 text-primary">Xem thêm</a></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <button type="button" class="btn btn-primary btn-top-search"><i class="icon-search"></i></button>
                            </form>
                        </div>
                    </div>
                    <div class="header__col header__nav">
                        <ul class="nav">
                            <%
                                int menuNgang = (int)PositionMenuFlag.SupperTop;
                                string filterMenuNgang = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", menuNgang);
                                DataTable dt_ngang = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "LinkTypeMenuFlag,FriendlyUrl,Link,Name", string.Format("ParentID=0 AND {0}", filterMenuNgang), "Sort");
                                if (Utils.CheckExist_DataTable(dt_ngang))
                                {
                                    foreach (DataRow dr_sub in dt_ngang.Rows)
                                    {
                                        Response.Write(string.Format(@"<li class=""nav-item""><a href=""{0}"" class=""nav-link"">{1}</a></li>", Utils.CreateCategoryLink(dr_sub["LinkTypeMenuFlag"], dr_sub["FriendlyUrl"], dr_sub["Link"]), dr_sub["Name"].ToString()));
                                    }
                                }
                            %>
                        </ul>
                    </div>
                    <div class="header__col header__contact">
                        <div class="d-flex align-items-center">
                            <div class="contact-no">
                                <a href="tel:<%= ConfigWeb.Hotline %>"><%= ConfigWeb.Hotline %></a>
                            </div>
                        </div>
                    </div>
                    <div class="header__cart block-top-cart" id="cart">
                        <a href="/gio-hang.html" data-loading-text="text_loading" class="btn btn-cart">
                            <i class="icon-shipping-cart"></i>
                            <span id="cart-total">0</span>
                        </a>
                        <ul id="loadCart" class="dropdown-menu list-item-cart">
                            <li class="item">
                                <a href="javascript:void(0)" onclick="cart.close('.block-top-cart');" class="btn-close-cart"><span class="icon-close"></span></a>
                                <div class="cart-inform">
                                    <div class="add-success"><span class="icon-check"></span></div>
                                    <p class="text-center">text_success</p>
                                </div>
                                <div class="buttons-set">
                                    <a href="#" class="btn btn-primary">Xem giỏ hàng</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</header>
