<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VideoUpdate.ascx.cs" Inherits="admin_Controls_video_VideoUpdate" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="left">
                    <!-- Tên sản phẩm -->
                    <div class="form-group">
                        <div>Tiêu đề <span class="required">*</span></div>
                        <div>
                            <input type="text" required id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Url Thân thiện -->
                    <div class="form-group">
                        <div>Url Thân thiện <span class="required">*</span></div>
                        <div>
                            <input type="text" id="friendlyurl" name="friendlyurl" value="<%= dr["FriendlyUrl"].ToString()%>" />
                        </div>
                    </div>
                    <!--- Danh mục -->
                    <div class="form-group">
                        <div>Danh mục<span class="required">*</span></div>
                        <div>
                            <input type="text" id="categoryid" name="categoryidlist" value="<%= Utils.CommaSQLRemove(dr["CategoryIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="categoryname" name="categorynamelist" value="<%= CatNameList %>" style="display: none" />
                            <select id="drCategory" multiple data-level="1" name="itemSelect" style="width: 100%"></select>
                        </div>
                    </div>

                    <!-- Thumbnail -->

                    <div class="form-group image-ck">
                        <div>Thumbnail </div>
                        <div data-thumb="thumbnail_icon" data-inputtext="thumbnail" data-folder="thumbnail">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= thumbnail %>" id="thumbnail_icon" alt="Chọn Thumbnail" />
                            </a>
                            <input type="text" id="thumbnail" name="thumbnail" class="thumbnail" value="<%= dr["Thumbnail"].ToString()%>" />
                        </div>
                    </div>


                    <!-- Link -->
                    <div class="form-group">
                        <div>Link </div>
                        <div>
                            <input type="text" id="link" name="link" value="<%= dr["Link"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Description -->
                    <div class="form-group">
                        <div>Mô tả </div>
                        <div>
                            <textarea name="description" id="description" rows="5" class="ckeditor"><%= dr["Description"].ToString()%></textarea>
                        </div>
                    </div>


                    <!--- Tags -->
                    <div class="form-group">
                        <div>Tags</div>
                        <div>
                            <input type="text" id="tag_id" name="tagidlist" value="<%= Utils.CommaSQLRemove(dr["TagIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="tag_name" name="tag_name" value="<%= Utils.CommaSQLRemove(dr["TagNameList"].ToString()) %>" style="display: none" />
                            <select id="drTag" data-idreturn="Name" multiple data-level="0" data-folder="<%= Utils.GetFolderControlAdmin() %>"></select>
                        </div>
                    </div>


                    <!--- Tag Ẩn -->
                    <div class="form-group">
                        <div>Tags Ẩn</div>
                        <div>
                            <input type="text" id="hashtag_id" name="hashtagidlist" value="<%= Utils.CommaSQLRemove(dr["HashTagIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="hashtag_name" name="hashtag_name" value="<%= Utils.CommaSQLRemove(dr["HashTagNameList"].ToString()) %>" style="display: none" />
                            <select id="drHashTag" data-idreturn="Name" multiple data-level="0" data-folder="<%= Utils.GetFolderControlAdmin() %>"></select>
                        </div>
                    </div>





                    <!--- Flag -->
                    <div class="flex threecol">
                        <div></div>
                        <div>
                            <div>Tùy chọn</div>
                            <div>
                                <input type="checkbox" id="HomeArticle" name="HomeArticle" <%= Utils.SetChecked(_flag.HasFlag(ArticleFlag.HomeArticle)) %> />
                                <label for="HomeArticle">Đặt lên Trang chủ</label><br>
                            </div>
                        </div>
                        <div>
                            <div>Ẩn</div>
                            <div>
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



                </div>
                <div class="right">
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
                        <button type="submit" data-value="delete" class="btnSubmit validate btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>
                        <%}
                            else
                            { %>
                        <button type="submit" data-value="saveandadd" class="btnSubmit btnAddAndAdd"><i class="fas fa-plus"></i>Thêm</button>
                        <button type="submit" data-value="saveandback" class="btnSubmit btnAddAndBack"><i class="fas fa-plus"></i>Thêm và Quay Lại</button>
                        <button type="submit" data-value="saveandcopy" class="btnSubmit btnAddAndCopy"><i class="fas fa-copy"></i>Thêm và Sao Chép</button>
                        <% } %>

                        <button type="submit" data-value="cancel" class="btnSubmit validate btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>
                        <input type="hidden" id="done" name="done" value="0" />


                        <script type="text/javascript">
                            $(".btnSubmit").click(function () {
                                var dataValue = $(this).attr("data-value");
                                if (dataValue == "delete")
                                    DeleteByID('<%= dr["ID"].ToString() %>', '<%= table %>', '<%= ControlAdminInfo.ShortName %>');

                                if ($(this).hasClass("validate") || ValidateForm()) {
                                    $('#frm_edit #done').val(dataValue);
                                    $(this).attr('disabled', 'disabled');
                                    $(this).html('Loading...');
                                    $("#frm_edit").submit();
                                }
                                else {
                                    return false;
                                }
                            });
                        </script>
                    </div>
                </div>
            </div>
        </div>
    </form>
</div>
