﻿$(document).ready(function () {
    AddUserInit();
});


function AddUserInit() {
    AddUserBind();

    AddUserSubmitBind();
}

function AddUserBind() {
    $("#DisplayName").bind("change", function () { multipleValidateField("DisplayName", "VN,VL", 6) });

    $("#RealName").bind("change", function () { validateField("RealName", "VN") });

    $("#LoginName").bind("change", function () { validateField("LoginName", "VN") });

    $("#Password").bind("change", function () { validateField("Password", "VN") });

    $("#Email").bind("change", function () { multipleValidateField("Email", "VN,VE") });

    $("#Late_point").bind("change", function () { multipleValidateField("Late_point", "VN,VNM") });

}

function AddUserSubmitBind() {
    $(this).bind("submit", function () { return AddUserSubmitValidation() });
}

function AddUserSubmitValidation() {
    var step1, step2, step3, step4, step5, step6;

    step1 = multipleValidateField("DisplayName", "VN,VL", 6);
    step2 = validateField("RealName", "VN");
    step3 = validateField("LoginName", "VN");
    step4 = validateField("Password", "VN");
    step5 = multipleValidateField("Email", "VN,VE");
    step6 = multipleValidateField("Late_point", "VN,VNM");

    if (step1 && step2 && step3 && step4 && step5 && step6) {
        return true;
    }
    else {
        return false;
    }
}

