<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="UserSetting.aspx.cs" Inherits="GatePassWeb.UserSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/plugins/select2/select2.css");
        @import url("AdminLte/additionalcss/popover.css");
        @import url("AdminLte/plugins/data-tables/DT_bootstrap.css");

        .notView {
            display: none;
        }

        .alignRight {
            text-align: right;
        }

        .alignLeft {
            text-align: left;
        }

        .alignCenter {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="UserSettingApp"
        ng-controller="UserSettingController">
        <section class="content-header">
        </section>
        <section class="content">
            <div class="row" ng-init="populateData()">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Data user</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body" style="display: block;">
                            <div class="table-responsive table-scrollable">
                                <table class="table table-bordered table-hover" id="tbl-user">
                                    <thead>
                                        <tr>
                                            <th style="display: none;">User Id</th>
                                            <th>Username</th>
                                            <th style="display: none;">Kode Cabang</th>
                                            <th>Cabang</th>
                                            <th>Nama</th>
                                            <th>Last login</th>
                                            <th style="display: none;">Role Id</th>
                                            <th>Role</th>
                                            <th></th>
                                            <th style="display: none;">ISAKTIF</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="box-footer" style="display: block;">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("~/Service/Setting/UserSettingSvc.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <script src="AdminLte/plugins/data-tables/jquery.dataTables.js"></script>
    <script src="AdminLte/plugins/data-tables/DT_bootstrap.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/select2/select2.js"></script>
    <script type="text/javascript" src="Script/Setting/UserSetting.js"></script>
</asp:Content>
