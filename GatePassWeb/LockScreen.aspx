<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockScreen.aspx.cs" Inherits="GatePassWeb.LockScreen" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Billing Gate Lock</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link href="AdminLte/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/additionalcss/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" />
    <link href="AdminLte/dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" />

</head>
<body class="lockscreen">
    <div class="lockscreen-wrapper">
        <div class="lockscreen-logo">
            <a href="javascript:;"><b>Billing Gate App.</b> <i class="fa fa-lock"></i></a>
        </div>
        <div class="lockscreen-name"><%= this.Session["Nama"]%></div>
        <div class="lockscreen-item">
            <div class="lockscreen-image">
                <img src="AdminLte/dist/img/user-lock.png" alt="user image" />
            </div>
            <form id="lockform" runat="server" method="post" class="lockscreen-credentials">
                <div class="input-group">
                    <input type="password" id="txtlockpswd" name="txtlockpswd"
                        class="form-control" placeholder="password" />
                    <div class="input-group-btn">
                        <button type="submit" class="btn"><i class="fa fa-arrow-right text-muted"></i></button>
                    </div>
                </div>
            </form>
        </div>
        <div class="help-block text-center">
            Ketik password untuk masuk ke dalam aplikasi.. !
        </div>
        <div class='lockscreen-footer text-center'>
            Copyright &copy; 2015
            <br>
            Adam Mizza Zamani, S.Kom
        </div>
    </div>
    <script src="AdminLte/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <script src="AdminLte/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
</body>
</html>
