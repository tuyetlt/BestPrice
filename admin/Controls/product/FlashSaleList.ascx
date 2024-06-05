<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlashSaleList.ascx.cs" Inherits="admin_Controls_product_FlashSaleList" %>
<%@ Import Namespace="System.Globalization" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <div class="container">
            <div class="edit" style="margin-bottom: 0">
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

                            <input type="text" class="datepicker" autocomplete="off" id="FlashSaleTimeDisplay" name="FlashSaleTimeDisplay" value="<%= outputDateTime %>" style="width: 360px" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div>Màu nền </div>
                        <div>
                            <input type="text" id="FlashSaleBackgroundColor" name="FlashSaleBackgroundColor" value="<%= dr["FlashSaleBackgroundColor"].ToString()%>" style="width: 360px" />
                        </div>
                    </div>

                    <div class="form-group image-ck">
                        <div>Ảnh tiêu đề </div>
                        <div data-thumb="thumbnail_FlashSaleHeader" data-inputtext="FlashSaleHeader" data-folder="flash-sale">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= FlashSaleHeader %>" id="thumbnail_FlashSaleHeader" alt="Chọn Ảnh" />
                            </a>
                            <input type="text" id="FlashSaleHeader" name="FlashSaleHeader" class="get_ck" value="<%= dr["FlashSaleHeader"].ToString()%>" style="width: 300px !important" />
                        </div>
                    </div>

                    <div class="form-group image-ck">
                        <div>Khung sản phẩm </div>
                        <div data-thumb="thumbnail_FlashSaleFrame1" data-inputtext="FlashSaleFrame1" data-folder="flash-sale">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= FlashSaleFrame1 %>" id="thumbnail_FlashSaleFrame1" alt="Chọn Ảnh" />
                            </a>
                            <input type="text" id="FlashSaleFrame1" name="FlashSaleFrame1" class="get_ck" value="<%= dr["FlashSaleFrame1"].ToString()%>" style="width: 300px !important" />
                        </div>
                    </div>
                    <div class="form-group image-ck">
                        <div>Icon phải </div>
                        <div data-thumb="thumbnail_FlashSaleFrame2" data-inputtext="FlashSaleFrame2" data-folder="flash-sale">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= FlashSaleFrame2 %>" id="thumbnail_FlashSaleFrame2" alt="Chọn Ảnh" />
                            </a>
                            <input type="text" id="FlashSaleFrame2" name="FlashSaleFrame2" class="get_ck" value="<%= dr["FlashSaleFrame2"].ToString()%>" style="width: 300px !important" />
                        </div>
                    </div>
                    <div class="form-group image-ck">
                        <div>Icon trái </div>
                        <div data-thumb="thumbnail_FlashSaleFrame3" data-inputtext="FlashSaleFrame3" data-folder="flash-sale">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= FlashSaleFrame3 %>" id="thumbnail_FlashSaleFrame3" alt="Chọn Ảnh" />
                            </a>
                            <input type="text" id="FlashSaleFrame3" name="FlashSaleFrame3" class="get_ck" value="<%= dr["FlashSaleFrame3"].ToString()%>" style="width: 300px !important" />
                        </div>
                    </div>

                    <%--  <div class="form-group">
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
                    </div>--%>
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
<div class="obj-list">
    <div class="filter">
        <a class="button" id="refresh" href="javascript:;"><i class="fas fa-sync-alt"></i>Refresh</a>
        <a class="button" id="remove_flashsale" href="javascript:;"><i class="fas fa-trash-alt"></i>Bỏ sản phẩm khỏi flash Sale (giá Flash Sale = 0)</a>
        <a class="button" href="/admin/product/productupdate"><i class="fas fa-plus"></i>Thêm sản phẩm</a>

        <input type="hidden" id="AttrProductFlag" value="<%= (int)AttrProductFlag.FlashSale %>" />
        <input type="hidden" id="table" value="<%= ControlAdminInfo.SQLNameTable %>" />
        <input type="hidden" id="fromDate" />
        <input type="hidden" id="toDate" />
        <input type="hidden" id="pageIndex" />
        <input type="hidden" id="control" value="<%= Utils.GetControlAdmin() %>" />
        <input type="hidden" id="folder" value="<%= Utils.GetFolderControlAdmin() %>" />
        <input type="hidden" id="controlName" value="<%= ControlAdminInfo.ShortName %>" />
        <input type="hidden" id="loadpaging" value="true" />
        <input type="hidden" id="pageSize" value="<%= C.PAGING_ADMIN %>" />
        <input type="hidden" id="sort" />
        <input type="hidden" id="fieldSort" />
        <input type="hidden" id="filterJson" />
        <input type="text" id="keyword" autocomplete="off" placeholder="Từ khoá tìm kiếm" />
    </div>
    <div class="content-list">
        <div class="tableData">
        </div>
    </div>
    <div class="clear"></div>
    <div id="paging"></div>
</div>


<script>
    $(document).ready(function () {
        $('#remove_flashsale').click(function () {
            var selectedIds = [];
            $('input[type="checkbox"]:checked[id^="select_"]').each(function () {
                selectedIds.push($(this).data('id'));
            });

            if (selectedIds.length === 0) {
                alert('Không có sản phẩm nào được chọn.');
                return;
            }

            var pidList = selectedIds.join(',');

            var confirmMessage = 'Bạn có chắc chắn muốn update cho ' + selectedIds.length + ' sản phẩm này không?';
            if (!confirm(confirmMessage)) {
                return;
            }

            $.ajax({
                url: '/admin/ajax/ajax.aspx',
                type: 'GET',
                data: {
                    ctrl: 'dynamic',
                    Action: 'remove-product-flashsale',
                    table: 'tblProducts',
                    pid_list: pidList
                },
                success: function (response) {
                    GetNotify('Sản phẩm với ID ' + pidList + ' đã được xóa khỏi flashsale.');
                    setTimeout(function () { getval(0); }, 100);
                },
                error: function (xhr, status, error) {
                    alert('Đã xảy ra lỗi: ' + error);
                }
            });
        });
    });
    </script>
