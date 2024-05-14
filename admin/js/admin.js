function stringIsEmpty(value) {
    return value ? value.trim().length == 0 : true;
}

function DeleteByID(removeID, table, name) {
    if (confirm('Bạn có chắc chắn muốn xoá?')) {

        $.ajax({
            url: "/admin/ajax/ajax.aspx",
            data: { ctrl: "objlist", removeID: removeID, table: table, ControlName: name, t: Math.random() },
            success: function (html) {
                getval(0);
                if (removeID > 0)
                    GetNotify("Xoá bản ghi thành công!");
            }
        });
    }
    else {
        return false;
    }
}


function getval(removeID) {
    var startDate = $("#fromDate").val();
    var endDate = $("#toDate").val();
    var keyword = $("#keyword").val();
    var sort = $("#sort").val();
    var fieldSort = $("#fieldSort").val();
    var filter = $("#filter").val();
    var pageIndex = $("#pageIndex").val();
    var pageSize = $("#pageSize").val();
    var table = $("#table").val();
    var loadpaging = $("#loadpaging").val();
    var categoryid = $("#categoryid").val();
    var field = $("#field").val();
    var control = $("#control").val();
    var folder = $("#folder").val();
    var controlName = $("#controlName").val();
    var attr_c_id = $("#attr_c_id");
    var flag = $("#flag").val();
    var PositionMenuFlag = $("#PositionMenuFlag").val();
    var AttrProductFlag = $("#AttrProductFlag").val();
    var hideEl = $("#hide");
    var filterJson = "";

    if ($("#filterJson").length)
        filterJson = $("#filterJson").val();


    if ($("#moduls").length) {
        filterJson = '[{"Field":"Moduls","Value":"' + $("#moduls").val() + '","Type":"equal"}]'; // equal là dấu = trong sql chứ không phải like N'%%'
    }
    var hide = "";
    if (hideEl.length) {
        if (hideEl.is(':checked'))
            hide = true;
        else
            hide = false;
    }

    var levelEl = $("#level");

    var level = "";
    if (levelEl.length) {
        if (levelEl.is(':checked'))
            level = true;
        else
            level = false;
    }

    //alert(hide);

    if (attr_c_id.length && !stringIsEmpty(attr_c_id.val())) {
        var filterJson = '[{"Field":"AttributeConfigIDList","Value":"' + attr_c_id.val() + '"}]';
    }
    //var obj = JSON.parse(field);
    //var strResult = "";
    //$.each(obj, function (key, value) {
    //    if (value.Show == true)
    //        strResult += value.Field + ",";
    //});

    //if (strResult.length > 1)
    //    strResult = strResult.slice(0, -1);

    //field = strResult;

    if (removeID > 0) {
        if (confirm('Bạn có chắc chắn muốn xoá ' + removeID)) {
            GetAjax(startDate, endDate, removeID, keyword, sort, fieldSort, filter, categoryid, pageIndex, pageSize, table, field, loadpaging, control, folder, controlName, filterJson, flag, PositionMenuFlag, AttrProductFlag, hide, level);
        }
    }
    else
        GetAjax(startDate, endDate, removeID, keyword, sort, fieldSort, filter, categoryid, pageIndex, pageSize, table, field, loadpaging, control, folder, controlName, filterJson, flag, PositionMenuFlag, AttrProductFlag, hide, level);
}



function GetAjax(startDate, endDate, removeID, keyword, sort, fieldSort, filter, categoryid, pageIndex, pageSize, table, field, loadpaging, control, folder, controlName, filterJson, flag, PositionMenuFlag, AttrProductFlag, hide, level) {
    $("#ajax-content").empty();
    startRealTime = 0;
    $("#div-ajax-loading").show();
    $.ajax({
        url: "/admin/ajax/ajax.aspx",
        data: { ctrl: "objlist", pageSize: pageSize, pageIndex: pageIndex, categoryid: categoryid, startDate: startDate, endDate: endDate, removeID: removeID, key: keyword, filter: filter, sort: sort, fieldSort: fieldSort, table: table, field: field, control: control, folder: folder, ControlName: controlName, filterJson: filterJson, flag: flag, PositionMenuFlag: PositionMenuFlag, AttrProductFlag: AttrProductFlag, hide: hide, level: level, t: Math.random() },
        success: function (html) {
            //$("table.tableData tbody").html(html);
            $(".tableData").html(html);
            $("#div-ajax-loading").hide();

            if (removeID > 0)
                GetNotify("Xoá bản ghi thành công!");
        }
    });


    if (loadpaging == "true") {
        //Paging Info
        $.ajax({
            url: "/admin/ajax/ajax.aspx",
            data: { ctrl: "objlist", pageSize: pageSize, pageIndex: pageIndex, categoryid: categoryid, startDate: startDate, endDate: endDate, removeID: removeID, key: keyword, filter: filter, sort: sort, fieldSort: fieldSort, table: table, field: field, action: "getTotalRecord", control: control, folder: folder, ControlName: controlName, filterJson: filterJson, flag: flag, hide: hide, t: Math.random() },
            //data: { ctrl: "objlist", pageSize: pageSize, pageIndex: pageIndex, categoryid: categoryid, startDate: startDate, endDate: endDate, removeID: removeID, key: keyword, filter: filter, sort: sort, table: table, field: field, action: "getTotalRecord", t: Math.random() },

            success: function (html) {
                var totalRecord = parseInt(html);
                GetPaging(totalRecord, '1')
            }
        });
    }
}


