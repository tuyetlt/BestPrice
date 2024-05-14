<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeProduct.ascx.cs" Inherits="ajax_Controls_HomeProduct" %>
<%@ Import Namespace="System.Data" %>

<%
    string categoryid = RequestHelper.GetString("categoryid", "");
    string filterWidget = string.Format("ID={0}", categoryid);
    DataTable dtWidget_1 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", filterWidget, "Sort", 1, 6);
    if (Utils.CheckExist_DataTable(dtWidget_1))
    {
        DataRow dr_1 = dtWidget_1.Rows[0];
        DataTable dtWidget_2 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("ParentID={0} AND {1}", dr_1["ID"], filterWidget), "Sort", 1, 5);
%>
<div class="title">
    <h2>
        <span><%= dr_1["Name"] %></span>
        <%
            if (Utils.CheckExist_DataTable(dtWidget_2))
            {
                foreach (DataRow dr_2 in dtWidget_2.Rows)
                {
                    Response.Write(string.Format(@"<a href=""{0}"">{1}</a>", "#", dr_2["Name"]));
                }
            }
        %>
    </h2>
    <a href="#" class="all">Xem tất cả <i class="far fa-chevron-right"></i></a>
</div>
<div class="slide_product_multi">
    <div class="banner">
        <a href="#">
            <span class="img" style="background: url('<%= C.ROOT_URL %><%= dr_1["Image_2"] %>') no-repeat;"></span>
        </a>
    </div>
    <div class="owl-carousel owl-theme owl_multi_product">
        <% 
            DataTable dtProduct = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", string.Format("(Hide is null OR Hide=0) AND CategoryIDParentList like N'%,{0},%'", categoryid), "newid()", 1, 20);
            if (Utils.CheckExist_DataTable(dtProduct))
            {
                int count = 0;
        %>
        <div class="item">
            <div class="flex-container">
                <%
                    for (int i = 0; i < 8 && i < dtProduct.Rows.Count; i++)
                    {
                        DataRow drProduct = dtProduct.Rows[i];
                        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                %>
                <div>
                    <a href="<%= linkDetail %>">
                        <div class="img">
                            <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 200, 200) %>" alt="<%= drProduct["Name"].ToString() %>" />
                        </div>

                        <div class="cont">
                            <h4><%= drProduct["Name"].ToString() %></h4>
                            <p><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></p>
                        </div>
                    </a>
                </div>
                <%
                        count++;
                    }%>
            </div>
            <div class="flex-container">
                <%
                    for (int i = count; i < 16 && i < dtProduct.Rows.Count; i++)
                    {
                        DataRow drProduct = dtProduct.Rows[i];
                        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                %>
                <div>
                    <a href="<%= linkDetail %>">
                        <div class="img">
                            <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 200, 200) %>" alt="<%= drProduct["Name"].ToString() %>" />
                        </div>

                        <div class="cont">
                            <h4><%= drProduct["Name"].ToString() %></h4>
                            <p><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></p>
                        </div>
                    </a>
                </div>
                <%
                        count++;
                    }%>
            </div>
        </div>
        <div class="item">
            <div class="flex-container">
                <%
                    for (int i = count; i < 24 && i < dtProduct.Rows.Count; i++)
                    {
                        DataRow drProduct = dtProduct.Rows[i];
                        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                %>
                <div>
                    <a href="<%= linkDetail %>">
                        <div class="img">
                            <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 200, 200) %>" alt="<%= drProduct["Name"].ToString() %>" />
                        </div>

                        <div class="cont">
                            <h4><%= drProduct["Name"].ToString() %></h4>
                            <p><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></p>
                        </div>
                    </a>
                </div>
                <%
                        count++;
                    }%>
            </div>
            <div class="flex-container">
                <%
                    for (int i = count; i < 32 && i < dtProduct.Rows.Count; i++)
                    {
                        DataRow drProduct = dtProduct.Rows[i];
                        string linkDetail = TextChanger.GetLinkRewrite_Products(drProduct["FriendlyUrlCategory"].ToString(), drProduct["FriendlyUrl"].ToString());
                %>
                <div>
                    <a href="<%= linkDetail %>">
                        <div class="img">
                            <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 200, 200) %>" alt="<%= drProduct["Name"].ToString() %>" />
                        </div>

                        <div class="cont">
                            <h4><%= drProduct["Name"].ToString() %></h4>
                            <p><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></p>
                        </div>
                    </a>
                </div>
                <%
                        count++;
                    }%>
            </div>
        </div>

        <%
            }
        %>
    </div>
</div>
<% 
    DataTable dtHot = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "", string.Format("(Hide is null OR Hide=0) AND CategoryIDParentList like N'%,{0},%'", dr_1["ID"]), "newid()", 1, 50);
    if (Utils.CheckExist_DataTable(dtHot))
    {
%>
<div class="product_hot">
    <h3>SẢN PHẨM NỔI BẬT</h3>
    <div class="list_">
        <div class="owl-carousel owl-theme owl_product_hot">
            <% foreach (DataRow drHot in dtHot.Rows)
                {
                    string linkDetail = TextChanger.GetLinkRewrite_Products(drHot["FriendlyUrlCategory"].ToString(), drHot["FriendlyUrl"].ToString());
            %>
            <div class="item">
                <a href="<%= linkDetail %>">
                    <div class="img">
                        <img src="<%= Utils.GetFirstImageInGallery_Json(drHot["Gallery"].ToString(), 200, 200) %>" alt="<%= drHot["Name"].ToString() %>" />
                    </div>

                    <div class="cont">
                        <h4><%= drHot["Name"].ToString() %></h4>
                        <div class="info">
                            <ins><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drHot["ID"]), "Price") %></ins>
                            <del>9.999.999 VNĐ</del>
                        </div>
                    </div>
                </a>
            </div>
            <% } %>
        </div>
    </div>
</div>
<% }
    }
%>