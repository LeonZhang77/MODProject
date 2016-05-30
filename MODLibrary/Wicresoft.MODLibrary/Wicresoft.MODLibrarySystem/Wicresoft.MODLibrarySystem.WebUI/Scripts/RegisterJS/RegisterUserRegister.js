$(document).ready(function () {
    RegisterUserRegisterInit();
});


function RegisterUserRegisterInit() {
    RegisterUserRegisterBind();

    RegisterUserRegisterSubmitBind();
}

function RegisterUserRegisterBind() {
    $("#DisplayName").bind("change", function () { multipleValidateField("DisplayName", "VN,VL", 6) });

    $("#RealName").bind("change", function () { validateField("RealName", "VN") });

    $("#LoginName").bind("change", function () { validateField("LoginName", "VN") });

    $("#Password").bind("change", function () { validateField("Password", "VN") });

    $("#Password").bind("change", function () { ConfirmPassword() });

    $("#rePassword").bind("change", function () { ConfirmPassword() });

    $("#Email").bind("change", function () { multipleValidateField("Email", "VN,VE") });
}

function RegisterUserRegisterSubmitBind() {
    $(this).bind("submit", function () { return RegisterUserRegisterSubmitValidation() });
}

function RegisterUserRegisterSubmitValidation() {
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

function ConfirmPassword() {
    var password, repassword, errormsg;
    errormsg = "Password should be same.";
    password = $("#Password").val();
    repassword = $("#rePassword").val();

    if (password != repassword) {
        $("#rePassword_errorData").text(errormsg).show();
    }
    else { $("#rePassword_errorData").text(errormsg).hide(); }
}