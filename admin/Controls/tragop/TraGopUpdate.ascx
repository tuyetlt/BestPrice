<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TraGopUpdate.ascx.cs" Inherits="admin_Controls_tragop_TraGopUpdate" %>
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
                    <!-- Phone -->
                    <div class="form-group">
                        <div>Phone </div>
                        <div>
                            <input type="text" id="phone" name="phone" value="<%= dr["Phone"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Email -->
                    <div class="form-group">
                        <div>Email </div>
                        <div>
                            <input type="text" id="email" name="email" value="<%= dr["Email"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Address -->
                    <div class="form-group">
                        <div>Address </div>
                        <div>
                            <input type="text" id="address" name="address" value="<%= dr["Address"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Product -->
                    <div class="form-group">
                        <div>Product </div>
                        <div>
                            <input type="text" id="product" name="product" value="<%= dr["Product"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Info -->
                    <div class="form-group">
                        <div>Info </div>
                        <div>
                            <textarea  id="info" name="info"><%= dr["Info"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- BankOrCard -->
                    <div class="form-group">
                        <div>BankOrCard </div>
                        <div>
                            <input type="text" id="bankorcard" name="bankorcard" value="<%= dr["BankOrCard"].ToString()%>" />
                        </div>
                    </div>
                    <!-- AdminNote -->
                    <div class="form-group">
                        <div>AdminNote </div>
                        <div>
                            <textarea id="adminnote" name="adminnote"><%= dr["AdminNote"].ToString()%></textarea>
                        </div>
                    </div>
                   <!-- Status -->
                    <div class="form-group">
                        <div>Status </div>
                        <div>
                            <input type="hidden" name="status" id="status" value="<%= dr["Status"].ToString()%>" />
                            <select id="drStatus">
                                <option value="Moi">Mới</option>
                                <option value="Da_Xu_Ly">Đã Xử Lý</option>
                            </select>

                            <script type="text/javascript">
                                $('#drStatus option[value=<%= dr["Status"].ToString()%>]').attr('selected', 'selected');
                                $("#drStatus").change(function () {
                                    var vl = $(this).val();
                                    $("#status").val($(this).val());
                                });
                            </script>


                            <%--<input type="text" id="status" name="status" value="<%= dr["Status"].ToString()%>" />--%>
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
                                        DeleteByID('<%= dr["ID"].ToString() %>', 'tblTraGop', '<%= ControlAdminInfo.ShortName %>');
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
