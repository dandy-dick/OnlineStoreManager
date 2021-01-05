
const REPORT_PAGE = {
    renderBarChart: function () {
        var barChartData = {
            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
            datasets: [{
                label: 'Doanh thu theo tháng',
                backgroundColor: 'black',
                data: [5, 10, 15, 50, 100]
            }]
        };

        var ctx = document.getElementById('bar_chart').getContext('2d');
        window.myBar = new Chart(ctx, {
            type: 'bar',
            data: barChartData,
        });
    },
    renderPieChart: function () {
        var chartColors = [
            'red', 'blue', 'orange', 'yellow', 'black', 'green', '', '', '', '',
        ]

        var config = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [5, 10, 15, 20, 50],
                    backgroundColor: chartColors,
                }],
                labels: ['Red', 'Orange', 'Yellow', 'Green', 'Blue']
            },
            options: {
                legend: {
                    position: 'left',
                },
            }
        };

        var ctx = document.getElementById('pie_chart').getContext('2d');
        window.myDoughnut = new Chart(ctx, config);
    }
}