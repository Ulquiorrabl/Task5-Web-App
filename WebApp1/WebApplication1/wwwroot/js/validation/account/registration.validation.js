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
                containLower: true,
                containUpper: true,
                containNum: true,
                containSym: true,
                minlength: 5,
                maxlength: 30
            },
            ConfirmPassword: {
                required: true,
                equalTo : "#Password"
            }

        },
        messages: {
            Password: {
                minlength: "Password must be at least 5 characters long",
                maxlength: "Password maximum length is 30",
                containNum: "Password must contain numbers",
                containSym: "Password must contain symbols",
                containUpper: "Password must contain upper case letters",
                containLower: "Password must contain lower case letters"
            },
            UserName: {
                required: "Username required",
                minlength: "Username must be at least 3 characters long",
                maxlength: "Username maximum length is 30"
            },
            Email: {
                required: "Email required",
                email: "Please, enter valid email"
            },
            ConfirmPassword: {
                required: "Enter confirm password",
                equalTo: "Passwords doesn't match"
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
    $.validator.addMethod("containNum", function (value) {
        var pattern = /(\d)/;
        return pattern.test(value);
    });
    $.validator.addMethod("containSym", function (value) {
        var pattern = /([!@#$%^&*])/;
        return pattern.test(value);
    });
    $.validator.addMethod("containUpper", function (value) {
        var pattern = /([A-Z])/;
        return pattern.test(value);
    });
    $.validator.addMethod("containLower", function (value) {
        var pattern = /([a-z])/;
        return pattern.test(value);

    });
});