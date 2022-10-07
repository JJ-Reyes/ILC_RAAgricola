<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Cabana.Campo.RAAgricola.Pre.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>


    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <!--<meta charset="utf-8">-->
    <title>LA CABAÑA | Login</title>

    <link href="css/bootstrap.min.css" rel="stylesheet"/>
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet"/>

    <link href="css/animate.css" rel="stylesheet"/>
    <link href="css/style.css" rel="stylesheet"/>

</head>

<body class="gray-bg">

    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>

                <!--<h2 class="logo-name">La Cabaña</h2>-->

            </div>
            <h3>Bienvenido a La Cabaña</h3>
            <p></p>
            <p>Ingresa tus datos para Iniciar sesion.</p>
            <form class="m-t" role="form" method="post" id="form-login">
                <div class="form-group">
                    <input name="login" id="login" type="text" class="form-control" placeholder="Usuario" required="" autocomplete="off"/>
                </div>
                <div class="form-group">
                    <input name="pass" id="pass" type="password" class="form-control" placeholder="Contraseña" required="" onkeypress="return get_KeyPress(this, event);"/>
                </div>
                <button type="button" class="btn btn-primary block full-width m-b" onclick="SessionUser(); " >Login</button>

                <!--<a href="#"><small>Forgot password?</small></a>-->
                <p class="text-muted text-center"><small>Contacto para soporte</small></p>
                <!--<a class="btn btn-sm btn-white btn-block" href="register.html">Create an account</a>-->
            </form>
            
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-2.1.1.js"></script>
    <script src="js/bootstrap.min.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                formLogin = $('#form-login');
                formLogin.submit(function (event) {

                    SessionUser();
                });

            });

            function get_KeyPress(textbox, evento) {
                var keyCode;
                
                if (evento.which || evento.charCode) {
                    keyCode = evento.which ? evento.which : evento.charCode;
                }
                /*
                else if (window.event) {
                    keyCode = event.keyCode;
                    if (keyCode == 13) {
                        if (event.keyCode)
                            event.keyCode = 9;
                    }
                }
                */
                if (keyCode == 13) {
                    SessionUser();
                    return false;
                }
                return true;
            }


            function SessionUser() {

                var login = $.trim($('#login').val()),
                    pass = $.trim($('#pass').val());
                $.ajax({
                    async: true,
                    url: "Session.aspx",
                    dataType: "html",
                    type: "POST",
                    data: {
                        USUARIO: login,
                        PASS: pass
                    },
                    success: function (data) {
                        $('body').append(data)
                        return false;
                    },
                    error: function () {
                    }
                });

                return false;
            }
    </script>

</body>
</html>
