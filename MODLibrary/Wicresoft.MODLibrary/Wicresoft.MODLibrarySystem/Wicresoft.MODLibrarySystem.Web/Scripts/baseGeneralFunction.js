//Delete Item
$(function () {
    $("#dialog-confirm").dialog({
        autoOpen: false,
        resizable: false,
        height: 200,
        width: 500,
        modal: true,
        buttons: {
            "Delete": function () {
                goDelete();
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
});

var _selectItem;

var deleteConfrimDialog = function (obj) {
    $("#dialog-confirm").dialog("open");
    _selectItem = $(obj);
    return false;
}

function goDelete() {
    var url = $(_selectItem).attr("href");
    if (url != null && url != undefined && url != '') {
        window.location = url;
    }
}

//Edit Item

$(function () {
    $("#dialog-edit-confirm").dialog({
        autoOpen: false,
        resizable: false,
        height: 200,
        width: 500,
        modal: true,
        buttons: {
            "Edit": function () {
                goEdit();
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
});

var theFORM;

var editConfrimDialog = function (obj) {
    $("#dialog-edit-confirm").dialog("open");
    theFORM= $(obj).closest("form");
    return false;
}

function goEdit() {
    theFORM.submit();
}
