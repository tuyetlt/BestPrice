<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductCategory.ascx.cs" Inherits="Controls_ProductCategory" %>
<%@ Import Namespace="System.Data" %>

<input type="hidden" id="idCategory" value="<%= drCat["ID"] %>" />
<input type="hidden" id="pageIndex" value="1" />
<input type="hidden" id="pageSize" value="<%= C.ROWS_PRODUCTCATEGORY %>" />
<input type="hidden" id="totalProduct" value="<%= _totalProduct %>" />
<input type="hidden" id="rootFilterCategoryID" value="0" />
<input type="hidden" id="attributeIDList" value="" />
<input type="hidden" id="categoryName" value="<%= drCat["Name"] %>" />
<input type="hidden" id="loadByFilter" value="0" />
<input type="hidden" id="thuonghieu" value="<%= thuonghieu %>" />

<input type="hidden" id="AttributesIDList" value="<%= Utils.CommaSQLRemove(drCat["AttributesIDList"].ToString()) %>" />
<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-md-flex align-items-end">
            <div class="col mb-3 mb-md-0">
                <h1 class="page__title"><%= categoryTitle %></h1>
                <%=Utils.LoadUserControl("~/Controls/WidgetBreadcrumb.ascx") %>
            </div>
            <div class="col">
                <div class="d-flex justify-content-between justify-content-end align-items-center">
                    <button type="button" class="btn btn-primary btn-sidebar-filter hidden-md"><i class="fa fa-filter"></i>LỌC</button>
                   <%-- <form class="ms-sm-auto" method="get">
                        <select name="orderby" class="form-select" aria-label="Đơn hàng của cửa hàng">
                            <option value="date" selected="selected">Mới nhất</option>
                            <option value="rating">Điểm đánh giá</option>
                            <option value="popularity">Mức độ phổ biến</option>
                            <option value="price">Giá từ thấp đến cao</option>
                            <option value="price-desc">Giá từ cao xuống thấp</option>
                        </select>
                    </form>--%>
                </div>
            </div>
        </div>
    </div>
    <section class="category category__topPage">
        <div class="container">
          <%--  <div class="heading">
                <h2 class="heading__title"><span><%= categoryTitle %></span></h2>
            </div>--%>
            <div class="category__inner">
                <div class="category-list row">
                    <%
                        int flagStyle2 = (int)PositionMenuFlag.Style2;

                        string filter_style2 = string.Format("(Hide is null OR Hide=0) AND PositionMenuFlag & {0} <> 0", flagStyle2);
                        DataTable dt_style2 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,Name,FriendlyUrl,Link,LinkTypeMenuFlag,ParentID,PositionMenuFlag,LinkTypeMenuFlag,Icon,HashTagUrlList", string.Format("ParentID={0} AND {1}", RootID, filter_style2), "Sort");
                        if (Utils.CheckExist_DataTable(dt_style2))
                        {
                            foreach (DataRow dr_3 in dt_style2.Rows)
                            {
                                string icon = C.NO_IMG_PATH;
                                if (!string.IsNullOrEmpty(dr_3["Icon"].ToString()))
                                    icon = dr_3["Icon"].ToString();
                                string link = Utils.CreateCategoryLink(dr_3["LinkTypeMenuFlag"], dr_3["FriendlyUrl"], dr_3["Link"]);
                                string selected = string.Empty;
                                if (ConvertUtility.ToInt32(drCat["ID"]) == ConvertUtility.ToInt32(dr_3["ID"]))
                                    selected = " selected";
                    %>
                    <div class="category__item col-3 col-sm-2">
                        <div class="category__item-inner<%= selected %>">
                            <a href="<%= link %>" class="category__image">
                                <div class="img-thumb">
                                    <img src="<%= icon %>" alt="<%= dr_3["Name"].ToString() %>" />
                                </div>
                            </a>
                            <h2 class="category__name"><a href="<%= link %>" class="category__link"><%= dr_3["Name"].ToString() %></a></h2>
                        </div>
                    </div>
                    <% }
                        } %>
                   
                </div>
            </div>
        </div>
    </section>

    <div class="container">

        <div class="row two-column">
            <div class="column__left col-12 col-md-3">
                <div class="sidebar">
                    <button class="btn-close btn-close-sidebar hidden-md"></button>
                    <%
                        string filter3 = string.Format("(Hide is null OR Hide=0) AND ParentID={0}", drCat["ID"]);
                        DataTable dt_3 = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "LinkTypeMenuFlag,FriendlyUrl,Link,ID,Icon,Name", filter3, "Sort");
                        if (Utils.CheckExist_DataTable(dt_3))
                        {
                    %>
                    <aside class="sidebar__inner sidebar__categories">
                        <h3 class="sidebar__title">Danh mục sản phẩm</h3>
                        <div class="tags">
                            <%  foreach (DataRow dr_3 in dt_3.Rows)
                                {
                                    string icon = C.NO_IMG_PATH;
                                    if (!string.IsNullOrEmpty(dr_3["Icon"].ToString()))
                                        icon = dr_3["Icon"].ToString();
                                    string link = Utils.CreateCategoryLink(dr_3["LinkTypeMenuFlag"], dr_3["FriendlyUrl"], dr_3["Link"]);

                                    string selected = string.Empty;
                                    if (ConvertUtility.ToInt32(drCat["ID"]) == ConvertUtility.ToInt32(dr_3["ID"]))
                                        selected = " selected";

                            %>
                            <a href="<%= link %>" class="tags__item<%= selected %>"><%= dr_3["Name"].ToString() %></a>
                            <% } %>
                        </div>
                    </aside>
                    <% } %>
                    <aside class="sidebar__filters">
                        <form method="POST" class="sidebar__filters-inner">

                            <div id="filted">
                            </div>

                            <div class="sidebar__inner filter-ajax">
                                <h4 class="sidebar__title">Công suất loa</h4>
                                <div class="sidebar__content">
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input type="checkbox" name="name" value="" class="form-check-input" checked />
                                            <span>Trên 1000W</span>
                                        </label>
                                    </div>
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input type="checkbox" name="name" value="" class="form-check-input" />
                                            <span>Từ 100 W - 500 W</span>
                                        </label>
                                    </div>
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input type="checkbox" name="name" value="" class="form-check-input" />
                                            <span>Từ 1000 W - 2000 W</span>
                                        </label>
                                    </div>
                                    <div class="form-check">
                                        <label class="form-check-label">
                                            <input type="checkbox" name="name" value="" class="form-check-input" />
                                            <span>Từ 500 W - 1000 W</span>
                                        </label>
                                    </div>
                                </div>
                            </div>


                        </form>
                    </aside>
                </div>
            </div>
            <div class="column__main col-12 col-md-9">
                <div class="product__wrapper">
                    <div class="product__grid row mb-5 product-list" id="products-container">

                        <% 
                            if (Utils.CheckExist_DataTable(dtProduct))
                            {
                                foreach (DataRow drProduct in dtProduct.Rows)
                                {
                                    string linkDetail = TextChanger.GetLinkRewrite_Products(ConvertUtility.ToString(drProduct["FriendlyUrlCategory"]), ConvertUtility.ToString(drProduct["FriendlyUrl"]));

                        %>


                        <div class="product__item col-6 col-sm-4 col-md-4 col-lg-3">
                            <div class="product__inner">
                                <a href="<%= linkDetail %>" title="<%= drProduct["Name"].ToString() %>" class="product__image">
                                <div class="product__thumb">

                                    <% if (!string.IsNullOrEmpty(SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"]))))
                                        { %>
                                    <label class="on-sale"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(drProduct["ID"])) %></span></label>
                                    <% } %>
                                   
                                        <img src="<%= Utils.GetFirstImageInGallery_Json(drProduct["Gallery"].ToString(), 300, 300) %>" alt="<%= drProduct["Name"].ToString() %>" width="350" height="400" />
                                    <div class="timeCountdown" data-date="December 24, 2024 21:14:01">
                                        <span class="hours"></span>
                                        <b>:</b>
                                        <span class="minutes"></span>
                                        <b>:</b>
                                        <span class="seconds"></span>
                                    </div>
                                    <div class="frame-flash-sale">
                                        <img src="/themes/images/summer.png" alt="Alternate Text" />
                                    </div>
                                     <div class="frame-label-sale">
                                         <img src="/themes/images/sale.webp" alt="Sale" />
                                     </div>
                                    <div class="icon-flash-sale">
                                        <img src="/themes/images/icon-flash-sale.png" alt="Alternate Text" />
                                    </div>
                                </div>
                                    </a>
                                <div class="product__info">
                                    <h3 class="product__name"><a href="<%= linkDetail %>"><%= drProduct["Name"].ToString() %></a></h3>

                                    <div class="product__price d-flex align-items-center">
                                        <div class="price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price") %></div>
                                        <div class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(drProduct["ID"]), "Price1") %></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%}
                            } %>
                    </div>
                    <div class="clear"></div>

                    <% if (_totalProduct > C.ROWS_PRODUCTCATEGORY)
                        { %>
                    <div class="container-btn show-more"><a id="category_paging" class="btn-see-more">Xem thêm</a></div>
                    <%} %>
                </div>
                <div class="section-readmore-cate">
                    <% if (!Utils.IsNullOrEmpty(drCat["LongDescription"].ToString()))
                        { %>
                    <%   string AddressFunction = ConfigWeb.AdressFunction;
                        string ContentHtml = ConvertUtility.ToString(drCat["LongDescription"]);
                        ContentHtml = ContentHtml.Replace("{Address}", AddressFunction);
                    %>
                    <div id="description_cate" class="description_cate">
                        <%= ContentHtml %>
                    </div>
                    <div class="bg-article"></div>
                    <p class="show-more">
                        <a id="readmore" href="javascript:;" class="readmore">Đọc thêm</a>
                    </p>

                    <%} %>




                    <%
                        string TagsList = drCat["TagIDList"].ToString().Trim(',');
                        if (!Utils.IsNullOrEmpty(TagsList))
                        {
                            DataTable dtTag = SqlHelper.SQLToDataTable("tblCategories", "Name,FriendlyUrl", string.Format("ID IN ({0})", TagsList));
                            if (dtTag != null && dtTag.Rows.Count > 0)
                            {
                    %>
                    <div class="clear"></div>
                    <div class="entry-tags">
                        <ul>

                            <%
                                foreach (DataRow drTag in dtTag.Rows)
                                {
                                    string link = TextChanger.GetLinkRewrite_Category(drTag["FriendlyUrl"].ToString());
                                    string tagName = drTag["Name"].ToString();
                            %>
                            <li class="entry-tag-item"><a href="<%= link %>" title="<%= tagName %>"><%= tagName %></a></li>
                            <%
                                }
                            %>
                        </ul>
                    </div>
                    <div class="clear"></div>
                    <%
                            }
                        }
                    %>
                </div>
            </div>
        </div>
    </div>
