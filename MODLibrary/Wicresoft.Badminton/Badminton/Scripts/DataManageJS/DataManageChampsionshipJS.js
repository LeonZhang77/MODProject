$(function () {
    $("#DeleteChampionshipDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doDeleteChampionship();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#AddChampionshipDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doAddChampionship();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });
    $("#startDatePicker").datepicker();
    $("#endDatePicker").datepicker();

    $("#EditChampionshipDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doEditChampionship();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });
    $("#EditStartDatePickerEdit").datepicker();
    $("#EditEndDatePickerEdit").datepicker();
});

function doDeleteChampionship() {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteChampionshipURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        var approveButtonId = "#championshipRow" + _id;
                        $(approveButtonId).hide();
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function deleteChampionship(i) {
    $("#DeleteChampionshipDialog").dialog("open");
    _id = i;
}

function doAddChampionship() {
    var dataString = "{" + "\"title\":\"" + $("#ChampionshipTitle").val() +
        "\",\"stratDate\":\"" + $("#startDatePicker").val() +
        "\",\"endDate\":\"" + $("#endDatePicker").val() +
        "\",\"Championtype\":\"" + $('#ChampionshipSelected option:selected').val() +
        "\",\"Competingtype\":\"" + $('#CompetingSelected option:selected').val() +
        "\"}";
    $(function () {
        $.ajax(
            {
                url: $("#AddChampionshipURL").attr("requstUrl"),
                data: { q: dataString },
                success: function (data) {
                    if (data == "true") {
                        alert(_strActionIsRecord);
                        $("#ChampionshipTitle").val("");
                        $("#startDatePicker").val("");
                        $("#endDatePicker").val("");
                        location.reload(true);
                        $("#mainTabContent").tabs({ active: 2 });
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function addChampionship() {
    $("#AddChampionshipDialog").dialog("open");
}

function doEditChampionship() {
    var dataString = "{" + "\"strID\":\"" + _id +
        "\",\"title\":\"" + $("#EditChampionshipTitle").val() +
        "\",\"stratDate\":\"" + $("#EditStartDatePickerEdit").val() +
        "\",\"endDate\":\"" + $("#EditEndDatePickerEdit").val() +
        "\",\"Championtype\":\"" + $('#EditChampionshipSelected option:selected').val() +
        "\",\"Competingtype\":\"" + $('#EditCompetingSelected option:selected').val() +
        "\"}";
    $(function () {
        $.ajax(
            {
                url: $("#EditChampionshipURL").attr("requstUrl"),
                data: { q: dataString },
                success: function (data) {
                    if (data == "true") {
                        alert(_strActionIsRecord);
                        $("#ChampionshipTitle").val("");
                        $("#startDatePicker").val("");
                        $("#endDatePicker").val("");
                        location.reload(true);
                        $("#mainTabContent").tabs({ active: 2 });
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function editChampionship(i) {
    var rowID = "#championshipRow" + i;
    var titleID = rowID + "Title";
    var ChampionshiptypeID = rowID + "ChampionType";
    var CompetingtypeID = rowID + "CompetingType";
    var StartDateID = rowID + "StartDate";
    var EndDateID = rowID + "EndDate";

    $("#EditChampionshipTitle").val($(titleID).text().trim());

    var dropdownList = document.getElementById("EditChampionshipSelected");
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].text == $(ChampionshiptypeID).text().trim()) {
            dropdownList.options[index].selected = true;
            break;
        }
    }

    dropdownList = document.getElementById("EditCompetingSelected");
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].text == $(CompetingtypeID).text().trim()) {
            dropdownList.options[index].selected = true;
            break;
        }
    }

    $("#EditStartDatePickerEdit").val($(StartDateID).text().trim());
    $("#EditEndDatePickerEdit").val($(EndDateID).text().trim());

    $("#EditChampionshipDialog").dialog("open");
    _id = i;
}
