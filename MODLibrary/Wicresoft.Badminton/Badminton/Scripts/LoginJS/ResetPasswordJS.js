/*Modify password*/
$(function () {
    ModifyInit();
    ModifyBind();
});

function ModifyInit() {

    ModifySubmitBind();
}

function ModifyBind() {
    $("#ConfirmPassword").bind("change", function () { ModifySubmitValidation(); });
}

function ModifySubmitBind() {
    $(this).bind("submit", function () { return ModifySubmitValidation() });
}

function ModifySubmitValidation() {
    if ($("#NewPassword").val() == $("#ConfirmPassword").val()) {
        $("#message").empty();
        return true;
    } else {
        $("#message").css("color", "Red");
        $("#message").empty().append("两次密码不一致");
        return false;
    }
}