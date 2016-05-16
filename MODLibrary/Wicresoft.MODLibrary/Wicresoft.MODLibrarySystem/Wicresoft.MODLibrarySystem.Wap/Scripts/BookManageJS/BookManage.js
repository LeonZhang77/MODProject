var pageIndex = 0;

$(document).ready(function () {
    partialView(0);
    $(document).bind("scroll", scroll);
    $("#search").bind("click", search);
})

function scroll() {
    if ($(document).scrollTop() == 0)
        partialView(-1); 
    else if ($(document).scrollTop() >= $(document).height() - $(window).height())
        partialView(1);
}

function partialView(direction) {
    if (pageIndex + direction >= 0) {
        direction <= 0 ? $('#Load1').show() : $('#Load2').show();
        $.ajax({
            type: "get",
            data: { pageIndex: pageIndex + direction },
            datatype: "json",
            success: function (data) {
                $('#header').html(data);
                $(document).scrollTop(10);
                pageIndex += direction;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            },
            complete: function (jqXHR, textStatus) {
                direction <= 0 ? $('#Load1').hide() : $('#Load2').hide();
            }
        });
    }
}
function search() {
    $("#option").slideToggle();
};