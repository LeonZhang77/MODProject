$(document).ready(function () {
    $("#submit-order").bind("click", SubmitOrder);
})

function SubmitOrder()
{
    $(function () {
        $.ajax(
            {            
                url: "/PersonalCenter/RequstBook",
                data: { q: $("#submit-order").data('bookid') },
                success: function (data) {
                    if (data == "true") {
                        console.log("1");
                    }
                    else {
                        console.log("2");
                    }
                }
            })
    });
}