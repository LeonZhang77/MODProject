$(function () {
    var mydate = new Date();
    var nowDate = mydate.getFullYear() + "/" + (mydate.getMonth() + 1) + "/" + mydate.getDate();
    $("#startDatePicker").val(nowDate);
    $("#endDatePicker").val(nowDate);

    $("#startDatePicker").datetimepicker({
        language: "zh-CN",
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        format: "yyyy/mm/dd",
    });

    $("#endDatePicker").datetimepicker({
        language: "zh-CN",
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        format: "yyyy/mm/dd",
    });
    $("#EditStartDatePickerEdit").datetimepicker({
        language: "zh-CN",
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        format: "yyyy/mm/dd",
    });
    $("#EditEndDatePickerEdit").datetimepicker({
        language: "zh-CN",
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        minView: 2,
        forceParse: 0,
        format: "yyyy/mm/dd",
    });
});

function doDeleteChampionship() {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteChampionshipURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        $("tr[data-id='championshipRow" + _id + "']").hide();
                        $("#DeleteChampionshipDialog").modal('hide');
                        ErrorTipMessage(_DelSucc,"success");
                    }
                    else {
                        ErrorTipMessage(_DelFail + data, "danger");
                        $("#DeleteChampionshipDialog").modal('hide');
                    }
                }
            })
    });
}

function deleteChampionship(i) {
    $("#DeleteChampionshipDialog").modal('show','center');
    _id = i;
}


function addChampionship() {
    $("#AddChampionshipDialog").modal('show', 'center');
}



function editChampionship(i) {
    var rowID = "#championshipRow" + i;
    var titleID = rowID + "Title";
    var ChampionshiptypeID = rowID + "ChampionType";
    var CompetingtypeID = rowID + "CompetingType";
    var StartDateID = rowID + "StartDate";
    var EndDateID = rowID + "EndDate";
    $("#EditChampion").val(i);
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

    $("#EditChampionshipDialog").modal('show', 'center');
    _id = i;
}
var status;
function SetChampionActive(i, active)
{
    _id = i;
    status = active;
    doSetChampionActive();
}

function doSetChampionActive()
{
    $(function () {
        $.ajax(
            {
                url: $("#ActiveChampionshipURL").attr("requstUrl"),
                data: { ChampionID: _id, IsActive: status },
                success: function (data) {
                    if (data) {
                        alert(_strActionIsRecord);
                        location.reload(true);
                    } else {
                        alert(_strActionIsNotRecord);
                    }
                }
            });
    });
}