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
<link href="<?php echo HTTP_SERVER; ?>/assets/css/font-awesome.min.css" rel="stylesheet" media="screen" />
<script src="<?php echo HTTP_SERVER; ?>/assets/js/jquery/jquery-3.7.1.min.js" type="text/javascript"></script>
<script src="<?php echo HTTP_SERVER; ?>/assets/js/bootstrap/popper.min.js"></script>
<script src="<?php echo HTTP_SERVER; ?>/assets/js/bootstrap/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<?php require_once 'template/header.php'; ?>
<?php require_once 'template/menu.php'; ?>
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-md-flex align-items-end">
            <div class="col mb-3 mb-md-0">
                <h1 class="page__title">Loa - Âm thanh</h1>
                <nav aria-label="breadcrumb">
                    <ul class="breadcrumb mb-0">
                        <li class="breadcrumb-item"><a href="#">Trang chủ</a></li>
                        <li class="breadcrumb-item"><a href="#">Điện tử</a></li>
                        <li class="breadcrumb-item active" aria-current="page">Loa - Âm thanh</li>
                    </ul>
                </nav>
            </div>
            <div class="col">
                <div class="d-flex justify-content-between justify-content-end align-items-center">
                    <button type="button" class="btn btn-primary btn-sidebar-filter hidden-md"><i class="fa fa-filter"></i> LỌC</button>
                    <span class="me-3 hidden-xs-sm">Hiển thị 1–24 của 129 kết quả</span>
                    <form class="ms-sm-auto" method="get">
                        <select name="orderby" class="form-select" aria-label="Đơn hàng của cửa hàng">
                            <option value="date" selected="selected">Mới nhất</option>
                            <option value="rating">Điểm đánh giá</option>
                            <option value="popularity">Mức độ phổ biến</option>
                            <option value="price">Giá từ thấp đến cao</option>
                            <option value="price-desc">Giá từ cao xuống thấp</option>
                        </select>
                    </form>
                </div>
            </div>
        </div>
        <div class="row two-column">
            <div class="column__left col-12 col-md-3">
                <?php require_once 'template/filter.php'; ?>
            </div>
            <div class="column__main col-12 col-md-9">
                <div class="product__wrapper">
                    <div class="product__grid row mb-5">
                        <?php require "template/product-item-list.php"; ?>
                    </div>
                    <div class="pagination__wrapper">
                        <ul class="pagination justify-content-center">
                            <li class="page-item"><a class="page-link" href="#"><i class="icon-angle-left"></i></a></li>
                            <li class="page-item"><a class="page-link" href="#">1</a></li>
                            <li class="page-item"><a class="page-link" href="#">2</a></li>
                            <li class="page-item"><a class="page-link" href="#">3</a></li>
                            <li class="page-item"><a class="page-link" href="#"><i class="icon-angle-right"></i></a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div><!-- End main -->
<?php require_once 'template/footer.php'; ?>
</body>
</html>