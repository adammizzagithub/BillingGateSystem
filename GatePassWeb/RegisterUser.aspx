<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterUser.aspx.cs" Inherits="GatePassWeb.RegisterUser" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Registrasi User</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link href="AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/additionalcss/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/additionalcss/customize-style.css" rel="stylesheet" type="text/css" />
</head>
<body class="register-page"
    ng-app="RegisterUserApp" ng-controller="RegisterUserController">
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("~/Service/Setting/RegisterUserSvc.asmx")%>" />
    <div class="register-box">
        <div class="register-logo kaushan-font">
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
            <a href="javascript:;" style="color: white; text-shadow: 2px 1px 3px #5C5C5C; font-size: 0.7em;">Billing Gate System - Register User</a>
        </div>
        <div class="register-box-body">
            <div class="row">
                <div class="alert alert-success alert-dismissable" style="display: none;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <h4><i class="icon fa fa-check"></i>Berhasil !</h4>
                    Registrasi user berhasil
                </div>
                <div class="alert alert-danger alert-dismissable" style="display: none;">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                    <h4><i class="icon fa fa-ban"></i>Gagal !</h4>
                    Registrasi user gagal
                </div>
            </div>
            <form id="registerform" runat="server" method="post">
                <div class="form-group has-feedback">
                    <input type="text" id="txtnama" class="form-control" placeholder="Nama" />
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="text" id="txtusername" class="form-control" placeholder="Username" />
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" id="txtpassword" class="form-control" placeholder="Password" />
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8" style="padding-top: 8px;">
                        <a href="/auth-login" class="text-center"><i class="fa fa-arrow-circle-o-left"></i>&nbsp;Kembali ke halaman Login</a>
                    </div>
                    <div class="col-xs-4">
                        <button type="submit" class="btn btn-primary btn-block btn-flat">
                            Register</button>
                    </div>
                </div>
            </form>
            
        </div>
    </div>

    <script src="AdminLte/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="AdminLte/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="AdminLte/plugins/angular-library/angular.js" type="text/javascript"></script>
    <script type="text/javascript" src="AdminLte/plugins/jQuery-validation/dist/jquery.validate.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jQuery-validation/dist/additional-methods.js"></script>
    <script src="Script/Setting/RegisterUser.js" type="text/javascript"></script>
    <script type="text/javascript" src="AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery.blockUI.js"></script>

</body>
</html>
