$(document).ready(function () {
    var arr_scroll = [];
    //search header
    /* Header Search */
    $('.header__search input[name="search"]').on('keydown', function (e) {
        if (e.keyCode == 13) {
            $('.header__search .btn-top-search').trigger('click');
        }
    });

    $('.btn-top-search').on('click', function () {
        let url = '/tim-kiem/';
        let value = $('.header__search input[name="key"]').val();
        if (value != '') {
            url += '?key=' + encodeURIComponent(value);
        }
        window.location = url;
    });

    $("#searchbox").focusin(function (e) {
        e.stopPropagation();
       /* $(this).parent().parent().dropdown('toggle');*/
        $('.header__search  .dropdown-menu').show();
    });

   /* $('#searchbox').on('click', function (e) {
        e.stopPropagation();
        $(this).parent().parents('.dropdown').find('[data-bs-toggle="dropdown"]').dropdown('toggle');
    });*/
    /* End Header Search */
    $('header .search').on('click', function () {
        $('header .header__right').addClass('search_ac');
        $('header .over-lay').css({
            display: 'block',
        });
    });

    $('header .search input').on('keyup', function () {
        /*$('.show_search_content').fadeIn();*/

        if ($('header .search input').val() == '') {
            $('.show_search_content').fadeOut();
            $(".over-lay-search").hide();

        }
        return false;
    });

    $('.sticky_mobi input').on('keyup', function () {
        $('.search_toggle_show').addClass('ac');

        return false;
    });

    $('.main').on('click', function () {
        $('.show_search_content').hide();
        $(".over-lay-search").hide();
        $('.dropdown-menu').hide();
        $(".loading-search").hide();
        $('.search_toggle_show').removeClass('ac');
        $('header .over-lay').css({
            display: 'none',
        });
    });
    $('.menu-header').on('click', function () {
        $('header').removeClass('search_ac');
        $('.show_search_content').hide();
        $(".over-lay-search").hide();
        $(".loading-search").hide();
        $('.search_toggle_show').removeClass('ac');
        $('header .over-lay').css({
            display: 'none',
        });
    });
    $('header .over-lay').on('click', function () {
        $('header').removeClass('search_ac');
        $('.show_search_content').hide();
        $(".over-lay-search").hide();
        $(".loading-search").hide();
        $('.search_toggle_show').removeClass('ac');
        $('header .over-lay').css({
            display: 'none',
        });
    });

    $('header .over-lay-search').on('click', function () {
        $('header').removeClass('search_ac');
        $('.show_search_content').hide();
        $(".over-lay-search").hide();
        $(".loading-search").hide();
        $('.dropdown-menu').hide();
        $('.search_toggle_show').removeClass('ac');
        $('header .over-lay').css({
            display: 'none',
        });
    });
    $('.nav-link.down').on('click', function () {
        var dataid = $(this).attr("data-id");
        var sub = $(".sub" + dataid);
        sub.slideToggle();
    });

    $('.category-item.down').on('click', function () {
        var dataid = $(this).attr("data-id");
        var sub = $(".sub2_" + dataid);
        sub.slideToggle();
    });

    //menu mobi
    //$('.l1').on('click', function () {
    //    var tag = $(this).attr('value');
    //    var tag1 = $(this).text();
    //    // window.alert("#layer"+tag1);
    //    var back_link = "#layer" + tag;
    //    //window.alert(back_link);
    //    $('.nav-link').attr('href', back_link);
    //    //$('.nav-link').text(tag1);
    //    $('.nav-link').attr('value', tag);
    //    $("#layer" + tag).removeClass('hide-menu');
    //    $("#layer" + tag).toggleClass('show-menu');
    //});
    //$('.nav-link').on('click', function () {
    //    var tag = $(this).attr('href');
    //    var val = $(this).attr('value');
    //    // window.alert(val);
    //    $(tag).removeClass('show-menu');
    //    var back_link = "#layer" + (val - 1);
    //    $('.nav-link').attr('href', back_link);
    //    $('.nav-link').attr('value', val - 1);
    //    //window.alert(back_link);

    //});

    $('.close_mobi').on('click', function () {
        $('.menu_mobi').hide();
    });

    $('.toggle_menu').on('click', function () {
        $('.menu_mobi').show();
        //alert("ok");
        //$(".navbar-toggler-mobile").animate({ width: 'toggle' }, 350);
    });

    $('.video_show_iframe .close_iframe').on('click', function () {
        $('.video_show_iframe').removeClass('ac');
    });

    $('.video .item').on('click', function () {
        let url = $(this).attr('data-url');
        $('.video_show_iframe .insider iframe').attr('src', url);
        $('.video_show_iframe').addClass('ac');
    });

    $('.video .right').on('click', function () {
        let url = $(this).attr('data-url');
        $('.video_show_iframe .insider iframe').attr('src', url);
        $('.video_show_iframe').addClass('ac');
    });



    /*hotline*/
    //$('.hotline-phone-ring-wrap').on('click', function () {
    //    $('.hotline_sup').show();
    //    return false;
    //});
    //$('.hotline_sup .close').on('click', function () {
    //    $('.hotline_sup').hide();
    //});
    //$('.hotline_sup .close_bg').on('click', function () {
    //    $('.hotline_sup').hide();
    //});

    $(document).scroll(function () {
        let scroll = $(document).scrollTop();
        let position = 0;
        if ($('.section2').length !== 0) {
            position = $('.section2').offset().top;
        }

        if (scroll >= position) {
            $('.sticky').slideDown();
        } else {
            $('.sticky').slideUp();
        }
    });

    var arr_scroll = [];

    $('.toggle_search').on('click', function () {
        $('.search_toggle_show').show();
        $(".over-lay").css({
            display: 'block',
            bottom: 40,
        })
        $("#searchbox").select();
    });

    $(".over-lay").click(function () {
        $('.search_toggle_show').css({
            display: 'none',
        })
    })

    $("main").click(function () {
        $('.search_toggle_show').css({
            display: 'none',
        })
    })

    $(".backtop").click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 1000);
    });

    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();

            document.querySelector(this.getAttribute('href')).scrollIntoView({
                behavior: 'smooth'
            });
        });
    });

    if ($("#searchbox").length) {
        window.onload = () => {
            function ajax(data) {
                console.log(new Date().toLocaleTimeString() + ' - ' + data)

                $.ajax({
                    url: "/ajax/ajax.aspx",
                    data: {
                        control: "search",
                        key: data
                    },
                    type: "POST",
                    success: function (html) {
                        $(".loading-search").hide();
                        //alert(html.length);

                        if (html.length > 10) {
                            $('.show_search_content').show();
                            $(".dropdown .dropdown-menu").hide();
                            $(".show_search_content").html(html);
                        }
                        else {
                            $('.show_search_content').hide();
                            $(".dropdown .dropdown-menu").show();
                        }
                    }
                });


            }

            function debounce(fn, delay) {
                return args => {
                    clearTimeout(fn.id)

                    fn.id = setTimeout(() => {
                        fn.call(this, args)
                    }, delay)
                }
            }

            const debounceAjax = debounce(ajax, 100);

            document.querySelector('#searchbox').addEventListener('keyup', e => {
                //alert("ok");
                $(".loading-search").show();

                $(".over-lay-search").show();
                debounceAjax(e.target.value)
            })
        }
    }
    $(".carousel-banner").slick({
        infinite: true,
        speed: 700,
        autoplay: true,
        autoplaySpeed: 1300,
        dots: false,
    });
    var showFilter = false;
    var showSort = false;
    $(".btn-filter").click(function () {
        if (showFilter) {
            $(".content-filter").css({
                visibility: "hidden",
                opacity: 0,
            });
            showFilter = false;
        } else {
            $(".content-filter").css({
                visibility: "inherit",
                opacity: 1,
            });
            showFilter = true;
        }
    });
    $(".content-filter").mouseleave(function () {
        $(".content-filter").css({
            visibility: "hidden",
            opacity: 0,
        });
        showFilter = false;
    });
    $(".filter-item .exit").click(function () {
        if (showFilter) {
            $(".content-filter").css({
                visibility: "hidden",
                opacity: 0,
            });
            showFilter = false;
        } else {
            $(".content-filter").css({
                visibility: "inherit",
                opacity: 1,
            });
            showFilter = true;
        }
    });

    $(".btn-sort").click(function () {
        if (showSort) {
            $(".content-sort").css({
                visibility: "hidden",
                opacity: 0,
            });
            showSort = false;
        } else {
            $(".content-sort").css({
                visibility: "inherit",
                opacity: 1,
            });
            showSort = true;
        }
    });
    $(".content-sort").mouseleave(function () {
        $(".content-sort").css({
            visibility: "hidden",
            opacity: 0,
        });
        showSort = false;
    });
    $(".sort-item .exit").click(function () {
        if (showSort) {
            $(".content-sort").css({
                visibility: "hidden",
                opacity: 0,
            });
            showSort = false;
        } else {
            $(".content-sort").css({
                visibility: "inherit",
                opacity: 1,
            });
            showSort = true;
        }
    });


    var showCategory = false;
    $(".category-box .see-more-category").click(function () {
        if (showCategory) {
            $(".category-item.category-item-none").css({
                display: "none",
            });
            $(".category-box .see-more-category").html("<p>Xem thêm</p>");
            showCategory = false;
        } else {
            $(".category-item.category-item-none").css({
                display: "block",
            });
            $(".category-box .see-more-category").html("<p>Thu gọn</p>");
            showCategory = true;
        }
    });

    var showBrand = false;
    $(".brand-option .see-more-brand").click(function () {
        if (showBrand) {
            $(".brand-item.brand-item-none").css({
                display: "none",
            });
            $(".brand-option .see-more-brand").html("<p>Xem thêm</p>");
            showBrand = false;
        } else {
            $(".brand-item.brand-item-none").css({
                display: "block",
            });
            $(".brand-option .see-more-brand").html("<p>Thu gọn</p>");
            showBrand = true;
        }
    });


    if ($(".floor-menu-fixed").length) {
        $(".floor-menu-fixed").offset().top;
        vt = $(".section_blog").offset().top;

        //vt = 9000;

        window.addEventListener("scroll", function () {
            console.log(vt);
            vtmenu = $(".menu-header").offset().top + 50;
            if (
                $(".floor-menu-fixed").offset().top > 500 &&
                $(".floor-menu-fixed").offset().top < vt
            ) {
                $(".floor-menu-fixed").css({
                    visibility: "initial",
                    opacity: 1,
                });
            } else {
                $(".floor-menu-fixed").css({
                    visibility: "hide",
                    opacity: 0,
                });
            }
            $(".main section").each(function (index, value) {
                if (index !== 0) {
                    if (
                        vtmenu > $(".main section")[index].offsetTop
                    ) {
                        $(".floor-menu-fixed li").each(function (key, value) {
                            $(this).removeClass("ac");
                            if (key === index) {
                                $(this).addClass("ac");
                            }
                        });
                    }
                }
                //if (index === 6) {
                //    if (vtmenu > $(".main section")[index].offsetTop) {
                //        $(".floor-menu-fixed li").each(function (key, value) {
                //            $(this).removeClass("ac");
                //            if (key === index) {
                //                $(this).addClass("ac");
                //            }
                //        });
                //    }
                //}
            });
        });
    }

    $(".carousel-img-product").slick({
        dots: true,
        infinite: true,
        nextArrow: '<i class="slick-button-prev"></i>',
        prevArrow: '<i class="slick-button-next"></i>'
    });
    $("button.owl-prev").each(function (index, value) {
        $(this).html('<i class="button-prev"></i>');
    });
    $("button.owl-next").each(function (index, value) {
        $(this).html('<i class="button-next"></i>');
    });
    $(".carousel-Ad").slick({
        infinite: true,
        speed: 700,
        autoplay: true,
        autoplaySpeed: 1300,
        dots: true,
    });
    $(".carousel-banner").slick({
        infinite: true,
        speed: 700,
        autoplay: true,
        autoplaySpeed: 1300,
        dots: true,
    });

    $(".tab-a").click(function () {
        $(".tab").removeClass("tab-active");
        $(".tab[data-id='" + $(this).attr("data-id") + "']").addClass("tab-active");
        $(".tab-a").removeClass("active-a");
        $(this).parent().find(".tab-a").addClass("active-a");
    });

    function seeMore() {
        var dots = document.getElementById("dots");
        var moreText = document.getElementById("more");
        var btnText = document.getElementById("see-more-btn");

        if (dots.style.display === "none") {
            dots.style.display = "inline";
            btnText.innerHTML = "Xem thêm";
            moreText.style.display = "none";
        } else {
            dots.style.display = "none";
            btnText.innerHTML = "Ẩn đi";
            moreText.style.display = "inline";
        }
    }


    //var i,
    //    $mvar = $(".box-detail h3");

    //function genmenu(string, index) {
    //    $(".table-of-contents").show();
    //    var text = document.createTextNode(string);
    //    $(".table-ct .content-list").append(
    //        "<li class='content-item'><span class='list-style'></span><a class='content-link' href='' name='mucluc" +
    //        index +
    //        "'>" +
    //        string +
    //        "</a></li>"
    //    );
    //}

    //for (i = 0; i < $mvar.length; i++) {
    //    genmenu($mvar.eq(i).html(), i);
    //    $mvar.eq(i).attr("name", "mucluc" + i);
    //}

    ////genmenu($mvar.length);

    //if ($mvar.length > 0) {
    //    $(".table-ct").css("display", "inline-block");
    //}



    function scrollToAnchor(aid) {
        var aTag = $(".box-detail h3[name='" + aid + "']");
        $("html,body").animate({
            scrollTop: aTag.offset().top
        }, "slow");
    }

    $(".content-link").click(function () {
        if ($(".lessLink").length) { } else {
            $("#see-more-btn").click();
        }
        var ahref = $(this).attr("name");
        //alert(ahref);
        scrollToAnchor(ahref);
        //$(".table-ct").hide();
        return false;
    });

    function increaseValue() {
        var value = parseInt(document.getElementById("number").value, 10);
        value = isNaN(value) ? 0 : value;
        value++;
        document.getElementById("number").value = value;
    }

    function decreaseValue() {
        var value = parseInt(document.getElementById("number").value, 10);
        value = isNaN(value) ? 0 : value;
        value < 1 ? (value = 1) : "";
        value--;
        document.getElementById("number").value = value;
    }

    //window.addEventListener("scroll", function () {
    //    var start = 0;
    //    var end = 0;
    //    if ($('.start-tb').length !== 0) {
    //        start = $(".start-tb").offset().top;
    //    }
    //    if ($('.end-tb').length !== 0) {
    //        end = $(".end-tb").offset().top;
    //    }
    //    var obj = $(".menu-header").offset().top;
    //    if (obj - start > -50 && obj - end < -200) {
    //        $(".icon-sticky").css({
    //            visibility: "inherit",
    //            opacity: 1,
    //        });
    //    } else {
    //        $(".icon-sticky").css({
    //            visibility: "hidden",
    //            opacity: 0,
    //        });
    //        $(".table-of-contents-input").prop("checked", false);
    //    }
    //});


    $(".menu-mobile .sub-menu-item").each(function () {
        $(this).click(function () {
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
            } else {
                $(this).addClass("active");
            }
        });
    });

    $(".menu-mobile .nav-item-mobile .nav-link").each(function () {
        $(this).click(function () {
            // var containermenu = $(this);
            // var link = containermenu.find(".nav-link");
            // let at = $(this).find(".nav-link");
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
            } else {
                $(this).addClass("active");
            }
        });
    });

    $(".section1 .category").hover(function () {
        $(".section1 .category .over-lay-menu").css({
            display: 'block',
        })
    }, function () {
        $(".section1 .category .over-lay-menu").css({
            display: 'none',
        })
    });

    if ($("#frm_giohang #btnSubmit_giohang").length) {
        var buttonSubmit = $("#frm_giohang #btnSubmit_giohang");
        buttonSubmit.click(function () {
            $('#frm_giohang #done_giohang').val(1);
            $(this).attr('disabled', 'disabled');
            $(this).html('Loading...');
            $("#frm_giohang").submit();
        });
    }
    if ($("#frm_giohang #btnSubmit_coupon").length) {
        var buttonSubmit = $("#frm_giohang #btnSubmit_coupon");
        buttonSubmit.click(function () {
            $('#frm_giohang #done_giohang').val(1);
            $(this).attr('disabled', 'disabled');
            $(this).html('Loading...');
            $("#frm_giohang").submit();
        });
    }


    if ($("#frm_checkout").length) {
        var buttonSubmit = $("#frm_checkout #btnSubmit");
        buttonSubmit.click(function () {
            $('#frm_checkout #done').val(1);
            $(this).attr('disabled', 'disabled');
            $(this).html('Loading...');

            $.cookie('customer_name', $("#frm_checkout #name").val(), {
                expires: 365,
                path: '/'
            });
            $.cookie('customer_tel', $("#frm_checkout #tel").val(), {
                expires: 365,
                path: '/'
            });
            $.cookie('customer_address', $("#frm_checkout #address").val(), {
                expires: 365,
                path: '/'
            });

            $("#frm_checkout").submit();
        });

        $("#frm_checkout #name").val($.cookie("customer_name"));
        $("#frm_checkout #tel").val($.cookie("customer_tel"));
        $("#frm_checkout #address").val($.cookie("customer_address"));

        $(document).ready(function () {
            $("#frm_checkout").validate({
                rules: {
                    name: "required",
                    address: "required",
                    name: {
                        required: true,
                        minlength: 1
                    },
                    address: {
                        required: true,
                        minlength: 5
                    },
                    tel: {
                        required: true,
                        minlength: 10
                    },
                    email: {
                        required: false,
                        email: true
                    }
                },
                messages: {
                    name: {
                        required: "Vui lòng nhập Họ và tên",
                        minlength: "Tên quá ngắn"
                    },
                    name: {
                        required: "Vui lòng nhập Địa chỉ",
                        minlength: "Địa chỉ quá ngắn"
                    },
                    email: {
                        email: "Vui lòng nhập email hợp lệ"

                    },
                    tel: {
                        required: "Vui lòng nhập số điện thoại",
                        minlength: "Điện thoại thiếu số"
                    }

                }
            });
        });


        $('#frm_checkout input[name="option_payment"]').bind('click', function () {
            $('#frm_checkout .list-content-nganluong li').removeClass('active');
            $(this).parent('li').addClass('active');
        });


    }




    var cart_count = $("#header-cart-count")
    $.getJSON('/ajax/ajax.aspx', {
        control: "dynamic",
        services: "cart_count,product_viewed,product_fav"
    }, function (data) {

        $.each(data, function (key, value) {
            console.log(key + " : " + value);

            if (key == "cart_count" && parseInt(value) > 0) {
                $("#header-cart-count").show();
                $("#header-cart-count").html(value);
            }
            //if (key == "product_viewed" && parseInt(value) > 0) {
            //    $("#header-product-viewed").show();
            //    $("#header-product-viewed").html(value);
            //}
            //if (key == "product_fav" && parseInt(value) > 0) {
            //    $("#header-product-fav").show();
            //    $("#header-product-fav").html(value);
            //}
        });
    });



    function GetPriceFormat(x) {
        var r = x.toString().replace(/,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".");
        r = r + " VNĐ";
        return r;
    }




    $('.del_cart').click(function () {

        var id = $(this).attr("data-id");
        var quantity = 0;
        //console.log(id + " : " + quantity);
        var cart_total_price = $(".cart_total_price");
        var hdfTotalPrice = $("#hdfTotalPrice");
        var price_item = $(".price_item_" + id);
        Loading(true);

        $.getJSON('/ajax/ajax.aspx', { control: "dynamic", services: "cart_update,cart_total,cart_count", id: id, quantity: 0 }, function (data) {
            var jsonContent = JSON.parse(JSON.stringify(data));
            var cartCount = jsonContent.cart_count;

            $("#GG_Items").val(jsonContent.jsonProduct);
            $("#GG_Price").val(jsonContent.finalPrice);
            $("#GG_CountItems").val(jsonContent.quantity);
            cart_total_price.html(jsonContent.finalPriceVND);
            hdfTotalPrice.val(jsonContent.finalPriceVND);

            var item_rows_delete = $(".cart__list .item" + id);
            item_rows_delete.fadeOut("slow");

            $(".cart_count").html("(" + cartCount + " sản phẩm)");
            $("#cart-total").html(cartCount);


            if (parseInt(cartCount) > 0) {
                $("cart__page").show();
                $("#header-cart-count").html(cartCount);
            } else {
                console.log("cart empty");
                $(".cart-empty").show();
                $(".cart__page").hide();
            }
        });

        Loading(false);
    });



    $(function () {
        $('.increment').click(function () {
            var id = $(this).closest('.block-qty-cart').find('input.quantity_cart').attr('id');
            var valueElement = $('#' + id);
            if ($(this).hasClass('plus')) {
                valueElement.val(Math.max(parseInt(valueElement.val()) + 1));
            } else if (valueElement.val() > 1) {
                valueElement.val(Math.max(parseInt(valueElement.val()) - 1));
            }
            Loading(true);

            $.getJSON('/ajax/ajax.aspx', { control: "dynamic", services: "cart_update", id: id, quantity: valueElement.val() }, function (data) {
                data.jsonProduct = JSON.parse(data.jsonProduct);
                var jsonContent = data;
                //console.log(jsonContent);
                for (var i = 0; i < jsonContent.jsonProduct.length; i++) {
                    var product = jsonContent.jsonProduct[i];
                    //console.log(product);
                    var productElement = $(".item" + product.id);

                    if (productElement.length > 0) {
                        productElement.find(".quantity_cart").val(product.quantity);

                        

                        var price = typeof product.price === 'string' ? parseFloat(product.price) : product.price;
                        
                        // Thêm dấu chấm ngăn cách hàng nghìn bằng cách sử dụng hàm tự định nghĩa
                        var formattedPrice = formatNumberWithDot(price);
                        productElement.find(".price_item_" + product.id + " strong").text(formattedPrice + " VNĐ");
                        //alert(formattedPrice);
                        productElement.find(".coupon_quantity_apply").text(product.voucher_quantity);
                        productElement.find(".hdfPrice_" + product.id).val(price);

                        var couponApplyElement = productElement.find(".couponapply");
                        if (parseInt(product.voucher_quantity) > 0) {
                            couponApplyElement.show();
                        } else {
                            couponApplyElement.hide();
                        }
                    }
                }


                // Cập nhật tổng tiền
                $(".cart_total_price").html(data.finalPriceVND);
            })
                .done(function () {
                    Loading(false);
                })
                .fail(function () {
                    // Xử lý khi ajax thất bại (nếu cần)
                    Loading(false);
                });


            return false;
        });
    });

    function formatNumberWithDot(number) {
        return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }


    /* Article */

    ShowMoreArticle(1, true);


    function ShowMoreArticle(pageIndex, fistLoad) {
        var TotalProduct = $.cookie("TotalArticle");
        var totalP = $("#totalArticle").val();
        var rowCategory = $("#pageSize").val();

        if (fistLoad)
            TotalProduct = totalP;
        var leftProduct = TotalProduct - (parseInt(pageIndex) * parseInt(rowCategory));
        if (leftProduct < 1)
            $(".show-more").hide();
        else
            $(".show-more").show();
        $("#category_paging_article").text("Xem thêm (" + leftProduct + " bài viết)");
    }


    $("#category_paging_article").click(function () {
        var pageIndex = 1;
        if (typeof $.cookie('pageIndex_Category') !== 'undefined') {
            pageIndex = $.cookie("pageIndex_Category");
        }
        var pageIndexShowMore = pageIndex;
        pageIndex = parseInt(pageIndex) + 1;
        var categoryID = $("#idCategory").val();
        console.log("page " + pageIndexShowMore);
        $(".div-ajax-loading").show();
        $(".article-list").css({
            opacity: 0.3
        });
        jQuery.ajax({
            url: '/ajax/ajax.aspx',
            type: "GET",
            data: {
                categoryID: categoryID,
                action: "article_list",
                control: "categoryload",
                pageIndex: pageIndexShowMore
            },
            complete: function (response) {
                setTimeout(function () {
                    $('.article-list').append(response.responseText);
                    $(".article-list").css({
                        opacity: 1
                    });
                    $(".div-ajax-loading").hide();
                    ShowMoreArticle(pageIndexShowMore, false);
                }, 10);
            }
        });
    });




    function Loading(isShow) {
        if (isShow)
            $(".div-ajax-loading").show();
        else
            $(".div-ajax-loading").hide();
    }

    function HomeProduct(categoryid) {
        jQuery.ajax({
            url: '/ajax/ajax.aspx',
            type: "GET",
            data: {
                categoryid: categoryid,
                control: "homeproduct"
            },
            complete: function (response) {
                setTimeout(function () {
                    $('.home-product-ajax-' + categoryid).append(response.responseText);
                }, 10);
            }
        });
    }
});


