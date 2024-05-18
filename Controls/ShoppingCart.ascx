<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShoppingCart.ascx.cs" Inherits="Controls_ShoppingCart" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="System.Data" %>



<% decimal finalPrice = 0; %>

<%
    PageInfo.ControlName = "Giỏ hàng";
    string displayNoCart = string.Empty;
    string displayFormInfo = string.Empty;
    if (ShoppingCart.CartCount == 0)
    {
        displayNoCart = "display:block";
        displayFormInfo = "display:none";
    }
    else
    {
        displayNoCart = "display:none";
        displayFormInfo = "display:block";
    }
%>


<div style="<%= displayFormInfo %>" class="main main__wrapper cart__page">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>
        <div class="page__heading">
            <h1 class="page__title on-left">Giỏ hàng</h1>
        </div>
        <div class="row two-column">
            <div class="column__main col-12 col-md-9">
                <div class="checkout__cart">

                    <div class="cart__list">

                        <%    List<OrderInfo> orderInfoList = ShoppingCart.GetOrderInfo(out finalPrice); %>
                        <div class="cart__item thead">
                            <div class="cart__inner d-grid align-items-center">
                                <div class="cart__desc">Sản phẩm <small class="text-black-50 cart_count">(<%= orderInfoList.Count %> sản phẩm)</small></div>
                                <div class="cart__prices d-none d-sm-block">Đơn giá</div>
                                <div class="cart__qty text-center d-none d-sm-block">Số lượng</div>
                                <div class="cart__action"><i class="icon-trash"></i></div>
                            </div>
                        </div>
                        <%
                            if (orderInfoList.Count > 0)
                            {
                                foreach (OrderInfo orderInfo in orderInfoList)
                                {
                                    DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", "ID=" + orderInfo.ProductID);
                                    if (Utils.CheckExist_DataTable(dtProduct))
                                    {
                                        string link = TextChanger.GetLinkRewrite_Products(dtProduct.Rows[0]["FriendlyUrlCategory"], dtProduct.Rows[0]["FriendlyUrl"]);
                        %>
                        <div class="cart__item tbody item<%= orderInfo.ProductID %>">
                            <div class="cart__inner d-grid align-items-sm-center">
                                <input class="hdfPrice_<%= orderInfo.ProductID %>" value="<%= orderInfo.TotalPrice %>" type="hidden" />
                                <div class="cart__img">
                                    <a href="#" class="thumb">
                                        <img src="<%= orderInfo.Image %>" alt="<%= orderInfo.Name %>" />
                                    </a>
                                </div>
                                <div class="cart__product d-grid align-items-center">
                                    <div class="cart__desc">
                                        <div class="product__name"><a href="<%=link %>"><%= orderInfo.Name %></a></div>
                                    </div>
                                    <div class="cart__prices">
                                        <div class="price price_item_<%= orderInfo.ProductID %>"><strong><%=string.Format("{0:N0}", orderInfo.TotalPrice) %> VNĐ</strong></div>
                                    </div>
                                    <div class="cart__qty text-sm-center">
                                        <div class="input-group block-qty-cart">
                                            <div class="input-group-btn">
                                                <button type="button" class="btn btn-default btn-touchspin-down increment minus"><i class="icon-minus"></i></button>
                                            </div>
                                            <div class="input-group-btn">
                                                <input type="text" name="quantity" size="1" class="inputQty text-center form-control quantity_cart" id="<%= orderInfo.ProductID %>" value="<%= orderInfo.Quantity %>">
                                            </div>
                                            <div class="input-group-btn">
                                                <button class="btn btn-default btn-touchspin-up increment plus" type="button"><i class="icon-plus"></i></button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="cart__action">
                                        <a href="javascript:void(0);" class="btn-delete btn-deleteCart del_cart" data-id="<%= orderInfo.ProductID %>"><i class="icon-trash"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%
                                    }
                                }
                            }
                        %>
                    </div>

                    <div class="checkout__address bg-white bg-frame mb-3">
                        <div class="d-flex">
                            <h3 class="checkout__title">Thông tin vận chuyển</h3>
                           <%-- <a class="ms-auto text-primary" href="#">Thay đổi</a>--%>
                        </div>
                        <form method="post" enctype="multipart/form-data" id="frm_checkout">
                            <div class="form-group row">
                                <div class="col">
                                    <input type="text" id="name" name="name" value="" placeholder="Nhập tên" class="form-control" required><span class="required">*</span>
                                </div>
                                <div class="col">
                                    <input type="text" id="tel" name="tel" value="" placeholder="Số điện thoại" class="form-control" required><span class="required">*</span>
                                </div>
                            </div>
                            <div class="form-group">
                                <input type="text" id="email" name="email" value="" placeholder="E-mail" class="form-control">
                            </div>
                            <div class="form-group">
                                <input type="text" id="address" name="address" value="" placeholder="Địa chỉ (ví dụ: 70 Đông Hưng Thuận 5, Phường Tân Hưng Thuận, Quận 12, Tp.HCM)" class="form-control" required><span class="required">*</span>
                            </div>
                            <%-- <div class="row">
                            <div class="form-group col-12 col-sm-4">
                                <select name="zone" class="form-select">
                                    <option value="*">--- Tỉnh / Thành phố ---</option>
                                    <option value="HCM">Tp.Hồ Chí Minh</option>
                                    <option value="HN">Hà Nội</option>
                                </select>
                            </div>
                            <div class="form-group col-12 col-sm-4">
                                <select name="district" class="form-select">
                                    <option value="*">--- Quận / Huyện ---</option>
                                    <option value="HCM">Quận 1</option>
                                    <option value="HN">Quận 2</option>
                                </select>
                            </div>
                            <div class="form-group col-12 col-sm-4">
                                <select name="ward" class="form-select">
                                    <option value="*">--- Phường / Xã ---</option>
                                    <option value="APĐ">An Phú Đông</option>
                                    <option value="TX">Thạnh Xuân</option>
                                </select>
                            </div>
                        </div>--%>
                            <div class="form-group">
                                <textarea name="note" id="note" rows="3" class="form-control" placeholder="Ghi chú thêm (Ví dụ: Giao hàng giờ hành chính)"></textarea>
                            </div>
                            <%-- <div class="checkbox">
                                <label>
                                    <input type="checkbox" name="">
                                    <span>Lưu vào sổ địa chỉ để dùng cho lần mua hàng tiếp theo</span>
                                </label>
                            </div>--%>
                            <ul class="list-content-nganluong" style="list-style:none; margin:5px 0 20px; padding:0">
                                <li class="active">
                                    <input type="radio" name="option_payment" id="COD" value="COD" checked="checked">
                                    <label for="COD">Thanh toán tiền mặt khi nhận hàng (COD)</label>
                                    <div class="boxContent">
                                        <img src="/themes/assets/images/cod.jpg" style="max-width:50%" alt="Giao hàng nhận tiền" />
                                    </div>
                                </li>

                                <li>
                                    <input type="radio" name="option_payment" id="chuyenkhoan" value="chuyenkhoan">
                                    <label for="chuyenkhoan">Liên hệ chuyển khoản trực tiếp với nhân viên tư vấn</label>

                                </li>

                                <li>
                                    <input type="radio" name="option_payment" id="vnpay" value="vnpay">
                                    <label for="vnpay">Thanh toán tự động bằng Internet banking</label>
                                </li>

                            </ul>

                            <div class="buttons text-center">
                                <a href="javascript:;" id="btnSubmit" class="btn btn-primary d-block">Hoàn tất</a>
                            </div>
                            <input type="hidden" id="done" name="done" value="0" />
                        </form>
                    </div>




                </div>
            </div>
            <div class="column__right col-12 col-md-3">
                <div class="cart__column-right">
                    <%--  <div class="cart__shipping bg-white bg-frame mb-3">
                        <div class="block-header d-flex mb-2">
                            <strong class="title">Giao tới</strong>
                            <a class="ms-auto text-primary" href="#">Thay đổi</a>
                        </div>
                        <div class="customer-info d-flex">
                            <span class="customer-info__name">NGUYEN VAN QUAN</span>
                            <span class="space">&nbsp;</span>
                            <span class="customer_info__phone">0967773668</span>
                        </div>
                        <div class="address">60/38 An Phú Đông 10, Phường An Phú Đông, Quận 12, Hồ Chí Minh</div>
                    </div>--%>
                    <%-- <div class="cart__coupon bg-white bg-frame mb-3">
                        <p><strong>Sử dụng Voucher</strong></p>
                        <form>
                            <div class="d-flex">
                                <div class="col">
                                    <input type="text" name="coupon" value="" placeholder="Nhập mã giảm giá" class="form-control" />
                                </div>
                                <div class="cart__coupon-action">
                                    <button type="submit" class="btn btn-primary">Áp dụng</button>
                                </div>
                            </div>
                        </form>
                    </div>--%>
                    <div class="cart__totals bg-white bg-frame">
                        <div class="total-item d-flex">
                            <div class="label">Tạm tính</div>
                            <div class="col text-end"><span class="cart_total_price"><%=string.Format("{0:N0}", finalPrice) %> VNĐ</span></div>
                        </div>
                        <%-- <div class="total-item d-flex">
                            <div class="label">Giảm giá</div>
                            <div class="col text-end"><span>-50.000đ</span></div>
                        </div>--%>
                        <div class="total-item d-flex">
                            <div class="label">Phí giao hàng</div>
                            <div class="col text-end"><span>Liên hệ</span></div>
                        </div>
                        <div class="total-item d-flex">
                            <div class="label">Tổng tiền</div>
                            <div class="col text-end"><strong class="cart_total_price"><%=string.Format("{0:N0}", finalPrice) %> VNĐ</strong></div>
                            <input type="hidden" id="hdfTotalPrice" name="hdfTotalPrice" value="<%= string.Format("{0}",Math.Round(finalPrice)) %>" />
                        </div>
                    </div>


                </div>
            </div>
        </div>

    </div>
