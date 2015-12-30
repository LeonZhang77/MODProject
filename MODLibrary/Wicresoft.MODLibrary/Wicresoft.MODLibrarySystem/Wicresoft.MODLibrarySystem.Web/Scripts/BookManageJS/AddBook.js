$(document).ready(function () {
    AddBookInit();
});


function AddBookInit() {
    AddBookBind();

    AddBookSubmitBind();

    ChooseDialogBind();

    OtherSpecailFunctionBind();
}

function AddBookBind() {
    $("#BookName").bind("change", function () { validateField("BookName", "VN") });

    $("#ISBN").bind("change", function () { validateField("ISBN", "VN") });

    $("#Publish_Date").bind("change", function () { validateField("Publish_Date", "VN") });
    $("#Publish_Date").datepicker({ dateFormat: browserDateFormat, altField: "#Publish_Date_errorData", altFormat: serverDateFormat, changeYear: true, changeMonth: true });

    $("#Avaliable_Inventory").bind("change", function () { multipleValidateField("Avaliable_Inventory", "VN,VNM") });

    $("#Max_Inventory").bind("change", function () { multipleValidateField("Max_Inventory", "VN,VNM") });

    $("#Price_Inventory").bind("change", function () { multipleValidateField("Price_Inventory", "VN,VNM") });

    $("#PublisherName").click(function () { $("#PublisherChoose").dialog("open"); });
    $("#PublisherNameValue").bind("change", function () { validateField("PublisherNameValue", "VN") });

    $("#AuthorName").click(function () { $("#AuthourChoose").dialog("open"); });
    $("#AuthorNameValue").bind("change", function () { validateField("AuthorNameValue", "VN") });

    $("#CatagoryName").click(function () { $("#CategoryChoose").dialog("open"); });
    $("#CatagoryNameValue").bind("change", function () { validateField("CatagoryNameValue", "VN") });
}
function AddBookSubmitBind() {
    $(this).bind("submit", function () { return AddBookSubmitValidation() });
}
function AddBookSubmitValidation() {
    var step1, step2, step3, step4, step5, step6, set7, set8, set9;

    step1 = multipleValidateField("BookName", "VN");
    step2 = validateField("ISBN", "VN");
    step3 = validateField("Publish_Date", "VN");
    step4 = multipleValidateField("Avaliable_Inventory", "VN,VNM");
    step5 = multipleValidateField("Max_Inventory", "VN,VNM");
    step6 = multipleValidateField("Price_Inventory", "VN,VNM");
    step7 = validateField("PublisherNameValue", "VN");
    step8 = validateField("AuthorNameValue", "VN");
    step9 = validateField("CatagoryNameValue", "VN");

    if (step1 && step2 && step3 && step4 && step5 && step6 && step7 && step8 && step9) {
        return true;
    }
    else {
        return false;
    }
}

