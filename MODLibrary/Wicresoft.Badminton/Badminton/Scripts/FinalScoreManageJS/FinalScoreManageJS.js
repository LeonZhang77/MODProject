$(function () {

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

function toAddNewBonusInfo() {
    $("#Parameters_ActionSteps").val("1");
}

function toCalcToReview() {
    $("#Parameters_ActionSteps").val("2");
}

function toAdjustAccordingToDateRange() {
    $("#Parameters_ActionSteps").val("3");
}

function toUpdateToDB() {
    $("#Parameters_ActionSteps").val("4");
}

