<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsDetail.ascx.cs" Inherits="Controls_NewsDetail" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Newtonsoft.Json" %>
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-md-flex align-items-end">
            <div class="col mb-3 mb-md-0">
                <% 
                    List<BreadCrumb> breadCrumbList = new List<BreadCrumb>();
                    try
                    {
                        string bCJson = ConvertUtility.ToString(drCategory["BreadCrumbJson"]);
                        breadCrumbList = JsonConvert.DeserializeObject<List<BreadCrumb>>(bCJson);
                    }
                    catch
                    {

                    }
                    string UCBreadCrumb = Utils.LoadUserControlBreadCrumb("~/Controls/WidgetBreadCrumb.ascx", breadCrumbList);
                %>
                <%= UCBreadCrumb %>
            </div>
        </div>
        <div class="row two-column">
            <article id="content" class="col-12 col-md-12 order-md-1">
                <div class="news__detail">
                    <div class="date">
                        <i class="fad fa-clock"></i><%=String.Format("{0:dd/MM/yyyy HH:mm}",dr["CreatedDate"]) %>
                    </div>
                    <h1 class="page__title"><%= ConvertUtility.ToString(dr["Name"]) %></h1>
                    <div class="news__detail-content">
                        <div class="news__meta-description"><%= ConvertUtility.ToString(dr["Description"]) %></div>
                        <div class="description">
                            <%= ConvertUtility.ToString(longDescription) %>
                        </div>
                         <div class="source d-flex justify-content-end">
                            <strong>Nguồn tin</strong><span class="px-2">|</span><a href="javascript:void(0);" class="btn-review"><span class="icon-envelope"></span></a>
                        </div>
                    </div>
                    <%
                        if (!Utils.IsNullOrEmpty(dr["NewsRelatedIDList"].ToString().Trim()))
                        {
                            string filter = string.Format("ID in ({0})", dr["NewsRelatedIDList"].ToString());
                            DataTable dtNews = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "ID,FriendlyUrl, Name, Gallery, Description", filter, "EditedDate DESC");
                            if (Utils.CheckExist_DataTable(dtNews))
                            {
                                int count = 0;
                    %>
                    <div class="block-information-another">
                        <h3><span>Các bài viết khác</span></h3>
                        <ul class="another-lists">
                            <% for (int i = 0; i < dtNews.Rows.Count; i++)
                                {
                            %>
                            <li>
                                <a href="<%= TextChanger.GetLinkRewrite_Article(dtNews.Rows[i]["FriendlyUrl"].ToString()) %>"><%= dtNews.Rows[i]["Name"].ToString() %></a>
                            </li>
                            <%  count++;
                                }
                            %>
                        </ul>
                    </div>
                    <%
                            }
                        } %>
                </div>
            </article>

        </div>
    </div>
</div>

