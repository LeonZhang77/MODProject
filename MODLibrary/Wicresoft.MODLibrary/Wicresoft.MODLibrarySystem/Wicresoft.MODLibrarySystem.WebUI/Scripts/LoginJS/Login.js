$(document).ready(function () {
    LoginInit();
});


function LoginInit() {
    LoginBind();

    LoginSubmitBind();
}

function LoginBind() {
    $("#UserName").bind("change", function () { validateField("UserName", "VN") });
    $("#Password").bind("change", function () { validateField("Password", "VN") });
}

function LoginSubmitBind() {
    $(this).bind("submit", function () { return LoginSubmitValidation() });
}

function LoginSubmitValidation() {
    var step1, step2;

    step1 = validateField("UserName", "VN");
    step2 = validateField("Password", "VN");
    if (step1 && step2) {
        return true;
    }
    else {
        return false;
    }
}