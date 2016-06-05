$(function () {
    $("#RejectUserRequestDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doRejectUserRequestWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#ApproveUserRequestDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doApproveUserRequestWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#TakeWaitingForTakeDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doTakeWaitingForTakeWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#RevokeWaitingForTakeDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doRevokeWaitingForTakeWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#ReturnWaitingForReturnDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doReturnWaitingForReturnWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    

});

var _id;
var _checked;
var _comments;

function rejectUserRequestDialogShow(i) {
    $("#RejectUserRequestDialog").dialog("open");
    _id = i;
}

function approveUserRequestButton(i) {
    $("#ApproveUserRequestDialog").dialog("open");
    _id = i;
}

function takeWaitingForTakeButton(i)
{
    $("#TakeWaitingForTakeDialog").dialog("open");
    _id = i;
}

function revokeWaitingForTakeButton(i) {
    $("#RevokeWaitingForTakeDialog").dialog("open");
    _id = i;
}

function returnWaitingForReturnButton(i) {
    $("#ReturnWaitingForReturnDialog").dialog("open");
    _id = i;
}

function doRejectUserRequestWork() {
    _checked =$("#setThisBookError").val();
    _comments = $("#commentsOfReject").val();
    var dataString = "{" + "\"idStr\":\"" + _id + "\",\"isChecked\":\"" + _checked + "\",\"comments\":\"" + _comments + "\"}";
    $(function () {
        $.ajax(
            {
                url: $("#RejectUserRequestURL").attr("requstUrl"),
                data: {q:dataString},
                success: function (data) {
                    if (data == "true") {
                        var approveButtonId = "#approveuserRequestButton" + _id;
                        var rejectButtonId = "#rejectuserRequestButton" + _id;
                        $(approveButtonId).hide();
                        $(rejectButtonId).hide();
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function doApproveUserRequestWork()
{
    _comments = $("#commentsOfApprove").val();
    var dataString = "{" + "\"idStr\":\"" + _id + "\",\"comments\":\"" + _comments + "\"}";
    $(function () {
        $.ajax(
            {
                url: $("#ApproveUserRequestURL").attr("requstUrl"),
                data: { q: dataString },
                success: function (data) {
                    if (data == "true") {
                        alert("Your action is record!");
                        document.execCommand('Refresh');
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function doTakeWaitingForTakeWork() {
    $(function () {
        $.ajax(
            {
                url: $("#TakeWaitingForTakeURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        alert("Your action is record!");
                        document.execCommand('Refresh');
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function doRevokeWaitingForTakeWork() {
    $(function () {
        $.ajax(
            {
                url: $("#RevokeWaitingForTakeURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        var takeButtonId = "#takeWaitingForTakeButton" + _id;
                        var revokeButtonId = "#revokeWaitingForTakeButton" + _id;
                        $(takeButtonId).hide();
                        $(revokeButtonId).hide();
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function doReturnWaitingForReturnWork() {
    $(function () {
        $.ajax(
            {
                url: $("#ReturnWaitingForReturnURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        alert("Your action is record!");
                        document.execCommand('Refresh');
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function checkSetThisBookError() {
    var obj =$("#setThisBookError").val();
    if(obj.checked)
    {
        document.getElementById("commentsOfReject").value = "This book is broken.";
    }
    else
    {
        document.getElementById("commentsOfReject").value = "The MOD Library is arranging books.";
    }
}