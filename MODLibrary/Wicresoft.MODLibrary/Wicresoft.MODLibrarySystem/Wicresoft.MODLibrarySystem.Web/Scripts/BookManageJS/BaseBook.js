function ChooseDialogBind() {
    $("#PublisherChoose").dialog({
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
                insertChooseArea("PublisherName", "PublisherChoose");
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $("#AuthourChoose").dialog({
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
                insertChooseArea("AuthorName", "AuthourChoose");
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $("#CategoryChoose").dialog({
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
                insertCategoryArea("CategoryChooseArea");
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

var insertCategoryArea = function (areaID) {
    var displayValue = "";
    var displayValueID = "";
    var checkedCount = 0;
    $("#" + areaID + " input[type=checkbox]").each(function () {
        if ($(this).is(":checked")) {
            displayValue += $(this).attr("displayname") + splitPathStr;
            displayValueID += $(this).attr("cID") + splitValueStr;
            checkedCount++;
        }
    });

    if (checkedCount > 0) {
        $("#CatagoryNameValue").val(displayValueID);
    }
    else {
        $("#CatagoryNameValue").val("");
    }

    $("#CatagoryName").val(displayValue);
}

function OtherSpecailFunctionBind() {
    $("#SearchPublisher").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: $("#SearchPublisher").attr("requstUrl"),
                dataType: "json",
                data: {
                    q: request.term
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.PublisherName + "_" + item.ID,
                            value: item.PublisherName,
                            ID: item.ID
                        }
                    }));
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            inputChooseDisplay(ui.item.value, "PublisherChooseDisplay", ui.item.ID, "SIN");
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });

    $("#SearchAuthour").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: $("#SearchAuthour").attr("requstUrl"),
                dataType: "json",
                data: {
                    q: request.term
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.AuthorName + "_" + item.ID,
                            value: item.AuthorName,
                            ID: item.ID
                        }
                    }));
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            inputChooseDisplay(ui.item.value, "AuthourChooseDisplay", ui.item.ID, "MUP");
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
    else {
        if (!checkChooseValueIsExsit($("#" + fieldid + "_Value"), chooseid)) {
            var tempStr = $(obj).html();
            tempStr += "<div>" + str + "---<span onclick=\"removeChooseResult(this,'" + chooseid + "','" + str + "','" + fieldid + "_Value')\">X</span></div>";
            $(obj).html(tempStr);
        }
    }

    inputChooseValue(str, fieldid, chooseid, type);
}

var inputChooseValue = function (str, fieldid, chooseid, type) {
    var obj = $("#" + fieldid + "_Value");
    if (type == "SIN") {
        $(obj).val(chooseid);
        $(obj).attr("displayname", str);
    }
    else {
        if (!checkChooseValueIsExsit(obj, chooseid)) {
            var tempValue = $(obj).val();
            tempValue += chooseid + splitValueStr;
            $(obj).val(tempValue);
            var tempValue = $(obj).attr("displayname");
            tempValue += str + splitDisplayStr;
            $(obj).attr("displayname", tempValue);
        }
    }
}

var checkChooseValueIsExsit = function (obj, checkID) {
    var flag = false;
    var value = $(obj).val();
    if (value.length > 0) {
        var splitValue = value.split(splitValueStr);
        for (var i = 0; i < splitValue.length; i++) {
            if (splitValue[i] == checkID) {
                flag = true;
                break;
            }
        }
    }
    return flag;
}

var removeChooseResult = function (obj, chooseid, str, valueField) {
    var eObj = $(obj);
    var obj = $("#" + valueField);
    var value = $(obj).val();
    var displayName = $(obj).attr("displayname");
    var newValue = "", newDisplayName = "";
    if (value.length > 0) {
        var splitValue = value.split(splitValueStr);
        for (var i = 0; i < splitValue.length; i++) {
            if (splitValue[i] != chooseid && splitValue[i] != undefined && splitValue[i] != "") {
                newValue += splitValue[i] + splitValueStr;
            }
        }
    }
    if (displayName.length > 0) {
        var splitValue = displayName.split(splitDisplayStr);
        for (var i = 0; i < splitValue.length; i++) {
            if (splitValue[i] != str && splitValue[i] != undefined && splitValue[i] != "") {
                newDisplayName += splitValue[i] + splitDisplayStr;
            }
        }
    }
    $(obj).val(newValue);
    $(obj).attr("displayname", newDisplayName);

    $(eObj).closest("div").remove();
}