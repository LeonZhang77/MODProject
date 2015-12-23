$(document).ready(function () {
    EditAuthorInit();
});


function EditAuthorInit() {
    EditAuthorBind();

    EditAuthorSubmitBind();
}

function EditAuthorBind() {
    $("#AuthorName").bind("change", function () { multipleValidateField("AuthorName", "VN,VS") });
}

function EditAuthorSubmitBind() {
    $(this).bind("submit", function () { return EditAuthorSubmitValidation() });
}

function EditAuthorSubmitValidation() {
    var step1;

    step1 = validateField("AuthorName", "VN");
    if (step1) {
        return true;
    }
    else {
        return false;
    }
}