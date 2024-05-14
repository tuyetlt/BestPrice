<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderLoad.ascx.cs" Inherits="admin_Controls_HeaderLoad" %>
<link rel="stylesheet" type="text/css" href="<%= Utils.CheckVersion_NonTemplate("/admin/DonHang/css/daterangepicker.css") %>" />
<script src="/assets/js/jquery-3.4.1.min.js"></script>
<link href="/assets/js/select2/select2.min.css" rel="stylesheet" />
<script src="/assets/js/select2/select2.min.js"></script>
<script src="/assets/js/select2/jquery.multi-select.min.js"></script>


<script src="/assets/Chart/js/code/highcharts.js"></script>
<script src="/assets/Chart/js/code/highcharts-3d.js"></script>
<script src="/assets/Chart/js/code/modules/cylinder.js"></script>
<script src="/assets/Chart/js/code/modules/exporting.js"></script>
<script src="/assets/Chart/js/code/modules/export-data.js"></script>
<script src="/assets/Chart/js/code/modules/accessibility.js"></script>


<script type="text/javascript" src="/admin/js/moment.min.js"></script>
<script type="text/javascript" src="/admin/js/daterangepicker.min.js"></script>
<script type="text/javascript" src="/admin/js/jquery.popupWindow.js"></script>
<link rel="stylesheet" type="text/css" href="<%= Utils.CheckVersion_NonTemplate("/admin/css/daterangepicker.css") %>" />


<link rel="stylesheet" href="/assets/css/jquery-ui.css">
<script src="/assets/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="/assets/ckeditor/ckeditor.js"></script>

<script type="text/javascript" src="/assets/js/jquery.twbsPagination.min.js"></script>
<script type="text/javascript" src="/assets/ckfinder/ckfinder.js"></script>
<script type="text/javascript" src="/assets/js/dropzone/dropzone.min.js"></script>
<script type="text/javascript" src="/assets/js/jquery.validate.min.js"></script>
<script type="text/javascript" src="/assets/js/jquery.cookie.min.js"></script>
<script type="text/javascript" src="/assets/js/datetimepicker/jquery.datetimepicker.full.js"></script>
<script type="text/javascript" src="/assets/js/freeze-table.js"></script>
<script type="text/javascript" src="/assets/js/toastr-master/toastr.js"></script>


<link rel="stylesheet" type="text/css" href="<%= Utils.CheckVersion_NonTemplate("/assets/js/datetimepicker/jquery.datetimepicker.min.css") %>" />


<link rel="stylesheet" type="text/css" href="/assets/fontawesome-pro-5.11.2-web/css/all.min.css" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-contextmenu/2.7.1/jquery.contextMenu.min.css" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-contextmenu/2.7.1/jquery.contextMenu.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-contextmenu/2.7.1/jquery.ui.position.js"></script>


<link rel="stylesheet" href="/assets/js/toastr-master/build/toastr.css" type="text/css" />
<link rel="stylesheet" type="text/css" href="/assets/js/dropzone/dropzone.min.css" />
<link rel="stylesheet" type="text/css" href="/assets/js/dropzone/basic.min.css" />

<link rel="stylesheet" type="text/css" href="<%= Utils.CheckVersion_NonTemplate("/admin/css/microtip.css") %>" />
<link rel="stylesheet" type="text/css" href="<%= Utils.CheckVersion_NonTemplate("/admin/css/admin.css") %>" />
<link rel="stylesheet" type="text/css" href="<%= Utils.CheckVersion_NonTemplate("/admin/css/admin-responsive.css") %>" />



<script type="text/javascript">
    function BrowseServer(startupPath, functionData) {
        var finder = new CKFinder();
        finder.startupPath = startupPath;
        finder.selectActionFunction = SetFileField;
        finder.selectActionData = functionData;
        finder.popup();
    }
    function SetFileField(fileUrl, data, test) {
        var thumbnail_image = $("#thumbnail_image_1");
        thumbnail_image.attr("src", data["fileUrl"]);
        var dataValue = $(this).attr("data-value");
        console.log(test);
        document.getElementById(data["selectActionData"]).value = fileUrl;
    }
</script>


