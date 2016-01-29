function ChooseDialogBind() {
    $("#OwnerChoose").dialog({
        autoOpen: false,
        height: 400,
        width: 700,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        },
        modal: true,
        buttons: {
            "OK": function () {
                insertChooseArea("Owner", "OwnerChoose");
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

function OtherSpecailFunctionBind() {
    $("#SearchOwner").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: $("#SearchOwner").attr("requstUrl"),
                dataType: "json",
                data: {
                    q: request.term
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.DisplayName + "_" + item.ID,
                            value: item.DisplayName,
                            ID: item.ID
                        }
                    }));
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            inputChooseDisplay(ui.item.value, "OwnerChooseDisplay", ui.item.ID, "SIN");
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
}

var insertChooseArea = function (UIfieldID, fieldid) {
    var obj = $("#" + fieldid + "Display_Value");
    var value = $(obj).val();
    var displayName = $(obj).attr("displayname");
    $("#" + UIfieldID).val(displayName.replace(eval("/" + splitDisplayStr + "/g"), splitDisplayUIStr));
    $("#" + UIfieldID + "Value").val(value);
}

var inputChooseDisplay = function (str, fieldid, chooseid, type) {
    var obj = $("#" + fieldid);
    if (type == "SIN") {
        $(obj).html(str);
    }

    inputChooseValue(str, fieldid, chooseid, type);
}

var inputChooseValue = function (str, fieldid, chooseid, type) {
    var obj = $("#" + fieldid + "_Value");
    if (type == "SIN") {
        $(obj).val(chooseid);
        $(obj).attr("displayname", str);
    }
}