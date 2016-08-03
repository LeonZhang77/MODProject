var MouseEvent = function (e) {
    this.x = e.pageX
    this.y = e.pageY
}

var Mouse = function (e) {
    var kdheight = jQuery(document).scrollTop();
    mouse = new MouseEvent(e);
    leftpos = mouse.x + 10;
    toppos = mouse.y - kdheight + 10;
}

function mIn(i) {
    var relatedStr = ".related" + i.toString();
    Mouse(window.event);
    jQuery(relatedStr).css({ top: toppos, left: leftpos }).fadeIn(100);
}

function mOut(i) {
    var relatedStr = ".related" + i.toString();
    jQuery(relatedStr).hide();
}

$(function () {
    $("#PagingViewContentTable table").addClass("table table-bordered");
    $("#PagingViewContentTable td").attr("align", "center");
    $("#disableFirst").addClass("btn btn-sm action_button_disabled").attr("disabled", "disabled");
    $("#disableLast").addClass("btn btn-sm action_button_disabled").attr("disabled", "disabled");
    $("pageData").addClass("features");
    $("#PagingViewContentTable a").addClass("btn btn-sm action_button");    
});