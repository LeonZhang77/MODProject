$(document).ready(function () {
    EditAccountInit();
});


function EditAccountInit() {
    EditAccountBind();

    EditAccountSubmitBind();
}

function EditAccountBind() {
    $("#DisplayName").bind("change", function () { validateField("DisplayName", "VN") });
    $("#RealName").bind("change", function () { validateField("RealName", "VN") });
}

function EditAccountSubmitBind() {
    $(this).bind("submit", function () { return EditAccountSubmitValidation() });
}

function EditAccountSubmitValidation() {
    var step1, step2;

    step1 = validateField("DisplayName", "VN");
    step2 = validateField("RealName", "VN");
    if (step1 && step2) {
        return true;
    }
    else {
        return false;
    }
}