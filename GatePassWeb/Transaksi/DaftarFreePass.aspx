<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DaftarFreePass.aspx.cs" Inherits="GatePassWeb.Master.KendaraanMstrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/additionalcss/popover.css");
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="DaftarFreePassApp"
        ng-controller="DaftarFreePassController">
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
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Daftar Free Pas Pelabuhan</h3>
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
                                            <th>No Trans</th>
                                            <th>Divisi</th>
                                            <th>Instansi</th>
                                            <th>No. Identitas</th>
                                            <th>Nama</th>
                                            <th>Jenis Kendaraan</th>
                                            <th>Nopol 1</th>
                                            <th>Nopol 2</th>
                                            <th>Tgl. Akhir</th>
                                            <th>Keterangan</th>
                                            <th>No. RFID</th>
                                            <th style="width: 15%;"></th>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="text-center">
                                                <select class="form-control"
                                                    ng-model="searchKeyword.NamaDivisi"
                                                    ng-options="opt.KODE_DIVISI as opt.NAMA_DIVISI for opt in listDivisi"
                                                    ng-change="initialize()">
                                                </select>
                                            </td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Instansi" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Identitas" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Nama" ng-enter="initialize()">
                                            </td>
                                            <td class="text-center">
                                                <select class="form-control"
                                                    ng-model="searchKeyword.JenisKendaraan"
                                                    ng-options="opt.KD_KEND as opt.JNS_KENDARAAN for opt in listJenisKendaraan"
                                                    ng-change="initialize()">
                                                </select>    
                                            </td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Nopol1" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Nopol2" ng-enter="initialize()"></td>
                                            <td></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.Keterangan" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RFID" ng-enter="initialize()">
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr ng-repeat="data in tabledata">
                                            <td>{{data.NO_STIKER}}</td>
                                            <td>{{data.NAMA_DIVISI}}</td>
                                            <td>{{data.INSTANSI}}</td>
                                            <td class="text-center">
                                                <!--<span ng-if="data.KENA_PPN === 'Y'" class="badge bg-green">YA</span>
                                                <span ng-if="data.KENA_PPN === 'N'" class="badge bg-red">TIDAK</span>-->
                                                {{data.NO_ID}}
                                            </td>
                                            <td>{{data.NAMA}}</td>
                                            <td>{{data.JNS_KENDARAAN}}</td>
                                            <td>{{data.NOPOL_2}}</td>
                                            <td>{{data.NOPOL_4}}</td>
                                            <td>{{data.TGL_AKHIR}}</td>
                                            <td>{{data.KETERANGAN}}</td>
                                            <td>{{data.NO_RFID}}</td>
                                            <td class="text-center">
                                                <button class="btn btn-primary btn-xs" ng-click="triggerMstreditmode(1,data)">Edit&nbsp;<i class="fa fa-edit"></i></button>&nbsp;
                                                <button class="btn btn-danger btn-xs" ng-click="triggerMstrdelete(data.ID)">Hapus&nbsp;<i class="fa fa-trash-o"></i></button>&nbsp;
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
                            <h3 class="box-title">Daftar Stiker</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>No. Stiker</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.NO_STIKER">
                                </div>
                                <div class="form-group">
                                    <label>Divisi</label>
                                    <select class="form-control"
                                        ng-model="DaftarStikerModel.DIVISI"
                                        ng-options="opt.KODE_DIVISI as opt.NAMA_DIVISI for opt in listDivisi">
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Instansi</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.INSTANSI">
                                </div>
                                <div class="form-group">
                                    <label>No. Identitas</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.NO_ID">
                                </div>
                                <div class="form-group">
                                    <label>Nama</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.NAMA">
                                </div>
                                <div class="form-group">
                                    <label>Jenis Kendaraan</label>
                                    <select class="form-control"
                                        ng-model="DaftarStikerModel.JNS_KEND"
                                        ng-options="opt.KD_KEND as opt.JNS_KENDARAAN for opt in listJenisKendaraan">
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label>Nopol 1</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.NOPOL_2">
                                </div>
                                <div class="form-group">
                                    <label>Nopol 2</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.NOPOL_4">
                                </div>
                                <div class="form-group">
                                    <label>Tanggal Akhir</label>
                                    <input type="text" class="form-control" mask-date ng-model="DaftarStikerModel.TGL_AKHIR">
                                </div>
                                <div class="form-group">
                                    <label>Keterangan</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.KETERANGAN">
                                </div>
                                <div class="form-group">
                                    <label>No. RFID</label>
                                    <input type="text" class="form-control" ng-model="DaftarStikerModel.NO_RFID">
                                </div>
                            </div>
                            <div class="box-footer">
                                <button type="button" class="btn btn-primary" ng-click="saveTransaksi()">Simpan</button>
                                <button type="button" class="btn btn-danger" ng-click="modeeditmstr = 0">Kembali</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("../Service/Transaksi/DaftarFreePass.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />
    <script type="text/javascript" src="../AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/input-mask/jquery.inputmask.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <script type="text/javascript" src="../Script/Transaksi/DaftarFreePass.js"></script>
</asp:Content>
