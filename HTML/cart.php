<?php
  /* Check if SSL */
  if ((isset($_SERVER['HTTPS']) && (($_SERVER['HTTPS'] == 'on') || ($_SERVER['HTTPS'] == '1'))) || $_SERVER['SERVER_PORT'] == 443) {
      $protocol = 'https://';
  } elseif (!empty($_SERVER['HTTP_X_FORWARDED_PROTO']) && $_SERVER['HTTP_X_FORWARDED_PROTO'] == 'https' || !empty($_SERVER['HTTP_X_FORWARDED_SSL']) && $_SERVER['HTTP_X_FORWARDED_SSL'] == 'on') {
      $protocol = 'https://';
  } else {
      $protocol = 'http://';
  }

  define('HTTP_SERVER', $protocol . $_SERVER['HTTP_HOST'] . rtrim(dirname($_SERVER['PHP_SELF']), '/.\\'));
?>
<!DOCTYPE html>
<html dir="ltr" lang="vi">
<head>
<meta charset="UTF-8" />
<meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<title>Công Ty Điện Lạnh Võ Gia Tiến</title>
<base href="/" />
<meta name="keywords" content="Công Ty Điện Lạnh Võ Gia Tiến" />
<meta name="description" content="Công Ty Điện Lạnh Võ Gia Tiến" />
<meta property="og:url" content="#" />
<meta property="og:type" content="article" />
<meta property="og:title" content="Công Ty Điện Lạnh Võ Gia Tiến" />
<meta property="og:description" content="Công Ty Điện Lạnh Võ Gia Tiến" />
<meta property="og:image" content="/images/logo.jpg" />
<link rel="apple-touch-icon" sizes="32x32" href="#" />
<link rel="apple-touch-icon" sizes="64x64" href="#" />
<link rel="icon" type="image/png" sizes="32x32" href="#" />
<link rel="icon" type="image/png" sizes="64x64" href="#" />
<link href="<?php echo HTTP_SERVER; ?>/assets/css/bootstrap.min.css" rel="stylesheet" media="screen" />
<script src="<?php echo HTTP_SERVER; ?>/assets/js/jquery/jquery-3.7.1.min.js" type="text/javascript"></script>
<script src="<?php echo HTTP_SERVER; ?>/assets/js/bootstrap/popper.min.js"></script>
<script src="<?php echo HTTP_SERVER; ?>/assets/js/bootstrap/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<?php require_once 'template/header.php'; ?>
<?php require_once 'template/menu.php'; ?>
<div class="main main__wrapper cart__page">
    <div class="container">
        <div class="page__heading">
            <h1 class="page__title on-left">Giỏ hàng</h1>
        </div>
        <div class="row two-column">
            <div class="column__main col-12 col-md-9">
                <div class="checkout__cart">
                    <form action="#" id="FormCart">
                        <div class="cart__list">
                            <div class="cart__item thead">
                                <div class="cart__inner d-grid align-items-center">
                                    <div class="cart__desc">Sản phẩm <small class="text-black-50">(3 items)</small></div>
                                    <div class="cart__prices d-none d-sm-block">Đơn giá</div>
                                    <div class="cart__qty text-center d-none d-sm-block">Số lượng</div>
                                    <div class="cart__action"><i class="icon-trash"></i></div>
                                </div>
                            </div>
                            <div class="cart__item tbody">
                                <div class="cart__inner d-grid align-items-sm-center">
                                    <div class="cart__img">
                                        <a href="#" class="thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </a>
                                    </div>
                                    <div class="cart__product d-grid align-items-center">
                                        <div class="cart__desc">
                                            <div class="product__name"><a href="#">Máy hút mùi SUNHOME 2721II thân inox chính hãng</a></div>
                                            <div class="product__model"><span>Mã SP:</span> PB056</div>
                                        </div>
                                        <div class="cart__prices">
                                            <div class="price"><strong>35,000đ</strong></div>
                                        </div>
                                        <div class="cart__qty text-sm-center">
                                            <div class="input-group block-qty-cart">
                                                <div class="input-group-btn">
                                                    <button type="button" class="btn btn-default btn-touchspin-down"><i class="icon-minus"></i></button>
                                                </div>
                                                <div class="input-group-btn">
                                                    <input type="text" name="quantity" value="1" size="1" id="input-quantity" class="inputQty text-center form-control">
                                                </div>
                                                <div class="input-group-btn">
                                                    <button class="btn btn-default btn-touchspin-up" type="button"><i class="icon-plus"></i></button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="cart__action">
                                            <a href="javascript:void(0);" class="btn-delete btn-deleteCart" onclick="cart.remove('846');"><i class="icon-trash"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="cart__item tbody">
                                <div class="cart__inner d-grid align-items-sm-center">
                                    <div class="cart__img">
                                        <a href="#" class="thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </a>
                                    </div>
                                    <div class="cart__product d-grid align-items-center">
                                        <div class="cart__desc">
                                            <div class="product__name"><a href="#">Máy hút mùi SUNHOME 2721II thân inox chính hãng</a></div>
                                            <div class="product__model"><span>Mã SP:</span> PB056</div>
                                        </div>
                                        <div class="cart__prices">
                                            <div class="price"><strong>35,000đ</strong></div>
                                        </div>
                                        <div class="cart__qty text-sm-center">
                                            <div class="input-group block-qty-cart">
                                                <div class="input-group-btn">
                                                    <button type="button" class="btn btn-default btn-touchspin-down"><i class="icon-minus"></i></button>
                                                </div>
                                                <div class="input-group-btn">
                                                    <input type="text" name="quantity" value="1" size="1" id="input-quantity" class="inputQty text-center form-control">
                                                </div>
                                                <div class="input-group-btn">
                                                    <button class="btn btn-default btn-touchspin-up" type="button"><i class="icon-plus"></i></button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="cart__action">
                                            <a href="javascript:void(0);" class="btn-delete btn-deleteCart" onclick="cart.remove('846');"><i class="icon-trash"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <div class="column__right col-12 col-md-3">
                <div class="cart__column-right">
                    <div class="cart__shipping bg-white bg-frame mb-3">
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
                    </div>
                    <div class="cart__coupon bg-white bg-frame mb-3">
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
                    </div>
                    <div class="cart__totals bg-white bg-frame">
                        <div class="total-item d-flex">
                            <div class="label">Tạm tính</div>
                            <div class="col text-end"><span>150.000đ</span></div>
                        </div>
                        <div class="total-item d-flex">
                            <div class="label">Giảm giá</div>
                            <div class="col text-end"><span>-50.000đ</span></div>
                        </div>
                        <div class="total-item d-flex">
                            <div class="label">Phí giao hàng</div>
                            <div class="col text-end"><span>Miễn phí</span></div>
                        </div>
                        <div class="total-item d-flex">
                            <div class="label">Tổng tiền</div>
                            <div class="col text-end"><strong>250.000đ</strong><br /><small class="fs-12">(Đã bao gồm VAT nếu có)</small></div>
                        </div>
                    </div>

                    <div class="buttons text-center">
                        <a href="<?php echo HTTP_SERVER ;?>/checkout.php" class="btn btn-primary d-block">Thanh toán</a>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
</div>
<?php require_once 'template/footer.php'; ?>
</body>
</html>