$('#delete').click(function () {
    var count = $('table').find('input[type="checkbox"]:checked').length;
    if (count == 0) {
        alert("Vui lòng chọn đối tượng cần xóa trước khi thực hiện thao tác")
    }
    else {
        if (confirm("Bạn có chắc chắn muốn xóa " + count + " mục?")) {
            $('table').find('input[type="checkbox"]:checked').each(function () {
                getval($(this).attr("id").replace("select_", ""));
                getval(0);
            });
        }
    }
});


function ShowLoading() {
    $("#div-ajax-loading").show();
}

function HideLoading() {
    $("#div-ajax-loading").hide();
}



function GetPaging(totalRecord, pageIndex) {
    $.ajax({
        url: "/admin/ajax/ajax.aspx",
        data: { ctrl: "paging", totalRecord: totalRecord, pageIndex: pageIndex },
        success: function (html) {
            $("#paging").html(html);
        }
    });
}

$('#refresh').click(function () {
    $("#fromDate").val("");
    $("#toDate").val("");
    $("#keyword").val("");
    $("#sort").val("");
    $("#filter").val("");
    $("#categoryid").val("");
    $("#drCategory").val("");
    $("#pageIndex").val("1");
    getval(0);
});


if ($("#keyword").length) {
    window.onload = () => {
        function ajax(data) {
            console.log(new Date().toLocaleTimeString() + ' - ' + data)
            getval(0);
        }

        function debounce(fn, delay) {
            return args => {
                clearTimeout(fn.id)

                fn.id = setTimeout(() => {
                    fn.call(this, args)
                }, delay)
            }
        }

        const debounceAjax = debounce(ajax, 1000);

        document.querySelector('#keyword').addEventListener('keyup', e => {
            $("#div-ajax-loading").show();
            debounceAjax(e.target.value)
        })
    }

}

$('#calendar').daterangepicker({
    "minYear": 2020,
    "maxYear": 2022,
    "showWeekNumbers": false,
    "showISOWeekNumbers": false,
    startDate: moment().startOf('month'),
    endDate: moment().endOf('month'),
    ranges: {
        'All time': [moment('1970-01-01'), moment()],
        'Hôm nay': [moment(), moment()],
        'Hôm qua': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
        '7 ngày qua': [moment().subtract(6, 'days'), moment()],
        '30 ngày qua': [moment().subtract(29, 'days'), moment()],
        'Tháng này': [moment().startOf('month'), moment().endOf('month')],
        'Tháng trước': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
    },
    "linkedCalendars": true,
    "autoUpdateInput": false,
    "opens": "left"
}, function (start, end) {

    $('#calendar').html(start.format('DD/MM/YYYY') + ' đến ' + end.format('DD/MM/YYYY'));
    $("#fromDate").val(start.format('YYYY-MM-DD'));
    $("#toDate").val(end.format('YYYY-MM-DD'));
    getval(0);
});




$(document).ready(function () {
    $('.tableData').on('click', 'th.sort', function () {
        var field = $(this).attr("data-field");
        var sort = $(this).attr("data-sort");
        $('#sort').val(sort);
        $('#fieldSort').val(field);
        $("#pageIndex").val("1");
        $("#loadpaging").val("true");
        getval(0);
    });
});



/**
 * Category
 */

if ($('#drCategory') && $('#categoryname') && $('#categoryid')) {
    var text_value = $('#categoryname');
    var id_value = $('#categoryid');
    var multilevel = $('#drCategory').attr("data-level");

    var data2 = [];
    var idArr = [];
    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }

        $("#drCategory").select2({
            tags: true,
            tokenSeparators: [' ', ' '],
            data: data2
        });

        $('#drCategory > option').prop("selected", true);
    }

    $('#drCategory').select2({
        placeholder: 'Chọn danh mục',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var query = { key: params.term, ctrl: "select2", multilevel: "1", table: "tblCategories", attr: "category", filterJson: '[{"Field":"Moduls","Value":"category", "Type":"equal"}]', t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });


    $('#drCategory').change(function () {
        var idSelect = $(this).select2("val");
        $('#categoryid').val(idSelect);

        //if ($("#attrCheckValue").lengh)


        if ($(".tableData").length) {
            $("#loadpaging").val("true");
            getval(0);
        }
        else {
            GetAttributeProduct(idSelect);
        }

        //var textSelect = $("#drCategory option:selected").text();
        //$('#categoryname').val(textSelect);
    });
}



/**
 * Attributes
 */

