$(document).ready(function () {
    var $form_wrapper = $('#form_wrapper');
    _loginForm = $form_wrapper.children('form.login');
    _registerForm = $form_wrapper.children('form.register');
    _registerUserForm = $form_wrapper.children('form.registerUser');
    LoginInit();
    IndexRegisterInit();
    RegisterUserRegisterInit();
    
    
});

var _loginForm;
var _registerForm;
var _registerUserForm;

function LoginInit() {
    LoginBind();
    LoginSubmitBind();
}

function LoginBind() {
    $("#LoginIndexModel_UserName").bind("change", function () { validateField("LoginIndexModel_UserName", "VN") });
    $("#LoginIndexModel_Password").bind("change", function () { validateField("LoginIndexModel_Password", "VN") });
}

function LoginSubmitBind() {
    _loginForm.bind("submit", function () { return LoginSubmitValidation() });
}

function LoginSubmitValidation() {
    var step1, step2;

    step1 = validateField("LoginIndexModel_UserName", "VN");
    step2 = validateField("LoginIndexModel_Password", "VN");
    if (step1 && step2) {
        return true;
    }
    else {
        return false;
    }
}

function IndexRegisterInit() {
    IndexRegisterBind();

    IndexRegisterSubmitBind();
}

function IndexRegisterBind() {
    $("#RegisterIndexModel_Email").bind("change", function () { multipleValidateField("RegisterIndexModel_Email", "VN,VE") });
}

function IndexRegisterSubmitBind() {
    _registerForm.bind("submit", function () { return IndexRegisterSubmitValidation() });
}

function IndexRegisterSubmitValidation() {
    var step1;

    step1 = multipleValidateField("RegisterIndexModel_Email", "VN,VE");
    if (step1) {
        return true;
    }
    else {
        return false;
    }
}

function RegisterUserRegisterInit() {
    RegisterUserRegisterBind();

    RegisterUserRegisterSubmitBind();
}

function RegisterUserRegisterBind() {
    $("#RegisterModel_DisplayName").bind("change", function () { multipleValidateField("RegisterModel_DisplayName", "VN,VL", 6) });

    $("#RegisterModel_RealName").bind("change", function () { validateField("RegisterModel_RealName", "VN") });

    $("#RegisterModel_LoginName").bind("change", function () { validateField("RegisterModel_LoginName", "VN") });

    $("#RegisterModel_Password").bind("change", function () { validateField("RegisterModel_Password", "VN") });

    $("#RegisterModel_Password").bind("change", function () { ConfirmPassword() });

    $("#rePassword").bind("change", function () { ConfirmPassword() });

    $("#RegisterModel_Email").bind("change", function () { multipleValidateField("RegisterModel_Email", "VN,VE") });
}

function RegisterUserRegisterSubmitBind() {
    _registerUserForm.bind("submit", function () { return RegisterUserRegisterSubmitValidation() });
}

function RegisterUserRegisterSubmitValidation() {
    var step1, step2, step3, step4, step5;

    step1 = multipleValidateField("RegisterModel_DisplayName", "VN,VL", 6);
    step2 = validateField("RegisterModel_RealName", "VN");
    step3 = validateField("RegisterModel_LoginName", "VN");
    step4 = validateField("RegisterModel_Password", "VN");
    step5 = multipleValidateField("RegisterModel_Email", "VN,VE");

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
    password = $("#RegisterModel_Password").val();
    repassword = $("#rePassword").val();

    if (password != repassword) {
        $("#rePassword_errorData").text(errormsg).show();
    }
    else {
        $("#rePassword_errorData").text(errormsg).hide();
    }
}