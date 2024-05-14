<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagList.ascx.cs" Inherits="admin_Controls_category_TagList" %>
<div class="obj-list">
    <div class="filter">
        <a class="button" id="refresh" href="javascript:;"><i class="fas fa-sync-alt"></i>Refresh</a>
        <a class="button" id="delete" href="javascript:;"><i class="fas fa-trash-alt"></i>Xóa</a>
        <a class="button" href="<%= Utils.GetEditControl() %>"><i class="fas fa-plus"></i>Thêm <%= ControlAdminInfo.ShortName %></a>


        <input type="hidden" id="moduls" name="moduls" value="tag" />
        <input type="radio" name="tag" id="tag" checked />
        <label for="tag">Tag</label><br>
        <input type="radio" name="tag" id="HashTag" />
        <label for="HashTag">Tag Ẩn</label>

        <script type="text/javascript">
            $("input[name='tag']").change(function () {
                if ($(this).is(":checked")) {
                    var val = $(this).attr("id");
                    $("#moduls").val(val);
                    getval(0);
                }
            });
           

        </script>

        <input type="hidden" id="table" value="<%= ControlAdminInfo.SQLNameTable %>" />
        <input type="hidden" id="field" value='<%= ControlAdminInfo.FieldSql %>' />
        <input type="hidden" id="fromDate" />
        <input type="hidden" id="toDate" />
        <input type="hidden" id="pageIndex" />
        <input type="hidden" id="loadpaging" value="true" />
        <input type="hidden" id="pageSize" value="<%= C.PAGING_ADMIN %>" />
        <input type="hidden" id="control" value="<%= Utils.GetControlAdmin() %>" />
        <input type="hidden" id="folder" value="<%= Utils.GetFolderControlAdmin() %>" />
        <input type="hidden" id="controlName" value="<%= ControlAdminInfo.ShortName %>" />
        <input type="hidden" id="sort" />

        <input type="hidden" id="fieldSort" />
    </div>
    <div class="content-list">
        <div class="tableData">
        </div>
    </div>
    <div class="clear"></div>
    <div id="paging"></div>
</div>
