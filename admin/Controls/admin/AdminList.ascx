<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminList.ascx.cs" Inherits="admin_Controls_AdminList" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>

<div class="obj-list">
    <div class="filter">
        <a class="button" id="refresh" href="javascript:;"><i class="fas fa-sync-alt"></i>Refresh</a>
               <a class="button" id="delete" href="javascript:;"><i class="fas fa-trash-alt"></i>Xóa</a>

        <a class="button" href="<%= Utils.GetEditControl() %>"><i class="fas fa-plus"></i>Thêm <%= ControlAdminInfo.ShortName %></a>

        <input type="hidden" id="table" value="<%= ControlAdminInfo.SQLNameTable %>" />
        <input type="hidden" id="field" value='<%= ControlAdminInfo.FieldSql %>' />

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
        <input type="text" id="keyword" autocomplete="off" placeholder="Từ khoá tìm kiếm" />

    </div>
<div class="content-list">
    <div class="tableData">
        </div>
    </div>
    <div class="clear"></div>
<div id="paging"></div></div>
