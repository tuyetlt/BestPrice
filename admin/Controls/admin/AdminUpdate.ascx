<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminUpdate.ascx.cs" Inherits="admin_Controls_AdminUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="full">
                    <!-- Name -->
                    <div class="form-group">
                        <div>Name </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Thư viện ảnh -->
                    <div class="form-group">
                        <div>Thư viện ảnh </div>
                        <div>
                            <input type="text" id="gallery" name="gallery" style="display: none" value='<%= dr["Gallery"].ToString()%>' />
                           
                            <div id="dZUpload" class="dropzone" data-folder="img">
                                <div class="dz-default dz-message">
                                    Bấm hoặc kéo ảnh vào vùng này để tải ảnh.
                                </div>
                            </div>
                        </div>
                    </div>
                     <!-- Email -->
                    <div class="form-group">
                        <div>Email </div>
                        <div>
                            <input type="text" id="email" name="email" autocomplete="off" value="<%= dr["Email"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Password -->
                    <div class="form-group">
                        <div>Password </div>
                        <div>
                            <input type="password" autocomplete="off" id="password" name="password" value="<%= Crypto.DecryptData(Crypto.KeyCrypto, dr["Password"].ToString())%>" />
                        </div>
                    </div>
                    <!-- Permission -->
                    <div class="form-group">
                        <div>Phân quyền</div>
                        <div>
                            <select id="drPermission" data-level="1" name="permission" style="width: 100%">
                                <% 
                                    if (dtPermission!=null && dtPermission.Rows.Count > 0)
                                    {
                                        foreach (DataRow item in dtPermission.Rows)
                                        {
                                            string quyen = dr["Permission"].ToString();
                                            string selected = "";
                                            if (item["ID"].ToString() == quyen)
                                                selected = " selected='selected'";

                                            Response.Write(string.Format(@"<option value=""{0}""{1}>{2}</option>", item["ID"], selected, item["Name"]));
                                        }
                                    }
                                %>
                            </select>
                            <script type="text/javascript">
                                $('#drPermission').select2({
                                    placeholder: 'Chọn quyền'
                                });
                            </script>

                        </div>
                    </div>

                 <%--   <div class="form-group">
                        <div>Trạng thái</div>
                        <div>
                            <input type="text" id="attr_c_id" name="attributeconfigidlist" value="<%= Utils.CommaSQLRemove(dr["AttributeConfigIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="attr_c_name" value="<%= Attr_Config_NameList %>" style="display: none" />
                            <select id="drAtrrC" multiple data-level="1" data-control="<%= Utils.GetControlAdmin() %>" name="drAtrrC" style="width: 100%"></select>
                        </div>
                    </div>--%>

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
                                if (dataValue == "delete") {
                                    if (!confirm('Bạn có chắc chắn muốn xoá?')) {
                                        return false;
                                    }
                                }
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
