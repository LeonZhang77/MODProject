var _strActionIsRecord = "已保存记录!";
var _strActionIsNotRecord = "保存记录失败";
var _strActionIsNull = "请先选择要审核的数据";
	

var _id;
function changeValue(x)
{
    $("#" + x).addClass("text-danger");    
    var Digit = x.substr(21, x.indexOf("__") - 21);
    $("#WaitingForVerifyList_" + Digit + "__IsChange").val("true");
   
}

function notValidMatch()
{
    $("#NotValidMatchDialog").modal('show','center');
}

function validMatch() {
    $("#ValidMatchDialog").modal('show', 'center');
}

function deleteMatch(i) {
    $("#DeleteMatchDialog").modal('show', 'center');
    _id = i;
}

function doNotValidMatch() {
    var NotValiedList = [];
    $("input[name='NoMatchValid']:checked").each(function (){
        NotValiedList.push($(this).val());
    });
    NotValiedList.join(",");
    if (NotValiedList.length <= 0) {
        alert(_strActionIsNull);
        return false;
    }

    $(function () {
        $.ajax(
            {
                url: $("#NotValidMatchURL").attr("requstUrl"),
                data: { q: NotValiedList },
                datatype: 'json',
                type:'post',
                success: function (data) {
                    if (data) {
                        location.reload(true);
                    }
                    else {
                        location.reload(true);
                    }
                }
            })
    });
}

function doValidMatch() {
    var ValidList = [];
    $("input[name=MatchValid]:checked").each(function () {
        ValidList.push($(this).val());
    });
    ValidList.join(",");
    if(ValidList.length <= 0)
    {
        alert(_strActionIsNull);
        return false;
    }
    $(function () {
        $.ajax(
            {
                url: $("#ValidMatchURL").attr("requstUrl"),
                data: { q: ValidList },
                datatype: 'json',
                type:'post',
                success: function (data) {
                    if (data) {
                        location.reload(true);
                    }
                    else {
                        location.reload(true);
                    }
                }
            })
    });
}

function doDeleteMatch(i) {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteMatchURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data) {
                        location.reload(true);
                    }
                    else {
                        location.reload(true);
                    }
                }
            })
    });
}

function ValidCheckedAll() {
    if ($("#ValidAll").is(':checked'))
    { $("input[name='MatchValid']").prop('checked', true); }
    else
    { $("input[name='MatchValid']").removeAttr("checked"); }

    if ($("#NoValidAll").is(':checked'))
    { $("input[name='NoMatchValid']").prop('checked', true); }
    else
    { $("input[name='NoMatchValid']").removeAttr("checked"); }
}
