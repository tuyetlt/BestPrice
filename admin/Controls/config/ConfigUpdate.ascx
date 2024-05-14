<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfigUpdate.ascx.cs" Inherits="admin_Controls_ConfigUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="1" />
        <div class="container">
            <div class="edit">
                <div class="left">
                    <!-- SiteName -->
                    <div class="form-group">
                        <div>Tên Website </div>
                        <div>
                            <input type="text" id="sitename" name="sitename" value="<%= dr["SiteName"].ToString()%>" />
                        </div>
                    </div>

                    <!-- SiteUrl -->
                    <div class="form-group">
                        <div>Địa chỉ Website </div>
                        <div>
                            <input type="text" id="siteurl" name="siteurl" value="<%= dr["SiteUrl"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Slogan -->
                    <div class="form-group">
                        <div>Slogan </div>
                        <div>
                            <input type="text" id="slogan" name="slogan" value="<%= dr["Slogan"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Hotline -->
                    <div class="form-group">
                        <div>Hotline </div>
                        <div>
                            <input type="text" id="hotline" name="hotline" value="<%= dr["Hotline"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Hotline -->
                    <div class="form-group">
                        <div>Hotline bảo hành</div>
                        <div>
                            <input type="text" id="hotlinebh" name="hotlinebh" value="<%= dr["HotlineBH"].ToString()%>" />
                        </div>
                    </div>

                    <%--     <!-- Address -->
                    <div class="form-group">
                        <div>Address </div>
                        <div>
                            <input type="text" id="address" name="address" value="<%= dr["Address"].ToString()%>" />
                        </div>
                    </div>--%>

                    <!-- Email -->
                    <div class="form-group">
                        <div>Email </div>
                        <div>
                            <input type="text" id="email" name="email" value="<%= dr["Email"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Email_Receiving -->
                    <div class="form-group">
                        <div>Email nhận đơn hàng</div>
                        <div>
                            <input type="text" id="email_receiving" name="email_receiving" value="<%= dr["Email_Receiving"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Logo -->
                    <div class="form-group image-ck">
                        <div>Logo </div>

                        <div data-thumb="thumbnail_logo" data-inputtext="logo" data-folder="logo">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= logo %>" id="thumbnail_logo" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="logo" name="logo" class="get_ck" value="<%= dr["Logo"].ToString()%>" />
                        </div>
                    </div>

                    <div class="form-group image-ck">
                        <div>Logo Admin</div>

                        <div data-thumb="thumbnail_logo1" data-inputtext="logoadmin" data-folder="logoadmin">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= logoadmin %>" id="thumbnail_logo1" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="logoadmin" name="logoadmin" class="get_ck" value="<%= dr["LogoAdmin"].ToString()%>" />
                        </div>
                    </div>


                    <!-- Logo -->
                    <div class="form-group image-ck">
                        <div>Logo chân trang </div>

                        <div data-thumb="thumbnail_logof" data-inputtext="logofooter" data-folder="logo">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= dr["LogoFooter"].ToString() %>" id="thumbnail_logof" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="logofooter" name="logofooter" class="get_ck" value="<%= dr["LogoFooter"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Icon -->

                    <div class="form-group image-ck">
                        <div>Biểu tượng </div>

                        <div data-thumb="thumbnail_icon" data-inputtext="icon" data-folder="logo">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= icon %>" id="thumbnail_icon" alt="Chọn Icon" />
                            </a>
                            <input type="text" id="icon" name="icon" class="get_ck" value="<%= dr["Icon"].ToString()%>" />
                        </div>
                    </div>



                    <!-- Image -->
                    <div class="form-group image-ck">
                        <div>Ảnh đại diện </div>

                        <div data-thumb="thumbnail_image" data-inputtext="image" data-folder="logo">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image %>" id="thumbnail_image" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image" name="image" class="get_ck" value="<%= dr["Image"].ToString()%>" />
                        </div>
                    </div>

                    <!-- ProductNumberInCategory -->
                    <%--   <div class="form-group">
                        <div>ProductNumberInCategory </div>
                        <div>
                            <input type="text" id="productnumberincategory" name="productnumberincategory" value="<%= dr["ProductNumberInCategory"].ToString()%>" />
                        </div>
                    </div>--%>

                    <%--  <!-- HeaderText -->
                    <div class="form-group">
                        <div>HeaderText </div>
                        <div>
                            <textarea id="headertext" name="headertext" rows="3" class="ckeditor"><%= dr["HeaderText"].ToString()%></textarea>
                        </div>
                    </div>--%>

                    <!-- FooterText -->
                    <div class="form-group">
                        <div>Địa chỉ chân trang </div>
                        <div>
                            <textarea id="footer_address" name="footer_address" rows="3" class="ckeditor"><%= dr["Footer_Address"].ToString()%></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <div>Chân trang 1</div>
                        <div>
                            <textarea id="footer1" name="footer1" rows="3" class="ckeditor"><%= dr["Footer1"].ToString()%></textarea>
                        </div>
                    </div>
                    <div class="form-group">
                        <div>Chân trang 2</div>
                        <div>
                            <textarea id="footer2" name="footer2" rows="3" class="ckeditor"><%= dr["Footer2"].ToString()%></textarea>
                        </div>
                    </div>

                    <!-- FooterText -->
                    <%--   <div class="form-group">
                        <div>Số điện thoại chân trang </div>
                        <div>
                            <textarea id="footer_phone" name="footer_phone" rows="3" class="ckeditor"><%= dr["Footer_Phone"].ToString()%></textarea>
                        </div>
                    </div>--%>

                    <!-- TextHome1 -->
                    <div class="form-group">
                        <div>Text box trang chủ 1 </div>
                        <div>
                            <input type="text" id="texthome1" name="texthome1" value="<%= dr["TextHome1"].ToString()%>" />
                        </div>
                    </div>
                    <!-- TextHome2 -->
                    <div class="form-group">
                        <div>Text box trang chủ 2 </div>
                        <div>
                            <input type="text" id="texthome2" name="texthome2" value="<%= dr["TextHome2"].ToString()%>" />
                        </div>
                    </div>
                    <!-- TextHome3 -->
                    <div class="form-group">
                        <div>Text box trang chủ 3 </div>
                        <div>
                            <input type="text" id="texthome3" name="texthome3" value="<%= dr["TextHome3"].ToString()%>" />
                        </div>
                    </div>
                    <!-- TextHome4 -->
                    <div class="form-group">
                        <div>Text box trang chủ 4 </div>
                        <div>
                            <input type="text" id="texthome4" name="texthome4" value="<%= dr["TextHome4"].ToString()%>" />
                        </div>
                    </div>
                    <!-- TextHome5 -->
                    <div class="form-group">
                        <div>Text box trang chủ 5 </div>
                        <div>
                            <input type="text" id="texthome5" name="texthome5" value="<%= dr["TextHome5"].ToString()%>" />
                        </div>
                    </div>

                    <!-- BoCongThuongLink -->
                    <div class="form-group">
                        <div>Bộ Công Thương Link </div>
                        <div>
                            <input type="text" id="bocongthuonglink" name="bocongthuonglink" value="<%= dr["BoCongThuongLink"].ToString()%>" /></div>
                    </div>




                    <div class="form-group">
                        <div>Nội dung trang bản đồ </div>
                        <div>
                            <textarea id="mappage" name="mappage" rows="3" class="ckeditor"><%= dr["MapPage"].ToString()%></textarea>
                        </div>
                    </div>

                    <!-- MapLocation -->
                    <div class="form-group">
                        <div>Tọa độ bản đồ </div>
                        <div>
                            <input type="text" id="maplocation" name="maplocation" value="<%= dr["MapLocation"].ToString()%>" />
                        </div>
                    </div>
                    <!-- MapLocation -->
                    <div class="form-group">
                        <div>Tọa độ bản đồ (Phía Bắc)</div>
                        <div>
                            <input type="text" id="maplocation1" name="maplocation1" value="<%= dr["MapLocation1"].ToString()%>" />
                        </div>
                    </div>
                    <!-- MapLocation -->
                    <div class="form-group">
                        <div>Tọa độ bản đồ (Phía Nam)</div>
                        <div>
                            <input type="text" id="maplocation2" name="maplocation2" value="<%= dr["MapLocation2"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Style -->
                    <div class="form-group">
                        <div>Mẫu giao diện </div>
                        <div>
                            <input type="text" id="style" name="style" value="<%= dr["Style"].ToString()%>" />
                        </div>
                    </div>


                    <!-- CodeHeader -->
                    <div class="form-group">
                        <div>CodeHeader </div>
                        <div>
                            <textarea id="codeheader" name="codeheader"><%= dr["CodeHeader"].ToString()%></textarea>
                        </div>
                    </div>

                    <!-- CodeBody -->
                    <div class="form-group">
                        <div>CodeBody </div>
                        <div>
                            <textarea id="codebody" name="codebody"><%= dr["CodeBody"].ToString()%></textarea>
                        </div>
                    </div>

                    <!-- SortProduct -->
                    <div class="form-group">
                        <div>Sắp xếp sản phẩm </div>
                        <div>

                            <% List<Tuple<string, string>> listSortProduct = new List<Tuple<string, string>>();
                                listSortProduct.Add(new Tuple<string, string>("Price ASC, ID DESC", "Giá thấp nhất trước"));
                                listSortProduct.Add(new Tuple<string, string>("CASE WHEN (price = 0 or price is null) THEN 1 ELSE 0 END, price, ID DESC", "Giá thấp nhất trước (trừ sp chưa có giá)"));
                                listSortProduct.Add(new Tuple<string, string>("Price DESC, ID DESC", "Giá cao nhất trước"));
                                listSortProduct.Add(new Tuple<string, string>("ID DESC", "Mới nhất trước"));
                                listSortProduct.Add(new Tuple<string, string>("ID ASC", "Cũ nhất trước"));
                                listSortProduct.Add(new Tuple<string, string>("Sort ASC", "STT nhỏ xếp trên"));
                                listSortProduct.Add(new Tuple<string, string>("Sort DESC", "STT lớn xếp trên"));
                            %>

                            <select id="sortproduct" name="sortproduct" style="width: 300px">
                                <%
                                    foreach (Tuple<string, string> tuple in listSortProduct)
                                    {
                                        string selected = "";
                                        if (tuple.Item1 == dr["SortProduct"].ToString())
                                            selected = "selected";
                                        Response.Write(string.Format(@"<option value=""{0}"" {1}>{2}</option>", tuple.Item1, selected, tuple.Item2));
                                    }
                                %>
                            </select>
                        </div>
                    </div>
                    <!-- SortProductHome -->
                    <div class="form-group">
                        <div>Sắp xếp sản phẩm trang chủ </div>
                        <div>

                            <% List<Tuple<string, string>> listSortHomeProduct = new List<Tuple<string, string>>();
                                listSortHomeProduct.Add(new Tuple<string, string>("Price ASC, ID DESC", "Giá thấp nhất trước"));
                                listSortProduct.Add(new Tuple<string, string>("CASE WHEN (price = 0 or price is null) THEN 1 ELSE 0 END, price, ID DESC", "Giá thấp nhất trước (trừ sp chưa có giá)"));
                                listSortHomeProduct.Add(new Tuple<string, string>("Price DESC, ID DESC", "Giá cao nhất trước"));
                                listSortHomeProduct.Add(new Tuple<string, string>("ID DESC", "Mới nhất trước"));
                                listSortHomeProduct.Add(new Tuple<string, string>("ID ASC", "Cũ nhất trước"));
                                listSortHomeProduct.Add(new Tuple<string, string>("Sort ASC", "STT nhỏ xếp trên"));
                                listSortHomeProduct.Add(new Tuple<string, string>("Sort DESC", "STT lớn xếp trên"));
                            %>

                            <select id="sortproducthome" name="sortproducthome" style="width: 300px">
                                <%
                                    foreach (Tuple<string, string> tuple in listSortHomeProduct)
                                    {
                                        string selected = "";
                                        if (tuple.Item1 == dr["SortProductHome"].ToString())
                                            selected = "selected";
                                        Response.Write(string.Format(@"<option value=""{0}"" {1}>{2}</option>", tuple.Item1, selected, tuple.Item2));
                                    }
                                %>
                            </select>

                        </div>
                    </div>
                    <!-- SortArticle -->
                    <div class="form-group">
                        <div>Sắp xếp tin </div>
                        <div>
                            <% List<Tuple<string, string>> listSortArticle = new List<Tuple<string, string>>();
                                listSortArticle.Add(new Tuple<string, string>("ID DESC", "Mới nhất trước"));
                                listSortArticle.Add(new Tuple<string, string>("ID ASC", "Cũ nhất trước"));
                                listSortArticle.Add(new Tuple<string, string>("Sort ASC", "STT nhỏ xếp trên"));
                                listSortArticle.Add(new Tuple<string, string>("Sort DESC", "STT lớn xếp trên"));
                            %>
                            <select id="sortarticle" name="sortarticle" style="width: 300px">
                                <%
                                    foreach (Tuple<string, string> tuple in listSortHomeProduct)
                                    {
                                        string selected = "";
                                        if (tuple.Item1 == dr["SortArticle"].ToString())
                                            selected = "selected";
                                        Response.Write(string.Format(@"<option value=""{0}"" {1}>{2}</option>", tuple.Item1, selected, tuple.Item2));
                                    }
                                %>
                            </select>
                        </div>
                    </div>

                    <!-- OAZalo -->
                    <div class="form-group">
                        <div>OAZalo </div>
                        <div>
                            <input type="text" id="oazalo" name="oazalo" value="<%= dr["OAZalo"].ToString()%>" />
                        </div>
                    </div>
                    <!-- FacebookID -->
                    <div class="form-group">
                        <div>FacebookID </div>
                        <div>
                            <input type="text" id="facebookid" name="facebookid" value="<%= dr["FacebookID"].ToString()%>" />
                        </div>
                    </div>




                    <!--- Submit Button -->

                    <div class="form-group">
                        <div>&nbsp;</div>
                        <div>

                            <button type="submit" data-value="saveandadd" class="btnSubmit btnSaveAndAdd"><i class="fas fa-save"></i>Cập nhật</button>

                            <input type="hidden" id="done" name="done" value="0" />

                            <script type="text/javascript">
                                $(".btnSubmit").click(function () {
                                    var dataValue = $(this).attr("data-value");
                                    if (dataValue == "delete")
                                        DeleteByID('<%= dr["ID"].ToString() %>', '<%= table %>', '<%= ControlAdminInfo.ShortName %>');
                                    $('#frm_edit #done').val(dataValue);
                                    $(this).attr('disabled', 'disabled');
                                    $(this).html('Loading...');
                                    $("#frm_edit").submit();
                                });
                            </script>
                        </div>
                    </div>


                </div>
                <div class="right">
                    <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />

                    <!-- MetaTitle -->
                    <div class="form-group">
                        <div>Meta Title </div>
                        <div>
                            <span class="count-char"></span>
                            <textarea id="metatitle" name="metatitle"><%= dr["MetaTitle"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- MetaKeyword -->
                    <div class="form-group">
                        <div>Meta Keyword </div>
                        <div>
                            <span class="count-char"></span>
                            <textarea id="metakeyword" name="metakeyword"><%= dr["MetaKeyword"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- MetaDescription -->
                    <div class="form-group">
                        <div>Meta Description </div>
                        <div>
                            <span class="count-char"></span>
                            <textarea id="metadescription" name="metadescription"><%= dr["MetaDescription"].ToString()%></textarea>
                        </div>
                    </div>

                    <div class="google-demo">
                        <p>
                            <img src="/admin/images/google-logo.png" />
                        </p>
                        <p class="title">Mô phỏng Website của bạn khi xuất hiện trên Google</p>
                        <p class="description">Lưu ý đây chỉ là mô phỏng website xuất hiện trên Google, để lên thực sự thì cần rất nhiều yếu tố khác</p>
                    </div>

                    <!-- SchemaLatitude -->
                    <div class="form-group">
                        <div>SchemaLatitude </div>
                        <div>
                            <input type="text" id="schemalatitude" name="schemalatitude" value="<%= dr["SchemaLatitude"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaLongitude -->
                    <div class="form-group">
                        <div>SchemaLongitude </div>
                        <div>
                            <input type="text" id="schemalongitude" name="schemalongitude" value="<%= dr["SchemaLongitude"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaSameAs -->
                    <div class="form-group">
                        <div>SchemaSameAs </div>
                        <div>
                            <input type="text" id="schemasameas" name="schemasameas" value='<%= ConvertUtility.ToString(dr["SchemaSameAs"]) %>' />
                        </div>
                    </div>
                    <!-- SchemaStreetAddress -->
                    <div class="form-group">
                        <div>SchemaStreetAddress </div>
                        <div>
                            <input type="text" id="schemastreetaddress" name="schemastreetaddress" value="<%= dr["SchemaStreetAddress"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaAddressLocality -->
                    <div class="form-group">
                        <div>SchemaAddressLocality </div>
                        <div>
                            <input type="text" id="schemaaddresslocality" name="schemaaddresslocality" value="<%= dr["SchemaAddressLocality"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaPostalCode -->
                    <div class="form-group">
                        <div>SchemaPostalCode </div>
                        <div>
                            <input type="text" id="schemapostalcode" name="schemapostalcode" value="<%= dr["SchemaPostalCode"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaTelephone -->
                    <div class="form-group">
                        <div>SchemaTelephone </div>
                        <div>
                            <input type="text" id="schematelephone" name="schematelephone" value="<%= dr["SchemaTelephone"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaBestRating -->
                    <div class="form-group">
                        <div>SchemaBestRating </div>
                        <div>
                            <input type="text" id="schemabestrating" name="schemabestrating" value="<%= dr["SchemaBestRating"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaRatingValue -->
                    <div class="form-group">
                        <div>SchemaRatingValue </div>
                        <div>
                            <input type="text" id="schemaratingvalue" name="schemaratingvalue" value="<%= dr["SchemaRatingValue"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SchemaRatingCount -->
                    <div class="form-group">
                        <div>SchemaRatingCount </div>
                        <div>
                            <input type="text" id="schemaratingcount" name="schemaratingcount" value="<%= dr["SchemaRatingCount"].ToString()%>" />
                        </div>
                    </div>

                    <!-- AdressFunction -->
                    <div class="form-group">
                        <div>AdressFunction </div>
                        <div>
                            <textarea id="adressfunction" name="adressfunction" rows="3" class="ckeditor"><%= dr["AdressFunction"].ToString()%></textarea>
                        </div>
                    </div>


                    <%-- <!-- ContactFunction -->
                    <div class="form-group">
                        <div>ContactFunction </div>
                        <div>
                            <input type="text" id="contactfunction" name="contactfunction" value="<%= dr["ContactFunction"].ToString()%>" /></div>
                    </div>--%>


                    <div class="flex">

                        <div>
                            <div></div>
                            <div>
                                <input type="checkbox" name="sitemap" id="sitemap" />
                                <label for="sitemap">Gen Sitemap</label><br>
                            </div>
                        </div>
                        <div>
                            <div></div>
                            <div>
                                <input type="checkbox" name="ggshopping" id="ggshopping" />
                                <label for="ggshopping">Tạo lại GG Shopping</label><br>
                            </div>
                        </div>
                        <div>
                            <a target="_blank" href="/admin/ajax/ajax.aspx?ctrl=tool">Tool</a> | <a target="_blank" href="/upload/gg-shopping-list.txt">Google Shopping Link</a> | <a target="_blank" href="/sitemap.xml">Sitemap Link</a>
                        </div>
                        <%--  <div>
                            <%
                                string filterCat = "";
                                DataTable dtCate = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID,Name,FriendlyUrl", string.Format("(Hide is null OR Hide=0) AND AttrMenuFlag & {0}<>0", (int)AttrMenuFlag.PriorityAds), "ID DESC");
                                if (Utils.CheckExist_DataTable(dtCate))
                                {
                                    filterCat = "( ";
                                    int count = 0;
                                    foreach (DataRow drCat in dtCate.Rows)
                                    {
                                        
                                        Response.Write(drCat["Name"] + "<br />");
                                        if (count > 0)
                                            filterCat += " OR ";
                                        count++;
                                        filterCat += string.Format("(CategoryIDList like '%,{0},%' OR CategoryIDParentList like '%,{0},%')", drCat["ID"].ToString());

                                    }
                                    filterCat += ")";
                                }

                                string filter = string.Format("(Hide is null OR Hide=0) AND Len(FriendlyUrlCategory)>0 AND " + filterCat);
                                DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID", filter, "ID DESC");
                                if (Utils.CheckExist_DataTable(dt))
                                {
                                    Response.Write(dt.Rows.Count);
                                }

                                %>
                        </div>--%>
                    </div>


                </div>
            </div>
        </div>

    </form>
</div>
