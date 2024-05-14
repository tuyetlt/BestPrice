<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PermissionUpdate.ascx.cs" Inherits="admin_Controls_PermissionUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="full">
                    <!-- Name -->

                    <!-- Name -->
                    <div class="form-group">
                        <div>Name </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Role -->
                    <div class="form-group" style="display:none">
                        <div>Tên </div>
                        <div>
                            <textarea id="role" name="role">
                                <%= dr["Role"].ToString()%>
                            </textarea>

                        </div>
                    </div>

                    <div class="form-group">
                        <div>Quyền</div>
                        <div>


                            <table class="tablePermission custom" style="width: 50%; border-collapse: collapse; border-color: #dbdbdb; padding: 10px" border="1">

                                  <tr data-id="default" data-control="default">
                                    <td>
                                        <b>Mặc định</b>
                                    </td>
                                    <td>
                                        <input type="radio" id="ALLOW_default" name="default" onclick="GetValueFromRadio()" data-value="ALLOW">
                                        <label for="ALLOW_default">Cho phép</label>
                                 &nbsp;
                                        <input type="radio" id="DENIED_default" name="default" onclick="GetValueFromRadio()" data-value="DENIED">
                                        <label for="DENIED_default">Cấm</label>

                                 &nbsp;
                                        <input type="radio" id="OWNER_default" name="default" onclick="GetValueFromRadio()" data-value="OWNER">
                                        <label for="OWNER_default">Cá nhân</label>
                                    </td>
                                </tr>



                                <%
                                    using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
                                    {
                                        string sqlQuery = string.Format(string.Format("SELECT * FROM tblAdminMenu WHERE ParentID=0 ORDER BY Sort"));
                                        var dt = db.ExecuteSqlDataTable(sqlQuery);
                                        if (dt.Rows.Count > 0)
                                        {
                                            foreach (System.Data.DataRow drMenu in dt.Rows)
                                            {
                                %>

                                <tr data-id="<%= drMenu["ID"] %>" data-control="<%= drMenu["Control"] %>">
                                    <td>
                                        <b><%= drMenu["Name"] %></b>
                                    </td>
                                    <td>
                                        <input type="radio" id="ALLOW_<%= drMenu["ID"] %>" name="<%= drMenu["ID"] %>" onclick="GetValueFromRadio()" data-value="ALLOW">
                                        <label for="ALLOW_<%= drMenu["ID"] %>">Cho phép</label>
                                 &nbsp;
                                        <input type="radio" id="DENIED_<%= drMenu["ID"] %>" name="<%= drMenu["ID"] %>" onclick="GetValueFromRadio()" data-value="DENIED">
                                        <label for="DENIED_<%= drMenu["ID"] %>">Cấm</label>

                                 &nbsp;
                                        <input type="radio" id="OWNER_<%= drMenu["ID"] %>" name="<%= drMenu["ID"] %>" onclick="GetValueFromRadio()" data-value="OWNER">
                                        <label for="OWNER_<%= drMenu["ID"] %>">Cá nhân</label>
                                    </td>
                                </tr>


                                <%

                                    using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
                                    {
                                        string sqlQueryx = string.Format(string.Format("SELECT * FROM tblAdminMenu WHERE ParentID={0} ORDER BY Sort", drMenu["ID"]));
                                        var dtx = dbx.ExecuteSqlDataTable(sqlQueryx);
                                        if (dtx.Rows.Count > 0)
                                        {

                                            foreach (System.Data.DataRow drx in dtx.Rows)
                                            {
                                %>

                                <tr data-id="<%= drx["ID"] %>" data-control="<%= drx["Control"] %>">
                                    <td>
                                        -- <%= drx["Name"] %>
                                    </td>
                                    <td>
                                        <input type="radio" id="ALLOW_<%= drx["ID"] %>" name="<%= drx["ID"] %>" onclick="GetValueFromRadio()" data-value="ALLOW">
                                        <label for="ALLOW_<%= drx["ID"] %>">Cho phép</label>
                                      &nbsp;
                                        <input type="radio" id="DENIED_<%= drx["ID"] %>" name="<%= drx["ID"] %>" onclick="GetValueFromRadio()" data-value="DENIED">
                                        <label for="DENIED_<%= drx["ID"] %>">Cấm</label>

                                      &nbsp;
                                        <input type="radio" id="OWNER_<%= drx["ID"] %>" name="<%= drx["ID"] %>" onclick="GetValueFromRadio()" data-value="OWNER">
                                        <label for="OWNER_<%= drx["ID"] %>">Cá nhân</label>
                                    </td>
                                </tr>


                                <%
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                %>
                            </table>

                            <script type="text/javascript">

                                BindDataToRadio();


                                function BindDataToRadio() {
                                    var datas = JSON.parse($("#role").text());
                                    //console.log(datas);
                                    $.each(datas, function (index, value) {

                                        var radioB = $('#' + value.Access + '_' + value.ID);
                                        if (radioB.length) {
                                            radioB.prop('checked', true);
                                            console.log(value.Access + '_' + value.ID);
                                        }
                                    });
                                }



                                function GetValueFromRadio() {
                                    var datas = [];

                                    $('.tablePermission tr').each(function () {
                                        var radioValue = $(this).find("input[type=radio]:checked").attr("data-value");
                                        var data = {
                                            ID: $(this).attr("data-id"),
                                            Control: $(this).attr("data-control"),
                                            Access: radioValue
                                        };
                                        datas.push(data);
                                    });

                                    $("#role").text(JSON.stringify(datas))

                                    console.log(datas);
                                }

                            </script>










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
        </div>
    </form>
</div>
