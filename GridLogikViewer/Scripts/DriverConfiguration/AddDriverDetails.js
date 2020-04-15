$("#saveDetail").hide();
$(document).on("keypress", ".addressRange", function (e) {
    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        //display error message
        setTimeout(function () {
            alert("Only Numbers are allow")
        }, 0);
        return false;
    }
});
$("#saveDetail").on("click", function () {
    if ($("#MeterModel_ModelName").val() == null || $("#MeterModel_ModelName").val() == "") {
        $("#modelError").show().fadeOut(3000);
        return false;
    }
    else {
        var form = $("#formDriverConfig").serialize();
        $.ajax({
            type: 'POST',
            url: "/DriverConfiguration/SaveDriverDetails",
            data: form,
            dataType: 'json',
            success: function (data) {
                if (data.error)
                    alert(data.message);
                if (data.success) {
                    alert(data.message);
                    window.location.href = "/Index";
                }
            }
        });
    }
});
$("#MeterModel_ModelName").on("change", function () {
    if ($("#MeterModel_ModelName").val() == null || $("#MeterModel_ModelName").val() == "") {
        $("#modelError").show().fadeOut(3000);
        return false;
    }
});

$("#add_row").on("click", function () {
    // Dynamic Rows Code

    // Get max row id and set new id
    var newid = 0;
    $.each($("#tab_logic tr"), function () {
        if (parseInt($(this).data("id")) > newid) {
            newid = parseInt($(this).data("id"));
        }
    });
    if (newid != 0 && ($("#address" + newid).val() == "" || $("#parameter" + newid).val() == "" || $("#MultiplicationFactor" + newid).val() == "" ||
        $("#InstanceData_Meter_Name" + newid).val() == "" || $("#dataType" + newid).val() == "")) {
        setTimeout(function () {
            alert("Please fill all the values in previous row");
        }, 1000);
    }
    else {
        newid++;
        var tr = $("<tr></tr>", {
            id: "addr" + newid,
            "data-id": newid
        });

        // loop through each td and create new elements with name of newid
        $.each($("#tab_logic tbody tr:nth(0) td"), function () {
            var cur_td = $(this);

            var children = cur_td.children();

            // add new td and element if it has a nane
            if ($(this).data("name") != undefined) {
                var td = $("<td></td>", {
                    "data-name": $(cur_td).data("name")
                });

                var c = $(cur_td).find($(children[0]).prop('tagName')).clone().val("");
                c.attr("name", $(cur_td).data("name") + newid);
                c.attr("id", $(cur_td).data("name") + newid);
                c.appendTo($(td));
                td.appendTo($(tr));
            } else {
                var td = $("<td></td>", {
                    'text': $('#tab_logic tr').length
                }).appendTo($(tr));
            }
        });

        // add delete button and td
        /*
        $("<td></td>").append(
            $("<button class='btn btn-danger glyphicon glyphicon-remove row-remove'></button>")
                .click(function() {
                    $(this).closest("tr").remove();
                })
        ).appendTo($(tr));
        */

        // add the new row
        $(tr).appendTo($('#tab_logic'));

        $(tr).find("td button.row-remove").on("click", function () {
            $(this).closest("tr").remove();
            $("#saveDetail").hide();
        });

        $("#saveDetail").show();
    }
});

$("#add_row").trigger("click");

