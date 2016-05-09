var _data;
//_data = [
//	{
//	    value: 30,
//	    color: "#F38630"
//	},
//	{
//	    value: 50,
//	    color: "#E0E4CC"
//	},
//	{
//	    value: 100,
//	    color: "#69D2E7"
//	}
//];

$(function () 
{
    $.ajax(
       {
            url: $("#myChart").attr("requestUrl"),
            dataType: "json",
           timeout: 60000,
            success: function (data) 
            {
                _data = data;
                var ctx = $("#myChart").get(0).getContext("2d");
                var myNewChart = new Chart(ctx).PolarArea(_data);
                new Chart(ctx).Doughnut(_data, null);
            }
       })
});


