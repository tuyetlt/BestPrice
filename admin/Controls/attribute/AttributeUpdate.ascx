<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributeUpdate.ascx.cs" Inherits="admin_Controls_AttributeUpdate" %>
<%@ Import Namespace="System.Data" %>

<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString()%>" />
        <div class="container">
            <div class="edit">
                <div class="left">
                    <!-- Name -->
                    <div class="form-group">
                        <div>Tên </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- FriendlyUrl -->
                    <div class="form-group">
                        <div>FriendlyUrl </div>
                        <div>
                            <input type="text" id="friendlyurl" name="friendlyurl" value="<%= dr["FriendlyUrl"].ToString()%>" />
                        </div>
                    </div>

                    <!-- NameDisplay -->
                    <div class="form-group">
                        <div>Tên hiển thị </div>
                        <div>
                            <input type="text" id="namedisplay" name="namedisplay" value="<%= dr["NameDisplay"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Description -->
                    <div class="form-group">
                        <div>Mô tả ngắn </div>
                        <div>
                            <input type="text" id="description" name="description" value="<%= dr["Description"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Image_1 -->
                    <div class="form-group image-ck">
                        <div>Banner 1 </div>

                        <div data-thumb="thumbnail_image_1" data-inputtext="image_1" data-folder="brand">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_1 %>" id="thumbnail_image_1" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_1" name="image_1" value="<%= dr["Image_1"].ToString()%>" />
                        </div>
                    </div>

                  
                    <!-- ParentID -->
                    <div class="form-group">
                        <div>Mục cha </div>
                        <div>
                            <select name="parentid" id="parentid">
                                <option value="0">-Mục gốc-</option>
                                <% DataTable dtParent = SqlHelper.SQLToDataTable(table, "ID, Name", "ParentID=0 OR ParentID is null");
                                    if (Utils.CheckExist_DataTable(dtParent))
                                    {

                                        foreach (DataRow drParent in dtParent.Rows)
                                        {
                                            string selected = "";
                                            if (drParent["ID"].ToString() == dr["ParentID"].ToString())
                                                selected = "selected='selected'";
                                %>
                                <option <%= selected %> value="<%= drParent["ID"] %>"><%= drParent["Name"] %></option>
                                <%
                                        }
                                    }

                                %>
                            </select>
                        </div>
                    </div>




                </div>
                <div class="right">
                </div>

                <!--- Submit Button -->

                <div class="clear"></div>
                <div class="form-group">
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
    </form>
</div>