function getCookie(name) {
    // Lấy tất cả các cookie
    var cookies = document.cookie.split(';');

    // Duyệt qua từng cookie để tìm cookie có tên giống tham số truyền vào
    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i].trim();

        // Nếu tìm thấy cookie có tên giống với tham số truyền vào, trả về giá trị của nó
        if (cookie.indexOf(name) === 0) {
            return cookie.substring(name.length, cookie.length);
        }
    }

    // Trả về null nếu không tìm thấy cookie nào có tên giống tham số truyền vào
    return null;
}

function setCookie(name, value, days = 30, path = '/') {
    var d = new Date();
    d.setTime(d.getTime() + (days * 24 * 60 * 60 * 1000)); // Thời gian sống: days ngày
    var expires = "expires=" + d.toUTCString();
    document.cookie = name + "=" + value + ";" + expires + ";path=" + path;
}

$("#category_paging").click(function () {
    var pageIndex = 1;
    if (typeof $.cookie('pageIndex_Category') !== 'undefined') {
        pageIndex = $.cookie("pageIndex_Category");
    }

    var pageIndexShowMore = pageIndex;
    pageIndex = parseInt(pageIndex) + 1;


    var rootFilterCategoryID = "";
    var attributeIDList = "";

    if ($("#loadByFilter").val() == "1");
    {
        rootFilterCategoryID = $("#rootFilterCategoryID").val();
        attributeIDList = $("#attributeIDList").val();
    }


    var categoryID = "";
    var keyword = "";
    if ($("#idCategory").length)
        categoryID = $("#idCategory").val();
    else
        keyword = $("#keyword").val();

    console.log("page " + pageIndexShowMore);

    $(".div-ajax-loading").show();
    $(".product-list").css({
        opacity: 0.3
    });

    var thuonghieu = $("#thuonghieu").val();
    jQuery.ajax({
        url: '/ajax/ajax.aspx',
        type: "GET",
        data: {
            categoryID: categoryID,
            action: "product_list",
            control: "categoryload",
            rootFilterCategoryID: rootFilterCategoryID,
            attributeIDList: attributeIDList,
            keyword: keyword,
            pageIndex: pageIndexShowMore,
            thuonghieu: thuonghieu
        },
        complete: function (response) {
            setTimeout(function () {

                //if (isMore)
                $('.product-list').append(response.responseText);
                //else
                //    $('.category').html(response.responseText);
                //$(".category").css({ opacity: 1 });
                //$("#div-ajax-loading").hide();
                $(".product-list").css({
                    opacity: 1
                });
                $(".div-ajax-loading").hide();
                ShowMore(pageIndexShowMore, false);
            }, 10);
        }
    });
});

