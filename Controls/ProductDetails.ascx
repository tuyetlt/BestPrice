<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductDetails.ascx.cs" Inherits="Controls_ProductDetails" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Newtonsoft.Json" %>

<% List<GalleryImage> galleryList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<GalleryImage>>(dr["Gallery"].ToString());
    List<string> imgList = new List<string>();
%>

<div class="main main__wrapper">
    <div class="container">
        <div class="heading d-flex align-items-end">
            <div class="col">
                <% 
                    List<BreadCrumb> breadCrumbList = new List<BreadCrumb>();
                    try
                    {
                        string bCJson = ConvertUtility.ToString(drCat["BreadCrumbJson"]);
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
        <div class="row">
            <div class="column__main col-12 col-sm-12">
                <div class="product__detail">
                    <div class="product__detail-top row mb-3">
                        <div class="product__columnLeft col-12 col-sm-6 col-md-4">
                            <div class="product__galleries">
                                <div class="product__galleries-main">
                                    <div class="product-image-gallery slider productMainSliders">

                                        <%
                                            if (galleryList != null && galleryList.Count > 0)
                                            {
                                                int count = 0;
                                                foreach (GalleryImage gallery in galleryList)
                                                {
                                                    imgList.Add(C.MAIN_URL + gallery.Path);
                                                    string alt = dr["name"].ToString() + " " + count.ToString();
                                                    count++;
                                        %>
                                        <div class="img-thumb">
                                            <img src="<%= C.MAIN_URL %><%= gallery.Path %>?width=500&height=500&quality=100" alt="<%= alt %>" />
                                        </div>
                                        <%
                                                }
                                            }
                                        %>
                                    </div>
                                </div>


                                <%
                                    if (galleryList != null && galleryList.Count > 1)
                                    { %>

                                <div class="product__galleries-bottom">
                                    <div class="product__thumbnail">
                                        <div class="product__thumbnail-inner">
                                            <div class="slider product__galleries-thumb product-media-sliders">
                                                <%
                                                    int count = 0;
                                                    foreach (GalleryImage gallery in galleryList)
                                                    {
                                                        string alt = dr["name"].ToString() + " " + count.ToString();
                                                        count++;
                                                %>
                                                <div class="product__item">
                                                    <div class="product__image">
                                                        <div class="product__thumb">
                                                            <img src="<%= C.MAIN_URL %><%= HttpUtility.UrlDecode(gallery.Path) %>?width=80&height=80&quality=100" alt="<%= alt %>" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <%
                                                    }%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%
                                    }
                                %>
                            </div>
                        </div>

                        <% ProductStatusFlag statusFlag = (ProductStatusFlag)ConvertUtility.ToInt32(dr["ProductStatusFlag"]);
                            string strStatus = string.Empty;
                            if (statusFlag.ToString() != ProductStatusFlag.None.ToString())
                            {
                                if (statusFlag.HasFlag(ProductStatusFlag.InStock))
                                    strStatus = "Có hàng";
                                else if (statusFlag.HasFlag(ProductStatusFlag.Contact))
                                    strStatus = "Liên hệ";
                                else if (statusFlag.HasFlag(ProductStatusFlag.Importing))
                                    strStatus = "Đang nhập";
                                else if (statusFlag.HasFlag(ProductStatusFlag.OutStock))
                                    strStatus = "Hết hàng";
                            }
                        %>

                        <div class="product__columnRight col-12 col-sm-6 col-md-5">
                            <h2 class="page__title"><%= dr["Name"] %></h2>
                            <div class="product__brand mb-3">
                                <span>Tình trạng:</span> <%= strStatus %>
                            </div>
                            <%--<div class="product__price mb-3">
                                <strong class="price"><%= string.Format("{0:N0} {1}", dr["Price"], "VNĐ") %></strong>
                                <% if (SqlHelper.GetPrice_Decimal(ConvertUtility.ToInt32(dr["ID"]), "Price1", true) > 0)
                                    { %>
                                <div class="d-flex align-items-center">
                                    <div class="me-2">
                                        <span class="txt-price me-1">Giá gốc:</span>
                                        <span class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(dr["ID"]), "Price1", true) %></span>
                                    </div>
                                    <div><span class="txt-price me-1">Tiết kiệm:</span><strong><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(dr["ID"])) %></strong></div>
                                </div>
                                <% } %>
                            </div>--%>
                            <div class="product__price product_price_sale  mb-3">
                                <div class="cnt-box-price">
                                    <div class="txt-price me-1">Giá online rẻ hơn:</div>
                                    <strong class="price"><%= string.Format("{0:N0} {1}", dr["Price"], "VNĐ") %></strong>
                                    <% if (SqlHelper.GetPrice_Decimal(ConvertUtility.ToInt32(dr["ID"]), "Price1", true) > 0)
                                        { %>
                                    <div class="d-flex align-items-center text-price">
                                        <div class="">
                                            <span class="old-price"><%= SqlHelper.GetPrice(ConvertUtility.ToInt32(dr["ID"]), "Price1", true) %></span>
                                        </div>
                                        <div class="box-percent"><span><%= SqlHelper.GetPricePercent(ConvertUtility.ToInt32(dr["ID"])) %></span></div>
                                    </div>
                                    <% } %>
                                </div>
                                <div class="cnt-box-time">
                                     <div class="timeCountdown" data-date="December 24, 2024 21:14:01">
                                         <span class="hours"></span>
                                         <b>:</b>
                                         <span class="minutes"></span>
                                         <b>:</b>
                                         <span class="seconds"></span>
                                     </div>
                                </div>
                            </div>
                            <div class="product__attr">
                                <%= dr["Description"] %>
                            </div>

                            <div id="product" class="product__options">
                                <div class="product__action">
                                    <form method="post" enctype="multipart/form-data" id="frm_giohang">
                                        <label class="control-label label-qty mb-2" for="input-quantity">Số lượng</label>
                                        <div class="product__action-addCart input-group mb-4">
                                            <div class="product__qty">
                                                <div class="input-group block-qty-cart">
                                                    <div class="input-group-btn">
                                                        <button type="button" class="btn btn-default btn-touchspin-down"><i class="icon-minus"></i></button>
                                                    </div>
                                                    <div class="input-group-btn">
                                                        <input type="text" name="quantity" value="1" size="1" id="input-quantity" class="inputQty text-center form-control">
                                                    </div>
                                                    <div class="input-group-btn">
                                                        <button class="btn btn-default btn-touchspin-up" type="button"><i class="icon-plus"></i></button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="buttons-set col">
                                                <input type="hidden" value="<%= dr["ID"].ToString() %>" name="hdfProductID" />
                                                <input type="hidden" id="done_giohang" name="done_giohang" value="" />
                                                <button type="button" id="button-cart" data-loading-text="Loading..." class="btn btn-primary btn-add-cart d-block">Mua Ngay</button>
                                            </div>
                                        </div>

                                    </form>

                                    <p>
                                        <a class="btn btn-outline-primary btn-lg d-block buy_payment" href="tel:<%= ConfigWeb.Hotline %>">Gọi đặt mua: <strong><%= ConfigWeb.Hotline %></strong> </a>
                                    </p>
                                    <%-- <ul class="product__commit list-unstyled row">
                                        <%
                                            string filter = string.Format("ParentID=0 AND Flags & {0} <> 0", (int)BaseTableFlag.Service);
                                            DataTable dtParent = SqlHelper.SQLToDataTable("tblBase", "", filter, "Sort");


                                            if (Utils.CheckExist_DataTable(dtParent))
                                            {
                                                foreach (DataRow drParent in dtParent.Rows)
                                                {
                                                    string image = Utils.GetFirstImageInGallery_Json(drParent["Gallery"].ToString());
                                        %>
                                        <li class="col-12 col-sm-6">
                                            <div class="product__commit-img">
                                                <img src="<%= image %>" alt="<%= Utils.RemoveHtmlAndWhiteSpace(drParent["Name"].ToString()) %>" />
                                            </div>
                                            <p><%= drParent["Name"].ToString() %></p>
                                        </li>
                                        <%
                                                }
                                            } %>
                                    </ul>--%>
                                </div>
                            </div>
                        </div>

                        <div class="product__support col-12 col-sm-3 hidden-xs-sm">
                            <div class="product__support-inner mb-3">
                                <h3 class="product__support-title">CAM KẾT CHẤT LƯỢNG</h3>
                                <ul class="list-unstyled">
                                    <%
                                        string filter1 = string.Format("ParentID=0 AND Flags & {0} <> 0", (int)BaseTableFlag.Social);
                                        DataTable dtParent1 = SqlHelper.SQLToDataTable("tblBase", "", filter1, "Sort");
                                        if (Utils.CheckExist_DataTable(dtParent1))
                                        {
                                            foreach (DataRow drParent1 in dtParent1.Rows)
                                            {
                                                string image = Utils.GetFirstImageInGallery_Json(drParent1["Gallery"].ToString());
                                    %>
                                    <li class="d-flex align-items-center">
                                        <span class="img-icon">
                                            <img src="<%= image %>" alt="<%= Utils.RemoveHtmlAndWhiteSpace(drParent1["Name"].ToString()) %>" /></span>
                                        <div><%= drParent1["Name"].ToString() %></div>
                                    </li>
                                    <%
                                            }
                                        } %>
                                </ul>
                            </div>

                            <%-- <div class="product__gift">
                                <h4 class="gift__title">Quà tặng đính kèm</h4>
                                <div class="product__gift-inner">
                                    <ul class="gift__list list-unstyled">
                                        <li class="gift__item">Tặng thêm vợt muỗi <a href="#" class="txt-link">YAGE YG-D003</a> áp dụng tất cả đơn hàng tủ lạnh trên 10 triệu.
                                        </li>
                                        <li class="gift__item">Giá khuyến mãi áp dụng từ <span class="txt-link">15/05/2023</span> đến hết <span class="txt-link">21/05/2023.</span>
                                        </li>
                                        <li class="gift__item">Giao <strong>siêu tốc trong ngày</strong> Giao siêu tốc trong ngày cho đơn hàng từ 3 triệu trong phạm vi 10km.
                                        </li>
                                    </ul>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                    <hr class="hr-line mb-5" />

                    <div id="description" class="product__description mb-5">
                        <div class="productReadMore readMore up-description">
                            <%= dr["Tab1"].ToString() %>
                            <%= longDescription %>
                        </div>
                    </div>

                    <div class="product__related mb-5">
                        <% string filterProduct = string.Format(@"(Hide is null OR Hide=0) AND (CategoryIDList Like N'%,{0},%' OR CategoryIDParentList Like N'%,{0},%' OR TagIDList Like N'%,{0},%')", PageInfo.CategoryID);
                        %>
                        <%=Utils.LoadUserControl("~/Controls/UCHomeProduct.ascx", "Sản phẩm cùng danh mục", "", filterProduct, 0, false, "Detail", 0) %>
                    </div>
                   
                </div>
            </div>
        </div>
    </div>
