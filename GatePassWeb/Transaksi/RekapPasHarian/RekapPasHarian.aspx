<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RekapPasHarian.aspx.cs" Inherits="GatePassWeb.Transaksi.RekapPasHarian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .table-fixed thead {
            width: 97%;
        }

        .table-fixed tbody {
            height: 230px;
            overflow-y: auto;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper"
        ng-app="RekapPasHarianApp"
        ng-controller="RekapPasHarianFormController">
        <section class="content-header">
            <div class="row">
                <div class="col-md-5">
                    <h3 class="box-title" style="font-family: 'Comic Sans MS';">Rekap Pas Harian</h3>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary" ng-init="initialize()">
                        <div class="box-header with-border">
                            <h3 class="box-title">Form rekap pas harian</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <label>Tanggal</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                    <input type="text" class="form-control" mask-date ng-model="inputModel.Date">
                                </div>
                                <label>Gate</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                    <select class="form-control"
                                        ng-model="inputModel.Gate"
                                        ng-options="opt.ID_REF_DATA as opt.KET_REF_DATA for opt in arrlovgate">
                                    </select>
                                </div>
                                <label>Periode pendapatan</label>
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                    <select class="form-control"
                                        ng-model="inputModel.Periode"
                                        ng-options="opt.Bultah as opt.Periode for opt in arrlovperiode">
                                    </select>
                                </div>
                                <br />
                                <button type="button" class="btn btn-primary" ng-click="queryDataNeto()">Proses</button>
                                <br />
                                <br />
                                <div class="table-scrollable table-responsive">
                                    <table class="table table-fixed table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th style="display: none;">Id Kend.</th>
                                                <th class="col-xs-3">Jenis Kendaraan</th>
                                                <th class="col-xs-2">
                                                    <label class="pull-right">Qty</label></th>
                                                <th class="col-xs-3">
                                                    <label class="pull-right">Tarif</label></th>
                                                <th class="col-xs-4">
                                                    <label class="pull-right">Jumlah</label></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr ng-repeat="obj in rekapqueryharian">
                                                <td style="display: none;">{{obj.Jns_Kend}}</td>
                                                <td class="col-xs-3">{{obj.Jns_Kendaraan}}</td>
                                                <td class="col-xs-2">
                                                    <label class="pull-right">{{obj.Qty | currency:""}}</label></td>
                                                <td class="col-xs-3">
                                                    <label class="pull-right">{{obj.Tarif | currency:""}}</label></td>
                                                <td class="col-xs-4">
                                                    <label class="pull-right">{{obj.Pndp_Gross | currency:""}}</label></td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="3">Total</td>
                                                <td class="col-xs-4">
                                                    <label class="pull-right">{{totalbruto | currency:""}}</label></td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">Pendapatan Net</td>
                                                <td class="col-xs-4">
                                                    <label class="pull-right">{{totalneto | currency:""}}</label></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                            <div class="box-footer">
                                <button type="button" class="btn btn-primary" ng-click="simpanrekap()">SIMPAN</button>
                                <button type="button" class="btn btn-danger">BATAL</button>
                                <button type="button" class="btn btn-success">PRANOTA</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("../../Service/Transaksi/RekapPasHarian/RekapharianSvc.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <script type="text/javascript" src="../../AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="../../AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../../AdminLte/plugins/input-mask/jquery.inputmask.js"></script>
    <script type="text/javascript" src="../../AdminLte/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script type="text/javascript" src="../../AdminLte/plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <script type="text/javascript" src="../../Script/Transaksi/RekapPasHarian.js"></script>
</asp:Content>
