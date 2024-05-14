<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentDetail.ascx.cs" Inherits="Controls_ContentDetail" %>
<%@ Import Namespace="System.Data" %>
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-md-flex align-items-end">
            <div class="col mb-3 mb-md-0">
                
            </div>
        </div>
        <div class="row two-column">
            <article id="content" class="col-12 col-md-9 order-md-2">
                <div class="news__detail">                  
                    <h1 class="page__title"><%= ConvertUtility.ToString(dr["Name"]) %></h1>
                    <div class="news__detail-content">   
                        <div class="description">
                           <%= ConvertUtility.ToString(dr["LongDescription"]) %>
                        </div>
                    </div>
                </div>
            </article>
            <aside id="column-left" class="column-left col-12 col-md-3 d-none d-md-flex order-md-1">
            </aside>
        </div>
    </div>
</div>

