$(function () {
    $("#Rent-Confirm-Dialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doRentWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    })
});

$(function () {
    $("#Submit-Result-Dialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 200,
        width: 500,
        modal: true,
    })
});

var _id;

var bindRentConfirmDialog = function (i) {
    $("#Rent-Confirm-Dialog").dialog("open");
    _id = i;
}

//function bindRentConfirmDialog(i) {
//        $("#Rent-Confirm-Dialog").dialog("open");
//        _id = i;
//}

function doRentWork() {
    $(function ()
    {
        $.ajax(
            {
                url:$("#Reserve-Button").attr("requstUrl"),
                data: {q:_id},
        success:function(data){
            if(data == "true")
            {
                $(function () {
                    $("#Submit-Result-Dialog").dialog(
                    {
                        buttons: {
                            "Go My List": function () {
                                $(this).dialog('close');
                                self.location = "/RentManage";
                            },
                            "Find others": function () {
                                $(this).dialog('close');
                                self.location = "/BookManage";
                            }
                        }
                    })
                });
                $("#Submit-Result-Dialog").append("<p>" +
                    "You Request has been submited successfully!." + "<br />" +
                    "Our administrator will handle it soon." + "<br />" +
                    "</p>");
                $("#Submit-Result-Dialog").dialog("open");
            }
            else
            {
                $(function () {
                    $("#Submit-Result-Dialog").dialog(
                    {
                        buttons: {
                            "Go To My List": function () {
                                $(this).dialog('close');
                                self.location = "/RentManage";
                            },
                            "Find another book": function () {
                                $(this).dialog('close');
                                self.location = "/BookManage";
                            }
                        }
                    })
                });
                $("#Submit-Result-Dialog").append("<p>"+
                    "Sorry, You Request was failed to Submit." + "<br />" +
                    data +
                    "</p>");
                $("#Submit-Result-Dialog").dialog("open");
            }
        }
            })
    });
}