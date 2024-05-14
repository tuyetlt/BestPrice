<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="admin_login_Controls_Login" %>
<div class="login">
    <h1>Đăng nhập hệ thống</h1>
    <form method="post" enctype="multipart/form-data" id="frm_login">

        <p>
            <input type="text" name="email" placeholder="Email" value="">
        </p>
        <p>
            <input type="password" name="password" placeholder="Mật khẩu" value="">
        </p>
        <p class="remember_me">
            <label>
                <input type="checkbox" name="remember" id="remember"><label for="remember"> Lưu mật khẩu</label>
            </label>
        </p>
        <p class="submit">
            <button type="submit" class="btnSubmit">Đăng nhập</button>
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
    <p>Nếu bạn quên mật khẩu? <a href="/admin/login/?control=forgotpassword">Bấm vào đây để lấy lại</a>.</p>
</div>