if ($('#drAttr') && $('#attr_name') && $('#attr_id')) {
    var text_value = $('#attr_name');
    var id_value = $('#attr_id');


    var data2 = [];
    var idArr = [];

    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }
        $("#drAttr").select2({
            tags: true,
            tokenSeparators: [',', ' '],
            data: data2
        });
        $('#drAttr > option').prop("selected", true);
    }

    $('#drAttr').select2({
        placeholder: 'Chọn thuộc tính',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var multilevel = $('#drAttr').attr("data-level");
                console.log("multilevel: " + multilevel);
                var query = { tagSearch: params.term, ctrl: "select2", multilevel: multilevel, table: "tblAttributes", t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });


    $('#drAttr').change(function () {

        var idSelect = $(this).select2("val");
        $('#attr_id').val(idSelect);
        console.log("---" + idSelect);
        GetAttributeProduct(idSelect)

        //if ($("table.tableData").length) {
        //    $("#loadpaging").val("true");
        //    getval(0);
        //}
    });
}


/**
 * Attributes Config
 */

if ($('#drAttrC') && $('#attr_c_name') && $('#attr_c_id')) {
    var text_value = $('#attr_c_name');
    var id_value = $('#attr_c_id');

    var data2 = [];
    var idArr = [];

    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }

        $("#drAttrC").select2({
            tags: true,
            tokenSeparators: [',', ' '],
            data: data2
        });

        $('#drAttrC > option').prop("selected", true);
    }

    $('#drAttrC').select2({
        placeholder: 'Chọn trạng thái',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var multilevel = $('#drAttrC').attr("data-level");
                var folder = $('#drAttrC').attr("data-folder");
                var id_return = $('#drAttrC').attr("data-idreturn");
                var query = { tagSearch: params.term, ctrl: "select2", folder: folder, multilevel: multilevel, id_return: id_return, table: "tblAttributeConfigs", t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });


    $('#drAttrC').change(function () {
        var idSelect = $(this).select2("val");
        $('#attr_c_id').val(idSelect);


        if ($("table.tableData").length) {
            $("#loadpaging").val("true");
            getval(0);
        }
    });
}



/**
 * Tags
 */

if ($('#drTag') && $('#tag_name') && $('#tag_id')) {
    var text_value = $('#tag_name');
    var id_value = $('#tag_name');

    var data2 = [];
    var idArr = [];

    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }

        $("#drTag").select2({
            tags: true,
            tokenSeparators: [',', ' '],
            data: data2
        });

        $('#drTag > option').prop("selected", true);
    }

    $('#drTag').select2({
        placeholder: 'Chọn Tags',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var multilevel = $('#drTag').attr("data-level");
                var folder = $('#drTag').attr("data-folder");
                var id_return = $('#drTag').attr("data-idreturn");

                var query = { key: params.term, ctrl: "select2", folder: folder, multilevel: multilevel, moduls: "tag", id_return: id_return, table: "tblCategories", t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });


    $('#drTag').change(function () {
        var idSelect = $(this).select2("val");
        $('#tag_name').val(idSelect);


        if ($("table.tableData").length) {
            $("#loadpaging").val("true");
            getval(0);
        }
    });
}


/**
 * Hash Tags
 */

if ($('#drHashTag') && $('#hashtag_name') && $('#hashtag_id')) {
    var text_value = $('#hashtag_name');
    var id_value = $('#hashtag_name');

    var data2 = [];
    var idArr = [];

    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }

        $("#drHashTag").select2({
            tags: true,
            tokenSeparators: [',', ' '],
            data: data2
        });

        $('#drHashTag > option').prop("selected", true);
    }

    $('#drHashTag').select2({
        placeholder: 'Chọn tag ẩn',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var multilevel = $('#drHashTag').attr("data-level");
                var folder = $('#drHashTag').attr("data-folder");
                var id_return = $('#drHashTag').attr("data-idreturn");
                //var filterJson = '[{"Field":"AttributeConfigUrlList","Value":"_hashTagType"}]';
                var query = { key: params.term, ctrl: "select2", folder: folder, multilevel: multilevel, moduls: "hashtag", id_return: id_return, table: "tblCategories", t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });


    $('#drHashTag').change(function () {
        var idSelect = $(this).select2("val");
        $('#hashtag_name').val(idSelect);


        if ($("table.tableData").length) {
            $("#loadpaging").val("true");
            getval(0);
        }
    });
}




/**
 * Attributes Config - List Page
 */

if ($('#drAttrC_List') && $('#attr_c_name') && $('#attr_c_id')) {
    var text_value = $('#attr_c_name');
    var id_value = $('#attr_c_id');

    var data2 = [];
    var idArr = [];

    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }

        $("#drAttrC_List").select2({
            width: 'resolve',
            tags: true,
            tokenSeparators: [',', ' '],
            data: data2
        });

        $('#drAttrC_List > option').prop("selected", true);
    }

    $('#drAttrC_List').select2({
        width: 'resolve',
        placeholder: 'Chọn thuộc tính',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var multilevel = $('#drAttrC_List').attr("data-level");
                var folder = $('#drAttrC_List').attr("data-folder");
                var id_return = $('#drAttrC_List').attr("data-idreturn");
                var query = { tagSearch: params.term, ctrl: "select2", folder: folder, multilevel: multilevel, id_return: id_return, table: "tblAttributeConfigs", t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });


    $('#drAttrC_List').change(function () {
        var idSelect = $(this).select2("val");
        $('#attr_c_id').val(idSelect);
        if ($("table.tableData").length) {
            $("#loadpaging").val("true");
            getval(0);
        }
    });
}






/**
 * Moduls
 */

