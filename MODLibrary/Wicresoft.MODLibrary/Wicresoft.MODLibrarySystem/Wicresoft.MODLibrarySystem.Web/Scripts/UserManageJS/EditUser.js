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

    $("#Password").bind("change", function () { validateField("Password", "VN") });
}

function EditUserSubmitBind() {
    $(this).bind("submit", function () { return EditUserSubmitValidation() });
}

function EditUserSubmitValidation() {
    var step1, step2, step3

    step1 = multipleValidateField("DisplayName", "VN,VL", 6);
    step2 = validateField("RealName", "VN");
    step3 = validateField("Password", "VN");
    
    if (step1 && step2 && step3) {
        return true;
    }
    else {
        return false;
    }
}

