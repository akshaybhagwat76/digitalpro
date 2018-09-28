$(document).ready(function () {
    $("#lblError").removeClass("success").removeClass("error").text('');

    $("#btn-submit").on("click", function () {
        $("#lblError").removeClass("success").removeClass("error").text('');
        var retval = true;
        $("#myForm .required").each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                retval = false;
            }
            else {
                $(this).removeClass("error");
            }
        });

        if (retval) {
            var data = {
                id: $("#Id").val().trim(),
                CategoryName: $("#CategoryName").val().trim(),
                Status: $("#Status").is(':checked')
            }
            StartProcess();
            $.ajax({
                type: "POST",
                url: "/Categories/AddOrUpdateCategories",
                data: { categoryVM: data },
                success: function (data) {
                    if (data.status == "Fail") {
                        StopProcess();
                        $("#lblError").addClass("error").text(data.error).show();
                    }
                    else {
                        window.location.href = '/Categories/Index'
                    }
                }
            });
        }
    })
});