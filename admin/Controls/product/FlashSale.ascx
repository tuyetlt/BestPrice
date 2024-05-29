<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FlashSale.ascx.cs" Inherits="admin_Controls_product_FlashSale" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <div class="container">
            <div class="edit">
                <div class="full">
                    <div class="form-group">
                        <div>Thời gian kết thúc FlashSale </div>
                        <div>
                            <input type="text" class="datepicker" autocomplete="off" id="startdate" name="startdate" value="" style="width: 400px" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div>&nbsp;</div>
                        <div>
                            <input type="checkbox" name="hide" id="hide" />
                            <label for="hide">Tự động gia hạn hàng ngày</label><br>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="form-group submit" style="position:unset; border-top:none; margin:0">
                        <div style="display:block">&nbsp;</div>
                        <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Lưu</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</div>
