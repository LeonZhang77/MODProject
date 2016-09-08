var recipient;

$(document).ready(function () {
    $('.bs-example-modal-sm').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        recipient = button.data('itemid');
    });
    $("#btn-model").bind("click", renewBookInHandButton);
    
})
function renewBookInHandButton() {
        $.ajax(
            {
                url: "/PersonalCenter/RenewBookInHand",
                data: { q: recipient }
            })
}