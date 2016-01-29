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