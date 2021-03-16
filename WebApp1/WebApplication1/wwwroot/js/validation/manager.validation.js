$(document).ready(function () {
    $("#productForm").validate({
        rules: {
            ManagerName: {
                minlength: 3,
                maxlength: 20,
                required: true
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});