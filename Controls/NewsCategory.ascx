<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsCategory.ascx.cs" Inherits="Controls_NewsCategory" %>
<%@ Import Namespace="System.Data" %>

<input type="hidden" id="idCategory" value="<%= drCat["ID"] %>" />
<input type="hidden" id="pageIndex" value="1" />
<input type="hidden" id="pageSize" value="<%= C.ROWS_PRODUCTCATEGORY %>" />
<input type="hidden" id="totalArticle" value="<%= _totalArticle %>" />

<div class="container">
    <div class="news__page">
        <div class="page__heading text-center">
            <h2 class="page__title"><span><%= drCat["Name"] %></span></h2>
        </div>
        <div class="news__content">
            <div class="article-list news__grid d-grid">
                <%
                    if (Utils.CheckExist_DataTable(dtNews))
                    {
                        foreach (DataRow drNews in dtNews.Rows)
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
                <%
                        }
                    }
                %>
            </div>
            <div class="clear"></div>
            <% if (_totalArticle > C.ROWS_PRODUCTCATEGORY)
                { %>
            <div class="container-btn show-more"><a id="category_paging_article" class="btn-see-more">Xem thêm <i class="fas fa-sort-down"></i></a></div>
            <%} %>
            <div class="clear"></div>

        </div>
    </div>
</div>