<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlashSale.ascx.cs" Inherits="admin_Controls_product_FlashSale" %>
<%@ Import Namespace="System.Globalization" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <div class="container">
            <div class="edit">
                <div class="full">
                    <div class="form-group">
                        <div>Thời gian kết thúc FlashSale </div>
                        <div>
                            <% 
                                string inputDateTime = dr["FlashSaleTimeDisplay"].ToString();
                                DateTime dateTime;
                                string outputDateTime = inputDateTime;
                                if (DateTime.TryParseExact(inputDateTime, "dd/MM/yyyy h:mm:ss tt", CultureInfo.GetCultureInfo("vi-VN"), DateTimeStyles.None, out dateTime))
                                {
                                    outputDateTime = dateTime.ToString("dd/MM/yyyy HH:mm:ss");
                                }
                            %>

                            <input type="text" class="datepicker" autocomplete="off" id="FlashSaleTimeDisplay" name="FlashSaleTimeDisplay" value="<%= outputDateTime %>" style="width: 400px" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div>&nbsp;</div>
                        <div>
                            <%
                                string autorenewal = string.Empty;
                                if (!string.IsNullOrEmpty(dr["AutoRenewal"].ToString()) && ConvertUtility.ToBoolean(dr["AutoRenewal"]))
                                    autorenewal = " checked";
                            %>
                            <input type="checkbox" name="autorenewal" id="autorenewal" <%= autorenewal %> />
                            <label for="hide">Tự động gia hạn hàng ngày</label><br>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="form-group submit" style="position: unset; border-top: none; margin: 0">
                        <div style="display: block">&nbsp;</div>
                        <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Lưu</button>
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
