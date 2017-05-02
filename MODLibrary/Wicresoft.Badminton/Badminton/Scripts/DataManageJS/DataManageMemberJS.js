$(function () {
    CheckNameInput();
});

function doDeleteMember() {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteMemberURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        $("tr[data-id='memberRow" + _id + "']").hide();
                        $("#DeleteMemberDialog").modal('hide');
                        ErrorTipMessage(_DelSucc,"success");
                    }
                    else {
                        ErrorTipMessage(_DelFail, "danger");
                    }
                }
            })
    });
}

function deleteMember(i) {
    $("#DeleteMemberDialog").modal('show', 'center');
    _id = i;
}


function addMember() {
    $("#AddMemberDialog").modal('show', 'center');
}

function editMember(i) {
    var rowID = "#memberRow" + i;
    var NameID = rowID + "Name";
    var MaleID = rowID + "Male";
    var ClubID = rowID + "Club";

    $("#EditMemberID").val(i);
    $("#EditMemberName").val($(NameID).text().trim());
    console.log(MaleID);
    console.log($(MaleID).text().trim());
    if ($(MaleID).text().trim() == "男")
    {
        $("#girl").prop("checked",false);
        $("#boy").prop("checked", true);
    }
    else
    {
        $("#girl").prop("checked", true);
        $("#boy").prop("checked", false);
    }
    $("#EditClub").find("option[Value='" + $(ClubID).text().trim() + "']").attr("selected", true);

    $("#EditMemberDialog").modal('show', 'center');
    _id = i;
}



