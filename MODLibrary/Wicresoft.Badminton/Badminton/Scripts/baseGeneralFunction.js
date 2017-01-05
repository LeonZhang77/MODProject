//Delete Item
$(function () {
    $("#dialog-confirm").dialog({
        autoOpen: false,
        resizable: false,
        height: 200,
        width: 500,
        modal: true,
        buttons: {
            "Ok": function () {
                doWork();
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
});

var _selectItem;
var _doItemType;

var bindConfrimDialog = function (obj, confrimType, btType, beforeFunc) {
    if (beforeFunc != null && beforeFunc != undefined) {
        if (!beforeFunc()) {
            return false;
        }
    }

    $("#dialog-confirm").dialog("open");
    _selectItem = $(obj);
    _doItemType = btType;

    changeTxtConfrim(confrimType);

    return false;
}

var changeTxtConfrim = function (confrimType) {
    if (confrimType == "edit") {
        $("#_txtConfirm").html("edited");
    }
}

function doWork() {
    if (_doItemType == "a") {
        var url = $(_selectItem).attr("href");
        if (url != null && url != undefined && url != '') {
            window.location = url;
        }
    }
    else {
        $(_selectItem).removeAttr("onclick");
        $(_selectItem).click();
    }
}