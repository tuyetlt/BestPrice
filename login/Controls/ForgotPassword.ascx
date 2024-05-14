<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.ascx.cs" Inherits="login_Controls_ForgotPassword" %>

<% if (!ResetOK)
    {  %>
<div class="login">
    <h1>Quên mật khẩu</h1>
    <form method="post" enctype="multipart/form-data" id="frm_login">
        <p>Nhập Email của bạn, hệ thống sẽ gửi hướng dẫn lấy lại mật khẩu</p>
        <p>
            <input type="text" name="email" placeholder="Email">
        </p>
        <input type="hidden" id="fairly-unique-id" value="submitform.php" />

        <div id="mc" style="margin-top: 20px">
            <p>Vui lòng vẽ hình dưới đây để xác minh</p>
            <canvas id="mc-canvas"></canvas>
        </div>


        <p class="submit">
            <button type="submit" disabled="disabled" class="btnSubmit">Thực hiện</button>
            <input type="hidden" id="done" name="done" value="0" />

            <script type="text/javascript">
                $(".btnSubmit").click(function () {
                    $('#frm_login #done').val("1");
                    $(this).attr('disabled', 'disabled');
                    $(this).html('Loading...');
                    $("#frm_login").submit();
                });
            </script>

            <script type="text/javascript">

                $('#frm_login').motionCaptcha({
                    cssClass: '.mc-active',
                    canvasId: '#mc-canvas',
                    errorMsg: 'Vui lòng thử lại.',
                    successMsg: 'Xác minh thành công!',
                    noCanvasMsg: "Your browser doesn't support <canvas> - try Chrome, FF4, Safari or IE9.",
                    label: '<p>Vui lòng vẽ hình để xác minh:</p>'
                });

            </script>
        </p>
    </form>
</div>

<%}
    else
    { %>
<div class="login">
    <h1>Thao tác thành công</h1>
    <p>Vui lòng kiểm tra Email để lấy lại mật khẩu</p>
</div>
<% } %>

<div class="login-help">
    <p>Nếu đã có tài khoản, vui lòng <a href="/login/">Đăng nhập</a>.</p>
</div>