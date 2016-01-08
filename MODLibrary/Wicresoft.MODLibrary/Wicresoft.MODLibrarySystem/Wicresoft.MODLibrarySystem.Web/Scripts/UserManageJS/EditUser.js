$(document).ready(function () {
    EditUserInit();
});


function EditUserInit() {
    EditUserBind();

    EditUserSubmitBind();
}

function EditUserBind() {
    $("#DisplayName").bind("change", function () { multipleValidateField("DisplayName", "VN,VL", 6) });

    $("#RealName").bind("change", function () { validateField("RealName", "VN") });

    $("#LoginName").bind("change", function () { validateField("LoginName", "VN") });

    $("#Password").bind("change", function () { validateField("Password", "VN") });

    $("#Email").bind("change", function () { multipleValidateField("Email", "VN,VE") });
}

function EditUserSubmitBind() {
    $(this).bind("submit", function () { return EditUserSubmitValidation() });
}

function EditUserSubmitValidation() {
    var step1, step2, step3, step4, step5;

    step1 = multipleValidateField("DisplayName", "VN,VL", 6);
    step2 = validateField("RealName", "VN");
    step3 = validateField("LoginName", "VN");
    step4 = validateField("Password", "VN");
    step5 = multipleValidateField("Email", "VN,VE");

    if (step1 && step2 && step3 && step4 && step5) {
        return true;
    }
    else {
        return false;
    }
}

