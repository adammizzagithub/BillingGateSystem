var DaftarFreePassApp = angular.module('DaftarFreePassApp', ['ngSanitize', 'ui.bootstrap']);

DaftarFreePassApp.directive('ngEnter', function () {
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

DaftarFreePassApp.directive('maskDate', function () {
    return {
        restrict: "EA",
        link: function (scope, elm, attr) {
            $(elm).inputmask("dd/mm/yyyy");
        }
    };
});

DaftarFreePassApp.controller('DaftarFreePassController', function ($scope, $modal) {
    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.kd_cabang = $('#KodeCabang').val();
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    $scope.totalrecords = 0;

    $scope.modeeditmstr = 0;

    $scope.searchKeyword = new searchKey("","","", "", "", "", "", "", "","", "");
    $scope.sortKeyWord = "INSTANSI";
    $scope.sortmtdKeyword = "ASC";
    $scope.optionkenappn = [
        { kode: "Y", name: "YA" },
        { kode: "N", name: "TIDAK" }
    ];
    $scope.optionsort = [
        { kode: "NO_STIKER", name: "No. Trans" },
        { kode: "DIVISI", name: "Divisi" },
        { kode: "INSTANSI", name: "Instansi" },
        { kode: "NO_ID", name: "NIP/No. Identitas" },
        { kode: "NAMA", name: "Nama" },
        { kode: "JNS_KEND", name: "Jenis Kendaraan" },
        { kode: "NOPOL_2", name: "Nopol 1" },
        { kode: "NOPOL_4", name: "Nopol 2" },
        { kode: "TGL_AKHIR", name: "Tgl Akhir" },
        { kode: "KETERANGAN", name: "Keterangan" },
        { kode: "NO_RFID", name: "No RFID" }
    ];
    $scope.defineSortCol = function (txt) {
        $scope.sortKeyWord = txt;
    };
    $scope.initialize = function () {
        QueryStringBuilder = new Object();
        getListDivisi($scope.kd_cabang);
        getListJenisKendaraan($scope.kd_cabang);
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
            $scope.id = "";
            $scope.DaftarStikerModel = new DaftarStikerClass($scope.kd_cabang, "", "", "", "", "", "", "", "", "", "", "");
        }
        else {
            $scope.id = obj.ID;
            $scope.DaftarStikerModel = new DaftarStikerClass($scope.kd_cabang, obj.NO_STIKER, obj.DIVISI, obj.INSTANSI, obj.NO_ID, obj.NAMA, obj.JNS_KEND, obj.NOPOL_2, obj.NOPOL_4, obj.TGL_AKHIR, obj.KETERANGAN, obj.NO_RFID);
        }
    };
    $scope.triggerMstrdelete = function (id) {
        bootbox.confirm("Yakin menghapus data ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/DeleteFreePass",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{id : '" + id + "'}",
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
    $scope.saveTransaksi = function () {
        $.ajax({
            url: $scope.baseURL + "/InsertUpdateFreePass",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.DaftarStikerModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editable + ",id : '" + $scope.id + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editable) {
                        $scope.DaftarStikerModel = new DaftarStikerClass($scope.kd_cabang, "", "", "", "", "", "", "", "", "", "", "");
                        $scope.$apply();
                    }
                    else {
                        $scope.id = $scope.DaftarStikerModel.ID;
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
            url: $scope.baseURL + "/GetFreePassData",
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
            url: $scope.baseURL + "/GetFreePassCount",
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
                columnName: "NO_STIKER",
                columnType: "string",
                columnValue: searchkey.NoStiker
            },
            {
                columnName: "DIVISI",
                columnType: "string",
                columnValue: searchkey.NamaDivisi
            },
            {
              columnName: "INSTANSI",
              columnType: "string",
              columnValue: searchkey.Instansi
          },
          {
              columnName: "NO_ID",
              columnType: "string",
              columnValue: searchkey.Identitas
          },
          {
              columnName: "NAMA",
              columnType: "string",
              columnValue: searchkey.Nama
          },
          {
              columnName: "NOPOL_2",
              columnType: "string",
              columnValue: searchkey.Nopol1
          },
          {
              columnName: "NOPOL_4",
              columnType: "string",
              columnValue: searchkey.Nopol2
          },
          {
              columnName: "KETERANGAN",
              columnType: "string",
              columnValue: searchkey.Keterangan
          },
          {
              columnName: "NO_RFID",
              columnType: "string",
              columnValue: searchkey.RFID
          },
          {
              columnName: "JNS_KEND",
              columnType: "string",
              columnValue: searchkey.JenisKendaraan
          },
          {
              columnName: "TGL_AKHIR",
              columnType: "string",
              columnValue: searchkey.TglAkhir
          }
        ];
    };
    function searchKey(NoStiker, NamaDivisi, Instansi, JenisKendaraan, Identitas, Nama, Nopol1, Nopol2, Keterangan, RFID, TglAkhir) {
        this.NoStiker = NoStiker;
        this.NamaDivisi = NamaDivisi;
        this.Instansi = Instansi;
        this.Identitas = Identitas;
        this.Nama = Nama;
        this.Nopol1 = Nopol1;
        this.Nopol2 = Nopol2;
        this.Keterangan = Keterangan;
        this.RFID = RFID;
        this.JenisKendaraan = JenisKendaraan;
        this.TglAkhir = TglAkhir;
    };
    function DaftarStikerClass(kdcabang, no_stiker, divisi, instansi, no_id, nama, jns_kend, nopol_2, nopol_4, tgl_akhir, keterangan, no_rfid) {
        this.KD_CABANG = kdcabang;
        this.NO_STIKER = no_stiker;
        this.DIVISI = divisi;
        this.INSTANSI = instansi;
        this.NO_ID = no_id;
        this.NAMA = nama;
        this.JNS_KEND = jns_kend;
        this.NOPOL_2 = nopol_2;
        this.NOPOL_4 = nopol_4;
        this.TGL_AKHIR = tgl_akhir;
        this.KETERANGAN = keterangan;
        this.NO_RFID = no_rfid;
    };

    function getListDivisi(kd_cabang) {
        $.ajax({
            url: $scope.baseURL + "/GetListDivisi",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{kd_cabang : '" + kd_cabang + "'}",
            success: function (data) {
                $scope.listDivisi = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };

    function getListJenisKendaraan(kd_cabang) {
        $.ajax({
            url: $scope.baseURL + "/GetListJenisKendaraan",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{kd_cabang : '" + kd_cabang + "'}",
            success: function (data) {
                $scope.listJenisKendaraan = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };

});