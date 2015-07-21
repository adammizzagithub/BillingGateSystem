<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GeneralRefMstrPage.aspx.cs" Inherits="GatePassWeb.Master.GeneralRefMstrPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        @import url("AdminLte/plugins/select2/select2.css");
        @import url("AdminLte/additionalcss/popover.css");
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="content-wrapper" ng-app="GeneralRefApp"
        ng-controller="GeneralRefController">
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
                            <h3 class="box-title" style="font-family: 'Comic Sans MS';">Data Master Referensi</h3>
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
                                            <th>Ref. Id</th>
                                            <th>Ref. Keterangan</th>
                                            <th>Ref. Attrib1</th>
                                            <th>Ref. Attrib2</th>
                                            <th>Ref. Val1</th>
                                            <th>Ref. Val2</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefId" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefKet" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefAttrib1" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefAttrib2" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefVal1" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <input type="text" class="form-control" ng-model="searchKeyword.RefVal2" ng-enter="initialize()"></td>
                                            <td class="text-center">
                                                <select class="select2me form-control"
                                                    ng-model="searchKeyword.Status"
                                                    ng-options="opt.kode as opt.name for opt in optionstatus"
                                                    ng-change="initialize()">
                                                </select>
                                            </td>
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
                                            <td>{{data.ID_REF_FILE}}</td>
                                            <td>{{data.KET_REFERENCE}}</td>
                                            <td>{{data.ATTRIB1}}</td>
                                            <td>{{data.ATTRIB2}}</td>
                                            <td>{{data.VAL1}}</td>
                                            <td>{{data.VAL2}}</td>
                                            <td class="text-center">
                                                <span ng-if="data.KD_AKTIF === 'Y'" class="badge bg-green">AKTIF</span>
                                                <span ng-if="data.KD_AKTIF === 'N'" class="badge bg-red">NON AKTIF</span>
                                            </td>
                                            <td>
                                                <button class="btn btn-primary btn-xs" ng-click="triggerMstreditmode(1,data)">Edit&nbsp;<i class="fa fa-edit"></i></button>
                                            </td>
                                        </tr>
                                        <tr ng-if="data.expanded">
                                            <td colspan="9" class="text-left">
                                                <button class="btn btn-success" ng-click="triggerDtleditmode(0,'',data.ID_REF_FILE)">Detail Referensi&nbsp;&nbsp;<i class="fa fa-plus-square"></i></button>
                                            </td>
                                        </tr>
                                        <tr ng-if="data.expanded">
                                            <th></th>
                                            <th>Ref. Data</th>
                                            <th>Ref. Keterangan</th>
                                            <th>Ref. Attrib1</th>
                                            <th>Ref. Attrib2</th>
                                            <th>Ref. Val1</th>
                                            <th>Ref. Val2</th>
                                            <th>Status</th>
                                            <th></th>
                                        </tr>
                                        <tr ng-if="data.expanded" ng-repeat="o in data.Details">
                                            <td><i class="fa fa-minus-circle"></i></td>
                                            <td>{{o.ID_REF_DATA}}</td>
                                            <td>{{o.KET_REF_DATA}}</td>
                                            <td>{{o.ATTRIB1}}</td>
                                            <td>{{o.ATTRIB2}}</td>
                                            <td>{{o.VAL1}}</td>
                                            <td>{{o.VAL2}}</td>
                                            <td>
                                                <span ng-if="o.KD_AKTIF === 'Y'" class="badge bg-green">AKTIF</span>
                                                <span ng-if="o.KD_AKTIF === 'N'" class="badge bg-red">NON AKTIF</span>
                                            </td>
                                            <td>
                                                <button class="btn btn-primary btn-xs" ng-click="triggerDtleditmode(1,o,data.ID_REF_FILE)">Edit&nbsp;<i class="fa fa-edit"></i></button>
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
                            <h3 class="box-title">Master Referensi</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="txtidref">Ref. Id</label>
                                    <input type="text" class="form-control" id="txtidref" ng-model="MstRefModel.ID_REF_FILE">
                                </div>
                                <div class="form-group">
                                    <label for="txtketref">Ref. Keterangan</label>
                                    <input type="text" class="form-control" id="txtketref" ng-model="MstRefModel.KET_REFERENCE">
                                </div>
                                <div class="form-group">
                                    <label for="txtattr1">Ref. Attrib1</label>
                                    <input type="text" class="form-control" id="txtattr1" ng-model="MstRefModel.ATTRIB1">
                                </div>
                                <div class="form-group">
                                    <label for="txtattr2">Ref. Attrib2</label>
                                    <input type="text" class="form-control" id="txtattr2" ng-model="MstRefModel.ATTRIB2">
                                </div>
                                <div class="form-group">
                                    <label for="txtval1">Ref. Val1</label>
                                    <input type="text" class="form-control" id="txtval1" ng-model="MstRefModel.VAL1">
                                </div>
                                <div class="form-group">
                                    <label for="txtval2">Ref. Val2</label>
                                    <input type="text" class="form-control" id="txtval2" ng-model="MstRefModel.VAL2">
                                </div>
                                <div class="form-group">
                                    <label>Status</label>
                                    <select class="form-control"
                                        ng-model="MstRefModel.KD_AKTIF"
                                        ng-options="opt.kode as opt.name for opt in optionstatus">
                                    </select>
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
                            <h3 class="box-title">Detail Referensi</h3>
                        </div>
                        <form role="form">
                            <div class="box-body">
                                <div class="form-group">
                                    <label for="txtidref">Ref. File</label>
                                    <input type="text" disabled class="form-control" id="dtxtidreffl" ng-model="DtlRefModel.ID_REF_FILE">
                                </div>
                                <div class="form-group">
                                    <label for="txtidref">Ref. Id</label>
                                    <input type="text" class="form-control" id="dtxtidref" ng-model="DtlRefModel.ID_REF_DATA">
                                </div>
                                <div class="form-group">
                                    <label for="txtketref">Ref. Keterangan</label>
                                    <input type="text" class="form-control" id="dtxtketref" ng-model="DtlRefModel.KET_REF_DATA">
                                </div>
                                <div class="form-group">
                                    <label for="txtattr1">Ref. Attrib1</label>
                                    <input type="text" class="form-control" id="dtxtattr1" ng-model="DtlRefModel.ATTRIB1">
                                </div>
                                <div class="form-group">
                                    <label for="txtattr2">Ref. Attrib2</label>
                                    <input type="text" class="form-control" id="dtxtattr2" ng-model="DtlRefModel.ATTRIB2">
                                </div>
                                <div class="form-group">
                                    <label for="txtval1">Ref. Val1</label>
                                    <input type="text" class="form-control" id="dtxtval1" ng-model="DtlRefModel.VAL1">
                                </div>
                                <div class="form-group">
                                    <label for="txtval2">Ref. Val2</label>
                                    <input type="text" class="form-control" id="dtxtval2" ng-model="DtlRefModel.VAL2">
                                </div>
                                <div class="form-group">
                                    <label>Status</label>
                                    <select class="form-control"
                                        ng-model="DtlRefModel.KD_AKTIF"
                                        ng-options="opt.kode as opt.name for opt in optionstatus">
                                    </select>
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
    <input type="hidden" id="wsurl" name="wsurl" value="<%=this.ResolveUrl("../Service/Master/GeneralReferensi/MstrGeneralRef.asmx")%>" />
    <input type="hidden" id="userId" name="userId" value="<%= this.Session["UserName"]%>" />
    <input type="hidden" id="KodeCabang" name="KodeCabang" value="<%= this.Session["KodeCabang"]%>" />

    <script type="text/javascript" src="../AdminLte/plugins/bootbox.min.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/jquery.blockUI.js"></script>
    <script type="text/javascript" src="../AdminLte/plugins/select2/select2.js"></script>
    <script type="text/javascript" src="../Script/Master/GeneralRef.js"></script>
</asp:Content>
