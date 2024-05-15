<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterLoad.ascx.cs" Inherits="Controls_FooterLoad" %>
<%@ Import Namespace="System.Data" %>
<script src="/themes/assets/js/slick/slick.min.js" type="text/javascript"></script>
<script src="/themes/assets/js/readmore/readmore.min.js" type="text/javascript"></script>
<script src="/themes/assets/js/menu.js" type="text/javascript"></script>
<script src="/themes/assets/js/qhg_script.js" type="text/javascript"></script>
<script src="/themes/assets/js/jquery/jquery.cookie.min.js"></script>
<script src="/themes/assets/js/control.js"></script>
<div class="div-ajax-loading">
    <div></div>
    <div></div>
    <div></div>
</div>
<div class="icon-bar">
<a target="_blank" rel="noopener noreferrer" href="#" class="float-shadow">
    <img src="/themes/images/face-icon.png" alt="Facebook" title="Facebook" width="48" height="48">
</a>
<a target="_blank" rel="noopener noreferrer" href="#" class="float-shadow">
    <img src="/themes/images/zalo-icon.png" width="48" height="48" alt="Zalo" title="Zalo">
</a>
<a target="_blank" rel="noopener noreferrer" href="https://www.tiktok.com/@dienmaygiakhangs" class="float-shadow">
    <img src="/themes/images/tiktok-icon.png" width="48" height="48" alt="youtube" title="youtube">
</a>
</div>
<script type="application/ld+json">{
"@context": "https://schema.org",
"@type": "LocalBusiness",

"url": "<%= C.ROOT_URL %>",
"logo": "<%= ConfigWeb.Logo %>",
"image": [
    "<%= C.ROOT_URL %><%= ConfigWeb.Image %>"
],
   "description": "<%= ConfigWeb.MetaDescription %>",
"name": "<%= ConfigWeb.SiteName %>",
   "telephone": "<%= ConfigWeb.SchemaTelephone %>",
"priceRange":"1$-100$",
"hasMap": "<%= ConfigWeb.MapLocation %>",
"email": "<%= ConfigWeb.Email %>",
  "address": {
    "@type": "PostalAddress",
    "streetAddress": "<%= ConfigWeb.SchemaStreetAddress %>",
    "addressLocality": "<%= ConfigWeb.SchemaAddressLocality %>",
    "postalCode": "<%= ConfigWeb.SchemaPostalCode %>",
    "addressCountry": "VN"
  },
  "geo": {
    "@type": "GeoCoordinates",
    "latitude": <%= ConfigWeb.SchemaLatitude %>,
    "longitude": <%= ConfigWeb.SchemaLongitude %>
  },
  "openingHoursSpecification": {
    "@type": "OpeningHoursSpecification",
    "dayOfWeek": [
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday",
      "Sunday"
    ],
    "opens": "00:00",
    "closes": "23:59"
  },
  "sameAs": <%= ConfigWeb.SchemaSameAs %>
    }
</script>
