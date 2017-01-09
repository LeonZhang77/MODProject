var _strActionIsRecord = "Your action is record!";

$(function () {
    $("#DeleteClubDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doDeleteClub();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

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
    $("#startDatePickerEdit").datepicker();
    $("#endDatePickerEdit").datepicker();
});

var _id;

function doDeleteClub()
{
    $(function () {
        $.ajax(
            {
                url: $("#DeleteClubURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        var approveButtonId = "clubRow" + _id;
                        $(approveButtonId).hide();                        
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function deleteClub(i)
{
    $("#DeleteClubDialog").dialog("open");
    _id = i;
}


function doDeleteChampionship() {
    alert($("#DeleteChampionshipURL").attr("requstUrl"));
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

function doAddChampionship()
{
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

function addChampionship()
{
    $("#AddChampionshipDialog").dialog("open");
}

function doEditChampionship() {
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

function editChampionship(i) {
    var rowID = "#championshipRow" + i;
    var titleID = rowID + "Title";
    var ChampionshiptypeID = rowID + "ChampionType";
    var CompetingtypeID = rowID + "CompetingType";
    var StartDateID = rowID + "StartDate";
    var EndDateID = rowID + "EndDate";
    
    $("#EditChampionshipTitle").val($(titleID).text().trim());
    
    var dropdownList = document.getElementById("EditChampionshipSelected");
    for (var i = 0; i < dropdownList.options.length; i++)
    {
        if (dropdownList.options[i].text == $(ChampionshiptypeID).text().trim())
        {
            dropdownList.options[i].selected = true;
            break;
        }
    }

    dropdownList = document.getElementById("EditCompetingSelected");
    for (var i = 0; i < dropdownList.options.length; i++) {
        if (dropdownList.options[i].text == $(CompetingtypeID).text().trim()) {
            dropdownList.options[i].selected = true;
            break;
        }
    }

    $("#startDatePickerEdit").val($(StartDateID).text().trim());
    $("#endDatePickerEdit").val($(EndDateID).text().trim());

    $("#EditChampionshipDialog").dialog("open");
    _id = i;
}
