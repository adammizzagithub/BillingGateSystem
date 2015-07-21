<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PegOprMstrPage.aspx.cs" Inherits="GatePassWeb.Master.PegOprMstrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/additionalcss/popover.css");
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="PegOprApp"
        ng-controller="PegOprController">
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
                ng-show="modeeditmstr === 0">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Data Master Pegawai Operator</h3>
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
                                            <th>NIP</th>
                                            <th>Nama</th>
                                            <th>Gate</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Nip" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Nama" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <select class="form-control"
                                                    ng-model="searchKeyword.NamaGate"
                                                    ng-options="opt.ID_REF_DATA as opt.KET_REF_DATA for opt in arrlovgate"
                                                    ng-change="initialize()">
                                                </select>
                                            </td>
                                            <td class="text-center">
                                                <select class="form-control"
                                                    ng-model="searchKeyword.RecStatus"
                                                    ng-options="opt.kode as opt.name for opt in optionstatus"
                                                    ng-change="initialize()">
                                                </select>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </thead>
                                    <tbody ng-repeat="data in tabledata">
                                        <tr>
                                            <td>{{data.NIP_PEG}}</td>
                                            <td>{{data.NM_PEG}}</td>
                                            <td>{{data.NM_GATE}}</td>
                                            <td class="text-center">
                                                <span ng-if="data.REC_STAT === 'Y'" class="badge bg-green">AKTIF</span>
                                                <span ng-if="data.REC_STAT === 'N'" class="badge bg-red">NON AKTIF</span>
                                            </td>
                                            <td>
                                                <button class="btn btn-primary btn-xs" ng-click="triggerMstreditmode(1,data)">Edit&nbsp;<i class="fa fa-edit"></i></button>&nbsp;
                                                <button class="btn btn-danger btn-xs" ng-click="triggerMstrdelete(data.NIP_PEG)">Hapus&nbsp;<i class="fa fa-trash-o"></i></button>&nbsp;
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
                            <h3 class="box-title">Master Pegawai Operator</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>NIP</label>
                                    <input type="text" class="form-control" ng-model="PegOprModel.NIP_PEG">
                                </div>
                                <div class="form-group">
                                    <label>Nama</label>
                                    <input type="text" class="form-control" ng-model="PegOprModel.NM_PEG">
                                </div>
                                <div class="form-group">
                                    <label>Gate</label>
                                    <select class="form-control"
                                        ng-model="PegOprModel.KD_GATE"
                                        ng-options="opt.ID_REF_DATA as opt.KET_REF_DATA for opt in arrlovgate">
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Status</label>
                                    <select class="form-control"
                                        ng-model="PegOprModel.REC_STAT"
                                        ng-options="opt.kode as opt.name for opt in optionstatus">
                                    </select>
                                </div>
                            </div>
                            <div class="box-footer">
                                <button type="button" class="btn btn-primary" ng-click="saveMstr()">Simpan</button>
                                <button type="button" class="btn btn-danger" ng-click="modeeditmstr = 0">Kembali</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("../Service/Master/PegawaiOperator/MstrPegOpr.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <script type="text/javascript" src="../AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../Script/Master/PegawaiOperator.js"></script>
</asp:Content>
