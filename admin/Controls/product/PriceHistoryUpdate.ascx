<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PriceHistoryUpdate.ascx.cs" Inherits="admin_Controls_product_PriceHistoryUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">

        <div class="container">
            <div class="edit">
                <div class="full">
                    <!-- Tên -->



                    <div class="priceHistory">

                        <%
                            string id = RequestHelper.GetString("id", "");

                            DataTable dtPriceHistory = SqlHelper.SQLToDataTable("tblPriceHistory", "", "ID=" + id);
                            if (Utils.CheckExist_DataTable(dtPriceHistory))
                            {
                                DataTable dt = SqlHelper.SQLToDataTable(C.PRODUCT_TABLE, "ID,Name", "ID=" + dtPriceHistory.Rows[0]["ProductID"].ToString());
                                if (Utils.CheckExist_DataTable(dt))
                                {
                                    dtPriceHistory = SqlHelper.SQLToDataTable("tblPriceHistory", "", "ProductID=" + dt.Rows[0]["ID"].ToString());
                        %>
                        <div class="form-group">
                            <div>Sản phẩm </div>
                            <div>
                                <%= dt.Rows[0]["Name"].ToString() %>
                                <input type="hidden" id="pid" value="<%= dt.Rows[0]["ID"].ToString() %>" />
                            </div>
                        </div>
                        <table>
                            <thead>
                                <tr>
                                    <th colspan="3"><i class="fad fa-history"></i>&nbsp;Lịch sử giá
                                    </th>
                                </tr>
                                <tr>
                                    <th>Giá</th>
                                    <th>Admin</th>
                                    <th>Ngày</th>
                                </tr>
                            </thead>


                            <%
                                foreach (DataRow drPriceHistory in dtPriceHistory.Rows)
                                {
                                    string User = "";
                                    DataTable dtUser = SqlHelper.SQLToDataTable("tblAdminUser", "", "ID=" + drPriceHistory["CreatedBy"].ToString());
                                    if (Utils.CheckExist_DataTable(dtPriceHistory))
                                        User = dtUser.Rows[0]["Name"].ToString();

                            %>
                            <tr>
                                <td>
                                    <%= string.Format("{0:N0} {1}", ConvertUtility.ToDecimal(drPriceHistory["Price"].ToString()), "VNĐ") %>
                                </td>
                                <td>
                                    <%= User %>
                                </td>
                                <td>
                                    <%= drPriceHistory["CreatedDate"].ToString() %></td>
                            </tr>

                            <%
                                }
                            %>
                        </table>
                        <%
                                }
                            }
                        %>
                    </div>

                    <div class="clear"></div>
                    <div id="chart_price"></div>
                    <script type="text/javascript">
                        var pid = $("#pid").val();
                        $.ajax({
                            url: "/admin/chart/Default.aspx?control=pricehistory&pid=" + pid, success: function (result) {
                                $("#chart_price").html(result);
                            }
                        });
                    </script>


                </div>

                <!--- Submit Button -->
                <div class="clear"></div>
                <div class="form-group submit">
                    <div>&nbsp;</div>
                    <div>



                        <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>


                        <input type="hidden" id="done" name="done" value="0" />

                        <script type="text/javascript">
                            $(".btnSubmit").click(function () {
                                var dataValue = $(this).attr("data-value");
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
