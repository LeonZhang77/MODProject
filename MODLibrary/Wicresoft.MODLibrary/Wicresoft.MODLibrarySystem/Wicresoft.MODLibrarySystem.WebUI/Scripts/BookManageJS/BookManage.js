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