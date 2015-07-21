<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="TarifMstrPage.aspx.cs" Inherits="GatePassWeb.Master.TarifMstrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/plugins/select2/select2.css");
        @import url("AdminLte/additionalcss/popover.css");
        @import url("../AdminLte/plugins/angularjs-table/ng-table.css");
        @import url("../AdminLte/plugins/angularjs-table/ng-table.less");
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="TarifApp"
        ng-controller="TarifController">
        <section class="content-header">
            <div class="row">
                <div class="col-md-2">
                    <button class="btn bg-olive" ng-click="triggerMstreditmode(0,'')">
                        <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Tambah
                    </button>
                </div>
            </div>
        </section>
        <section class="content">
            <div class="row" ng-init="initialize()"
                ng-show="modeeditmstr === 0 && modeeditdtl === 0">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Data Master Tarif</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>
                        </div>
                        <div class="box-body" style="display: block;">
                            <div class="col-lg-6 input-group">
                                <span class="input-group-addon" style="border: none; padding-top: 0px;">Sort by :</span>
                                <select class="form-control"
                                    ng-model="sortKeyWord"
                                    ng-options="opt.kode as opt.name for opt in optionsort"
                                    ng-change="defineSortCol(sortKeyWord)">
                                </select>
                                <span class="input-group-addon" style="border: none; padding-top: 0px;">
                                    <input type="radio" ng-model="sortmtdKeyword" value="ASC"><i class="fa fa-sort-alpha-asc"></i>&nbsp;&nbsp;
                                    <input type="radio" ng-model="sortmtdKeyword" value="DESC"><i class="fa fa-sort-alpha-desc"></i>
                                </span>
                                <span class="input-group-addon" style="border: none; padding-top: 0px;">
                                    <a href="javascript:;" class="btn btn-warning" ng-click="doingsort()">SORT</a>
                                </span>
                            </div>
                            <div class="table-responsive table-scrollable">
                                <table class="table table-bordered table-hover table-striped">
                                    <thead>
                                        <tr>
                                            <th></th>
                                            <th>Tarif. Kode</th>
                                            <th>Tarif. No.B.A</th>
                                            <th>Tarif. Tgl berlaku</th>
                                            <th>Tarif. Keterangan</th>
                                            <th>Tarif. Jenis pas</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.TrfKode" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.TrfNoBa" ng-enter="initialize()"></td>
                                            <td></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.TrfKet" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.TrfPlyName" ng-enter="initialize()"></td>

                                            <td></td>
                                        </tr>
                                    </thead>
                                    <tbody ng-repeat="data in tabledata">
                                        <tr>
                                            <td class="text-center">
                                                <a ng-if="data.expanded" ng-click="data.expanded = false">
                                                    <i class="fa fa-chevron-circle-down"></i></a>
                                                <a ng-if="!data.expanded" ng-click="data.expanded = true">
                                                    <i class="fa fa-chevron-circle-right"></i></a>
                                            </td>
                                            <td>{{data.KD_TARIF}}</td>
                                            <td>{{data.NO_BA}}</td>
                                            <td>{{data.TGL_TMT}}</td>
                                            <td>{{data.KET_TARIF}}</td>
                                            <td>{{data.NM_PLY}}</td>
                                            <td>
                                                <button class="btn btn-primary btn-xs" ng-click="triggerMstreditmode(1,data)">Edit&nbsp;<i class="fa fa-edit"></i></button>&nbsp;
                                                <button class="btn btn-danger btn-xs" ng-click="triggerMstrdelete(data.KD_TARIF)">Hapus&nbsp;<i class="fa fa-trash-o"></i></button>&nbsp;
                                            </td>
                                        </tr>
                                        <tr ng-if="data.expanded">
                                            <td colspan="7" class="text-left">
                                                <button class="btn btn-success" ng-click="triggerDtleditmode(0,'',data.KD_TARIF)">Detail Tarif&nbsp;&nbsp;<i class="fa fa-plus-square"></i></button>
                                            </td>
                                        </tr>
                                        <tr ng-if="data.expanded">
                                            <th></th>
                                            <th style="width: 25px;">No.</th>
                                            <th>Jenis Kendaraan</th>
                                            <th>Tarif</th>
                                            <th colspan="2">Keterangan</th>
                                            <th></th>
                                        </tr>
                                        <tr ng-if="data.expanded" ng-repeat="o in data.Details">
                                            <td><i class="fa fa-minus-circle"></i></td>
                                            <td style="width: auto;">{{o.NO_SEQ}}</td>
                                            <td>{{o.NM_KEND}}</td>
                                            <td class="text-right">{{o.TARIF | currency:""}}</td>
                                            <td colspan="2">{{o.KETERANGAN}}</td>
                                            <td>
                                                <button class="btn btn-primary btn-xs" ng-click="triggerDtleditmode(1,o,data.KD_TARIF)">Edit&nbsp;<i class="fa fa-edit"></i></button>
                                                <button class="btn btn-danger btn-xs" ng-click="triggerDtldelete(data.KD_TARIF,o.NO_SEQ)">Hapus&nbsp;<i class="fa fa-trash-o"></i></button>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="text-right">
                                    <pagination boundary-links="true" total-items="totalrecords"
                                        page="currentPage" max-size="maxSize"
                                        previous-text="&lsaquo;" next-text="&rsaquo;"
                                        first-text="&laquo;" last-text="&raquo;" on-select-page="changepage(page)">
                                    </pagination>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" style="display: block;">
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" ng-show="modeeditmstr === 1">
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Master Tarif</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Tarif. Kode</label>
                                    <input type="text" class="form-control" ng-model="TarifModel.KD_TARIF">
                                </div>
                                <div class="form-group">
                                    <label>Tarif. Pelayanan pas</label>
                                    <select class="form-control"
                                        ng-model="TarifModel.KD_PLY"
                                        ng-options="opt.ID_REF_DATA as opt.KET_REF_DATA for opt in arrlovref">
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Tarif. No.B.A</label>
                                    <input type="text" class="form-control" ng-model="TarifModel.NO_BA">
                                </div>
                                <div class="form-group">
                                    <label>Tarif. Tgl.berlaku</label>
                                    <input type="text" class="form-control" mask-date ng-model="TarifModel.TGL_TMT">
                                </div>
                                <div class="form-group">
                                    <label>Tarif. Keterangan</label>
                                    <textarea class="form-control" rows="3" ng-model="TarifModel.KET_TARIF"
                                        placeholder="Ketik ..."></textarea>
                                </div>
                            </div>
                            <div class="box-footer">
                                <button type="button" class="btn btn-primary" ng-click="saveMstr()">Simpan</button>
                                <button type="button" class="btn btn-danger" ng-click="modeeditmstr = 0;modeeditdtl = 0;">Kembali</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
            <div class="row" ng-show="modeeditdtl === 1">
                <div class="col-md-6">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Master Tarif</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Tarif. Kode</label>
                                    <input type="text" disabled class="form-control" ng-model="TarifDetModel.KD_TARIF">
                                </div>
                                <div class="form-group">
                                    <label>Jenis Kendaraan</label>
                                    <select class="form-control"
                                        ng-model="TarifDetModel.JNS_KEND"
                                        ng-options="opt.KD_KEND as opt.JNS_KENDARAAN for opt in arrlovkend">
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Tarif</label>
                                    <input type="number" class="form-control" ng-model="TarifDetModel.TARIF">
                                </div>
                                <div class="form-group">
                                    <label>Tarif. Keterangan</label>
                                    <textarea class="form-control" rows="3" ng-model="TarifDetModel.KETERANGAN"
                                        placeholder="Ketik ..."></textarea>
                                </div>
                            </div>
                            <div class="box-footer">
                                <button type="button" class="btn btn-primary" ng-click="saveDtl()">Simpan</button>
                                <button type="button" class="btn btn-danger" ng-click="modeeditmstr = 0;modeeditdtl = 0;">Kembali</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("../Service/Master/Tarif/MstrTarif.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <script type="text/javascript" src="../AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/input-mask/jquery.inputmask.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <script type="text/javascript" src="../Script/Master/Tarif.js"></script>
</asp:Content>