</div>



<script id="product-template" type="text/template">
    <div class="product__item col-6 col-sm-4 col-md-4 col-lg-3">
        <div class="product__inner">
            <div class="product__thumb">
                <label class="on-sale"><span>{{PricePercent}}</span></label>
                <a href="{{Link}}" title="{{Name}}" class="product__image">
                    <img src="{{Image}}" alt="{{Name}}" width="350" height="400" />
                </a>
            </div>
            <div class="product__info">
                <h3 class="product__name"><a href="{{Link}}">{{Name}}</a></h3>
                <div class="product__price d-flex align-items-center">
                    <div class="price">{{Price}}</div>
                    <div class="old-price">{{Price1}}</div>
                </div>
            </div>
        </div>
    </div>
</script>



<input type="hidden" value="category" id="GG_Page" />
<input type="hidden" value="<%= string.Format("{0:0}", 1000000) %>" id="GG_Price" />



<script type="application/ld+json">
{
"@context" : "http://schema.org",
"@type" : "<%= SEO_Schema.Type %>",
"name" : "<%= Utils.QuoteRemove(SEO_Schema.Title) %>",
"alternateName" : "<%= Utils.QuoteRemove(SEO_Schema.Description) %>",
"url" : "<%= SEO_Schema.Url %>",
"image" : "<%= SEO_Schema.Image %>",
"author": {
"@type": "Person",
    "name": "<%= ConfigWeb.SiteName %>",
  "url": "<%= ConfigWeb.SiteUrl %>"
},
"datePublished" : "<%= SEO_Schema.PublisherDate %>",
"headline" : "<%= Utils.QuoteRemove(SEO_Schema.Title) %>",
"dateModified" : "<%= SEO_Schema.PublisherDate %>",
"mainEntityOfPage": {
  "@type": "WebPage",
  "@id": "<%= SEO_Schema.Url %>"
},
"publisher": {
  "@type": "Organization",
  "name": "<%= SEO_Schema.Publisher_Name %>",
  "logo": {
    "@type": "ImageObject",
    "url": "<%= SEO_Schema.Publisher_Logo %>"
  }
}
}
</script>
