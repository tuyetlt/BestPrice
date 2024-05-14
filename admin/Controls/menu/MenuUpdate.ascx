<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuUpdate.ascx.cs" Inherits="admin_Controls_MenuUpdate" %>
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
                    <!-- ShortName -->
                    <div class="form-group">
                        <div>ShortName </div>
                        <div>
                            <input type="text" id="shortname" name="shortname" value="<%= dr["ShortName"].ToString()%>" />
                        </div>
                    </div>
                    <!-- ParentID -->
                    <div class="form-group">
                        <div>ParentID </div>
                        <div>
                            <input type="text" id="parentid" name="parentid" value="<%= dr["ParentID"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Control -->
                    <div class="form-group">
                        <div>Control </div>
                        <div>
                            <input type="text" id="control" name="control" value="<%= dr["Control"].ToString()%>" />
                        </div>
                    </div>
                    <!-- SQLNameTable -->
                    <div class="form-group">
                        <div>Tên Bảng </div>
                        <div>
                            <input type="text" id="sqlnametable" name="sqlnametable" value="<%= dr["SQLNameTable"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Modul Filter -->
                    <div class="form-group">
                        <div>Modul Filter </div>
                        <div>
                            <input type="text" id="modulfilter" name="modulfilter" value="<%= dr["ModulFilter"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Icon -->
                    <div class="form-group">
                        <div>Icon </div>
                        <div>
                            <input type="text" id="icon" name="icon" value="<%= dr["Icon"].ToString().Replace(@"""", @"'") %>" />
                        </div>
                    </div>
                    <!-- Sort -->
                    <div class="form-group">
                        <div>Sort </div>
                        <div>
                            <input type="text" id="sort" name="sort" value="<%= dr["Sort"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Hide -->
                    <div class="form-group">
                        <div>Hide </div>
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
                    <!-- FieldSql -->
                    <div class="form-group">
                        <div>FieldSql </div>
                        <div>
                            <textarea id="fieldsql" name="fieldsql" style="display: none"><%= dr["FieldSql"].ToString()%></textarea>

                            <%

                                using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
                                {
                                    string sqlQuery = string.Format("select * from INFORMATION_SCHEMA.COLUMNS where TABLE_Name='{0}' order by ORDINAL_POSITION", dr["SQLNameTable"].ToString());
                                    System.Data.DataTable dataTable = dbx.ExecuteSqlDataTable(sqlQuery);
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        Response.Write("<table class='custom'>");

                                        foreach (DataRow drSchema in dataTable.Rows)
                                        {
                                            Response.Write("<tr>");

                                            Response.Write(@"<td class=""field"">" + drSchema["COLUMN_NAME"] + "</td>");
                                            Response.Write(string.Format(@"<td><input type=""text"" id=""text_{0}"" name=""text_{0}"" /></td>", drSchema["COLUMN_NAME"]));
                                            Response.Write(string.Format(@"<td><input type=""text"" id=""width_{0}""  placeholder=""Độ rộng cột"" style=""width:100px"" /></td>", drSchema["COLUMN_NAME"]));
                                            Response.Write(string.Format(@"<td><input type=""checkbox"" id=""show_{0}"" name=""show_{0}""><label for=""show_{0}"">Hiển thị</label></td>", drSchema["COLUMN_NAME"]));
                                            Response.Write(string.Format(@"<td><input type=""text"" id=""sort_{0}""  placeholder=""Sắp xếp"" style=""width:100px"" /></td>", drSchema["COLUMN_NAME"]));

                                            Response.Write("</tr>");
                                        }

                                        Response.Write("</table>");

                                    }
                                }
                            %>

                            <script type="text/javascript">

                                BindDataToRadio();


                                function BindDataToRadio() {
                                    var datas = JSON.parse($("#fieldsql").text());
                                    $.each(datas, function (index, value) {
                                        var show_list = $("input[type=checkbox]#show_" + value.Field);
                                        var text = $("input[type=text]#text_" + value.Field);
                                        var sort = $("input[type=text]#sort_" + value.Field);
                                        var width = $("input[type=text]#width_" + value.Field);

                                        if (show_list.length && value.Show == true)
                                            show_list.prop('checked', true);

                                        if (text.length)
                                            text.val(value.Text);

                                        if (sort.length)
                                            sort.val(value.Sort);

                                        if (width.length)
                                            width.val(value.Width);
                                    });
                                }

                                function GetValueFromTable() {
                                    var datas = [];

                                    $('.custom tr').each(function () {
                                        var field = $(this).find("td.field").html();

                                        var show_list = $(this).find("input[type=checkbox]#show_" + field);
                                        var text = $(this).find("input[type=text]#text_" + field).val();
                                        var sort = $(this).find("input[type=text]#sort_" + field).val();
                                        var width = $(this).find("input[type=text]#width_" + field).val();

                                        var data = {
                                            Field: field,
                                            Text: text,
                                            Sort: sort,
                                            Width: width,
                                            Show: show_list.prop('checked'),
                                        };
                                        datas.push(data);
                                    });

                                    datas.sort(function (a, b) {
                                        return a.Sort.localeCompare(b.Sort);
                                    });


                                    $("#fieldsql").text(JSON.stringify(datas))

                                    console.log(datas);
                                }

                            </script>
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
                            <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-arrow-left"></i>Bỏ Qua</button>
                            <input type="hidden" id="done" name="done" value="0" />
                            <script type="text/javascript">
                                $(".btnSubmit").click(function () {
                                    var dataValue = $(this).attr("data-value");
                                    if (dataValue == "delete")
                                        DeleteByID('<%= dr["ID"].ToString() %>', '<%= table %>', '<%= ControlAdminInfo.ShortName %>');
                                    GetValueFromTable();
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
