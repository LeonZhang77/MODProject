var errorDataSuffix = "_errorData";
var errorDataMessagePropety = "errorDataMessage";

var vNull = "VN";
var vLengh = "VL";
var vEmail = "VE";
var vPhone = "VP";
var vSpecail = "VS";
var vNumber = "VNM";

var defualtRegEx = "^[a-zA-Z\u4e00-\u9fa5()]+$";
var emailRegEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var phoneRegex = /^\+?\d{0,4}\(?\d+\)?\d+-?\d+$/;

var browserDateFormat = "yy/m/dd";
var serverDateFormat = "yyyy-MM-dd";

var multipleValidateField = function (fieldID, multipleType, maxLen, regEx) {
    if (!arguments[3]) {
        maxLen == 0;
    }
    if (!arguments[4]) {
        regEx == defualtRegEx;
    }

    var flag;
    var multipleTypeArrary = multipleType.split(',');

    if (multipleTypeArrary.length > 0) {
        for (var i = 0; i < multipleTypeArrary.length; i++) {
            flag = validateField(fieldID, multipleTypeArrary[i], maxLen, regEx);
            if (!flag) {
                break;
            }
        }
    }

    return flag;
}

var validateField = function (fieldID, validateType, maxLen, regEx) {

    if (!arguments[2]) {
        maxLen == 0;
    }
    if (!arguments[3]) {
        regEx == defualtRegEx;
    }

    var flag;
    var valObject = $("#" + fieldID);

    if (valObject == undefined) {
        return false
    }

    switch (validateType) {
        case vNull:
            flag = validateIsNull(valObject);
            break;
        case vLengh:
            flag = validateLengthRequire(valObject, maxLen);
            break;
        case vEmail:
            flag = validateEmail(valObject);
            break;
        case vPhone:
            flag = validatePhone(valObject);
            break;
        case vSpecail:
            flag = validateSpecailRegExp(valObject, regEx);
            break;
        case vNumber:
            flag = validateNumber(valObject);
            break;
        default:
            flag = false;
            break;
    }

    operationErrorField(valObject, flag);

    return flag;
}

var validateIsNull = function (obj) {
    var value = $(obj).val();
    if (value.length <= 0) {
        return false;
    }
    else {
        return true;
    }
}

var validateLengthRequire = function (obj, maxLen) {
    var value = $(obj).val();
    if (value.length <= maxLen) {
        return true
    }
    else {
        return false
    }
}

var validateEmail = function (obj) {
    return regTest($(obj).val(), emailRegEx);
}

var validatePhone = function (obj) {
    return regTest($(obj).val(), phoneRegex);
}

var validateSpecailRegExp = function (obj, regEx) {
    if (regEx == null || regEx == '') {
        regEx = defualtRegEx;
    }
    return regTest($(obj).val(), regEx);
}

var validateNumber = function (obj) {
    var value = $(obj).val();
    return !isNaN(value);
}

var operationErrorField = function (obj, flag) {
    var errorObj = $("#" + $(obj).attr("id") + errorDataSuffix);
    if (!flag) {
        openErrorField(errorObj)
    }
    else {
        closeErrorField(errorObj)
    }
}

var openErrorField = function (obj) {
    var eMessage = $(obj).attr(errorDataMessagePropety);
    $(obj).html(eMessage);
    $(obj).show();
}

var closeErrorField = function (obj) {
    $(obj).val("");
    $(obj).hide();
}

var regTest = function (text, regEx) {
    var reg = new RegExp(regEx);
    if (reg.test(text)) {
        return true;
    }
    else {
        return false;
    }
}

