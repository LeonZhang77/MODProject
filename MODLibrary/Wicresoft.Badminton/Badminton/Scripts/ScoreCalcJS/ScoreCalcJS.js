

$(function () {    
    
    //$("#StateMessageDialog").dialog(
    //{
    //    autoOpen: false,
    //    resizable: false,
    //    height: 400,
    //    width: 500,
    //    modal: true,
    //    buttons: {
    //        "OK": function () {
    //            $(this).dialog('close');
    //        }
    //    }
    //});

    $("#StateMessageDialog").modal('show', 'center');

    $("#EditStandDatePickerEdit").datetimepicker({
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