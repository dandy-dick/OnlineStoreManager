
const REPORT_PAGE = {
    chartColors: ['#357266', '#65532F', '#65532F', '#FF5A5F',
        '#C81D25', '#704C5E', '#6dc0d5', '#ffd791', '#0e3b43', 'black'
    ],
    onSelectDate: function () {
        var fromDate = $('input[name="FromDate"]').val(),
            toDate = $('input[name="ToDate"]').val();
        window.location.search = "";
        window.location.search = `?FromDate=${fromDate}&ToDate=${toDate}`;
    },
    renderRevenueReport: function (_callBack) {
        var _fromDate = $('input[name="FromDate"]').val(),
            _toDate = $('input[name="ToDate"]').val();

        var _data = {};
        if (_fromDate)
            _data['FromDate'] = _fromDate;
        if (_toDate)
            _data['ToDate'] = _toDate;

        if (!_fromDate || !_toDate)
            return;

        var that = this;
        $.post('/Report/RevenueReport', _data, function(res) {
            // render summary cards
            $('#revenue-summary').html(res.revenue + ' VND');
            $('#profit-summary').html(res.profit + ' VND');

            // render chart
            var chartOptions = {
                labels: Array.from(Array(13).keys()).map(p =>
                    (p > 0) ? 'Tháng ' + p : ''),
                datasets: [{
                    label: 'Doanh thu theo tháng',
                    backgroundColor: that.chartColors[0],
                    data: res.chartData
                }]
            };

            var ctx = document.getElementById('bar_chart').getContext('2d');
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: chartOptions,
            });

            // render next chart
            _callBack();
        });
    },
    renderBestSellerReport: function () {
        var _fromDate = $('input[name="FromDate"]').val(),
            _toDate = $('input[name="ToDate"]').val();
        var _data = {};
        if (_fromDate)
            _data['FromDate'] = _fromDate;
        if (_toDate)
            _data['ToDate'] = _toDate;

        var that = this;
        $.post('/Report/BestSellingReport', _data, function (res) {
            // render grid data
            //
            //res = [ {top, name, quantity, revenue } ]
            var _columns = [
                { field: 'top', caption: 'Top', size: '10%', resizable: false, },
                { field: 'name', caption: 'Tên sản phẩm', size: '40%', resizable: false, },
                {
                    field: 'revenue', caption: 'Doanh số', size: '30%', resizable: false,
                    render: function (rec) {
                        return rec.revenue + " VND";
                    }
                },
                {
                    field: 'quantity', caption: 'Đã bán', size: '15%', resizable: false,
                },
            ];
            if (w2ui['product_grid'])
                w2ui['product_grid'].destroy();
            $('#product_grid').w2grid({
                name: 'product_grid',
                header: 'Top sản phẩm bán chạy nhất',
                show: { header: true },
                recid: 'top',
                columns: _columns,
                onRender: function (e) {
                    e.onComplete = function () {
                        this.records = res.map((v, index) => {
                            return {
                                top: index + 1,
                                name: v.productName,
                                quantity: v.quantity,
                                revenue: v.revenue
                            }
                        });
                        this.refresh();
                    }
                }
            });
            // render pie chart
            //
            var chartOptions = {
                type: 'doughnut',
                data: {
                    datasets: [
                        {
                            data: res.map(p => p.revenue),
                            backgroundColor: that.chartColors,
                        }
                    ],
                    labels: res.map(p => p.productName)
                },
                options: {
                    legend: {
                        position: 'left',
                    },
                }
            };

            var ctx = document.getElementById('pie_chart').getContext('2d');
            window.myDoughnut = new Chart(ctx, chartOptions);
        });
    }
}