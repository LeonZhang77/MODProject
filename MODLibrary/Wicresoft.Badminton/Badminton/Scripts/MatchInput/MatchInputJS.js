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

function setCompetingType()
{
    var selectedChampionshipID = $("#SelectChampionship option:selected").val();
    
    var CompetingType;
    var dropdownList = document.getElementById("ChompionshipCompetingTypeList");
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].value == selectedChampionshipID) {
            CompetingType = dropdownList.options[index].text;
            break;
        }
    }

    dropdownList = document.getElementById("SelectCompetingType");    
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].text == CompetingType) {
            dropdownList.options[index].selcted = true;
            break;
        }
    }
   
    $("#CompetingType").val(CompetingType);
    
    $("#Winner2TR").show();
    $("#Loser2TR").show();

    if (CompetingType.indexOf("Singles")!=-1)
    {
        $("#Winner2TR").hide();
        $("#Loser2TR").hide();
    }

    if (CompetingType.indexOf("Male's") != -1)
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

    if (CompetingType.indexOf("Female's") != -1)
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

    if (CompetingType.indexOf("Mixed") != -1) {
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

function doAddMatch() { }

function addMatch()
{
    $("#AddMatchDialog").dialog("open");
}