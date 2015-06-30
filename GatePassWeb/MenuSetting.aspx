<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MenuSetting.aspx.cs" Inherits="GatePassWeb.MenuSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/plugins/select2/select2.css");
        @import url("AdminLte/additionalcss/popover.css");
        @import url("AdminLte/additionalcss/jquery.webui-popover.css");
        @import url("AdminLte/plugins/jquery-nestable/jquery.nestable.css");
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="MenuSettingApp"
        ng-controller="MenuSettingController">
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
            <div class="row" ng-init="initializemenu()">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Edit Menu</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body" style="display: block;">
                            <div class="row">
                                <div class="col-md-12" compile="treeviewdata">
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" style="display: block;">
                            <button type="button" class="btn btn-success"
                                    ng-click="saveConfig()">
                                Simpan Konfigurasi Menu <i class="fa fa-check"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("~/Service/Setting/MenuSettingSvc.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <input type="hidden" id="modalMenu" name="modalMenu" value="<%=this.ResolveUrl("~/Html/TreeView.html")%>" />
    <input type="hidden" id="editmodalMenu" name="editmodalMenu" value="<%=this.ResolveUrl("~/Html/EditMenuSetting.html")%>" />
    <script type="text/javascript" src="AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/select2/select2.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery-nestable/jquery.nestable.js"></script>
    <script type="text/javascript" src="AdminLte/plugins/jquery.webui-popover.js"></script>
    <script type="text/javascript" src="Script/Setting/MenuSetting.js"></script>
</asp:Content>
