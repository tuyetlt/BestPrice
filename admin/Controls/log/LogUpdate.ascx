<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LogUpdate.ascx.cs" Inherits="admin_Controls_log_LogUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="full">

                    <!-- Action -->
                    <div class="form-group">
                        <div>Hành động </div>
                        <div>
                            <input readonly type="text" id="action" name="action" value="<%= dr["Action"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Name -->
                    <div class="form-group">
                        <div>Hành động cho </div>
                        <div>
                            <input readonly type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Json_Content -->
                    <div class="form-group">
                        <div>Nội dung </div>
                        <div>

                            <table class="custom">
                                <% List<JsonObjectByField> jsonObjectByField = SqlHelper.GetJsonObjectByField(dr["Json_Content"].ToString());
                                    if (jsonObjectByField != null && jsonObjectByField.Count > 0)
                                    {
                                        foreach (JsonObjectByField entry in jsonObjectByField)
                                        {
                                            Response.Write("<tr>");

                                            if(SqlHelper.GetForeignTable_Check(entry.Field))
                                                Response.Write(string.Format("<td>{0}</td><td>{1}</td>", entry.Field, SqlHelper.GetForeignTable(entry.Field, entry.Value)));
                                            else
                                                Response.Write(string.Format("<td>{0}</td><td>{1}</td>", entry.Field, entry.Value));

                                            Response.Write("</tr>");
                                        }
                                    }
                                %>
                            </table>


                        </div>
                    </div>
                    <!-- Control -->
                    <div class="form-group">
                        <div>Control </div>
                        <div>
                            <input readonly type="text" id="control" name="control" value="<%= dr["Control"].ToString()%>" />
                        </div>
                    </div>
                    <!-- TableSql -->
                    <div class="form-group">
                        <div>Bảng </div>
                        <div>
                            <input readonly type="text" id="tablesql" name="tablesql" value="<%= dr["TableSql"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Link -->
                    <div class="form-group">
                        <div>Link </div>
                        <div>
                            <input readonly type="text" id="link" name="link" value="<%= dr["Link"].ToString()%>" />
                        </div>
                    </div>
                    <!-- CreatedDate -->
                    <div class="form-group">
                        <div>Ngày thao tác </div>
                        <div>
                            <input readonly type="text" id="createddate" name="createddate" value="<%= dr["CreatedDate"].ToString()%>" />
                        </div>
                    </div>
                    <!-- CreatedBy -->
                    <div class="form-group">
                        <div>Thực hiện bởi </div>
                        <div>
                            <%   DataTable dtUser = dtUser = SqlHelper.SQLToDataTable("tblAdminUser", "Name", "ID=" + ConvertUtility.ToInt32(dr["CreatedBy"]));
                                if (dtUser != null && dtUser.Rows.Count > 0)
                                {%>
                            <input readonly type="text" id="createdby" name="createdby" value="<%= dtUser.Rows[0]["Name"].ToString()%>" />
                        </div>
                        <%
                            }
                        %>
                    </div>
                </div>


                <!--- Submit Button -->
                <div class="clear"></div>
                <div class="form-group submit">
                    <div>&nbsp;</div>
                    <div>

                        <% if (IsUpdate)
                            { %>
                        <button type="submit" data-value="restore" class="btnSubmit btnRestore"><i class="fad fa-undo"></i>Khôi phục</button>

                        <%}%>

                        <button type="submit" data-value="cancel" class="btnSubmit validate btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>
                        <input type="hidden" id="done" name="done" value="0" />


                        <script type="text/javascript">
                            $(".btnSubmit").click(function () {
                                var dataValue = $(this).attr("data-value");
                                if (dataValue == "restore") {
                                    if (!confirm('Bạn có chắc chắn muốn khôi phục?')) {
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
    </form>
</div>
