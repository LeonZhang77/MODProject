$(document).ready(function () {
    IndexRegisterInit();
});


function IndexRegisterInit() {
    IndexRegisterBind();

    IndexRegisterSubmitBind();
}

function IndexRegisterBind() {
    $("#Email").bind("change", function () { multipleValidateField("Email", "VN,VE") });
}

function IndexRegisterSubmitBind() {
    $(this).bind("submit", function () { return IndexRegisterSubmitValidation() });
}

function IndexRegisterSubmitValidation() {
    var step1;

    step1 = multipleValidateField("Email", "VN,VE");
    if (step1) {
        return true;
    }
    else {
        return false;
    }
}
