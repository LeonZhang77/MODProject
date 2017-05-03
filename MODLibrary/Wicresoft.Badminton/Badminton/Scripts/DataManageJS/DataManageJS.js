var _strActionIsRecord = "Your action is record!";
var _DelFail = "删除失败！";
var _DelSucc = "删除成功！";
var _BatchAddFail = "批量添加失败！";
var _RepeatError = "该名称已存在！";

var _id;


function ErrorTipMessage(message,statue)
{
    new $.zui.Messager('提示消息：'+message, {
        icon: 'bell',
        type: statue
    }).show();

}

function CheckNameRepeat(Url, ID, Name, IDName, SaveButton) {
    var statue = false;
    $.ajax({
        url: Url,
        data: { ID: ID, Name: Name },
        dataType: 'json',
        type: 'get',
        async: 'false',
        success: function (data) {
            if (data) {
                statue = true;
                $(IDName).text("");
                $(SaveButton).attr("disabled", false);
            }
            else {
                $(IDName).text(_RepeatError);
                $(SaveButton).attr("disabled", true);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $(IDName).text(XMLHttpRequest.readyState + "," + textStatus + "," + errorThrown);
            $(SaveButton).attr("disabled", true);
        }
    });
    return statue;
}

function CheckNameInput() {

    var MemberNameURL = $("#CheckMemberNameURL").attr("requstUrl");
    var ChampionNameURL = $("#CheckChampionNameURL").attr("requstUrl");
    var ClubNameURL = $("#CheckClubNameURL").attr("requstUrl");

    $("#MemberName").blur(function () {
        $("#Addtip").text("");
        CheckNameRepeat(MemberNameURL, -1, $.trim($("#MemberName").val()), "#Addtip", "#memberSave");
    });

    $("#EditMemberName").blur(function () {
        $("#Edittip").text("");
        CheckNameRepeat(MemberNameURL, $("#EditMemberID").val(), $.trim($("#MemberName").val()), "#Edittip", "#memberEditSave");
    });

    $("#Title").blur(function () {
        $("#ChampionAddTip").text("");
        CheckNameRepeat(ChampionNameURL, -1, $.trim($("#Title").val()), "#ChampionAddTip", "#championSave");
    });

    $("#EditChampionshipTitle").blur(function () {
        $("#ChampionEditTip").text("");
        CheckNameRepeat(ChampionNameURL, $("#EditChampion").val(), $.trim($("#EditChampionshipTitle").val()), "#ChampionEditTip", "#championEditSave");
    });

    $("#ClubName").blur(function () {
        $("#ClubAddTip").text("");
        CheckNameRepeat(ClubNameURL, -1, $.trim($("#ClubName").val()), "#ClubAddTip", "#clubSave");
    });

    $("#EditClubName").blur(function () {
        $("#ClubEditTip").text("");
        CheckNameRepeat(ClubNameURL, $("#EditID").val(), $.trim($("#EditClubName").val()), "#ClubEditTip", "#clubEditSave");
    });
}