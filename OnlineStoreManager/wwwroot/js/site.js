
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
            debugger
            // get query from this element
            //
            var name = $(element).attr('extend_query'),
                value = ($(element).attr('submit_on_keypress') != undefined) ?
                    $(element).val() : $(element).attr('query');
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

function _w2uiPagination({ container, pageSize, currentPage, totalItems }) {
    var obj = {
        container,
        pageSize,
        currentPage,
        totalItems,
        totalPage: 1,
        queryName: 'CurrentPage',
        start: null,
        pageOffset: 5,
        currentOffset: null,
        maxOffset: null,
        init: function () {
            this.totalPage = Math.ceil(this.totalItems / this.pageSize);
            this.totalPage = (this.totalPage) ? this.totalPage : 1;
            this.currentOffset = Math.ceil(this.currentPage / this.pageOffset) - 1;
            this.currentOffset = (this.currentOffset < 0) ? 0 : this.currentOffset;
            this.maxOffset = Math.floor(this.totalPage / this.pageOffset) - 1;
            this.maxOffset = (this.maxOffset < 0) ? 0 : this.maxOffset;
            this.start = this.currentOffset * this.pageOffset + 1;

            var that = this;
            $(`${container} .prev`).click(function () { that.loadPrevPages(); })
            $(`${container} .next`).click(function () { that.loadNextPages(); })

            this.updatePagination();
            
        },
        updatePagination: function () {
            // render pagination
            //
            var html = ``;
            for (var i = this.start; i > 0 && i <= this.start + 4 && i <= this.totalPage; i++) {
                var isActive = (this.currentPage == i) ? "active" : "";
                html += `
                    <span class="pagination_item ${isActive} clickable" 
                        extend_query="CurrentPage" submit_on_click query="${i}"> ${i} </span>
                `;
            }
            $(`${container} .pagination-container`).html(html);
            extendQuery();
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

    obj.init();
}

function autocomplete(inp, arr) {
    var currentFocus;
    inp.addEventListener("input", function (e) {
        var a, b, i, val = this.value;
        /*close any already open lists of autocompleted values*/
        closeAllLists();

        if (!val) { return false; }
        currentFocus = -1;

        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            if (arr[i].substr(0, val.length).toUpperCase() == val.toUpperCase()) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                b.innerHTML = "<strong>" + arr[i].substr(0, val.length) + "</strong>";
                b.innerHTML += arr[i].substr(val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });

    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
    });
}

function datePicker(el) {
   var inp = $(el).attr('')
}