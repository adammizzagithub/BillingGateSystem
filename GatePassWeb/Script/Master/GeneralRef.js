var GeneralRefApp = angular.module('GeneralRefApp', ['ngSanitize', 'ui.bootstrap']);

GeneralRefApp.directive('ngEnter', function () {
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
GeneralRefApp.controller('GeneralRefController', function ($scope, $modal) {
    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.currentPage = 1;
    $scope.maxSize = 5;
    $scope.totalrecords = 0;

    $scope.modeeditmstr = 0;
    $scope.modeeditdtl = 0;

    $scope.searchKeyword = new searchKey("", "", "", "", "", "", "Y");
    $scope.sortKeyWord = "ID_REF_FILE";
    $scope.sortmtdKeyword = "ASC"
    $scope.optionstatus = [
        { kode: "Y", name: "AKTIF" },
        { kode: "N", name: "NON AKTIF" }
    ];
    $scope.optionsort = [
        { kode: "ID_REF_FILE", name: "Ref. Id" },
        { kode: "KET_REFERENCE", name: "Ref. Keterangan" },
        { kode: "ATTRIB1", name: "Ref. Attrib1" },
        { kode: "ATTRIB2", name: "Ref. Attrib2" },
        { kode: "VAL1", name: "Ref. Val1" },
        { kode: "VAl2", name: "Ref. Val2" }
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
            $scope.oldreffile = "";
            $scope.MstRefModel = new MstrRefClass($scope.KodeCabang, "", "", "", "", "", "", "Y");
        }
        else {
            $scope.oldreffile = obj.ID_REF_FILE;
            $scope.MstRefModel = new MstrRefClass($scope.KodeCabang, obj.ID_REF_FILE, obj.KET_REFERENCE, obj.ATTRIB1, obj.ATTRIB2, obj.VAL1, obj.VAL2, obj.KD_AKTIF);
        }
    };
    $scope.saveMstr = function () {
        $.ajax({
            url: $scope.baseURL + "/InsertUpdateMstr",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.MstRefModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editable + ",oldreffile : '" + $scope.oldreffile + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editable) {
                        $scope.MstRefModel = new MstrRefClass($scope.KodeCabang, "", "", "", "", "", "", "Y");
                        $scope.$apply();
                    }
                    else {
                        $scope.oldreffile = $scope.MstRefModel.ID_REF_FILE;
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
    $scope.triggerDtleditmode = function (isEdit, obj, idreffile) {
        $scope.modeeditdtl = 1;
        $scope.editabledtl = isEdit === 0 ? false : true;
        if (isEdit === 0) {
            $scope.oldrefdata = "";
            $scope.DtlRefModel = new DtlRefClass($scope.KodeCabang, idreffile, "", "", "", "", "", "", "Y");
        }
        else {
            $scope.oldrefdata = obj.ID_REF_DATA;
            $scope.DtlRefModel = new DtlRefClass($scope.KodeCabang, idreffile, obj.ID_REF_DATA, obj.KET_REF_DATA, obj.ATTRIB1, obj.ATTRIB2, obj.VAL1, obj.VAL2, obj.KD_AKTIF);
        }
    };
    $scope.saveDtl = function () {

        $.ajax({
            url: $scope.baseURL + "/InsertUpdateDtl",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonobj : '" + JSON.stringify($scope.DtlRefModel) + "', userid : '" + $scope.userId + "', isedit : " + $scope.editabledtl + ",oldrefdata : '" + $scope.oldrefdata + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Penyimpanan gagal");
                }
                else {
                    bootbox.alert("Penyimpanan berhasil");
                    if (!$scope.editabledtl) {
                        $scope.DtlRefModel = new DtlRefClass($scope.KodeCabang, $scope.DtlRefModel.ID_REF_FILE, "", "", "", "", "", "", "Y");
                        $scope.$apply();
                    }
                    else {
                        $scope.oldrefdata = $scope.DtlRefModel.ID_REF_DATA;
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
            url: $scope.baseURL + "/GetReferenceData",
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
            url: $scope.baseURL + "/GetReferenceCount",
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
              columnName: "ID_REF_FILE",
              columnType: "string",
              columnValue: searchkey.RefId
          },
          {
              columnName: "KET_REFERENCE",
              columnType: "string",
              columnValue: searchkey.RefKet
          },
          {
              columnName: "ATTRIB1",
              columnType: "string",
              columnValue: searchkey.RefAttrib1
          },
          {
              columnName: "ATTRIB2",
              columnType: "string",
              columnValue: searchkey.RefAttrib2
          },
          {
              columnName: "VAL1",
              columnType: "string",
              columnValue: searchkey.RefVal1
          },
          {
              columnName: "VAL2",
              columnType: "string",
              columnValue: searchkey.RefVal2
          },
          {
              columnName: "KD_AKTIF",
              columnType: "string",
              columnValue: searchkey.Status
          }
        ];
    };
    function searchKey(RefId, RefKet, RefAttrib1, RefAttrib2, RefVal1, RefVal2, Status) {
        this.RefId = RefId;
        this.RefKet = RefKet;
        this.RefAttrib1 = RefAttrib1;
        this.RefAttrib2 = RefAttrib2;
        this.RefVal1 = RefVal1;
        this.RefVal2 = RefVal2;
        this.Status = Status;
    };
    function MstrRefClass(KdCabang, RefId, RefKet, RefAttrib1, RefAttrib2, RefVal1, RefVal2, KdAktif) {
        this.KD_CABANG = KdCabang;
        this.ID_REF_FILE = RefId;
        this.KET_REFERENCE = RefKet;
        this.ATTRIB1 = RefAttrib1;
        this.ATTRIB2 = RefAttrib2;
        this.VAL1 = RefVal1;
        this.VAL2 = RefVal2;
        this.KD_AKTIF = KdAktif;
    };
    function DtlRefClass(KdCabang, RefFile, RefData, RefKetData, RefAttrib1, RefAttrib2, RefVal1, RefVal2, KdAktif) {
        this.KD_CABANG = KdCabang;
        this.ID_REF_FILE = RefFile;
        this.ID_REF_DATA = RefData;
        this.KET_REF_DATA = RefKetData;
        this.ATTRIB1 = RefAttrib1;
        this.ATTRIB2 = RefAttrib2;
        this.VAL1 = RefVal1;
        this.VAL2 = RefVal2;
        this.KD_AKTIF = KdAktif;
    };
});
