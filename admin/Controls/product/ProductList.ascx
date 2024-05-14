<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductList.ascx.cs" Inherits="admin_Controls_ProductList" %>
<%@ Import Namespace="System.Data" %>

<div class="obj-list">
    <div class="filter">
        <a class="button" id="refresh" href="javascript:;"><i class="fas fa-sync-alt"></i>Refresh</a>
        <a class="button" id="delete" href="javascript:;"><i class="fas fa-trash-alt"></i>Xóa</a>
        <a class="button" href="<%= Utils.GetEditControl() %>"><i class="fas fa-plus"></i>Thêm <%= ControlAdminInfo.ShortName %></a>
        <a class="button" id="calendar" href="javascript:;"><i class="far fa-calendar-alt"></i>Thời gian</a>

        <input type="hidden" id="table" value="<%= ControlAdminInfo.SQLNameTable %>" />
        <input type="hidden" id="fromDate" />
        <input type="hidden" id="toDate" />
        <input type="hidden" id="pageIndex" />
        <input type="hidden" id="control" value="<%= Utils.GetControlAdmin() %>" />
        <input type="hidden" id="folder" value="<%= Utils.GetFolderControlAdmin() %>" />
        <input type="hidden" id="controlName" value="<%= ControlAdminInfo.ShortName %>" />

        <input type="hidden" id="loadpaging" value="true" />
        <input type="hidden" id="pageSize" value="<%= C.PAGING_ADMIN %>" />
        <input type="hidden" id="sort" />
        <input type="hidden" id="fieldSort" />
        <input type="hidden" id="filterJson" />
        <input type="text" id="keyword" autocomplete="off" placeholder="Từ khoá tìm kiếm" />



        <%--        <input type="text" id="categoryid" name="ParentID" value="<%= ParentID %>" style="display: none" />
        <input type="text" id="categoryname" name="categorynamelist" value="<%= CatNameList %>" style="display: none" />--%>
        <select id="drCat" name="itemSelect" style="width: 300px">
            <option value="0">- Chọn danh mục -</option>
        </select>

        <script type="text/javascript">

            var defaultCatID = $("#categoryid").val();
            var selected = "";
            $.getJSON("/admin/ajax/ajax.aspx", { ctrl: "select2", multilevel: "1", table: "tblCategories", moduls: "category", t: Math.random() },
                function (json) {
                    //cấp 1
                    $.each(json,
                        function (key, value) {
                            if (value.id == defaultCatID)
                                selected = "selected='selected'";
                            $("#drCat").append("<option " + selected + " value='" + value.id + "'><b>" + value.text + "</b></option>");
                            selected = "";
                            //cấp 2
                            $.each(value.children,
                                function (key, value) {
                                    if (value.id == defaultCatID)
                                        selected = "selected='selected'";
                                    $("#drCat").append("<option " + selected + " value='" + value.id + "'> ⟶ " + value.text + "</option>");
                                    selected = "";
                                    //cấp 3
                                    $.each(value.children,
                                        function (key, value) {
                                            if (value.id == defaultCatID)
                                                selected = "selected='selected'";
                                            $("#drCat").append("<option " + selected + " value='" + value.id + "'> ⟶⟶ " + value.text + "</option>");
                                            selected = "";
                                            //cấp 4
                                            $.each(value.children,
                                                function (key, value) {
                                                    if (value.id == defaultCatID)
                                                        selected = "selected='selected'";
                                                    $("#drCat").append("<option " + selected + " value='" + value.id + "'> ⟶⟶⟶ " + value.text + "</option>");
                                                    selected = "";
                                                });
                                        });
                                });
                        });
                });


            $(document).ready(function () {
                var defaultCatID = $("#categoryid").val();
                console.log(defaultCatID);

                //$('#drCat > option[value="4"]').prop("selected", true);
            });


            $("#drCat").change(function () {
                var filterJson = '[{"Field":"CategoryIDList","Value":"' + $(this).val() + '"},{"Field":"CategoryIDParentList","Value":"' + $(this).val() + '"}]';
                $("#filterJson").val(filterJson);
                getval(0);
            });

        </script>


        <input type="hidden" id="AttrProductFlag" value="0" />
        <select id="flags">
            <option value="0">Tùy chọn lọc</option>
            <option value="<%= (int)AttrProductFlag.Home %>">Home</option>
            <option value="<%= (int)AttrProductFlag.Priority %>">Ưu tiên</option>
            <option value="<%= (int)AttrProductFlag.New %>">Mới</option>
        </select>

    </div>
    <div class="content-list">
        <div class="tableData">
        </div>
    </div>
    <div class="clear"></div>
    <div id="paging"></div>
</div>

<script type="text/javascript">
            $("select#flags").change(function () {
                $("#AttrProductFlag").val($(this).val());
                getval(0);
            });

</script>