ShowMore(1, true);

function ShowMore(pageIndex, fistLoad) {
    var TotalProduct = $.cookie("TotalProduct");
    var totalP = $("#totalProduct").val();
    var rowCategory = $("#pageSize").val();

    if (fistLoad)
        TotalProduct = totalP;



    var leftProduct = TotalProduct - (parseInt(pageIndex) * parseInt(rowCategory));

    console.log("pageIndex: " + pageIndex + " - TotalProduct: " + TotalProduct);



    //alert(leftProduct);
    if (leftProduct < 1) {
        $(".show-more").hide();
        $(".section-readmore-cate").hide();
    }
    else {
        $(".show-more").show();
        $(".section-readmore-cate").show();
    }

    //alert(TotalProduct + ", " + pageIndex);
    $("#category_paging").text("Xem thêm (" + leftProduct + " sản phẩm)");
}
$(document).ready(function () {
    GetAttributeProduct();
});
function GetAttributeProduct() {

    if ($("#AttributesIDList").length) {
        var htmlContent = '';
        var count = 0;
        var categoryIDList = $("#idCategory").val();
        $.getJSON('/ajax/ajax.aspx', { control: "attributeproduct", categoryList: String(categoryIDList) }, function (data) {
            var jsonContent = JSON.parse(JSON.stringify(data));
            var divAttrAjax = $(".filter-ajax");
            for (var i = 0; i < jsonContent.length; i++) {

                var item = jsonContent[i];

                if (item.Name == "RootID") {
                    $("#rootFilterCategoryID").val(item.ID);
                }
                else {
                    count++;

                    htmlContent += "<div class='box-cate-side'>"
                    htmlContent += "<div class='title-sidebar'>" + item.Name + "</div>";
                    htmlContent += "<div class='list-cate-side'>"
                    var jsonChild = JSON.parse(JSON.stringify(item.attributeProductChild));

                    for (var j = 0; j < jsonChild.length; j++) {
                        var itemChild = jsonChild[j];
                        var selected = "";

                        var categoryName = $("#categoryName").val();
                        if (itemChild.Name == categoryName) {
                            var filted_html = "<a href='javascript:;' onclick='RemoveAttr(" + itemChild.ID + ")' data-id='" + itemChild.ID + "'><span>" + itemChild.Name + "</span><i class='fas fa-times'></i></a>";
                            $("#filted").html(filted_html);
                            //selected = " checked"; //Tự động tích vào nếu cùng tên với danh mục
                            $("#attributeIDList").val(itemChild.ID);
                        }
                        //console.log(categoryName + " - " + itemChild.Name);

                        htmlContent += "<div class='item-filter'><input" + selected + " type='checkbox' class='checkboxAttr' onclick='GetValueFromAttr()' id='checkboxAttr_" + itemChild.ID + "' data-url='" + itemChild.FriendlyUrl + "' data-url-parent='" + itemChild.FriendlyUrlParent + "' data-name='" + itemChild.Name + "' />";
                        if (itemChild.Image != '')
                            htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'><img src='" + itemChild.Image + "'></label></div>";
                        else
                            htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'>" + itemChild.Name + "</label></div>";


                        //console.log(categoryName + " - " + itemChild.Name);

                    }
                    htmlContent += "</div></div>"
                }
            }
            if (data.length) {

                divAttrAjax.show();
                divAttrAjax.html(htmlContent);
                BindDataToAttr();
            }
        });

        if (count == 0) {
            $(".filter-ajax").hide();
        }
        else {
            $(".filter-ajax").show();
        }
    }
}
function BindDataToAttr() {
    var datas = $("#AttributesIDList").val();
    var arr = datas.split(',');
    $.each(arr, function (index, value) {
        var checkbox = $("#checkboxAttr_" + value);
        if (checkbox.length) {
            checkbox.prop('checked', true);
            console.log(value.Access + '_' + value.ID);
        }
    });
}
// Load theo Attributes
function GetValueFromAttr() {
    $("#loadByFilter").val("1");

    //console.log("đã chọn");




    var list_id = "";
    var filted_html = "";

    var urlParam = "?";

    $('.checkboxAttr:checked').each(function () {
        if (list_id != null && list_id != '')
            list_id += ",";
        var checkboxValue = $(this).attr("id");
        list_id += checkboxValue.replace("checkboxAttr_", "");

        var name = $(this).attr("data-name");
        filted_html += "<a href='javascript:;' onclick='RemoveAttr(" + checkboxValue.replace("checkboxAttr_", "") + ")' data-id='" + checkboxValue.replace("checkboxAttr_", "") + "'><span>" + name + "</span><i class='fas fa-times'></i></a>";


        var url = $(this).attr("data-url");
        var url_parent = $(this).attr("data-url-parent");
        var current_url = window.location.href;

        //if (current_url.indexOf(url_parent) != -1) { // Nếu Param đã tồn tại
        //    urlParam.replace(url_parent + "=" + url, "");//Remove cái cũ
        //    var currentParam = getUrlParameter(url_parent);
        //    url = currentParam + "." + url;
        //}


        //alert(current_url);



        //if (urlParam != "?")
        //    urlParam += "&";

        urlParam = "?" + url_parent + "=" + url;

    });

    window.history.replaceState(null, null, urlParam);

    console.log(list_id);
    $("#attributeIDList").val(list_id);
    $("#filted").html(filted_html);

    var pageIndex = 1;
    var rootFilterCategoryID = $("#rootFilterCategoryID").val();
    var attributeIDList = $("#attributeIDList").val();

    var pageIndexShowMore = pageIndex;
    pageIndex = parseInt(pageIndex) + 1;
    var categoryID = "";
    var keyword = "";
    if ($("#idCategory").length)
        categoryID = $("#idCategory").val();
    else
        keyword = $("#keyword").val();

    $(".div-ajax-loading").show();
    $(".product-list").css({
        opacity: 0.3
    });
    jQuery.ajax({
        url: '/ajax/ajax.aspx',
        type: "GET",
        data: {
            action: "product_list",
            control: "categoryload",
            rootFilterCategoryID: rootFilterCategoryID,
            attributeIDList: attributeIDList,
            pageIndex: pageIndexShowMore,
            keyword: keyword,
            categoryID: categoryID
        },
        complete: function (response) {
            setTimeout(function () {
                $('.product-list').html(response.responseText);
                $(".product-list").css({
                    opacity: 1
                });
                $(".div-ajax-loading").hide();
                ShowMore(pageIndexShowMore, false);
            }, 500);
        }
    });

}
function RemoveAttr(data_id) {
    var removeitem = $("#filted").find("[data-id='" + data_id + "']");
    console.log(data_id);
    removeitem.hide();

    var checkbox = $("#checkboxAttr_" + data_id);
    checkbox.prop("checked", false);

    var AttrList = $("#attributeIDList");
    var array = AttrList.val().split(",");

    array = jQuery.grep(array, function (value) {
        return value != data_id;
    });

    AttrList.val(array.toString());

    GetValueFromAttr();



}
$('#readmore').click(function () {
    $('.section-readmore-cate').toggleClass('add');
    if ($('#readmore').text() == "Đọc thêm") {
        $(this).text("Thu gọn")
    } else {
        $(this).text("Đọc thêm")
    }
});
function OpenSocial() {
    var screenWidth = $(window).width();
    var isTouch = !!('ontouchstart' in window) || !!('onmsgesturechange' in window);
    var isPhone = isTouch || screenWidth <= 600;
    var alwayShow = "0";
    var hoz = "1";

    var timer = null;

    var icon = {};
    icon.top = "&#11165;";
    icon.phone = "&phone;";
    icon.cart = "&#128722;";
    var ul = $("#navs"),
        li = $("li", ul),
        length = li.length,
        n = length - 1,
        r = 70;
    console.log(ul);
    function navAnimate($ul) {
        $ul.toggleClass('active');
        if ($ul.hasClass('active')) {
            for (var i = 0; i < length; i++) {
                var _li = li.eq(i);
                _li.css({
                    'transition-delay': "" + (50 * i) + "ms",
                    '-webkit-transition-delay': "" + (50 * i) + "ms",
                    //'left': -1 * (r * Math.cos(90 / n * i * (Math.PI / 180))),
                    //'top': (-r * Math.sin(90 / n * i * (Math.PI / 180)))
                    //'left': alwayShow == "1" ? 0 : -1 * (r * Math.cos(90 / n * i * (Math.PI / 180))),
                    'right': hoz == "1" ? 0 : 1 * (r * Math.cos(90 / n * i * (Math.PI / 180))),
                    'top': hoz == "1" ? -50 * (i + 1) : (-r * Math.sin(90 / n * i * (Math.PI / 180)))
                });

            }
            setTimeout(function () { $('li.call').addClass('active'); }, 500);
        } else {
            li.removeAttr('style');
            $('li.call').removeClass('active');
        }
    }

    ul.on('switchArc', function () {
        navAnimate($(this));
    });

    if (isPhone) {
        ul.on('click touch', function () {
            $(this).trigger('switchArc');
        });
    } else {
        ul.hover(function () {
            if (timer)
                clearTimeout(timer);
            if (!$(this).hasClass('active'))
                $(this).trigger('switchArc');
        },
            function () {
                timer = setTimeout(function () {
                    if (ul.hasClass('active'))
                        ul.trigger('switchArc');
                }, 250);
            });
    }

    $(".arc-item-3.gotop a").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
        if ($('html, body').scrollTop() == 0) return;
        $('html, body').animate({
            scrollTop: 0
        }, 1000, function () {
            if (ul.hasClass('active')) {
                ul.trigger('switchArc');
            }
        });
    });

 /*   $(".nav-hotline-wrap").click(function (e) {
        e.preventDefault();
        e.stopPropagation();
    });*/

    $('.navs-bg').on('click touch', function (e) {
        if (isPhone)
            ul.trigger('switchArc');
    })

    if (alwayShow == "1")
        navAnimate(ul);
};
$(document).ready(function () {
    OpenSocial();
});
/* Check Validate from*/
if ($("#frm_checkout").length) {
    var buttonSubmit = $("#frm_checkout #btnSubmit");
    buttonSubmit.click(function () {
        $('#frm_checkout #done').val(1);
        $(this).attr('disabled', 'disabled');
        $(this).html('Loading...');

        $.cookie('customer_name', $("#frm_checkout #name").val(), {
            expires: 365,
            path: '/'
        });
        $.cookie('customer_tel', $("#frm_checkout #tel").val(), {
            expires: 365,
            path: '/'
        });
        $.cookie('customer_address', $("#frm_checkout #address").val(), {
            expires: 365,
            path: '/'
        });

        $("#frm_checkout").submit();
    });

    $("#frm_checkout #name").val($.cookie("customer_name"));
    $("#frm_checkout #tel").val($.cookie("customer_tel"));
    $("#frm_checkout #address").val($.cookie("customer_address"));

    $(document).ready(function () {
        $("#frm_checkout").validate({
            rules: {
                name: "required",
                address: "required",
                name: {
                    required: true,
                    minlength: 1
                },
                address: {
                    required: true,
                    minlength: 5
                },
                tel: {
                    required: true,
                    minlength: 10
                },
                email: {
                    required: false,
                    email: true
                }
            },
            messages: {
                name: {
                    required: "Vui lòng nhập Họ và tên",
                    minlength: "Tên quá ngắn"
                },
                address: {
                    required: "Vui lòng nhập Địa chỉ",
                    minlength: "Địa chỉ quá ngắn"
                },
                email: {
                    email: "Vui lòng nhập email hợp lệ"

                },
                tel: {
                    required: "Vui lòng nhập số điện thoại",
                    minlength: "Điện thoại thiếu số"
                }

            }
        });
    });


    $('#frm_checkout input[name="option_payment"]').bind('click', function () {
        $('#frm_checkout .list-content-nganluong li').removeClass('active');
        $(this).parent('li').addClass('active');
    });


}

