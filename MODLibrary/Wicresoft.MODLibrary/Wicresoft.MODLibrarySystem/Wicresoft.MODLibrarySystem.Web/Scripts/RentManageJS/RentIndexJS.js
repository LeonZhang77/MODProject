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
    })
});

var _id;
var _checked;
var _comments;

function rejectUserRequestDialogShow(i) {
    $("#RejectUserRequestDialog").dialog("open");
    _id = i;
}

function doRejectUserRequestWork() {
    //_checked = document.getElementById("setThisBookError").checked;
    //_comments = document.getElementById("comments").value;
    _checked =$("#setThisBookError").val();
    _comments = $("#comments").val();
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

function checkSetThisBookError() {
    var obj = document.getElementById("setThisBookError");
    if(obj.checked)
    {
        document.getElementById("comments").value = "This book is broken.";
    }
    else
    {
        document.getElementById("comments").value = "The MOD Library is arranging books.";
    }
}