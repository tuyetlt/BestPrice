<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopPanel.ascx.cs" Inherits="admin_AdminControl_Controls_TopPanel" %>
<div class="top-panel">
    <a href="javascript:;" id="menu">
        <i class="fas fa-bars"></i>
    </a>
    <a href="<%= Request.RawUrl %>"><%= SEO.meta_title %></a>
    <form method="post" enctype="multipart/form-data" id="frm_user_info" class="admin-info">
            <%  if (dt.Rows.Count > 0)
                { %>
         <img src="<%= avatar %>?width=50&height=50&quality=100&mode=crop" alt="<%= dr["Name"].ToString() %>" /> <b><%= dr["Name"].ToString() %></b> | 
        <a href="javascript:;" id="btnClearCache">Clear Cache</a> | <a href="javascript:;" id="logout">Thoát</a>
            <% } %>
        <input type="hidden" id="done_login" name="done_login" value="0" />
        <script type="text/javascript">
            $("#logout").click(function () {
                $('#frm_user_info #done_login').val("logout");
                $("#frm_user_info").submit();
            });
            $("#btnClearCache").click(function () {
                $('#frm_user_info #done_login').val("cache");
                $("#frm_user_info").submit();
            });
        </script>
    </form>
</div>