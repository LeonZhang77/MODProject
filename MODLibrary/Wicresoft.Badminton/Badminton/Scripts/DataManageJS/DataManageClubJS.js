var _internetTimeOutError = "网络连接超时";

function doDeleteClub() {
    $(function () {
        $.ajax(
            {
                url: $("#DeleteClubURL").attr("requstUrl"),
                data: { q: _id },
                success: function (data) {
                    if (data == "true") {
                        $("tr[data-id='clubRow" + _id + "']").hide();
                        $("#DeleteClubDialog").modal('hide');
                        ErrorTipMessage(_DelSucc, "success");
                    }
                    else {
                        alert(data);
                    }
                },
                error: function () {
                    alert(data);
                }
            });
    });
}





function addClub() {
    $("#AddClubDialog").modal('show','center');
}


function editClub(i) {
    $("#EditClubName").val("");
    $("#EditCaptain").html("<option value='0'></option>");
    $("#EditDescription").val("");

    var rowID = "#clubRow" + i;
    var NameID = rowID + "Name";
    var CaptainID = rowID + "Captain";
    var Description = rowID + "Description";

        $.ajax({
            url: "/DataManage/GetMemberAndClubRelationListToJsonByClub",
            data: { clubID: i },
            dataType: 'json',
            type: 'get',
            success: function (data) {
                $.each(data, function (index, value) {
                    
                    if (value.IsCaption) {
                        $("#EditCaptain").append("<option value='" + value.ID + "' selected='selected'>" + value.MemberID.Name + "</option>");
                    } else {
                        $("#EditCaptain").append("<option value='" + value.ID + "'>" + value.MemberID.Name + "</option>");
                    }
                });
            }

        });

    $("#EditClubName").val($(NameID).text().trim());
    $("#EditDescription").val($(Description).text().trim());
    $("#EditID").val(i);
    $("#EditClubDialog").modal('show','center');
    _id = i;
}

function batchAddMember(i) {
    $("#BatchAddMemberDialog").modal('show', 'center');
    _id = i;
}


function doBatchAddMember() {

    var MemberList = [];
    $("input[name=AddMemberBox]:checked").each(function () {
        MemberList.push($(this).val());
    });
    MemberList.join(",");

    $(function () {
        $.ajax({
            url: $("#BatchAddMemberURL").attr("requsturl"),
            data: { ClubID: _id, Members: MemberList },
            type: 'post',
            dataType: 'json',
            success: function (data) {
                if (data) {
                    location.reload(true);
                } else {
                    ErrorTipMessage(_BatchAddFail, "danger");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown)
            {
                ErrorTipMessage(_BatchAddFail + "," + XMLHttpRequest.readyState + "," + textStatus + "," + errorThrown, "danger");
            }
        });
    });
}

function batchRemoveMember(i) {
    $("#RemoveClubMemberList").html("");

    $.ajax({
        url: "/DataManage/GetMemberAndClubRelationListToJsonByClub",
        data: { clubID: i },
        success: function (data) {
            $.each(data, function (index, value) {
                $("#RemoveClubMemberList").append("<li class='list-group-item'><span><input type='checkbox' name='RemoveMemberBox' value='" + value.ID + "' /></span><span>" + value.MemberID.Name + "</span></li>");
            });
        }

    });

    $("#BatchRemoveMemberDialog").modal('show', 'center');
    _id = i;
}

function doBatchRemoveMember() {
    var RelationList = [];
    $("input[name=RemoveMemberBox]:checked").each(function () {
        RelationList.push($(this).val());
    });
    RelationList.join(",");

    $(function () {
        $.ajax({
            url: $("#BatchRemoveMemberURL").attr("requsturl"),
            data: { Relations: RelationList },
            type: 'post',
            dataType: 'json',
            success: function (data) {
                if (data) {
                    location.reload(true);
                } else {
                    ErrorTipMessage(_DelFail, "danger");
                }
            },
            error: function (data)
            {
                ErrorTipMessage(_DelFail+","+XMLHttpRequest.readyState + "," + textStatus + "," + errorThrown, "danger");
            }
        });
    });

}

function checkedAll()
{
    if ($("#AllMembers").is(':checked'))
    {$("input[name='AddMemberBox']").prop("checked", true);}
    else {$("input[name='AddMemberBox']").removeAttr("checked");}

    if ($("#RemoveAllMembers").is(':checked'))
    {$("input[name='RemoveMemberBox']").prop("checked", true);}
    else {$("input[name='RemoveMemberBox']").removeAttr("checked");}
    
}

function deleteClub(i)
{
    $("#DeleteClubDialog").modal('show', 'center');
    _id = i;
    
}