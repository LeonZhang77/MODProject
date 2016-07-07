var loginexist = true;

$(document).ready(function () {
    $("#register-next").bind("click", registerEmail);
    $("#register-name").bind("blur", registerName);
    $("#password").bind("blur", validateNull);
    $("#display-name").bind("blur", validateNull);
    $("#real-name").bind("blur", validateNull);
})

function registerEmail() {
    var href = "Register/validateEmail";
    var email = $("#register-email").val();
    if (email == "") {
        $("#email-exist").hide();
        $("#email-null").show();
    }
    else {
        $("#email-null").hide();
        $.ajax(
            {
                type: "post",
                url: href,
                data: { email: email },
                datatype: "text",
                success: function (responsedata) {
                    if (responsedata == "False") {
                        emailExist = false;
                        $("#email-exist").hide();
                            $("#register-next").slideToggle();
                            $(".register1").slideToggle();
                            $(".register2").slideToggle();
                    }
                    else {
                        emailExist = true;
                        $("#email-exist").show();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            }
        );
    }
    return false;
}

function registerName() {
    var href = "Register/validateName";
    var loginName = $("#register-name").val();
    if (loginName == "") {
        $("#name-exist").hide();
        $("#name-null").show();
    }
    else {
        $("#name-null").hide();
        $.ajax(
        {
            type: "post",
            url: href,
            data: { name: loginName },
            datatype: "text",
            success: function (responsedata) {
                if (responsedata == "False") {
                    loginexist = true;
                    $("#name-exist").show();
                }
                else {
                    loginexist = false;
                    $("#name-exist").hide();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }
        }
    );
    }
    validateWarning();
}

function validateNull() {
    var password = $("#password").val();
    var displayName = $("#display-name").val();
    var realName = $("#real-name").val();

    if (password == "")
        $("#password-null").show();
    else
        $("#password-null").hide();

    if (displayName == "")
        $("#display-null").show();     
    else
        $("#display-null").hide();

    if (realName == "")
        $("#real-null").show();      
    else
        $("#real-null").hide();

    validateWarning();
}

function validateWarning() {
    if ($("#password").val() != ""
        && $("#display-name").val() != ""
        && $("#real-name").val() != ""
        && $("#register-name").val() != ""
        && loginexist == false)
    {
        $("#register-submit").removeAttr("disabled");        
    }
    else
    {
        $("#register-submit").attr('disabled', "true");
    }
        
}