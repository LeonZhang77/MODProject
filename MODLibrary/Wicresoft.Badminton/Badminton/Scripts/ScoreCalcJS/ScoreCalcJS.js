

$(function () {    
    
    $("#StateMessageDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#StateMessageDialog").dialog("open");

    $("#EditStandDatePickerEdit").datepicker();
})

function toCalcToReview() {
    $("#Parameters_ActionSteps").val("1");
}

function toAdjustAccordingToDateRange() {
    $("#Parameters_ActionSteps").val("2");    
}

function toSaveScoreEntry() {
    $("#Parameters_ActionSteps").val("3");    
}

function toOnlyAdjustAccordingToDateRange() {
    $("#Parameters_ActionSteps").val("4");    
}

function toSaveUpdateMemberScore() {
    $("#Parameters_ActionSteps").val("5");   
}