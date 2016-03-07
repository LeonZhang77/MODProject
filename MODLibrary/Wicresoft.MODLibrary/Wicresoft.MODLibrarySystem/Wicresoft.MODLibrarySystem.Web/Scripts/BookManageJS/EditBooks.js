$(document).ready(function () {
    EditBookDetailInit();
});

function EditBookDetailInit() {
    EditBookDetailBind();

    EditBookDetailSubmitBind();

    ChooseDialogBind();

    OtherSpecailFunctionBind();

    LoadBookDetailData();
}

function EditBookDetailBind() {
    $("#Owner").click(function () { $("#OwnerChoose").dialog("open"); });
    $("#Owner").bind("change", function () { validateField("Owner", "VN") });

    $("#BookStatusSelected").bind("change", function () { validateField("BookStatusSelected", "VN") });

    $("#Storage_Time").bind("change", function () { validateField("Storage_Time", "VN") });
    $("#Storage_Time").datepicker({ dateFormat: browserDateFormat, altField: "#Storage_Time_errorData", altFormat: serverDateFormat, changeYear: true, changeMonth: true });
}
function EditBookDetailSubmitBind() {
    $(this).bind("submit", function () { return EditBookDetailSubmitValidation() });
}
function EditBookDetailSubmitValidation() {
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

function LoadBookDetailData() {
    var ownerValue = $("#OwnerValue").val();
    var ownerDisplay = $("#Owner").val();

    InsertLogArea(ownerValue, ownerDisplay);

    InsertLogResultArea(ownerValue, ownerDisplay);
}

function InsertLogArea(ov, od) {
    $("#OwnerChooseDisplay_Value").val(ov);
    $("#OwnerChooseDisplay_Value").attr("displayname", od);
}


function InsertLogResultArea(ov, od) {
    inputChooseDisplay(od, "OwnerChooseDisplay", ov, "SIN");
}