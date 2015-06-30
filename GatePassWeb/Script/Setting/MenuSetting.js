var MenuSettingApp = angular.module('MenuSettingApp', ['ngSanitize', 'ui.bootstrap']);

MenuSettingApp.directive('compile', function ($compile) {
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
MenuSettingApp.directive('ngEnter', function () {
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
MenuSettingApp.controller('MenuSettingController', function ($scope, $modal, $sce, $compile) {

    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.treeviewdata;
    $scope.link_menu = $('#modalMenu').val();
    $scope.jsonserialize;

    $scope.initializemenu = function () {
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Loading menu ...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/getMenuTreeView",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                $scope.treeviewdata = data.d;
                $scope.$apply();
                $('.dd').nestable();
                $('.dd').on('change', function () {
                    $scope.jsonserialize = $('.dd').nestable('serialize');
                });
                $.unblockUI();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $.unblockUI();

            }
        });
    };

    $scope.saveConfig = function () {
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Menyimpan data ...</h5>' });
        obj = new Object();
        obj.ListConfig = $scope.jsonserialize;
        console.log(obj.ListConfig);
        $.ajax({
            url: $scope.baseURL + "/SaveMenuConfiguration",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{jsonstring : '" + JSON.stringify(obj) + "', userid: '" + $scope.userId + "'}",
            success: function (data) {
                bootbox.dialog({
                    message: "Perubahan berhasil dilakukan dan akan dilakukan reload halaman",
                    title: "Notification",
                    buttons: {
                        success: {
                            label: "Reload",
                            className: "btn-success",
                            callback: function () {
                                window.location.reload();
                                $.unblockUI();
                            }
                        }
                    }
                });
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $.unblockUI();

            }
        });
    };

    $('#tmbhnew').webuiPopover(
        {
            title: 'Tambah Menu Baru',
            type: 'html',
            content: generateHtmlPopoverAddMenu(),
            width: 600,
            heigth: 800,
            closeable: true,
            animation: 'pop',
            placement: 'right-bottom'
        }
    );
    $(document).on("click", "#btntmbhsimpannew", function () {
        newMenu = new Object();
        newMenu.Bgsm_Menu_Nama = $('#nama').val();
        newMenu.Bgsm_Menu_Vurl = $('#vurl').val();
        newMenu.Bgsm_Menu_Purl = $('#purl').val();
        newMenu.Bgsm_Menu_Icon = $('#iconic').val();
        newMenu.Creation_By = $scope.userId;
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Memuat data...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/CreateNewMenu",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{obj : '" + JSON.stringify(newMenu) + "'}",
            success: function (data) {
                if (data.d === 1) {
                    $('#tmbhnew').webuiPopover('hide');
                    clearinputTambah();
                    bootbox.alert("Proses berhasil");
                    $scope.initializemenu();
                }
                else {
                    $('#tmbhnew').webuiPopover('hide');
                    clearinputTambah();
                    bootbox.alert("Proses gagal");
                }
                $.unblockUI();
            },
            error: function (err) {
                $('#tmbhnew').webuiPopover('hide');
                clearinputTambah();
                bootbox.alert(err.responseText);
                $.unblockUI();

            }
        });
    });
    $('body').on('click', '.edit-menu', function () {
        var menu_id = $(this).attr("menu-id");
        $.ajax({
            url: $scope.baseURL + "/getMenuViaId",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{menuid: " + parseInt(menu_id) + "}",
            success: function (data) {
                obj = JSON.parse(data.d);
                var modalInstance = $modal.open({
                    templateUrl: $('#editmodalMenu').val(),
                    controller: 'EditMenuSettingController',
                    resolve: {
                        userid: function () { return $scope.userId },
                        wsUrl: function () { return $scope.baseURL },
                        menuObj: function () { return obj; }
                    }
                });
                modalInstance.result.then(function () {
                    $scope.initializemenu();
                });
            },
            error: function (err) {
                bootbox.alert(err.responseText);
            }
        });

    });
    $('body').on('click', '.delete-menu', function () {
        var menu_id = $(this).attr("menu-id");
        bootbox.confirm("Yakin menghapus menu ?", function (result) {
            if (result) {
                $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Menghapus ...</h5>' });
                $.ajax({
                    url: $scope.baseURL + "/DeleteMenu",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{menuid: " + parseInt(menu_id) + "}",
                    success: function (data) {
                        if (data.d === 1) {
                            bootbox.alert("Proses berhasil");
                            $scope.initializemenu();
                        }
                        else {
                            bootbox.alert("Proses gagal");
                        }
                        $.unblockUI();
                    },
                    error: function (err) {
                        bootbox.alert(err.responseText);
                        $.unblockUI();
                    }
                });
            }
        });
    });


    /* START FUNCTION */
    function generateHtmlPopoverAddMenu() {
        var html = "";
        html += '<div class="row">';
        html += ' <div class="col-md-12">';
        html += ' <div class="box box-primary">';
        html += ' <div class="box-header with-border">';
        html += '  <h3 class="box-title">Tambah Menu</h3>';
        html += '  </div>';
        html += ' <div class="box-body">';
        html += '   <div class="form-group">';
        html += '    <label for="nama">Nama</label>';
        html += '    <input type="text" class="form-control" id="nama" placeholder="Nama">';
        html += '   </div>';
        html += '  <div class="form-group">';
        html += '     <label for="vurl">Virtual URL</label>';
        html += '     <input type="text" class="form-control" id="vurl" placeholder="URL Browser">';
        html += '  </div>';
        html += '    <div class="form-group">';
        html += '     <label for="purl">Physical URL</label>';
        html += '     <input type="text" class="form-control" id="purl" placeholder="URL Aplikasi">';
        html += '  </div>';
        html += '   <div class="form-group">';
        html += '     <label for="iconic">Icon</label>';
        html += '      <input type="text" class="form-control" id="iconic" placeholder="Menu Ikon">';
        html += '    </div>';
        html += ' </div>';
        html += ' <div class="box-footer">';
        html += '    <button id="btntmbhsimpannew" type="button" class="btn btn-primary"> Tambah</button>';
        html += ' </div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        return html;
    };
    function clearinputTambah() {
        $('#nama').val('');
        $('#vurl').val('');
        $('#purl').val('');
        $('#iconic').val('');
    }
    /* END FUNCTION */

});
MenuSettingApp.controller('EditMenuSettingController', function ($scope, $modalInstance, userid, wsUrl, menuObj) {
    $scope.menus = menuObj;
    $scope.save = function () {
        $scope.menus.Last_Update_By = userid;
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Menyimpan ...</h5>' });
        $.ajax({
            url: wsUrl + "/UpdateMenu",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{obj: '" + JSON.stringify($scope.menus) + "'}",
            success: function (data) {
                if (data.d === 1) {
                    bootbox.alert("Proses berhasil");
                    $modalInstance.close();
                }
                else {
                    bootbox.alert("Proses gagal");
                }
                $.unblockUI();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $.unblockUI();
            }
        });
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
});