var RekapPasHarianApp = angular.module('RekapPasHarianApp', ['ngRoute']);
RekapPasHarianApp.directive('maskDate', function () {
    return {
        restrict: "EA",
        link: function (scope, elm, attr) {
            $(elm).inputmask("dd/mm/yyyy");
        }
    };
});
RekapPasHarianApp.directive('ngEnter', function () {
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
RekapPasHarianApp.controller('RekapPasHarianFormController', function ($scope) {

    $scope.baseURL = $('#wsurl').val();
    $scope.kodecabang = $('#KodeCabang').val();
    $scope.userid = $('#userId').val();


    $scope.initialize = function () {
        newDate = new Date();
        var dateNow = newDate.getDate() < 10 ? "0" + newDate.getDate() : newDate.getDate();
        var mthNow = (newDate.getMonth() + 1) < 10 ? "0" + (newDate.getMonth() + 1) : (newDate.getMonth() + 1);
        $scope.inputModel = new formrekapinput(dateNow + "/" + mthNow + "/" + newDate.getFullYear(), "", "");
        /* LOAD GATE */
        $.ajax({
            url: $scope.baseURL + "/GetGate",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                $scope.arrlovgate = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
        /* LOAD PERIODE */
        $.ajax({
            url: $scope.baseURL + "/GetPeriode",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                $scope.arrlovperiode = data.d;
                for (var i = 0; i < $scope.arrlovperiode.length; i++)
                    $scope.arrlovperiode[i] = JSON.parse($scope.arrlovperiode[i]);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    $scope.queryDataNeto = function () {
        $.ajax({
            url: $scope.baseURL + "/GetRekapQuery",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{tanggal : '" + $scope.inputModel.Date + "', kdgate : '" + $scope.inputModel.Gate + "'}",
            success: function (data) {
                $scope.rekapqueryharian = data.d;
                for (var i = 0; i < $scope.rekapqueryharian.length; i++)
                    $scope.rekapqueryharian[i] = JSON.parse($scope.rekapqueryharian[i]);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
        $.ajax({
            url: $scope.baseURL + "/GetTotalBruto",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{tanggal : '" + $scope.inputModel.Date + "', kdgate : '" + $scope.inputModel.Gate + "'}",
            success: function (data) {
                $scope.totalbruto = data.d;
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
        $.ajax({
            url: $scope.baseURL + "/GetTotalNeto",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{tanggal : '" + $scope.inputModel.Date + "', kdgate : '" + $scope.inputModel.Gate + "'}",
            success: function (data) {
                $scope.totalneto = data.d;
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    $scope.simpanrekap = function () {
        var datas = new Object();
        datas.data = [];
        for (var i = 0; i < $scope.rekapqueryharian.length; i++) {
            objInput = new Object();
            objInput.KdCabang = $scope.kodecabang;
            objInput.TglTrans = new Date(parseInt($scope.inputModel.Date.split("/")[2]), parseInt($scope.inputModel.Date.split("/")[1]) - 1, parseInt($scope.inputModel.Date.split("/")[0]));
            objInput.Gate = $scope.inputModel.Gate;
            objInput.Tahun = $scope.inputModel.Date.split("/")[2];
            objInput.Bulan = $scope.inputModel.Date.split("/")[1];
            objInput.JnsKend = $scope.rekapqueryharian[i].Jns_Kend;
            objInput.Jumlah = $scope.rekapqueryharian[i].Qty;
            objInput.Tarif = $scope.rekapqueryharian[i].Tarif;
            objInput.Pendapatan = $scope.rekapqueryharian[i].Pndp_Gross;
            objInput.Ppn = $scope.rekapqueryharian[i].Pndp_Gross;
            objInput.CreatedBy = $scope.userid;
            objInput.LastUpdatedBy = $scope.userid;
            datas.data.push(objInput);
        }
        /* SAVE INPUT */
        $.ajax({
            url: $scope.baseURL + "/SimpanRekapHarian",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{objectdata : '" + JSON.stringify(datas) + "'}",
            success: function (data) {
                if (data.d === 1) {
                    bootbox.alert("Transaksi Berhasil");
                }
                else {
                    bootbox.alert("Transaksi Gagal");
                }
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    function formrekapinput(Date, Gate, Periode) {
        this.Date = Date;
        this.Gate = Gate;
        this.Periode = Periode;
    };

});