var PegOprApp = angular.module('PegOprApp', ['ngSanitize', 'ui.bootstrap']);

PegOprApp.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});
PegOprApp.controller('PegOprController', function ($scope, $modal) {
    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    $scope.totalrecords = 0;

    $scope.modeeditmstr = 0;

    $scope.searchKeyword = new searchKey("", "", "Y", "");
    $scope.sortKeyWord = "NIP_PEG";
    $scope.sortmtdKeyword = "ASC";
    $scope.optionstatus = [
        { kode: "Y", name: "AKTIF" },
        { kode: "N", name: "NON AKTIF" }
    ];
    $scope.optionsort = [
        { kode: "NIP_PEG", name: "Nip. Pegawai" },
        { kode: "NM_PEG", name: "Nama. Pegawai" },
        { kode: "NM_GATE", name: "Gate" }
    ];
    $scope.defineSortCol = function (txt) {
        $scope.sortKeyWord = txt;
    };
    $scope.initialize = function () {
        QueryStringBuilder = new Object();
        LovGate($scope.KodeCabang);
        BuildQueryString(QueryStringBuilder, 1, 10, $scope.sortKeyWord, $scope.sortmtdKeyword, $scope.searchKeyword);
        GetData(QueryStringBuilder);
        GetCount(QueryStringBuilder);
    };
    $scope.changepage = function (page) {
        $scope.currentPage = page;
        QueryStringBuilder = new Object();
        BuildQueryString(QueryStringBuilder, (page * 10) - 9, page * 10, $scope.sortKeyWord, $scope.sortmtdKeyword, $scope.searchKeyword);
        GetData(QueryStringBuilder);
        GetCount(QueryStringBuilder);
    };
    $scope.doingsort = function () {
        QueryStringBuilder = new Object();
        BuildQueryString(QueryStringBuilder, ($scope.currentPage * 10) - 9, $scope.currentPage * 10, $scope.sortKeyWord, $scope.sortmtdKeyword, $scope.searchKeyword);
        GetData(QueryStringBuilder);
        GetCount(QueryStringBuilder);
    };
    $scope.triggerMstreditmode = function (isEdit, obj) {
        $scope.modeeditmstr = 1;
        $scope.editable = isEdit === 0 ? false : true;
        if (isEdit === 0) {
            $scope.oldnippeg = "";
            $scope.PegOprModel = new PegOprClass($scope.KodeCabang, "", "", "", "Y");
        }
        else {
            $scope.oldnippeg = obj.NIP_PEG;
            $scope.PegOprModel = new PegOprClass($scope.KodeCabang, obj.NIP_PEG, obj.NM_PEG, obj.KD_GATE, obj.REC_STAT);
        }
    };
    $scope.triggerMstrdelete = function (nippeg) {
        bootbox.confirm("Yakin menghapus data ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/DeletePegOpr",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{nippeg : '" + nippeg + "'}",
                    success: function (data) {
                        if (data.d === 0) {
                            bootbox.alert("Gagal menghapus data");
                        }
                        else {
                            bootbox.alert("Berhasil Menghapus data");
                            $scope.initialize();
                        }
                    },
                    error: function (err) {
                        bootbox.alert(err.responseText);
                    }
                });
            };
        });
    };
    $scope.saveMstr = function () {
        $.ajax({
            url: $scope.baseURL + "/InsertUpdatePegOpr",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.PegOprModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editable + ",nippeg : '" + $scope.oldnippeg + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editable) {
                        $scope.PegOprModel = new PegOprClass($scope.KodeCabang, "", "", "", "Y");
                        $scope.$apply();
                    }
                    else {
                        $scope.oldnippeg = $scope.PegOprModel.NIP_PEG;
                        $scope.$apply();
                    }
                    $scope.initialize();
                }
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };

    function GetData(queryString) {
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Loading ...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/GetPegOprData",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{queryString : '" + JSON.stringify(queryString) + "'}",
            success: function (data) {
                $scope.tabledata = data.d;
                for (var i = 0; i < $scope.tabledata.length; i++)
                    $scope.tabledata[i] = JSON.parse($scope.tabledata[i]);
                $scope.$apply();
                $.unblockUI();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $.unblockUI();
            }
        });
    };
    function GetCount(queryString) {
        $.ajax({
            url: $scope.baseURL + "/GetPegOprCount",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{queryString : '" + JSON.stringify(queryString) + "'}",
            success: function (data) {
                $scope.totalrecords = data.d;
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    function BuildQueryString(QueryStringBuilder, offset, limit, sortname, sortmethod, searchkey) {
        QueryStringBuilder.orderName = sortname;
        QueryStringBuilder.orderMethod = sortmethod;
        QueryStringBuilder.offset = offset;
        QueryStringBuilder.limit = limit;
        QueryStringBuilder.datas = [
          {
              columnName: "KD_CABANG",
              columnType: "string",
              columnValue: $scope.KodeCabang
          },
          {
              columnName: "NIP_PEG",
              columnType: "string",
              columnValue: searchkey.Nip
          },
          {
              columnName: "NM_PEG",
              columnType: "string",
              columnValue: searchkey.Nama
          },
          {
              columnName: "REC_STAT",
              columnType: "string",
              columnValue: searchkey.RecStatus
          },
          {
              columnName: "KD_GATE",
              columnType: "string",
              columnValue: searchkey.NamaGate
          }
        ];
    };
    function searchKey(Nip, Nama, RecStatus, NamaGate) {
        this.Nip = Nip;
        this.Nama = Nama;
        this.RecStatus = RecStatus;
        this.NamaGate = NamaGate;
    };
    function LovGate(kdcabang) {
        $.ajax({
            url: $scope.baseURL + "/GetLovGate",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{kdcabang : '" + kdcabang + "'}",
            success: function (data) {
                $scope.arrlovgate = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    function PegOprClass(kdcabang, NipPeg, NamaPeg, KdGate, RecStat) {
        this.KD_CABANG = kdcabang;
        this.NIP_PEG = NipPeg;
        this.NM_PEG = NamaPeg;
        this.KD_GATE = KdGate;
        this.REC_STAT = RecStat;
    };

});