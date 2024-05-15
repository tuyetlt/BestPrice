<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WidgetNews.ascx.cs" Inherits="Controls_WidgetNews" %>
<%@ Import Namespace="System.Data" %>
<section class="news news__evenTop moduleNewsSlideshow-newsEvent">
    <div class="container news__event-inner">
        <div class="heading">
            <h2 class="heading__title text-center"><span>TIN TỨC & SỰ KIỆN</span></h2>
        </div>
        <div class="news__event-inner">
            <div class="news__section">
                <div class="news__section-inner">
                    <div class="news__grid row createModuleNewSlideshow slider" module="newsEvent">
                        <%
                            string filterNews = "Flags=1";
                            DataTable dtNews = SqlHelper.SQLToDataTable("tblArticle", "Name,Gallery,Description,FriendlyUrl", filterNews, ConfigWeb.SortArticle, 1, 4);
                            if (Utils.CheckExist_DataTable(dtNews))
                            {
                                foreach (DataRow drNews in dtNews.Rows)
                                {
                                    string linkDetail = TextChanger.GetLinkRewrite_Article(drNews["FriendlyUrl"].ToString());
                        %>
                        <div class="news__item col-12 col-sm-6 col-md-3">
                            <div class="news__item-inner">
                                <div class="news__image">
                                    <a href="<%= linkDetail %>" title="<%= drNews["Name"].ToString() %>" class="news__thumb">
                                        <img src="<%= Utils.GetFirstImageInGallery_Json(drNews["Gallery"].ToString(), 400, 300) %>" alt="<%= drNews["Name"].ToString() %>" class="img-responsive" /></a>
                                </div>
                                <div class="news__info">
                                    <h2 class="news__title"><a href="<%= linkDetail %>" title="<%= drNews["Name"].ToString() %>"><%= drNews["Name"].ToString() %></a></h2>
                                    <div class="news__des mb-3"><%= drNews["Description"].ToString() %></div>
                                    <div class="news__action">
                                        <a href="<%= linkDetail %>" class="btn btn-primary d-block right">Xem chi tiết <i class="icon-arrow-circle-right"></i></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%
                                }
                            }
                        %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>
