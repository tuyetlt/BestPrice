<%@ Control Language="C#" AutoEventWireup="true" CodeFile="tool.ascx.cs" Inherits="admin_Controls_tool_tool" %>
<form method="post" enctype="multipart/form-data" id="frm_edit">
    <div class="container">
        <div class="edit">
            <div class="clear"></div>
            <div class="form-group submit">
                <div>&nbsp;</div>
                <div>
                    <input type="hidden" id="done" name="done" value="0" />
                    <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Xuất txt Cho Google Shopping</button>
                    <script type="text/javascript">
                        $(".btnSubmit").click(function () {
                            var dataValue = $(this).attr("data-value");
                            if ($(this).hasClass("validate") || ValidateForm()) {
                                $('#frm_edit #done').val(dataValue);
                                $(this).attr('disabled', 'disabled');
                                $(this).html('Loading...');
                                $("#frm_edit").submit();
                            }
                            else {
                                return false;
                            }
                        });
                    </script>
                </div>
            </div>
        </div>
    </div>
</form>
