
const ORDER_PAGE = {
    onAddOrderItem: function () {
        var _quantity = $('#add-item-form input[name="quantity"]').val() ?? 1;
        _quantity = _quantity <= 0 ? 1: _quantity;

        var item = {
            id: w2ui['_order_items_grid'].records.length + 1,
            productname: $('#add-item-form input[name="productname"]').val(),
            quantity: _quantity,

        };

        w2ui['_order_items_grid'].records.push(item);
        w2ui['_order_items_grid'].refresh();

    },
    onRemoveItem: function (id) {
        var items = w2ui['_order_items_grid'].records;
        w2ui['_order_items_grid'].records = items.filter(v => 
            v.id != id
        );
        w2ui['_order_items_grid'].refresh();
    },
    renderOrderItemsGrid: function (_action) {
        //
        var _columns = [
            { field: 'id', caption: 'STT', size: '10%', resizable: false, },
            { field: 'productname', caption: 'Tên sản phẩm', size: '60%', resizable: false, },
            {
                field: 'quantity', caption: 'Số lượng', size: '15%',resizable: false,
                render: function (rec,index) {
                    return `
                    <input type="number" value="${rec.quantity}" min="0" name="item-quantity" index="${index}"
                        style="height:24px;width:110%;margin:-3px -3px;
                            border: 1px solid #c5c5c5;outline:none;text-align:center;"  
                    />
                    `;
                }
            },
            {
                field: 'control', caption: '', size: '15%',
                resizable: false, style: "padding: 1px;",
                render: function (rec,index) {
                    return `
                    <button type="submit" 
                        onclick="ORDER_PAGE.onRemoveItem(${rec.id})"
                        style="height:23px;width:100%;background-color:red;color:white;"  
                    > Xóa </button>
                    `
                }
            },
        ];

        $('#order-items').w2grid({
            name: '_order_items_grid',
            recid: 'id',
            columns: _columns,
            onRender: function (e) {
                var w2uiGrid = this;
                e.onComplete = function () {
                    // assign events
                    setTimeout(() => {
                        debugger;
                        $('input[name="item-quantity"]').change(function (e) {
                            debugger;
                            var index = $(e.target).attr('index'),
                                val = $(e.target).val();
                            w2uiGrid.records[index].quantity = val;
                        });
                    }, 500);

                    // request data
                    //
                    if (_action == 'Update') {
                        $.post('/Order/GetOrderItems', {
                            orderid: $('#order-form input[name="id"]').val()
                        },
                        function (res) {
                            w2uiGrid.records = res;
                            w2uiGrid.refresh();
                        });
                    }
                }
            }
        });
    },
    setModificationContent:function (_action) {
        var orderId = w2ui['_grid'].getSelection()[0];
        if (_action == 'Update' && !orderId)
            return;

        var data = {
            action: _action,       // hardcode enum Repository.CRUD
            order: {
                id: _action == 'Update' ? orderId : null,// get order ID if action is update
            }
        };

        var title = _action == 'Update' ? 'Chỉnh sửa' : 'Tạo mới';

        var that = this;
        $.post('/Order/Modify', data, function (requestModifyForm) {
            var buttons = $('<div></div>').addClass(['flex_center']);

            $(`<button id="popup_confirm"> ${ title } </button>`)
                .addClass(['medium_btn', 'btn_success'])
                .css({ 'margin-right': '14px' })
                .appendTo(buttons);

            $('<button id="popup_abort"> Hủy bỏ </button>')
                .addClass(['medium_btn', 'btn_secondary'])
                .appendTo(buttons);

            // render popup
            var formName = 'modify_form';
            var popupContentStyle = `display:flex;flex-direction:column;`;
            w2popup.open({
                title: `Đơn hàng Ecommerce - ${title}`,
                modal: true,
                showClose: true,
                width: 1000,
                height: 800,
                body: `<div id="${formName}" style="${popupContentStyle}"> ${requestModifyForm} </div>`,
                buttons: $(buttons).wrap('<div></div>').parent().html(),
                onOpen: function (event) {
                    event.onComplete = function () {
                        // auto-completes
                        $.post('/Product/GetList', null, function (res) {
                            autocomplete(
                                document.getElementById('productname'),
                                res.records
                            );
                        });

                        that.renderOrderItemsGrid(_action);
                    }
                },
                onClose: function () {
                    w2ui['_order_items_grid'].destroy();
                }

            });
            // assign event, because element was not yet added to DOM
            $('#popup_confirm').click(e => {
                that.submit(_action);
            })
            $('#popup_abort').click(e => w2popup.close())
        });
    },
    setDeletionContent() {
        //  Popup Buttons
        //
        var buttons = $('<div></div>').addClass(['flex_center']);

        $('<button id="popup_delete"> Xóa </button>')
            .addClass(['medium_btn', 'btn_danger'])
            .css({ 'margin-right': '14px' })
            .appendTo(buttons);

        $('<button id="popup_abort"> Hủy bỏ </button>')
            .addClass(['medium_btn', 'btn_secondary'])
            .appendTo(buttons);

        // Popup Content
        var message = $('<div></div>').addClass('flex_col_center')
            .css({ 'text-align': 'center', 'padding': '1rem' });

        $('<img/>').addClass('big_icon')
            .attr('src', '/icons/warning.svg')
            .appendTo(message);

        $('<div></div>').css({ 'margin-top': '1rem' })
            .html('<b>Bạn chắc chứ</b><p> Bạn sẽ không thể hoàn tác tác vụ này!! <p/>')
            .appendTo(message)

        w2popup.open({
            title: 'Confirmation',
            showClose: true,
            body: $(message).wrap('<div></div>').parent().html(),
            buttons: $(buttons).wrap('<div></div>').parent().html(),
            width: 300,
            height: 240,
            modal: true,
        });

        // assign event, because element was not yet added to DOM
        var that = this;
        $('#popup_delete').click(e => that.delete())
        $('#popup_abort').click(e => w2popup.close())
    },
    onAdd: function () {
        this.setModificationContent('Insert');
    },
    onEdit: function () {
        this.setModificationContent('Update');
    },
    onDelete: function () {
        if (w2ui['_grid'].getSelection().length)
            this.setDeletionContent()
    },
    onSelectDate:function() {
        var fromDate = $('input[name="FromDate"]').val(),
            toDate = $('input[name="ToDate"]').val();
        window.location.search = "";
        window.location.search = `?FromDate=${fromDate}&ToDate=${toDate}`;
    },
    submit: function (_action) {
        var _order = {
            id: $('#order-form input[name="id"]').val(),
            receivername: $('#order-form input[name="receivername"]').val(),
            deliveryaddress: $('#order-form input[name="deliveryaddress"]').val(),
            receiverphonenumber: $('#order-form input[name="receiverphonenumber"]').val(),
            createddate: $('#order-form input[name="createddate"]').val(),
            description: $('#order-form textarea[name="description"]').val() ?? '',
        };
        var _orderItems = w2ui['_order_items_grid'].records.map((v, i) => {
            return {
                'quantity': v.quantity,
                'product.name': v.productname
            }
        });

        $.post('/Order/Update', {
            action: _action, // harcode Repository.CRUD
            order: _order,
            orderitems: _orderItems
        }, (result) => {
            if (result.isSuccess == true)
                window.location.reload();
            // model state errors validation
            this.showModificationValidation(result.data);
        })
    },
    delete: function () {
        var deleteids = w2ui['_grid'].getSelection();
        var url = '/Order/Delete';
        $.post(url, { deleteids: deleteids }, () => {
            var recs = w2ui['_grid'].records;
            recs = recs.filter(value => {
                return deleteids.find(id => id != value.id);
            })

            window.location.reload();
        });
    },
    showModificationValidation: function (validations) {
        debugger
        for (var key in validations) {
            if (validations[key].state == 'invalid') {
                var el = $(`.validation[for="${key}"]`)

                $(el).parent('.field').addClass('invalid');
                $(el).html(validations[key].errorMessages);
            }
        }
    },
}