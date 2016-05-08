$(function () {
    $("#supportOrObjection-dialog-confirm").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 200,
        width: 500,
        modal: true,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                doSOOWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    })
});

var _selectItem;
var _doWorkType;
var _id;

var bindSOOConfirmDialog = function (obj, i, dSOOWType) {
    $("#supportOrObjection-dialog-confirm").dialog("open");
    _selectItem = $(obj);
    _id = i;
    _doSOOWorkType = dSOOWType;
}

function doSOOWork() {
    if (_doSOOWorkType == "supports") {
        var supportVal = document.getElementById("Supports");
        supportVal.innerText = supportVal.innerText / 1 + 1;
    }
    else {
        var objectionVal = document.getElementById("Objections");
        objectionVal.innerText = objectionVal.innerText / 1 - 1;
    }
    $("#SupportsActionLink").hide();
    $("#ObjectionsActionLink").hide();
}
