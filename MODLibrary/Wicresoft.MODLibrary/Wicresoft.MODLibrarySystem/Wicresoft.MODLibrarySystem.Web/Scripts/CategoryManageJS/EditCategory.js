$(document).ready(function () {
    EditCategoryInit();
});


function EditCategoryInit() {
    EditCategoryBind();

    EditCategorySubmitBind();
}

function EditCategoryBind() {
    $("#CategoryName").bind("focusout", function () { multipleValidateField("CategoryName", "VN,VS") });
}

function EditCategorySubmitBind() {
    $(this).bind("submit", function () { return EditCategorySubmitValidation() });
}

function EditCategorySubmitValidation() {
    var step1;

    step1 = multipleValidateField("CategoryName", "VN,VS");

    if (step1) {
        return true;
    }
    else {
        return false;
    }
}

