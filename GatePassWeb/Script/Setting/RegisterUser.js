var RegisterUserApp = angular.module('RegisterUserApp', []);

RegisterUserApp.controller('RegisterUserController', function ($scope) {

    $scope.baseURL = $('#wsurl').val();
    $scope.alerts = 0;
    $scope.alertg = 0;

    $('#registerform').validate({
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        rules: {
            txtnama: {
                required: true
            },
            txtusername: {
                required: true
            },
            txtpassword: {
                required: true
            }
        },

        messages: {
        },

        invalidHandler: function (event, validator) { //display error alert on form submit   
            $('.alert-danger', $('#registerform')).show();
        },

        highlight: function (element) { // hightlight error inputs
            $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        success: function (label) {
            label.closest('.form-group').removeClass('has-error');
            label.remove();
        },

        errorPlacement: function (error, element) {
            error.insertAfter(element.closest('.input-icon'));
        },

        submitHandler: function (form) {
            $scope.register();
        }
    });

    $scope.register = function () {
        $scope.alerts = 0;
        $scope.alertg = 0;

        userobject = new Object();
        userobject.Bgsm_User_Nama = $('#txtnama').val();
        userobject.Bgsm_User_Username = $('#txtusername').val();
        userobject.Bgsm_User_Password = $('#txtpassword').val();
        $('.register-box').block({ message: '<h5><img src="' + 'http://' + window.location.host + '/AdminLte/img/busy.gif" />  Loading ...</h5>' });
        $.ajax({
            url: $scope.baseURL + "/CreateNewUser",
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: "{obj : '" + JSON.stringify(userobject) + "'}",
            success: function (data) {
                if (data.d === 0) {
                    $('.alert-success').hide();
                    $('.alert-danger').show();
                    $scope.$apply();
                }
                else {
                    $('.alert-success').show();
                    $('.alert-danger').hide();
                    $scope.$apply();
                }
                clearinput();
                $('.register-box').unblock();
            },
            error: function (err) {
                bootbox.alert(err.responseText);
                $('.register-box').unblock();
            }
        });
    };

    function clearinput() {
        $('#txtnama').val('');
        $('#txtusername').val('');
        $('#txtpassword').val('');
    };

});
