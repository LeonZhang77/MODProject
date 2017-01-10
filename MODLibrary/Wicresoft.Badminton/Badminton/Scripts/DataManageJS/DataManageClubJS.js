$(function () {
    $("#DeleteClubDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doDeleteClub();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });    
});

function doDeleteClub() {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteClubURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        var approveButtonId = "clubRow" + _id;
                        $(approveButtonId).hide();
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function deleteClub(i) {
    $("#DeleteClubDialog").dialog("open");
    _id = i;
}
