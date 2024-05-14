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
<script src="<?php echo HTTP_SERVER; ?>/assets/js/jquery/jquery-3.7.1.min.js"></script>
<script src="<?php echo HTTP_SERVER; ?>/assets/js/bootstrap/popper.min.js"></script>
<script src="<?php echo HTTP_SERVER; ?>/assets/js/bootstrap/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
<?php require_once 'template/header.php'; ?>
<?php require_once 'template/menu.php'; ?>
<div class="main">
  <!-- Slideshow -->
  <section class="banner banner__slideshow mb-4">
    <div class="banner__section">
      <div class="banner__section-inner">
        <div class="slider createBannerSliders">
          <div class="banner__item">
            <div class="banner__item-inner">
              <img src="<?php echo HTTP_SERVER; ?>/images/banner-01.jpg" alt="Banner title" />
            </div>
          </div>
          <div class="banner__item">
            <div class="banner__item-inner">
              <img src="<?php echo HTTP_SERVER; ?>/images/banner-02.jpeg" alt="Banner title" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!-- End slideshow -->
  <!-- Block html -->
  <section class="banner banner__advert mb-4">
    <div class="banner__advert-inner container">
      <div class="row">
        <div class="col-12 col-sm-4 mb-3 mb-sm-0">
          <a href="#" class="banner__image"><img src="<?php echo HTTP_SERVER; ?>/images/HEAD-2.png" alt=""></a>
        </div>
        <div class="col-12 col-sm-4 mb-3 mb-sm-0">
          <a href="#" class="banner__image"><img src="<?php echo HTTP_SERVER; ?>/images/HEAD-3.png" alt=""></a>
        </div>
        <div class="col-12 col-sm-4 mb-3 mb-sm-0">
          <a href="#" class="banner__image"><img src="<?php echo HTTP_SERVER; ?>/images/HEAD-5.png" alt=""></a>
        </div>
      </div>
    </div>
  </section>
  <!-- End block html -->
  <!-- Product Special -->
  <?php require_once 'template/product-special.php'; ?>
  <!-- End product special -->
  <!-- Product latest -->
  <?php require_once 'template/product-latest.php'; ?>
  <!-- End product latest -->
  <!-- Product Advert -->
  <?php require_once 'template/product-advert.php'; ?>
  <?php require_once 'template/product-advert-2.php'; ?>
  <!-- End Product Advert -->
  <!-- Product Bestseller -->
  <?php require_once 'template/product-bestseller.php'; ?>
  <!-- End product bestseller -->
  <!-- Product Houseware -->
  <?php require_once 'template/product-houseware.php'; ?>
  <!-- End product Houseware -->
  <!-- Category List -->
  <?php require_once 'template/category-menu.php'; ?>
  <!-- End categories -->
  <?php require_once 'template/news-event.php'; ?>
  <?php require_once 'template/banner.php'; ?>
  <?php require_once 'template/testimonial.php'; ?>
</div><!-- End main -->
<?php require_once 'template/footer.php'; ?>
</body>
</html>