<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="login_Controls_Login" %>
<form method="post" enctype="multipart/form-data" id="frm_login">
    <a href="<%= C.ROOT_URL %>" class="header__logo-link">
                    <img src="<%= C.ROOT_URL %><%= ConfigWeb.Logo %>" alt="<%= ConfigWeb.MetaTitle %>" class="img-responsive"></a>
    <span class="login100-form-title">Đăng nhập hệ thống
    </span>

    <div class="wrap-input100 validate-input" data-validate="Valid email is required: ex@abc.xyz">
        <input class="input100" type="text" name="email" placeholder="Email">

        <span class="focus-input100"></span>
        <span class="symbol-input100">
            <i class="fa fa-envelope" aria-hidden="true"></i>
        </span>
    </div>

    <div class="wrap-input100 validate-input" data-validate="Password is required">
        <input class="input100" type="password" name="password" placeholder="Mật khẩu">
        <span class="focus-input100"></span>
        <span class="symbol-input100">
            <i class="fa fa-lock" aria-hidden="true"></i>
        </span>
    </div>
   <%-- <div class="wrap-input100 validate-input" data-validate="Password is required">
        <input type="checkbox" name="remember" id="remember"><label for="remember"> Lưu mật khẩu</label>
    </div>--%>
    <div class="container-login100-form-btn">
        <button class="login100-form-btn" type="submit">
            Login
        </button>
    </div>

    <div class="text-center p-t-12">
        <span class="txt1">Quên mật khẩu? 
        </span>
        <a class="txt2" href="/login/?control=forgotpassword">Bấm vào đây để lấy lại</a>
    </div>

     <div class="text-center p-t-136">
        <a class="txt2" href="/">Về trang chủ
							<i class="fa fa-long-arrow-right m-l-5" aria-hidden="true"></i>
        </a>
    </div>
    <input type="hidden" id="done" name="done" value="0" />
    <script type="text/javascript">
        $(".login100-form-btn").click(function () {
            $('#frm_login #done').val("1");
            $(this).attr('disabled', 'disabled');
            $(this).html('Loading...');
            $("#frm_login").submit();
        });
    </script>
</form>
