$(document).ready(function () {
    AddCategoryInit();
});


function AddCategoryInit() {
    AddCategoryBind();

    AddCategorySubmitBind();
}

function AddCategoryBind() {
    $("#CategoryName").bind("focusout", function () { multipleValidateField("CategoryName", "VN,VL,VS", 20) });
}

function AddCategorySubmitBind() {
    $(this).bind("submit", function () { return AddCategorySubmitValidation() });
}

function AddCategorySubmitValidation() {
    var step1;

    step1 = multipleValidateField("CategoryName", "VN,VL,VS", 20);
    
    if (step1) {
        return true;
    }
    else {
        return false;
    }
}

