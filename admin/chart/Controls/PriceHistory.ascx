<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PriceHistory.ascx.cs" Inherits="admin_chart_Controls_PriceHistory" %>

<% if (dataTable.Rows.Count > 1)
    { %>

<figure class="highcharts-figure">
    <div id="container" style="width:100%"></div>

</figure>



<script type="text/javascript">
    Highcharts.chart('container', {
        chart: {
            type: 'line',
            style: {
                fontFamily: 'arial'
            },
            options3d: {
                enabled: true,
                alpha: 0,
                beta: 0,
                depth: 20,
                viewDistance: 50
            }
        },
        title: {
            text: 'Lịch sử giá'
        },
        plotOptions: {
            series: {
                depth: 100,
                colorByPoint: false
            }
        }, 
        xAxis: [{
            categories: [<%= jsonName %>],
            crosshair: true
        }],
        yAxis: [{ // Primary yAxis
            labels: {
                format: '{value}k',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            },
            title: {
                text: 'Giá',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            }
        }],
        series: [{
            name: '',
            type: 'line',
            allowDecimals: false,
            data: [<%= jsonTien %>],
                tooltip: {
                    valueSuffix: 'k VNĐ'
                }
            }]
    });
</script>
<%} %>