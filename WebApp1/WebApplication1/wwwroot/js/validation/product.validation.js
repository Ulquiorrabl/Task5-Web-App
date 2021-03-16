        $(document).ready(function () {
            $("#productForm").validate({
                rules: {
                    ProductName: {
                        minlength: 3,
                        maxlength: 30,
                        required: true
                    },
                    Cost: {
                        required: true,
                        min: 1,
                        max: 500000
                    }
                },
                submitHandler: function (form) {
                    form.submit();
                }
            });
            });