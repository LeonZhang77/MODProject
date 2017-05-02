//var _data;
var _chartID;
var _player1;
var _player2;
var _player1Name;
var _playre2Name;
var _twoUserError="请选择两位选手！";
function showBattleRate(_chartID, _player1, _player2) {
    var dataString = "{" + "\"player1\":\"" + _player1 + "\",\"player2\":\"" + _player2 + "\"}";
       $.ajax(
       {
           url: $(_chartID).attr("requestUrl"),
           data: { q: dataString },
           dataType: "json",
           success: function (data) {
               var ctx = $(_chartID).get(0).getContext("2d");
               var myNewChart = new $.zui.Chart(ctx).Pie(data);
           }
       })       
}

function showChart() {
    if ($("tr[class='active']").length != 2) {
        TipErrorMessage(_twoUserError);
    }
    else {
        var flag = 0;
        $("tr[class='active']").each(function () {            
            if (flag == 0)
                { _player1 = $(this).attr("data-id"); flag++; }
            else
            { _player2 = $(this).attr("data-id"); }
        });
        $("#SearchSelectedID").find("option").each(function () {
            if ($(this).val() == _player1) { _player1Name = $(this).text(); }
            if ($(this).val() == _player2) { _player2Name = $(this).text(); }
        });

        $("#chart").show();
        $("#legend_1").empty().append("<b>" + _player1Name + "</b>");
        $("#legend_2").empty().append("<b>" + _player2Name + "</b>");

        _chartID = "#myChart1";
        showBattleRate(_chartID, _player1, _player2);

        _chartID = "#myChart2";
        showBattleRate(_chartID, _player1, _player2);

        _chartID = "#myChart3";
        showBattleRate(_chartID, _player1, _player2);
      
    }
}


function showAllRows() {
    $("#chart").hide();
    $("tr").each(function () {
        $(this).show();
    });
}


function showSelectedRows() {
    $("#chart").hide();
    $("tbody>tr").each(function () {
        $(this).hide();
    });
    $("tr[class='active']").show();
}

function selectThisGuy() {
    $("#SearchSelectedID").find("option").each(function () {
        if ($(this).text() == $("#selectMemberNameTxt").val()) { ID = $(this).val(); }
    });
    $("tr[data-id='" + ID + "']").attr("class", "active");
}

function TipErrorMessage(message) {
    new $.zui.Messager('提示消息：' + message, {
        type: 'danger',
        close: false
    }).show();
}