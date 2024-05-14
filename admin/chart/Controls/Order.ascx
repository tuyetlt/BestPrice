<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Order.ascx.cs" Inherits="admin_chart_Controls_Order" %>

<figure class="highcharts-figure">
    <div id="container" style="width:100%"></div>

</figure>



<script type="text/javascript">
    Highcharts.chart('container', {
        chart: {
            type: 'cylinder',
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
            text: 'Đơn hàng'
        },
        plotOptions: {
            series: {
                depth: 20,
                colorByPoint: false
            }
        }, credits: {
            enabled: false,
            text: 'Example.com',
            href: 'http://www.example.com'
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
                text: 'Số tiền',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            }
        }, { // Secondary yAxis
            title: {
                text: 'Số đơn',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            labels: {
                format: '{value} đơn',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            opposite: true
        }],
        series: [{
            name: 'Số đơn hàng',
            type: 'cylinder',
            yAxis: 1,
            allowDecimals: false,
            data: [<%= jsonDon %>],
            tooltip: {
                valueSuffix: ' đơn'
            }

        }, {
            name: 'Tiền đơn hàng',
            type: 'cylinder',
            allowDecimals: false,
            data: [<%= jsonTien %>],
            tooltip: {
                valueSuffix: 'k VNĐ'
            }
        }]
    });
</script>