if ($('#drModuls') && $('#modul_name') && $('#modul_id')) {
    var text_value = $('#modul_name');
    var id_value = $('#modul_id');

    var data2 = [];
    var idArr = [];

    if (text_value.val() !== undefined && id_value.val() !== undefined) {
        var tmpArr = text_value.val().split(',');
        var tmpArr1 = id_value.val().split(',');
        for (var i = 0; i < tmpArr.length; i++) {
            data2.push({
                'id': tmpArr1[i],
                'text': tmpArr[i]
            });
        }
        $("#drModuls").select2({
            tags: true,
            tokenSeparators: [',', ' '],
            data: data2
        });
        $('#drModuls > option').prop("selected", true);
    }

    $('#drModuls').select2({
        placeholder: 'Chọn moduls',
        tags: true,
        ajax: {
            url: '/admin/ajax/ajax.aspx',
            dataType: 'json',
            delay: 0,
            data: function (params) {
                var multilevel = $('#drModuls').attr("data-level");
                console.log("multilevel: " + multilevel);
                var query = { tagSearch: params.term, ctrl: "select2", multilevel: multilevel, table: "tblAttributeConfigs", filter: "Moduls_attributeconfig", id_return: "FriendlyUrl", t: Math.random() }; return query;
            },
            processResults: function (data) {
                console.log(data);
                return { results: data };
            },
            cache: true
        }
    });

    $('#drModuls').change(function () {
        var idSelect = $(this).select2("val");
        $('#modul_id').val(idSelect);
        console.log("---" + idSelect);

        //if ($("table.tableData").length) {
        //    $("#loadpaging").val("true");
        //    getval(0);
        //}
    });
}



InitDropzone();

Dropzone.autoDiscover = false;
Dropzone.prototype.defaultOptions.dictRemoveFile = "Xoá ảnh";
function InitDropzone() {
    var gallery = $("#gallery").val();

    //alert(friendly.val());


    //var fUrl = "image";




    $(".dropzone").dropzone({
        url: "/Controls/UploadFile.ashx",
        params: { 'folder': $(".dropzone").attr("data-folder") },
        acceptedFiles: "image/*",
        addRemoveLinks: true,
        maxFiles: 30,
        autoProcessQueue: true,
        success: function (file, response) {
            file.serverId = response;
            var imgName = response;
            if (gallery == "")
                gallery = JSON.parse("[]");
            else
                gallery = JSON.parse($("#gallery").val());

            gallery.push(
                { Name: imgName, Path: "/upload/" + $(".dropzone").attr("data-folder") + "/" + imgName, Size: file.size }
            );

            var strJson = JSON.stringify({ Name: imgName, Path: "/upload/" + $(".dropzone").attr("data-folder") + "/" + imgName, Size: file.size });

            $("#gallery").val(JSON.stringify(gallery));

            file.previewElement.classList.add("dz-success");
            $(file.previewElement).attr("data-json", strJson);
            console.log("Successfully uploaded :" + imgName);
        },
        error: function (file, response) {
            file.previewElement.classList.add("dz-error");
            $(file.previewElement).find('.dz-error-message').text("Có lỗi");
        },
        removedfile: function (file) {
            console.log(file);

            var fileName = file.serverId;
            if (fileName === undefined || fileName === null) {
                fileName = file.name;
            }
            var result = confirm("Bạn có chắc chắn xoá ảnh?" + fileName);
            if (result) {

                file.previewElement.remove();
                var hdfJson = JSON.parse($("#gallery").val());
                const dataRemoved = hdfJson.filter((el) => {
                    return el.Name !== fileName;
                });

                $("#gallery").val(JSON.stringify(dataRemoved));

                var hdf = $("#gallery").val();
                $("#gallery").val(hdf.replace(file.serverId + ",", ""));


                console.log(fileName + ": removed");
                console.log($("#gallery").val());
            }
        },
        init: function () {


            if (gallery !== "") {
                var thisDropzone = this;
                var jSonData = JSON.parse(gallery);
                var index = 0;
                $.each(jSonData, function (key, data) {
                    var mockFile = { name: data.Name, size: data.Size };
                    thisDropzone.emit("addedfile", mockFile);
                    thisDropzone.emit("thumbnail", mockFile, data.Path);
                    thisDropzone.emit("complete", mockFile);
                    $(".dz-preview").eq(index).attr("data-json", JSON.stringify(data));
                    index++;
                })
            }

            this.on("sending", function (file, xhr, formData) {
                var fUrl = "img";
                var friendly = $("#friendlyurl");
                if (friendly.length) {
                    fUrl = friendly.val();
                }

                formData.append("pname", fUrl);

            });
        }
    });



    var Path = "/upload/img/";
    $(".dropzone").sortable({
        update: function (event, ui) {
            var jSonAll = JSON.parse("[]");
            $.each($('.dz-preview'), function () {
                var currJson = JSON.parse($(this).attr("data-json"));
                jSonAll.push(currJson);
            });
            $("#gallery").val(JSON.stringify(jSonAll));
        }
    });
}

