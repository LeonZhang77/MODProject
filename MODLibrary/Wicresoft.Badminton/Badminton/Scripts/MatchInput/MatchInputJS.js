$(function(){
    $("#MatchDate").datepicker();
    $("#inputFeild").hide();

    $("#AddMatchDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doAddMatch();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

});

var _CompetingType;
var _ChampionshipID;
//var _InputPersonID;
//var _Winner1ID;
//var _Winner2ID;
//var _Loser1ID;
//var _Loser2ID;

function setCompetingType()
{
    _ChampionshipID = $("#SelectChampionship option:selected").val();
    
    
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
   
    $("#CompetingType").val(_CompetingType);
    
    $("#Winner2TR").show();
    $("#Loser2TR").show();

    if (_CompetingType.indexOf("Singles")!=-1)
    {
        $("#Winner2TR").hide();
        $("#Loser2TR").hide();
    }

    if (_CompetingType.indexOf("Male's") != -1)
    {
        $("#Winner1List option").remove();
        $("#Winner2List option").remove();
        $("#Loser1List option").remove();
        $("#Loser2List option").remove();
        $("#MaleMemberList option").clone(true).appendTo("#Winner1List");
        $("#MaleMemberList option").clone(true).appendTo("#Winner2List");
        $("#MaleMemberList option").clone(true).appendTo("#Loser1List");
        $("#MaleMemberList option").clone(true).appendTo("#Loser2List");
    }

    if (_CompetingType.indexOf("Female's") != -1)
    {
        $("#Winner1List option").remove();
        $("#Winner2List option").remove();
        $("#Loser1List option").remove();
        $("#Loser2List option").remove();
        $("#FemaleMemberList option").clone(true).appendTo("#Winner1List");
        $("#FemaleMemberList option").clone(true).appendTo("#Winner2List");
        $("#FemaleMemberList option").clone(true).appendTo("#Loser1List");
        $("#FemaleMemberList option").clone(true).appendTo("#Loser2List");
    }

    if (_CompetingType.indexOf("Mixed") != -1) {
        $("#Winner1List option").remove();
        $("#Winner2List option").remove();
        $("#Loser1List option").remove();
        $("#Loser2List option").remove();
        $("#MemberList option").clone(true).appendTo("#Winner1List");
        $("#MemberList option").clone(true).appendTo("#Winner2List");
        $("#MemberList option").clone(true).appendTo("#Loser1List");
        $("#MemberList option").clone(true).appendTo("#Loser2List");
    }

    $("#inputFeild").show();
}

//function setInputPersonID() {
//    _InputPersonID = $("#InputPersonList option:selected").val();
//}

//function setWinner1ID()
//{
//    _Winner1ID = $("#Winner1List option:selected").val();
//}

//function setWinner2ID() {
//    _Winner2ID = $("#Winner2List option:selected").val();
//}

//function setLoser1ID() {
//    _Loser1ID = $("#Loser1List option:selected").val();
//}

//function setLoser2ID() {
//    _Loser2ID = $("#Loser2List option:selected").val();
//}

function doAddMatch()
{
    //var dataString = "{" + "\"InputPersonID\":\"" + _InputPersonID +
    //    "\",\"Championship\":\"" + _ChampionshipID +
    //    "\",\"Winner1ID\":\"" + _Winner1ID +
    //    "\",\"Loser1ID\":\"" + _Loser1ID +
    //    "\",\"WinnerPoints\":\"" + $('#WinnerPoints').val() +
    //    "\",\"LoserPoints\":\"" + $('#LoserPoints').val() +
    //    "\",\"MatchDate\":\"" + $('#MatchDate').val();
    var dataString = "{" + "\"InputPersonID\":\"" + $("#InputPersonList option:selected").val() +
       "\",\"Championship\":\"" + $("#SelectChampionship option:selected").val() +
       "\",\"Winner1ID\":\"" + $("#Winner1List option:selected").val() +
       "\",\"Loser1ID\":\"" + $("#Loser1List option:selected").val() +
       "\",\"WinnerPoints\":\"" + $('#WinnerPoints').val() +
       "\",\"LoserPoints\":\"" + $('#LoserPoints').val() +
       "\",\"MatchDate\":\"" + $('#MatchDate').val();

    if (_CompetingType.indexOf("Singles") != -1) {
        dataString = dataString + "\"}";
    }
    else
    {
        dataString = dataString +
       "\",\"Winner2ID\":\"" + $("#Winner2List option:selected").val() +
       "\",\"Loser2ID\":\"" + $("#Loser2List option:selected").val() +
       "\"}";
    }

    $(function () {
        $.ajax(
            {
                url: $("#AddMatchURL").attr("requstUrl"),
                data: { q: dataString },
                success: function (data) {
                    if (data == "true") {
                        alert(_strActionIsRecord);
                        location.reload(true);                        
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function addMatch()
{
    $("#AddMatchDialog").dialog("open");
}