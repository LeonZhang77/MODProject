$(document).ready(function () {
    AddBookDetailInit();
});

function AddBookDetailInit() {
    AddBookDetailBind();

    AddBookDetailSubmitBind();

    ChooseDialogBind();

    OtherSpecailFunctionBind();
}

function AddBookDetailBind() {
    $("#Owner").click(function () { $("#OwnerChoose").dialog("open"); });
    $("#Owner").bind("change", function () { validateField("Owner", "VN") });

    $("#BookStatusSelected").bind("change", function () { validateField("BookStatusSelected", "VN") });

    $("#Storage_Time").bind("change", function () { validateField("Storage_Time", "VN") });
    $("#Storage_Time").datepicker({ dateFormat: browserDateFormat, altField: "#Storage_Time_errorData", altFormat: serverDateFormat, changeYear: true, changeMonth: true });
}
function AddBookDetailSubmitBind() {
    $(this).bind("submit", function () { return AddBookDetailSubmitValidation() });
}
function AddBookDetailSubmitValidation() {
    var step1, step2, step3;

    step1 = validateField("Owner", "VN");
    step2 = validateField("BookStatusSelected", "VN");
    step3 = validateField("Storage_Time", "VN");

    if (step1 && step2 && step3) {
        return true;
    }
    else {
        return false;
    }
}