if ($("#fileavatar").lengh) {
    //alert($("#fileavatar"));

    //window.addEventListener('load', function () {
    //    $("#fileavatar")[0].addEventListener('change', function () {
    //        if (this.files && this.files[0]) {
    //            var img = $("#myImg")[0];
    //            img.src = URL.createObjectURL(this.files[0]);
    //            img.onload = imageIsLoaded;
    //            $("#image").val($("#fileavatar").val().split('\\').pop());
    //        }
    //    });
    //});
}


if ($("#name").length && $("#friendlyurl").length) {
    var run = 0;
    window.onload = () => {
        function ajax(data) {
            //console.log(new Date().toLocaleTimeString() + ' - ' + data)
            var fUrl = $("#friendlyurl");
            var name = $("#name");
            if (fUrl.val().trim().length == 0 || run == 1) {
                run = 1;
                $.ajax({
                    url: "/admin/ajax/ajax.aspx",
                    data: { ctrl: "dynamic", key: name.val(), Action: 'text-unsign', t: Math.random() },
                    success: function (html) {
                        fUrl.val(html);
                        //InitDropzone();
                    }
                });
            }
        }

        function debounce(fn, delay) {
            return args => {
                clearTimeout(fn.id)

                fn.id = setTimeout(() => {
                    fn.call(this, args)
                }, delay)
            }
        }

        const debounceAjax = debounce(ajax, 20);

        document.querySelector("#name").addEventListener('keyup', e => {
            debounceAjax(e.target.value)
        })

        document.querySelector("#friendlyurl").addEventListener('keyup', e => {
            run = 0;
        })
    }
}



function ValidateForm() {
    if (!$("#frm_edit").validate().form()) {
        return false;
    } else {
        return true;
    }



    //$("#frm_edit").validate({
    //    submitHandler: function (form) {
    //        return true;
    //    }
    //});
    //return false;
}


var notice_info;

var cookie_notice = $.cookie("notice");
//alert(cookie_notice);
if (cookie_notice == "delete_success")
    notice_info = "Xoá bản ghi thành công";
else if (cookie_notice == "insert_success")
    notice_info = "Dữ liệu đã được Thêm mới thành công";
else if (cookie_notice == "update_success")
    notice_info = "Dữ liệu đã được cập nhật thành công";
else if (cookie_notice == "update_copy_success")
    notice_info = "Dữ liệu đã được cập nhật & nhân bản, bạn có thể tiếp tục chỉnh sửa và thêm mới";
else if (cookie_notice == "insert_copy_success")
    notice_info = "Dữ liệu đã được thêm mới & nhân bản, bạn có thể tiếp tục chỉnh sửa và thêm mới";
else if (cookie_notice == "login_success")
    notice_info = "Đăng nhập thành công";
else if (cookie_notice == "login_error")
    notice_info = "Đăng nhập không thành công";
else if (cookie_notice == "forgotpass_sendmail_success")
    notice_info = "Check email và làm theo hướng dẫn để lấy lại mật khẩu";
else if (cookie_notice == "forgotpass_sendmail_error")
    notice_info = "Lỗi! Email không tồn tại";

if (cookie_notice !== undefined && cookie_notice !== null) {
    var className = "success";
    if (cookie_notice.indexOf("_error") != -1)
        className = "error";
    GetNotify(notice_info, className);
    $.removeCookie('notice', { path: '/' });
}

var cookie_notice_text_success = $.cookie("notice_success");
if (cookie_notice_text_success !== undefined && cookie_notice_text_success !== null) {
    GetNotify(cookie_notice_text_success, "success");
    $.removeCookie('notice_success', { path: '/' });
}

var cookie_notice_text_error = $.cookie("notice_error");
if (cookie_notice_text_error !== undefined && cookie_notice_text_error !== null) {
    GetNotify(cookie_notice_text_error, "error");
    $.removeCookie('notice_error', { path: '/' });
}

function GetNotify(notice_info, className) {
    className = (typeof className !== 'undefined') ? className : "success";
    toastr[className](notice_info);
}

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "rtl": false,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": 300,
    "hideDuration": 1000,
    "timeOut": 5000,
    "extendedTimeOut": 1000,
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}



$(function () {
    var $form = $("#frm_edit");
    var $input = $form.find(".price");

    if ($form.length && $input.length) {
        $input.on("keyup", function (event) {
            // When user select text in the document, also abort.
            var selection = window.getSelection().toString();
            if (selection !== '') {
                return;
            }
            // When the arrow keys are pressed, abort.
            if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
                return;
            }
            var $this = $(this);
            // Get the value.
            var input = $this.val();
            var input = input.replace(/[\D\s\._\-]+/g, "");
            input = input ? parseInt(input, 10) : 0;
            $this.val(function () {
                return (input === 0) ? "" : input.toLocaleString("en-US");
            });
        });
    }
});



$("span.select2").attr("style", "float:right !important;width:200px");


/*
   Sửa giá
*/

