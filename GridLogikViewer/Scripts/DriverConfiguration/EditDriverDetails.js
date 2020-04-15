$("#ddlModelList").attr("disabled", true);
$("#add_rowEdit").on("click", function () {
    // Dynamic Rows Code
    var rowCount = $('#tab_logicEdit tr').length;

    // Get max row id and set new id
    var newid = rowCount - 1;
    $.each($("#tab_logicEdit tr"), function () {
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
        $.each($("#tab_logicEdit tbody tr:nth(0) td"), function () {
            var cur_td = $(this);

            var children = cur_td.children();

            // add new td and element if it has a nane
            if ($(this).data("name") != undefined) {
                var td = $("<td></td>", {
                    "data-name": $(cur_td).data("id")
                });

                var c = $(cur_td).find($(children[0]).prop('tagName')).clone().val("");
                c.attr("name", $(cur_td).data("name") + newid);
                c.attr("id", $(cur_td).data("name") + newid);
                c.appendTo($(td));
                td.appendTo($(tr));
            } else {
                var td = $("<td></td>", {
                    'text': $('#tab_logicEdit tr').length
                }).appendTo($(tr));
            }
        });        

        // add the new row
        $(tr).appendTo($('#tab_logicEdit'));

        $(tr).find("td button.row-remove").on("click", function () {
            $(this).closest("tr").remove();
            $("#saveDetailEdit").hide();
        });

        $("#saveDetailEdit").show();
    }
});

$("#saveDetailEditj").on("click", function () {
    if ($("#ModelID").val() == null || $("#ModelID").val() == "") {
        $("#modelError").show().fadeOut(3000);
        return false;
    }
    else {
        var form = $("#formDriverConfigEdit").serialize();        
        $.ajax({
            type: 'POST',
            url: "/DriverConfiguration/EditDriverDetails",
            data: form,
            dataType: 'json',
            success: function (data) {
                if (data.error) {
                    alert(data.message);
                    if (data.success)
                        alert(data.message);
                }
            }
        });
    }
});

$("#ModelID").on("change", function () {
    if ($("#ModelID").val() == null || $("#ModelID").val() == "") {
        $("#modelError").show().fadeOut(3000);
        return false;
    }
});

function Remove(obj) {
    $("#" + obj.id).closest("tr").remove();
}