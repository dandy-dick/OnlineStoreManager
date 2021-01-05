
const ORDER_PAGE = {
    renderOrderItemsGrid: function () {
        //
        var _columns = [
            { field: 'id', caption: 'Mã sp', size: '10%' },
            { field: 'name', caption: 'Tên sản phẩm', size: '60%' },
            {
                field: 'quantity', caption: 'Số lượng', size: '15%',
                resizable: false,
                render: function (rec) {

                }
                
            },
            {
                field: 'control', caption: '', size: '15%',
                resizable: false,
                render: function (rec) {

                }
            },
        ];

        $('#order-items').w2grid({
            name: '_order_items_grid',
            recid: 'id',
            columns: _columns,
            records: [],
            onRender: function (e) {
                e.onComplete = function () {
                    this.refresh();
                }
            }
        });

    },
    setModificationContent:function (_action) {
        // request form content
        // vi validation phia server
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
            var popupContentStyle = `display:flex;flex-direction:column;padding: 1rem;`;
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

                        that.renderOrderItemsGrid();
                    }
                }
            });
            // assign event, because element was not yet added to DOM
            $('#popup_confirm').click(e => {
                if (_action == 'Update')
                    that.edit()
                else
                    that.add()
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
        debugger
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
    add: function () {
        var recs = w2ui['modify_form'].record;
        var order = {
            name: recs.name,
            description: recs.description,
        };
        $.post('/Order/Add', {
            action: 'Insert', // harcode Repository.CRUD
            order: order
        }, (result) => {
            if (result.isSuccess == true)
                    window.location.reload();
            // model state errors validation
            this.showModificationValidation(result.data);
        })
    },
    edit: function () {
        var recs = w2ui['modify_form'].record;
        var order = {
            id: recs.id,
            receivername: recs.receivername,
            deliveryaddress: recs.deliveryaddress,
            receiverphonenumber: recs.receiverphonenumber,
            paymentdate: recs.paymentdate,
            expecteddeliverydate: recs.expecteddeliverydate,
            orderstatus: recs.orderstatus,
            canceldescription: recs.canceldescription,
            description: recs.description,
        };
        $.post('/Order/Update', {
            action: 'Update', // harcode Repository.CRUD
            order: order
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

            w2ui['_grid'].record = recs;
            w2ui['_grid'].refresh();

        });
    },
    showModificationValidation: function (validation) {
        debugger;
    },
}