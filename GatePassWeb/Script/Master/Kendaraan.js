var KendaraanApp = angular.module('KendaraanApp', ['ngSanitize', 'ui.bootstrap']);

KendaraanApp.directive('ngEnter', function () {
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
KendaraanApp.controller('KendaraanController', function ($scope, $modal) {
    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    $scope.totalrecords = 0;

    $scope.modeeditmstr = 0;

    $scope.searchKeyword = new searchKey("", "", "Y", "", "", "");
    $scope.sortKeyWord = "KD_KEND";
    $scope.sortmtdKeyword = "ASC";
    $scope.optionkenappn = [
        { kode: "Y", name: "YA" },
        { kode: "N", name: "TIDAK" }
    ];
    $scope.optionsort = [
        { kode: "KD_KEND", name: "Kode Kend." },
        { kode: "JNS_KENDARAAN", name: "Jenis Kend." },
        { kode: "INEX_PPN", name: "Inex PPN" },
        { kode: "KLAS_TRANS", name: "Klas Trans" },
        { kode: "REF_CODE", name: "Ref. Code" }
    ];
    $scope.defineSortCol = function (txt) {
        $scope.sortKeyWord = txt;
    };
    $scope.initialize = function () {
        QueryStringBuilder = new Object();
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
            $scope.kdkend = "";
            $scope.KendaraanModel = new KendaraanClass($scope.KodeCabang, "", "", "Y", "", "", "");
        }
        else {
            $scope.kdkend = obj.KD_KEND;
            $scope.KendaraanModel = new KendaraanClass($scope.KodeCabang, obj.KD_KEND, obj.JNS_KENDARAAN, obj.KENA_PPN, obj.INEX_PPN, obj.KLAS_TRANS, obj.REF_CODE);
        }
    };
    $scope.triggerMstrdelete = function (kdkend) {
        bootbox.confirm("Yakin menghapus data ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/DeleteKend",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{kdkend : '" + kdkend + "'}",
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
            url: $scope.baseURL + "/InsertUpdateKend",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.KendaraanModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editable + ",kdkend : '" + $scope.kdkend + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editable) {
                        $scope.KendaraanModel = new KendaraanClass($scope.KodeCabang, "", "", "Y", "", "", "");
                        $scope.$apply();
                    }
                    else {
                        $scope.kdkend = $scope.KendaraanModel.KD_KEND;
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
            url: $scope.baseURL + "/GetKendData",
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
            url: $scope.baseURL + "/GetKendCount",
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
              columnName: "KD_KEND",
              columnType: "string",
              columnValue: searchkey.KdKend
          },
          {
              columnName: "JNS_KENDARAAN",
              columnType: "string",
              columnValue: searchkey.JnsKend
          },
          {
              columnName: "KENA_PPN",
              columnType: "string",
              columnValue: searchkey.KenaPpn
          },
          {
              columnName: "INEX_PPN",
              columnType: "string",
              columnValue: searchkey.InexPpn
          },
          {
              columnName: "KLAS_TRANS",
              columnType: "string",
              columnValue: searchkey.KlasTrans
          },
          {
              columnName: "REF_CODE",
              columnType: "string",
              columnValue: searchkey.RefCode
          },
        ];
    };
    function searchKey(KdKend, JnsKend, KenaPpn, InexPpn, KlasTrans, RefCode) {
        this.KdKend = KdKend;
        this.JnsKend = JnsKend;
        this.KenaPpn = KenaPpn;
        this.InexPpn = InexPpn;
        this.KlasTrans = KlasTrans;
        this.RefCode = RefCode;
    };
    function KendaraanClass(kdcabang, KdKend, JnsKend, KenaPpn, InexPpn, KlasTrans, RefCode) {
        this.KD_CABANG = kdcabang;
        this.KD_KEND = KdKend;
        this.JNS_KENDARAAN = JnsKend;
        this.KENA_PPN = KenaPpn;
        this.INEX_PPN = InexPpn;
        this.KLAS_TRANS = KlasTrans;
        this.REF_CODE = RefCode;
    };

});