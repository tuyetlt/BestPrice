<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderLoad.ascx.cs" Inherits="Controls_HeaderLoad" %>
<link rel="stylesheet" type="text/css" href="<%=Utils.CheckVersion_NonTemplate("/themes/assets/css/bootstrap.min.css") %>" media="screen" />
<link rel="stylesheet" type="text/css" href="<%=Utils.CheckVersion_NonTemplate("/themes/assets/css/custom.css") %>" />
<% if(!string.IsNullOrEmpty(ConfigWeb.Style)){ %>
<link rel="stylesheet" type="text/css" href="<%=Utils.CheckVersion_NonTemplate("/themes/assets/css/" + ConfigWeb.Style) %>" />
<% } %>
<link href="https://cdnjs.cloudflare.com/ajax/libs/paginationjs/2.1.4/pagination.css" rel="stylesheet" />
<script src="/themes/assets/js/jquery/jquery-3.6.3.min.js" type="text/javascript"></script>
<script src="/themes/assets/js/jquery/jquery-ui.min.js" type="text/javascript"></script>
<script src="/themes/assets/js/bootstrap/popper.min.js"></script>
<script src="/themes/assets/js/bootstrap/bootstrap.min.js" type="text/javascript"></script>
<%--<script src="/themes/assets/js/paging.js" type="text/javascript"></script>--%>