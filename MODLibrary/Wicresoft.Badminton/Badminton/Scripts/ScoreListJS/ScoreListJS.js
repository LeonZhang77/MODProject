//var _data;
var _chartID;
var _player1;
var _player2;
var _player1Name;
var _playre2Name;

function showBattleRate(_chartID, _player1, _player2) {
    var dataString = "{" + "\"player1\":\"" + _player1 + "\",\"player2\":\"" + _player2 + "\"}";
       $.ajax(
       {
           url: $(_chartID).attr("requestUrl"),
           data: { q: dataString },
           dataType: "json",
           success: function (data) {
               var ctx = $(_chartID).get(0).getContext("2d");
               var myNewChart = new Chart(ctx).Pie(data);
           }
       })       
}

function showChart() {
    if ($("input[name='chk_list']:checked").length != 2) {
        alert("请选择两位选手，不多不少！");
    }
    else {
        var flag = 0;
        $("input[name='chk_list']").each(function () {
            if ($(this).is(':checked')) {
                if (flag == 0)
                    { _player1 = $(this).val(); flag++; }
                else
                    { _player2 = $(this).val(); }
            }
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

function selectAll() {
    if ($("#chk_all").is(':checked')) {
        $("input[name='chk_list']").prop("checked", true);
    }
    else {
        $("input[name='chk_list']").removeAttr("checked");
    }
}

function showAllRows() {
    $("#chart").hide();
    $("tr").each(function () {
        $(this).show();
    });
}

function hideAllRows() {
    $("tr").each(function () {
        $(this).hide();
    });
    $("#functionRow").show();
    $("#firstRow").show();    
}

function showSelectedRows() {
    $("#chart").hide();
    $("input[name='chk_list']").each(function () {
        if (!$(this).is(':checked')) {
           hideThisRow($(this).val());
        }
    });    
}

function showThisRow(ID) {
    var rowID = "#scoreRow_" + ID;
    $(rowID).show();
}

function hideThisRow(ID){
    var rowID = "#scoreRow_" + ID;
    $(rowID).hide();
}

function selectThisGuy() {
    $("#SearchSelectedID").find("option").each(function () {
        if ($(this).text() == $("#selectMemberNameTxt").val()) { ID = $(this).val(); }
    });
    checkBoxID = "#chk_list_" + ID;
    $(checkBoxID).prop("checked", true);
}