<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WidgetSlider.ascx.cs" Inherits="Controls_WidgetSlider" %>
<%@ Import Namespace="System.Data" %>

<section class="banner banner__slideshow mb-4">
    <div class="banner__section">
        <div class="banner__section-inner">
            <div class="slider createBannerSliders">
                <%
                    string filter0 = string.Format("Flags & {0} <> 0 AND {1}", (int)BannerPositionFlag.HomeSlider, Utils.CreateFilterHide);

                    DataTable dt = SqlHelper.SQLToDataTable(C.BANNER_TABLE, "Link,Image_1,Alt", filter0, "Sort", 1, 10);
                    if (Utils.CheckExist_DataTable(dt))
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string link = ConvertUtility.ToString(dr["Link"]);
                %>
                <div class="banner__item">
                    <div class="banner__item-inner">
                        <img src="<%= dr["Image_1"] %>" alt="<%= dr["Alt"] %>" />
                    </div>
                </div>
                <% }
                    } %>
            </div>
        </div>
    </div>
</section>

<section class="banner banner__advert mb-4">
    <div class="banner__advert-inner container">
        <div class="row">
            <%
                string filter1 = string.Format("Flags & {0} <> 0 AND {1}", (int)BannerPositionFlag.BottomSlider, Utils.CreateFilterHide);
                DataTable dt1 = SqlHelper.SQLToDataTable(C.BANNER_TABLE, "Link,Image_1,Alt", filter1, "Sort", 1, 6);
                if (Utils.CheckExist_DataTable(dt))
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string link = ConvertUtility.ToString(dr["Link"]);
            %>
            <div class="col-12 col-sm-4 mb-3 mb-sm-0">
                <a href="<%= link %>" class="banner__image">
                    <img src="<%= dr["Image_1"] %>" alt="<%= dr["Alt"] %>"></a>
            </div>
            <% }
                } %>
        </div>
    </div>
</section>
