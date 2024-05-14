<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetPassword.ascx.cs" Inherits="admin_login_Controls_SetPassword" %>
<div class="login">
    <h1>Đặt mật khẩu</h1>
    <form method="post" enctype="multipart/form-data" id="frm_login">
        <p>Vui lòng nhập mật khẩu mới của bạn</p>
        <p>
             <input type="password" name="password1" placeholder="Mật khẩu" value="">
        </p>
        <p>
            <input type="password" name="password2" placeholder="Nhập lại Mật khẩu" value="">
        </p>
        <p class="submit">
            <button type="submit" class="btnSubmit">Đặt mật khẩu</button>
            <input type="hidden" id="done" name="done" value="0" />

            <script type="text/javascript">
                $(".btnSubmit").click(function () {
                    $('#frm_login #done').val("1");
                    $(this).attr('disabled', 'disabled');
                    $(this).html('Loading...');
                    //alert($('#frm_login #done').val())
                    $("#frm_login").submit();
                });
            </script>
        </p>
    </form>
</div>

<div class="login-help">
    <p>Nếu đã có tài khoản, vui lòng <a href="/admin/login/">Đăng nhập</a>.</p>
</div>