$(".tableData").on("click", "td.price", function () {
    var edit = $(this).find(".price-edit-edit");
    var entertoupdate = $(this).find(".entertoupdate");
    $(this).find("span").hide();
    $(this).find(".edit-price").show();
    $(this).find(".edit-price").select();
    entertoupdate.show();
    edit.hide();
});
$(document).on("focusout", "td.price input", function () {
    SuaGiaChuaXong($(this));
});
$(document).on("keyup", "td.price input", function (event) {
    if (event.keyCode == 13) {
        SuaGiaXong($(this));
    }
    var selection = window.getSelection().toString();
    if (selection !== '') {
        return;
    }
    if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
        return;
    }
    var $this = $(this);
    var input = $this.val();
    var input = input.replace(/[\D\s\._\-]+/g, "");
    input = input ? parseInt(input, 10) : 0;
    $this.val(function () {
        return (input === 0) ? "" : input.toLocaleString("en-US");
    });
});


function SuaGiaXong(el) {
    var newPrice = el.val().replace(",", ".").replace(",", ".").replace(",", ".").replace(",", ".").replace(",", ".");
    var newPriceAjax = newPrice.replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
    if (typeof (newPriceAjax) == "undefined" || newPriceAjax=="")
        newPriceAjax = "0";
    console.log("newPrice: " + newPrice);
    el.hide();
    el.parent().find("span").show();
    el.parent().find("span").text(newPrice + " VNĐ");
    var pid = el.attr("data-id");
    var field = el.attr("data-field");
    var table = $("#table").val();
    $.ajax({
        url: "/admin/ajax/ajax.aspx",
        data: { ctrl: "dynamic", Action: "update-price", table: table, field: field, newPrice: newPriceAjax, pid: pid, t: Math.random() },
        success: function (html) {
            GetNotify("Giá đã được cập nhật thành " + newPrice);
        }
    });
    var edit = el.parent().find(".price-edit-edit");
    var entertoupdate = el.parent().find(".entertoupdate");
    entertoupdate.hide();
    edit.show();
}

function SuaGiaChuaXong(el) {
    el.hide();
    el.parent().find("span").show();
    var edit = el.parent().find(".price-edit-edit");
    var entertoupdate = el.parent().find(".entertoupdate");
    entertoupdate.hide();
    edit.show();
}

/*
   Sửa sắp xếp
*/

$(".tableData").on("click", "td.sort", function () {
    var edit = $(this).find(".price-edit-edit");
    var entertoupdate = $(this).find(".entertoupdate");
    $(this).find("span").hide();
    $(this).find(".edit-sort").show();
    $(this).find(".edit-sort").select();
    entertoupdate.show();
    edit.hide();
});
$(document).on("focusout", "td.sort input", function () {
    SuaSortChuaXong($(this));
});
$(document).on("keyup", "td.sort input", function (event) {
    if (event.keyCode == 13) {
        SuaSortXong($(this));
    }
    var selection = window.getSelection().toString();
    if (selection !== '') {
        return;
    }
    if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
        return;
    }
    var $this = $(this);
    var input = $this.val();
    var input = input.replace(/[\D\s\._\-]+/g, "");
    input = input ? parseInt(input, 10) : 0;
    $this.val(function () {
        return (input === 0) ? "" : input.toLocaleString("en-US");
    });
});
function SuaSortXong(el) {
    var newSort = el.val().replace(",", ".").replace(",", ".").replace(",", ".").replace(",", ".").replace(",", ".");
    var newSortAjax = newSort.replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
    console.log("new sort: " + newSort);
    el.hide();
    el.parent().find("span").show();
    el.parent().find("span").text(newSort);
    var pid = el.attr("data-id");
    var table = $("#table").val();

    $.ajax({
        url: "/admin/ajax/ajax.aspx",
        data: { ctrl: "dynamic", Action: "update-sort", table: table, newSort: newSortAjax, pid: pid, t: Math.random() },
        success: function (html) {
            GetNotify("Cập nhật vị trí " + newSort);
        }
    });

    var edit = el.parent().find(".price-edit-edit");
    var entertoupdate = el.parent().find(".entertoupdate");
    entertoupdate.hide();
    edit.show();
}
function SuaSortChuaXong(el) {
    el.hide();
    el.parent().find("span").show();
    var edit = el.parent().find(".price-edit-edit");
    var entertoupdate = el.parent().find(".entertoupdate");
    entertoupdate.hide();
    edit.show();
}

//function GenHeaderTable() {
//    var field = $('#field').val();
//    var obj = JSON.parse(field);
//    var strResult = "";
//    $.each(obj, function (key, value) {
//        console.log(key + "-" + value);
//        if (value.Show == true) {
//            if (value.Field == "CreatedDate")
//                strResult += '<th class="sort" data-field="CreatedDate">Tạo bởi</th>';
//            else if (value.Field == "EditedDate")
//                strResult += '<th class="sort" data-field="EditedDate">Sửa bởi</th>';
//            else if (value.Field == "CreatedBy" || value.Field == "EditedBy")
//                strResult += "";
//            else
//                strResult += '<th class="sort" data-field="' + value.Field + '">' + value.Text + '</th>';
//        }
//    });
//    strResult += '<th>Thao tác</th>';
//    $(".tableData thead").html(strResult);
//}




