<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentDetail.ascx.cs" Inherits="Controls_ContentDetail" %>
<%@ Import Namespace="System.Data" %>
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
        </div>
        <div class="row">
            <article id="content" class="col-12 col-md-12 order-md-1">
                <div class="news__detail">
                    <h1 class="page__title"><%= ConvertUtility.ToString(dr["Name"]) %></h1>
                    <div class="news__detail-content">
                        <div class="description">
                            <%= ConvertUtility.ToString(dr["LongDescription"]) %>
                        </div>
                    </div>
                </div>
            </article>
        </div>
    </div>
</div>

