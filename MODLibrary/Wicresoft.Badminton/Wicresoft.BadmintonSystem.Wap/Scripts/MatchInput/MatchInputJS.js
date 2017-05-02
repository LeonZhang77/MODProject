var _Winner1Winner2SameError = "胜者1和胜者2不能为同一人!";
var _Winner1Loser1SameError = "胜者1和败者1不能为同一人!";
var _Winner1Loser2SameError = "胜者1和败者2不能为同一人!";
var _Winner2Loser1SameError = "胜者2和败者1不能为同一人!";
var _Winner2Loser2SameError = "胜者2和败者2不能为同一人!";
var _Loser1Loser2SameError = "败者1和败者2不能为同一人!";
var _id;

$(function () {
    $("#MatchDate").datetimepicker({
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

    var mydate = new Date();
    var nowDate = mydate.getFullYear() + "/0" + (mydate.getMonth() + 1) + "/0" + mydate.getDate();
    $("#MatchDate").val(nowDate);

    $("#inputFeild").hide();

    $("form").submit(function () {
        return submitVeiry();
    });

    setCompetingType();
});

var _CompetingType;
var _ChampionshipID;

function setCompetingType() {
    _ChampionshipID = $("#ChampionshipID option:selected").val();


    var dropdownList = document.getElementById("ChompionshipCompetingTypeList");
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].value == _ChampionshipID) {
            _CompetingType = dropdownList.options[index].text;
            break;
        }
    }

    dropdownList = document.getElementById("SelectCompetingType");
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].text == _CompetingType) {
            dropdownList.options[index].selcted = true;
            break;
        }
    }

    $("#CompetingType").empty().text(_CompetingType);
    $("#Winner2TR").attr("required", "required");
    $("#Loser2TR").attr("required", "required");
    $("#Winner2TR").show();
    $("#Loser2TR").show();

    if (_CompetingType.indexOf("Singles") != -1) {
        $("#Winner2TR").removeAttr("required");
        $("#Loser2TR").removeAttr("required");
        $("#Winner2TR").hide();
        $("#Loser2TR").hide();
    }

    if (_CompetingType.indexOf("Male's") != -1) {
        $("#Winner1ID option").remove();
        $("#Winner2ID option").remove();
        $("#Loser1ID option").remove();
        $("#Loser2ID option").remove();
        $("#MaleMemberList option").clone(true).appendTo("#Winner1ID");
        $("#MaleMemberList option").clone(true).appendTo("#Winner2ID");
        $("#MaleMemberList option").clone(true).appendTo("#Loser1ID");
        $("#MaleMemberList option").clone(true).appendTo("#Loser2ID");
    }

    if (_CompetingType.indexOf("Female's") != -1) {
        $("#Winner1ID option").remove();
        $("#Winner2ID option").remove();
        $("#Loser1ID option").remove();
        $("#Loser2ID option").remove();
        $("#FemaleMemberList option").clone(true).appendTo("#Winner1ID");
        $("#FemaleMemberList option").clone(true).appendTo("#Winner2ID");
        $("#FemaleMemberList option").clone(true).appendTo("#Loser1ID");
        $("#FemaleMemberList option").clone(true).appendTo("#Loser2ID");
    }

    if (_CompetingType.indexOf("Mixed") != -1) {
        $("#Winner1ID option").remove();
        $("#Winner2ID option").remove();
        $("#Loser1ID option").remove();
        $("#Loser2ID option").remove();
        $("#MemberList option").clone(true).appendTo("#Winner1ID");
        $("#MemberList option").clone(true).appendTo("#Winner2ID");
        $("#MemberList option").clone(true).appendTo("#Loser1ID");
        $("#MemberList option").clone(true).appendTo("#Loser2ID");
    }

    $("#inputFeild").show();
}
function submitVeiry() {
    if (_CompetingType.indexOf("Singles") != -1) {
        if ($("#Winner1ID").val() == $("#Loser1ID").val()) { TipErrorMessage(_Winner1Loser1SameError); return false; }
    }
    else {
        if ($("#Winner1ID").val() == $("#Loser1ID").val()) { TipErrorMessage(_Winner1Loser1SameError); return false; }
        if ($("#Winner1ID").val() == $("#Winner2ID").val()) { TipErrorMessage(_Winner1Winner2SameError); return false; }
        if ($("#Winner1ID").val() == $("#Loser1ID").val()) { TipErrorMessage(_Winner1Loser1SameError); return false; }
        if ($("#Winner1ID").val() == $("#Loser2ID").val()) { TipErrorMessage(_Winner1Loser2SameError); return false; }
        if ($("#Winner2ID").val() == $("#Loser1ID").val()) { TipErrorMessage(_Winner2Loser1SameError); return false; }
        if ($("#Winner2ID").val() == $("#Loser2ID").val()) { TipErrorMessage(_Winner2Loser2SameError); return false; }
        if ($("#Loser1ID").val() == $("#Loser2ID").val()) { TipErrorMessage(_Loser1Loser2SameError); return false; }
    }

    return true;
}
function TipErrorMessage(message) {
    new $.zui.Messager('提示消息：' + message, {
        type: 'danger',
        close: false
    }).show();
}
function addMatch() {
    $("#AddMatchDialog").dialog("open");
}

function EditMatch(i) {
    var MatchRowID = "#MatchRow" + i;
    var MatchInputPersonName = MatchRowID + "PersonName";
    var MatchDate = MatchRowID + "Date";
    var Championship = MatchRowID + "Championship";
    var WinPoints = MatchRowID + "WinPoints";
    var LosePoints = MatchRowID + "LosePoints";
    var Winner1Name = MatchRowID + "Winner1Name";
    var Winner2Name = MatchRowID + "Winner2Name";
    var Loser1Name = MatchRowID + "Loser1Name";
    var Loser2Name = MatchRowID + "Loser2Name";
    var MatchType = MatchRowID + "MatchType";

    $("#MatchRow" + _id).css("color", "black");
    $(MatchRowID).css("color", "red");

    $("#EditNum").text($("#requestid").text());
    $("#MatchDate").val($(MatchDate).text().trim());
    $("#LoserPoints").val($(LosePoints).text().trim());
    $("#WinnerPoints").val($(WinPoints).text().trim());

    $("#SelectChampionship option").each(function () {
        if ($(this).text() == $(Championship).text().trim()) {
            $(this).attr("selected", true);
        }

    });

    $("#SelectMatchType option").each(function () {
        if ($(this).text() == $(MatchType).text().trim()) {
            $(this).attr("selected", true);
        }

    });

    setCompetingType();

    $("#InputPersonList option").each(function () {
        if ($(this).text() == $(Winner1Name).text().trim()) {
            $("#Winner1List").find("option[Value='" + $(this).val() + "']").attr("selected", true);
        }
        if ($(this).text() == $(Loser1Name).text().trim()) {
            $("#Loser1List").find("option[Value='" + $(this).val() + "']").attr("selected", true);
        }
        if ($(this).text() == $(MatchInputPersonName).text().trim()) {
            $("#InputPersonList").find("option[Value='" + $(this).val() + "']").attr("selected", true);
        }
        if ($(this).text() == $(Winner2Name).text().trim()) {
            $("#Winner2List").find("option[Value='" + $(this).val() + "']").attr("selected", true);
        }
        if ($(this).text() == $(Loser2Name).text().trim()) {
            $("#Loser2List").find("option[Value='" + $(this).val() + "']").attr("selected", true);
        }

    });
    $("#EditMatchDialog").show();
    _id = i;


}