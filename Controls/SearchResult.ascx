<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchResult.ascx.cs" Inherits="Controls_SearchResult" %>
<%@ Import Namespace="System.Data" %>

<input type="hidden" id="keyword" value="<%= keyword %>" />
<input type="hidden" id="pageIndex" value="1" />
<input type="hidden" id="pageSize" value="<%= C.ROWS_PRODUCTCATEGORY %>" />
<input type="hidden" id="totalProduct" value="<%= _totalProduct %>" />


<main class="products">
    <div class="container">
        <div class="content">
            <div class="container-product">
                <div class="heading">
                    <h2 class="title">
                        <span><%= keyword %></span>
                    </h2>

                </div>
                <div class="product-list">
                    <% 
                        if (Utils.CheckExist_DataTable(dtProduct))
                        {
                            foreach (DataRow drProduct in dtProduct.Rows)
                            {
                                string linkDetail = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(drProduct["FriendlyUrlCategory"]), ConvertUtility.ToString(drProduct["FriendlyUrl"]));

                    %>
                    <div class="product-item">
                        <a href="<%= linkDetail %>">
                            <div class="img">
                                <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 200, 200) %>" alt="<%= drProduct["Name"].ToString() %>" />
                            </div>
                            <%--<span class="sale">50%</span>--%>
                            <div class="cont">
                                <h4 class="name"><%= drProduct["Name"].ToString() %></h4>
                                <div class="info">

                                    <ins><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></ins>
                                    <del><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></del>

                                </div>
                            </div>
                        </a>
                    </div>
                    <%}
                        } %>
                </div>
                <div class="clear"></div>
                <% if (_totalProduct > C.ROWS_PRODUCTCATEGORY)
                    { %>
                <div class="container-btn show-more"><a id="category_paging" class="btn-see-more">Xem thêm <i class="fas fa-sort-down"></i></a></div>
                <%} %>
            </div>
        </div>
    </div>
</main>