$(".get_ck").click(function () {

    var thumbnail_image_name = $(this).parent().attr("data-thumb");
    var input_text_name = $(this).parent().attr("data-inputtext");

    var input_text = $("#" + input_text_name);
    var thumbnail_image = $("#" + thumbnail_image_name);

    var folder = $(this).parent().attr("data-folder");
    var startupPath = "Images:/" + folder + "/";

    var finder = new CKFinder();
    finder.startupPath = startupPath;
    finder.selectActionFunction = function (fileUrl, data) {
        input_text.val(fileUrl);
        thumbnail_image.attr("src", fileUrl);
    };
    finder.selectActionData = input_text;
    finder.popup();

});

/*
    Meta Seo
*/

var metatitle = $("#metatitle");
var metakeyword = $("#metakeyword");
var metadescription = $("#metadescription");

var google_demo = $(".google-demo");
var google_demo_title = $(".google-demo .title");
var google_demo_description = $(".google-demo .description");

if (metatitle.length && metakeyword.length && metadescription.length && google_demo.length && google_demo_title.length && google_demo_description.length) {
    var count_metatitle = metatitle.parent().find(".count-char");
    var count_metakeyword = metakeyword.parent().find(".count-char");
    var count_metadescription = metadescription.parent().find(".count-char");

    var metakeyword = $("#metakeyword");
    var metadescription = $("#metadescription");

    if (metatitle.val().length > 3) {
        google_demo.show();
        google_demo_title.html(metatitle.val());
    }

    if (metadescription.val().length > 3) {
        google_demo.show();
        google_demo_description.html(metadescription.val());
    }


    metatitle.keyup(function () {
        var title_leng = $(this).val().length;
        count_metatitle.show();
        count_metatitle.html(title_leng);
        if ($(this).val().length > 70)
            count_metatitle.css("background", "red");
        else
            count_metatitle.css("background", "#0db110");
        google_demo_title.html($(this).val())
        google_demo.show();
    });

    metadescription.keyup(function () {
        var description_leng = $(this).val().length;
        count_metadescription.show();
        count_metadescription.html(description_leng);
        if ($(this).val().length > 300)
            count_metadescription.css("background", "red");
        else
            count_metadescription.css("background", "#0db110");
        google_demo_description.html($(this).val())
        google_demo.show();
    });

    metakeyword.keyup(function () {
        var keyword_leng = $(this).val().length;
        count_metakeyword.show();
        count_metakeyword.html(keyword_leng);
        if ($(this).val().length > 300)
            count_metakeyword.css("background", "red");
        else
            count_metakeyword.css("background", "#0db110");
    });

    if ($("#name").length) {
        $("#name").focusout(function () {
            if (metatitle.val().length == 0 && $(this).val().length > 10) {
                metatitle.html($(this).val());
                google_demo_title.html($(this).val());
            }
            google_demo.show();
        });
    }
}

$("#menu").click(function () {
    var $window = $(window);
    var windowsize = $window.width();
    if (windowsize > 900) {
        var margin_r;
        var margin_l;
        var width_l;

        var logo;

        if ($('.right-panel').css('margin-left') == "0px") {
            margin_r = '230px';
            width_l = '230px';
            margin_l = '0px';
            logo = 'block';
        } else {
            margin_r = '0px';
            width_l = '0px';
            logo = 'none';
            margin_l = '-230px';
        }

        $(".right-panel").animate({
            "margin-left": margin_r,
        }, 500);

        $(".left-panel").animate({
            "margin-left": margin_l,
        }, 500);

        $(".logo").animate({
            "display": logo,
        }, 500);
    }
    else {
        if ($(".left-panel").width() == 0) {
            $(".left-panel").show();
            $(".left-panel").width(230);
        }
        else {
            $(".left-panel").hide();
            $(".left-panel").width(0);
        }
    }

 


});


if ($(".datepicker").length) {
    $('.datepicker').datetimepicker({
        ownerDocument: document,
        contentWindow: window,

        value: '',
        rtl: false,

        format: 'd/m/Y H:i',
        formatTime: 'H:i',
        formatDate: 'd/m/Y',

        startDate: false, // new Date(), '1986/12/08', '-1970/01/05','-1970/01/05',
        step: 60,
        monthChangeSpinner: true,

        closeOnDateSelect: false,
        closeOnTimeSelect: true,
        closeOnWithoutClick: true,
        closeOnInputClick: true,
        openOnFocus: true,

        timepicker: true,
        datepicker: true,
        weeks: false,

        defaultTime: false, // use formatTime format (ex. '10:00' for formatTime: 'H:i')
        defaultDate: false, // use formatDate format (ex new Date() or '1986/12/08' or '-1970/01/05' or '-1970/01/05')

        minDate: false,
        maxDate: false,
        minTime: false,
        maxTime: false,
        minDateTime: false,
        maxDateTime: false,

        allowTimes: [],
        opened: false,
        initTime: true,
        inline: false,
        theme: '',
        touchMovedThreshold: 5,

        onSelectDate: function () { },
        onSelectTime: function () { },
        onChangeMonth: function () { },
        onGetWeekOfYear: function () { },
        onChangeYear: function () { },
        onChangeDateTime: function () { },
        onShow: function () { },
        onClose: function () { },
        onGenerate: function () { },

        withoutCopyright: true,
        inverseButton: false,
        hours12: false,
        next: 'xdsoft_next',
        prev: 'xdsoft_prev',
        dayOfWeekStart: 0,
        parentID: 'body',
        timeHeightInTimePicker: 25,
        timepickerbar: true,
        todayButton: true,
        prevButton: true,
        nextButton: true,
        defaultSelect: true,

        scrollMonth: true,
        scrollTime: true,
        scrollInput: true,

        lazyInit: false,
        mask: false,
        validateOnBlur: true,
        allowBlank: true,
        yearStart: 1950,
        yearEnd: 2050,
        monthStart: 0,
        monthEnd: 11,
        style: '',
        id: '',
        fixed: false,
        roundTime: 'round', // ceil, floor
        className: '',
        weekends: [],
        highlightedDates: [],
        highlightedPeriods: [],
        allowDates: [],
        allowDateRe: null,
        disabledDates: [],
        disabledWeekDays: [],
        yearOffset: 0,
        beforeShowDay: null,

        enterLikeTab: true,
        showApplyButton: false
    });
}