document.addEventListener('readystatechange', event => {
    if (event.target.readyState === "complete") {
        var clockdiv = document.getElementsByClassName("timeCountdown");
        var countDownDate = new Array();
        for (var i = 0; i < clockdiv.length; i++) {
            countDownDate[i] = new Array();
            countDownDate[i]['el'] = clockdiv[i];
            countDownDate[i]['time'] = new Date(clockdiv[i].getAttribute('data-date')).getTime();
            countDownDate[i]['hours'] = 0;
            countDownDate[i]['seconds'] = 0;
            countDownDate[i]['minutes'] = 0;
        }

        var countdownfunction = setInterval(function () {
            for (var i = 0; i < countDownDate.length; i++) {
                var now = new Date().getTime();
                var distance = countDownDate[i]['time'] - now;
                countDownDate[i]['hours'] = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                countDownDate[i]['minutes'] = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
                countDownDate[i]['seconds'] = Math.floor((distance % (1000 * 60)) / 1000);
                
                if (distance < 0) {
                    
                    countDownDate[i]['el'].querySelector('.hours').innerHTML = 0;
                    countDownDate[i]['el'].querySelector('.minutes').innerHTML = 0;
                    countDownDate[i]['el'].querySelector('.seconds').innerHTML = 0;
                } else {
                    console.log(countDownDate[i]['hours'])
                    if (countDownDate[i]['hours'] < 10) {
                        countDownDate[i]['el'].querySelector('.hours').innerHTML = "0" + countDownDate[i]['hours'];
                    }
                    else {
                        countDownDate[i]['el'].querySelector('.hours').innerHTML = countDownDate[i]['hours'];
                    }
                    if (countDownDate[i]['minutes'] < 10) {
                        countDownDate[i]['el'].querySelector('.minutes').innerHTML ='0' + countDownDate[i]['minutes'];
                    }
                    else {
                        countDownDate[i]['el'].querySelector('.minutes').innerHTML = countDownDate[i]['minutes'];
                    }
                    if (countDownDate[i]['seconds'] < 10) {
                        countDownDate[i]['el'].querySelector('.seconds').innerHTML ='0' + countDownDate[i]['seconds'];
                    }
                    else {
                        countDownDate[i]['el'].querySelector('.seconds').innerHTML = countDownDate[i]['seconds'];
                    } 
                }
            }
        }, 1000);
    }
});





