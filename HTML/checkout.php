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
<div class="main main__wrapper checkout__page">
    <div class="container">
        <div class="page__heading">
            <h1 class="page__title on-left">Thanh toán</h1>
        </div>
        <div class="row two-column">
            <div class="column__main col-12 col-md-6">
                <div class="checkout__address bg-white bg-frame mb-3">
                    <div class="d-flex">
                        <h3 class="checkout__title">Thông tin vận chuyển</h3>
                        <a class="ms-auto text-primary" href="#">Thay đổi</a>
                    </div>
                    <form>
                        <div class="form-group row">
                            <div class="col">
                                <input type="text" name="name" value="" placeholder="Nhập tên" class="form-control" />
                            </div>
                            <div class="col">
                                <input type="text" name="phone" value="" placeholder="Số điện thoại" class="form-control" />
                            </div>
                        </div>
                        <div class="form-group">
                            <input type="text" name="email" value="" placeholder="E-mail" class="form-control" />
                        </div>
                        <div class="form-group">
                            <input type="text" name="address" value="" placeholder="Địa chỉ (ví dụ: 123 Vạn Phúc, Phường Vạn Phúc...)" class="form-control" />
                        </div>
                        <div class="row">
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
                        </div>
                        <div class="form-group">
                            <textarea name="" rows="3" class="form-control" placeholder="Ghi chú thêm (Ví dụ: Giao hàng giờ hành chính)"></textarea>
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" name="" />
                                <span>Lưu vào sổ địa chỉ để dùng cho lần mua hàng tiếp theo</span>
                            </label>
                        </div>
                    </form>
                </div>
                <div class="checkout__payment bg-white bg-frame mb-3">
                    <h3 class="checkout__title">Hình thức thanh toán</h3>
                    <form action="#">
                        <div class="radio">
                            <label>
                                <input type="radio" name="payment_method" value="momo" checked />
                                <span class="d-flex align-items-center">
                                    <span class="img"><img src="<?php echo HTTP_SERVER; ?>/assets/images/icon/momo-icon.jpeg" alt=""></span>
                                    <span class="col">Thanh toán qua Momo</span>
                                </span>
                            </label>
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="payment_method" value="cod" />
                                <span class="d-flex align-items-center">
                                    <span class="img"><img src="<?php echo HTTP_SERVER; ?>/assets/images/icon/COD.svg" alt=""></span>
                                    <span class="col">COD<br />Thanh toán khi nhận hàng</span>
                                </span>
                            </label>
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="payment_method" value="zalopay" />
                                <span class="d-flex align-items-center">
                                    <span class="img"><img src="<?php echo HTTP_SERVER; ?>/assets/images/icon/logo-zalopay.svg" alt=""></span>
                                    <span class="col">Ví điện tử ZaloPay<br />Thẻ ATM/Thẻ tín dụng (Credit card) / Thẻ ghi nợ (Debit card)</span>
                                </span>
                            </label>
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="payment_method" value="vnpay" />
                                <span class="d-flex align-items-center">
                                    <span class="img"><img src="<?php echo HTTP_SERVER; ?>/assets/images/icon/vnpay.jpeg" alt=""></span>
                                    <span class="col">Ví điện tử VNPAY / VNPAY QR</span>
                                </span>
                            </label>
                        </div>
                        <div class="radio">
                            <label>
                                <input type="radio" name="payment_method" value="bank_transfer" />
                                <span class="d-flex align-items-center">
                                    <span class="img"><img src="<?php echo HTTP_SERVER; ?>/assets/images/icon/mceclip1_21.jpeg" alt=""></span>
                                    <span class="col">Chuyển khoản liên ngân hàng bằng QR Code<br />Chuyển tiền qua ví điện tử (MoMo, Zalopay,...)</span>
                                </span>
                            </label>
                        </div>
                    </form>
                </div>
            </div>
            <div class="column__right col-12 col-md-6">
                <div class="cart__column-right">
                    <div class="cart__shipping bg-white bg-frame mb-3">
                        <div class="block-header mb-2">
                            <strong class="title">Đơn hàng được giao tới</strong>
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
                        <a href="checkout.php" class="btn btn-primary d-block">Thanh toán</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<?php require_once 'template/footer.php'; ?>
</body>
</html>
