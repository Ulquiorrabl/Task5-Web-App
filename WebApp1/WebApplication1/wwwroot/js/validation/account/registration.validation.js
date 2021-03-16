$(document).ready(function () {
    $("#registerForm").validate({
        rules: {
            UserName: {
                required: true,
                minlength: 3,
                maxlength: 30
            },
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 3,
                maxlength: 30
            },
            ConfirmPassword: {
                required: true
            }

        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});