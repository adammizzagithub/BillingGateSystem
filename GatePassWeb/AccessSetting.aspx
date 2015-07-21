<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AccessSetting.aspx.cs" Inherits="GatePassWeb.AccessSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/plugins/select2/select2.css");
        @import url("AdminLte/additionalcss/popover.css");
        @import url("AdminLte/additionalcss/jquery.webui-popover.css");
        @import url("AdminLte/additionalcss/angular-ui-tree.min.css");

        .btn {
            margin-right: 8px;
        }

        .angular-ui-tree-handle {
            background: #f8faff;
            border: 1px solid #dae2ea;
            color: #7c9eb2;
            padding: 10px 10px;
        }

            .angular-ui-tree-handle:hover {
                color: #438eb9;
                background: #f4f6f7;
                border-color: #dce2e8;
            }

        .angular-ui-tree-placeholder {
            background: #f0f9ff;
            border: 2px dashed #bed2db;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="AccessSettingApp"
        ng-controller="AccessSettingController">
        <section class="content-header">
            <div class="row">
                <div class="col-md-2">
                    <button id="tmbhnew" class="btn bg-olive">
                        <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Tambah
                    </button>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="row" ng-init="initialize()">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Manajemen Hak Akses</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body" style="display: block;">
                            <div class="row">
                                <div class="col-xs-12" ng-show="viewcaption === 0">
                                    <div class="table-responsive table-scrollable">
                                        <table class="table table-hover table-striped">
                                            <tbody>
                                                <tr>
                                                    <th style="display: none;">RoleId</th>
                                                    <th class="text-center">Role</th>
                                                    <th class="text-center">Status</th>
                                                    <th class="text-center"></th>
                                                </tr>
                                                <tr ng-repeat="obj in tabledata">
                                                    <td style="display: none;">{{obj.Bgsm_Akses_Id}}</td>
                                                    <td class="text-center">{{obj.Bgsm_Akses_Role}}</td>
                                                    <td class="text-center">
                                                        <span ng-if="obj.Bgsm_Akses_Status === 'Y'" class="label label-primary">Aktif</span>
                                                        <span ng-if="obj.Bgsm_Akses_Status === 'N'" class="label label-danger">Non Aktif</span>
                                                    </td>
                                                    <td class="text-right">
                                                        <button class="btn btn-danger btn-xs" ng-click="deleterole(obj.Bgsm_Akses_Id)">Hapus <i class="fa fa-times"></i></button>
                                                        <button class="btn btn-warning btn-xs" ng-click="initialmenu(obj.Bgsm_Akses_Id)">Menu <i class="fa fa-tags"></i></button>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div id="menushow" class="col-xs-12" ng-show="viewcaption === 1">
                                    <div ui-tree data-drag-enabled="false">
                                        <ol ui-tree-nodes="" ng-model="controlroledata" >
                                            <li ng-repeat="node in controlroledata" ui-tree-node 
                                                ng-include="linkrender"></li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" style="display: block;">
                            <button type="button" class="btn btn-primary"
                                ng-click="saveSettingRole()" ng-show="viewcaption === 1">
                                Simpan <i class="fa fa-check"></i>
                            </button>
                            <button type="button" class="btn btn-danger"
                                ng-click="batalSettingRole()" ng-show="viewcaption === 1">
                                Batal <i class="fa fa-reply"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("~/Service/Setting/AccessSettingSvc.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <input type="hidden" id="menutree" name="menutree" value="<%= this.ResolveUrl("~/Html/TreeView.html")%>" />
    <script type="text/javascript" src="AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/select2/select2.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery.webui-popover.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/angular-library/angular-ui-tree.js"></script>
    <script type="text/javascript" src="Script/Setting/AccessSetting.js"></script>
</asp:Content>