</div>
<div style="<%= displayNoCart %>" class="main main__wrapper cart-empty">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>
        <div class="page__heading">
            <p style="text-align: center">
                <h2 class="page__title on-left">Giỏ hàng của quý khách chưa có sản phẩm!</h2>
                <a href="/">Tiếp tục mua sắm</a>
            </p>
        </div>
        <p style="text-align: center">
            <a href="/">
                <img src="/themes/assets/images/cart-empty.png" style="max-width: 400px; margin: 0 auto" /></a>
        </p>
    </div>
</div>

<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        SaveUTMData('ShoppingCart', "");
    });
</script>

<%
    int count = 0;
    string Items = "[";
    if (orderInfoList.Count > 0)
    {
        foreach (OrderInfo orderInfo in orderInfoList)
        {
            count++;
            string dau_phay = ",";
            if (count == orderInfoList.Count)
                dau_phay = string.Empty;

            Items += string.Format(@"{{""id"":""{0}"",""productName"":""{1}"",""quantity"":""{2}""}}{3}", orderInfo.ProductID, orderInfo.Name, orderInfo.Quantity, dau_phay);
        }
    }
    Items += "]";
%>

<input type="hidden" value="cart" id="GG_Page" />
<input type="hidden" value="<%= string.Format("{0:0}", finalPrice) %>" id="GG_Price" />
<input type="hidden" value='<%= Items %>' id="GG_Items" />
<input type="hidden" value="<%= count %>" id="GG_CountItems" />
