
const PRODUCT_PAGE = {
    setModificationContent(_action) {
        var productId = w2ui['_grid'].getSelection()[0];
        if (_action == 'Update' && !productId) {
            alert('Bạn chưa chọn dòng nào cả');
            return;
        }

        var data = {
            action: _action,       // hardcode enum Repository.CRUD
            product: {
                id: productId
            }
        };

        var title = _action == 'Update' ? 'Chỉnh sửa' : 'Tạo mới';

        var that = this;
        $.post('/Product/Modify', data, function (requestModifyForm) {
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
            var popupContentStyle = `padding: 1rem;width:100%;height:100%;`;
            w2popup.open({
                title: `Sản phẩm - ${title}`,
                modal: true,
                showClose: true,
                width: 500,
                height: 600,
                body: `<div id="${formName}" style="${popupContentStyle}"> 
                            ${requestModifyForm} </div>`,
                buttons: $(buttons).wrap('<div></div>').parent().html(),
                onOpen: function (event) {
                    event.onComplete = function () {
                        var options = w2uiFormFromHtml(requestModifyForm, formName);
                        // set form records
                        $(`#${formName}`).w2form(options);
                        $(`#w2ui-popup #${formName}`).w2render(formName);
                    }
                },
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
        this.setModificationContent('Insert');
    },
    onEdit: function () {
        this.setModificationContent('Update');
    },
    onDelete: function () {
        if (w2ui['_grid'].getSelection().length)
            this.setDeletionContent()
    },
    add: function () {
        var recs = w2ui['modify_form'].record;
        var product = {
            name: recs.name,
            cost: recs.cost,
            price: recs.price,
            categoryid: recs.categoryid ? recs.categoryid.id: null,
            supplierid: recs.supplierid ? recs.supplierid.id: null,
            description: recs.description,
        };
        $.post('/Product/Add', {
            action: 'Insert', // harcode Repository.CRUD
            product: product
        }, (result) => {
            if (result.isSuccess == true)
                    window.location.reload();
            // model state errors validation
            this.showModificationValidation(result.data);
        })
    },
    edit: function () {
        var recs = w2ui['modify_form'].record;
        var product = {
            id: recs.id,
            name: recs.name,
            cost: recs.cost,
            price: recs.price,
            categoryid: recs.categoryid ? recs.categoryid.id : null,
            supplierid: recs.supplierid ? recs.supplierid.id : null,
            description: recs.description,
        };
        $.post('/Product/Update', {
            action: 'Update', // harcode Repository.CRUD
            product: product
        }, (result) => {
            if (result.isSuccess == true)
                window.location.reload();
            // model state errors validation
            this.showModificationValidation(result.data);
        })
    },
    delete: function () {
        var deleteids = w2ui['_grid'].getSelection();
        var url = '/Product/Delete';

        $.post(url, { deleteids: deleteids } , () => {
            var recs = w2ui['_grid'].records;
            recs = recs.filter(value => {
                return deleteids.find(id => id != value.id);
            })

            w2ui['_grid'].record = recs;
            w2ui['_grid'].refresh();
            w2popup.close();
        });
    },
    showModificationValidation: function (validation) {
        debugger;
    },
}