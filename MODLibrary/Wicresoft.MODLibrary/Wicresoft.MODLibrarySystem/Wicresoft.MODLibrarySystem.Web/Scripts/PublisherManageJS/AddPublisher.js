$(document).ready(function () {
    AddPublisherInit();
});


function AddPublisherInit() {
    AddPublisherBind();

    AddPublisherSubmitBind();
}

function AddPublisherBind() {
    $("#PublisherName").bind("focusout", function () { multipleValidateField("PublisherName", "VN,VL,VS", 50) });
}

function AddPublisherSubmitBind() {
    $(this).bind("submit", function () { return AddPublisherSubmitValidation() });
}

function AddPublisherSubmitValidation() {
    var step1;

    step1 = multipleValidateField("PublisherName", "VN,VL,VS", 50);

    if (step1) {
        return true;
    }
    else {
        return false;
    }
}

