<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="KendaraanMstrPage.aspx.cs" Inherits="GatePassWeb.Master.KendaraanMstrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/additionalcss/popover.css");
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="KendaraanApp"
        ng-controller="KendaraanController">
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
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Data Master Kendaraan</h3>
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
                                            <th>Kode</th>
                                            <th>Jenis</th>
                                            <th>Kena PPN</th>
                                            <th>Inex PPN</th>
                                            <th>Klas. Trans</th>
                                            <th>Ref. Code</th>
                                            <th style="width: 15%;"></th>
                                        </tr>
                                        <tr>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.KdKend" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.JnsKend" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <select class="form-control"
                                                    ng-model="searchKeyword.KenaPpn"
                                                    ng-options="opt.kode as opt.name for opt in optionkenappn"
                                                    ng-change="initialize()">
                                                </select>
                                            </td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.InexPpn" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.KlasTrans" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefCode" ng-enter="initialize()"></td>
                                            <td></td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-repeat="data in tabledata">
                                            <td>{{data.KD_KEND}}</td>
                                            <td>{{data.JNS_KENDARAAN}}</td>
                                            <td class="text-center">
                                                <span ng-if="data.KENA_PPN === 'Y'" class="badge bg-green">YA</span>
                                                <span ng-if="data.KENA_PPN === 'N'" class="badge bg-red">TIDAK</span>
                                            </td>
                                            <td>{{data.INEX_PPN}}</td>
                                            <td>{{data.KLAS_TRANS}}</td>
                                            <td>{{data.REF_CODE}}</td>
                                            <td class="text-center">
                                                <button class="btn btn-primary btn-xs" ng-click="triggerMstreditmode(1,data)">Edit&nbsp;<i class="fa fa-edit"></i></button>&nbsp;
                                                <button class="btn btn-danger btn-xs" ng-click="triggerMstrdelete(data.KD_KEND)">Hapus&nbsp;<i class="fa fa-trash-o"></i></button>&nbsp;
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
                            <h3 class="box-title">Master Kendaraan</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Kode Kendaraan</label>
                                    <input type="text" class="form-control" ng-model="KendaraanModel.KD_KEND">
                                </div>
                                <div class="form-group">
                                    <label>Jenis Kendaraan</label>
                                    <input type="text" class="form-control" ng-model="KendaraanModel.JNS_KENDARAAN">
                                </div>
                                <div class="form-group">
                                    <label>Kena PPN</label>
                                    <select class="form-control"
                                        ng-model="KendaraanModel.KENA_PPN"
                                        ng-options="opt.kode as opt.name for opt in optionkenappn">
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Inex PPN</label>
                                    <input type="text" class="form-control" ng-model="KendaraanModel.INEX_PPN">
                                </div>
                                <div class="form-group">
                                    <label>Klas Trans</label>
                                    <input type="text" class="form-control" ng-model="KendaraanModel.KLAS_TRANS">
                                </div>
                                <div class="form-group">
                                    <label>Ref. Code</label>
                                    <input type="text" class="form-control" ng-model="KendaraanModel.REF_CODE">
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
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("../Service/Master/Kendaraan/MstrKendaraan.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <script type="text/javascript" src="../AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../Script/Master/Kendaraan.js"></script>
</asp:Content>
