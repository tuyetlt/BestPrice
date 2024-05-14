<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderUpdate.ascx.cs" Inherits="admin_Controls_order_OrderUpdate" %>
<%@ Import Namespace="System.Data" %>

<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString()%>" />
        <input type="hidden" name="token" value="<%= dr["Token"].ToString()%>" />
        <div class="container">
            <div class="edit">
                <div class="left">
                    <div class="ngaydathang">
                        <i class="fad fa-calendar-plus"></i>&nbsp;Ngày đặt hàng <b><%= ConvertUtility.ToDateTime(dr["CreatedDate"]).ToString("dd/MM/yyyy - HH:mm:ss") %></b> - Website: <%= dr["Website"].ToString()%>
                        <input type="hidden" id="website" name="website" value="<%= dr["Website"].ToString()%>" />
                    </div>
                    <!-- CreatedDate -->
                    <div class="form-group">
                        <div></div>
                        <div>
                            <table border="0" cellpadding="5" cellspacing="5" rules="all" style="width: 100%; border: 1px solid #dbdbdb">
                                <tbody>
                                    <tr align="center" style="background-color: #ededed; font-weight: bold;">
                                        <td>Hình ảnh
                                        </td>
                                        <td>Tên sản phẩm
                                        </td>
                                        <td>Số lượng
                                        </td>
                                        <td>Đơn giá
                                        </td>
                                        <td>Thành tiền
                                        </td>
                                    </tr>
                                    <%= ProductList %>
                                </tbody>
                            </table>
                            <div class="tongtiendonhang">
                                Tổng tiền <b><%= string.Format("{0:N0} VNĐ", dr["PriceFinal"]) %></b>
                                <input type="hidden" id="pricefinal" name="pricefinal" value="<%= dr["PriceFinal"] %>" />
                            </div>
                        </div>
                    </div>

                    <!-- NoteAdmin -->
                    <div class="form-group">
                        <div>Ghi chú của Admin </div>
                        <div>
                            <textarea name="NoteAdmin"><%= dr["NoteAdmin"].ToString()%></textarea>
                        </div>
                    </div>

                    <style>
                        .utm{
                            border-collapse: collapse; border: 1px solid #ddd;
                            background:#fff;
                        }
                        .utm td{
                            border: 1px solid #ddd;
                            padding:10px;
                        }
                    </style>

                    <div style="margin-top:20px; background-color:#f8f8f8; padding:10px">
                        <table class="utm">
                            <tr>
                                <td>utm_source</td>
                                <td><%= dr["utm_source"].ToString()%></td>
                            </tr>
                            <tr>
                                <td>utm_medium</td>
                                <td><%= dr["utm_medium"].ToString()%></td>
                            </tr>
                            <tr>
                                <td>utm_campaign</td>
                                <td><%= dr["utm_campaign"].ToString()%></td>
                            </tr>
                            <tr>
                                <td>utm_adgroup</td>
                                <td><%= dr["utm_adgroup"].ToString()%></td>
                            </tr>
                            <tr>
                                <td>utm_term</td>
                                <td><%= dr["utm_term"].ToString()%></td>
                            </tr>
                             <tr>
                                <td>utm_content</td>
                                <td><%= dr["utm_content"].ToString()%></td>
                            </tr>
                             <tr>
                                <td>ip_address</td>
                                <td><%= dr["IPAddress"].ToString()%></td>
                            </tr>
                        </table>
                    </div>



                    <%--    <div>
                            <%= dr["MailTemplate"].ToString()%>
                        </div>--%>
                </div>
                <div class="right">

                    <!-- Name -->
                    <div class="form-group">
                        <div>Name </div>
                        <div>
                            <input type="text" id="name" name="name" readonly="readonly" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>

                    <!-- Status -->
                    <div class="form-group">
                        <div>Status </div>
                        <div>
                            <input type="hidden" name="status" id="status" value="<%= dr["Status"].ToString()%>" />
                            <select id="drStatus">
                                <option value="1">Mới</option>
                                <option value="2">Đã chốt</option>
                                <option value="3">Chờ khách xem thêm</option>
                                <option value="4">Hết hàng</option>
                                <option value="10">đã Xử lý</option>
                                <option value="20">Huỷ đơn</option>
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
                    <!-- Address -->
                    <div class="form-group">
                        <div>Address </div>
                        <div>
                            <textarea id="address" name="address" readonly="readonly"><%= dr["Address"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- Phone -->
                    <div class="form-group">
                        <div>Phone </div>
                        <div>
                            <input type="text" id="phone" name="phone" readonly="readonly" value="<%= dr["Phone"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Email -->
                    <div class="form-group">
                        <div>Email </div>
                        <div>
                            <input type="text" id="email" name="email" readonly="readonly" value="<%= dr["Email"].ToString()%>" />
                        </div>
                    </div>



                    <!-- PaymentMethod -->
                    <div class="form-group">
                        <div>Phương thức thanh toán </div>
                        <div>
                            <input type="text" id="paymentmethod" readonly="readonly" name="paymentmethod" value="<%= dr["PaymentMethod"].ToString()%>" />
                        </div>
                    </div>

                    <!-- NoteMember -->
                    <% if (!string.IsNullOrEmpty(dr["NoteMember"].ToString()))
                        { %>
                    <div class="form-group">
                        <div>Ghi chú khách hàng </div>
                        <div>
                            <%= dr["NoteMember"].ToString()%>
                        </div>
                    </div>
                    <% } %>


                    


                    <!--- Submit Button -->

                    <div class="clear"></div>
                    <div class="form-group">
                        <div>&nbsp;</div>
                        <div>
                            <% if (IsUpdate)
                                { %>
                            <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Lưu</button>
                            <%--  <button type="submit" data-value="delete" class="btnSubmit btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>--%>
                            <%}
                                else
                                { %>
                            <% } %>
                            <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-share"></i>Quay lại</button>
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
