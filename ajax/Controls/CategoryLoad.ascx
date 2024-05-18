<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryLoad.ascx.cs" Inherits="ajax_Controls_CategoryLoad" %>
<%@ Import Namespace="System.Data" %>

<% 
    if (action == "product_list")
    {
        if (Utils.CheckExist_DataTable(dtProduct))
        {
            foreach (DataRow drProduct in dtProduct.Rows)
            {
                string linkDetail = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(drProduct["FriendlyUrlCategory"]), ConvertUtility.ToString(drProduct["FriendlyUrl"]));
%>
<div class="product__item col-6 col-sm-4 col-md-4 col-lg-3">
    <div class="product__inner">
        <div class="product__thumb">

            <% if (!string.IsNullOrEmpty(SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"]))))
                { %>
            <label class="on-sale"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"])) %></span></label>
            <% } %>



            <a href="<%= linkDetail %>" title="Máy nước nóng Panasonic DH-4RP1" class="product__image">
                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 300, 300) %>" alt="<%= drProduct["Name"].ToString() %>" width="350" height="400" /></a>
        </div>
        <div class="product__info">
            <h3 class="product__name"><a href="<%= linkDetail %>"><%= drProduct["Name"].ToString() %></a></h3>

            <div class="product__price d-flex align-items-center">
                <div class="price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></div>
                <div class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></div>
            </div>
        </div>
    </div>
</div>
<%
            }
        }
    }

    %>

<%
    if (action == "article_list")
    {
        if (Utils.CheckExist_DataTable(dtArticle))
        {
            foreach (DataRow drNews in dtArticle.Rows)
            {
                string linkDetail = TextChanger.GetLinkRewrite_Article(drNews["FriendlyUrl"].ToString());
%>

<div class="item">
    <a href="<%= linkDetail %>" title="<%= drNews["Name"].ToString() %>" class="news__inner">
        <div class="news__image">
            <picture class="thumb">
                <a href="<%= linkDetail %>">
                    <img src="<%= Utils.GetFirstImageInGallery_Json(drNews["Gallery"].ToString(), 300, 150) %>" alt="<%= drNews["Name"].ToString() %>" width="600" height="350" /></a>
            </picture>
        </div>
        <div class="news__info">
            <h3 class="news__title"><a href="<%= linkDetail %>"><%= drNews["Name"].ToString() %></a></h3>
            <div class="news__des"><%= drNews["Description"].ToString() %></div>
        </div>
    </a>

</div>

<%}
        }
    } %>