var target_date = new Date().getTime() + (1000 * 3600 * 12);
var days, hours, minutes, seconds;

var countdown = document.getElementById("tiles");

getCountdown();

setInterval(function () { getCountdown(); }, 1000);

function getCountdown() {


    var current_date = new Date().getTime();
    var seconds_left = (target_date - current_date) / 1000;

    hours = pad(parseInt(seconds_left / 3600));
    seconds_left = seconds_left % 3600;

    minutes = pad(parseInt(seconds_left / 60));
    seconds = pad(parseInt(seconds_left % 60));

    if (countdown) {
        countdown.innerHTML = "<span>" + hours + "</span><b> : </b><span>" + minutes + "</span> <b> : </b> <span>" + seconds + "</span>";
    }
    
}

function pad(n) {
    return (n < 10 ? '0' : '') + n;
}
/* Fixed slidebar */

var $sidebar = $('.sidebar');
var sidebarTop = $sidebar.position().top;
var blogHeight = $('.product__wrapper').outerHeight() - 10;
console.log($('.sidebar').offsetHeight)
$(window).scroll(fixSidebarOnScroll);

function fixSidebarOnScroll() {
    var windowScrollTop = $(window).scrollTop();
    if (windowScrollTop >= blogHeight || windowScrollTop <= sidebarTop) {
        $sidebar.removeClass('sticky');
    }

    else if (windowScrollTop >= sidebarTop) {
        if (!$sidebar.hasClass('sticky')) {
            $sidebar.addClass('sticky');
        }
    }
}