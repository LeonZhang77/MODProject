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

    alert(CompetingType);
    dropdownList = document.getElementById("SelectCompetingType");    
    for (var index = 0; index < dropdownList.options.length; index++) {
        if (dropdownList.options[index].text == CompetingType) {
            dropdownList.options[index].selcted = true;
            break;
        }
    }
    alert(CompetingType);
    $("#CompetingType").val(CompetingType);
}