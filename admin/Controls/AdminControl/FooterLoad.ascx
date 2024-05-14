<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterLoad.ascx.cs" Inherits="admin_Controls_FooterLoad" %>

<div id="div-ajax-loading">
</div>

<script type="text/javascript" src="<%= Utils.CheckVersion_NonTemplate("/admin/js/admin.js") %>"></script>

<% if(Utils. GetControlAdmin().ToLower().Contains("list")){  %>
<script type="text/javascript">
    $(document).ready(function () {
        getval(0);
        setTimeout(function () { getval(0); }, 100);
    });
</script>
<% } %>