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

                    htmlContent += "<div>"
                    htmlContent += "<div class='title-sidebar'>" + item.Name + "</div>";

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
