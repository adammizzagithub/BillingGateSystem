var Login = function () {
    var handleLogin = function () {

        $('#loginform').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                txtusername: {
                    required: true
                },
                txtpassword: {
                    required: true
                }
            },

            messages: {
                txtusername: {
                    required: "Username is required."
                },
                txtpassword: {
                    required: "Password is required."
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                $('.alert-danger', $('#loginform')).show();
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
                form.submit();
                console.log($('#txtusername').val());
                console.log($('#txtpassword').val());
            }
        });
    }
    return {
        init: function () {
            handleLogin();
        }
    };

}();