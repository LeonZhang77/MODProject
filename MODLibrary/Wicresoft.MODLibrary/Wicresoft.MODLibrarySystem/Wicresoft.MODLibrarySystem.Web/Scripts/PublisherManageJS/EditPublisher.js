$(document).ready(function () {
    EditPublisherInit();
});


function EditPublisherInit() {
    EditPublisherBind();

    EditPublisherSubmitBind();
}

function EditPublisherBind() {
    $("#PublisherName").bind("focusout", function () { multipleValidateField("PublisherName", "VN,VL,VS", 50) });
}

function EditPublisherSubmitBind() {
    $(this).bind("submit", function () { return EditPublisherSubmitValidation() });
}

function EditPublisherSubmitValidation() {
    var step1;

    step1 = multipleValidateField("PublisherName", "VN,VL,VS", 50);

    if (step1) {
        return true;
    }
    else {
        return false;
    }
}

