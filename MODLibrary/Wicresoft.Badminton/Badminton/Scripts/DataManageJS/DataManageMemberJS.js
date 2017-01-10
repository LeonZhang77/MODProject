$(function () {
    $("#DeleteMemberDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doDeleteMember();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#AddMemberDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doAddMember();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });

    $("#EditMemberDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doEditMember();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });
});

function doDeleteMember() {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteMemberURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        var approveButtonId = "#memberRow" + _id;
                        $(approveButtonId).hide();
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function deleteMember(i) {
    $("#DeleteMemberDialog").dialog("open");
    _id = i;
}

function doAddMember() {
    var dataString = "{" + "\"Name\":\"" + $("#MemberName").val() +
       "\",\"Male\":\"" + $("input[name='MaleOrFemale']:checked").val() +
       "\"}";
    $(function () {
        $.ajax(
            {
                url: $("#AddMemberURL").attr("requstUrl"),
                data: { q: dataString },
                success: function (data) {
                    if (data == "true") {
                        alert(_strActionIsRecord);
                        $("#ChampionshipTitle").val("");
                        $("#startDatePicker").val("");
                        $("#endDatePicker").val("");
                        location.reload(true);
                        $("#mainTabContent").tabs({ active: 1 });
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function addMember() {
    $("#AddMemberDialog").dialog("open");    
}

function doEditMember() {
    var dataString = "{" + "\"strID\":\"" + _id +
        "\",\"Name\":\"" + $("#EditMemberName").val() +
       "\",\"Male\":\"" + $("input[name='EditMaleOrFemale']:checked").val() +
       "\"}";
    $(function () {
        $.ajax(
            {
                url: $("#EditMemberURL").attr("requstUrl"),
                data: { q: dataString },
                success: function (data) {
                    if (data == "true") {
                        alert(_strActionIsRecord);
                        $("#ChampionshipTitle").val("");
                        $("#startDatePicker").val("");
                        $("#endDatePicker").val("");
                        location.reload(true);
                        $("#mainTabContent").tabs({ active: 1 });
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

function editMember(i) {
    var rowID = "#memberRow" + i;
    var NameID = rowID + "Name";
    var MaleID = rowID + "Male";

    $("#EditMemberName").val($(NameID).text().trim());
    var radioList = document.getElementsByName("EditMaleOrFemale");
    if ($(MaleID).text().trim() == '男')
    { radioList[0].checked = true;}
    else
    { radioList[1].checked = true; }
   
    $("#EditMemberDialog").dialog("open");
    _id = i;
}