</div>



<%--<div class="modal fade" id="modaReview" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-lg">
        <div class="modal-content">
            <div class="modal-body">
                <div class="modal-heading d-flex align-items-center">
                    <h3 class="modal-title" id="staticBackdropLabel">Đánh giá Loa tháp Samsung MX-ST50B/XV</h3>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body__inner">
                    <div class="form-group comment-form-comment">
                        <textarea rows="5" id="comment" name="comment" placeholder="Mời bạn chia sẻ thêm một số cảm nhận..." class="form-control"></textarea>
                    </div>
                    <div class="form-group wrap-attaddsend">
                        <div class="d-flex align-items-center">
                            <div class="review-attach">
                                <span class="btn-attach devvn_insert_attach">Gửi ảnh thực tế</span>
                            </div>
                            <span id="countContent" class="ms-auto">0 ký tự (Tối thiểu 100)</span>
                        </div>
                    </div>
                    <div class="form-group form_row_reviews row">
                        <div class="col-6 comment-form-author">
                            <input id="author" name="author" type="text" value="" placeholder="Họ tên (bắt buộc)" class="form-control" />
                        </div>
                        <div class="col-6 comment-form-email">
                            <input id="email" name="email" type="email" value="" placeholder="Email" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="buttons text-center mt-4 mb-3">
                    <button type="button" class="btn btn-primary">Gửi đánh giá</button>
                </div>
                <div class="note_review"><u>Lưu ý:</u> để đánh giá được phê duyệt, vui lòng tham khảo <a href="#" title="" target="_blank" class="txt-link">Điều khoản và Điều kiện</a></div>
            </div>
        </div>
    </div>
