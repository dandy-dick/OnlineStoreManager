
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
            w2popup.open({
                title: `Sản phẩm - ${title}`,
                modal: true,
                showClose: true,
                width: 650,
                height: 530,
                body: `<div id="${formName}" style="padding-bottom:1rem 0;width:100%;height:100%;"> 
                            ${requestModifyForm} </div>`,
                buttons: $(buttons).wrap('<div></div>').parent().html(),
                onOpen: function (event) {
                    event.onComplete = function () {
                        // auto-completes
                        $.post('/Category/GetList', null, function (res) {
                            autocomplete(
                                document.getElementById('categoryname'),
                                res.records
                            );
                        });

                        $.post('/Supplier/GetList', null, function (res) {
                            autocomplete(
                                document.getElementById('suppliername'),
                                res.records
                            );
                        });
                    }
                },
            });
            // assign event, because element was not yet added to DOM
            $('#popup_confirm').click(e => {
                that.submit(_action);
            });
            $('#popup_abort').click(e => w2popup.close());
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
    submit: function (_action) {
        var photoFile = $('input[name="photo"]').prop('files')[0];
        var data = {
            'action': _action,
            'formfile': photoFile,
            'requestproduct.id': $('input[name="id"]').val(),
            'requestproduct.name': $('input[name="name"]').val(),
            'requestproduct.cost': $('input[name="cost"]').val(),
            'requestproduct.price': $('input[name="price"]').val(),
            'requestproduct.categoryname': $('input[name="categoryname"]').val(),
            'requestproduct.suppliername': $('input[name="suppliername"]').val(),
            'requestproduct.description': $('input[name="description"]').val() ?? '',
        }
        if (_action == 'Insert')
            delete data['requestproduct.id'];

        var _formData = new FormData();
        for (let key in data) {
            _formData.append(key, data[key]);
        }

        var that = this;
        $.ajax({
            url: '/Product/Update',
            type: 'POST',
            data: _formData,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (result) {
            debugger;
            if (result.isSuccess == true)
                window.location.reload();
            // model state errors validation
            that.showValidation(result.data);
        });
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

            window.location.reload();
        });
    },
    showValidation: function (validations) {
        for (var key in validations) {
            if (validations[key].state == 'invalid') {
                var el = $(`.validation[for="${key}"]`)

                $(el).parent('.field').addClass('invalid');
                $(el).html(validations[key].errorMessages);
            }
        }
    },
}