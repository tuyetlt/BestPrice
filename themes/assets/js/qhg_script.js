if (typeof QHGraphic == "undefined") {
    var QHGraphic = {};
}

QHGraphic.Module = {
    winWidth: $(window).width(),
    winHeight: $(window).height(),
    scrollTop: $(window).scrollTop(),
    init: function () {
        this.createMenuMobile();
        this.createBannerSlider();
        this.createProductSlideshow();
        this.createProductAdvertSlideshow();
        this.createBannerProductAdvertSliders();
        this.createTestimonialSliders();
        this.createProductMediaSlide();
        //this.createProductReadMore();
        this.windowScrollTop();

        /* Menu */
        if ($('.btnNavigationMenu').length) {
            let event = "click";

            if ($('html').hasClass('mobile')) {
                event = 'touchstart';
            }

            $(document).on(event, ".btnNavigationMenu", function (e) {
                e.preventDefault();

                if ($('html').hasClass('nav-open')) {
                    $('html').removeClass('nav-open');
                } else {
                    setTimeout(function () {
                        $('html').removeClass('sidebar-open').addClass('nav-open');
                    }, 100);
                }
            });
        }

        if($('.btn-sidebar-filter').length) {
            let event = "click";

            if ($('html').hasClass('mobile')) {
                event = 'touchstart';
            }

            $(document).on(event, ".btn-sidebar-filter", function (e) {
                if ($('html').hasClass('sidebar-open')) {
                    $('html').removeClass('sidebar-open');
                } else {
                    setTimeout(function () {
                        $('html').removeClass('nav-open').addClass('sidebar-open');
                    }, 100);
                } 
            });

            $('.btn-close-sidebar').on('click', function(e) {
                e.preventDefault();

                $('.btn-sidebar-filter').trigger('click');
            });
        }
    },
    createMenuMobile: function() {
        /* Variables */
        var $menuRoot 	= $('.navigation__menu');
        var $nextAction = $menuRoot.find('button.btn-next-menu'),
            $backAction = $menuRoot.find('button.btn-prev-menu');

        /* ------------------------- Next Button ------------------------- */
        $nextAction.on('click', function(event){
            event.preventDefault();

            setTimeout(function() {
                $('html, body').animate({
                    scrollTop:0
                }, 250);
            }, 250);

            /* Set is-in-view class for current ul and only that ul */
            $(this).parents('.has-children').find('.dropdown-menu').addClass('is-in-view');
            /* Once the sub ul is visible we hide other sibling uls so that they do not appear above the is-in-view ul */
            $(this).parents().siblings('li').find('.dropdown-menu').hide();
            /* If we use the back button and decide to go into another submenu we need to bring back the hidden sibling uls */
            $(this).parents('.has-children').find('.dropdown-menu').show();
            /* Once we go in a level we remove the is-in-view class and add the has-been-viewed class, this allows for the slide off animation and the slide in animations  */
            $(this).parents('.navigation__menu').addClass('has-been-viewed').removeClass('is-in-view');
        });

        /* ------------------------- Back Button ------------------------- */
        $backAction.click(function(event){
            event.preventDefault();

            $(this).parents('.is-in-view').closest('.has-been-viewed').toggleClass('has-been-viewed is-in-view').promise().done(function() {
                $menuRoot.find('.is-in-view').toggleClass('is-in-view');
            });
        });
    },
    createBannerSlider: function () {
        const bannerSliders = $('.createBannerSliders');

        if(bannerSliders.length > 0) {
            $(bannerSliders).not('.slick-initialized').slick({
                dots: false,
                arrows: false,
                infinite: true,
                autoplay: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                autoplaySpeed: 5000,
                speed: 800,
               /* appendDots: '.banner__slideshow .banner__section-inner',
                appendArrows: 'banner__slideshow .banner__section-inner',
                responsive: [
                    {
                        breakpoint: 768,
                        settings: {
                            arrows: false
                        }
                    }
                ]*/
            });
        }
    },
    createProductSlideshow: function() {
        const productSliders = document.querySelectorAll('.productSliders');
        
        if (productSliders.length) {
            for(let i = 0; i < productSliders.length; i++) {
                var module_id = productSliders[i].getAttribute('module');
                const sliders = document.querySelector('.createProductSlideshows_' + module_id);

                $(sliders).not('.slick-initialized').slick({
                    arrows: true,
                    infinite: true,
                    autoplay: false,
                    slidesToShow: 5,
                    slidesToScroll: 4,
                    autoplaySpeed: 5000,
                    speed: 800,
                    /*appendArrows: '.moduleProductSlideshow-' + module_id + ' .moduleProductSlideshow__wrap',*/
                    responsive: [
                        {
                            breakpoint: 1200,
                            settings: {
                                slidesToShow: 4,
                                slidesToScroll: 1
                            }
                        }, {
                            breakpoint: 1024,
                            settings: {
                                slidesToShow: 3,
                                slidesToScroll: 1
                            }
                        }, {
                            breakpoint: 768,
                            settings: {
                                slidesToShow: 2,
                                slidesToScroll: 1
                            }
                        }
                    ]
                });
            }
        }
    },
    createProductAdvertSlideshow: function() {
        const productSlider = document.querySelectorAll('.createProductAdvertSlideshow');
        
        if (productSlider.length) {
            for(let i = 0; i < productSlider.length; i++) {
                var module_id = productSlider[i].getAttribute('module');

                $(productSlider[i]).not('.slick-initialized').slick({
                    arrows: true,
                    infinite: true,
                    autoplay: false,
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    autoplaySpeed: 5000,
                    speed: 800,
                   /* appendArrows: '.banner__productAdvert-slideshow' + module_id + ' .banner__productAdvert-slide__inner',*/
                    responsive: [
                        {
                            breakpoint: 1200,
                            settings: {
                                slidesToShow: 2,
                                slidesToScroll: 1
                            }
                        },
                        {
                            breakpoint: 1024,
                            settings: {
                                slidesToShow: 3,
                                slidesToScroll: 1
                            }
                        },
                        {
                            breakpoint: 768,
                            settings: {
                                slidesToShow: 2,
                                slidesToScroll: 1
                            }
                        }
                    ]
                });
            }
        }
    },
    createBannerProductAdvertSliders: function() {
        var slider = document.querySelectorAll('.createBannerProductAdvertSliders');

        if (slider) {
            $(slider).not('.slick-initialized').slick({
                fade: true,
                dots: false,
                arrows: false,
                infinite: true,
                autoplay: true,
                speed: 1000
            });
        }
    },
    createTestimonialSliders: function() {
        const testimonialSlider = document.querySelector('.testimonialSliders');

        if (testimonialSlider) {
            $(testimonialSlider).not('.slick-initialized').slick({
                dots: true,
                arrows: false,
                infinite: true,
                autoplay: false,
                slidesToShow: 4,
                slidesToScroll: 1,
                autoplaySpeed: 5000,
                speed: 800,
                appendDots: ".testimonials__section",
                responsive: [
                    {
                        breakpoint: 1024,
                        settings: {
                            slidesToShow: 3,
                            slidesToScroll: 1,
                        }
                    }, {
                        breakpoint: 768,
                        settings: {
                            slidesToShow: 2,
                            slidesToScroll: 1,
                        }
                    }, {
                        breakpoint: 480,
                        settings: {
                            slidesToShow: 1,
                            slidesToScroll: 1
                        }
                    }
                ]
            });
        }    
    },
    createProductMediaSlide: function () {
        if ($('.product-thumbnails, .productMainSliders').length) {
            $('.productMainSliders').not('.slick-initialized').slick({
                infinite: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                fade: true,
                appendArrows: '.product__galleries .product__galleries-main',
                asNavFor: '.product-media-sliders'
            });

            $('.product-media-sliders').not('.slick-initialized').slick({
                arrows: false,
                infinite: true,
                slidesToShow: 4,
                slidesToScroll: 1,
                focusOnSelect: true,
                asNavFor: '.productMainSliders',
                responsive: [
                    {
                        breakpoint: 1024,
                        settings: {
                            slidesToShow: 3,                       
                        }
                    }
                ]
            });
        }
    },
    createProductReadMore: function() {
        var readMore = $('.productReadMore');

        if (readMore.length) {
            readMore.readmore({
                speed: 500,
                collapsedHeight: 350,
                moreLink: '<div class="buttons text-center"><button type="button" class="btn btn-outline-primary btn-collapse-more">Xem thêm nội dung</button></div>',
                lessLink: '<div class="buttons text-center"><button type="button" class="btn btn-outline-primary btn-collapse-less">Thu gọn nội dung</button></div>',
                afterToggle: function(trigger, element, expanded) {
                    if(! expanded) {
                        $('html, body').animate({
                        scrollTop: element.offset().top
                        }, {duration: 600 });
                    } else {
                        element.css("height", "auto");
                    }
                }
            });
            
            $(document).on('click', ".btn-collapse-less", function(event) {
                event.preventDefault();
            
                $('body, html').animate({
                    scrollTop: $('#description').offset().top - 100
                }, 300);
            });
        }
    },
    windowScrollTop: function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() > 150) {
                $('#backToTop').css('opacity', 1);
            } else {
                $('#backToTop').css('opacity', 0);
            }
        });

        $('#backToTop a').on('click', function () {
            $('body, html').animate({
                scrollTop: 0
            }, 600);

            return false;
        });
    }
};

