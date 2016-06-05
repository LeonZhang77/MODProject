$(function () {
    $("#RenewConfirmDialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 400,
        width: 500,
        modal: true,
        buttons: {
            "OK": function () {
                $(this).dialog('close');
                doRenewBookInHandWork();
            },
            "Cancel": function () {
                $(this).dialog('close');
            }
        }
    });
});


var _id;

    function renewBookInHandButton(i)
    {
        $("#RenewConfirmDialog").dialog("open");
        _id = i;
    }
   
    function doRenewBookInHandWork() {
    $(function () {
        $.ajax(
            {
                url: $("#RenewBookInHandURL").attr("requstUrl"),
                data: { q:_id },
                success: function (data) {
                    if (data == "true") {
                        alert("Your action is record!");
                        document.execCommand('Refresh');
                    }
                    else {
                        alert(data);
                    }
                }
            })
    });
}

