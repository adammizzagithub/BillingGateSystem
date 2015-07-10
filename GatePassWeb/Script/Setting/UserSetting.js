var UserSettingApp = angular.module('UserSettingApp', ['ngSanitize', 'ui.bootstrap']);

UserSettingApp.directive('compile', function ($compile) {
    return function (scope, element, attrs) {
        scope.$watch(
          function (scope) {
              // watch the 'compile' expression for changes
              return scope.$eval(attrs.compile);
          },
          function (value) {
              // when the 'compile' expression changes
              // assign it into the current DOM
              element.html(value);

              // compile the new DOM and link it to the current
              // scope.
              // NOTE: we only compile .childNodes so that
              // we don't get into infinite loop compiling ourselves
              $compile(element.contents())(scope);
          }
      );
    };
});
UserSettingApp.directive('ngEnter', function () {
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
UserSettingApp.controller('UserSettingController', function ($scope) {

    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.arrCabang = [];
    $scope.arrRole = [];

    $scope.populateData = function () {
        $.ajax({
            url: $scope.baseURL + "/GetReference",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{code : 0}",
            success: function (data) {
                $scope.arrCabang = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
        $.ajax({
            url: $scope.baseURL + "/GetReference",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{code : 1}",
            success: function (data) {
                $scope.arrRole = JSON.parse(data.d);
                $scope.$apply();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });
    };
    function bindTable() {
        $('#tbl-user').dataTable().fnDestroy();
        $('#tbl-user').dataTable({
            "aLengthMenu": [
               [10, 25, 50, 100, -1],
               [10, 25, 50, 100, "All"]
            ],

            "bProcessing": true,
            "bServerSide": true,
            "bRetrieve": true,
            "bDestroy": true,
            "bAutoWidth": false,
            "sAjaxSource": "Service/Setting/UserTable.ashx?method=ProcessRequest",
            //"fnServerParams": function (aoData) {
            //    aoData.push(
            //        { "name": "KodeCabang", "value": kdCabang }
            //    );
            //},
            "iDisplayLength": 10,
            "bStateSave": true,
            "sPaginationType": "bootstrap_full_number",
            "oLanguage": {
                "sProcessing": '<i class="fa fa-coffee"></i>&nbsp;Please wait...',
                "sLengthMenu": "_MENU_ records",
                "oPaginate": {
                    "sPrevious": "Prev",
                    "sNext": "Next"
                }
            },
            "aoColumns": [
              { "bSortable": false, "sClass": "notView" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sClass": "notView" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sClass": "notView" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "notView" }
            ]
        });
        $('#tbl-user_wrapper .dataTables_filter input').addClass("form-control");
        $('#tbl-user_wrapper .dataTables_length select').addClass("form-control");
    }
    var userTable = $('#tbl-user').dataTable({
        "aLengthMenu": [
           [10, 25, 50, 100, -1],
           [10, 25, 50, 100, "All"]
        ],

        "bProcessing": true,
        "bServerSide": true,
        "bRetrieve": true,
        "bDestroy": true,
        "bAutoWidth": false,
        "sAjaxSource": "Service/Setting/UserTable.ashx?method=ProcessRequest",
        //"fnServerParams": function (aoData) {
        //    aoData.push(
        //        { "name": "KodeCabang", "value": kdCabang }
        //    );
        //},
        "iDisplayLength": 10,
        "bStateSave": true,
        "sPaginationType": "bootstrap_full_number",
        "oLanguage": {
            "sProcessing": '<i class="fa fa-coffee"></i>&nbsp;Please wait...',
            "sLengthMenu": "_MENU_ records",
            "oPaginate": {
                "sPrevious": "Prev",
                "sNext": "Next"
            }
        },
        "aoColumns": [
           { "bSortable": false, "sClass": "notView" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sClass": "notView" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sClass": "notView" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "alignCenter" },
           { "bSortable": false, "sWidth": "15%", "sClass": "notView" }
        ]
    });
    $('#tbl-user_wrapper .dataTables_filter input').addClass("form-control");
    $('#tbl-user_wrapper .dataTables_length select').addClass("form-control");
    //$('#tbl-user_wrapper .dataTables_length select').select2();

    /* EDIT BUTTON DATATABLE */
    $('body').on('click', '.btn-edituser', function () {
        var row = $(this).parents('tr')[0];
        var td = $('>td', row);
        var idunique = parseInt(td[0].innerHTML);
        var kdcabang = td[2].innerHTML;
        var nmcabang = td[3].innerHTML;
        var roleid = td[6].innerHTML;
        var rolename = td[7].innerHTML;
        var isaktif = td[9].innerHTML;

        td[3].innerHTML = '<select id="cbg' + idunique + '" class="select2me form-control"></select>';
        td[7].innerHTML = '<select id="role' + idunique + '" class="select2me form-control"></select>';
        td[8].innerHTML = '<a class="btn btn-primary btn-xs btn-simpan-useredit" idunique=' + idunique + '><i class="fa fa-check"></i></a> ' +
                          '<a class="btn btn-danger btn-xs btn-batal-useredit" isaktif="' + isaktif + '" idunique=' + idunique + ' kdcabang="' + kdcabang + '" nmcabang="' + nmcabang + '" kdrole="' + roleid + '" nmrole="' + rolename + '"><i class="fa fa-times"></i></a>';

        $('#cbg' + idunique + '').find('option').remove();
        $('#role' + idunique + '').find('option').remove();
        for (var i = 0; i < $scope.arrCabang.length; i++) $('#cbg' + idunique + '').append("<option value=\"" + $scope.arrCabang[i].Kode_Cabang + "\">" + $scope.arrCabang[i].Nama_Cabang + "</option>");
        for (var i = 0; i < $scope.arrRole.length; i++) $('#role' + idunique + '').append("<option value=\"" + $scope.arrRole[i].Bgsm_Akses_Id + "\">" + $scope.arrRole[i].Bgsm_Akses_Role + "</option>");

        $('#cbg' + idunique + '').val(kdcabang);
        $('#role' + idunique + '').val(roleid);

    });
    $('body').on('click', '.btn-simpan-useredit', function (e) {
        obj = new Object();
        var id = $(this).attr('idunique');
        obj.Bgsm_User_Id = id;
        obj.Bgsm_User_Kdcabang = $('#cbg' + id + '').val();
        obj.Bgsm_User_Cabang = $("#cbg" + id + " option:selected").text();
        obj.Bgsm_User_Roleid = $('#role' + id + '').val();
        obj.Bgsm_User_Rolename = $("#role" + id + " option:selected").text();
        obj.Last_Update_By = $scope.userId;
        $.ajax({
            url: $scope.baseURL + "/UpdateUser",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{obj : '" + JSON.stringify(obj) + "'}",
            success: function (data) {
                if (data.d === 0) {
                    bootbox.alert("Pengubahan gagal !");
                }
                else {
                    bootbox.alert("Pengubahan berhasil !");
                    bindTable();
                }
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });

    });
    $('body').on('click', '.btn-batal-useredit', function (e) {
        e.preventDefault();
        var row = $(this).parents('tr')[0];
        var td = $('>td', row);
        isaktif = $(this).attr('isaktif');

        td[2].innerHTML = $(this).attr('kdcabang');
        td[3].innerHTML = $(this).attr('nmcabang');
        td[6].innerHTML = $(this).attr('kdrole');
        td[7].innerHTML = $(this).attr('nmrole');
        if (isaktif === "Y")
            td[8].innerHTML = "<a href='javascript:;' class='btn btn-primary btn-xs btn-edituser'><i class='fa fa-edit'></i> Edit</a> <a href='javascript:;' class='btn btn-danger btn-xs btn-activate' data-isaktif='N'><i class='fa fa-times'></i> Nonaktifkan</a>";
        else
            td[8].innerHTML = "<a href='javascript:;' class='btn btn-primary btn-xs btn-edituser'><i class='fa fa-edit'></i> Edit</a> <a href='javascript:;' class='btn btn-default btn-xs btn-activate' data-isaktif='Y'><i class='fa fa-times'></i> Aktifkan</a>";
    });
    /* ACTIVATE BUTTON DATATABLE */
    $('body').on('click', '.btn-activate', function () {
        //ActivateUser
        var row = $(this).parents('tr')[0];
        var td = $('>td', row);
        userid = parseInt(td[0].innerHTML);
        newval = $(this).attr('data-isaktif');
        bootbox.confirm("Setuju untuk mengubah ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/ActivateUser",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{userId : " + userid + ", newvalue : '" + newval + "'}",
                    success: function (data) {
                        if (data.d === 0) {
                            bootbox.alert("Pengubahan gagal !");
                        }
                        else {
                            bootbox.alert("Pengubahan berhasil !");
                            bindTable();
                        }
                    },
                    error: function (err) {
                        bootbox.alert(err.responseText);
                    }
                });
            }
        });
    });

});