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
        <div class="news__page">
            <div class="page__heading text-center">
                <h2 class="page__title"><span>Tin tức</span></h2>
            </div>
            <div class="news__content">
                <div class="news__grid d-grid">
                    <div class="news__item">
                        <a href="<?php echo HTTP_SERVER ;?>/news-detail.php" title="Cách các mẹ thông thái chọn Lego cho bé - Mẹ biết chưa ?" class="news__inner">
                            <div class="news__image">
                                <picture class="thumb">
                                    <img src="https://nhuabinhchanh.com/uploads/cache/images/tin-tuc/img-news-04-600x400.jpg" title="Cách các mẹ thông thái chọn Lego cho bé - Mẹ biết chưa ?" alt="Cách các mẹ thông thái chọn Lego cho bé - Mẹ biết chưa ?" width="600" height="400" />
                                </picture>
                            </div>
                            <div class="news__info">
                                <h3 class="news__title">Cách các mẹ thông thái chọn Lego cho bé - Mẹ biết chưa ?</h3>
                                <div class="news__des">Lego từ nhựa sạch thường không chứa các chất hóa học độc hại, giúp bảo vệ sức khỏe của trẻ trong quá...</div>
                            </div>
                            <div class="news__posted d-flex flex-column"><strong>11</strong><span>12-2022</span></div></a>
                        </div>
                        <div class="news__item">
                            <a href="<?php echo HTTP_SERVER ;?>/news-detail.php" title="Những điều cần biết khi bạn muốn thành công trong kinh doanh ngành hàng nước chấm - gia vị." class="news__inner">
                                <div class="news__image">
                                    <picture class="thumb">
                                        <img src="https://nhuabinhchanh.com/uploads/cache/images/tin-tuc/img-news-07-600x400.jpg" title="Những điều cần biết khi bạn muốn thành công trong kinh doanh ngành hàng nước chấm - gia vị." alt="Những điều cần biết khi bạn muốn thành công trong kinh doanh ngành hàng nước chấm - gia vị." width="600" height="400">
                                    </picture>
                                </div>
                                <div class="news__info">
                                    <h3 class="news__title">Những điều cần biết khi bạn muốn thành công trong kinh doanh ngành hàng nước chấm - gia vị.</h3>
                                    <div class="news__des">Đối với nhà sản xuất nước chấm – gia vị, việc lựa chọn nắp đậy cho chai sản phẩm có vai trò quan trọ...</div>
                                </div>
                                <div class="news__posted d-flex flex-column"><strong>04</strong><span>01-2020</span></div>
                            </a>
                        </div>
                        <div class="news__item">
                            <a href="<?php echo HTTP_SERVER ;?>/news-detail.php" title="Có nên mua rổ nhựa đa năng không cần khoan đục tường ?" class="news__inner">
                                <div class="news__image">
                                    <picture class="thumb">
                                        <img src="https://nhuabinhchanh.com/uploads/cache/images/tin-tuc/img-news-03-600x400.jpg" title="Có nên mua rổ nhựa đa năng không cần khoan đục tường ?" alt="Có nên mua rổ nhựa đa năng không cần khoan đục tường ?" width="600" height="400">
                                    </picture>
                                </div>
                                <div class="news__info">
                                    <h3 class="news__title">Có nên mua rổ nhựa đa năng không cần khoan đục tường ?</h3>
                                    <div class="news__des">Rổ nhựa đa năng không khoan, đục tường có nhiều ưu điểm mà người tiêu dùng có thể tận dụng, bao gồm:...</div>
                                </div>
                                <div class="news__posted d-flex flex-column"><strong>04</strong><span>01-2020</span></div>
                            </a>
                        </div>
                        <div class="news__item">
                            <a href="<?php echo HTTP_SERVER ;?>/news-detail.php" title="Sự khác biệt giữa một thương hiệu chọn muỗng nhựa giá rẻ và thương hiệu chọn muỗng nhựa sạch là gì?" class="news__inner"><div class="news__image">
                                <picture class="thumb">
                                    <img src="https://nhuabinhchanh.com/uploads/cache/images/tin-tuc/img-news-02-600x400.jpg" title="Sự khác biệt giữa một thương hiệu chọn muỗng nhựa giá rẻ và thương hiệu chọn muỗng nhựa sạch là gì?" alt="Sự khác biệt giữa một thương hiệu chọn muỗng nhựa giá rẻ và thương hiệu chọn muỗng nhựa sạch là gì?" width="600" height="400">
                                </picture>
                            </div>
                            <div class="news__info">
                                <h3 class="news__title">Sự khác biệt giữa một thương hiệu chọn muỗng nhựa giá rẻ và thương hiệu chọn muỗng nhựa sạch là gì?</h3>
                                <div class="news__des">Đầu tiên, về chất lượng nguyên liệu: Muỗng nhựa từ nguyên liệu sạch thường được làm từ nhựa không ch...</div>
                            </div>
                            <div class="news__posted d-flex flex-column"><strong>04</strong><span>01-2020</span></div>
                        </a>
                    </div>
                    <div class="news__item">
                        <a href="<?php echo HTTP_SERVER ;?>/news-detail.php" title="Sự lựa chọn muỗng nĩa được làm từ nguyên liệu nhựa sạch có thể giúp tăng giá trị thương hiệu một cách hiệu quả" class="news__inner">
                            <div class="news__image">
                                <picture class="thumb">
                                    <img src="https://nhuabinhchanh.com/uploads/cache/images/tin-tuc/img-news-01-600x400.jpg" title="Sự lựa chọn muỗng nĩa được làm từ nguyên liệu nhựa sạch có thể giúp tăng giá trị thương hiệu một cách hiệu quả" alt="Sự lựa chọn muỗng nĩa được làm từ nguyên liệu nhựa sạch có thể giúp tăng giá trị thương hiệu một cách hiệu quả" width="600" height="400">
                                </picture>
                            </div>
                            <div class="news__info">
                                <h3 class="news__title">Sự lựa chọn muỗng nĩa được làm từ nguyên liệu nhựa sạch có thể giúp tăng giá trị thương hiệu một cách hiệu quả</h3>
                                <div class="news__des">Sử dụng muỗng nhựa sạch thể hiện cam kết của quán ăn đối với an toàn thực phẩm và sức khỏe của khách...</div>
                            </div>
                            <div class="news__posted d-flex flex-column"><strong>04</strong><span>01-2020</span></div>
                        </a>
                    </div>
                </div>
                <div class="block-pagination">
                    <ul class="pagination justify-content-center">
                        <li class="page-item"><a class="page-link" href="#"><i class="icon-arrow-prev"></i></a></li>
                        <li class="page-item"><a class="page-link" href="#">1</a></li>
                        <li class="page-item"><a class="page-link" href="#">2</a></li>
                        <li class="page-item"><a class="page-link" href="#">3</a></li>
                        <li class="page-item"><a class="page-link" href="#"><i class="icon-arrow-next"></i></a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>
<?php require_once 'template/footer.php'; ?>
</body>
</html>
