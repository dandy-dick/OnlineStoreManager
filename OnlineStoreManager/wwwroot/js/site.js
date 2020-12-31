
/* lấy mã nguồn bên trong 1 wrapper và trả về dạng string, remove khỏi DOM nếu cần */
function htmlToString(wrapper, remove = false) {
    var elementStr = $(wrapper);
    if (remove)
        $(wrapper).remove();
    return elementStr;
}

/* Hide all script that have attribute name = attrName (or hide) */
function hideScripts(attrName) {
    var _attrName = 'hide';
    if (typeof attrName === 'string' || attrName instanceof String) {
        if (attrName.length > 0)
            _attrName = attrName;
    }
    $(`script[${_attrName}]`).remove();
}

function extendQuery() {
    var obj = {
        queries: null,
        init: function () {
            this.setQueriesFromLink();
            var that = this;
            $('[extend_query][submit_on_click]').on('click', (e) => {
                that.submit(e.target)
            });

            $('[extend_query][submit_on_keypress]').on('keypress', (e) => {
                var key = $(e.target).attr('submit_on_keypress');
                if (!key)
                    key = 13;
                if (key == e.which) {
                    that.submit(e.target);
                    return false;
                }
            });
        },
        setQueriesFromLink: function () {
            var search = window.location.search.substr(1);
            // get all current queries
            this.queries = search.split('&').reduce((result, query) => {
                query = query.split('=');
                var name = query[0], value = query[1];
                if (name.length)
                    result[name] = value;
                return result;
            }, {});
        },
        submit: function (element) {
            // get query from this element
            //
            var name = $(element).attr('extend_query'),
                value = ($(element).attr('submit_on_keypress') != undefined) ?
                    $(element).val() : $(element).attr('query');
            debugger
            if (value) {
                // extend to this.queries
                //
                this.queries[name] = value;

                var _search = `?`;
                for (var key in this.queries) {
                    if (this.queries[key])
                        _search += key + "=" + this.queries[key] + "&";
                }
                window.location.search = _search;
            }
        }
    };
    obj.init();
}

function _w2uiPagination(currentPage = 1, totalItems = 0, pageSize = 20) {
    return {
        currentPage: currentPage,
        totalPage: 1,
        pageSize: pageSize,
        totalItems: totalItems,
        pageOffset: 5,
        start: null,
        currentOffset: null,
        maxOffset: null,
        init: function () {
            this.totalPage = Math.ceil(totalItems / pageSize);
            this.totalPage = (this.totalPage) ? this.totalPage : 1;

            this.currentOffset = Math.ceil(this.currentPage / this.pageOffset) - 1;
            this.currentOffset = (this.currentOffset < 0) ? 0 : this.currentOffset;
            this.maxOffset = Math.floor(this.totalPage / this.pageOffset) - 1;
            this.maxOffset = (this.maxOffset < 0) ? 0 : this.maxOffset;

            this.start = this.currentOffset * this.pageOffset + 1;
        },
        updatePagination: function () {
            if (this.currentOffset > 0 && this.currentOffset < this.maxOffset) {
                var str = "";
                for (var i = this.start; i > 0 && i <= this.start + 4 && i <= this.totalPage; i++) {
                    var isActive = (this.currentPage == i) ? "active" : "";
                    str += `
                    <span class="pagination_item  ${isActive} clickable"
                        submit_on_click
                        extend_query="CurrentPage" query="${i}"> ${i} </span>
                `;
                }
                $('#_pagination_container').html(str);
                extendQuery();
            }
        },
        loadPrevPages: function () {
            this.currentOffset -= 1;
            if (this.currentOffset < 0)
                this.currentOffset = 0;
            this.start = this.currentOffset * this.pageOffset + 1;
            this.updatePagination();
        },
        loadNextPages: function () {
            this.currentOffset += 1;
            if (this.currentOffset >= this.maxOffset)
                this.currentOffset = this.maxOffset;
            this.start = this.currentOffset * this.pageOffset + 1;
            this.updatePagination();
        }
    }
}



/**
 * Trả về w2ui form options từ mã nguồn HTML
 * 
 * @param {any} htmlData
 * @param {any} formName
 */
function w2uiFormFromHtml(htmlData, formName) {
    // create Jquery DOM Element from HtmlData
    var formEl = $(htmlData);
    var fieldEls = $(formEl).find('input[w2field],textarea[w2field]');

    // create fields html
    var _fields = [],
        _record = {};
    fieldEls.each(function (i, el) {
        var type = $(el).attr('w2field');
        switch (type) {
            case 'text':
            case 'int': {
                _fields.push({
                    name: $(el).attr('name'), type: type,
                });
                _record[$(el).attr('name')] = $(el).attr('value');

                break;
            }
            case 'textarea': {
                _fields.push({
                    name: $(el).attr('name'), type: 'textarea',
                });
                _record[$(el).attr('name')] = $(el).attr('value');

                break;
            }
            case 'list': {
                var _val = $(el).attr('value');
                var _selected = JSON.parse(_val);

                _fields.push({
                    name: $(el).attr('name'), type: type,
                    options: {
                        url: $(el).attr('url'), minLength: 0,
                        selected: _selected,
                    },
                });
                _record[$(el).attr('name')] = $(el).attr('value');
                break
            }
        }
    });

    return {
        name: formName,
        fields: _fields,
        record: _record
    }
}
