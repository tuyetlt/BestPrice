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
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <nav aria-label="breadcrumb">
                    <ul class="breadcrumb mb-0">
                        <li class="breadcrumb-item"><a href="index.php">Trang chủ</a></li>
                        <li class="breadcrumb-item"><a href="product.php">Điện tử</a></li>
                        <li class="breadcrumb-item"><a href="product.php">Loa - Âm thanh</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Loa thanh LG SP2</li>
                    </ul>
                </nav>
            </div>
        </div>
        <div class="row">
            <div class="column__main col-12 col-sm-12">
                <div class="product__detail">
                    <div class="product__detail-top row mb-3">
                        <div class="product__columnLeft col-12 col-sm-6 col-md-4">
                            <div class="product__galleries">
                                <div class="product__galleries-main">
                                    <div class="product-image-gallery slider productMainSliders">
                                        <div class="img-thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </div>
                                        <div class="img-thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </div>
                                        <div class="img-thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </div>
                                        <div class="img-thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </div>
                                        <div class="img-thumb">
                                            <img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" title="Loa tháp Samsung MX-ST50B/XV" alt="Loa tháp Samsung MX-ST50B/XV" />
                                        </div>
                                    </div>
                                </div>
                                <div class="product__galleries-bottom">
                                    <div class="product__thumbnail">
                                        <div class="product__thumbnail-inner">
                                            <div class="slider product__galleries-thumb product-media-sliders">
                                                <div class="product__item">
                                                    <div class="product__image">
                                                        <div class="product__thumb"><img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" alt="Loa tháp Samsung MX-ST50B/XV" /></div>
                                                    </div>
                                                </div>
                                                <div class="product__item">
                                                    <div class="product__image">
                                                        <div class="product__thumb"><img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" alt="Loa tháp Samsung MX-ST50B/XV" /></div>
                                                    </div>
                                                </div>
                                                <div class="product__item">
                                                    <div class="product__image">
                                                        <div class="product__thumb"><img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" alt="Loa tháp Samsung MX-ST50B/XV" /></div>
                                                    </div>
                                                </div>
                                                <div class="product__item">
                                                    <div class="product__image">
                                                        <div class="product__thumb"><img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" alt="Loa tháp Samsung MX-ST50B/XV" /></div>
                                                    </div>
                                                </div>
                                                <div class="product__item">
                                                    <div class="product__image">
                                                        <div class="product__thumb"><img src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/MX-ST90BXV-LAYER-6-300x300.jpg" alt="Loa tháp Samsung MX-ST50B/XV" /></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Product Image Slider -->

                        <div class="product__columnRight col-12 col-sm-6 col-md-5">
                            <h2 class="page__title">Máy hút mùi SUNHOME 2721II thân inox chính hãng</h2>
                            <div class="product__brand mb-3">
                                <span>Hãng:</span> <a href="#">Đồng Phục Lan Hương</a>
                            </div>

                            <div class="product__price mb-3">
                                <strong class="price">2,135,000<span>đ</span></strong>
                                <div class="d-flex align-items-center">
                                    <div class="me-2">
                                        <span class="txt-price me-1">Giá thị trường:</span>
                                        <span class="old-price">3,050,000<span>đ</span></span>
                                    </div>
                                    <div><span class="txt-price me-1">Tiết kiệm:</span><strong>30%</strong></div>
                                </div>
                            </div>

                            <ul class="product__attr">
                                <li>Chế độ Group Play có dây và không dây.</li>
                                <li>Hiệu ứng đèn LED tối ưu Party Light+.</li>
                                <li>Điều khiển bằng ứng dụng di động.</li>
                                <li>Khả năng kháng nước chuẩn IPX5.</li>
                                <li>Xuất xứ: Việt Nam.</li>
                                <li>Thời gian bảo hành: 12 tháng chính hãng, có người đến tận nhà.</li>
                            </ul>

                            <div id="product" class="product__options">
                                <div class="product__action">
                                    <label class="control-label label-qty mb-2" for="input-quantity">Số lượng</label>
                                    <div class="product__action-addCart input-group mb-4">
                                        <div class="product__qty">
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
                                        <div class="buttons-set col">
                                            <button type="button" id="button-cart" data-loading-text="Loading..." class="btn btn-primary btn-add-cart d-block">Mua Ngay</button>
                                        </div>
                                    </div>
                                    <p>
                                        <a class="btn btn-outline-primary btn-lg d-block buy_payment" href="tel:0919535935">Gọi đặt mua: <strong>096 777 3668</strong> </a>
                                    </p>
                                    <ul class="product__commit list-unstyled row">
                                        <li class="col-12 col-sm-6">
                                            <div class="product__commit-img"><img src="<?php echo HTTP_SERVER; ?>/images/giao-hang-sieu-toc.png" alt="" /></div>
                                            <p><strong>Giao hàng siêu tốc</strong><span>Freeship đơn hàng trên 3 triệu</span></p>
                                        </li>
                                        <li class="col-12 col-sm-6">
                                            <div class="product__commit-img"><img src="<?php echo HTTP_SERVER; ?>/images/gia-tot-icon.png" alt="" /></div>
                                            <p><strong>Giá sỉ online siêu tốt</strong><span>Cam kết giá tốt nhất thị trường</span></p>
                                        </li>
                                        <li class="col-12 col-sm-6">
                                            <div class="product__commit-img"><img src="<?php echo HTTP_SERVER; ?>/images/chinh-hang-icon.png" alt="" /></div>
                                            <p><strong>Bảo hành chính hãng</strong><span>Đổi trả trong vòng 30 ngày</span></p>
                                        </li>
                                        <li class="col-12 col-sm-6">
                                            <div class="product__commit-img"><img src="<?php echo HTTP_SERVER; ?>/images/nhieu-hang-hoa.png" alt="" /></div>
                                            <p><strong>Kho hàng khắp miền nam</strong><span>Vận chuyển, lắp đặt nhanh chóng</span></p>
                                        </li>
                                    </ul>
                                </div>
                            </div> 
                        </div>

                        <div class="product__support col-12 col-sm-3 hidden-xs-sm">
                            <div class="product__support-inner mb-3">
                                <h3 class="product__support-title">CAM KẾT CHẤT LƯỢNG<br />ĐIỆN MÁY VŨNG TÀU</h3>
                                <ul class="list-unstyled">
                                    <li class="d-flex align-items-center">
                                        <span class="img-icon"><img src="<?php echo HTTP_SERVER; ?>/images/chinh-hang.png" alt="chính hãng 100%" /></span>
                                        <div>Hàng chính hãng, mới <strong>100%</strong></div>
                                    </li>
                                    <li class="d-flex align-items-center">
                                        <span class="img-icon"><img src="<?php echo HTTP_SERVER; ?>/images/thanh-toan-khi-nhan-hang.png" alt="thanh toán COD" /></span>
                                        <div>Lắp đặt và Thanh toán <strong>khi nhận hàng</strong> (COD)</div>
                                    </li>
                                    <li class="d-flex align-items-center">
                                        <span class="img-icon"><img src="<?php echo HTTP_SERVER; ?>/images/bao-hanh.png" alt="bảo hành nhanh chóng" /></span>
                                        <div>Đổi trả trong <strong>30 ngày</strong> (Nếu do lỗi kỹ thuật)</div>
                                    </li>
                                    <li class="d-flex align-items-center">
                                        <span class="img-icon"><img src="<?php echo HTTP_SERVER; ?>/images/doi-tra.png" alt="đổi trả" /></span>
                                        <div>Bảo hành và <a href="/bao-hanh-doi-tra/">đổi trả</a> tại nhà <strong>chỉ cần số điện thoại</strong></div>
                                    </li>
                                    <li class="d-flex align-items-center">
                                        <span class="img-icon"><img src="<?php echo HTTP_SERVER; ?>/images/giao-hang-sieu-toc.png" alt="giao hàng siêu tốc" /></span>
                                        <div>Dịch vụ giao hàng <strong>siêu tốc trong ngày</strong> <a href="#">GK-EXPRESS</a></div>
                                    </li>
                                </ul>
                            </div>

                            <div class="product__gift">
                                <h4 class="gift__title">Quà tặng đính kèm</h4>
                                <div class="product__gift-inner">
                                    <ul class="gift__list list-unstyled">
                                        <li class="gift__item">
                                            Tặng thêm vợt muỗi <a href="#" class="txt-link">YAGE YG-D003</a> áp dụng tất cả đơn hàng tủ lạnh trên 10 triệu.
                                        </li>
                                        <li class="gift__item">
                                            Giá khuyến mãi áp dụng từ <span class="txt-link">15/05/2023</span> đến hết <span class="txt-link">21/05/2023.</span>
                                        </li>
                                        <li class="gift__item">
                                            Giao <strong>siêu tốc trong ngày</strong> Giao siêu tốc trong ngày cho đơn hàng từ 3 triệu trong phạm vi 10km.
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <hr class="hr-line mb-5" />

                    <div id="description" class="product__description mb-5">
                        <div class="productReadMore readMore">
                            <h5 class="uppercase mt">THÔNG SỐ KỸ THUẬT</h5>
                            <div class="table-responsive mb-4">
                                <table class="table">
                                    <tbody>
                                        <tr>
                                            <td>Model:</td>
                                            <td width="159">MX-ST50B/XV</td>
                                        </tr>
                                        <tr>
                                            <td>Nhà sản xuất:</td>
                                            <td>Samsung</td>
                                        </tr>
                                        <tr>
                                            <td>Xuất xứ:</td>
                                            <td>Việt Nam</td>
                                        </tr>
                                        <tr>
                                            <td>Thời gian bảo hành:</td>
                                            <td>12 Tháng</td>
                                        </tr>
                                        <tr>
                                            <td>Địa điểm bảo hành:</td>
                                            <td>Toàn quốc</td>
                                        </tr>
                                        <tr>
                                            <td>Công suất loa:</td>
                                            <td>240W</td>
                                        </tr>
                                        <tr>
                                            <td>Cổng USB(Amply):</td>
                                            <td>Có</td>
                                        </tr>
                                        <tr>
                                            <td>Cổng Bluetooth:</td>
                                            <td>Có</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33.3952%;">Kích thước mạng (Loa chính) (WxHxD):</td>
                                            <td style="width: 259.373%;">329,0 x 665,0 x 301,0 mm</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33.3952%;">Trọng lượng:</td>
                                            <td style="width: 259.373%;">11.9Kg</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33.3952%;">Tiêu thụ nguồn ở chế độ chờ:</td>
                                            <td style="width: 259.373%;">&lt;0,5W</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                            <div class="description">
                                <h3 class="feature-benefit-full-bleed__title" data-font-size-pc="48" data-font-size-mo="30">Giai điệu cuồng nhiệt tại mọi bữa tiệc</h3>
                                <p class="feature-benefit-full-bleed__text" data-font-size-pc="18" data-font-size-mo="12">Đắm chìm trong không gian tiệc tùng tràn ngập âm nhạc với âm thanh đa hướng công suất 160W mạnh mẽ</p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><img width="1440" height="740" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344.jpg" alt="5 people are conversing with each other with Sound Tower in the middle." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344.jpg" data-ll-status="loaded" class="entered lazyloaded"><noscript><img width="1440" height="740" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344.jpg" alt="5 people are conversing with each other with Sound Tower in the middle." /></noscript></p>
                                <h3 data-font-size-pc="18" data-font-size-mo="12"><strong class="feature-benefit-full-bleed__sub-title" data-font-size-pc="28" data-font-size-mo="16">Công nghệ âm thanh Bi-directional</strong></h3>
                                <p class="feature-benefit-full-bleed__text" data-font-size-pc="18" data-font-size-mo="12">Bùng nổ hết mình với những giai điệu mạnh mẽ lan toả, lấp đầy mọi không gian rộng lớn nhờ thiết kế đa hướng độc đáo từ loa tháp MX-ST40B</p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><img width="1440" height="808" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-1.jpg" alt="7 people are at an outdoor area having a party with a Sound Tower in the middle, which is sending out sound bi-directionally." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-1.jpg"><noscript><img width="1440" height="808" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-1.jpg" alt="7 people are at an outdoor area having a party with a Sound Tower in the middle, which is sending out sound bi-directionally." /></noscript></p>
                                <h3 class="feature-benefit-full-bleed__title" data-font-size-pc="48" data-font-size-mo="30">Giai điệu cuồng nhiệt tại mọi bữa tiệc c<strong class="feature-benefit-full-bleed__sub-title" data-font-size-pc="28" data-font-size-mo="16">ông suất 160W</strong></h3>
                                <p class="feature-benefit-full-bleed__text" data-font-size-pc="18" data-font-size-mo="12">Khuấy động mọi cuộc vui với loa tháp Samsung MX-ST40B mang đến âm thanh đa hướng và công suất đến 160W cực sôi động.</p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><img width="1440" height="826" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-2.jpg" alt="A bird's eye view of a Sound Tower with Total RMS 240 watt logo is on display." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-2.jpg"><noscript><img width="1440" height="826" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-2.jpg" alt="A bird's eye view of a Sound Tower with Total RMS 240 watt logo is on display." /></noscript></p>
                                <h3 class="feature-benefit-full-bleed__title" data-font-size-pc="48" data-font-size-mo="30"><strong class="feature-benefit-full-bleed__sub-title" data-font-size-pc="28" data-font-size-mo="16">Pin tích hợp m</strong>ang âm nhạc đến mọi nơi</h3>
                                <p class="feature-benefit-full-bleed__text" data-font-size-pc="18" data-font-size-mo="12">Thời lượng pin lên đến 18 giờ cho bạn tận hưởng bữa tiệc ở bất cứ đâu bạn đến.</p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><img width="1440" height="814" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-3.jpg" alt="People are having a party on the rooftop with Sound Tower. Built-in battery signifies Sound Tower can last for 18 hours." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-3.jpg"><noscript><img width="1440" height="814" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-3.jpg" alt="People are having a party on the rooftop with Sound Tower. Built-in battery signifies Sound Tower can last for 18 hours." /></noscript></p>
                                <h3 class="feature-benefit-full-bleed__title" data-font-size-pc="48" data-font-size-mo="30">Khả năng chống nước IPX5</h3>
                                <p class="feature-benefit-full-bleed__text" data-font-size-pc="18" data-font-size-mo="12">Với xếp hạng IPX5, bạn có thể sử dụng Loa tháp trong các điều kiện thời tiết khác nhau không lo bắn nước.</p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><span class="feature-benefit-full-bleed__disclaimer">* Bảo vệ khỏi điều kiện nước dựa trên tiêu chuẩn IPX5. </span></p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><span class="feature-benefit-full-bleed__disclaimer">* Có thể chống lại tia nước áp suất thấp, bền vững, được chiếu bằng vòi phun (6,3 mm (0,25 in)) chống lại bao vây từ bất kỳ hướng nào.</span></p>
                                <p data-font-size-pc="18" data-font-size-mo="12"><img width="1440" height="702" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-4.jpg" alt="A Sound Tower is on display with water splashed over it to show it is water-resistant." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-4.jpg"><noscript><img width="1440" height="702" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-4.jpg" alt="A Sound Tower is on display with water splashed over it to show it is water-resistant." /></noscript></p>
                                <h3 class="feature-benefit__title contrast-target" data-font-size-mo="30" data-font-size-pc="48">Hoà nhịp cùng buổi tiệc ánh sáng nhờ tính năng Party Lights+</h3>
                                <div class="feature-benefit__desc-wrap">
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Tận hưởng không khí bữa tiệc sôi động với tính năng điều chỉnh ánh sáng đa dạng và phù hợp với nhiều âm điệu khác nhau như Party, Ambient, Dance, Thunder Bolt, Star và Lover. Dễ dàng điều chỉnh chế độ ánh sáng trên loa tháp MX-ST40B với ứng dụng chuyên biệt trên thiết bị di động của bạn.</p>
                                <p data-font-size-mo="12" data-font-size-pc="18"><img width="720" height="540" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-5.jpg" alt="Sound Tower is beaming out blue light to create a party mood. Colorful lights are surrounding the Sound Tower." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-5.jpg"><noscript><img width="720" height="540" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-5.jpg" alt="Sound Tower is beaming out blue light to create a party mood. Colorful lights are surrounding the Sound Tower." /></noscript></p>
                                <h3 class="feature-benefit__title contrast-target" data-font-size-mo="30" data-font-size-pc="48">Tính năng Group Play</h3>
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Tính năng Group Play giúp kết nối nhiều loa tháp Samsung với nhau cho bữa tiệc thêm phần sôi động và cuồng nhiệt.</p>
                                <p class="feature-benefit__disclaimer contrast-target">* Kết nối lên đến 10 (khả năng tương thích không dây / có dây giữa MX-ST * và kiểu MX-T). * Trong kết nối không dây, chất lượng phát lại có thể không ổn định tùy thuộc vào môi trường blutooth.</p>
                                <p><img width="720" height="540" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-6.jpg" alt="4 Sound Towers are placed in the midst of a pool party with many people." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-6.jpg"><noscript><img width="720" height="540" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-6.jpg" alt="4 Sound Towers are placed in the midst of a pool party with many people." /></noscript></p>
                                <h3 class="feature-benefit__title contrast-target" data-font-size-mo="30" data-font-size-pc="48">Kết nối bất tận với Bluetooth</h3>
                                <div class="feature-benefit__desc-wrap">
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Thêm chia sẻ, thêm niềm vui, Kết Nối Đa Năng Bluetooth® ghép nối loa với 2 thiết bị di động cùng một lúc, để bạn dễ dàng tận hưởng âm nhạc cùng bạn bè.</p>
                                <p data-font-size-mo="12" data-font-size-pc="18"><img width="720" height="800" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-7.jpg" alt="2 people are pairing their phones via Sound Tower's Bluetooth Multi Connection function. A Sound Tower is on display." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-7.jpg"><noscript><img width="720" height="800" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-7.jpg" alt="2 people are pairing their phones via Sound Tower's Bluetooth Multi Connection function. A Sound Tower is on display." /></noscript></p>
                                <h3 class="feature-benefit__sub-title contrast-target" data-font-size-mo="16" data-font-size-pc="28">Ứng dụng Sound Tower</h3>
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Để bắt đầu bữa tiệc bạn cần một DJ thực thụ khuấy động không khí. Ứng dụng Sound Tower sẽ giúp bạn dễ dàng kiểm soát nhiều hiệu ứng ánh sáng và âm thanh, thậm chí cả cài đặt EQ ngay trên điện thoại.</p>
                                <p class="feature-benefit__disclaimer contrast-target">* Ứng dụng Samsung Sound Tower tương thích với các thiết bị điện thoại di động Android chạy hệ điều hành Android 6.0 trở lên. * Ứng dụng Samsung Sound Tower tương thích với các thiết bị di động của Apple chạy iOS 10.0 trở lên. * Hình thức, thiết kế và GUI (Giao diện người dùng đồ họa) các thông số kỹ thuật không ảnh hưởng đến hiệu suất sản phẩm có thể thay đổi mà không cần thông báo. * Ngày khởi chạy ứng dụng TBD</p>
                                <p><img width="720" height="680" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-8.jpg" alt="The Sound Tower app UI is on display in 3 smartphone screens." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-8.jpg"><noscript><img width="720" height="680" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-8.jpg" alt="The Sound Tower app UI is on display in 3 smartphone screens." /></noscript></p>
                                <h3 class="feature-benefit__sub-title contrast-target" data-font-size-mo="16" data-font-size-pc="28">Chế độ Karaoke</h3>
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Chế độ Karaoke giúp bạn khoe trọn chất giọng trong mỗi bài hát. Đơn giản cầm mic lên và tận hưởng bữa tiệc âm nhạc đầy sống động theo cách của bạn.</p>
                                <p data-font-size-mo="12" data-font-size-pc="18"><img width="720" height="800" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-9.jpg" alt="A woman is singing with the Sound Tower on Karake Mode with many people surrounding the singer." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-9.jpg"><noscript><img width="720" height="800" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-9.jpg" alt="A woman is singing with the Sound Tower on Karake Mode with many people surrounding the singer." /></noscript></p>
                                <h3 class="feature-benefit__title contrast-target" data-font-size-mo="30" data-font-size-pc="48">Tận hưởng playlist riêng với tích hợp cổng USB</h3>
                                <div class="feature-benefit__desc-wrap">
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Với cổng USB tích hợp, bạn có thể kết nối thiết bị khác với loa để chơi nhạc với danh sách của riêng mình.</p>
                                <p data-font-size-mo="12" data-font-size-pc="18"><img width="720" height="790" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-10.jpg" alt="A closeup of Sound Tower's USB port is on display as the back of the Sound Tower is shown." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-10.jpg"><noscript><img width="720" height="790" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-10.jpg" alt="A closeup of Sound Tower's USB port is on display as the back of the Sound Tower is shown." /></noscript></p>
                                <h3 class="feature-benefit__title contrast-target" data-font-size-mo="30" data-font-size-pc="48">Kết nối bất tận với Bluetooth</h3>
                                <div class="feature-benefit__desc-wrap">
                                <p class="feature-benefit__desc contrast-target" data-font-size-mo="12" data-font-size-pc="18">Kết nối Loa Tháp với TV của bạn qua Bluetooth và thưởng thức âm nhạc yêu thích mà không cần dây cáp rườm rà.</p>
                                <p data-font-size-mo="12" data-font-size-pc="18"><img width="720" height="660" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-11.jpg" alt="People are watching a soccer game with Sound Tower connected to the TV via Bluetooth." data-lazy-src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-11.jpg"><noscript><img width="720" height="660" src="https://dienmaygiakhang.vn/wp-content/uploads/2022/06/loa-thap-samsung-mx-st90b-xv-69344-11.jpg" alt="People are watching a soccer game with Sound Tower connected to the TV via Bluetooth." /></noscript></p>
                                </div>
                                </div>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="product__related mb-5">
                        <?php require_once 'template/product-related.php'; ?>
                    </div>

                    <?php require_once 'template/review.php'; ?>
                </div>
            </div>
        </div>
    </div>
