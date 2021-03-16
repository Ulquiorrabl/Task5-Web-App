$(document).ready(function () {
    $("#loginForm").validate({
        rules: {
            Email: {
                required: true,
                email: true
            },
            Password: {
                required: true,
                minlength: 5
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});