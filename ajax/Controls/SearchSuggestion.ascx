<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchSuggestion.ascx.cs" Inherits="ajax_Controls_SearchSuggestion" %>
<%@ Import Namespace="System.Data" %>
<%
    if (Utils.CheckExist_DataTable(dtProduct))
    {
        Response.Write("<ul>");
        foreach (DataRow dr in dtProduct.Rows)
        {
            string linkDetail = TextChanger.GetLinkRewrite_Products(dr["FriendlyUrlCategory"].ToString(), dr["FriendlyUrl"].ToString());
%>
<li>
    <a href="<%= linkDetail %>" class="search_click">
        <img src="<%= Utils.GetFirstImageInGallery_Json(dr["Gallery"].ToString(), 200, 200) %>" alt="<%= dr["Name"].ToString() %>" />
        <div class="cont">
            <h3><%= dr["Name"].ToString() %></h3>
            <div class="price">
                <div class="cnt-price">
                    <ins><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(dr["ID"]), "Price") %></ins>
                    <del><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(dr["ID"]), "Price1") %></del>
                </div>
                <div class="per"><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(dr["ID"])) %></div>
            </div>
        </div>
    </a>
</li>

<%
        }
        Response.Write("<ul>");
    }
%>