// Increment Value
function incrementValue(e) {
    e.preventDefault();

    let parent = $(e.target).closest('.block-qty-cart');
    let currentVal = parseInt(parent.find('.inputQty').val(), 10);

    if (!isNaN(currentVal)) {
      parent.find('.inputQty').val(currentVal + 1);
    } else {
      parent.find('.inputQty').val(1);
    }
}

// Decrement Value
function decrementValue(e) {
    e.preventDefault();

    let parent = $(e.target).closest('.block-qty-cart');
    let currentVal = parseInt(parent.find('.inputQty').val(), 10);

    if (!isNaN(currentVal) && currentVal > 1) {
      parent.find('.inputQty').val(currentVal - 1);
    } else {
      parent.find('.inputQty').val(1);
    }
}

$(document).ready(function () {
    QHGraphic.Module.winWidth = $(window).width();
    QHGraphic.Module.init();

    // Update Qty
    if ($('.btn-touchspin-down').length || $('.btn-touchspin-up').length) {
        $(document).on('click', '.btn-touchspin-up', function (e) {
            e.preventDefault();

            incrementValue(e);

            // if ($('#FormCart').length) {
            //     $('#FormCart').submit();
            // }
        });

        $(document).on('click', '.btn-touchspin-down', function (e) {
            e.preventDefault();

            decrementValue(e);

            // if ($('#FormCart').length) {
            //     $('#FormCart').submit();
            // }
        });
    }
});

