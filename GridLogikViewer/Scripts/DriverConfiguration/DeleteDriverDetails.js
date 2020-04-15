
$(".container").find('input,select').attr("disabled", true);

$('.row-remove').on("click", function () {
    $.ajax({
        type: 'POST',
        url: "/DriverConfiguration/DeleteDriverDetails",
        data: { id: $('#ModelID').removeAttr("disabled").val() },
        dataType: 'json',
        success: function (data) {
            if (data.error) {
                alert(data.message);
                if (data.success)
                    alert(data.message);
            }
        }
    });
});