</div><!-- End main -->
<?php require_once 'template/footer.php'; ?>
<!-- Modal -->
<div class="modal fade" id="modaReview" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content">
            <div class="modal-body">
                <div class="modal-heading d-flex align-items-center">
                    <h3 class="modal-title" id="staticBackdropLabel">Đánh giá Loa tháp Samsung MX-ST50B/XV</h3>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body__inner">
                    <div class="form-group comment-form-comment">
                        <textarea rows="5" id="comment" name="comment" placeholder="Mời bạn chia sẻ thêm một số cảm nhận..." class="form-control"></textarea>
                    </div>
                    <div class="form-group wrap-attaddsend">
                        <div class="d-flex align-items-center">
                            <div class="review-attach">
                                <span class="btn-attach devvn_insert_attach">Gửi ảnh thực tế</span>
                            </div>
                            <span id="countContent" class="ms-auto">0 ký tự (Tối thiểu 100)</span>
                        </div>
                    </div>
                    <div class="form-group form_row_reviews row">
                        <div class="col-6 comment-form-author">
                            <input id="author" name="author" type="text" value="" placeholder="Họ tên (bắt buộc)" class="form-control" />
                        </div>
                        <div class="col-6 comment-form-email">
                            <input id="email" name="email" type="email" value="" placeholder="Email" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="buttons text-center mt-4 mb-3">
                    <button type="button" class="btn btn-primary">Gửi đánh giá</button>
                </div>
                <div class="note_review"><u>Lưu ý:</u> để đánh giá được phê duyệt, vui lòng tham khảo <a href="#" title="" target="_blank" class="txt-link">Điều khoản và Điều kiện</a></div>
            </div>
        </div>
    </div>
</div>
<!-- End modal -->
</body>
</html>