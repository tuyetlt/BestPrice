<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TagUpdate.ascx.cs" Inherits="admin_Controls_category_TagUpdate" %>

<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString()%>" />
        <div class="container">
            <div class="edit">
                <div class="left">
                    <!-- Name -->
                    <div class="form-group">
                        <div>Tên Tag </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- FriendlyUrl -->
                    <div class="form-group">
                        <div>Url </div>
                        <div>
                            <input type="text" id="friendlyurl" name="friendlyurl" value="<%= dr["FriendlyUrl"].ToString()%>" />
                        </div>
                    </div>


                    <!-- Sort -->
                    <div class="form-group">
                        <div>Sắp xếp </div>
                        <div>
                            <input type="text" id="sort" name="sort" value="<%= dr["Sort"].ToString()%>" />
                        </div>
                    </div>

                    <div class="flex">
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
                        <div>
                            <div>Kiểu Tag</div>
                            <div>
                                <input type="hidden" id="moduls" name="moduls" value="tag" />
                                <input type="radio" name="tag" id="tag" checked />
                                <label for="tag">Tag</label><br>

                                <%  string check = "";
                                    if (dr["Moduls"].ToString().ToLower() == "hashtag")
                                        check = " checked";
                                %>
                                <input type="radio" name="tag" id="HashTag" <%= check %> />
                                <label for="HashTag">Tag Ẩn</label>

                                <script type="text/javascript">
                                    $("input[name='tag']").change(function () {
                                        if ($(this).is(":checked")) {
                                            var val = $(this).attr("id");
                                            $("#moduls").val(val);
                                        }
                                    });
                                </script>
                            </div>
                        </div>
                    </div>

                    <!-- Mô tả sản phẩm -->
                    <div class="form-group" id="div_noidung">
                        <div>Nội dung </div>
                        <div>
                            <textarea name="longdescription" id="longdescription" rows="5" class="ckeditor"><%= dr["LongDescription"].ToString()%></textarea>
                        </div>
                    </div>

                </div>
                <div class="right">
                    <!-- MetaTitle -->
                    <div class="form-group">
                        <div>Meta Title </div>
                        <div>
                            <span class="count-char">1234</span>
                            <textarea id="metatitle" name="metatitle"><%= dr["MetaTitle"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- MetaKeyword -->
                    <div class="form-group">
                        <div>Meta Keyword </div>
                        <div>
                            <span class="count-char">123</span>
                            <textarea id="metakeyword" name="metakeyword"><%= dr["MetaDescription"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- MetaDescription -->
                    <div class="form-group">
                        <div>Meta Description </div>
                        <div>
                            <span class="count-char">123</span>
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

                    <!-- Image_1 -->
                    <div class="form-group image-ck">
                        <div>Banner 1 </div>

                        <div data-thumb="thumbnail_image_1" data-inputtext="image_1" data-folder="category_banner/thumbnail_category">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_1 %>" id="thumbnail_image_1" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_1" name="image_1" value="<%= dr["Image_1"].ToString()%>" />
                        </div>
                    </div>
                </div>
                <!--- Submit Button -->

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
                        <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-arrow-left"></i>Bỏ Qua</button>
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
    </form>
</div>
