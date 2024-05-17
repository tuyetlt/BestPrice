<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" MasterPageFile="~/Main.master" %>
<%@ Register Src="~/Controls/ContentDetail.ascx" TagPrefix="uc1" TagName="ContentDetail" %>
<%@ Register Src="~/Controls/ProductDetails.ascx" TagPrefix="uc1" TagName="ProductDetails" %>
<%@ Register Src="~/Controls/NewsDetail.ascx" TagPrefix="uc1" TagName="NewsDetail" %>
<%@ Register Src="~/Controls/NewsCategory.ascx" TagPrefix="uc1" TagName="NewsCategory" %>
<%@ Register Src="~/Controls/ProductCategory.ascx" TagPrefix="uc1" TagName="ProductCategory" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolder" runat="server"></asp:PlaceHolder>
     <asp:PlaceHolder ID="errorPlaceholder" runat="server"></asp:PlaceHolder>
</asp:Content>