var _strActionIsRecord = "Your action is record!";
var _strActionIsNotRecord = "Something is wrong, your action isn't recored";

$(function () {
    $("#DeleteMatchDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doDeleteMatch();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#ValidMatchDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doValidMatch();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#NotValidMatchDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doNotValidMatch();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

});

var _id;


function notValidMatch(i)
{
    $("#NotValidMatchDialog").dialog("open");
    _id = i;
}

function validMatch(i) {
    $("#ValidMatchDialog").dialog("open");
    _id = i;
}

function deleteMatch(i) {
    $("#DeleteMatchDialog").dialog("open");
    _id = i;
}

function doNotValidMatch(i) {
    $(function () {
        $.ajax(
            {
                url: $("#NotValidMatchURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data) {
                        location.reload();
                    }
                    else {
                        alert(_strActionIsNotRecord);
                    }
                }
            })
    });
}

function doValidMatch(i) {
    $(function () {
        $.ajax(
            {
                url: $("#ValidMatchURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data) {
                        location.reload();
                    }
                    else {
                        alert(_strActionIsNotRecord);
                    }
                }
            })
    });
}

function doDeleteMatch(i) {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteMatchURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data) {
                        location.reload();
                    }
                    else {
                        alert(_strActionIsNotRecord);
                    }
                }
            })
    });
}