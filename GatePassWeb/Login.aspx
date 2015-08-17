<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="GatePassWeb.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login To System</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link href="AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/additionalcss/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/additionalcss/customize-style.css" rel="stylesheet" type="text/css" />
</head>
<body class="login-page">
    <div class="login-box">
        <div class="login-logo kaushan-font">
            <div class="row">
                <div class="col-md-8 center-block" style="float: none!important;">
                    <img src="AdminLte/img/logo-pelindo3.png" class="img-responsive" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <h4 >PT. Pelabuhan Indonesia III (Persero)</h4>
                </div>
            </div>
            <a href="javascript:;" style="color: white; text-shadow: 2px 1px 3px #5C5C5C;"><b>Billing Gate System</b></a>
        </div>
        <div class="login-box-body" style="padding-top: 5px;">
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
                        <button type="submit" class="btn btn-primary btn-block btn-flat"><i class="fa fa-arrow-circle-o-right"></i>&nbsp;Login</button>
                    </div>
                    <div class="col-xs-4">
                        <a href="/register-new-user" class="btn btn-success btn-block btn-flat"><i class="fa fa-pencil-square-o"></i>&nbsp;Register</a>
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
