<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="GatePassWeb.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login To System</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link href="AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/additionalcss/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />
</head>
<body class="login-page">
    <div class="login-box">
        <div class="login-logo">
            <a href="javascript:;"><b>Billing Gate System</b></a>
        </div>
        <div class="login-box-body">
            <p class="login-box-msg">
                <asp:Literal ID="lblMessage" runat="server"></asp:Literal>
            </p>
            <form id="loginform" runat="server" method="post">
                <div class="form-group has-feedback">
                    <input type="text" class="form-control" id="txtusername" name="txtusername"
                        placeholder="Username" />
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" class="form-control" id="txtpassword" name="txtpassword"
                        placeholder="Password" />
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <a href="/register-new-user" class="btn btn-success btn-block btn-flat">Register</a>
                    </div>
                    <div class="col-xs-4">
                        <button type="submit" class="btn btn-primary btn-block btn-flat">Login</button>
                    </div>
                </div>
            </form>
        </div>

    </div>

    <script src="AdminLte/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="AdminLte/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="AdminLte/plugins/iCheck/icheck.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="AdminLte/plugins/jQuery-validation/dist/jquery.validate.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jQuery-validation/dist/additional-methods.js"></script>
    <script type="text/javascript" src="Script/Login.js"></script>
    <script>
        jQuery(document).ready(function () {
            Login.init();
        });
    </script>
</body>
</html>
