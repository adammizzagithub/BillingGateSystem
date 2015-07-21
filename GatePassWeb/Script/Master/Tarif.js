var TarifApp = angular.module('TarifApp', ['ngSanitize', 'ui.bootstrap']);

TarifApp.directive('ngEnter', function () {
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
TarifApp.directive('maskDate', function () {
    return {
        restrict: "EA",
        link: function (scope, elm, attr) {
            $(elm).inputmask("dd/mm/yyyy");
        }
    };
});
TarifApp.controller('TarifController', function ($scope, $modal) {
    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    $scope.totalrecords = 0;

    $scope.modeeditmstr = 0;
    $scope.modeeditdtl = 0;

    $scope.searchKeyword = new searchKey("", "", "", "", "");
    $scope.sortKeyWord = "KD_TARIF";
    $scope.sortmtdKeyword = "ASC";
    $scope.optionsort = [
        { kode: "KD_TARIF", name: "Kode Tarif" },
        { kode: "NO_BA", name: "Nomor B.A" },
        { kode: "TGL_TMT", name: "Tanggal berlaku" },
        { kode: "KET_TARIF", name: "Keterangan" },
        { kode: "NM_PLY", name: "Jenis pelayanan pas" }
    ];
    $scope.defineSortCol = function (txt) {
        $scope.sortKeyWord = txt;
    };
    $scope.initialize = function () {
        QueryStringBuilder = new Object();
        BuildQueryString(QueryStringBuilder, 1, 10, $scope.sortKeyWord, $scope.sortmtdKeyword, $scope.searchKeyword);
        GetData(QueryStringBuilder);
        GetCount(QueryStringBuilder);
        LovKendaraan($scope.KodeCabang);
        LovReference($scope.KodeCabang);
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
            $scope.oldkdtrf = "";
            $scope.TarifModel = new TarifClass($scope.KodeCabang, "", "", "", "", "");
        }
        else {
            $scope.oldkdtrf = obj.KD_TARIF;
            $scope.TarifModel = new TarifClass($scope.KodeCabang, obj.KD_TARIF, obj.KD_PLY, obj.NO_BA, obj.TGL_TMT, obj.KET_TARIF);
        }
    };
    $scope.triggerMstrdelete = function (kdtarif) {
        bootbox.confirm("Yakin menghapus data ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/DeleteMstr",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{kdtrf : '" + kdtarif + "'}",
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
            url: $scope.baseURL + "/InsertUpdateMstr",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.TarifModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editable + ",oldkdtrf : '" + $scope.oldkdtrf + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editable) {
                        $scope.TarifModel = new TarifClass($scope.KodeCabang, "", "", "", "", "");
                        $scope.$apply();
                    }
                    else {
                        $scope.oldkdtrf = $scope.TarifModel.KD_TARIF;
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

    $scope.triggerDtleditmode = function (isEdit, obj, kdtarif) {
        $scope.modeeditdtl = 1;
        $scope.editabledtl = isEdit === 0 ? false : true;
        if (isEdit === 0) {
            $scope.oldid = 0;
            $scope.TarifDetModel = new TarifDetClass($scope.KodeCabang, kdtarif, 0, "", 0, "");
        }
        else {
            $scope.oldid = obj.NO_SEQ;
            $scope.TarifDetModel = new TarifDetClass($scope.KodeCabang, obj.KD_TARIF, obj.NO_SEQ, obj.JNS_KEND, obj.TARIF, obj.KETERANGAN);
        }
    };
    $scope.triggerDtldelete = function (kdtarif, noseq) {
        bootbox.confirm("Yakin menghapus data ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/DeleteDetail",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{kdtarif : '" + kdtarif + "',noseq: "+noseq+"}",
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
    $scope.saveDtl = function () {
        $.ajax({
            url: $scope.baseURL + "/InsertUpdateDetail",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.TarifDetModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editabledtl + ",noseq : " + $scope.oldid + "}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editabledtl) {
                        $scope.TarifDetModel = new TarifDetClass($scope.KodeCabang, $scope.TarifDetModel.KD_TARIF, 0, "", 0, "");
                        $scope.$apply();
                    }
                    else {
                        $scope.oldid = $scope.TarifDetModel.NO_SEQ;
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
            url: $scope.baseURL + "/GetTarifData",
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
            url: $scope.baseURL + "/GetTarifCount",
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
              columnName: "KD_TARIF",
              columnType: "string",
              columnValue: searchkey.TrfKode
          },
          {
              columnName: "NM_PLY",
              columnType: "string",
              columnValue: searchkey.TrfPlyName
          },
          {
              columnName: "NO_BA",
              columnType: "string",
              columnValue: searchkey.TrfNoBa
          },
          {
              columnName: "KET_TARIF",
              columnType: "string",
              columnValue: searchkey.TrfKet
          }
        ];
    };
    function searchKey(TrfKode, TrfPlyName, TrfNoBa, TrfKet) {
        this.TrfKode = TrfKode;
        this.TrfPlyName = TrfPlyName;
        this.TrfNoBa = TrfNoBa;
        this.TrfKet = TrfKet;
    };
    function LovReference(kdcabang) {
        $.ajax({
            url: $scope.baseURL + "/GetLovJenisPas",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{kdcabang : '" + kdcabang + "'}",
            success: function (data) {
                $scope.arrlovref = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    function LovKendaraan(kdcabang) {
        $.ajax({
            url: $scope.baseURL + "/GetLovKendaraan",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{kdcabang : '" + kdcabang + "'}",
            success: function (data) {
                $scope.arrlovkend = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    function TarifClass(kdcabang, KdTarif, KdPly, NoBa, TglTmt, KetTarif) {
        this.KD_CABANG = kdcabang;
        this.KD_TARIF = KdTarif;
        this.KD_PLY = KdPly;
        this.NO_BA = NoBa;
        this.TGL_TMT = TglTmt;
        this.KET_TARIF = KetTarif;
    };
    function TarifDetClass(kdcabang, KdTarif, NoSeq, JnsKend, Tarif, Keterangan) {
        this.KD_CABANG = kdcabang;
        this.KD_TARIF = KdTarif;
        this.NO_SEQ = NoSeq;
        this.JNS_KEND = JnsKend;
        this.TARIF = Tarif;
        this.KETERANGAN = Keterangan;
    };
});