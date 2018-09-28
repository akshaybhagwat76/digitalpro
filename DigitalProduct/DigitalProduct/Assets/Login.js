$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
    });

    $("#lblError").removeClass("success").removeClass("error").text('');
    $('form').each(function () {
        $(this).find('input').keypress(function (e) {
            // Enter pressed?
            if (e.which == 10 || e.which == 13) {
                $("#btn-submit").click();
            }
        });
    });
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
        var email = $("#Email").val().trim();
        var password = $("#Password").val().trim();
        var returnUrl = $('#myForm').data("url").trim();
        if (email && !isEmail(email)) {
            $("#Email").addClass("error");
            retval = false;
        }
        if (retval) {
            var data = {
                Email: email,
                Password: password
            }
            StartProcess();
            $.ajax({
                type: "POST",
                url: "/Account/Login",
                data: { loginVM: data },
                success: function (data) {
                    if (data.status == "Fail") {
                        StopProcess();
                        $("#lblError").addClass("error").text(data.error).show();
                    }
                    else {
                        if (returnUrl != null && returnUrl != "")
                            window.location.href = $('#myForm').data("url");
                        else
                            window.location.href = '/Home/Index';
                    }
                }
            });
        }
    })
});