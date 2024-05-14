<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AttributeConfigUpdate.ascx.cs" Inherits="admin_Controls_AttributeConfigUpdate" %>


<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString()%>" />
        <div class="container">
            <div class="edit">
                                <div class="full">

                <!-- Name -->
                <div class="form-group">
                    <div>Name </div>
                    <div>
                        <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" /></div>
                </div>
                <!-- FriendlyUrl -->
                <div class="form-group">
                    <div>FriendlyUrl </div>
                    <div>
                        <input type="text" id="friendlyurl" name="friendlyurl" value="<%= dr["FriendlyUrl"].ToString()%>" /></div>
                </div>
                <!-- ParentID -->
                <div class="form-group">
                    <div>ParentID </div>
                    <div>
                        <input type="text" id="parentid" name="parentid" value="<%= dr["ParentID"].ToString()%>" /></div>
                </div>
                <%--      <!--- Thuộc tính hệ thống -->

                    <div class="form-group">
                        <div>Moduls</div>
                        <div>
                            <input type="text" id="modul_id" name="moduls" value="<%= Utils.CommaSQLRemove(dr["Moduls"].ToString()) %>" style="display: none" />
                            <input type="text" id="modul_name" value="<%= ModulsName %>" style="display: none" />
                            <select id="drModuls" multiple data-level="0" data-control="<%= Utils.GetControlAdmin() %>" style="width: 100%"></select>
                        </div>
                    </div>--%>

                      <div class="form-group">
                        <div>Áp dụng cho Control</div>
                        <div>
                            <input type="text" id="attr_c_id" name="moduls" value="<%= Utils.CommaSQLRemove(dr["Moduls"].ToString()) %>" style="display: none" />
                            <input type="text" id="attr_c_name" value="<%= ModulsName %>" style="display: none" />
                            <select id="drAttrC" multiple data-level="1" data-idreturn="FriendlyUrl" data-folder="<%= Utils.GetFolderControlAdmin() %>" name="drAttrC" style="width: 100%"></select>
                        </div>
                    </div>



                <!--- Submit Button -->

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
        </div>
    </form>
</div>
