$(document).ready(function () {

    const pg = document.getElementById("pagination");
    const pages = document.getElementById("pages");
    const curPage = document.getElementById("curpage");
    const numLinksTwoSide = document.getElementById("delta");
    const checks = document.querySelectorAll(".check");
    const btnNextPg = document.querySelector("button.next-page");
    const btnPrevPg = document.querySelector("button.prev-page");
    const btnFirstPg = document.querySelector("button.first-page");
    const btnLastPg = document.querySelector("button.last-page");
    if (pages.length) {
    const valuePage = {
        truncate: true,
        curPage: 1,
        numLinksTwoSide: 1,
        totalPages: parseInt(pages.value, 10)
    };

    
        pagination();

    }

    pg.onclick = (e) => {
        const ele = e.target;

        if (ele.dataset.page) {
            const pageNumber = parseInt(e.target.dataset.page, 10);
            fetchData(pageNumber);
            valuePage.curPage = pageNumber;
            curPage.value = pageNumber;
            pagination(valuePage);

            handleButtonLeft();
            handleButtonRight();
        }
    };

    pages.onchange = () => {
        valuePage.totalPages = parseInt(pages.value, 10);
        handleCheckTruncate();
        handleCurPage();
        pagination();
        handleButtonLeft();
        handleButtonRight();
    };
    curPage.onchange = () => {
        handleCurPage();
        pagination();
        handleButtonLeft();
        handleButtonRight();
    };
    numLinksTwoSide.onchange = function () {
        if (this.value > 5) {
            this.value = 1;
            valuePage.numLinksTwoSide = 1;
        } else {
            valuePage.numLinksTwoSide = parseInt(this.value, 10);
        }
        pagination();
    };

    checks.forEach((check) => {
        check.onclick = (e) => {
            //console.log(e.target);
            handleCheckTruncate();
            pagination();
        };
    });

    // DYNAMIC PAGINATION
    function pagination() {
        const { totalPages, curPage, truncate, numLinksTwoSide: delta } = valuePage;

        const range = delta + 4; // use for handle visible number of links left side

        let render = "";
        let renderTwoSide = "";
        let dot = `<li class="pg-item"><a class="pg-link">...</a></li>`;
        let countTruncate = 0; // use for ellipsis - truncate left side or right side

        // use for truncate two side
        const numberTruncateLeft = curPage - delta;
        const numberTruncateRight = curPage + delta;

        let active = "";
        for (let pos = 1; pos <= totalPages; pos++) {
            active = pos === curPage ? "active" : "";

            // truncate
            if (totalPages >= 2 * range - 1 && truncate) {
                if (numberTruncateLeft > 3 && numberTruncateRight < totalPages - 3 + 1) {
                    // truncate 2 side
                    if (pos >= numberTruncateLeft && pos <= numberTruncateRight) {
                        renderTwoSide += renderPage(pos, active);
                    }
                } else {
                    // truncate left side or right side
                    if (
                        (curPage < range && pos <= range) ||
                        (curPage > totalPages - range && pos >= totalPages - range + 1) ||
                        pos === totalPages ||
                        pos === 1
                    ) {
                        render += renderPage(pos, active);
                    } else {
                        countTruncate++;
                        if (countTruncate === 1) render += dot;
                    }
                }
            } else {
                // not truncate
                render += renderPage(pos, active);
            }
        }

        if (renderTwoSide) {
            renderTwoSide =
                renderPage(1) + dot + renderTwoSide + dot + renderPage(totalPages);
            pg.innerHTML = renderTwoSide;
        } else {
            pg.innerHTML = render;
        }
    }

    function renderPage(index, active = "") {
        if (window.innerWidth <= 800) {
            if (active == "active")
                return ` Trang ${index}/${pages.value}`;
            else
                return "";
        }
        else {
            return `<li class="pg-item ${active}" data-page="${index}"><a class="pg-link" href="#">${index}</a></li>`;
        }
    }
    function handleCurPage() {
        if (+curPage.value > pages.value) {
            curPage.value = 1;
            valuePage.curPage = 1;
        } else {
            valuePage.curPage = parseInt(curPage.value, 10);
        }
    }
    function handleCheckTruncate() {

        valuePage.truncate = true;

    }

    document.querySelector(".paging").onclick = function (e) {
        handleButton(e.target);
    };

    function handleButton(element) {
        if (element.classList.contains("first-page")) {
            valuePage.curPage = 1;
            fetchData(1);
            btnNextPg.disabled = false;
            btnLastPg.disabled = false;
            btnPrevPg.disabled = true;
            btnFirstPg.disabled = true;
        } else if (element.classList.contains("last-page")) {
            valuePage.curPage = parseInt(pages.value, 10);
            fetchData(parseInt(pages.value, 10));
            btnPrevPg.disabled = false;
            btnFirstPg.disabled = false;
            btnNextPg.disabled = true;
            btnLastPg.disabled = true;
        } else if (element.classList.contains("prev-page")) {
            valuePage.curPage--;
            fetchData(valuePage.curPage);
            handleButtonLeft();
            btnNextPg.disabled = false;
            btnLastPg.disabled = false;
        } else if (element.classList.contains("next-page")) {
            valuePage.curPage++;
            fetchData(valuePage.curPage);
            handleButtonRight();
            btnPrevPg.disabled = false;
            btnFirstPg.disabled = false;
        }
        pagination();
    }
    function handleButtonLeft() {
        if (valuePage.curPage === 1) {
            btnPrevPg.disabled = true;
            btnFirstPg.disabled = true;
        } else {
            btnPrevPg.disabled = false;
            btnFirstPg.disabled = false;
        }
    }
    function handleButtonRight() {
        if (valuePage.curPage === valuePage.totalPages) {
            btnNextPg.disabled = true;
            btnLastPg.disabled = true;
        } else {
            btnNextPg.disabled = false;
            btnLastPg.disabled = false;
        }
    }



    function fetchData(page) {
        var idCategory = $("#idCategory").val();
        smoothScrollingTo(".page__title", function () {
            $(".div-ajax-loading").show();
            clearAndPrepareProductWrapper()
            $.ajax({
                url: '/ajax/ajax.aspx',
                method: 'GET',
                dataType: 'json',
                data: {
                    catid: idCategory,
                    control: 'paging',
                    page: page,
                },
                success: function (data) {
                    setTimeout(function () {
                        //$('.product-list').html(data.responseText);
                        renderProducts(data);
                        //alert(data);
                        $(".div-ajax-loading").hide();
                        handleAjaxSuccess();
                    }, 20);
                },
                error: function () {
                    console.error('Failed to fetch data');
                    $(".div-ajax-loading").hide();
                    handleAjaxSuccess();
                }
            });
        });
    }


    var productWrapper = $('.product__wrapper');

    function clearAndPrepareProductWrapper() {
        //var currentHeight = productWrapper.height();
        //productWrapper.empty();
        //productWrapper.height(currentHeight);
        productWrapper.addClass('loading');
    }

    function handleAjaxSuccess() {
        productWrapper.removeClass('loading');
    }


    function renderProducts(data) {
        var productsContainer = $('#products-container');
        var template = $('#product-template').html();

        productsContainer.empty();

        $.each(data, function (index, product) {
            var renderedTemplate = template.replace(/\{\{(\w+)\}\}/g, function (match, p1) {
                return product[p1];
            });

            productsContainer.append(renderedTemplate);
        });
    }


    function smoothScrollingTo(target, callback) {
        if (target.length) {
            $('html, body').animate({
                scrollTop: $(target).offset().top
            }, 50, 'swing', callback);
            return false;
        }
    }






















    GetAttributeProduct();


    //Load theo category
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

                        htmlContent += "<div>"
                        htmlContent += "<h4 class='sidebar__title'>" + item.Name + "</h4>";

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

                            htmlContent += "<input" + selected + " type='checkbox' class='checkboxAttr' onclick='GetValueFromAttr()' id='checkboxAttr_" + itemChild.ID + "' data-url='" + itemChild.FriendlyUrl + "' data-url-parent='" + itemChild.FriendlyUrlParent + "' data-name='" + itemChild.Name + "' />";
                            if (itemChild.Image != '')
                                htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'><img src='" + itemChild.Image + "'></label><br />";
                            else
                                htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'>" + itemChild.Name + "</label><br />";


                            //console.log(categoryName + " - " + itemChild.Name);

                        }
                        htmlContent += "</div>"
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


    // Hàm lấy Param từ Url
    var getUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
            }
        }
        return false;
    };







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


});


// Load theo Attributes
function GetValueFromAttr() {
    $("#loadByFilter").val("1");
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



    //$.ajax({
    //    url: '/ajax/ajax.aspx',
    //    method: 'GET',
    //    dataType: 'json',
    //    data: {
    //        catid: idCategory,
    //        control: 'paging',
    //        page: page,
    //    },
    //    success: function (data) {
    //        setTimeout(function () {
    //            //$('.product-list').html(data.responseText);
    //            renderProducts(data);
    //            //alert(data);
    //            $(".div-ajax-loading").hide();
    //            handleAjaxSuccess();
    //        }, 20);
    //    },
    //    error: function () {
    //        console.error('Failed to fetch data');
    //        $(".div-ajax-loading").hide();
    //        handleAjaxSuccess();
    //    }
    //});


    jQuery.ajax({
        url: '/ajax/ajax.aspx',
        type: "GET",
        dataType: 'json',
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

                //renderProducts(data);

                $('.product-list').html(response.responseText);
                $(".product-list").css({
                    opacity: 1
                });
                $(".div-ajax-loading").hide();


                //ShowMore(pageIndexShowMore, false);


            }, 500);
        }
    });

}