var windowResize;

$(window).resize(function () {
    let winNewWidth = $(window).width();
    let winNewHeight = $(window).height();

    if (QHGraphic.winWidth != winNewWidth || QHGraphic.winHeight != winNewHeight) {
        clearTimeout(windowResize);

        windowResize = setTimeout(function () {
            QHGraphic.winWidth = winNewWidth;
            QHGraphic.winHeight = winNewHeight;
        }, 100);
    }
});

if ($("#frm_giohang #button-cart").length) {
    var buttonSubmit = $("#frm_giohang #button-cart");
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


//$(document).ready(function () {
//    // Số lượng tối đa các từ khóa tìm kiếm được lưu trữ
//    var maxHistoryItems = 10;

//    // Load and display search history on page load
//    displaySearchHistory();

//    // Search input keyup event for suggestions
//    $('#input-search').on('keyup', function () {
//        var searchKeyword = $(this).val();
//        //alert(searchKeyword);

//        // Check if the search keyword is not empty
//        if (searchKeyword.trim() !== '') {
//            // Fetch search suggestions from the API
//            fetchSearchSuggestions(searchKeyword);
//        } else {
//            // Clear search suggestions if the input is empty
//            clearSearchSuggestions();
//        }
//    });

//    // Search input keydown event
//    $('#input-search').on('keydown', function (e) {
//        // Check if the key pressed is Enter (key code 13)
//        if (e.keyCode === 13) {
//            var searchKeyword = $(this).val();

//            // Check if the search keyword is not empty
//            if (searchKeyword.trim() !== '') {
//                // Save the search keyword to local storage
//                saveSearchKeyword(searchKeyword);

//                // Display updated search history
//                displaySearchHistory();

//                // Clear the search suggestions
//                clearSearchSuggestions();
//            }
//        }
//    });

//    // Remove search history item click event
//    $(document).on('click', '.btn-remove-search', function (e) {
//        e.stopPropagation(); // Prevent dropdown from closing
//        var index = $(this).data('index');

//        // Remove the search history item from local storage
//        removeSearchKeyword(index);

//        // Display updated search history
//        displaySearchHistory();
//    });

//    // Click on a search history item to redirect
//    $(document).on('click', '.search-history-item', function () {
//        var keyword = $(this).find('.product__name').text();
//        // Redirect to the search page with the selected keyword
//        window.location.href = '/tim-kiem.html?key=' + encodeURIComponent(keyword);
//    });

//    // Click on the "Xoá tất cả lịch sử" button
//    $('#del-all-search-history').on('click', function () {
//        // Clear all search history from local storage
//        clearAllSearchHistory();

//        // Display updated search history
//        displaySearchHistory();
//    });

//    // Click on a category__latest item to save it to search history
//    $(document).on('click', '.category__latest a', function () {
//        var categoryName = $(this).find('.search-cat__name').text();

//        // Save the category name to local storage
//        saveSearchKeyword(categoryName);

//        // Display updated search history
//        displaySearchHistory();
//    });

//    function fetchSearchSuggestions(keyword) {
//        // Your Ajax call here
//        $.ajax({
//            url: '/ajax/ajax.aspx',
//            method: 'GET',
//            data: {
//                control: 'search',
//                key: keyword
//            },
//            success: function (response) {
//                // Handle the response and display suggestions
//                handleSearchSuggestions(response);
//            },
//            error: function (error) {
//                console.error('Ajax error:', error);
//            }
//        });
//    }

//    function handleSearchSuggestions(response) {
//        var suggestionsList = $('#search-suggestions ul');
//        suggestionsList.empty(); // Clear existing suggestions

//        // Append the new suggestions to the list
//        suggestionsList.append(response);

//        // Show the suggestions dropdown
//        $('#search-suggestions').show();
//    }

//    // Function to display search suggestions
//    function displaySearchSuggestions(suggestions) {
//        var suggestionList = $('.search-suggestions');

//        // Clear existing search suggestions
//        suggestionList.empty();

//        // Parse the HTML string into DOM elements
//        var suggestionsDOM = $.parseHTML(suggestions);

//        // Display each search suggestion
//        $(suggestionsDOM).find('li').each(function () {
//            // Clone the suggestion and append it to the suggestion list
//            suggestionList.append($(this).clone());
//        });

//        // Show the search suggestions dropdown
//        suggestionList.show();
//    }

//    // Function to clear search suggestions
//    function clearSearchSuggestions() {
//        var suggestionList = $('.search-suggestions');
//        suggestionList.empty();
//        suggestionList.hide();
//    }

//    // Function to save search keyword to local storage
//    function saveSearchKeyword(keyword) {
//        var searchHistory = getSearchHistory();

//        // Add the new keyword to the search history array
//        searchHistory.unshift(keyword); // Add to the beginning of the array

//        // Limit the number of items to maxHistoryItems
//        searchHistory = searchHistory.slice(0, maxHistoryItems);

//        // Save the updated search history to local storage
//        localStorage.setItem('searchHistory', JSON.stringify(searchHistory));
//    }

//    // Function to display search history on the page
//    function displaySearchHistory() {
//        var searchHistory = getSearchHistory();
//        var historyList = $('.dropdown-inner ul');

//        // Clear existing search history
//        historyList.empty();

//        // Display each search history item with remove button
//        for (var i = 0; i < searchHistory.length; i++) {
//            var item = $('<li class="search-history-item">' +
//                '<div class="d-flex align-items-center">' +
//                '<div class="col d-flex align-items-center">' +
//                '<div class="img">' +
//                '<img src="/themes/assets/images/icon/history-icon.png" width=20 />' +
//                '</div>' +
//                '<div class="product__name">' + searchHistory[i] + '</div>' +
//                '</div>' +
//                '<a href="#" class="btn-remove-search" data-index="' + i + '"><i class="icon-close"></i></a>' +
//                '</div>' +
//                '</li>');

//            historyList.append(item);
//        }
//    }

//    // Function to get search history from local storage
//    function getSearchHistory() {
//        var searchHistory = localStorage.getItem('searchHistory');
//        return searchHistory ? JSON.parse(searchHistory) : [];
//    }

//    // Function to remove search history item from local storage
//    function removeSearchKeyword(index) {
//        var searchHistory = getSearchHistory();

//        // Remove the item at the specified index
//        searchHistory.splice(index, 1);

//        // Save the updated search history to local storage
//        localStorage.setItem('searchHistory', JSON.stringify(searchHistory));
//    }

//    // Function to clear all search history from local storage
//    function clearAllSearchHistory() {
//        // Remove the search history from local storage
//        localStorage.removeItem('searchHistory');
//    }
//});