$(function () {
    $("#Rent-Confirm-Dialog").dialog(
    {
        autoOpen: false,
        resizable: false,
        height: 200,
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

function doRentWork() {
    $(function ()
    {
        $.ajax(
            {
                url:$("#Reserve-Button").attr("requstUrl"),
                data: {q:_id},
        success:function(data){
            if(data == "true")
            {}
            else
            {}
        }
            })
    });
}