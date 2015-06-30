<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="GatePassWeb.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodySidebar" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="col-md-offset-3 col-md-6">
        ADAM MIZZA ZAMANI
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">Login Account</h3>
                <div class="col-md-12">
                    <asp:Literal ID="lblMessage" runat="server"></asp:Literal>
                </div>
            </div>
            <form id="loginform" runat="server" class="form-horizontal" method="post">
                <div class="box-body">
                    <div class="form-group">
                        <label for="txtusername" class="col-sm-2 control-label">Username</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="txtusername" name="txtusername"
                                placeholder="Username">
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtpassword" class="col-sm-2 control-label">Password</label>
                        <div class="col-sm-10">
                            <input type="password" class="form-control" id="txtpassword" name="txtpassword"
                                placeholder="Password">
                        </div>
                    </div>
                </div>
                <div class="box-footer">
                    <button type="submit" class="btn btn-info pull-right">Login</button>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyFooter" runat="server">
    <script type="text/javascript" src="AdminLte/plugins/jQuery-validation/dist/jquery.validate.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jQuery-validation/dist/additional-methods.js"></script>
    <script type="text/javascript" src="Script/Login.js"></script>
    <script>
        jQuery(document).ready(function () {
            Login.init();
        });
    </script>
</asp:Content>
