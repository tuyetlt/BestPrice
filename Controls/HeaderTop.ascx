<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderTop.ascx.cs" Inherits="Controls_HeaderTop" %>
<%@ Import Namespace="System.Data" %>
<header class="header">
    <div class="header__top container">
        <button class="btn btnNavigationMenu d-md-none" role="button"><span class="icon-fa-menu"></span></button>
        <div class="header__top-inner d-md-flex  align-items-sm-center">
            <div class="header__logo">
                <a href="<%= C.ROOT_URL %>" class="header__logo-link">
                    <img src="<%= C.ROOT_URL %><%= ConfigWeb.Logo %>" alt="<%= ConfigWeb.MetaTitle %>" class="img-responsive"></a>
            </div>
            <div class="header__right col pr-0 search_ac">
                <div class="header__right-inner d-md-flex align-items-center justify-content-end">
                    <div class="header__search col">
                        <div class="header__search-inner">
                            <div class="header__search-field dropdown search">
                                <form method="GET" action="<%=C.ROOT_URL %>/tim-kiem/" data-search="internal">
                                    <input type="text" name="key" class="search_input" id="searchbox" autocomplete="off" placeholder="Tìm kiếm" />

                                    <div class="loading-search"></div>
                                    <button type="button" class="btn btn-primary btn-top-search"><i class="icon-search"></i></button>
                                </form>
                                <div class="over-lay-search">
                                </div>
                               <div class="dropdown-menu">
                                     <div class="dropdown-inner">
                                        <%-- <div class="content-new">
                                               <%
                                                   string filterNews = "Flags=1";
                                                   DataTable dtNews = SqlHelper.SQLToDataTable("tblArticle", "Name,Gallery,Description,FriendlyUrl", filterNews, ConfigWeb.SortArticle, 1, 4);
                                                   if (Utils.CheckExist_DataTable(dtNews))
                                                   {
                                                       int count = 0;

                                                       /*foreach (DataRow drNews in dtNews.Rows)
                                                       {*/
                                                       for (int i = 0; i < 1 && i < dtNews.Rows.Count; i++)
                                                       {
                                                           string linkDetail = TextChanger.GetLinkRewrite_Article(dtNews.Rows[i]["FriendlyUrl"].ToString());
                                                %>
                                                    <div class="d-flex align-items-center px-3 border-css">
                                                        <div class="col d-flex align-items-center">
                                                            <a href="<%= linkDetail %>" title="<%= dtNews.Rows[i]["Name"].ToString() %>"><%= dtNews.Rows[i]["Name"].ToString() %></a>
                                                        </div>
                                                        
                                                    </div>
                                                
                                                <%
                                                        }
                                                    }
                                                %>
                                            
                                         </div>--%>
                                         
                                         <div class="category__latest ">
                                             <p class="fs-14 title-box">Danh mục nổi bật</p>
                                             <div class="list-category-search">
                                             <div class="search-cat-list d-grid">
                                                 
                                                  <%
                                                     string filter = string.Format("{0} AND {1}", Utils.CreateFilterHide, Utils.CreateFilterFlags(PositionMenuFlag.Main, "PositionMenuFlag"));
                                                     DataTable dt_1 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, Name, FriendlyUrl, Link, LinkTypeMenuFlag, PositionMenuFlag, ParentID,Icon", string.Format("ParentID=0 AND {0}", filter), "Sort", 1, C.MAX_ITEM_MENU);
                                                     if (Utils.CheckExist_DataTable(dt_1))
                                                     {
                                                         foreach (DataRow dr_1 in dt_1.Rows)
                                                         {
                                                             string link_1 = Utils.CreateCategoryLink(dr_1["LinkTypeMenuFlag"], dr_1["FriendlyUrl"], dr_1["Link"]);
                                                             int subMenu = (int)PositionMenuFlag.Main;
                                                             string filter2 = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", subMenu);
                                                             DataTable dt_2 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, Name, FriendlyUrl, Link, LinkTypeMenuFlag, PositionMenuFlag, ParentID, Icon", string.Format("ParentID={0} AND {1}", dr_1["ID"], filter2), "Sort");

                                                 %>
                                                                                                  
                                                     <%
                                                    int count2 = 0;

                                                    if (Utils.CheckExist_DataTable(dt_2))
                                                    {
                                                        foreach (DataRow dr_sub in dt_2.Rows)
                                                        {
                                                                 string icon = "";
                                                            count2++;
                                                                 if (!string.IsNullOrEmpty(dr_sub["Icon"].ToString()))
                                                                  icon = dr_sub["Icon"].ToString();
                                                            string link_2 = Utils.CreateCategoryLink(dr_sub["LinkTypeMenuFlag"], dr_sub["FriendlyUrl"], dr_sub["Link"]);
                                                        %>
                                                        <div class="search-cat-item"><a href="<%= link_2 %>" class="nav-link">
                                                             <% if (!string.IsNullOrEmpty(icon))
                                                                    { %>
                                                                <div class="img">
                                                                      <img src="<%= icon %>" alt="<%= dr_sub["Name"].ToString() %>" />
                                                                 </div>
                                                                <% } %>
                                                            <div class="search-cat__name"> <%= dr_sub["Name"].ToString() %></div>
                                                        </a></div>
                                                        <% 
                                                    }
                                                  } %>
                                               <%}
                                                 } %>
                                         </div>
                                             
                                             </div>
                                     </div>
                                 </div>
                                   </div>

                                <div class="show_search_content">
                                    <label>Đang tải sản phẩm gợi ý...</label>
                                    <%--  <div class="box-loading">
                                        <div class="box-thumbnail"></div>

                                        <div class="box-line-sm"></div>
                                        <div class="box-line-xs"></div>

                                        <div class="box-line-df"></div>
                                        <div class="box-line-lgx"></div>
                                        <div class="box-line-lg"></div>
                                    </div>--%>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="header__col header__nav">
                        <ul class="nav">
                            <%
                                int menuNgang = (int)PositionMenuFlag.SupperTop;
                                string filterMenuNgang = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", menuNgang);
                                DataTable dt_ngang = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "LinkTypeMenuFlag,FriendlyUrl,Link,Name", string.Format("ParentID=0 AND {0}", filterMenuNgang), "Sort");
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
                    <div class="header__col header__contact">
                        <div class="d-flex align-items-center">
                            <div class="contact-no">
                                <a href="tel:<%= ConfigWeb.Hotline %>"><%= ConfigWeb.Hotline %></a>
                            </div>
                        </div>
                    </div>
                    <div class="header__cart block-top-cart" id="cart">
                        <a href="/gio-hang/" data-loading-text="text_loading" class="btn btn-cart">
                            <i class="icon-shipping-cart"></i>
                            <span id="cart-total"><%= ShoppingCart.CartCount %></span>
                        </a>
                        <ul id="loadCart" class="dropdown-menu list-item-cart">
                            <li class="item">
                                <a href="javascript:void(0)" onclick="cart.close('.block-top-cart');" class="btn-close-cart"><span class="icon-close"></span></a>
                                <div class="cart-inform">
                                    <div class="add-success"><span class="icon-check"></span></div>
                                    <p class="text-center">text_success</p>
                                </div>
                                <div class="buttons-set">
                                    <a href="/gio-hang/" class="btn btn-primary">Xem giỏ hàng</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</header>
