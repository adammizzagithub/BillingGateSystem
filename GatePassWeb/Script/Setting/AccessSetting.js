var AccessSettingApp = angular.module('AccessSettingApp', ['ngSanitize', 'ui.bootstrap', 'ui.tree']);

AccessSettingApp.directive('compile', function ($compile) {
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
AccessSettingApp.directive('ngEnter', function () {
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
AccessSettingApp.controller('AccessSettingController', function ($scope, $modal, $sce, $compile) {
    $scope.baseURL = $('#wsurl').val();
    $scope.userId = $('#userId').val();
    $scope.KodeCabang = $('#KodeCabang').val();
    $scope.tabledata;
    $scope.controlroledata;
    $scope.linkrender = $("#menutree").val();
    $scope.viewcaption = 0;
    $scope.roleIdGeneral = -99;

    $scope.initialize = function () {
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Loading ...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/GetAllRoles",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (data) {
                $scope.tabledata = JSON.parse(data.d);
                $scope.$apply();
                $.unblockUI();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $.unblockUI();
            }
        });
    };
    $scope.deleterole = function (roleId) {
        bootbox.confirm("Yakin menghapus role ?", function (res) {
            if (res) {
                $.ajax({
                    url: $scope.baseURL + "/DeleteRole",
                    type: "POST",
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    data: "{RoleId : " + roleId + "}",
                    success: function (data) {
                        if (data.d === 1) {
                            bootbox.alert("Proses berhasil");
                            $scope.initialize();
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
    };
    $scope.initialmenu = function (roleId) {
        $scope.viewcaption = 1;
        $scope.roleIdGeneral = roleId;
        $('#menushow').block({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Menyimpan data...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/GetRolesByHakAksesId",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{HakAksesId : " + roleId + "}",
            success: function (data) {
                $scope.controlroledata = JSON.parse(data.d);
                $scope.tabulardata = [];
                for (var j = 0; j < $scope.controlroledata.length; j++)
                    getAllDataFromRecursive($scope.controlroledata[j]);
                $scope.$apply();
                $('#menushow').unblock();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $('#menushow').unblock();

            }
        });
    };
    $scope.automate = function (node, isCheck) {
        function getNodeParent(idNode) {
            var idx = -1;
            $.each($scope.tabulardata, function (index, item) {
                if (item.Bgsm_Menu_Id === idNode) idx = index;
            });
            return $scope.tabulardata[idx];
        };
        function recursiveParent(temporaryNode) {
            if (temporaryNode.Bgsm_Menu_Parent !== -1) {
                var ibiNode = getNodeParent(temporaryNode.Bgsm_Menu_Parent);
                ibiNode.Status = '1';
                recursiveParent(ibiNode);
            }
        };
        function recursiveCheck(node, isCheck) {
            if (node.Childs !== null) {
                for (a = 0; a < node.Childs.length; a++) {
                    node.Childs[a].Status = isCheck;
                    recursiveCheck(node.Childs[a], isCheck);
                }
            }
        };
        /* NODE HAS CHILD */
        if (node.Childs !== null) {
            for (var is = 0; is < node.Childs.length; is++) {
                node.Childs[is].Status = isCheck;
                recursiveCheck(node.Childs[is], isCheck);
            }
        }
        /* NODE HAS PARENT */
        if (node.Bgsm_Menu_Parent !== -1 && isCheck === '1') {
            var tempNode = getNodeParent(node.Bgsm_Menu_Parent);
            tempNode.Status = '1';
            recursiveParent(tempNode);
        }
    };

    /* START MENU BUTTON ACTION */
    $scope.saveSettingRole = function () {
        $scope.viewcaption = 0;
        $scope.check = [];
        $('.cekIt:checked').each(function (i) {
            $scope.check.push($(this).val());
        });
        var objSend = new Object();
        arr = [];
        for (var i = 0; i < $scope.check.length; i++) {
            obj = new Object();
            obj.Bgsm_Control_Aksesid = $scope.roleIdGeneral;
            obj.Bgsm_Control_Menuid = parseInt($scope.check[i]);
            obj.Created_Date = new Date();
            obj.Created_By = $scope.userId;
            arr.push(obj);
        }
        objSend.multipleValue = arr;
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Loading ...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/CreateControlByRole",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{json : '" + JSON.stringify(objSend) + "'}",
            success: function (data) {
                if (data.d === 1) {
                    bootbox.alert("Proses berhasil");
                    $scope.initialize();
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
    $scope.batalSettingRole = function () {
        $scope.viewcaption = 0;
    };
    /* END MENU BUTTON ACTION */

    $('#tmbhnew').webuiPopover(
       {
           title: 'Tambah Role Baru',
           type: 'html',
           content: generateHtmlPopoverAddMenu(),
           width: 600,
           heigth: 800,
           closeable: true,
           animation: 'pop',
           placement: 'right-bottom'
       }
   );
    $('#select-aktif-role').select2();
    $(document).on("click", "#btntmbhsimpannew", function () {
        newRole = new Object();
        newRole.Bgsm_Akses_Role = $('#txtrole').val();
        newRole.Bgsm_Akses_Status = $('#select-aktif-role').val();
        newRole.Creation_By = $scope.userId;
        $.blockUI({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Menyimpan data...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/CreateNewRole",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{obj : '" + JSON.stringify(newRole) + "'}",
            success: function (data) {
                if (data.d === 1) {
                    $('#tmbhnew').webuiPopover('hide');
                    clearinputTambah();
                    bootbox.alert("Proses berhasil");
                    $scope.initialize();
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

    /* START FUNCTION */
    function generateHtmlPopoverAddMenu() {
        var html = "";
        html += '<div class="row">';
        html += ' <div class="col-md-12">';
        html += ' <div class="box box-primary">';
        html += ' <div class="box-header with-border">';
        html += '  <h3 class="box-title">Tambah Role</h3>';
        html += '  </div>';
        html += ' <div class="box-body">';
        html += '   <div class="form-group">';
        html += '    <label for="txtrole">Role</label>';
        html += '    <input type="text" class="form-control" id="txtrole" placeholder="Role">';
        html += '   </div>';
        html += '   <div class="form-group">';
        html += '     <label for="select-aktif-role">Status</label>';
        html += '      <select class="select2me form-control " id="select-aktif-role"><option value="Y">Aktif</option><option value="N">Non Aktif</option></select>';
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
        $('#txtrole').val('');
        $('#select-aktif-role').select2('data', {
            id: 'Y', value: 'Aktif'
        });
    };
    function getAllDataFromRecursive(obj) {
        if (obj.Childs === null)
            $scope.tabulardata.push(obj);
        else {
            $scope.tabulardata.push(obj);
            for (var k = 0; k < obj.Childs.length; k++)
                getAllDataFromRecursive(obj.Childs[k]);
        }
    };
    /* END FUNCTION */
});
