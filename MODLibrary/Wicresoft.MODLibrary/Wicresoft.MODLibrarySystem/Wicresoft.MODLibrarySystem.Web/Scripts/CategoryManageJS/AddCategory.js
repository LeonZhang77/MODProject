$(document).ready(function () {
    AddCategoryInit();
});


function AddUserInit() {
    AddCategoryBind();

    AddCategorySubmitBind();
}

function AddCategoryBind() {
    $("#DisplayName").bind("change", function () { multipleValidateField("DisplayName", "VN,VL", 6) });

    $("#RealName").bind("change", function () { validateField("RealName", "VN") });

    $("#LoginName").bind("change", function () { validateField("LoginName", "VN") });

    $("#Password").bind("change", function () { validateField("Password", "VN") });

    $("#Email").bind("change", function () { multipleValidateField("Email", "VN,VE") });

    $("#Late_point").bind("change", function () { multipleValidateField("Late_point", "VN,VNM") });

    $("#TestDateTime").bind("change", function () { validateField("TestDateTime", "VN") });
    $("#TestDateTime").datepicker({ dateFormat: browserDateFormat, altField: "#TestDateTime_errorData", altFormat: serverDateFormat, changeYear: true, changeMonth: true });

    $("#TestDropDownList").bind("change", function () { validateField("TestDropDownList", "VN") });
}

function AddCategorySubmitBind() {
    $(this).bind("submit", function () { return AddUserSubmitValidation() });
}

function AddCategorySubmitValidation() {
    var step1, step2, step3, step4, step5, step6, step7, step8;

    step1 = multipleValidateField("DisplayName", "VN,VL", 6);
    step2 = validateField("RealName", "VN");
    step3 = validateField("LoginName", "VN");
    step4 = validateField("Password", "VN");
    step5 = multipleValidateField("Email", "VN,VE");
    step6 = multipleValidateField("Late_point", "VN,VNM");
    step7 = validateField("TestDateTime", "VN");
    step8 = validateField("TestDropDownList", "VN");

    if (step1 && step2 && step3 && step4 && step5 && step6 && step7 && step8) {
        return true;
    }
    else {
        return false;
    }
}

