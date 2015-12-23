$(document).ready(function () {
    AddAuthorInit();
});


function AddAuthorInit() {
    AddAuthorBind();

    AddAuthorSubmitBind();
}

function AddAuthorBind() {
    $("#AuthorName").bind("change", function () { multipleValidateField("AuthorName", "VN,VS") });
}

function AddAuthorSubmitBind() {
    $(this).bind("submit", function () { return AddAuthorSubmitValidation() });
}

function AddAuthorSubmitValidation() {
    var step1;

    step1 = validateField("AuthorName", "VN");
    if (step1) {
        return true;
    }
    else {
        return false; 
    }
}