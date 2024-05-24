<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomeCategory.ascx.cs" Inherits="Controls_HomeCategory" %>
<%@ Import Namespace="System.Data" %>
<%
    int flag = (int)AttrMenuFlag.MenuHome;
    int flag1 = (int)PositionMenuFlag.MenuSubMainHome;
    int count = 0;
    string filterWidget = string.Format("(AttrMenuFlag & {0} <> 0) AND (PositionMenuFlag & {1} <> 0)", flag, flag1);
    DataTable dtWidget_1 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,Name,FriendlyUrl,ParentID,Link,PositionMenuFlag,LinkTypeMenuFlag,Image_2,Icon", string.Format("{0} AND {1}", filterWidget, Utils.CreateFilterHide), "Sort", 1, C.MAX_ITEM_CATEGORY_HOME);
    if (Utils.CheckExist_DataTable(dtWidget_1))
    {
        foreach (DataRow dr_1 in dtWidget_1.Rows)
        {
            count++;
            string linkDetail = Utils.CreateCategoryLink(dr_1["LinkTypeMenuFlag"], dr_1["FriendlyUrl"], dr_1["Link"]);
            DataTable dtWidget_2 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,Name,FriendlyUrl,ParentID,Link,PositionMenuFlag,LinkTypeMenuFlag,Image_2,Icon", string.Format("ParentID={0} AND AttrMenuFlag & {1} <> 0 AND {2}", dr_1["ID"], flag1, Utils.CreateFilterHide), "Sort", 1, 8);
            string img_src = dr_1["Image_2"].ToString();
%>
<section class="banner banner__productAdvert banner__productAdvert-slideshow1 mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title justify-content-center"><span><a href="<%= linkDetail %>"><%= dr_1["Name"] %></a></span></h2>
        </div>
    </div>
    <div class="banner__productAdvert-section d-md-flex">
        <% if ((count % 2 == 0 || Utils.isMobileBrowser) && !string.IsNullOrEmpty(img_src))
            {  %>
        <div class="col-12 col-md-6 banner-cus-snow">
            <div class="slider createBannerProductAdvertSliders">
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="<%= img_src %>" alt="<%= dr_1["Name"] %>" />
                    </div>
                </div>
            </div>
        </div>
        <%} %>
        <div class="col-12 col-md-6">
            <div class="banner__productAdvert-inner d-flex align-items-center">
                <div class="banner__productAdvert-slide__inner col-12">
                    <div class="slider product__grid createProductAdvertSlideshow mb-3" module="1">
                        <%= Utils.LoadUserControl("~/Controls/UCHomeProduct.ascx", "", linkDetail, "", ConvertUtility.ToInt32(dr_1["ID"]), true, "Home", -1) %>
                    </div>
                    <div class="tags d-flex flex-wrap justify-content-center">
                        <%
                            if (Utils.CheckExist_DataTable(dtWidget_2))
                            {
                                int c = 0;
                                foreach (DataRow dr_2 in dtWidget_2.Rows)
                                {
                                    string split = "";
                                    if (c > 0)
                                        split = "";
                                    Response.Write(string.Format(@"{0}<a class=""btn tag"" href=""{1}""><span>{2}</span></a>", split, Utils.CreateCategoryLink(dr_2["LinkTypeMenuFlag"], dr_2["FriendlyUrl"], dr_2["Link"]), dr_2["Name"], dr_2["Link"]));
                                    c++;
                                }
                            }
                        %>
                        <a href="<%= Utils.CreateCategoryLink(dr_1["LinkTypeMenuFlag"], dr_1["FriendlyUrl"], dr_1["Link"]) %>" target="_self" class="btn tag more">Tất cả <%= dr_1["Name"] %></a>
                    </div>
                </div>
            </div>
        </div>
        <% if (count % 2 != 0 && !Utils.isMobileBrowser && !string.IsNullOrEmpty(img_src))
            {  %>
        <div class="col-12 col-md-6 banner-cus-snow">
            <div class="slider createBannerProductAdvertSliders">
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="<%= img_src %>" alt="<%= dr_1["Name"] %>" />
                    </div>
                </div>

            </div>
        </div>
        <%} %>
    </div>
</section>
<%}
    } %>




<%--<section class="banner banner__productAdvert banner__productAdvert-slideshow1 mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title justify-content-center"><span>Tivi</span></h2>
        </div>
    </div>
    <div class="banner__productAdvert-section d-md-flex">
        <div class="col-12 col-md-6">
            <div class="slider createBannerProductAdvertSliders">
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="/themes/images/GR-X257MC-1024x512.jpg" alt="" />
                    </div>
                </div>
               
            </div>
        </div>
        <div class="col-12 col-md-6">
            <div class="banner__productAdvert-inner d-flex align-items-center">
                <div class="banner__productAdvert-slide__inner col-12">
                    <div class="slider product__grid createProductAdvertSlideshow mb-3" module="1">
                        <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>
                    </div>
                    <div class="tags d-flex flex-wrap justify-content-center">
                        <a href="#" target="_self" class="btn tag"><span>Tất cả tivi</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Samsung</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Sony</span></a>
                        <a href="#" target="_self" class="btn tag"><span>LG</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Casper</span></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

<section class="banner banner__productAdvert banner__productAdvert-slideshow2 mb-5">
    <div class="container">
        <div class="heading">
            <h2 class="heading__title justify-content-center"><span>Tivi</span></h2>
        </div>
    </div>
    <div class="banner__productAdvert-section d-md-flex">
        <div class="col-12 col-md-6">
            <div class="banner__productAdvert-inner d-flex align-items-center">
                <div class="banner__productAdvert-slide__inner col-12">
                    <div class="slider product__grid createProductAdvertSlideshow mb-3" module="2">
                       <%=Utils.LoadUserControl("~/template/product-item-slide.ascx") %>  
                    </div>
                    <div class="tags d-flex flex-wrap justify-content-center">
                        <a href="#" target="_self" class="btn tag"><span>Tất cả tivi</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Samsung</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Sony</span></a>
                        <a href="#" target="_self" class="btn tag"><span>LG</span></a>
                        <a href="#" target="_self" class="btn tag"><span>Casper</span></a>
                    </div>
                </div>
            </div>
        </div>    
        <div class="col-12 col-md-6">
            <div class="slider createBannerProductAdvertSliders">
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="/themes/images/GR-X257MC-1024x512.jpg" alt="" />
                    </div>
                </div>
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="/themes/images/banner-HTP.jpg" alt="" />
                    </div>
                </div>    
            </div>
        </div>
        
    </div>
</section>--%>