$(".left").resizable();
$('.left').resize(function () {
    $('.right').width($("#parent").width() - $(".left").width());
});
$(window).resize(function () {
    $('.right').width($("#parent").width() - $(".left").width());
    //$('.left').height($("#parent").height());
});

//resizeDiv();
//function resizeDiv() {
//    vpw = $(window).width();
//    vph = $(window).height();
//    $(".left-panel").css({"height": vph + "px"});
//}

if ($(".expan").length) {
    $(".expan").click(function () {
        $(this).parent().find("ul").slideToggle();
    });
}


$(document).ready(function () {
    $(".freezetable").freezeTable({
        'scrollBar': true,
    });

    //$(".content-list").freezeTable({
    //    freezeHead: true,
    //    freezeColumn: true,
    //    freezeColumnHead: true,
    //    scrollBar: false,
    //    fixedNavbar: '.navbar-fixed-top',
    //    namespace: 'freeze-table',
    //    container: false,
    //    scrollable: false,
    //    columnNum: 1,
    //    columnKeep: false,
    //    columnBorderWidth: 1,
    //    columnWrapStyles: null,
    //    headWrapStyles: null,
    //    columnHeadWrapStyles: null,
    //    backgroundColor: 'white',
    //    shadow: false,
    //    fastMode: false,
    //    callback: null
    //    });
});



if ($(".content-list").length) {
    if ($("#hide").length) {
        $("#hide").change(function () {
            getval(0);
        });
    }

    if ($("#level").length) {
        $("#level").change(function () {
            getval(0);
        });
    }
}



function queryString(parameter) {
    var loc = location.search.substring(1, location.search.length);
    var param_value = false;
    var params = loc.split("&");

    for (i = 0; i < params.length; i++) {
        param_name = params[i].substring(0, params[i].indexOf('='));
        if (param_name == parameter) {
            param_value = params[i].substring(params[i].indexOf('=') + 1)
        }
    }

    if (param_value) {
        return param_value;
    }
    else {
        return false; //Here determine return if no parameter is found
    }
}

document.addEventListener("DOMContentLoaded", function (event) {
    document.querySelectorAll('img').forEach(function (img) {
        img.onerror = function () { this.style.display = 'none'; };
    })
});


if ($("#name").length && $("#friendlyurl").length) {
    var run = 0;
    window.onload = () => {
        function ajax(data) {
            var fUrl = $("#friendlyurl");
            if (fUrl.val().trim().length == 0 || run == 1) {
                run = 1;
                GetAjaxUrl(false);
            }
        }

        function debounce(fn, delay) {
            return args => {
                clearTimeout(fn.id)

                fn.id = setTimeout(() => {
                    fn.call(this, args)
                }, delay)
            }
        }

        const debounceAjax = debounce(ajax, 20);

        document.querySelector("#name").addEventListener('keyup', e => {
            debounceAjax(e.target.value)
        })

        document.querySelector("#friendlyurl").addEventListener('keyup', e => {
            run = 0;
            GetAjaxUrl(true);
        });

        document.querySelector("#apply_url").addEventListener('click', e => {
            run = 1;
            GetAjaxUrl(false);
        });

        function GetAjaxUrl(isCheck) {
            var fUrl = $("#friendlyurl");
            var table = $("#table");
            var url_valid = $("#url_valid");
            var url_invalid = $("#url_invalid");
            var current_id = $("#id");
            var name = $("#name");
            var hdfUrlValid = $("#hdfUrlValid");
            if (isCheck)
                name = fUrl;

            $.getJSON('/admin/ajax/ajax.aspx', { ctrl: "dynamic", key: name.val(), id: current_id.val(), table: table.val(), Action: 'text-unsign', t: Math.random() }, function (data) {
                var jsonContent = JSON.parse(JSON.stringify(data));
                $.each(data, function (key, value) {
                    if (key == "esixt") {
                        if (value == "1") {
                            url_valid.css({ "display": "none" });
                            url_invalid.css({ "display": "block" });
                            hdfUrlValid.val("0");
                        }
                        else if (value == "0") {
                            url_valid.css({ "display": "block" });
                            url_invalid.css({ "display": "none" });
                            hdfUrlValid.val("1");
                        }
                    }
                    if (!isCheck) {
                        if (key == "url")
                            fUrl.val(value);
                    }
                });
            });
        }
    }
}