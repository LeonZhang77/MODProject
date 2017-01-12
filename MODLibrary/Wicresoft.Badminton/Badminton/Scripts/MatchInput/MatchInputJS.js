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
    
    alert(selectedChampionshipID);
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
   // if (CompetingType == "" || CompetingType == "" || CompetingType == "" || CompetingType.contains("Singles"))
    if (CompetingType.indexOf("Singles")!=-1)
    {
        $("#Winner2TR").hide();
        $("#Loser2TR").hide();
    }

    if (CompetingType.indexOf("Male's") != -1)
    {
      
    }

    if (CompetingType.indexOf("Female's") != -1)
    {
        $("Winner1List").empty();


    }

    $("#inputFeild").show();
}

function doAddMatch() { }

function addMatch()
{
    $("#AddMatchDialog").dialog("open");
}