<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu.ascx.cs" Inherits="template_menu" %>
<%@ Import Namespace="System.Data" %>
<div id="navbarSidebarMenu" class="navigation">
    <span class="btn-close-navigation d-md-none" onclick="$('.btnNavigationMenu').trigger('click');"><i class="icon-close"></i></span>
    <h3 class="navigation__title d-md-none">Danh mục sản phẩm</h3>
    <div class="navigation__content container">
        <div class="navigation__inner d-md-flex align-items-md-center">
            <div class="navigation__categories dropdown">
                <a href="#" class="navigation__textbar nav-link d-md-flex align-items-center hidden-xs-sm">
                    <i class="icon-fa-menu me-2"></i>
                    <span>Danh mục sản phẩm</span>
                    <i class="icon-angle-down ms-auto"></i>
                </a>
                <ul class="navigation__menu nav is-in-view">
                    <%
                        string filter = string.Format("{0} AND {1}", Utils.CreateFilterHide, Utils.CreateFilterFlags(PositionMenuFlag.Main, "PositionMenuFlag"));
                        DataTable dt_1 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, Name, FriendlyUrl, Link, LinkTypeMenuFlag, PositionMenuFlag, ParentID", string.Format("ParentID=0 AND {0}", filter), "Sort", 1, C.MAX_ITEM_MENU);
                        if (Utils.CheckExist_DataTable(dt_1))
                        {
                            foreach (DataRow dr_1 in dt_1.Rows)
                            {
                                string link_1 = "#";// = Utils.CreateCategoryLink(dr_1["LinkTypeMenuFlag"], dr_1["FriendlyUrl"], dr_1["Link"]);
                    %>
                    <li class="nav-item has-children">
                        <div class="d-flex align-items-center">
                            <a href="<%= link_1 %>" class="nav-link col" title="<%= dr_1["Name"].ToString() %>"><%= dr_1["Name"].ToString() %></a>
                            <button type="button" class="btn btn-next-menu"><i class="icon-angle-right"></i></button>
                        </div>
                        <div class="dropdown-menu">
                            <button type="button" class="btn btn-prev-menu d-md-none">
                                <i class="icon-angle-left"></i>
                                <span><%= dr_1["Name"].ToString() %></span>
                            </button>
                            <div class="dropdown-inner row">
                                <%
                                    int count2 = 0;
                                    int subMenu = (int)PositionMenuFlag.Main;
                                    string filter2 = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", subMenu);
                                    DataTable dt_2 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, Name, FriendlyUrl, Link, LinkTypeMenuFlag, PositionMenuFlag, ParentID", string.Format("ParentID={0} AND {1}", dr_1["ID"], filter2), "Sort");
                                    if (Utils.CheckExist_DataTable(dt_2))
                                    {
                                        Response.Write(@"<div class=""col""><ul class=""nav nav__sub col flex-column"">");
                                        foreach (DataRow dr_sub in dt_2.Rows)
                                        {
                                            count2++;
                                            string link_2 = "#"; //Utils.CreateCategoryLink(dr_sub["LinkTypeMenuFlag"], dr_sub["FriendlyUrl"], dr_sub["Link"]);
                                %>
                                <%
                                    if (count2 % dt_1.Rows.Count == 0 && count2 != 1 && count2 != dt_2.Rows.Count - 1)
                                        Response.Write(@"</ul></div><div class=""col col_split""><ul class=""nav nav__sub col flex-column"">");
                                %>
                                <li class="nav-item"><a href="<%= link_2 %>" class="nav-link">Cấp 2 - <%= dr_sub["Name"].ToString() %></a>

                                    <%
                                        int subMenu1 = (int)PositionMenuFlag.Main;
                                        string filter3 = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", subMenu1);
                                        DataTable dt3 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, Name, FriendlyUrl, Link, LinkTypeMenuFlag, PositionMenuFlag, ParentID", string.Format("ParentID={0} AND {1}", dr_sub["ID"], filter3), "Sort");
                                        if (Utils.CheckExist_DataTable(dt3))
                                        {
                                            int count3 = 0;
                                            Response.Write(@"<div class=""col sub_menu""><ul class=""nav nav__sub col flex-column"">");
                                            foreach (DataRow dr3 in dt3.Rows)
                                            {
                                                count3++;
                                                string link_3 = "#"; // Utils.CreateCategoryLink(dr3["LinkTypeMenuFlag"], dr3["FriendlyUrl"], dr3["Link"]);
                                    %><li class="nav-item"><a href="<%= link_3 %>" class="nav-link">Cấp 3 - <%= dr3["Name"].ToString() %></a></li>
                                    <%
                                                if (count3 % dt_2.Rows.Count == 0 && count3 != 1 && count3 != dt3.Rows.Count)
                                                    Response.Write(@"</ul></div><div class=""col""><ul class=""nav nav__sub col flex-column"">");
                                            }
                                            Response.Write(@"</ul></div>");
                                        } %>
                                </li>
                                <%
                                        }
                                        Response.Write(@"</ul></div>");
                                    }
                                %>
                            </div>
                        </div>
                    </li>
                    <%
                            }
                        }
                    %>
                </ul>
            </div>
            <div class="hidden-xs-sm">
                <ul class="nav align-items-center">

                    <%
                        int menuNgang = (int)PositionMenuFlag.Top;
                        string filterMenuNgang = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", menuNgang);
                        DataTable dt_ngang = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "", string.Format("ParentID=0 AND {0}", filterMenuNgang), "Sort");
                        if (Utils.CheckExist_DataTable(dt_ngang))
                        {
                            foreach (DataRow dr_sub in dt_ngang.Rows)
                            {
                                Response.Write(string.Format(@"<li class=""nav-item""><a href=""{0}"" class=""nav-link"">{1}</a></li>", Utils.CreateCategoryLink(dr_sub["LinkTypeMenuFlag"], dr_sub["FriendlyUrl"], dr_sub["Link"]), dr_sub["Name"].ToString()));
                            }
                        }
                    %>
                </ul>
            </div>
        </div>
    </div>
</div>