</div>--%>

<input type="hidden" value="1" id="productDetail" />
<input type="hidden" value="product" id="GG_Page" />
<input type="hidden" value="<%= ConvertUtility.ToString(dr["ID"]) %>" id="GG_ID" />
<input type="hidden" value="<%= ConvertUtility.ToString(dr["Name"]) %>" id="GG_ProductName" />
<input type="hidden" value="<%= Utils.PriceConversion_Product(dr["Price"]) %>" id="GG_Price" />
<input type="hidden" value="<%= Utils.PriceConversion_Product(dr["Price1"]) %>" id="GG_Price1" />



<script type="application/ld+json">
{
  "@context": "https://schema.org/", 
  "@type": "Product", 
  "name": "<%= Utils.QuoteRemove(dr["Name"].ToString()) %>",
  "image": [
     <%
    int countIMG = 0;
    foreach (string img in imgList)
    {
        countIMG++;
        %>
    "<%= img %>"
    <%
        if (countIMG < imgList.Count)
            Response.Write(",");
    } %> 
    ],
  "description": "<%= Utils.QuoteRemove(SEO_Schema.Description) %>",
  "offers": {
    "@type": "Offer",
    "url": "<%= SEO_Schema.Url %>",
    "priceCurrency": "VND",
    "price": "<%= SEO_Schema.Price %>",
    "priceValidUntil": "2040-04-12",
    "availability": "https://schema.org/InStock",
    "itemCondition": "https://schema.org/NewCondition"
  },
  "aggregateRating": {
    "@type": "AggregateRating",
    "bestRating": "100",
    "ratingValue": "<%= SEO_Schema.RatingValue %>",
    "ratingCount": "<%= SEO_Schema.RatingCount %>"
  }, 
    "sku": "<%= TextChanger.Translate(ConfigWeb.SiteName, "_") %><%= dr["ID"].ToString() %>",
            "mpn": "<%= TextChanger.Translate(ConfigWeb.SiteName, "_") %><%= dr["ID"].ToString() %>",
    "review": {
                "@type": "Review",
                "reviewRating": {
                    "@type": "Rating",
                    "ratingValue": "<%= SEO_Schema.ReviewRatingValue %>",
                    "bestRating": "5"
                },
                "author": {
                    "@type": "Person",
                    "name": "<%= ConfigWeb.SiteName %>"
                }
            },
     "brand": {
                "@type": "Brand",
                "name": "<%= SEO_Schema.Brand %>"
            }
}
</script>




<script type="text/javascript">
    window.dataLayer = window.dataLayer || [];
    dataLayer.push({
        'event': 'view_item',
        'ecommerce': {
            'items': [{
                'item_name': '<%= Utils.QuoteRemove(dr["Name"].ToString()) %>',
                'item_id': '<%= dr["ID"].ToString() %>',
                'item_category': '<%= dr["ProductType"] %>',
                'price': <%= SEO_Schema.Price %>,
                'currency': 'VND',
                'quantity': 1
            }]
        }
    });
</script>
