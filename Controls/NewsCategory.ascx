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
            <div class="news__grid d-grid">
                <%
                    if (Utils.CheckExist_DataTable(dtNews))
                    {
                        foreach (DataRow drNews in dtNews.Rows)
                        {
                            string linkDetail = TextChanger.GetLinkRewrite_Article(drNews["FriendlyUrl"].ToString());
                %>

                <div class="news__item">
                    <a href="<%= linkDetail %>" title="<%= drNews["Name"].ToString() %>" class="news__inner">
                        <div class="news__image">
                            <picture class="thumb">
                                <a href="<%= linkDetail %>">
                                    <img src="<%= Utils.GetFirstImageInGallery_Json(drNews["Gallery"].ToString(), 200, 150) %>" alt="<%= drNews["Name"].ToString() %>" width="600" height="400" /></a>
                            </picture>
                        </div>
                        <div class="news__info">
                            <h3 class="news__title"><%= drNews["Name"].ToString() %></h3>
                            <div class="news__des"><%= drNews["Description"].ToString() %></div>
                        </div>
                        <div class="news__posted d-flex flex-column"><strong>11</strong><span>12-2022</span></div>
                    </a>
                </div>
                <%
                        }
                    }
                %>
            </div>

             <% if (_totalArticle > C.ROWS_PRODUCTCATEGORY)
                { %>
            <div class="container-btn show-more"><a id="category_paging_article" class="btn-see-more">Xem thêm <i class="fas fa-sort-down"></i></a></div>
             <div class="block-pagination">
                <ul class="pagination justify-content-center">
                    <li class="page-item"><a class="page-link" href="#"><i class="icon-arrow-prev"></i></a></li>
                    <li class="page-item"><a class="page-link" href="#">1</a></li>
                    <li class="page-item"><a class="page-link" href="#">2</a></li>
                    <li class="page-item"><a class="page-link" href="#">3</a></li>
                    <li class="page-item"><a class="page-link" href="#"><i class="icon-arrow-next"></i></a></li>
                </ul>
            </div>
            <%} %>
        </div>
    </div>
</div>