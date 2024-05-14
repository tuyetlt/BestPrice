<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BannerUpdate.ascx.cs" Inherits="admin_Controls_banner_BannerUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="full">
                    <!-- Tên -->
                    <div class="form-group">
                        <div>Tên banner </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>


                    <!-- Link -->
                    <div class="form-group">
                        <div>Link </div>
                        <div>
                            <input type="text" id="link" name="link" value="<%= dr["Link"].ToString()%>" /></div>
                    </div>
                    <!-- Alt -->
                    <div class="form-group">
                        <div>Alternate Text</div>
                        <div>
                            <input type="text" id="alt" name="alt" value="<%= dr["Alt"].ToString()%>" /></div>
                    </div>

                    <!-- Sort -->
                    <div class="form-group">
                        <div>Sắp xếp </div>
                        <div>
                            <input type="text" id="sort" name="sort" value="<%= dr["Sort"].ToString()%>" /></div>
                    </div>



                    <%--                    <!--- Thuộc tính hệ thống -->

                    <div class="form-group">
                        <div>Thuộc tính</div>
                        <div>
                            <input type="text" id="attr_c_id" name="attributeconfigidlist" value="<%= dr["AttributeConfigUrlList"].ToString().Trim(',') %>" style="display: none" />
                            <input type="text" id="attr_c_name" value="<%= Attr_Config_NameList %>" style="display: none" />
                            <select id="drAttrC" multiple data-level="1" data-idreturn="FriendlyUrl" data-folder="<%= Utils.GetFolderControlAdmin() %>" name="drAttrC" style="width: 100%"></select>
                        </div>
                    </div>--%>

                    
                    <!--- Danh mục sản phẩm -->

                    <div class="form-group">
                        <div>Danh mục<span class="required">*</span></div>
                        <div>
                            <input type="text" id="categoryid" name="categoryidlist" value="<%= Utils.CommaSQLRemove(dr["CategoryIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="categoryname" name="categorynamelist" value="<%= CatNameList %>" style="display: none" />
                            <select id="drCategory" multiple data-level="1" name="itemSelect" style="width: 100%"></select>
                        </div>
                    </div>


                    <!-- StartDate -->
                    <div class="form-group">
                        <div>Ngày bắt đầu </div>
                        <div>
                            <input type="text" class="datepicker" autocomplete="off" id="startdate" name="startdate" value="<%= dr["StartDate"].ToString()%>" />
                        </div>
                    </div>
                    <!-- EndDate -->
                    <div class="form-group">
                        <div>Ngày kết thúc </div>
                        <div>
                            <input type="text" class="datepicker" autocomplete="off" id="enddate" name="enddate" value="<%= (dr["EndDate"].ToString()) %>" />
                        </div>
                    </div>

                    <!-- Image_1 -->
                    <div class="form-group image-ck">
                        <div>Hình ảnh</div>

                        <div data-thumb="thumbnail_image_1" data-inputtext="image_1" data-folder="banner">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_1 %>" id="thumbnail_image_1" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_1" name="image_1" value="<%= dr["Image_1"].ToString()%>" />
                        </div>
                    </div>
                   <div class="flex threecol">
                        <div></div>
                        <div>
                            <div>Vị trí banner</div>
                            <div>
                                <input type="checkbox" id="HomeSlider" name="HomeSlider" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.HomeSlider)) %> />
                                <label for="HomeSlider">Slider Trang chủ</label><br>
                                <input type="checkbox" id="RightSlider" name="RightSlider" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.RightSlider)) %> />
                                <label for="RightSlider">Bên phải Slider</label><br>
                                 <input type="checkbox" id="BottomSlider" name="BottomSlider" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.BottomSlider)) %> />
                                <label for="BottomSlider">Phía dưới Slider</label><br>
                                <input type="checkbox" id="ByCategory" name="ByCategory" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.ByCategory)) %> />
                                <label for="ByCategory">Theo danh mục</label><br>
                                <input type="checkbox" id="RightProductDetail" name="RightProductDetail" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.RightProductDetail)) %> />
                                <label for="RightProductDetail">Bên phải chi tiết sản phẩm</label><br>
                                <input type="checkbox" id="TopBanner" name="TopBanner" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.TopBanner)) %> />
                                <label for="TopBanner">Banner Top</label><br>
                                <input type="checkbox" id="Popup" name="Popup" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.Popup)) %> />
                                <label for="Popup">Popup</label><br>
                            </div>
                        </div>
                        <div>
                            <div>Tùy chỉnh</div>
                            <div>
                                <input type="checkbox" id="OpenNewWindows" name="OpenNewWindows" <%= Utils.SetChecked(bannerPositionFlag.HasFlag(BannerPositionFlag.OpenNewWindows)) %> />
                                <label for="OpenNewWindows">Mở liên kết trong tab mới</label><br>
                                <%
                                    string isHide = "";
                                    if (!string.IsNullOrEmpty(dr["Hide"].ToString()) && ConvertUtility.ToBoolean(dr["Hide"]))
                                        isHide = " checked";
                                %>
                                <input type="checkbox" name="hide" id="hide" <%= isHide %> />
                                <label for="hide">Tạm ẩn</label><br>
                            </div>
                        </div>
                    </div>
                    <!--- Submit Button -->
                    <div class="clear"></div>
                    <div class="form-group submit">
                        <div>&nbsp;</div>
                        <div>

                            <% if (IsUpdate)
                                { %>
                            <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Lưu</button>
                            <button type="submit" data-value="saveandadd" class="btnSubmit btnSaveAndAdd"><i class="fas fa-save"></i>Lưu và Thêm</button>
                            <button type="submit" data-value="saveandback" class="btnSubmit btnSaveAndBack"><i class="fas fa-save"></i>Lưu và Quay Lại</button>
                            <button type="submit" data-value="saveandcopy" class="btnSubmit btnSaveAndCopy"><i class="fas fa-copy"></i>Lưu và Sao Chép</button>
                            <button type="submit" data-value="delete" class="btnSubmit btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>
                            <%}
                                else
                                { %>
                            <button type="submit" data-value="saveandadd" class="btnSubmit btnAddAndAdd"><i class="fas fa-plus"></i>Thêm</button>
                            <button type="submit" data-value="saveandback" class="btnSubmit btnAddAndBack"><i class="fas fa-plus"></i>Thêm và Quay Lại</button>
                            <button type="submit" data-value="saveandcopy" class="btnSubmit btnAddAndCopy"><i class="fas fa-copy"></i>Thêm và Sao Chép</button>
                            <% } %>

                            <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>


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


            </div>
        </div>
    </form>
</div>
