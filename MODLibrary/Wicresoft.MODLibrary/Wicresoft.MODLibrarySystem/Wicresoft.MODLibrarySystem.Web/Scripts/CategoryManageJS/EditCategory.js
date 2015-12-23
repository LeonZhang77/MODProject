$(document).ready(function () {
    EditCategoryInit();
});


function EditCategoryInit() {
    EditCategoryBind();

    EditCategorySubmitBind();
}

function EditCategoryBind() {
    $("#CategoryName").bind("focusout", function () { multipleValidateField("CategoryName", "VN,VL,VS", 20) });
}

function EditCategorySubmitBind() {
    $(this).bind("submit", function () { return EditCategorySubmitValidation() });
}

function EditCategorySubmitValidation() {
    var step1;

    step1 = multipleValidateField("CategoryName", "VN,VL,VS", 20);

    if (step1) {
        return true;
    }
    else {
        return false;
    }
}

