<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Article.ascx.cs" Inherits="admin_chart_Controls_Article" %>

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
            text: 'Bài viết'
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
        yAxis: [{ 
            title: {
                text: 'Số bài',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            labels: {
                format: '{value} bài',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            opposite: true
        }],
        series: [{
            name: 'Số bài viết',
            type: 'cylinder',
            yAxis: 1,
            allowDecimals: false,
            data: [<%= jsonDon %>],
            tooltip: {
                valueSuffix: ' đơn'
            }

        }]
    });
</script>
