<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="Controls_Footer" %>
<%@ Import Namespace="System.Data" %>
<footer class="footer">
    <div class="footer__top">
        <div class="container">
            <div class="row align-items-center">
                 <%
                     string filter = string.Format("ParentID=0 AND Flags & {0} <> 0", (int)BaseTableFlag.Service);
                     DataTable dtParent = SqlHelper.SQLToDataTable("tblBase", "", filter, "Sort");
                     if (Utils.CheckExist_DataTable(dtParent))
                     {
                         foreach (DataRow drParent in dtParent.Rows)
                         {
                             string image = Utils.GetFirstImageInGallery_Json(drParent["Gallery"].ToString());
        %>      
                <div class="col-12 col-sm-3 mb-3 mb-sm-0">
                    <div class="d-flex align-items-center">
                        <span class="img-thumb">
                            <img src="<%= image %>" alt="<%= drParent["Name"].ToString() %>" /></span>
                        <div class="col">
                            <p class="title"><%= drParent["Name"].ToString() %></p>
                            <p><%= drParent["Phone"].ToString() %></p>
                        </div>
                    </div>
                </div>
                <%
                        }
                    } %>
            </div>
        </div>
    </div>
    <div class="footer__content">
        <div class="container">
            <div class="footer__inner">
                <div class="footer__list row">
                    <div class="footer__item col-12 col-sm-4 mb-3 mb-sm-0">
                        <div class="footer__item-inner">
                            <%= ConfigWeb.Footer1 %>
                            <div class="footer__support">
                                <a href="tel:<%= ConfigWeb.Hotline %>"><strong>HOTLINE</strong><br>
                                    <strong><%= ConfigWeb.Hotline %></strong> </a>
                            </div>
                            <%= ConfigWeb.Footer2 %>
                        </div>
                    </div>
                    <div class="footer__item col-12 col-sm-4 mb-3 mb-sm-0">
                        <div class="footer__item-inner">
                            <h3 class="footer__title">Thông tin</h3>
                            <ul class="list-unstyled information mb-3">

                                 <%
                        int menuNgang = (int)PositionMenuFlag.Bottom;
                        string filterMenuNgang = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", menuNgang);
                        DataTable dt_ngang = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "LinkTypeMenuFlag,FriendlyUrl,Link,Name", string.Format("ParentID=0 AND {0}", filterMenuNgang), "Sort");
                        if (Utils.CheckExist_DataTable(dt_ngang))
                        {
                            foreach (DataRow dr_sub in dt_ngang.Rows)
                            {
                                Response.Write(string.Format(@"<li><a href=""{0}""><i class=""icon-angle-right""></i>{1}</a></li>", Utils.CreateCategoryLink(dr_sub["LinkTypeMenuFlag"], dr_sub["FriendlyUrl"], dr_sub["Link"]), dr_sub["Name"].ToString()));
                            }
                        }
                    %>
                            </ul>
                            <div class="social">
                                <ul class="list-unstyled social__list d-flex align-items-center">
                                    <li class="social__item">
                                        <a href="<%= ConfigWeb.FacebookID %>" class="social__link facebook" title="Facebook" rel="nofollow" target="_blank"><i class="icon-facebook"></i></a>
                                    </li>
                                    <li class="social__item">
                                        <a href="<%= ConfigWeb.OAZalo %>" class="social__link zalo" title="Zalo" rel="nofollow" target="_blank"><i class="icon-zalo"></i></a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="footer__item col-12 col-sm-4">
                        <div class="footer__item-inner">
                            <div class="footer__logo">
                                <a href="<%= C.ROOT_URL %>">
                                    <img src="<%= C.ROOT_URL %><%= ConfigWeb.Logo %>" alt="<%= ConfigWeb.MetaTitle %>"></a>
                            </div>
                            <%= ConfigWeb.Footer_Address %>
                            <div class="d-flex align-items-center">
                               
                                <a href="<%= ConfigWeb.BoCongThuongLink %>">
                                    <img width="130" height="39" src="/themes/assets/images/logo_bct-300x91.png" alt="Website đã thông báo BCT"></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</footer>
