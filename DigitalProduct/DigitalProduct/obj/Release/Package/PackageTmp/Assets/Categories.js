$(document).ready(function () {

});

$("#myTable").on("click", "a.btn-delete", function () {
    var id = $(this).data('id');
    $('#deleteModal').data('id', id).modal('show');
    $('#deleteModal').modal('show');
});

$('#delete-btn').click(function () {
    var id = $('#deleteModal').data('id');
    $.ajax({
        type: "POST",
        url: "/Categories/Delete",
        data: { id: id },
        success: function (response) {
            if (response.status != "Fail") {
                funToastr(true, response.msg);
                $('#deleteModal').modal('hide');
                location.reload();
            }
            else {
                $('#deleteModal').modal('hide');
                funToastr(false, response.error);
            }
        },
        error: function (error) {
            toastr.error(error)
        }
    });
});