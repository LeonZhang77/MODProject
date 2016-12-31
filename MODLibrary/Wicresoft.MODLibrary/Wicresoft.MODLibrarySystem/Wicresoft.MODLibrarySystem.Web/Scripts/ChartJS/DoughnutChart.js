var _data;

$(function () 
{
    $.ajax(
       {
            url: $("#myChart").attr("requestUrl"),
            dataType: "json",
            success: function (data) 
            {
                _data = data;
                var ctx = $("#myChart").get(0).getContext("2d");
                var myNewChart = new Chart(ctx).PolarArea(_data);
                new Chart(ctx).Doughnut(_data, null);
                for (var i = 0; i < _data.length; i++)
                {
                    $("#titleColorThRow").after("<tr><td>" +
                                                                    _data[i].title + "</td><td>" +
                                                                    _data[i].color + "</td><td>" +
                                                                    _data[i].value + "</td></tr>");
                    $("#titleColorThRow + tr").css("background", _data[i].color);
                }                
            }
       })
});


