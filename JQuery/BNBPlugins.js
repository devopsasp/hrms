var autocompleteUrl = 'WebServices/BNBPlugins.asmx/Operations';



//Bootstrap Popup
(function ($) {
    $.fn.bootstrapModal = function (option) {
        var value,
           args = Array.prototype.slice.call(arguments, 1);
        $(this).each(function () {
            var $this = $(this),
            data = $this.data('bnb.bs.modal'),
            options = $.extend({}, BootstrapModal.DEFAULTS, $this.data(),
                typeof option === 'object' && option);
            if (typeof option === 'string') {
                //if ($.inArray(option, FormCreate.allowedMethods) < 0)
                //    throw new Error("Unknown method: " + option);
                if (!data)
                    return;
                value = data[option].apply(data, args);
                if (option === 'destroy')
                    $this.removeData('bnb.bs.modal');
            }
            if (!data)
                $(this).data("bnb.bs.modal", new BootstrapModal(this, options));
        });
        return typeof value === "undefined" ? this : value;
    }
    var BootstrapModal = function (target, options) {
        this.$target = $(target);
        this.$target_ = this.$target.clone();
        this.settings = options;
        this.settings.classes = $.extend({}, BootstrapModal.DEFAULTSCLASS, this.settings.classes);
        this.settings.template = $.extend({}, BootstrapModal.TEMPLATEDEFAULT, this.settings.template);
        this.bodycont = "";
        this.init();
    };

    BootstrapModal.TEMPLATEDEFAULT = {
        modal: '<div class="modal fade" role="dialog">',
        dialog: '<div class="modal-dialog"></div>',
        content: '<div class="modal-content"></div>',
        header: '<div class="modal-header"></div>',
        title: '<h4 class="modal-title"></h4>',
        close: '<button type="button" class="close" data-dismiss="modal">&times;</button>',
        body: '<div class="modal-body">',
        footer: '<div class="modal-footer">',
        button: '<button type="button" class="btn"></button>',
    }
    BootstrapModal.DEFAULTS = {
        template: {},
        classes: {},

        header: "",
        body: "",
        footer: "",
        buttons: {},
        buttonClass: {},

        autoOpen: true,
        autoDestroy: true,
        includeColse: true,

        timeoutforhide: 200,

        defaultfocus: "",
        closeOnEscape: true,

        focusOnAfterClose: "",

        setHeight: function (that) {
            if (!that.$target.find(".modal").hasClass("alert-modal")) {
                $(".form .modal-body").height($(window).height() - 140);
                //$(".form .modal-body").mCustomScrollbar("destroy");
                //$(".form .modal-body").mCustomScrollbar({
                //    theme: "minimal",
                //    contentTouchScroll: false,
                //    scrollButtons: {
                //        enable: false,
                //    },
                //});
            }

        },
        beforeOpen: function () {
        },
        afterOpen: function () {
            $(".form .modal-body").height($(window).height() - 140);
            //$(".form .modal-body").mCustomScrollbar({
            //    theme: "minimal",
            //    contentTouchScroll: false,
            //    scrollButtons: {
            //        enable: false,
            //    },
            //});
        },
        onclose: function () {
        }
    }
    BootstrapModal.prototype.init = function () {
        this.initElements();
        this.initPanel();
        this.initHeader();
        this.initBody();
        this.initFooter();
        this.initClasses();
        if (this.settings.autoOpen) {
            this.openModal();
        }
    }

    BootstrapModal.prototype.initElements = function () {
        this.$modal = $(this.settings.template.modal);
        this.$dialog = $(this.settings.template.dialog);
        this.$content = $(this.settings.template.content);
        this.$header = $(this.settings.template.header);
        this.$closeModal = $(this.settings.template.close);
        this.$title = $(this.settings.template.title);
        this.$body = $(this.settings.template.body);
        this.$footer = $(this.settings.template.footer);
    }

    BootstrapModal.prototype.initPanel = function () {
        this.$modal.append(this.$dialog.append(this.$content));
        var that = this;
        $(document).on('keydown keypress', function (event) {
            if (event.keyCode == 13 || event.which == 13) {
                that.$target.find(that.settings.defaultfocus).trigger("click");
            }
            else if (event.keyCode == 27 || event.which == 27) {
                if (that.settings.closeOnEscape) {
                    that.closeModal();
                }
            }
        });

        this.$target.append(this.$modal);
    }

    BootstrapModal.prototype.initHeader = function () {
        var that = this;
        if (this.settings.includeColse) {
            this.$header.append(this.$closeModal);
            this.$closeModal.click(function () {
                that.closeModal();
            });
        }
        this.$title.html(this.settings.header);
        this.$header.append(this.$title);
        this.$content.append(this.$header);
    }

    BootstrapModal.prototype.initBody = function () {
        this.$body.append(this.settings.body);
        this.$content.append(this.$body);
    }

    BootstrapModal.prototype.initFooter = function () {
        var that = this;
        for (button in this.settings.buttons) {
            var btn = $(that.settings.template.button);
            btn.html(button);
            btn.off("").on("click", that.settings.buttons[button]);
            btn.addClass(that.settings.buttonClass[button]);
            that.$footer.append(btn);
        }
        this.$content.append(this.$footer);
    }

    BootstrapModal.prototype.openModal = function () {
        var that = this;
        this.settings.beforeOpen.call();
        this.$modal.addClass("in");
        this.$modal.css({ display: "block" });
        $("body").addClass("modal-open").css("overflow", "hidden");
        $("body").append("<div class='modal-backdrop in'></div>");
        this.settings.afterOpen.call();
        this.settings.setHeight.call(this, that);
    }

    BootstrapModal.prototype.closeModal = function () {
        var that = this;
        this.$modal.removeClass("in");
        if ($("body").find('.modal').length >= 1 || $("body").find('.modal').length == 0) {
            $(".modal-backdrop.in").fadeOut(200);
            $("body").removeClass("modal-open").css("overflow", "auto");
        }

        setTimeout(function () {
            that.$modal.css({ display: "none" });
            if ($("body").find('.modal').length == 1 || $("body").find('.modal').length == 0) {
                $("body").removeClass("modal-open").css("overflow", "auto");
                $(".modal-backdrop.in").remove();
            }
            if (that.settings.focusOnAfterClose != undefined && that.settings.focusOnAfterClose != null && that.settings.focusOnAfterClose != "") {
                $("#" + that.settings.focusOnAfterClose).focus();
            }
            if (that.settings.autoDestroy) {
                that.destroy();
            }
            that.settings.onclose.call();
        }, this.settings.timeoutforhide);

    }

    BootstrapModal.prototype.destroy = function () {
        this.$target.removeData('bnb.bs.modal');
        this.$target.html(this.$target_.html());
    }

    BootstrapModal.prototype.initClasses = function () {
        this.$modal.addClass(this.settings.classes.modal);
        this.$dialog.addClass(this.settings.classes.dialog);
        this.$content.addClass(this.settings.classes.content);
        this.$header.addClass(this.settings.classes.header);
        this.$closeModal.addClass(this.settings.classes.closeModal);
        this.$title.addClass(this.settings.classes.title);
        this.$body.addClass(this.settings.classes.body);
        this.$footer.addClass(this.settings.classes.footer);
    }

    BootstrapModal.DEFAULTSCLASS = {
        "modal": "modal right",
        "dialog": "modal-dialog",
        "content": "modal-content",
        "header": "modal-header",
        "close": "close",
        "title": "modal-title",
        "body": "modal-body",
        "footer": "modal-footer",
    };

}(jQuery));

//Menu loader
(function ($) {
    $.fn.menuLoader = function (option) {

        var value,
           args = Array.prototype.slice.call(arguments, 1);
        $(this).each(function () {
            var $this = $(this),
            data = $this.data('bnb.menuloader'),
            options = $.extend({}, MenuLoader.DEFAULTS, $this.data(),
                typeof option === 'object' && option);
            if (typeof option === 'string') {
                //if ($.inArray(option, FormCreate.allowedMethods) < 0)
                //    throw new Error("Unknown method: " + option);
                if (!data)
                    return;
                value = data[option].apply(data, args);
                if (option === 'destroy')
                    $this.removeData('bnb.menuloader');
            }
            if (!data)
                $(this).data("bnb.menuloader", new MenuLoader(this, options));
        });
        return typeof value === "undefined" ? this : value;

    }

    var MenuLoader = function (target, options) {
        this.$target = $(target);
        this.$target_ = this.$target.clone();
        this.settings = options;
        this.settings.template = $.extend({}, MenuLoader.TEMPLATEDEFAULT, this.settings.template);
        this.bodycont = "";
        this.init();
    }
    MenuLoader.TEMPLATEDEFAULT = {
        header: "<div class='navbar-header'></div>",
        body: "<div class='navbar-collapse'></div>",
        toogleButton: '<button type="button" class="navbar-toggle" data-toggle="collapse"></button>',
        ulLeft: "<ul class='nav navbar-nav'></ul>",
        ulRight: "<ul class='nav navbar-nav navbar-right'></ul>",
        ulDropdown: "<ul class='dropdown-menu'></ul>",
        li: "<li></li>",
        liDropdown: "<li class='dropdown'></li>",
        liAnchor: "<a></a>",
        liDropdownAnchor: "<a data-toggle='dropdown'></a>",
        liDropdownIndicator: '<span class="caret"></span>',
        emptyliDiv: "<div></div>",
        emptyli: "<li></li>",
        iconspan: "<span></span>",
        imgicon: "<img />"
    }

    MenuLoader.DEFAULTS = {
        template: {},
        emptyli: false,
        emptyliAttr: {},
        emptyliClass: "visible-md visible-lg",
        data: [],
        logoHtml: "",
        pushStateFunction: function (event, title, url) {
            if (event.ctrlKey) {
                window.open($(this).data("menu-link"));
            }
            else {
                window.location.replace($(this).data("menu-link"));
            }
        },
    }
    MenuLoader.MENUDEFAULTS = {
        enc_menu_id: 0,
        MenuName: "",
        MenuLink: "javascript:void(0)",
        enc_parent_id: "",
        description: "",
        Menu_icon: "",
        align: "left",

        liclass: "",
        liattr: {},
        liAnchorclass: "",
        liAnchorattr: {},
        liAnchorClick: function () {

        },
        dropdownMenuclass: "",
        dropdownMenuContainer: "",
        dropdownMenuattr: {},

        showDropdownIndicator: true,
        anchorAlternate: "",
        removeOldDropdownClass: false,

        onDropdownShow: function (event) { },
        onDropdownShown: function (event) { },
        onDropdownHide: function (event) { },
        onDropdownHidden: function (event) { },
    }
    MenuLoader.prototype.init = function () {
        this.initStructure();
        this.initMenuHeader();
        this.initMenu();
    }

    MenuLoader.prototype.initStructure = function () {
        var that = this;
        this.$header = $(this.settings.template.header);
        this.$ulRight = $(this.settings.template.ulRight);
        this.$ulLeft = $(this.settings.template.ulLeft);

        this.$target.append(this.$header);
        this.$body = [];
        if (Array.isArray(this.settings.template.body)) {
            this.settings.template.body.forEach(function (val, index) {
                that.$body.push($(val));
                that.$target.append(that.$body[index]);
            });
        }
        else {
            this.$body = [$(this.settings.template.body)];
            this.$target.append(this.$body);
        }

        this.$body[0].append(this.$ulLeft);
        this.$body[0].append(this.$ulRight);
    }

    MenuLoader.prototype.initMenuHeader = function () {
        var that = this;
        this.$toggleBtn = $(that.settings.template.toogleButton).click(function () {
            that.$body[0].collapse("toggle");
        });
        this.$body[0].on("show.bs.collapse", function () {
            that.$toggleBtn.addClass("menu-opened");
        });
        this.$body[0].on("hide.bs.collapse", function () {
            that.$toggleBtn.removeClass("menu-opened");
        });

        this.$header.html(this.$toggleBtn);
        this.$header.append(this.settings.logoHtml);
    }

    MenuLoader.prototype.initMenu = function () {
        var that = this;
        if (that.settings.emptyli) {
            that.$body.forEach(function (targetBody, bodyIndex) {
                var emptyOption;
                if (bodyIndex > 0) {
                    emptyOption = $(that.settings.template.emptyliDiv);
                }
                else {
                    targetBody = that.$ulLeft;
                    emptyOption = $(that.settings.template.emptyli);
                }
                emptyOption.addClass(that.settings.emptyliClass);
                emptyOption.attr(that.settings.emptyliAttr);

                targetBody.append(emptyOption);
            });
        }

        if (Array.isArray(that.settings.data)) {
            that.settings.data.forEach(function (menudata, menuIndex) {
                that.createMenu(menudata, menuIndex);
            });
        }
    }

    MenuLoader.prototype.destroy = function () {
        this.$target.html(this.$target_.html());
        this.$target.removeData("bnb.menuloader");
        this.settings = {};
    }

    MenuLoader.prototype.createMenu = function (createObj, menuIndex) {
        var that = this;
        createObj = $.extend({}, MenuLoader.MENUDEFAULTS, createObj);
        var menuOption;
        var menuAnchor;
        var isParent = that.isParent(createObj);

        var href = "javascript:void(0)";
        if (isParent) {
            menuOption = $(that.settings.template.liDropdown);
            menuAnchor = $(that.settings.template.liDropdownAnchor);
            menuAnchor.click(function () {
                //event.preventDefault();
                //event.stopPropagation();
                menuAnchor.parent().siblings().removeClass('open');
                menuAnchor.parent().toggleClass('open');
                return false;
            });
            if (createObj.enc_parent_id != undefined && createObj.enc_parent_id != null && createObj.enc_parent_id != "") {
                menuOption.addClass("dropdown-submenu");
                menuAnchor.addClass("dropdown-toggle");
                //menuAnchor.click(function (event) {
                //    //$(".dropdown-submenu").find("ul").show();
                //    menuAnchor.next('ul').toggle();
                //    event.stopPropagation();
                //});
            }

        }
        else {
            menuOption = $(that.settings.template.li);
            menuAnchor = $(that.settings.template.liAnchor);
            href = createObj.MenuLink;
            menuAnchor.click(function () {
                //that.settings.pushStateFunction.call(this, event, createObj.MenuName, createObj.MenuLink);
                createObj.liAnchorClick.call(this);
                //event.stopPropagation();
                //event.preventDefault();
            });
        }

        if (createObj.anchorAlternate != undefined && createObj.anchorAlternate != null && createObj.anchorAlternate != "") {
            menuAnchor = $(createObj.anchorAlternate);
        }

        menuOption.addClass(createObj.liclass);
        menuOption.attr($.extend({}, createObj.liattr, { "data-menu-id": createObj.enc_menu_id, "data-menu-index": menuIndex }));

        menuAnchor.html('');
        if (createObj.Menu_icon != undefined && createObj.Menu_icon != null && createObj.Menu_icon != "") {
            var icon;
            if (createObj.Menu_icon.substr(createObj.Menu_icon.lastIndexOf(".") + 1) != createObj.Menu_icon) {
                icon = $(that.settings.template.imgicon);
                icon.attr({ "src": createObj.Menu_icon, "style": "width:25px;margin-right:5px" });
            }
            else {
                icon = $(that.settings.template.iconspan);
                icon.css({ "font-size": "16px", "margin-right": "7px" });
                icon.addClass("glyphicon glyphicon-" + createObj.Menu_icon);
                //icon.html("&nbsp;")
            }
            menuAnchor.append(icon);
        }

        menuAnchor.append(createObj.MenuName);
        menuAnchor.addClass(createObj.liAnchorclass);
        menuAnchor.attr($.extend({}, createObj.liAnchorattr, { "data-menu-link": createObj.MenuLink, "href": href, "title": createObj.description }));
        if (createObj.description != undefined && createObj.description != null && createObj.description != "") {
            menuAnchor.tooltip({ placement: "bottom" });
        }
        menuOption.html('').append(menuAnchor);
        if (isParent) {
            if (createObj.showDropdownIndicator) {
                menuAnchor.append($(that.settings.template.liDropdownIndicator));
            }
            var dropdownMenu = $(that.settings.template.ulDropdown);
            if (createObj.removeOldDropdownClass) {
                dropdownMenu.removeAttr("class");
            }

            dropdownMenu.addClass(createObj.dropdownMenuclass);
            dropdownMenu.attr($.extend({}, createObj.dropdownMenuattr, { "data-ul-container-for": createObj.enc_menu_id }));

            dropdownMenu.on("hide.bs.dropdown", createObj.onDropdownHide);
            dropdownMenu.on("hidden.bs.dropdown", createObj.onDropdownHidden);
            dropdownMenu.on("show.bs.dropdown", createObj.onDropdownShow);
            dropdownMenu.on("shown.bs.dropdown", createObj.onDropdownShown);

            if (createObj.dropdownMenuContainer != undefined && createObj.dropdownMenuContainer != null && createObj.dropdownMenuContainer != "") {
                $(createObj.dropdownMenuContainer).append(dropdownMenu).appendTo(menuOption);
            }
            else {
                menuOption.append(dropdownMenu);
            }
        }

        var appendTarget;
        if (createObj.align == "right") {
            appendTarget = that.$ulRight;
        }
        else {
            appendTarget = that.$ulLeft;
        }
        if (createObj.enc_parent_id != undefined && createObj.enc_parent_id != null && createObj.enc_parent_id != "") {
            if (that.$target.find("[data-menu-id='" + createObj.enc_parent_id + "'] [data-ul-container-for='" + createObj.enc_parent_id + "']").length > 0) {
                appendTarget = that.$target.find("[data-menu-id='" + createObj.enc_parent_id + "'] [data-ul-container-for='" + createObj.enc_parent_id + "']");
            }
        }
        appendTarget.append(menuOption);
    }

    MenuLoader.prototype.getParents = function (obj) {
        if (typeof obj == "object") {
            obj = obj.enc_parent_id;
        }
        return filterArrayFromArrayByColumn(this.settings.data, "enc_menu_id", obj);
    }

    MenuLoader.prototype.getChilds = function (obj) {
        if (typeof obj == "object") {
            obj = obj.enc_menu_id;
        }
        return filterArrayFromArrayByColumn(this.settings.data, "enc_parent_id", obj);
    }

    MenuLoader.prototype.isParent = function (obj) {
        if (obj == undefined || obj == null || obj == "") {
            return;
        }

        if (typeof obj == "object") {
            obj = obj.enc_menu_id;
        }
        var objj = filterObjectFromArrayByColumn(this.settings.data, "enc_parent_id", obj);
        return (objj != undefined && objj != null && objj != "" && typeof objj == "object");
    }

    var filterObjectFromArrayByColumn = function (data, field, value) {
        return $.grep(data,
                function (row) {
                    if (Array.isArray(value)) {
                        return value.indexOf(row[field]) > -1;
                    }
                    else {
                        return row[field] == value;
                    }
                })[0];
    }
    var filterArrayFromArrayByColumn = function (data, field, value) {
        return $.grep(data,
                function (row) {
                    if (Array.isArray(value)) {
                        return value.indexOf(row[field]) > -1;
                    }
                    else {
                        return row[field] == value;
                    }
                });
    }
    var removeObjectFromArrayByColumn = function (data, field, value) {
        return $.grep(data,
                function (row) {
                    if (Array.isArray(value)) {
                        return value.indexOf(row[field]) == -1;
                    }
                    else {
                        return row[field] != value;
                    }
                });
    }
}(jQuery));

//Table Export
(function ($) {

    'use strict';
    var DownloadEvt = null;
    var sprintf = function (str) {
        var args = arguments,
            flag = true,
            i = 1;

        str = str.replace(/%s/g, function () {
            var arg = args[i++];

            if (typeof arg === 'undefined') {
                flag = false;
                return '';
            }
            return arg;
        });
        return flag ? str : '';
    };
    var $table = $('#tblUser');

    var TYPE_NAME = {
        json: 'JSON',
        xml: 'XML',
        png: 'PNG',
        csv: 'CSV',
        tsv: 'TSV',
        txt: 'TXT',
        sql: 'SQL',
        doc: 'MS-Word',
        excel: 'Excel',
        xlsx: 'Excel (OpenXML)',
        //powerpoint: 'MS-Powerpoint',
        pdf: 'PDF'
    };

    $.extend($.fn.bootstrapTable.defaults, {
        exportByServerSide: true,
        showExport: false,
        pagination: false,
        exportDataType: 'all', // basic, all, selected
        exportTypes: ['excel'], //exportTypes: [ 'json', 'xml', 'png', 'csv', 'txt', 'sql', 'doc', 'excel', 'pdf'],
        exportOptions: {
            pagination: false,
        },
        consoleLog: false,
        csvEnclosure: '"',
        csvSeparator: ',',
        csvUseBOM: true,
        displayTableName: false,
        escape: false,
        excelstyles: [],       // e.g. ['border-bottom', 'border-top', 'border-left', 'border-right']
        fileName: 'tableExport',
        footer: '',
        htmlContent: false,
        header: '',
        ignoreColumn: [],
        ignoreRow: [],
        jsonScope: 'all', // head, data, all
        jspdf: {
            orientation: 'p',
            unit: 'pt',
            format: 'a4', // jspdf page format or 'bestfit' for autmatic paper format selection
            margins: { left: 20, right: 10, top: 10, bottom: 10 },
            autotable: {
                styles: {
                    cellPadding: 2,
                    rowHeight: 12,
                    fontSize: 8,
                    fillColor: 255,        // color value or 'inherit' to use css background-color from html table
                    textColor: 50,         // color value or 'inherit' to use css color from html table
                    fontStyle: 'normal',   // normal, bold, italic, bolditalic or 'inherit' to use css font-weight and fonst-style from html table
                    overflow: 'ellipsize', // visible, hidden, ellipsize or linebreak
                    halign: 'left',        // left, center, right
                    valign: 'middle'       // top, middle, bottom
                },
                headerStyles: {
                    fillColor: [52, 73, 94],
                    textColor: 255,
                    fontStyle: 'bold',
                    halign: 'center'
                },
                alternateRowStyles: {
                    fillColor: 245
                },
                tableExport: {
                    onAfterAutotable: null,
                    onBeforeAutotable: null,
                    onTable: null,
                    outputImages: true
                }
            }
        },
        numbers: {
            html: {
                decimalMark: '.',
                thousandsSeparator: ','
            },
            output: // set to false to not format numbers in exported output
                    {
                        decimalMark: '.',
                        thousandsSeparator: ','
                    }
        },
        onCellData: null,
        onCellHtmlData: null,
        outputMode: 'file',  // 'file', 'string', 'base64' or 'window' (experimental)
        tbodySelector: 'tr',
        tfootSelector: 'tr', // set empty ('') to prevent export of tfoot rows
        theadSelector: 'tr',
        tableName: 'myTableName',
        worksheetName: 'xlsWorksheetName'
    });

    $.extend($.fn.bootstrapTable.defaults.icons, {
        export: "glyphicon glyphicon-export"
    });

    $.extend($.fn.bootstrapTable.locales, {
        formatExport: function () {
            return 'Export data';
        }
    });
    $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales);

    var BootstrapTable = $.fn.bootstrapTable.Constructor,
        _initToolbar = BootstrapTable.prototype.initToolbar;
    function downloadFile(filename, header, data) {

        var ua = window.navigator.userAgent;
        if (filename !== false && (ua.indexOf("MSIE ") > 0 || !!ua.match(/Trident.*rv\:11\./))) {
            if (window.navigator.msSaveOrOpenBlob)
                window.navigator.msSaveOrOpenBlob(new Blob([data]), filename);
            else {
                // Internet Explorer (<= 9) workaround by Darryl (https://github.com/dawiong/tableExport.jquery.plugin)
                // based on sampopes answer on http://stackoverflow.com/questions/22317951
                // ! Not working for json and pdf format !
                var frame = document.createElement("iframe");

                if (frame) {
                    document.body.appendChild(frame);
                    frame.setAttribute("style", "display:none");
                    frame.contentDocument.open("txt/html", "replace");
                    frame.contentDocument.write(data);
                    frame.contentDocument.close();
                    frame.focus();

                    frame.contentDocument.execCommand("SaveAs", true, filename);
                    document.body.removeChild(frame);
                }
            }
        }
        else {
            var DownloadLink = document.createElement('a');

            if (DownloadLink) {
                var blobUrl = null;

                DownloadLink.style.display = 'none';
                if (filename !== false)
                    DownloadLink.download = filename;
                else
                    DownloadLink.target = '_blank';

                if (typeof data == 'object') {
                    blobUrl = window.URL.createObjectURL(data);
                    DownloadLink.href = blobUrl;
                }
                else if (header.toLowerCase().indexOf("base64,") >= 0)
                    DownloadLink.href = header + base64encode(data);
                else
                    DownloadLink.href = header + encodeURIComponent(data);

                document.body.appendChild(DownloadLink);

                if (document.createEvent) {
                    if (DownloadEvt === null)
                        DownloadEvt = document.createEvent('MouseEvents');

                    DownloadEvt.initEvent('click', true, false);
                    DownloadLink.dispatchEvent(DownloadEvt);
                }
                else if (document.createEventObject)
                    DownloadLink.fireEvent('onclick');
                else if (typeof DownloadLink.onclick == 'function')
                    DownloadLink.onclick();

                if (blobUrl)
                    window.URL.revokeObjectURL(blobUrl);

                document.body.removeChild(DownloadLink);
            }
        }
    }

    function base64encode(input) {
        var keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;
        input = utf8Encode(input);
        while (i < input.length) {
            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);
            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;
            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }
            output = output +
                    keyStr.charAt(enc1) + keyStr.charAt(enc2) +
                    keyStr.charAt(enc3) + keyStr.charAt(enc4);
        }
        return output;
    }
    function utf8Encode(string) {
        string = string.replace(/\x0d\x0a/g, "\x0a");
        var utftext = "";
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }
        return utftext;
    }
    BootstrapTable.prototype.initToolbar = function () {
        this.showToolbar = this.options.showExport;

        _initToolbar.apply(this, Array.prototype.slice.apply(arguments));

        if (this.options.showExport) {
            var that = this,
                $btnGroup = this.$toolbar.find('>.btn-group'),
                $export = $btnGroup.find('div.export');

            if (!$export.length) {
                $export = $([
                    '<div class="export btn-group">',
                        '<button class="btn' +
                            sprintf(' btn-%s', this.options.buttonsClass) +
                            sprintf(' btn-%s', this.options.iconSize) +
                            ' dropdown-toggle" aria-label="export type" ' +
                            'title="' + this.options.formatExport() + '" ' +
                            'data-toggle="dropdown" type="button">',
                            sprintf('<i class="%s %s"></i> ', this.options.iconsPrefix, $.fn.bootstrapTable.defaults.icons.export),
                            '<span class="caret"></span>',
                        '</button>',
                        '<ul class="dropdown-menu" role="menu">',
                        '</ul>',
                    '</div>'].join('')).appendTo($btnGroup);

                var $menu = $export.find('.dropdown-menu'),
                    exportTypes = this.options.exportTypes;

                if (typeof this.options.exportTypes === 'string') {
                    var types = this.options.exportTypes.slice(1, -1).replace(/ /g, '').split(',');

                    exportTypes = [];
                    $.each(types, function (i, value) {
                        exportTypes.push(value.slice(1, -1));
                    });
                }
                $.each(exportTypes, function (i, type) {
                    if (TYPE_NAME.hasOwnProperty(type)) {
                        $menu.append(['<li role="menuitem" data-type="' + type + '">',
                                '<a href="javascript:void(0)">',
                                    TYPE_NAME[type],
                                '</a>',
                            '</li>'].join(''));
                    }
                });

                $menu.find('li').click(function () {
                    if (false) {
                        var data = that.data, columns = that.columns;
                        var defaults = that.options;
                        defaults.type = $(this).data('type');
                        var html = $("<table><thead><tr class='header'></tr></thead><tbody></tbody><tfoot></tfoot></table>");
                        columns.forEach(function (column, colIndex) {
                            if (column.field != "" && column.field != undefined && column.field != null && $.inArray(column.field, defaults.ignoreColumn) == -1) {
                                html.find("thead .header").append("<th>" + column.title + "</th>");
                            }
                        });
                        data.forEach(function (row, index) {
                            var tr = $("<tr></tr>");
                            columns.forEach(function (column, colIndex) {
                                if (column.field != "" && column.field != undefined && column.field != null && $.inArray(column.field, defaults.ignoreColumn) == -1) {
                                    if (row[column.field] == undefined && row[column.field] == null) {
                                        tr.append("<td></td>");
                                    }
                                    else {
                                        tr.append("<td>" + row[column.field] + "</td>");
                                    }

                                }
                            });
                            html.find("tbody").append(tr);
                        });
                        var MSDocType = (defaults.type == 'excel' || defaults.type == 'xls') ? 'excel' : 'word';
                        var MSDocExt = (MSDocType == 'excel') ? 'xls' : 'doc';
                        var MSDocSchema = 'xmlns:x="urn:schemas-microsoft-com:office:' + MSDocType + '"';

                        var docFile = '<html xmlns:o="urn:schemas-microsoft-com:office:office" ' + MSDocSchema + ' xmlns="http://www.w3.org/TR/REC-html40">';
                        docFile += '<meta http-equiv="content-type" content="application/vnd.ms-' + MSDocType + '; charset=UTF-8">';
                        docFile += "<head>";
                        if (MSDocType === 'excel') {
                            docFile += "<!--[if gte mso 9]>";
                            docFile += "<xml>";
                            docFile += "<x:ExcelWorkbook>";
                            docFile += "<x:ExcelWorksheets>";
                            docFile += "<x:ExcelWorksheet>";
                            docFile += "<x:Name>";
                            docFile += defaults.worksheetName;
                            docFile += "</x:Name>";
                            docFile += "<x:WorksheetOptions>";
                            docFile += "<x:DisplayGridlines/>";
                            docFile += "</x:WorksheetOptions>";
                            docFile += "</x:ExcelWorksheet>";
                            docFile += "</x:ExcelWorksheets>";
                            docFile += "</x:ExcelWorkbook>";
                            docFile += "</xml>";
                            docFile += "<![endif]-->";
                        }
                        docFile += "<style>br {mso-data-placement:same-cell;}</style>";
                        docFile += "</head>";
                        docFile += "<body>";
                        docFile += "<table border='1'>" + html.html() + "</table>";
                        docFile += "</body>";
                        docFile += "</html>";

                        try {
                            blob = new Blob([docFile], { type: 'application/vnd.ms-' + defaults.type });
                            saveAs(blob, defaults.fileName + '.' + MSDocExt);
                        }
                        catch (e) {

                            downloadFile(defaults.fileName + '.' + MSDocExt,
                                         'data:application/vnd.ms-' + MSDocType + ';base64,',
                                         docFile);
                        }
                        return false;
                    }
                    if (that.options.exportByServerSide) {
                        that.$el.tableExport($.extend({}, that.options.exportOptions, {
                            type: type,
                            fileName: that.options.fileName,
                            worksheetName: that.options.worksheetName,
                            header: that.options.header,
                            footer: that.options.footer,
                            tableName: that.options.tableName,
                            escape: false,
                            consoleLog: that.options.consoleLog,
                            outputMode: that.options.outputMode,  // 'file', 'string', 'base64' or 'window' (experimental)
                        }));
                        return false;
                    }
                    var type = $(this).data('type'),
                        doExport = function () {
                            //that.togglePagination();
                            that.$el.tableExport($.extend({}, that.options.exportOptions, {
                                type: type,
                                fileName: that.options.fileName,
                                worksheetName: that.options.worksheetName,
                                header: that.options.header,
                                footer: that.options.footer,
                                tableName: that.options.tableName,
                                escape: false,
                                consoleLog: that.options.consoleLog,
                                outputMode: that.options.outputMode,  // 'file', 'string', 'base64' or 'window' (experimental)
                            }));
                            //that.togglePagination();
                        };

                    if (that.options.exportDataType === 'all' && that.options.pagination) {
                        that.$el.one(that.options.sidePagination === 'server' ? 'post-body.bs.table' : 'page-change.bs.table', function () {
                            doExport();
                            that.togglePagination();
                        });
                        that.togglePagination();
                    } else if (that.options.exportDataType === 'selected') {
                        var data = that.getData(),
                            selectedData = that.getAllSelections();

                        // Quick fix #2220
                        if (that.options.sidePagination === 'server') {
                            data = { total: that.options.totalRows };
                            data[that.options.dataField] = that.getData();

                            selectedData = { total: that.options.totalRows };
                            selectedData[that.options.dataField] = that.getAllSelections();
                        }

                        that.load(selectedData);
                        doExport();
                        that.load(data);
                    } else {
                        doExport();
                    }
                });
            }
        }
    };
})(jQuery);

//Bind
(function ($) {
    $.fn.binder = function (objDegree) {
        $.fn.bindElements(this, { objData: objDegree })
    }
}(jQuery));

//PDF object Viewer
//$.fn.pdfViewer = function (options) {
//    PDFObject.embed(options.fileName, this, options);
//}
//}(jQuery));

//PDF Viewer
(function ($) {
    $.fn.pdfViewer = function (options) {
        var opt = $.extend(PDFViewer.DEFAULTS, options);
        new PDFViewer(this, opt);
    }

    var PDFViewer = function (target, options) {
        this.$target = target;
        this.settings = options;
        this._init();
    };

    PDFViewer.prototype._init = function () {
        getViewer(this.settings.fileName);
    }

    PDFViewer.DEFAULTS = {
        fileName: "",
    }
}(jQuery));

//File upload
(function ($) {
    $.fn.fileUploader = function (options) {
        var opt = $.extend(FILEUploader.DEFAULTS, options);
        new FILEUploader(this, opt);
    }

    var FILEUploader = function (target, options) {
        this.$target = target;
        this.settings = options;
        this._init();
    };

    FILEUploader.prototype._init = function () {
        $(this.$target).fileUpload({
            contextMenuId: this.settings.contextMenuId,
            fileAllowed: this.settings.fileAllowed,
            viewImageperRow: this.settings.viewImageperRow,
            files: this.settings.files,
            header: this.settings.header,
            fileExtension: this.settings.fileExtension,
            fileSavePath: this.settings.fileSavePath,
            uploadFileHeight: this.settings.uploadFileHeight,
            uploadFileWidth: this.settings.uploadFileWidth,
            onAdd: this.settings.onAdd,
            onDownload: this.settings.onDownload,
            onDelete: this.settings.onDelete,
            //onRename: this.settings.onRename,
        });
        //$(this.$target).fileUpload("destroy");
    }

    FILEUploader.DEFAULTS = {
        contextMenuId: undefined,
        fileAllowed: undefined,
        viewImageperRow: 5,
        files: [],
        header: 'File Upload',
        fileExtension: ["jpg", "png", "jpeg", "gif", "pdf", "xls", "doc", "xlsx", "docx", "mp4", "rtf", "ods", "odt", "zip", "txt", "rar"],
        fileSavePath: 'uploads/',
        uploadFileHeight: 100,
        uploadFileWidth: 100,
        onAdd: function (files) {
            return false;
        },
        onDownload: function (files) {
            return false;
        },
        onDelete: function (file) {
            return false;
        },
        onRename: function (file) {
            return false;
        }
    }
}(jQuery));

//Element Creation
(function ($) {
    var reg = {
        likeVariable: /^[a-zA-Z][a-zA-Z0-9_]*?$/
    };
    var elementEnum = {
        option: "<option></option>",
        button: "<button type='button' data-element-type='button' class='btn'></button>",
        text: "<input type='text'  data-element-type='text' class='form-control input-sm' />",
        textarea: "<textarea data-element-type='textarea' class='form-control input-sm' style='resize:vertical;'></textarea>",

        int: "<input type='text' data-element-type='int' data-type='int' class='form-control input-sm' />",
        float: "<input type='text' data-element-type='float' data-type='float' class='form-control input-sm' />",
        str: "<input type='text' data-element-type='str' data-type='str' class='form-control'/>",
        strint: "<input type='text' data-element-type='strint' data-type='strint' class='form-control input-sm' />",
        email: "<input type='text' data-element-type='email' data-type='email' class='form-control input-sm' />",
        password: "<input type='password' data-element-type='password' class='form-control input-sm' />",

        select: "<select class='drp form-control input-sm inpselect' data-defaultval='' data-element-type='select'></select>",
        checkbox: "<input type='checkbox' data-element-type='checkbox'/>",
        radio: "<input type='radio' data-element-type='radio' />",
        autoComplete: "<input type='text' data-element-type='autoComplete' class='form-control input-sm ' style='background:#F8F8F8;'/>",
        label: "<label data-element-type='label' class='fieldname'></label>",
        autogenerate: "<label data-element-type='autogenerate'></label>",
        span: "<span></span>",

        date: "<input type='text' data-element-type='date' class='form-control input-sm' />",
        ldate: "<input type='text' data-element-type='ldate' class='form-control input-sm' />",
        udate: "<input type='text' data-element-type='udate' class='form-control input-sm' />",
        datetime: "<input type='text' data-element-type='datetime' class='form-control input-sm' />",
        ldatetime: "<input type='text' data-element-type='ldatetime' class='form-control input-sm' />",
        udatetime: "<input type='text' data-element-type='udatetime' class='form-control input-sm' />",
        time: "<input type='text' data-element-type='time' class='form-control input-sm' />",
        ltime: "<input type='text' data-element-type='ltime' class='form-control input-sm' />",
        utime: "<input type='text' data-element-type='utime' class='form-control input-sm' />",
        file: "<input type='file' data-element-type='file' style='display:none' />",

        sum: "<input type='text' data-element-type='sum' data-type='float' class='form-control input-sm' />",
        count: "<input type='text' data-element-type='count' data-type='float' class='form-control input-sm' />",
        min: "<input type='text' data-element-type='min' data-type='float' class='form-control input-sm' />",
        max: "<input type='text' data-element-type='max' data-type='float' class='form-control input-sm' />",
        avg: "<input type='text' data-element-type='avg' data-type='float' class='form-control input-sm' />",

        selectize: "<input type='text' data-element-type='selectize' class='' style='background:#F8F8F8;'/>",
        numbercalc: "<label data-element-type='numbercalc' class='fieldname'></label>",
        dateaddcalc: "<label data-element-type='dateaddcalc' class='fieldname'></label>",
        dateadiffcalc: "<label data-element-type='dateadiffcalc' class='fieldname'></label>",
        textcalc: "<label data-element-type='textcalc' class='fieldname'></label>",
    };
    var htmls = {
        file: "<button type='button' class='' data-element-type='button-file'>Upload File</button>",
    };
    var fileUploads = {
        image: ["jpg", "png", "jpeg", "gif"],
        pdf: ["pdf"]
    };

    var dataEnum = {
        elementPanel: ["data-element-panel-unique-name"],
        elementContainer: ["data-element-container-unique-name"],
        labelContainer: ["data-label-container-unique-name"]
    }
    var validateEnum = {
        require: 'mandatory',
        phonenumber: 'phonenumber',
        date: 'date',
        time: 'time',
        dateTime: 'dateTime',
    }
    var dateEnum = ["ldate", "udate", "date"];
    var dateTimeEnum = ["ldatetime", "udatetime", "datetime"];
    var timeEnum = ["ltime", "utime", "time"];
    var datePick = dateEnum.concat(dateTimeEnum, timeEnum);

    var calculateFields = ["int", "decimal"];

    var throwError = function (str) {
        throw new Error(str);
    }
    var checkAll = function (element) {
        if (element.find("[data-element-type='checkbox']").length ==
            element.find("[data-element-type='checkbox']:checked").length)
            element.find("[data-element-type='checkAll']").prop("checked", true);
        else
            element.find("[data-element-type='checkAll']").prop("checked", false);
    }
    var filterValFromJSON = function (json, str) {
        var arr = [];
        if (json.length > 0) {
            json.forEach(function (row) {
                arr.push(row[str]);
            });
        }
        return arr;
    };
    var getDataAttr = function (input, arr) {
        if (input == "") {
            return;
        }
        var obj = {};
        dataEnum[input].forEach(function (str, index) {
            obj[str] = arr[index];
        });
        return obj;
    };
    var filterObjectFromArrayByColumn = function (data, field, value) {
        return $.grep(data,
                function (row) {
                    return row[field] == value;
                })[0];
    }
    var filterArrayFromArrayByColumn = function (data, field, value) {
        return $.grep(data,
                function (row) {
                    if (Array.isArray(value)) {
                        return value.indexOf(row[field]) > -1;
                    }
                    else {
                        return row[field] == value;
                    }

                });
    }

    Function.prototype.clone = function () {
        var that = this;
        var temp = function temporary() { return that.apply(this, arguments); };
        for (var key in this) {
            if (this.hasOwnProperty(key)) {
                temp[key] = this[key];
            }
        }
        return temp;
    };

    var FormCreate = function (target, options) {
        this.$el = $(target);
        this.$el_ = this.$el.clone();
        this.options = options;
        this.init();
    }

    FormCreate.DEFAULTS = {
        overallElementContainer: "<div class='col-md-12 divElements no-margin no-padding'></div>",
        overallButtonContainer: "<div class='col-md-12 divBtns no-margin no-padding'></div>",
        data: [{}],
        classes: "container",
        elementPanel: "<div class='col-md-12 form-group'></div>",
        labelContainer: "<div class='col-md-4 label-selector'></div>",
        elementContainer: "<div class='col-md-8 input-selector'></div>",
        beforeLabel: "",
        afterElement: "",
        callValidate: false,
        validateOptions: {},
        showAlsoVisibleFalse: false,
        VisibleFalseFieldClass: "visible-false-field",
        buttonContainer: "<center></center>",
        buttonContainerSelector: "center",
        afterCreation: function () {
            $(".input input,.input select,.input textarea").on("focus", function () {
                $(this).parents(".input").addClass("focused");
            });
            $(".input input,.input select,.input textarea").on("blur", function () {
                $(this).parents(".input").removeClass("focused");
            });
        }
    };
    FormCreate.FIELDSDEFAULTS = {
        type: "text",
        label: "",
        value: "",
        labelClass: "lbl",
        elementClass: "",
        uniqueName: "",
        validateName: "",
        attr: {},

        disabled: false,

        multiple: false,
        multipleOptions: {},
        labelField: "label",
        valueField: "value",
        validate: {},
        withoutLabel: false,
        elementPanel: undefined,
        labelContainer: undefined,
        elementContainer: undefined,
        beforeLabel: undefined,
        afterElement: undefined,
        elementFirst: false,
        maximumLength: "",

        autocompleteUrl: "WebServices/BNBPlugins.asmx/Operations",

        isRequire: false,
        mentionRequireSymbol: true,
        requireSymbol: "*",
        requireSymbolClass: "",

        visible: true,
        date: {},

        showOnGrid: true,

        file: {},
        events: {},

        IncludeTimestampIcon: "CSS/Images/IncludeTimestamp.png",
        IncludeTimestampIconClick: function (target) {

        },

        DependancyFieldFrom: undefined,
        DependancyField: undefined,
        CalculateField: undefined,

        zeroVal: "RmFuYWxfRnVuYWRpbmdfOTg3MEZhbmFsX0Z1bmFkaW5nXzk4Nw==",
        selectize: {
        }
    };
    FormCreate.VALIDATEDEFAULS = {
        type: "",
        character: "",
        length: "",
        range: "",
        mask: "",
        defaultval: "",
        groupname: "",
    };
    FormCreate.FILEDEFAULS = {
        path: "uploads/",
        allowFiles: [],

        //alignment
        imagewidth: "50",
        imageheight: "50",
        renderAfter: false,
        imageClass: "img",
        spaceBetween: "20px",

        fileSize: "",

        buttonClass: "btn btn-primary",
        buttonText: "Choose File",
        onChangeClass: "btn btn-danger",
        onChangeText: "Change File",

        //upload
        handlerPath: "fileUploader.ashx",

        multiOptions: {}

    };
    FormCreate.SELECTIZEDEFAULTS = {
        plugins: ['remove_button', 'drag_drop', 'restore_on_backspace'],
        persist: false,
        delimiter: ',',
        create: true,
        duplicates: true,
        dataAttr: 'data',
        searchField: ['iscol'],
        //render: {
        //    item: function (data, escape) {
        //        return '<div  data-iscol=1>"' + escape(data.label) + '"</div>';
        //    },
        //    //item: function (data) {

        //    //    return "<div data-value='" + data.value + "' data-iso='" + data.population + "' data-type='" + data.type + "' class='item'>" + escape(data.label) + " </div>";
        //    //}
        //},
    }
    /**************************
     * functions
     **************************/
    FormCreate.prototype.destroy = function () {
        this.$el.html(this.$el_.html());
        //this.$el.find("> .divElements").remove();
        //this.$el.find("> .divBtns").remove();
    };

    FormCreate.prototype.init = function () {
        this.createParentElement();
        this.initElement();
        this.elementDetails();
    };

    FormCreate.prototype.createParentElement = function () {
        this.$element = this.$el;
        this.$element.append($(this.options.overallElementContainer));
        this.$element.append($(this.options.overallButtonContainer).html(this.options.buttonContainer));
        this.$elementBtn = this.$element.find(".divBtns " + this.options.buttonContainerSelector);
        this.$element = this.$element.find(".divElements");
    };

    FormCreate.prototype.initElement = function (bln) {
        var that = this;
        if (bln) {
            that.destroy();
            that.createParentElement();
        }

        that.options.data.forEach(function (obj, index) {
            var row = $.extend({}, FormCreate.FIELDSDEFAULTS, obj);
            if (row.elementPanel == undefined) {
                row.elementPanel = that.options.elementPanel;
            }
            if (row.labelContainer == undefined) {
                row.labelContainer = that.options.labelContainer;
            }
            if (row.elementContainer == undefined) {
                row.elementContainer = that.options.elementContainer;
            }
            if (row.beforeLabel == undefined) {
                row.beforeLabel = that.options.beforeLabel;
            }
            if (row.afterElement == undefined) {
                row.afterElement = that.options.afterElement;
            }
            if (!row.multiple && row.type == "autoComplete" && row.type == "select") {
                row.multiple = true;
            }
            that.createElement(row, index);
        });

        if (that.options.callValidate) {
            that.$el.bnbValidate("destroy");
            that.$el.bnbValidate(that.options.validateOptions);
        }
        if (bln) {
            that.elementDetails();
        }
    };

    FormCreate.prototype.uploadFiles = function (arrUniqueName, callBack) {
        if (arrUniqueName == undefined || arrUniqueName == null || arrUniqueName == "")
            arrUniqueName = [];
        var that = this;
        if (arrUniqueName.length > 0) {
            arrUniqueName.forEach(function (val) {
                var row = that.getOption(val);
                if (row.visible) {
                    if (row.multiple) {
                        that.$el.find("[data-element-container-unique-name='" + row.uniqueName + "']").fileUpload("uploadFiles", callBack);
                    }
                    else {
                        that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").each(function () {
                            that.uploadFile(this.files, row.file, callBack);
                        });
                    }
                }
                else {
                    callBack.call();
                }
            });
        }
        else {
            var arr = filterArrayFromArrayByColumn(that.options.data, "visible", true);
            arr = filterArrayFromArrayByColumn(arr, "type", "file");
            var arrMultiple = filterArrayFromArrayByColumn(arr, "multiple", true);
            arr = filterArrayFromArrayByColumn(arr, "multiple", [undefined, false, null]);
            var inarr = 0;
            var inmulti = 0;
            if (arr.length > 0) {
                arr.forEach(function (row) {

                    that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").each(function () {
                        var fileLen = this.files.length;
                        that.uploadFile(this.files, row.file, function (uploadedFiles) {
                            inarr += 1;
                            if (fileLen > 0) {
                                if (that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").siblings("img").length > 0) {
                                    that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").siblings("img").attr("src", uploadedFiles[0]);
                                }
                                else {
                                    that.$el.find("[data-file-render-unique-name='" + row.uniqueName + "']").text(uploadedFiles[0]);
                                }
                                that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").data("filePath", uploadedFiles[0]);
                                that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").val("");
                            }
                            if (inarr == arr.length) {
                                if (arrMultiple.length > 0) {
                                    arrMultiple.forEach(function (multirow) {
                                        that.$el.find("[data-element-container-unique-name='" + multirow.uniqueName + "']").fileUpload("uploadFiles", function () {
                                            inmulti += 1;
                                            if (arrMultiple.length == inmulti) {
                                                if (typeof callBack == "function") {
                                                    callBack.call();
                                                }
                                            }
                                        });
                                    });
                                }
                                else {
                                    if (typeof callBack == "function") {
                                        callBack.call();
                                    }
                                }
                            }
                        });
                    });

                });
            }
            else {
                if (arrMultiple.length > 0) {
                    arrMultiple.forEach(function (multirow) {
                        that.$el.find("[data-element-container-unique-name='" + multirow.uniqueName + "']").fileUpload("uploadFiles", function () {
                            inmulti += 1;
                            if (arrMultiple.length == inmulti) {
                                if (typeof callBack == "function") {
                                    callBack.call();
                                }
                            }
                        });
                    });
                }
                else {
                    if (typeof callBack == "function") {
                        callBack.call();
                    }
                }
            }
        }
    };

    FormCreate.prototype.uploadFile = function (files, options, successCallBack) {
        options = $.extend({}, FormCreate.FILEDEFAULS, options);
        if (files.length > 0) {
            var file = files;
            var arr = [];
            for (var inti = 0; inti < file.length; inti++) {
                var allFiles = new FormData();
                var filename = file[inti].name;
                var strFileName = filename.substr(0, filename.lastIndexOf('.'));
                var strFileExtension = filename.split(".").pop();

                var now = new Date();
                var year = "" + now.getFullYear();
                var month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
                var day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
                var hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
                var minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
                var second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
                var milliseconds = "" + now.getMilliseconds(); if (milliseconds.length == 1) { milliseconds = "0" + milliseconds; }


                strFileName += "" + day + "" + month + "" + year + "" + hour + "" + minute + "" + second + "" + milliseconds;
                var fullPath = options.path + strFileName + "." + strFileExtension;
                arr.push(fullPath);
                allFiles.append(strFileName + "." + strFileExtension, file[0], fullPath);
            }
            var result = "";
            $.ajax({
                type: "POST",
                url: options.handlerPath,
                contentType: false,
                processData: false,
                cache: false,
                async: false,
                complete: function () {

                    if (successCallBack != undefined && successCallBack != null && successCallBack != "") {
                        if (typeof successCallBack == "function") {
                            successCallBack.call(result, arr);
                        }
                        else if (typeof successCallBack == "string") {
                            eval(successCallBack);
                        }
                    }
                },
                data: allFiles,
                success: function (data) {
                    if (data == "Success") {
                        console.log(file.length + " file(s) uploaded");
                    }
                    result = data;

                },
                error: function (msg) {
                    result = msg;
                    alert("Failed upload file.");
                }
            });
        }
        else {
            if (successCallBack != undefined && successCallBack != null && successCallBack != "") {
                if (typeof successCallBack == "function") {
                    successCallBack.call(result, arr);
                }
                else if (typeof successCallBack == "string") {
                    eval(successCallBack);
                }
            }
        }
    }

    FormCreate.prototype.deleteFiles = function (arrUniqueName, callBack) {
        if (arrUniqueName == undefined || arrUniqueName == null || arrUniqueName == "")
            arrUniqueName = [];
        var that = this;
        if (arrUniqueName.length > 0) {
            arrUniqueName.forEach(function (val) {
                var row = that.getOption(val);
                if (row.visible) {
                    if (row.multiple) {
                        that.$el.find("[data-element-container-unique-name='" + row.uniqueName + "']").fileUpload("deleteFiles", callBack);
                    }
                    else {
                        that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").each(function () {
                            var deletePath = that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").data("filePath");
                            if (this.files.length > 0 && deletePath != undefined && deletePath != "" && deletePath != null)
                                that.deleteFile([that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").data("filePath")], row.file, callBack);
                        });
                    }
                }
                else {
                    callBack.call();
                }
            });
        }
        else {
            var arr = filterArrayFromArrayByColumn(that.options.data, "visible", true);
            arr = filterArrayFromArrayByColumn(arr, "type", "file");
            var arrMultiple = filterArrayFromArrayByColumn(arr, "multiple", true);
            arr = filterArrayFromArrayByColumn(arr, "multiple", [undefined, false, null]);

            var inarr = 0;
            var inmulti = 0;
            if (arr.length > 0) {
                arr.forEach(function (row) {
                    that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").each(function () {
                        var deletePath = that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").data("filePath");
                        if (this.files.length > 0 && deletePath != undefined && deletePath != "" && deletePath != null) {
                            that.deleteFile([that.$el.find("[data-element-type='file'][data-unique-name='" + row.uniqueName + "']").data("filePath")], row.file, function () {
                                inarr += 1;
                                if (inarr == arr.length) {
                                    if (arrMultiple.length > 0) {
                                        arrMultiple.forEach(function (multirow) {
                                            that.$el.find("[data-element-container-unique-name='" + multirow.uniqueName + "']").fileUpload("deleteFiles", function () {
                                                inmulti += 1;
                                                if (arrMultiple.length == inmulti) {
                                                    if (typeof callBack == "function") {
                                                        callBack.call();
                                                    }
                                                }
                                            });
                                        });
                                    }
                                    else {
                                        if (typeof callBack == "function") {
                                            callBack.call();
                                        }
                                    }
                                }
                            });
                        }
                        else {
                            inarr += 1;
                            if (inarr == arr.length) {
                                if (arrMultiple.length > 0) {
                                    arrMultiple.forEach(function (multirow) {
                                        that.$el.find("[data-element-container-unique-name='" + multirow.uniqueName + "']").fileUpload("deleteFiles", function () {
                                            inmulti += 1;
                                            if (arrMultiple.length == inmulti) {
                                                if (typeof callBack == "function") {
                                                    callBack.call();
                                                }
                                            }
                                        });
                                    });
                                }
                                else {
                                    if (typeof callBack == "function") {
                                        callBack.call();
                                    }
                                }
                            }
                        }
                    });

                });
            }
            else {
                if (arrMultiple.length > 0) {
                    arrMultiple.forEach(function (multirow) {
                        that.$el.find("[data-element-container-unique-name='" + multirow.uniqueName + "']").fileUpload("deleteFiles", function () {
                            inmulti += 1;
                            if (arrMultiple.length == inmulti) {
                                if (typeof callBack == "function") {
                                    callBack.call();
                                }
                            }
                        });
                    });
                }
                else {
                    if (typeof callBack == "function") {
                        callBack.call();
                    }
                }
            }
        }
    }

    FormCreate.prototype.deleteFile = function (path, options, successCallBack) {
        var that = this;
        options = $.extend({}, FormCreate.FILEDEFAULS, options);
        var requestObj = { strFilePaths: path };
        if (path.length > 0) {
            var result = "";
            $.ajax({
                type: "POST",
                url: that.options.fileDeletePath,
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    $("#loading").show();
                },
                complete: function () {
                    if (successCallBack != undefined && successCallBack != null && successCallBack != "") {
                        if (typeof successCallBack == "function") {
                            successCallBack.call(result, path);
                        }
                        else if (typeof successCallBack == "string") {
                            eval(successCallBack);
                        }
                    }
                },
                data: JSON.stringify(requestObj),
                async: true,
                success: function (data) {
                    if (data.d == 1) {
                        console.log(path.length + " file(s) deleted");
                    }
                    result = data;
                },
                error: function (msg) {
                    result = msg;
                    alert("Failed delete file.");
                }
            });
        }
        else {
            if (successCallBack != undefined && successCallBack != null && successCallBack != "") {
                if (typeof successCallBack == "function") {
                    successCallBack.call(result, path);
                }
                else if (typeof successCallBack == "string") {
                    eval(successCallBack);
                }
            }
        }
    };

    FormCreate.prototype.createElement = function (row, index, appendAfterUniqueName) {
        var that = this;
        if (row.uniqueName == undefined || row.uniqueName == "") {
            throwError("\"uniqueName\" field is required");
        }
        if (!reg["likeVariable"].test(row.uniqueName)) {
            throwError("Give proper format in \"uniqueName\" field \"" + row.uniqueName + "\"");
        }
        if (that.$el.find("[data-unique-name='" + row.uniqueName + "']").length > 0) {
            throwError("Unique name \"" + row.uniqueName + "\" is already exist");
        }
        var option;
        if (row.attr == "" || row.attr == null) {
            row.attr = { autocomplete: "off" };
        }
        else {
            row.attr = $.extend({}, row.attr, { autocomplete: "off" });
        }
        if (row.multipleOptions == "" || row.multipleOptions == null) {
            row.multipleOptions = {};
        }
        if (row.validate == "" || row.validate == null) {
            row.validate = {};
        }
        if (row.date == "" || row.date == null) {
            row.date = {};
        }
        var optionId = row.uniqueName;
        if (row.visible || that.options.showAlsoVisibleFalse) {
            if (row.type == "button") {
                row.afterElement = "";
                row.beforeLabel = "";
            }
            if ((!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) || row.type == "selectize") {
                if (row.type == "file") {
                    row.file = $.extend({}, FormCreate.FILEDEFAULS, row.file);
                    option = $(htmls["file"]);
                    option.text(row.file.buttonText);
                    option.addClass(row.file.buttonClass);
                    var fileType = $(elementEnum["file"]);

                    fileType.attr($.extend({}, { "id": "file" + row.uniqueName, "data-unique-name": row.uniqueName, "name": row.uniqueName }, row.attr));
                    option.attr($.extend({}, { "id": "button" + row.uniqueName, "data-unique-name": row.uniqueName, "name": row.uniqueName, "data-target": "#" + fileType.attr("id") }, row.attr));

                    option.data("validate-name", row.validateName);
                    fileType.data("validate-name", row.validateName);
                    option.addClass(row.elementClass);
                    option.prop("disabled", row.disabled);
                    fileType.prop("disabled", row.disabled);

                    that.initValidate(option, row);
                    that.initValidate(fileType, row);
                    if (row.value != undefined && row.value != null && row.value != "")
                        option.html(row.value);
                    var $button;
                    if (option[0].nodeName != "BUTTON") {
                        $button = option.find("button");
                    }
                    else {
                        $button = option;
                    }

                    $button.click(function () {
                        fileType.trigger("click");
                    });
                    fileType.change(function () {
                        that.$el.find("[data-file-render-unique-name='" + row.uniqueName + "']").remove();
                        that.$el.find("[data-file-error-msg-unique-name='" + row.uniqueName + "']").remove();
                        $button.text(row.file.buttonText);
                        $button.removeAttr("class");
                        $button.addClass(row.file.buttonClass);

                        var file = this.files;
                        if (file.length > 0) {
                            var allFiles = new FormData();
                            var filename = file[0].name;
                            var strFileName = filename.substr(0, filename.lastIndexOf('.'));
                            var strFileExtension = filename.split(".").pop().toLowerCase();
                            if (row.file.allowFiles.indexOf(strFileExtension) > -1) {
                                var byteSize = 0;
                                if (row.file.fileSize != undefined && row.file.fileSize != null && row.file.fileSize != "") {
                                    var byteSize = parseInt(row.file.fileSize.replace(/[^0-9]/g, ''));
                                    byteSize = (byteSize * 1024) * 1024;
                                }
                                if (byteSize == 0 || file[0].size <= byteSize) {
                                    $button.text(row.file.onChangeText);
                                    $button.removeClass(row.file.buttonClass);
                                    $button.addClass(row.file.onChangeClass);
                                    if (fileUploads.image.indexOf(strFileExtension) > -1) {
                                        var img = $("<img height='" + row.file.imageheight + "' width='" + row.file.imagewidth + "' data-file-render-unique-name='" + row.uniqueName + "' class='" + row.file.imageClass + "' />");
                                        var reader = new FileReader();
                                        reader.onload = function (e) {
                                            img.attr('src', e.target.result);
                                            if (row.file.renderAfter) {
                                                img.css("margin-left", row.file.spaceBetween);
                                                $button.after(img);
                                            }
                                            else {
                                                img.css("margin-right", row.file.spaceBetween);
                                                $button.before(img);
                                            }
                                        }
                                        reader.readAsDataURL(file[0]);
                                    }
                                    else {
                                        var span = $("<span class='" + row.file.imageClass + "' data-file-render-unique-name='" + row.uniqueName + "'>" + filename + "</span>");
                                        if (row.file.renderAfter) {
                                            span.css("margin-left", row.file.spaceBetween);
                                            $button.after(span);
                                        }
                                        else {
                                            span.css("margin-right", row.file.spaceBetween);
                                            $button.before(span);
                                        }
                                    }
                                }
                                else {
                                    var spanErr = $("<code data-file-error-msg-unique-name='" + row.uniqueName + "'>Please upload file only " + row.file.fileSize.replace(/[^0-9]/g, '') + " MB</code>");
                                    $button.after(spanErr);
                                }
                            }
                            else {
                                var spanErr = $("<code data-file-error-msg-unique-name='" + row.uniqueName + "'>Please upload file only " + row.file.allowFiles.join(',') + " </code>");
                                $button.after(spanErr);
                            }
                        }
                    });

                    if (row.elementContainer != undefined && row.elementContainer != "" && row.type != "button") {
                        //option = $(row.elementContainer).append(option);                        
                        var option1 = option;
                        option = $(row.elementContainer);
                        if (option.find(".input-selector").length > 0) {
                            option.find(".input-selector").append(option1);
                        }
                        else {
                            option.append(option1);
                        }
                    }

                    $button.before(fileType);
                }
                    //else if (row.type == "selectize") {
                    //    row.selectize = $.extend({}, FormCreate.SELECTIZEDEFAULTS, row.selectize);
                    //    option = $(elementEnum[row.type]);
                    //    option.attr($.extend({}, { "id": row.uniqueName, "data-unique-name": row.uniqueName, "name": row.uniqueName }, row.attr));
                    //    option.data("validate-name", row.validateName);
                    //    option.addClass(row.elementClass);
                    //    option.prop("disabled", row.disabled);
                    //    that.initEvents(option, row.events);

                    //    that.initValidate(option, row);
                    //    if (row.type == "button" || row.type == "label") {
                    //        option.html(row.value);
                    //    }
                    //    else {
                    //        if (checkableEnum.indexOf(row.type) != -1)
                    //            option.prop("checked", row.value);
                    //        else
                    //            option.val(row.value);
                    //    }

                    //    if (row.elementContainer != undefined && row.elementContainer != "" && row.type != "button") {
                    //        option = $(row.elementContainer).append(option);
                    //    }

                    //    if (row.isSummaryField) {
                    //        var calculateField = filterObjectFromArrayByColumn(that.options.data, "uniqueName", row.CalculateField);
                    //        if (calculateField != undefined && calculateField != null && calculateField != "") {
                    //            if (calculateField.events != undefined && calculateField.events != null && calculateField.events != "") {
                    //                if (typeof calculateField.events == "string") {
                    //                    calculateField.events = JSON.parse(calculateField.events);
                    //                }
                    //            }
                    //            else {
                    //                calculateField.events = {};
                    //            }

                    //            var changeee = calculateField.events.change;

                    //            calculateField.events.change = function () {

                    //            }

                    //        }
                    //    }
                    //}
                else {
                    option = $(elementEnum[row.type]);
                    if (row.maximumLength != "" && row.maximumLength != undefined && row.maximumLength != null) {
                        option.attr("maxlength", row.maximumLength);
                    }
                    option.attr($.extend({}, { "id": row.uniqueName, "data-unique-name": row.uniqueName, "name": row.uniqueName }, row.attr));
                    option.data("validate-name", row.validateName);
                    option.addClass(row.elementClass);
                    option.prop("disabled", row.disabled);
                    that.initEvents(option, row.events);

                    that.initValidate(option, row);
                    if (row.type == "button" || row.type == "label" || row.type == "numbercalc" || row.type == "span") {
                        option.html(row.value);
                    }
                    else {
                        if (checkableEnum.indexOf(row.type) != -1)
                            option.prop("checked", row.value);
                        else if (row.type != "selectize")
                            option.val(row.value);
                        //else
                        //    option.val(row.value);
                    }

                    if (row.elementContainer != undefined && row.elementContainer != "" && row.type != "button") {
                        //option = $(row.elementContainer).append(option); 
                        var option1 = option;
                        option = $(row.elementContainer);
                        if (option.find(".input-selector").length > 0) {
                            option.find(".input-selector").append(option1);
                        }
                        else {
                            option.append(option1);
                        }
                    }

                    if (row.isSummaryField) {
                        var calculateField = filterObjectFromArrayByColumn(that.options.data, "uniqueName", row.CalculateField);
                        if (calculateField != undefined && calculateField != null && calculateField != "") {
                            if (calculateField.events != undefined && calculateField.events != null && calculateField.events != "") {
                                if (typeof calculateField.events == "string") {
                                    calculateField.events = JSON.parse(calculateField.events);
                                }
                            }
                            else {
                                calculateField.events = {};
                            }

                            var changeee = calculateField.events.change;

                            calculateField.events.change = function () {

                            }

                        }
                    }
                }
            }
            else {
                if (row.type == "file") {
                    var tt;
                    if (row.elementContainer != undefined && row.elementContainer != "") {
                        option = $(row.elementContainer);
                    }
                    else
                        option = $("<div data-element-container-unique-name=" + row.uniqueName + " data-element-type='multifile' class='input-selector'></div>");
                    if (option.find(".input-selector").length > 0) {
                        tt = option.find(".input-selector");
                    }
                    else {
                        tt = option;
                    }

                    tt.fileUpload($.extend({}, { fileSavePath: row.file.path, fileExtension: row.file.allowFiles, data: row.data }, row.file, row.file.multiOptions));
                }
                else {
                    var tt;
                    if (row.elementContainer != undefined && row.elementContainer != "") {
                        option = $(row.elementContainer);
                    }
                    else
                        option = $("<div  data-element-panel-unique-name='" + row.uniqueName + "' class='input-selector'></div>");

                    if (option.find(".input-selector").length > 0) {
                        tt = option.find(".input-selector");
                    }
                    else {
                        tt = option;
                    }

                    option.attr("data-multiple-type", row.type);
                    if (row.multipleOptions != undefined && row.multipleOptions != null && row.multipleOptions != "") {
                        if (typeof row.multipleOptions == "string") {
                            row.multipleOptions = JSON.parse(row.multipleOptions);
                        }
                    }
                    else {
                        row.multipleOptions = {};
                    }
                    if (row.multipleOptions.ajaxOptions != undefined && row.multipleOptions.ajaxOptions != null && row.multipleOptions.ajaxOptions != "") {
                        row.value = that.getDataFromServer(row);
                    }

                    var dependancyField = filterObjectFromArrayByColumn(that.options.data, "DependancyFieldFrom", row.uniqueName);
                    if (dependancyField != undefined && dependancyField != null && dependancyField != "") {
                        if (row.multipleOptions.events != undefined && row.multipleOptions.events != null && row.multipleOptions.events != "") {
                            if (typeof row.multipleOptions.events == "string") {
                                row.multipleOptions.events = JSON.parse(row.multipleOptions.events);
                            }
                        }
                        else {
                            row.multipleOptions.events = {};
                        }
                        var changeee = row.multipleOptions.events.change.clone();

                        row.multipleOptions.events.change = function () {
                            if (dependancyField.type != "autoComplete" && dependancyField.multiple) {
                                var ojjj = {};
                                var selectedValues = that.getFormData();
                                if (selectedValues == undefined || selectedValues == null || selectedValues == "") {
                                    selectedValues = {};
                                    selectedValues[row.uniqueName] = row.zeroVal;
                                }
                                if (selectedValues[row.uniqueName] == undefined || selectedValues[row.uniqueName] == null || selectedValues[row.uniqueName] == "") {
                                    selectedValues[row.uniqueName] = row.zeroVal;
                                }
                                if (Array.isArray(selectedValues[row.uniqueName])) {
                                    selectedValues[row.uniqueName] = selectedValues[row.uniqueName].join(',');
                                }
                                ojjj[dependancyField.DependancyField] = selectedValues[row.uniqueName];
                                if (typeof dependancyField.multipleOptions.ajaxOptions.data.strJSON == "string")
                                    dependancyField.multipleOptions.ajaxOptions.data.strJSON = JSON.parse(dependancyField.multipleOptions.ajaxOptions.data.strJSON)
                                var opp = dependancyField.multipleOptions.ajaxOptions.data.strJSON;
                                if (opp.cols == undefined || opp.cols == null || opp.cols == "") {
                                    opp.cols = {};
                                }
                                else {
                                    if (typeof opp.cols == "string") {
                                        opp.cols = JSON.parse(opp.cols);
                                    }
                                }
                                opp.cols = $.extend({}, opp.cols, ojjj);
                                opp.cols = JSON.stringify(opp.cols);
                                dependancyField.multipleOptions.ajaxOptions.data.strJSON = JSON.stringify(opp);
                                that.updateElement({ uniqueName: dependancyField.uniqueName, options: dependancyField });

                                if (typeof changeee == "function") {
                                    changeee();
                                }
                                else {
                                    eval(changeee);
                                }
                            }
                        }
                    }

                    var dependancyFieldBased = filterObjectFromArrayByColumn(that.options.data, "uniqueName", row.DependancyFieldFrom);
                    if (dependancyFieldBased != undefined && dependancyFieldBased != null && dependancyFieldBased != "") {
                        if (dependancyFieldBased.multipleOptions.events != undefined && dependancyFieldBased.multipleOptions.events != null && dependancyFieldBased.multipleOptions.events != "") {
                            if (typeof dependancyFieldBased.multipleOptions.events == "string") {
                                dependancyFieldBased.multipleOptions.events = JSON.parse(dependancyFieldBased.multipleOptions.events);
                            }
                        }
                        else {
                            dependancyFieldBased.multipleOptions.events = {};
                        }

                        var changeee = dependancyFieldBased.multipleOptions.events.change;

                        dependancyFieldBased.multipleOptions.events.change = function () {
                            if (row.type != "autoComplete" && row.multiple) {
                                var ojjj = {};
                                var selectedValues = that.getFormData();
                                if (selectedValues == undefined || selectedValues == null || selectedValues == "") {
                                    selectedValues = {};
                                    selectedValues[dependancyFieldBased.uniqueName] = row.zeroVal;
                                }
                                if (selectedValues[dependancyFieldBased.uniqueName] == undefined || selectedValues[dependancyFieldBased.uniqueName] == null || selectedValues[dependancyFieldBased.uniqueName] == "") {
                                    selectedValues[dependancyFieldBased.uniqueName] = row.zeroVal;
                                }
                                if (Array.isArray(selectedValues[dependancyFieldBased.uniqueName])) {
                                    selectedValues[dependancyFieldBased.uniqueName] = selectedValues[dependancyFieldBased.uniqueName].join(',');
                                }
                                ojjj[row.DependancyField] = selectedValues[dependancyFieldBased.uniqueName];

                                if (typeof row.multipleOptions.ajaxOptions.data.strJSON == "string")
                                    row.multipleOptions.ajaxOptions.data.strJSON = JSON.parse(row.multipleOptions.ajaxOptions.data.strJSON)
                                var opp = row.multipleOptions.ajaxOptions.data.strJSON;
                                if (opp.cols == undefined || opp.cols == null || opp.cols == "") {
                                    opp.cols = {};
                                }
                                else {
                                    if (typeof opp.cols == "string") {
                                        opp.cols = JSON.parse(opp.cols);
                                    }
                                }
                                opp.cols = $.extend({}, opp.cols, ojjj);
                                opp.cols = JSON.stringify(opp.cols);
                                row.multipleOptions.ajaxOptions.data.strJSON = JSON.stringify(opp);
                                that.updateElement({ uniqueName: row.uniqueName, options: row });

                                if (typeof changeee == "function") {
                                    changeee();
                                }
                                else {
                                    eval(changeee);
                                }
                            }
                        }
                    }
                    if (Array.isArray(row.multipleOptions)) {
                        var opts1010 = $.extend({},
                            {
                                type: row.type,
                                data: row.value,
                                display: row.labelField,
                                value: row.valueField,
                                ID: row.uniqueName,
                                autocompleteUrl: row.autocompleteUrl,
                                validateName: row.validateName,
                                fieldOptions: row.multipleOptions,
                                DependancyField: row.DependancyField,
                                DependancyFieldFrom: row.DependancyFieldFrom,
                                parentDefaults: that,
                                zeroVal: row.zeroVal,
                            },
                                {
                                    parent: {
                                        isRequire: row.isRequire
                                    },
                                }
                            );

                        tt.element(opts1010);
                    }
                    else {
                        tt.element($.extend({},
                            {
                                type: row.type,
                                data: row.value,
                                display: row.labelField,
                                value: row.valueField,
                                ID: row.uniqueName,
                                validateName: row.validateName,
                                DependancyField: row.DependancyField,
                                DependancyFieldFrom: row.DependancyFieldFrom,
                                parentDefaults: that,
                                zeroVal: row.zeroVal,
                            },
                            {
                                parent: {
                                    isRequire: row.isRequire
                                },
                            },
                            row.multipleOptions));
                    }
                }
            }

            option.attr(getDataAttr("elementContainer", [row.uniqueName]));
            if (row.type == "button") {
                that.$elementBtn.append($("<div class='divbtn' style='display:inline-block;' data-element-panel-unique-name='" + row.uniqueName + "'></div>").append(option));
            }
            else if (row.elementPanel != undefined && row.elementPanel != "") {
                var elementPanel = $(row.elementPanel);

                if (!row.visible && that.options.showAlsoVisibleFalse) {
                    elementPanel.addClass(that.options.VisibleFalseFieldClass);
                }
                else {
                    elementPanel.removeClass(that.options.VisibleFalseFieldClass);
                }

                elementPanel.attr(getDataAttr("elementPanel", [row.uniqueName]));
                if (appendAfterUniqueName != undefined && appendAfterUniqueName != "") {
                    if (that.$element.find("[" + dataEnum.elementPanel[0] + "=" + appendAfterUniqueName + "]").length > 0) {
                        that.$element.find("[" + dataEnum.elementPanel[0] + "=" + appendAfterUniqueName + "]").after(elementPanel.append(option));
                    }
                    else if (that.$element.find("[" + dataEnum.elementContainer[0] + "=" + appendAfterUniqueName + "]").length > 0) {
                        that.$element.find("[" + dataEnum.elementContainer[0] + "=" + appendAfterUniqueName + "]").after(elementPanel.append(option));
                    }
                    else if (that.$element.find("[data-unique-name=" + appendAfterUniqueName + "]").length > 0) {
                        that.$element.find("[data-unique-name=" + appendAfterUniqueName + "]").after(elementPanel.append(option));
                    }
                    else {
                        that.$element.append(elementPanel.append(option));
                    }
                }
                else {
                    that.$element.append(elementPanel.append(option));
                }
            }
            else {
                if (appendAfterUniqueName != undefined && appendAfterUniqueName != "") {
                    if (that.$element.find("[" + dataEnum.elementPanel[0] + "=" + appendAfterUniqueName + "]").length > 0) {
                        that.$element.find("[" + dataEnum.elementPanel[0] + "=" + appendAfterUniqueName + "]").after(option);
                    }
                    else if (that.$element.find("[" + dataEnum.elementContainer[0] + "=" + appendAfterUniqueName + "]").length > 0) {
                        that.$element.find("[" + dataEnum.elementContainer[0] + "=" + appendAfterUniqueName + "]").after(option);
                    }
                    else if (that.$element.find("[data-unique-name=" + appendAfterUniqueName + "]").length > 0) {
                        that.$element.find("[data-unique-name=" + appendAfterUniqueName + "]").after(option);
                    }
                    else {
                        that.$element.append(option);
                    }
                }
                else {
                    that.$element.append(option);
                }

                if (!row.visible && that.options.showAlsoVisibleFalse) {
                    option.addClass(that.options.VisibleFalseFieldClass);
                }
                else {
                    option.removeClass(that.options.VisibleFalseFieldClass);
                }
            }

            if (!row.withoutLabel && row.type != "button") {
                var label = $(elementEnum.label);
                label.html(row.label);

                if (optionId != undefined)
                    label.attr("for", optionId);
                if (row.type == "textarea" && row.IncludeTimestamp) {
                    label.append("<img width='30' height='30' class='IncludeTimestampIcon' src='" + row.IncludeTimestampIcon + "'/>");
                    label.find(".IncludeTimestampIcon").click(function () {
                        if (typeof row.IncludeTimestampIconClick == "function") {
                            row.IncludeTimestampIconClick.call(this, $("#" + optionId))
                        }
                    });
                }
                label.addClass(row.labelClass);
                if (row.isRequire && row.mentionRequireSymbol) {
                    label.append("<code class='requireSymbol " + row.requireSymbolClass + "'>" + row.requireSymbol + "</code>");
                }
                if (row.labelContainer != undefined && row.labelContainer != "") {
                    //label = $(row.labelContainer).append(label);
                    var label1 = label;
                    label = $(row.labelContainer)
                    if (label.find(".label-selector").length > 0) {
                        label.find(".label-selector").append(label1);
                    }
                    else {
                        label.append(label1);
                    }
                    label.attr(getDataAttr("labelContainer", [row.uniqueName]));
                }
                if (row.elementFirst)
                    option.after(label);
                else
                    option.before(label);

                if (label.find(".label-selector").length > 0) {
                    label.find(".label-selector").before(row.beforeLabel);
                }
                else {
                    label.before(row.beforeLabel);
                }

            }
            else {
                if (option.find(".input-selector").length > 0) {
                    option.find(".input-selector").before(row.beforeLabel);
                }
                else {
                    option.before(row.beforeLabel);
                }
            }

            if (option.find(".input-selector").length > 0) {
                option.find(".input-selector").after(row.afterElement);
            }
            else {
                option.after(row.afterElement);
            }
        }
    };

    FormCreate.prototype.refreshElementData = function (uniqueName) {
        var that = this;
        var option = that.getOption(uniqueName);
        if (option != undefined && option != null && option != "") {
            if (option.multiple) {
                $("[data-element-panel-unique-name='" + uniqueName + "']").element("refreshData");
            }
        }
    }

    FormCreate.prototype.initEvents = function (target, events) {
        for (var event in events)
            if (typeof events[event] === "string")
                target.on(event, function () { eval(events[event]) });
            else
                target.on(event, events[event]);

    };

    FormCreate.prototype.triggerEvents = function (target, event) {
        //for (var event in events)
        target.trigger(event);
    };

    FormCreate.prototype.elementDetails = function () {
        var that = this;
        if (this.$element.find("input[data-element-type='selectize']").length > 0) {
            this.$element.find("input[data-element-type='selectize']").each(function () {
                $(this).selectize('destroy');
                $(this).data('data', that.getOption($(this).data("unique-name")).datavalue);
                $(this).selectize($.extend({}, { labelField: that.getOption($(this).data("unique-name")).labelField, valueField: that.getOption($(this).data("unique-name")).valueField }, FormCreate.SELECTIZEDEFAULTS, that.getOption($(this).data("unique-name")).selectize));
                //$(this).selectize('clear');
            });
        }

        if (this.$element.find("input[data-element-type='ldate']").length > 0) {
            this.$element.find("input[data-element-type='ldate']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "d-m-Y" }, that.getOption($(this).data("unique-name")).date, { maxDate: new Date(), timepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='udate']").length > 0) {
            this.$element.find("input[data-element-type='udate']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "d-m-Y" }, that.getOption($(this).data("unique-name")).date, { minDate: new Date(), timepicker: false }));
            });
        }

        if (this.$element.find("input[data-element-type='date']").length > 0) {
            this.$element.find("input[data-element-type='date']").each(function () {
                $(this).datetimepicker($.extend({}, { timepicker: false, format: "d-m-Y" }, that.getOption($(this).data("unique-name")).date));
            });
        }
        if (this.$element.find("input[data-element-type='ldatetime']").length > 0) {
            this.$element.find("input[data-element-type='ldatetime']").each(function () {
                $(this).datetimepicker($.extend({}, that.getOption($(this).data("unique-name")).date, { maxDate: new Date(), maxTime: new Date().getTime() }));
            });
        }
        if (this.$element.find("input[data-element-type='udatetime']").length > 0) {
            this.$element.find("input[data-element-type='udatetime']").each(function () {
                $(this).datetimepicker($.extend({}, that.getOption($(this).data("unique-name")).date, { minDate: new Date(), minTime: new Date().getTime() }));
            });
        }
        if (this.$element.find("input[data-element-type='datetime']").length > 0) {
            this.$element.find("input[data-element-type='datetime']").each(function () {
                $(this).datetimepicker($.extend({}, that.getOption($(this).data("unique-name")).date));
            });
        }

        if (this.$element.find("input[data-element-type='ltime']").length > 0) {
            this.$element.find("input[data-element-type='ltime']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "H:i:s" }, that.getOption($(this).data("unique-name")).date, { maxTime: new Date().getTime(), datepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='utime']").length > 0) {
            this.$element.find("input[data-element-type='utime']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "H:i:s" }, that.getOption($(this).data("unique-name")).date, { minTime: new Date().getTime(), datepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='time']").length > 0) {
            this.$element.find("input[data-element-type='time']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "H:i:s" }, that.getOption($(this).data("unique-name")).date, { datepicker: false }));
            });
        }


        if (this.$element.find("[data-element-type='checkbox']").length > 0) {
            this.$element.find("[data-element-type='checkbox']").click(function () {
                checkAll(that.$element);
            });
        }
        if (this.$element.find("[data-element-type='checkAll']").length > 0) {
            this.$element.find("[data-element-type='checkAll']").change(function () {
                that.$element.find("[data-element-type='checkbox'][data-unique-name='" + $(this).attr("name") + "']")
                    .prop("checked", $(this).prop("checked"));
            });
        }

        this.options.afterCreation.call(this);

        //if (this.$element.find("input[data-element-type='phonenumber']").length > 0)
        //    this.$element.find("input[data-element-type='phonenumber']").mask("(999) 999-9999");


    };

    FormCreate.prototype.actualData = function (orderby) {
        if (orderby) {
            var returnData = [], that = this;
            //this.$el.find("[data-unique-name]:visible").each(function (index) {
            //    var allreadyObj = filterObjectFromArrayByColumn(returnData, "uniqueName", $(this).data("unique-name"));
            //    if (allreadyObj == undefined) {
            //        var obj = that.getOption($(this).data("unique-name"));
            //        obj.orderby = index + 1;
            //        obj.FieldsOrder = index + 1;
            //        returnData.push(obj);
            //    }
            //});

            //if (this.$el.find("[data-element-panel-unique-name]:visible").length != this.$el.find("[data-unique-name]:visible").length) {
            returnData = [];
            this.$el.find("[data-element-panel-unique-name]:visible").each(function (index) {
                var allreadyObj = filterObjectFromArrayByColumn(returnData, "uniqueName", $(this).data("element-panel-unique-name"));
                if (allreadyObj == undefined) {
                    var obj = that.getOption($(this).data("element-panel-unique-name"));
                    obj.orderby = index + 1;
                    obj.FieldsOrder = index + 1;
                    returnData.push(obj);
                }
            });
            //}
            var objRe = this.options;
            objRe.data = returnData;
            return objRe;
        }
        return this.options;
    }

    FormCreate.prototype.getField = function (str) {
        var arr = [];
        this.options.data.forEach(function (row) {
            for (var col in row) {
                if (col == str)
                    arr.push(row[col]);
            }
        });
        return arr;
    };

    FormCreate.prototype.getOption = function (uniqueName) {
        return $.grep(this.options.data,
            function (row) {
                return row.uniqueName == uniqueName;
            })[0];
    };

    FormCreate.prototype.getText = function (uniqueName) {
        if (uniqueName != "" && uniqueName != undefined && uniqueName != null) {
            var row = $.extend({}, FormCreate.FIELDSDEFAULTS, this.getOption(uniqueName)),
                that = this,
                returnVal;
            if (row.type == "button") {
            }
            else if (row.type == "radio") {
                //if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                returnVal = $("[for='" + that.$el.find("input[data-unique-name='" + row.uniqueName + "']:checked").attr("id") + "']").text();
                //}
                //else {
                //returnVal = that.$el.find("input[data-unique-name='" + row.uniqueName + "']:checked").text();
                //}
            }
            else if (row.type == "checkbox") {
                if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                    returnVal = $("[for='" + that.$el.find("input[data-unique-name='" + row.uniqueName + "']:checked").attr("id") + "']").text();
                }
                else {
                    var arr = [];
                    that.$el.find("input[data-unique-name='" + row.uniqueName + "']:checked").each(function () {
                        arr.push($("[for='" + $(this).attr("id") + "']").text());
                    });
                    returnVal = arr;
                }
            }
            else if (row.type == "autoComplete") {
                returnVal = that.$el.find("[data-unique-name='" + row.uniqueName + "']").val();
            }
            else if (row.type == "select") {
                returnVal = that.$el.find("[data-unique-name='" + row.uniqueName + "'] option:selected").text();
            }
            else if (row.type == "file") {
                if (!row.multiple) {
                    returnVal = that.$el.find("[data-unique-name='" + row.uniqueName + "'][data-element-type='file']").data("filePath");
                }
                else {
                    returnVal = [];
                    var uploadedfiles = that.$el.find("[data-element-container-unique-name='" + row.uniqueName + "']").fileUpload("getData");
                    uploadedfiles.forEach(function (uploadedfile, index) {
                        returnVal.push(uploadedfile.filePath);
                    });
                }
            }
            else {
                if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                    returnVal = that.$el.find("[data-unique-name='" + row.uniqueName + "']").val();
                } else {
                    var arr = [];
                    that.$el.find("input[data-unique-name='" + row.uniqueName + "']").each(function () {
                        arr.push($(this).val());
                    });
                    returnVal = arr;
                }
            }
            return returnVal;
        }
        else {
            return "";
        }
    }

    FormCreate.prototype.resetForm = function () {
        var that = this;
        that.options.data.forEach(function (row, index) {
            if (row.multiple) {
                if (that.options.data[index].multipleOptions != undefined && that.options.data[index].multipleOptions != null && that.options.data[index].multipleOptions != "") {
                    if (that.options.data[index].multipleOptions.ajaxOptions != undefined && that.options.data[index].multipleOptions.ajaxOptions != null && that.options.data[index].multipleOptions.ajaxOptions != "") {
                        if (that.options.data[index].multipleOptions.ajaxOptions.data != undefined && that.options.data[index].multipleOptions.ajaxOptions.data != null && that.options.data[index].multipleOptions.ajaxOptions.data != "") {
                            if (typeof that.options.data[index].multipleOptions.ajaxOptions.data == "string") {
                                that.options.data[index].multipleOptions.ajaxOptions.data = JSON.parse(that.options.data[index].multipleOptions.ajaxOptions.data);
                            }
                            if (that.options.data[index].multipleOptions.ajaxOptions.data.strJSON != undefined && that.options.data[index].multipleOptions.ajaxOptions.data.strJSON != null && that.options.data[index].multipleOptions.ajaxOptions.data.strJSON != "") {
                                if (typeof that.options.data[index].multipleOptions.ajaxOptions.data.strJSON == "string") {
                                    that.options.data[index].multipleOptions.ajaxOptions.data.strJSON = JSON.parse(that.options.data[index].multipleOptions.ajaxOptions.data.strJSON);
                                }
                                that.options.data[index].multipleOptions.ajaxOptions.data.strJSON.cols = "";
                                that.options.data[index].multipleOptions.ajaxOptions.data.strJSON = JSON.stringify(that.options.data[index].multipleOptions.ajaxOptions.data.strJSON);
                            }
                        }
                    }
                }
            }
        });
        this.initElement(true);
    };

    FormCreate.prototype.resetDataOnly = function () {
        var that = this;
        that.options.data.forEach(function (row, index) {
            if (row.multiple) {
                if (that.options.data[index].multipleOptions != undefined && that.options.data[index].multipleOptions != null && that.options.data[index].multipleOptions != "") {
                    if (that.options.data[index].multipleOptions.ajaxOptions != undefined && that.options.data[index].multipleOptions.ajaxOptions != null && that.options.data[index].multipleOptions.ajaxOptions != "") {
                        if (that.options.data[index].multipleOptions.ajaxOptions.data != undefined && that.options.data[index].multipleOptions.ajaxOptions.data != null && that.options.data[index].multipleOptions.ajaxOptions.data != "") {
                            if (typeof that.options.data[index].multipleOptions.ajaxOptions.data == "string") {
                                that.options.data[index].multipleOptions.ajaxOptions.data = JSON.parse(that.options.data[index].multipleOptions.ajaxOptions.data);
                            }
                            if (that.options.data[index].multipleOptions.ajaxOptions.data.strJSON != undefined && that.options.data[index].multipleOptions.ajaxOptions.data.strJSON != null && that.options.data[index].multipleOptions.ajaxOptions.data.strJSON != "") {
                                if (typeof that.options.data[index].multipleOptions.ajaxOptions.data.strJSON == "string") {
                                    that.options.data[index].multipleOptions.ajaxOptions.data.strJSON = JSON.parse(that.options.data[index].multipleOptions.ajaxOptions.data.strJSON);
                                }
                                that.options.data[index].multipleOptions.ajaxOptions.data.strJSON.cols = "";
                                that.options.data[index].multipleOptions.ajaxOptions.data.strJSON = JSON.stringify(that.options.data[index].multipleOptions.ajaxOptions.data.strJSON);
                            }
                        }
                    }
                }
            }

            var key = row.uniqueName;
            if (checkableEnum.indexOf(row.type) > -1) {
                that.$element.find("[data-unique-name='" + key + "']").prop("checked", false);
                if (row.multipleOptions != undefined && row.multipleOptions != null && typeof row.multipleOptions == "object" && row.multipleOptions[row.type] != undefined && row.multipleOptions[row.type] != null && typeof row.multipleOptions[row.type] == "object") {

                    if (Array.isArray(row.multipleOptions[row.type].defaultSelectedVal)) {
                        row.multipleOptions[row.type].defaultSelectedVal.forEach(function (vvaall) {
                            that.$element.find("[data-unique-name='" + key + "'][value='" + vvaall + "']").prop("checked", false);
                        });
                    }
                    else {
                        that.$element.find("[data-unique-name='" + key + "'][value='" + row.multipleOptions[row.type].defaultSelectedVal + "']").prop("checked", false);
                    }
                }
            }
            else if (row.type == "select") {
                that.$element.find("[data-unique-name='" + key + "']").val('');
                if (row.multipleOptions != undefined && row.multipleOptions != null && typeof row.multipleOptions == "object" && row.multipleOptions.select != undefined && row.multipleOptions.select != null && typeof row.multipleOptions.select == "object") {
                    that.$element.find("[data-unique-name='" + key + "']").val(row.multipleOptions.select.defaultSelectedVal);
                }
                if (row.multipleOptions != undefined && row.multipleOptions != null && typeof row.multipleOptions == "object" && row.multipleOptions.select != undefined && row.multipleOptions.select != null && typeof row.multipleOptions.select == "object" && row.multipleOptions.select.multiSelect) {
                    that.$element.find("[data-unique-name='" + key + "']").dropdown("chooseAll", false);
                    if (Array.isArray(row.multipleOptions.select.defaultSelectedVal)) {
                        row.multipleOptions.select.defaultSelectedVal.forEach(function (vvaall) {
                            that.$element.find("[data-unique-name='" + key + "']").dropdown("choose", vvaall, true);
                        });
                    }
                    else {
                        that.$element.find("[data-unique-name='" + key + "']").dropdown("choose", row.multipleOptions.select.defaultSelectedVal, true);
                    }
                }
            }
            else if (row.type == "autoComplete") {
                that.$element.find("[data-unique-name='" + key + "']").val('');
                that.$element.find("[data-unique-name='" + key + "']").data(key, '');
            }
            else if (row.type == "file") {
                if (row.multiple) {
                    that.$el.find("[data-element-container-unique-name='" + key + "']").fileUpload("removeAll");
                }
                else {
                    that.$element.find("[data-unique-name='" + key + "'][data-element-type='file']").data("filePath", "");
                    that.$element.find("[data-file-render-unique-name='" + key + "']").remove();

                    var $button = that.$element.find("[data-unique-name='" + key + "'][data-element-type='button-file']");

                    $button.text(row.file.buttonText);
                    $button.removeAttr("class");
                    $button.addClass(row.file.buttonClass);
                }
            }
            else {
                that.$element.find("[data-unique-name='" + key + "']").val('');
            }
        });
    }

    FormCreate.prototype.updateData = function (data) {
        if (Array.isArray(data)) {
            this.options.data = data;
            this.initElement(true);
        }
    }

    FormCreate.prototype.setFormData = function (params) {
        if (params != undefined && params == "" && typeof params !== 'object')
            throwError("send parameter is object");
        var that = this;
        var lateKey = [];
        var multiLateKey = [];
        for (var key in params) {
            var currentOptions = that.getOption(key);
            //if (that.$element.find("[data-unique-name='" + key + "']").length > 0) {
            if (currentOptions != undefined && currentOptions != null && currentOptions != "") {
                if (currentOptions.type == 'radio') {
                    var chkVal = that.$element.find("[data-unique-name='" + key + "']:checked").val();
                    that.$element.find("[data-unique-name='" + key + "'][value='" + params[key] + "']").prop("checked", true);
                    if (chkVal != params[key])
                        that.triggerEvents(that.$element.find("[data-unique-name='" + key + "'][value='" + params[key] + "']"), "change");
                }
                else if (currentOptions.type == 'checkbox') {
                    if (currentOptions.multiple) {
                        that.$element.find("[data-unique-name='" + key + "']").prop("checked", false);
                        if (typeof params[key] == "string") {
                            if (params[key].indexOf("[") == 0 && params[key].indexOf("]") == (params[key].length - 1)) {
                                params[key] = JSON.parse(params[key]);
                            }
                            else {
                                params[key] = params[key].split(",");
                            }
                        }
                        params[key].forEach(function (val) {
                            var chkVal = that.$element.find("[data-unique-name='" + key + "'][value='" + val + "']").prop("checked");
                            that.$element.find("[data-unique-name='" + key + "'][value='" + val + "']").prop("checked", true);
                            if (chkVal != that.$element.find("[data-unique-name='" + key + "'][value='" + val + "']"))
                                that.triggerEvents(that.$element.find("[data-unique-name='" + key + "'][value='" + val + "']"), "change");
                        });
                        checkAll(that.$element);
                    }
                    else {
                        var chkVal = that.$element.find("[data-unique-name='" + key + "']").prop("checked");
                        that.$element.find("[data-unique-name='" + key + "']").prop("checked", params[key]);
                        if (params[key] != chkVal)
                            that.triggerEvents(that.$element.find("[data-unique-name='" + key + "']"), "change");
                    }
                }
                else if (datePick.indexOf(currentOptions.type) > -1) {
                    if (currentOptions.multiple && typeof currentOptions.value !== 'string') {
                        params[key].forEach(function (val, index) {
                            var indx = parseInt(index + 1);
                            var chkVal = that.$element.find("#" + key + indx).val();
                            that.$element.find("#" + key + indx).datetimepicker('setOptions', { value: new Date(val) });
                            if (that.$element.find("#" + key + indx).val() != chkVal)
                                that.triggerEvents(that.$element.find("#" + key + indx), "change");
                        });
                    }
                    else {
                        var chkVal = that.$element.find("[data-unique-name='" + key + "']").val();
                        that.$element.find("[data-unique-name='" + key + "']").datetimepicker('setOptions', { value: new Date(params[key]) });
                        if (chkVal != that.$element.find("[data-unique-name='" + key + "']").val())
                            that.triggerEvents(that.$element.find("[data-unique-name='" + key + "']"), "change");
                    }
                }
                else if (currentOptions.type == 'select') {
                    if (currentOptions.multipleOptions != undefined && currentOptions.multipleOptions != null && typeof currentOptions.multipleOptions == "object" && currentOptions.multipleOptions.select != undefined && currentOptions.multipleOptions.select != null && typeof currentOptions.multipleOptions.select == "object" && currentOptions.multipleOptions.select.multiSelect) {
                        if (typeof params[key] == "string") {
                            if (params[key].indexOf("[") == 0 && params[key].indexOf("]") == (params[key].length - 1)) {
                                params[key] = JSON.parse(params[key]);
                            }
                            else {
                                params[key] = params[key].split(",");
                            }
                        }

                        if (that.$element.find("[data-unique-name='" + key + "']").html() != "") {
                            var chkVal = that.$element.find("[data-unique-name='" + key + "']").val().join(',');
                            that.$element.find("[data-unique-name='" + key + "']").val(params[key]);
                            that.$element.find("[data-unique-name='" + key + "']").dropdown("chooseAll", false);
                            that.$element.find("[data-unique-name='" + key + "']").dropdown("choose", params[key], true);
                        }
                        else {
                            multiLateKey.push(key);
                            setTimeout(function () {
                                multiLateKey.forEach(function (keyyy) {
                                    var chkVal = that.$element.find("[data-unique-name='" + keyyy + "']").val().join(',');
                                    that.$element.find("[data-unique-name='" + keyyy + "']").val(params[keyyy]);
                                    that.$element.find("[data-unique-name='" + keyyy + "']").dropdown("chooseAll", false);
                                    that.$element.find("[data-unique-name='" + keyyy + "']").dropdown("choose", params[keyyy], true);
                                });
                            }, 1000);
                        }
                    }
                    else {
                        //var chkVal = that.$element.find("[data-unique-name='" + key + "']").val();
                        //that.$element.find("[data-unique-name='" + key + "']").val(params[key]);
                        //if (chkVal != that.$element.find("[data-unique-name='" + key + "']").val())
                        //    that.triggerEvents(that.$element.find("[data-unique-name='" + key + "']"), "change");
                        if (that.$element.find("[data-unique-name='" + key + "']").html() != "") {
                            var chkVal = that.$element.find("[data-unique-name='" + key + "']").val();
                            that.$element.find("[data-unique-name='" + key + "']").val(params[key]);
                            if (chkVal != that.$element.find("[data-unique-name='" + key + "']").val())
                                that.triggerEvents(that.$element.find("[data-unique-name='" + key + "']"), "change");
                        }
                        else {
                            lateKey.push(key);
                            setTimeout(function () {
                                lateKey.forEach(function (keyyy) {
                                    var chkVal = that.$element.find("[data-unique-name='" + keyyy + "']").val();
                                    that.$element.find("[data-unique-name='" + keyyy + "']").val(params[keyyy]);
                                    if (chkVal != that.$element.find("[data-unique-name='" + keyyy + "']").val())
                                        that.triggerEvents(that.$element.find("[data-unique-name='" + keyyy + "']"), "change");
                                });
                            }, 1000);
                        }

                    }
                }
                else if (currentOptions.type == 'autoComplete') {
                    if (params[key] != "") {
                        if (!params[key].hasOwnProperty(currentOptions.labelField) || !params[key].hasOwnProperty(key)) {
                            //throwError("value is want to object like {" + currentOptions.labelField + ": \" \"," + currentOptions.valueField + ": \" \"}");
                            that.$element.find("[data-unique-name='" + key + "']").val(params[currentOptions.labelField]);
                            that.$element.find("[data-unique-name='" + key + "']").data(key, params[key]);
                            that.$element.find("[data-unique-name='" + key + "']").data(currentOptions.valueField, params[currentOptions.valueField]);
                        }
                        else {
                            that.$element.find("[data-unique-name='" + key + "']").val(params[key][currentOptions.labelField]);
                            that.$element.find("[data-unique-name='" + key + "']").data(key, params[key][key]);
                            that.$element.find("[data-unique-name='" + key + "']").data(currentOptions.valueField, params[key][currentOptions.valueField]);
                        }
                    }
                }
                else if (currentOptions.type == "file") {
                    currentOptions = $.extend({}, FormCreate.DEFAULTS, currentOptions);
                    currentOptions.file = $.extend({}, FormCreate.FILEDEFAULS, currentOptions.file);
                    if (currentOptions.multiple) {
                        if (params[key] != undefined && params[key] != null && params[key] != "") {
                            if (typeof params[key] == "string") {
                                params[key] = params[key].split(",");
                            }
                            var viewtoimage = [];
                            params[key].forEach(function (val) {
                                viewtoimage.push({ filePath: val });
                            });

                            that.$el.find("[data-element-container-unique-name='" + currentOptions.uniqueName + "']").fileUpload("removeAll");
                            that.$el.find("[data-element-container-unique-name='" + currentOptions.uniqueName + "']").fileUpload("fileAdd", { index: "0", files: viewtoimage }, false);
                        }
                    }
                    else {
                        if (params[key] != undefined && params[key] != null && params[key] != "") {
                            var fileExtension = params[key].replace(/^.*\./, '').toLowerCase();
                            that.$element.find("[data-unique-name='" + key + "'][data-element-type='file']").data("filePath", params[key]);
                            that.$element.find("[data-file-render-unique-name='" + key + "']").remove();
                            var $button = that.$element.find("[data-unique-name='" + key + "'][data-element-type='button-file']");

                            $button.text(currentOptions.file.buttonText);
                            $button.removeAttr("class");
                            $button.addClass(currentOptions.file.buttonClass);

                            $button.text(currentOptions.file.onChangeText);
                            $button.removeClass(currentOptions.file.buttonClass);
                            $button.addClass(currentOptions.file.onChangeClass);

                            if (fileUploads.image.indexOf(fileExtension) > -1) {
                                var img = $("<img height='" + currentOptions.file.imageheight + "' width='" + currentOptions.file.imagewidth + "' data-file-render-unique-name='" + currentOptions.uniqueName + "' class='" + currentOptions.file.imageClass + "' />");
                                img.attr("src", params[key]);
                                if (currentOptions.file.renderAfter) {
                                    img.css("margin-left", currentOptions.file.spaceBetween);
                                    $button.after(img);
                                }
                                else {
                                    img.css("margin-right", currentOptions.file.spaceBetween);
                                    $button.before(img);
                                }
                            }
                            else {
                                var span = $("<span class='" + currentOptions.file.imageClass + "' data-file-render-unique-name='" + currentOptions.uniqueName + "'>" + params[key] + "</span>");
                                if (currentOptions.file.renderAfter) {
                                    span.css("margin-left", currentOptions.file.spaceBetween);
                                    $button.after(span);
                                }
                                else {
                                    span.css("margin-right", currentOptions.file.spaceBetween);
                                    $button.before(span);
                                }
                            }
                        }
                    }
                }
                else if (currentOptions.type == "selectize") {
                    that.$element.find("[data-unique-name='" + key + "']").selectize('addOption', JSON.parse(params[key]));
                    that.$element.find("[data-unique-name='" + key + "']").selectize('setValue', filterObjectToArray(JSON.parse(params[key]), 'uniqueName', 'label'))
                }
                else {
                    if (currentOptions.multiple && typeof currentOptions.value !== 'string') {
                        params[key].forEach(function (val, index) {
                            var indx = parseInt(index + 1);
                            var chkVal = that.$element.find("#" + key + indx).val();
                            that.$element.find("#" + key + indx).val(val);
                            if (that.$element.find("#" + key + indx).val() != chkVal)
                                that.triggerEvents(that.$element.find("#" + key + indx), "change");
                        });
                    }
                    else {
                        var chkVal = that.$element.find("[data-unique-name='" + key + "']").val();
                        that.$element.find("[data-unique-name='" + key + "']").val(params[key]);
                        if (chkVal != params[key])
                            that.triggerEvents(that.$element.find("[data-unique-name='" + key + "']"), "change");
                    }
                }
            }
            //}
        }
    };

    FormCreate.prototype.getFormData = function (blnIsmultiple) {
        var obj = {};
        var that = this;
        this.options.data.forEach(function (obj1) {
            var row = $.extend({}, FormCreate.FIELDSDEFAULTS, obj1);
            if (row.visible) {
                if (row.type == "button") {
                }
                else if (row.type == "radio") {
                    obj[row.uniqueName] = that.$el.find("input[data-unique-name='" + row.uniqueName + "']:checked").val();
                }
                else if (row.type == "checkbox") {
                    if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                        obj[row.uniqueName] = that.$el.find("input[data-unique-name='" + row.uniqueName + "']").prop("checked") == true ? 1 : 0;
                    }
                    else {
                        var arr = [];
                        that.$el.find("input[data-unique-name='" + row.uniqueName + "']:checked").each(function () {
                            arr.push($(this).val());
                        });
                        obj[row.uniqueName] = arr;
                    }
                }
                else if (row.type == "autoComplete") {
                    obj[row.uniqueName] = that.$el.find("[data-unique-name='" + row.uniqueName + "']").data(row.uniqueName) || "";
                }
                else if (row.type == "select") {
                    obj[row.uniqueName] = that.$el.find("[data-unique-name='" + row.uniqueName + "']").val();
                }
                else if (datePick.indexOf(row.type) > -1) {
                    var val;
                    if (dateEnum.indexOf(row.type) > -1) {
                        val = that.$el.find("[data-unique-name='" + row.uniqueName + "']").datetimepicker("getValue");
                        if (val != null && val != undefined && val != "") {
                            obj[row.uniqueName] = val.getFullYear() + "-" + (val.getMonth() + 1) + "-" + val.getDate();
                        }
                    }
                    else if (timeEnum.indexOf(row.type) > -1) {
                        val = that.$el.find("[data-unique-name='" + row.uniqueName + "']").datetimepicker("getValue");
                        if (val != null && val != undefined && val != "") {
                            obj[row.uniqueName] = val.getHours() + ":" + val.getMinutes() + ":" + val.getSeconds();
                        }
                    }
                    else if (dateTimeEnum.indexOf(row.type) > -1) {
                        val = that.$el.find("[data-unique-name='" + row.uniqueName + "']").datetimepicker("getValue");
                        if (val != null && val != undefined && val != "") {
                            obj[row.uniqueName] = val.getFullYear() + "-" + (val.getMonth() + 1) + "-" + val.getDate() + " " + val.getHours() + ":" + val.getMinutes() + ":" + val.getSeconds();
                        }
                    }
                }
                else if (row.type == "file") {

                    if (!row.multiple) {
                        obj[row.uniqueName] = that.$el.find("[data-unique-name='" + row.uniqueName + "'][data-element-type='file']").data("filePath");
                    }
                    else {
                        obj[row.uniqueName] = [];
                        var uploadedfiles = that.$el.find("[data-element-container-unique-name='" + row.uniqueName + "']").fileUpload("getData");
                        uploadedfiles.forEach(function (uploadedfile, index) {
                            obj[row.uniqueName].push(uploadedfile.filePath);
                        });
                    }
                }
                else if (row.type == "label") {
                    if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                        obj[row.uniqueName] = that.$el.find("[data-unique-name='" + row.uniqueName + "']").html();
                    } else {
                        var arr = [];
                        that.$el.find("input[data-unique-name='" + row.uniqueName + "']").each(function () {
                            arr.push($(this).html());
                        });
                        obj[row.uniqueName] = arr;
                    }
                }
                else if (row.type == "selectize") {
                    if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                        obj[row.uniqueName] = JSON.stringify(that.$el.find("[data-unique-name='" + row.uniqueName + "']").selectize('getValue', 1));
                    }
                }
                else {
                    if (!row.multiple && (typeof row.value === "string" || typeof row.value === "number" || typeof row.value === "boolean")) {
                        obj[row.uniqueName] = that.$el.find("[data-unique-name='" + row.uniqueName + "']").val();
                    } else {
                        var arr = [];
                        that.$el.find("input[data-unique-name='" + row.uniqueName + "']").each(function () {
                            arr.push($(this).val());
                        });
                        obj[row.uniqueName] = arr;
                    }
                }
            }
        });
        return obj;
    };

    FormCreate.prototype.updateElement = function (params) {
        var that = this;
        if (!params.hasOwnProperty('uniqueName') || !params.hasOwnProperty('options'))
            throwError("Parameter is object with (uniqueName:'',options:'')");
        that.options.data.forEach(function (row, index) {
            if (row.uniqueName == params.uniqueName) {
                var option = $.extend({}, FormCreate.FIELDSDEFAULTS, row, params.options);

                if (option.elementPanel == undefined) {
                    option.elementPanel = that.options.elementPanel;
                }
                if (option.labelContainer == undefined) {
                    option.labelContainer = that.options.labelContainer;
                }
                if (option.elementContainer == undefined) {
                    option.elementContainer = that.options.elementContainer;
                }
                var uniqueName = params.uniqueName;
                var beforeUniqueName = "";
                if (index > 0) {
                    beforeUniqueName = that.options.data[index - 1].uniqueName;
                }
                that.removeElement([uniqueName]);
                that.options.data.splice(index, 0, option);
                that.createElement(option, 0, beforeUniqueName);
            }
        });
        //this.initElement(true);
    };

    FormCreate.prototype.addElement = function (params) {
        var that = this;
        if (!params.hasOwnProperty('uniqueName') || !params.hasOwnProperty('options'))
            throwError("Parameter is object with (uniqueName,options)");
        params.options.forEach(function (option, index) {
            option = $.extend({}, FormCreate.FIELDSDEFAULTS, option);
            if (option.elementPanel == undefined) {
                option.elementPanel = that.options.elementPanel;
            }
            if (option.labelContainer == undefined) {
                option.labelContainer = that.options.labelContainer;
            }
            if (option.elementContainer == undefined) {
                option.elementContainer = that.options.elementContainer;
            }
            var uniqueName = params.uniqueName;
            if (index != 0)
                uniqueName = params.options[index - 1].uniqueName;
            that.createElement(option, 0, uniqueName);
            that.options.data.push(option);

        });
        this.elementDetails();
        // this.initElement(true);
    };

    FormCreate.prototype.appendElement = function (params) {
        var that = this;
        if (params == undefined || params == "")
            throwError("give values in array");
        params.forEach(function (option) {
            option = $.extend({}, FormCreate.FIELDSDEFAULTS, option);

            if (option.elementPanel == undefined) {
                option.elementPanel = that.options.elementPanel;
            }
            if (option.labelContainer == undefined) {
                option.labelContainer = that.options.labelContainer;
            }
            if (option.elementContainer == undefined) {
                option.elementContainer = that.options.elementContainer;
            }

            that.createElement(option, that.options.data.length);
            that.options.data.push(option);
        });
        this.elementDetails();
    };

    FormCreate.prototype.removeElement = function (arrUniqueNames) {
        var that = this;
        if (arrUniqueNames == undefined || arrUniqueNames == "")
            throwError("give values is array");
        arrUniqueNames.forEach(function (unique) {
            that.options.data.forEach(function (row, index) {
                if (row.uniqueName == unique) {
                    that.options.data.splice(index, 1);
                    that.$el.find("[" + dataEnum.elementPanel[0] + "=" + unique + "]").remove();
                    that.$el.find("[" + dataEnum.labelContainer[0] + "=" + unique + "]").remove();
                    that.$el.find("[" + dataEnum.elementContainer[0] + "=" + unique + "]").remove();
                    that.$el.find("[data-unique-name=" + unique + "]").remove();
                }
            });
        });
        this.elementDetails();
        //}
        //else {
        //    that.options.data.forEach(function (row, index) {
        //        if (row.uniqueName == arrUniqueNames[0]) {
        //            that.options.data.splice(index, 1);
        //        }
        //    });
        //}
        //this.initElement(true);
    };

    FormCreate.prototype.removeAllElement = function () {
        this.options.data = [];
        this.$el.find("[" + dataEnum.elementPanel[0] + "]").remove();
        this.$el.find("[" + dataEnum.labelContainer[0] + "]").remove();
        this.$el.find("[" + dataEnum.elementContainer[0] + "]").remove();
        this.$el.find("[data-unique-name" + "]").remove();
        this.elementDetails();
    };

    FormCreate.prototype.showElement = function (arrUniqueNames) {
        var that = this;
        if (arrUniqueNames == undefined || arrUniqueNames == "")
            throwError("give values is array");

        arrUniqueNames.forEach(function (uniqueName) {
            var option = that.getOption(uniqueName);
            if (!option.visible) {
                that.updateElement({ uniqueName: uniqueName, options: $.extend({}, option, { visible: true }) });
            }
            for (var key in dataEnum) {
                $("[" + dataEnum[key][0] + "='" + uniqueName + "']").removeClass("hidden");
            }
        });
        this.elementDetails();
    };

    FormCreate.prototype.hideElement = function (arrUniqueNames) {
        var that = this;
        if (arrUniqueNames == undefined || arrUniqueNames == "" || arrUniqueNames.length <= 0)
            throwError("give values is array");
        arrUniqueNames.forEach(function (uniqueName) {
            var option = that.getOption(uniqueName);

            //if (option.visible) {
            that.updateElement({ uniqueName: uniqueName, options: $.extend({}, option, { visible: false }) });
            //}

            //for (var key in dataEnum) {
            //    $("[" + dataEnum[key][0] + "='" + uniqueName + "']").addClass("hidden");
            //}
        });
        this.elementDetails();
    };

    FormCreate.prototype.initValidate = function (target, row) {
        if (row.isRequire) {
            target.addClass(validateEnum.require);
        }
        var validate = row.validate;
        for (var val in validate) {
            target.attr("data-" + val, validate[val]);
        }
    }

    FormCreate.prototype.getDataFromServer = function (row) {
        var that = this, data = [];
        var options = row.multipleOptions;
        if (options.ajaxOptions != undefined && options.ajaxOptions.url != undefined) {
            if (row.DependancyField != undefined && row.DependancyField != null && row.DependancyField != "") {
                var ojjj = {};
                if (typeof options.ajaxOptions.data.strJSON == "string")
                    options.ajaxOptions.data.strJSON = JSON.parse(options.ajaxOptions.data.strJSON);
                var opp = options.ajaxOptions.data.strJSON;
                if (opp.cols == undefined || opp.cols == null || opp.cols == "") {
                    opp.cols = {};
                }
                else {
                    if (typeof opp.cols == "string") {
                        opp.cols = JSON.parse(opp.cols);
                    }
                }

                if (opp.cols[row.DependancyField] != undefined && opp.cols[row.DependancyField] != null && opp.cols[row.DependancyField] != "" && opp.cols[row.DependancyField] != row.zeroVal) {
                    ojjj[row.DependancyField] = opp.cols[row.DependancyField];
                }
                else {
                    ojjj[row.DependancyField] = row.zeroVal;
                }
                opp.cols = $.extend({}, opp.cols, ojjj);
                opp.cols = JSON.stringify(opp.cols);
                options.ajaxOptions.data.strJSON = JSON.stringify(opp);
            }

            var request = $.extend({},
                options.ajaxOptions,
                {
                    type: options.ajaxOptions.type,
                    url: options.ajaxOptions.url,
                    data: options.ajaxOptions.data === undefined
                        ? ""
                        : JSON.stringify(options.ajaxOptions.data),
                    contentType: options.ajaxOptions.contentType,
                    dataType: options.ajaxOptions.dataType,
                    async: false,
                    success: function (res) {
                        if (res.d != undefined && res.d != null && res.d != "") {
                            if (typeof res.d == "string") {
                                data = JSON.parse(res.d);
                            }
                        }
                        else {
                            data = [];
                        }
                    },
                    error: function (res) {
                        console.log('load-error', res.status, res);
                    }
                });
            $.ajax(request);
            return data;
        }
    }


    FormCreate.allowedMethods = [
        "hideElement", "showElement",
        "removeElement", "removeAllElement", "addElement", "appendElement", "updateElement",
        "setFormData", "getFormData",
        "actualData", "getOption", "getText",
        "updateData",
        "destroy", "resetForm", "resetDataOnly",
        "uploadFiles", "deleteFiles"
    ];

    $.fn.formCreate = function (option) {
        var value,
            args = Array.prototype.slice.call(arguments, 1);
        $(this).each(function () {
            var $this = $(this),
            data = $this.data('bnb.form'),
            options = $.extend({}, FormCreate.DEFAULTS, $this.data(),
                typeof option === 'object' && option);
            if (typeof option === 'string') {
                if ($.inArray(option, FormCreate.allowedMethods) < 0)
                    throw new Error("Unknown method: " + option);
                if (!data)
                    return;
                value = data[option].apply(data, args);
                if (option === 'destroy')
                    $this.removeData('bnb.form');
            }
            if (!data)
                $(this).data("bnb.form", new FormCreate(this, options));
        });
        //return typeof value === "undefined" ? this : value;
        return value;
    };

    var checkableEnum = ["checkbox", "radio"];
    var Element = function (target, options) {
        this.$el = $(target);
        this.$el_ = this.$el.clone();
        this.options = options;
        this.init();
    };
    Element.ARRAYDEFAULTS = {
        data: undefined,
        ID: undefined,
        target: undefined,
        display: "display",
        value: "value",
        fieldOptions: [{}]
    }
    Element.DEFAULTS = {
        display: "display",
        value: "value",
        autocompleteUrl: "WebServices/BNBPlugins.asmx/Operations",
        ID: undefined,
        validateName: "",
        target: undefined,
        data: undefined,

        isRequire: false,
        mentionRequireSymbol: true,
        requireSymbol: "*",
        requireSymbolClass: "",

        parent: {
        },

        elementClass: "",
        labelClass: "",
        type: "select",
        disabled: false,
        elementFirst: false,
        withoutLabel: false,
        elementPanel: "",
        labelContainer: "",
        elementContainer: "",
        beforeLabel: "",
        afterElement: "",
        marginBetweenElementLabel: "",
        paddingBetweenElementLabel: "",
        spaceBetweenOptions: "",
        maximumLength: "",
        attr: {},
        ajaxOptions: {},
        select: {},
        checkbox: {},
        radio: {},
        autoComplete: {},
        date: {},
        events: {
            "focus": function () { },
            "blur": function () { },
            "change": function () { },
            "keypress": function () { },
            "keydown": function () { },
            "keyup": function () { },
            "click": function () { }
        },
        IncludeTimestampIcon: "CSS/Images/IncludeTimestamp.png",
        IncludeTimestampIconClick: function (target) {

        },


        DependancyFieldFrom: undefined,
        DependancyField: undefined,

        parentDefaults: undefined,
        zeroVal: "",
    };
    Element.SELECTDEFAULTS = {
        multiSelect: false,
        multipleMode: 'label',
        input: '<input type="text" maxLength="20" placeholder="Search" />',
        emptyOption: true,
        emptyOptionText: "--Select--",
        emptyOptionValue: "",
        defaultSelectedVal: "",
        multiSelectOptions: {
        }
    };
    Element.CHECKBOXDEFAULTS = {
        selectAll: true,
        defaultSelectedVal: "",
        selectAllText: "Select All"
    };
    Element.RADIODEFAULTS = {
        defaultSelectedVal: "",
    };
    Element.AUTOCOMPLETEDEFAULTS = {
        sp_name: "",
        fromMetadata: false,
        json: "",
        searchOnFocus: true,
        multiSelect: false
    };

    Element.prototype.init = function () {
        var that = this;
        this.createParentElement();
        if (this.options.type != "autoComplete") {
            //if (this.options.ajaxOptions != undefined && this.options.ajaxOptions.url != undefined) {
            //    //if (this.options.DependancyFieldFrom == undefined || this.options.DependancyFieldFrom == null || this.options.DependancyFieldFrom == "") {
            //    this.getDataFromServer();
            //    //}
            //}
            //else {
            this.initData();
            //}
        }
        else {
            this.initData();
        }
        //this.elementDetails();
    };

    Element.prototype.createParentElement = function () {
        //if (this.$el[0].nodeName.toLowerCase() == this.options.type.toLowerCase())
        //    this.$element = this.$el;
        if (this.options.ID != undefined && $("#" + this.options.ID).length != 0)
            this.$element = $("#" + this.options.ID);
        else if (this.options.target != undefined && $(this.options.target).length != 0)
            this.$element = $(this.options.target);
        else {
            if (this.options.type == "select" || this.options.type == "autoComplete") {
                this.$element = $([elementEnum[this.options.type]].join(''));
                this.$element.attr({ "id": this.options.ID, "data-unique-name": this.options.ID });

                this.$el.append(this.$element);
                this.$element.addClass(this.options.elementClass);
            }
            else {
                this.$element = this.$el;
            }
        }
        var multiAttr = this.$element.attr("multiple");
        if (typeof multiAttr !== typeof undefined && multiAttr !== false)
            this.options[this.options.type].multiSelect = true;
    };

    Element.prototype.initData = function () {
        this.$element.html("");
        var that = this;
        if (this.options.hasOwnProperty('fieldOptions')) {
            this.options.data.forEach(function (option, index) {
                var option123 = $.extend({}, $.extend({}, Element.DEFAULTS, that.options.fieldOptions[index]), that.options, { data: [option] });
                that.renderData(option123);
            });
        }
        else {
            that.renderData(this.options);
        }
    };

    Element.prototype.renderData = function (options) {
        var that = this;
        if (options.attr == "")
            options.attr = { autocomplete: "off" };
        else if (typeof options.attr == "string") {
            options.attr = JSON.parse(options.attr);
        }
        options.attr = $.extend({}, options.attr, { autocomplete: "off" });

        if (options.type == "autoComplete") {

            var arr = [options.display, options.value]
            that.$element.data("data-keys", arr);

            that.$element.autocomplete($.extend({},
                {
                    source: function (request, response) {//ttt
                        var objAC = {};
                        if (options.json != "" && options.json != undefined && options.json != null) {
                            if (typeof options.json == "string") {
                                options.json = JSON.parse(options.json);
                            }
                            objAC = options.json;
                        }

                        if (options.DependancyFieldFrom != undefined && options.DependancyFieldFrom != null && options.DependancyFieldFrom != "") {
                            var selectedValues = options.parentDefaults.getFormData();
                            if (selectedValues == undefined || selectedValues == null || selectedValues == "") {
                                selectedValues = {};
                                selectedValues[options.DependancyFieldFrom] = options.zeroVal;
                            }
                            if (selectedValues[options.DependancyFieldFrom] == undefined || selectedValues[options.DependancyFieldFrom] == null || selectedValues[options.DependancyFieldFrom] == "") {
                                selectedValues[options.DependancyFieldFrom] = options.zeroVal;
                            }
                            if (Array.isArray(selectedValues[options.DependancyFieldFrom])) {
                                selectedValues[options.DependancyFieldFrom] = selectedValues[options.DependancyFieldFrom].join(',');
                            }
                            objAC[options.DependancyField] = selectedValues[options.DependancyFieldFrom];
                        }

                        if (options.fromMetadata) {
                            objAC['metadata'] = request.term;
                        }
                        else {
                            objAC[options.display] = request.term;
                        }
                        var json = JSON.stringify(objAC);
                        json = JSON.stringify([{ ps: options.sp_name, cols: json }]);
                        $.ajax({
                            type: "POST",
                            dataType: "Json",
                            contentType: "application/json; charset=utf-8",
                            url: that.options.autocompleteUrl,
                            data: JSON.stringify({ strJSON: json, intTimeout: 30, strConnection: conn }),
                            success: function (data) {
                                if (data.d != "" && data.d != null && data.d != undefined) {
                                    data = JSON.parse(data.d);
                                    response($.map(data, function (item) {
                                        return {
                                            label: item[options.display],
                                            val: item[options.value],
                                            obj: item
                                        }
                                    }))
                                }
                                else {
                                    data = [];
                                    response($.map(data, function (item) {
                                        return {
                                            label: item[options.display],
                                            val: item[options.value],
                                            obj: item
                                        }
                                    }))
                                }
                            },
                            error: function (response) {
                                console.log("error");
                            }
                        });

                        //response($.map(options.data,
                        //    function (item) {
                        //        if (item[options.display].toLowerCase().indexOf(request.term.toLowerCase()) > -1) {
                        //            return {
                        //                label: item[options.display],
                        //                val: item[options.value]
                        //            }
                        //        }
                        //    }));
                    },
                    select: function (e, i) {
                        that.$element.data(options.ID, i.item.val);
                        for (var key in i.item.obj) {
                            that.$element.data(key, i.item.obj[key]);
                        }
                        that.$element.trigger("change");
                    }
                },
                options["autoComplete"])).focus(function () {
                    that.$element.autocomplete("search", " ");
                });
            that.$element.addClass(options.className);
            if ((options.parent != null && options.parent != "" && options.parent != undefined && typeof options.parent == "object") && (options.parent.isRequire)) {
                that.$element.addClass(validateEnum.require);
            }
            that.$element.data("validate-name", options.validateName);
            that.$element.prop("disabled", options.disabled);
            that.$element.attr(options.attr);
            that.$element.on("blur", function () {
                if ($(this).val() != "" && (that.$element.data(options.ID) == "" || that.$element.data(options.ID) == undefined || that.$element.data(options.ID) == null))
                    $(this).val("");
            });
            that.initEvents(that.$element, options.events);
            that.initValidate(that.$element, options);
        }
        else {
            if (options.type == "checkbox") {
                options.checkbox = $.extend({}, Element.CHECKBOXDEFAULTS, options.checkbox);
                if (options.checkbox.selectAll) {
                    var option = $(elementEnum[options.type]);
                    option.prop("disabled", options.disabled);
                    option.attr({
                        "name": options.ID,
                        "value": "0",
                        "id": options.ID + "0",
                        "data-element-type": "checkAll"
                    });

                    option.addClass(options.elementClass);
                    option.css({
                        "margin": options.marginBetweenElementLabel,
                        "padding": options.paddingBetweenElementLabel
                    });
                    if (options.elementContainer != undefined && options.elementContainer != "") {
                        var option1 = option;
                        option = $(options.elementContainer);
                        if (option.find(".input-selector").length > 0) {
                            option.find(".input-selector").append(option1);
                        }
                        else {
                            option.append(option1);
                        }
                    }

                    if (option.find(".input-selector").length > 0) {
                        option.find(".input-selector").after(options.afterElement);
                    }
                    else {
                        option.after(options.afterElement);
                    }

                    if (options.elementPanel != undefined && options.elementPanel != "")
                        that.$element.append($(options.elementPanel).append(option).css("margin-right", options.spaceBetweenOptions));
                    else
                        that.$element.append(option);

                    if (!options.withoutLabel) {
                        var label;
                        label = $(elementEnum.label);
                        label.attr("for", options.ID + "0");
                        label.html(options.checkbox.selectAllText);
                        label.addClass(options.labelClass);
                        label.css({
                            "margin": options.marginBetweenElementLabel,
                            "padding": options.paddingBetweenElementLabel
                        });

                        if (options.labelContainer != undefined && options.labelContainer != "") {
                            //label = $(options.labelContainer).append(label);
                            var label1 = label;
                            label = $(options.labelContainer)
                            if (label.find(".label-selector").length > 0) {
                                label.find(".label-selector").append(label1);
                            }
                            else {
                                label.append(label1);
                            }
                        }
                        if (options.elementFirst)
                            option.after(label);
                        else
                            option.before(label);

                        if (label.find(".label-selector").length > 0) {
                            label.find(".label-selector").before(options.beforeLabel);
                        }
                        else {
                            label.before(options.beforeLabel);
                        }

                        if (options.elementFirst) {
                            label.css("margin-right", options.spaceBetweenOptions);
                        }
                    }
                    else {
                        if (option.find(".input-selector").length > 0) {
                            option.find(".input-selector").before(options.beforeLabel);
                        }
                        else {
                            option.before(options.beforeLabel);
                        }
                        if (!options.elementFirst) {
                            option.css("margin-right", options.spaceBetweenOptions);
                        }
                    }
                }
            }
            else if (options.type == "radio") {
                options.radio = $.extend({}, Element.RADIODEFAULTS, options.radio);
            }
            if (Array.isArray(options.data)) {
                options.data.forEach(function (row, index) {
                    if (options.type == "select") {
                        var option = $(elementEnum.option);
                        option.html(row[options.display]);
                        option.attr($.extend({}, { "value": row[options.value] }, options.attr));
                        that.$element.append(option);
                        for (var key in row)
                            if (key != options.display && key != options.value)
                                option.data(key, row[key]);
                    }
                    else {
                        var element = that.renderElement(options, options.type, row, index);
                        that.renderLabel(options, row, index, element);
                    }
                });
            }
            if (options.type == "select") {
                options.select = $.extend({}, Element.SELECTDEFAULTS, options.select);
                if (options.select.emptyOption) {
                    if (that.$element.find("option:first-child").length == 0) {
                        that.$element.append("<option value='" + options.select.emptyOptionValue + "'>" + options.select.emptyOptionText + "</option>");
                    }
                    else {
                        that.$element.find("option:first-child").before("<option value='" + options.select.emptyOptionValue + "'>" + options.select.emptyOptionText + "</option>");
                    }
                }
                that.$element.val(options.select.defaultSelectedVal);
                that.$element.data("validate-name", options.validateName);
                var dat = [];
                dat = arr2dropArr(options.data, options.display, options.value);
                if ((options.parent != null && options.parent != "" && options.parent != undefined && typeof options.parent == "object") && (options.parent.isRequire)) {
                    that.$element.addClass(validateEnum.require);
                }
                if (options.select.multiSelect) {
                    that.$element.attr("multiple", "multiple");
                    var opts = $.extend({},
                        Element.SELECTDEFAULTS,
                        options.select);
                    that.$el.dropdown(opts);
                }
                else {
                    that.$element.removeAttr("multiple");
                }

                that.$element.addClass(options.className);

                that.$element.prop("disabled", options.disabled);
                that.$element.attr(options.attr);
                that.initEvents(that.$element, options.events);
                that.initValidate(that.$element, options);
            }
            if (options.type == "checkbox") {
                checkAll(this.$element);
            }
        }
        this.elementDetails();
    };

    var arr2dropArr = function (arr, labelField, valueField) {
        var arrR = [];
        if (!Array.isArray(arr)) {
            return arrR;
        }
        arr.forEach(function (row) {
            var objR = {};
            objR['label'] = row[labelField];
            objR['value'] = row[valueField];
            arrR.push(objR);
        })
        return arrR;
    };

    Element.prototype.elementDetails = function () {
        var that = this;

        if (this.$element.find("input[data-element-type='ldate']").length > 0) {
            this.$element.find("input[data-element-type='ldate']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "d-m-Y" }, that.options.date, { maxDate: new Date(), timepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='udate']").length > 0) {
            this.$element.find("input[data-element-type='udate']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "d-m-Y" }, that.options.date, { minDate: new Date(), timepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='date']").length > 0) {
            this.$element.find("input[data-element-type='date']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "d-m-Y" }, that.options.date, { timepicker: false }));
            });
        }

        if (this.$element.find("input[data-element-type='ldatetime']").length > 0) {
            this.$element.find("input[data-element-type='ldatetime']").each(function () {
                $(this).datetimepicker($.extend({}, that.options.date, { maxDate: new Date(), maxTime: new Date().getTime() }));
            });
        }
        if (this.$element.find("input[data-element-type='udatetime']").length > 0) {
            this.$element.find("input[data-element-type='udatetime']").each(function () {
                $(this).datetimepicker($.extend({}, that.options.date, { minDate: new Date(), minTime: new Date().getTime() }));
            });
        }
        if (this.$element.find("input[data-element-type='datetime']").length > 0) {
            this.$element.find("input[data-element-type='datetime']").each(function () {
                $(this).datetimepicker(that.options.date);
            });
        }

        if (this.$element.find("input[data-element-type='ltime']").length > 0) {
            this.$element.find("input[data-element-type='ltime']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "H:i:s" }, that.options.date, { maxDate: new Date(), datepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='utime']").length > 0) {
            this.$element.find("input[data-element-type='utime']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "H:i:s" }, that.options.date, { minDate: new Date(), datepicker: false }));
            });
        }
        if (this.$element.find("input[data-element-type='time']").length > 0) {
            this.$element.find("input[data-element-type='time']").each(function () {
                $(this).datetimepicker($.extend({}, { format: "H:i:s" }, that.options.date, { datepicker: false }));
            });
        }

        if (this.$element.find("[data-element-type='checkbox']").length > 0) {
            this.$element.find("[data-element-type='checkbox']").click(function () {
                checkAll(that.$element);
            });
        }
        if (this.$element.find("[data-element-type='checkAll']").length > 0) {
            this.$element.find("[data-element-type='checkAll']").change(function () {
                that.$element.find("[data-element-type='checkbox'][data-unique-name='" + $(this).attr("name") + "']")
                    .prop("checked", $(this).prop("checked"));
            });
        }

        //if (this.$element.find("input[data-element-type='phonenumber']").length > 0)
        //    this.$element.find("input[data-element-type='phonenumber']").mask("(999) 999-9999");

    };

    Element.prototype.renderElement = function (options, type, row, index) {
        if (options.attr == "" || options.attr == null) {
            options.attr = {};
        }
        var element, that = this;
        element = $(elementEnum[type]);

        element.prop("disabled", options.disabled);

        if (options.maximumLength != "" && options.maximumLength != undefined && options.maximumLength != null) {
            element.attr("maxlength", options.maximumLength);
        }
        element.attr($.extend({}, { "value": row[options.value], "id": options.ID + (index + 1), "name": options.ID, "data-unique-name": options.ID }, options.attr));
        element.data("validate-name", row.validateName);
        if (type == "button")
            element.html(row[options.value]);
        element.addClass(options.elementClass);
        if (index == 0) {
            if ((options.parent != null && options.parent != "" && options.parent != undefined && typeof options.parent == "object") && (options.parent.isRequire)) {
                element.addClass(validateEnum.require);
            }
        }

        for (var key in row)
            if (key != options.display && key != options.value)
                element.data(key, row[key]);
        that.initEvents(element, options.events);
        that.initValidate(element, options);

        if (checkableEnum.indexOf(options.type) != -1) {
            element.prop("checked", row[options.value]);
            if (options[options.type].defaultSelectedVal.indexOf(row[options.value]) > -1) {
                element.prop("checked", true);
            }
            else {
                element.prop("checked", false);
            }
        }
        if (options.elementContainer != undefined && options.elementContainer != "") {
            //element = $(options.elementContainer).append(element);
            var element1 = element;
            element = $(options.elementContainer);
            if (element.find(".input-selector").length > 0) {
                element.find(".input-selector").append(option1);
            }
            else {
                element.append(element1);
            }
        }
        element.css({
            "margin": options.marginBetweenElementLabel,
            "padding": options.paddingBetweenElementLabel,
        });
        if (options.elementPanel != undefined && options.elementPanel != "")
            that.$element.append($(options.elementPanel).append(element).css("margin-right", options.spaceBetweenOptions));
        else
            that.$element.append(element);

        if (element.find(".input-selector").length > 0) {
            element.find(".input-selector").after(options.afterElement);
        }
        else {
            element.after(options.afterElement);
        }

        return element;
    };

    Element.prototype.renderLabel = function (options, row, index, element) {
        var label, that = this;
        if (!options.withoutLabel) {
            label = $(elementEnum.label);
            label.attr({ "for": options.ID + (index + 1) });

            if (row.type == "textarea" && row.IncludeTimestamp) {
                label.append("<img width='30' height='30' class='IncludeTimestampIcon' src='" + row.IncludeTimestampIcon + "'/>");
                label.find(".IncludeTimestampIcon").click(function () {
                    if (typeof row.IncludeTimestampIconClick == "function") {
                        row.IncludeTimestampIconClick.call(this, $("#" + options.ID))
                    }
                });
            }
            label.addClass(options.labelClass);
            label.html(row[options.display]);
            label.addClass(options.labelClass);

            if (options.isRequire && options.mentionRequireSymbol) {
                label.append("<code class='requireSymbol " + options.requireSymbolClass + "'>" + options.requireSymbol + "</code>");
            }
            label.css({
                "margin": options.marginBetweenElementLabel,
                "padding": options.paddingBetweenElementLabel
            });
            if (options.labelContainer != undefined && options.labelContainer != "") {
                //label = $(options.labelContainer).append(label);
                var label1 = label;
                label = $(options.labelContainer);
                if (label.find(".label-selector").length > 0) {
                    label.find(".label-selector").append(label1);
                }
                else {
                    label.append(label1);
                }
            }
            if (options.elementFirst)
                element.after(label);
            else
                element.before(label);

            //element.before(row.beforeLabel);
            if (label.find(".label-selector").length > 0) {
                label.find(".label-selector").before(row.beforeLabel);
            }
            else {
                label.before(row.beforeLabel);
            }

            if (options.elementFirst) {
                label.css("margin-right", options.spaceBetweenOptions);
            }
        }
        else {
            if (element.find(".input-selector").length > 0) {
                element.find(".input-selector").before(options.beforeLabel);
            }
            else {
                element.before(options.beforeLabel);
            }

            //element.before(options.beforeLabel);
            if (!options.elementFirst) {
                element.css("margin-right", options.spaceBetweenOptions);
            }
        }


    };

    Element.prototype.getDataFromServer = function () {
        var that = this;

        if (that.options.ajaxOptions != undefined && that.options.ajaxOptions.url != undefined) {
            if (that.options.DependancyField != undefined && that.options.DependancyField != null && that.options.DependancyField != "") {
                var ojjj = {};
                if (typeof that.options.ajaxOptions.data.strJSON == "string")
                    that.options.ajaxOptions.data.strJSON = JSON.parse(that.options.ajaxOptions.data.strJSON);
                var opp = that.options.ajaxOptions.data.strJSON;
                if (opp.cols == undefined || opp.cols == null || opp.cols == "") {
                    opp.cols = {};
                }
                else {
                    if (typeof opp.cols == "string") {
                        opp.cols = JSON.parse(opp.cols);
                    }
                }

                if (opp.cols[that.options.DependancyField] != undefined && opp.cols[that.options.DependancyField] != null && opp.cols[that.options.DependancyField] != "" && opp.cols[that.options.DependancyField] != that.options.zeroVal) {
                    ojjj[that.options.DependancyField] = opp.cols[that.options.DependancyField];
                }
                else {
                    ojjj[that.options.DependancyField] = that.options.zeroVal;
                }
                opp.cols = $.extend({}, opp.cols, ojjj);
                opp.cols = JSON.stringify(opp.cols);
                that.options.ajaxOptions.data.strJSON = JSON.stringify(opp);
            }

            var request = $.extend({},
                that.options.ajaxOptions,
                {
                    type: that.options.ajaxOptions.type,
                    url: that.options.ajaxOptions.url,
                    data: that.options.ajaxOptions.data === undefined
                        ? ""
                        : JSON.stringify(that.options.ajaxOptions.data),
                    contentType: that.options.ajaxOptions.contentType,
                    dataType: that.options.ajaxOptions.dataType,
                    async: false,
                    success: function (res) {
                        if (res.d != undefined && res.d != null && res.d != "") {
                            if (typeof res.d == "string") {
                                that.options.data = JSON.parse(res.d);
                                that.initData();
                            }
                        }
                        else {
                            that.options.data = [];
                            that.initData();
                        }
                    },
                    error: function (res) {
                        console.log('load-error', res.status, res);
                    }
                });
            $.ajax(request);
        }
    };

    Element.prototype.initEvents = function (target, events) {

        var that = this;
        if (events != undefined)
            for (var event in events)
                if (typeof events[event] === "string")
                    target.on(event, function () { eval(events[event]) });
                else
                    target.on(event, events[event]);
        else
            for (var event in that.options.events)
                if (typeof that.options.events[event] === "string")
                    target.on(event, function () { eval(that.options.events[event]) });
                else
                    target.on(event, that.options.events[event]);

    };

    Element.prototype.initValidate = function (target, row) {
        if (row.isRequire) {
            target.addClass(validateEnum.require);
        }
        var validate = row.validate;
        for (var val in validate) {
            target.attr("data-" + val, validate[val]);
        }
    };
    //Functions
    Element.prototype.updateData = function (data) {
        this.options.data = data;
        this.initData();
    };

    Element.prototype.destroy = function () {
        this.$el.html(this.$el_.html());
    };

    Element.prototype.refreshData = function () {
        if (that.options.ajaxOptions != undefined && that.options.ajaxOptions.url != undefined) {
            this.getDataFromServer();
        }
        else {
            this.initData();
        }
        //this.init();
    };

    Element.prototype.updateElement = function (options) {
        this.options = $.extend({}, this.options, options);
        this.init();
    };

    Element.allowedMethods = [
        "updateElement",
        "updateData",
        "destroy",
        "refreshData"
    ];
    $.fn.element = function (option) {
        var value,
            args = Array.prototype.slice.call(arguments, 1);
        $(this).each(function () {
            var $this = $(this),
            data = $this.data('bnb.element'),
            options;

            if (typeof option === 'string') {
                if ($.inArray(option, Element.allowedMethods) < 0)
                    throw new Error("Unknown method: " + option);
                if (!data)
                    return;
                value = data[option].apply(data, args);
                if (option === 'destroy')
                    $this.removeData('bnb.element');

            }
            if (option.hasOwnProperty('fieldOptions')) {
                options = $.extend({}, Element.ARRAYDEFAULTS, $this.data(), typeof option === 'object' && option);
            }
            else {
                options = $.extend({}, Element.DEFAULTS, $this.data(), typeof option === 'object' && option);
            }
            if (!data)
                $(this).data("bnb.element", new Element(this, options));
        });
        //return typeof value === 'undefined' ? this : value;
        return value;
    };


}(jQuery));

//Bootstrap Notify
(function ($) {
    // Create the defaults once
    var defaults = {
        element: 'body',
        position: null,
        type: "info",
        allow_dismiss: true,
        allow_duplicates: true,
        newest_on_top: false,
        showProgressbar: false,
        placement: {
            from: "top",
            align: "right"
        },
        offset: 20,
        spacing: 10,
        z_index: 1055,
        delay: 5000,
        timer: 1000,
        url_target: '_blank',
        mouse_over: null,
        animate: {
            enter: 'animated fadeInDown',
            exit: 'animated fadeOutUp'
        },
        onShow: null,
        onShown: null,
        onClose: null,
        onClosed: null,
        onClick: null,
        icon_type: 'class',
        template: '<div data-notify="container" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert"><button type="button" aria-hidden="true" class="close" data-notify="dismiss">&times;</button><span data-notify="icon"></span> <span data-notify="title">{1}</span> <span data-notify="message">{2}</span><div class="progress" data-notify="progressbar"><div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div></div><a href="{3}" target="{4}" data-notify="url"></a></div>'
    };

    String.format = function () {
        var args = arguments;
        var str = arguments[0];
        return str.replace(/(\{\{\d\}\}|\{\d\})/g, function (str) {
            if (str.substring(0, 2) === "{{") return str;
            var num = parseInt(str.match(/\d/)[0]);
            return args[num + 1];
        });
    };

    function isDuplicateNotification(notification) {
        var isDupe = false;

        $('[data-notify="container"]').each(function (i, el) {
            var $el = $(el);
            var title = $el.find('[data-notify="title"]').html().trim();
            var message = $el.find('[data-notify="message"]').html().trim();

            // The input string might be different than the actual parsed HTML string!
            // (<br> vs <br /> for example)
            // So we have to force-parse this as HTML here!
            var isSameTitle = title === $("<div>" + notification.settings.content.title + "</div>").html().trim();
            var isSameMsg = message === $("<div>" + notification.settings.content.message + "</div>").html().trim();
            var isSameType = $el.hasClass('alert-' + notification.settings.type);

            if (isSameTitle && isSameMsg && isSameType) {
                //we found the dupe. Set the var and stop checking.
                isDupe = true;
            }
            return !isDupe;
        });

        return isDupe;
    }

    function Notify(element, content, options) {
        // Setup Content of Notify
        var contentObj = {
            content: {
                message: typeof content === 'object' ? content.message : content,
                title: content.title ? content.title : '',
                icon: content.icon ? content.icon : '',
                url: content.url ? content.url : 'javascript:void(0)',
                target: content.target ? content.target : '-'
            }
        };

        options = $.extend(true, {}, contentObj, options);
        this.settings = $.extend(true, {}, defaults, options);
        this._defaults = defaults;
        if (this.settings.content.target === "-") {
            this.settings.content.target = this.settings.url_target;
        }
        this.animations = {
            start: 'webkitAnimationStart oanimationstart MSAnimationStart animationstart',
            end: 'webkitAnimationEnd oanimationend MSAnimationEnd animationend'
        };

        if (typeof this.settings.offset === 'number') {
            this.settings.offset = {
                x: this.settings.offset,
                y: this.settings.offset
            };
        }

        //if duplicate messages are not allowed, then only continue if this new message is not a duplicate of one that it already showing
        if (this.settings.allow_duplicates || (!this.settings.allow_duplicates && !isDuplicateNotification(this))) {
            this.init();
        }
    }

    $.extend(Notify.prototype, {
        init: function () {
            var self = this;

            this.buildNotify();
            if (this.settings.content.icon) {
                this.setIcon();
            }
            if (this.settings.content.url != "#") {
                this.styleURL();
            }
            this.styleDismiss();
            this.placement();
            this.bind();

            this.notify = {
                $ele: this.$ele,
                update: function (command, update) {
                    var commands = {};
                    if (typeof command === "string") {
                        commands[command] = update;
                    } else {
                        commands = command;
                    }
                    for (var cmd in commands) {
                        switch (cmd) {
                            case "type":
                                this.$ele.removeClass('alert-' + self.settings.type);
                                this.$ele.find('[data-notify="progressbar"] > .progress-bar').removeClass('progress-bar-' + self.settings.type);
                                self.settings.type = commands[cmd];
                                this.$ele.addClass('alert-' + commands[cmd]).find('[data-notify="progressbar"] > .progress-bar').addClass('progress-bar-' + commands[cmd]);
                                break;
                            case "icon":
                                var $icon = this.$ele.find('[data-notify="icon"]');
                                if (self.settings.icon_type.toLowerCase() === 'class') {
                                    $icon.removeClass(self.settings.content.icon).addClass(commands[cmd]);
                                } else {
                                    if (!$icon.is('img')) {
                                        $icon.find('img');
                                    }
                                    $icon.attr('src', commands[cmd]);
                                }
                                self.settings.content.icon = commands[command];
                                break;
                            case "progress":
                                var newDelay = self.settings.delay - (self.settings.delay * (commands[cmd] / 100));
                                this.$ele.data('notify-delay', newDelay);
                                this.$ele.find('[data-notify="progressbar"] > div').attr('aria-valuenow', commands[cmd]).css('width', commands[cmd] + '%');
                                break;
                            case "url":
                                this.$ele.find('[data-notify="url"]').attr('href', commands[cmd]);
                                break;
                            case "target":
                                this.$ele.find('[data-notify="url"]').attr('target', commands[cmd]);
                                break;
                            default:
                                this.$ele.find('[data-notify="' + cmd + '"]').html(commands[cmd]);
                        }
                    }
                    var posX = this.$ele.outerHeight() + parseInt(self.settings.spacing) + parseInt(self.settings.offset.y);
                    self.reposition(posX);
                },
                close: function () {
                    self.close();
                }
            };

        },
        buildNotify: function () {
            var content = this.settings.content;
            this.$ele = $(String.format(this.settings.template, this.settings.type, content.title, content.message, content.url, content.target));
            this.$ele.attr('data-notify-position', this.settings.placement.from + '-' + this.settings.placement.align);
            if (!this.settings.allow_dismiss) {
                this.$ele.find('[data-notify="dismiss"]').css('display', 'none');
            }
            if ((this.settings.delay <= 0 && !this.settings.showProgressbar) || !this.settings.showProgressbar) {
                this.$ele.find('[data-notify="progressbar"]').remove();
            }
        },
        setIcon: function () {
            if (this.settings.icon_type.toLowerCase() === 'class') {
                this.$ele.find('[data-notify="icon"]').addClass(this.settings.content.icon);
            } else {
                if (this.$ele.find('[data-notify="icon"]').is('img')) {
                    this.$ele.find('[data-notify="icon"]').attr('src', this.settings.content.icon);
                } else {
                    this.$ele.find('[data-notify="icon"]').append('<img src="' + this.settings.content.icon + '" alt="Notify Icon" />');
                }
            }
        },
        styleDismiss: function () {
            this.$ele.find('[data-notify="dismiss"]').css({
                position: 'absolute',
                right: '10px',
                top: '5px',
                zIndex: this.settings.z_index + 2
            });
        },
        styleURL: function () {
            this.$ele.find('[data-notify="url"]').css({
                backgroundImage: 'url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7)',
                height: '100%',
                left: 0,
                position: 'absolute',
                top: 0,
                width: '100%',
                zIndex: this.settings.z_index + 1
            });
        },
        placement: function () {
            var self = this,
                offsetAmt = this.settings.offset.y,
                css = {
                    display: 'inline-block',
                    margin: '0px auto',
                    position: this.settings.position ? this.settings.position : (this.settings.element === 'body' ? 'fixed' : 'absolute'),
                    transition: 'all .5s ease-in-out',
                    zIndex: this.settings.z_index
                },
                hasAnimation = false,
                settings = this.settings;

            $('[data-notify-position="' + this.settings.placement.from + '-' + this.settings.placement.align + '"]:not([data-closing="true"])').each(function () {
                offsetAmt = Math.max(offsetAmt, parseInt($(this).css(settings.placement.from)) + parseInt($(this).outerHeight()) + parseInt(settings.spacing));
            });
            if (this.settings.newest_on_top === true) {
                offsetAmt = this.settings.offset.y;
            }
            css[this.settings.placement.from] = offsetAmt + 'px';

            switch (this.settings.placement.align) {
                case "left":
                case "right":
                    css[this.settings.placement.align] = this.settings.offset.x + 'px';
                    break;
                case "center":
                    css.left = 0;
                    css.right = 0;
                    break;
            }
            this.$ele.css(css).addClass(this.settings.animate.enter);
            $.each(Array('webkit-', 'moz-', 'o-', 'ms-', ''), function (index, prefix) {
                self.$ele[0].style[prefix + 'AnimationIterationCount'] = 1;
            });

            $(this.settings.element).append(this.$ele);

            if (this.settings.newest_on_top === true) {
                offsetAmt = (parseInt(offsetAmt) + parseInt(this.settings.spacing)) + this.$ele.outerHeight();
                this.reposition(offsetAmt);
            }

            if ($.isFunction(self.settings.onShow)) {
                self.settings.onShow.call(this.$ele);
            }

            this.$ele.one(this.animations.start, function () {
                hasAnimation = true;
            }).one(this.animations.end, function () {
                self.$ele.removeClass(self.settings.animate.enter);
                if ($.isFunction(self.settings.onShown)) {
                    self.settings.onShown.call(this);
                }
            });

            setTimeout(function () {
                if (!hasAnimation) {
                    if ($.isFunction(self.settings.onShown)) {
                        self.settings.onShown.call(this);
                    }
                }
            }, 600);
        },
        bind: function () {
            var self = this;

            this.$ele.find('[data-notify="dismiss"]').on('click', function () {
                self.close();
            });

            if ($.isFunction(self.settings.onClick)) {
                this.$ele.on('click', function (event) {
                    if (event.target != self.$ele.find('[data-notify="dismiss"]')[0]) {
                        self.settings.onClick.call(this, event);
                    }
                });
            }

            this.$ele.mouseover(function () {
                $(this).data('data-hover', "true");
            }).mouseout(function () {
                $(this).data('data-hover', "false");
            });
            this.$ele.data('data-hover', "false");

            if (this.settings.delay > 0) {
                self.$ele.data('notify-delay', self.settings.delay);
                var timer = setInterval(function () {
                    var delay = parseInt(self.$ele.data('notify-delay')) - self.settings.timer;
                    if ((self.$ele.data('data-hover') === 'false' && self.settings.mouse_over === "pause") || self.settings.mouse_over != "pause") {
                        var percent = ((self.settings.delay - delay) / self.settings.delay) * 100;
                        self.$ele.data('notify-delay', delay);
                        self.$ele.find('[data-notify="progressbar"] > div').attr('aria-valuenow', percent).css('width', percent + '%');
                    }
                    if (delay <= -(self.settings.timer)) {
                        clearInterval(timer);
                        self.close();
                    }
                }, self.settings.timer);
            }
        },
        close: function () {
            var self = this,
                posX = parseInt(this.$ele.css(this.settings.placement.from)),
                hasAnimation = false;

            this.$ele.attr('data-closing', 'true').addClass(this.settings.animate.exit);
            self.reposition(posX);

            if ($.isFunction(self.settings.onClose)) {
                self.settings.onClose.call(this.$ele);
            }

            this.$ele.one(this.animations.start, function () {
                hasAnimation = true;
            }).one(this.animations.end, function () {
                $(this).remove();
                if ($.isFunction(self.settings.onClosed)) {
                    self.settings.onClosed.call(this);
                }
            });

            setTimeout(function () {
                if (!hasAnimation) {
                    self.$ele.remove();
                    if ($.isFunction(self.settings.onClosed)) {
                        self.settings.onClosed.call(this);
                    }
                }
            }, 600);
        },
        reposition: function (posX) {
            var self = this,
                notifies = '[data-notify-position="' + this.settings.placement.from + '-' + this.settings.placement.align + '"]:not([data-closing="true"])',
                $elements = this.$ele.nextAll(notifies);
            if (this.settings.newest_on_top === true) {
                $elements = this.$ele.prevAll(notifies);
            }
            $elements.each(function () {
                $(this).css(self.settings.placement.from, posX);
                posX = (parseInt(posX) + parseInt(self.settings.spacing)) + $(this).outerHeight();
            });
        }
    });

    $.notify = function (content, options) {
        var plugin = new Notify(this, content, options);
        return plugin.notify;
    };
    $.notifyDefaults = function (options) {
        defaults = $.extend(true, {}, defaults, options);
        return defaults;
    };

    $.notifyClose = function (selector) {

        if (typeof selector === "undefined" || selector === "all") {
            $('[data-notify]').find('[data-notify="dismiss"]').trigger('click');
        } else if (selector === 'success' || selector === 'info' || selector === 'warning' || selector === 'danger') {
            $('.alert-' + selector + '[data-notify]').find('[data-notify="dismiss"]').trigger('click');
        } else if (selector) {
            $(selector + '[data-notify]').find('[data-notify="dismiss"]').trigger('click');
        }
        else {
            $('[data-notify-position="' + selector + '"]').find('[data-notify="dismiss"]').trigger('click');
        }
    };

    $.notifyCloseExcept = function (selector) {

        if (selector === 'success' || selector === 'info' || selector === 'warning' || selector === 'danger') {
            $('[data-notify]').not('.alert-' + selector).find('[data-notify="dismiss"]').trigger('click');
        } else {
            $('[data-notify]').not(selector).find('[data-notify="dismiss"]').trigger('click');
        }
    };


}(jQuery));

// Jquery Validation
(function ($) {
    var Regjax = {
        email: '^[a-zA-Z0-9@. _]+$',
        int: '^[0-9*]+$',
        float: '^[0-9.]+$',
        str: '^[a-zA-Z*]+$',
        strint: '^[a-zA-Z0-9*]+$',
    }
    var RegjaxVal = {
        email: /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/,
        int: '^[0-9*]+$',
        str: '^[a-zA-Z*]+$',
        strint: '^[a-zA-Z0-9*]+$',
        ip: '/^[0-9]{2,3}(.){1}[0-9]{2,3}(.){1}[0-9]{2,3}(.){1}$/'
    }

    var elementEnum = ['input', 'select', 'textarea', 'button', ''];
    //elementEnum = elementEnum.join(':visible,').substr(0, elementEnum.length - 1);
    var bValidate = function (target, options) {
        this.option = options;
        this.$el = $(target);
        this.$el_ = this.$el.clone();
        //this.targetID = $('#' + options.targetGroup);

        this._ValidationInit();

    };

    bValidate.defaults = {
        ValidateType: "1",//To check all mandatory fields together
        AlertType: "span",//To display alert in modal or span tag
        modalName: "divModal",//If alert in modal popup means then modal name
        validateRuntimeEvents: ["blur", "input"],
        validateRuntimeAlert: true,
        RuntimeAlertType: "span",
        notifyOptions: {},
        RuntimeMessage: "Provide valid information",
        RuntimeMessageAppend: false,
        EventMessage: "",
        RuntimeClear: true,
    }

    bValidate.prototype._ValidationInit = function () {
        var that = this;
        this.$el.find('[data-mask]').each(function (index, element) {
            var strPlaceHolder = '';
            if ($(element).data('placeholder') != undefined && $(element).data('placeholder') != "") {
                $(element).mask($(element).data('mask'), { placeholder: $(element).data('placeholder') })
            }
            else {
                $(element).mask($(element).data('mask'))
            }
        });
        var elementsSelector = elementEnum.join(',');
        elementsSelector = elementsSelector.substr(0, elementsSelector.length - 1);
        this.$el.find(elementsSelector).each(function (index, element) {
            //if (($(element).data('type') != undefined && $(element).data('type') != "") || ($(element).data('groupname') != "" && $(element).data('groupname') != undefined && $(element).data('groupname') != null)) {            
            if ((element.nodeName == "INPUT" || element.nodeName == "SELECT") && element.type != "file") {
                that.option.validateRuntimeEvents.forEach(function (key) {
                    $(element).on(key, function (event) {
                        var objElement = that.ExtractValidationElement(event, this, that);
                        return that.ExecEvent(objElement);
                    });
                })
            }
            //}
        });

        //this.$el.find(".save:visible").each(function (index, element) {
        //    $(element).on("click", function () {
        //        return that.ExecValidateEvent(that.option);
        //    });
        //});


    }

    bValidate.prototype.getOptions = function () {
        return this.option;
    }

    bValidate.prototype.ExtractRegExp = function (element) {
        var str = '';
        if ($(element).data('type') != undefined && $(element).data('type') != "") {
            var splcharacter = '';
            if ($(element).data('character') != undefined && $(element).data('character') != "") {
                splcharacter = $(element).data('character');
            }
            str = Regjax[$(element).data('type')];
            if ($(element).data('type') != 'email')
                str = str.replace('*', splcharacter);
        }
        return new RegExp(str);
    }

    bValidate.prototype.ExtractRegExpValidation = function (element) {
        var str = '';
        if ($(element).data('type') != undefined && $(element).data('type') != "") {
            str = RegjaxVal[$(element).data('type')];
        }
        return new RegExp(str);
    }

    bValidate.prototype.PressKey = function (e) {
        if (e.keyCode == 8 || (e.keyCode == 46 && e.key == 'Delete') || e.keyCode == 9) {
            return true;
        }
        else {
            return false;
        }
    }

    bValidate.prototype.ValidateEvent = function (ObjElement) {
        var ElementValue = $(ObjElement.Element).val(this.PasteContent($(ObjElement.Element).val(), ObjElement.RuntimeRegExp)).val();
        if ((!this.PressKey(event)) && (ElementValue == '' || (ElementValue != '' && (ObjElement.RuntimeRegExp).test(ElementValue)))) {
            return true;
        }
        else {
            event.preventDefault();
            return false;
        }
    }

    bValidate.prototype.ExecEvent = function (ObjElement) {
        var that = this;
        this.ValidateEvent(ObjElement);

        if (ObjElement.event.type == "blur") {
            if ($(ObjElement.Element).data("groupname") != "" && $(ObjElement.Element).data("groupname") != undefined && $(ObjElement.Element).data("groupname") != null) {
                var bln = [true];
                if ($(ObjElement.Element).val() != "") {
                    this.$el.find("[data-groupname='" + $(ObjElement.Element).data("groupname") + "']").each(function () {
                        if ($(this).val() != $(ObjElement.Element).val() && $(this).val() != "") {
                            bln.push(false);
                        }
                    });
                }
                bln.forEach(function (val) {
                    if (!val) {
                        ObjElement.strMessage = that.option.RuntimeMessage;
                        that.ErrorMessage(ObjElement);
                        return false;
                    }
                });
            }
            if (this.ValidateLength(ObjElement)) {
                if (this.option.RuntimeMessageAppend) {
                    ObjElement.strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " with minimum " + ObjElement.charLength + " character.";
                }
                else {
                    ObjElement.strMessage = this.option.RuntimeMessage;
                }
                this.ErrorMessage(ObjElement);
                return false;
            }
        }

        if (this.option.validateRuntimeAlert) {
            if (ObjElement.Type == 'float' || ObjElement.Type == 'int') {
                this.ValidateNumeric(ObjElement);
            }
            else if (ObjElement.Type == 'email') {
                this.ValidateEmail(ObjElement);
            }
        }
    }

    bValidate.prototype.PasteContent = function (ElementValue, regExp) {
        var Content = '';
        for (var intLoop = 0; intLoop < ElementValue.length ; intLoop++) {
            if (regExp.test(ElementValue.charAt(intLoop))) {
                Content = Content + ElementValue.charAt(intLoop).toString();
            }
        }
        return Content;
    }

    bValidate.prototype.ExtractValidationElement = function (event, Element, that) {
        $(".spnError").remove();
        var ObjValidationElement = {};
        ObjValidationElement.event = event;
        ObjValidationElement.Element = Element;
        if ($(Element).data('range') != undefined && $(Element).data('range') != "") {
            if ($(Element).data('range').toString().indexOf(',') > -1) {
                ObjValidationElement.Range = { min: $(Element).data('range').split(',')[0], max: $(Element).data('range').split(',')[1] }
            }
            else {
                ObjValidationElement.Range = { min: $(Element).data('range'), max: undefined }
            }

            if (($(Element).data('range').toString().indexOf(',') > -1) && $(Element).data('range').split(',')[0].split('.').length > 1) {
                ObjValidationElement.DecimalPlace = $(Element).data('range').split(',')[0].split('.')[1].length;
            }
        }
        if ($(Element).attr('maxlength') != undefined && $(Element).attr('maxlength') != "") {
            ObjValidationElement.maxLength = $(Element).attr('maxlength');
        }
        if ($(Element).data('length') != undefined && $(Element).data('length') != "") {
            ObjValidationElement.charLength = $(Element).data('length');
        }
        ObjValidationElement.Type = $(Element).data('type');
        ObjValidationElement.RuntimeRegExp = this.ExtractRegExp(Element);
        ObjValidationElement.ExecutedRegExp = this.ExtractRegExpValidation(Element);
        ObjValidationElement.Keypressed = event.key;
        ObjValidationElement.strMessage = '';
        return ObjValidationElement;
    }

    bValidate.prototype.ValidateRange = function (ObjElement, FloatValue) {
        return ((ObjElement.Range != undefined) && (ObjElement.Range.min != undefined && parseFloat(FloatValue) >= parseFloat(ObjElement.Range.min)) && ((ObjElement.Range.max == undefined || ObjElement.Range.max == '') || (ObjElement.Range.max != undefined && ObjElement.Range.max != '' && parseFloat(FloatValue) <= parseFloat(ObjElement.Range.max))))
    }

    bValidate.prototype.ValidateNumeric = function (ObjElement) {
        var FloatValue = $(ObjElement.Element).val();
        if (FloatValue != "") {
            if (FloatValue.split('.').length > 2) {
                if (this.option.RuntimeMessageAppend) {
                    ObjElement.strMessage = (($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter valid " + $(ObjElement.Element).data('validate-name');
                }
                else {
                    ObjElement.strMessage = this.option.RuntimeMessage;
                }

                this.ErrorMessage(ObjElement);
                return false;
            }
            else if (ObjElement.Range != undefined && (!this.ValidateRange(ObjElement, FloatValue)) && (ObjElement.event.type == "blur")) {
                if (this.option.RuntimeMessageAppend) {
                    if (ObjElement.Range.max != undefined && ObjElement.Range.max != '')
                        ObjElement.strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " between " + ObjElement.Range.min + " to " + ObjElement.Range.max;
                    else
                        ObjElement.strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " above " + ObjElement.Range.min;
                }
                else {
                    ObjElement.strMessage = this.option.RuntimeMessage;
                }
                this.ErrorMessage(ObjElement);
                return false;
            }
            else if (FloatValue.split('.')[1] == '' && ObjElement.event.type == "blur") {
                if (this.option.RuntimeMessageAppend) {
                    ObjElement.strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " in " + ObjElement.DecimalPlace + " Decimal place";
                }
                else {
                    ObjElement.strMessage = this.option.RuntimeMessage;
                }
                this.ErrorMessage(ObjElement);
                return false;
            }
            else if (FloatValue.length > 1 && FloatValue.split('.').length > 1 && ObjElement.DecimalPlace != undefined && FloatValue.split('.')[1].length > ObjElement.DecimalPlace && ObjElement.event.type == "blur") {
                if (this.option.RuntimeMessageAppend) {
                    ObjElement.strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " in " + ObjElement.DecimalPlace + " Decimal place";
                }
                else {
                    ObjElement.strMessage = this.option.RuntimeMessage;
                }
                this.ErrorMessage(ObjElement);
                return false;
            }
            return true;
        }
    }

    bValidate.prototype.ValidateEmail = function (ObjElement) {
        var EmailIdvalue = $(ObjElement.Element).val();
        if (EmailIdvalue != "" && !(EmailIdvalue.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) && (ObjElement.event.type == "blur")) {
            if (this.option.RuntimeMessageAppend) {
                ObjElement.strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter valid " + $(ObjElement.Element).data('validate-name');
            }
            else {
                ObjElement.strMessage = this.option.RuntimeMessage;
            }
            this.ErrorMessage(ObjElement);
            return false;
        }
        return true;
    }

    bValidate.prototype.CheckNumeric = function (ObjElement) {

        var FloatValue = $(ObjElement.Element).val();
        var strMessage = '';
        if (FloatValue != "") {
            if (FloatValue.split('.').length > 2) {
                if (this.option.RuntimeMessageAppend) {
                    strMessage = (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter valid " + $(Element).data('validate-name');
                }
                else {
                    strMessage = this.option.RuntimeMessage;
                }
                return strMessage;
            }
            else if (ObjElement.Range != undefined && (!this.ValidateRange(ObjElement, FloatValue))) {
                if (this.option.RuntimeMessageAppend) {
                    if (ObjElement.Range.max != undefined && ObjElement.Range.max != '')
                        strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " between " + ObjElement.Range.min + " to " + ObjElement.Range.max;
                    else
                        strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " above " + ObjElement.Range.min;
                }
                else {
                    strMessage = this.option.RuntimeMessage;
                }
                return strMessage;
            }
            else if (FloatValue.split('.')[1] == '') {
                if (this.option.RuntimeMessageAppend) {
                    strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " in " + ObjElement.DecimalPlace + " Decimal place";
                }
                else {
                    strMessage = this.option.RuntimeMessage;
                }
                return strMessage;
            }
            else if (FloatValue.length > 1 && FloatValue.split('.').length > 1 && ObjElement.DecimalPlace != undefined && FloatValue.split('.')[1].length > ObjElement.DecimalPlace) {
                if (this.option.RuntimeMessageAppend) {
                    strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter the " + $(ObjElement.Element).data('validate-name') + " in " + ObjElement.DecimalPlace + " Decimal place";
                }
                else {
                    strMessage = this.option.RuntimeMessage;
                }
                return strMessage;
            }
        }
        return strMessage;
    }

    bValidate.prototype.CheckEmail = function (ObjElement) {
        var EmailIdvalue = $(ObjElement.Element).val();
        var strMessage = '';
        if (EmailIdvalue != "" && !(EmailIdvalue.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)) {
            if (this.option.RuntimeMessageAppend) {
                strMessage = ($(ObjElement.Element).data('validate-message') != "" && $(ObjElement.Element).data('validate-message') != undefined && $(ObjElement.Element).data('validate-message') != null) ? $(ObjElement.Element).data('validate-message') : (this.option.RuntimeMessage != "" && this.option.RuntimeMessage != undefined && this.option.RuntimeMessage != null) ? this.option.RuntimeMessage + ". " : this.option.RuntimeMessage + "Enter valid " + $(ObjElement.Element).data('validate-name');
            }
            else {
                strMessage = this.option.RuntimeMessage;
            }
        }
        return strMessage;
    }

    bValidate.prototype.ValidateLength = function (ObjElement) {
        var dataLength = $(ObjElement.Element).val();
        return ((ObjElement.charLength != undefined && parseInt(ObjElement.charLength) > dataLength.length && dataLength != ''))
    }

    bValidate.prototype.CallPopup = function (msg, focus, func) {
        var that = this;
        $('#' + this.option.modalName).bootstrapModal({
            header: "Message",
            body: msg,
            focusOnAfterClose: focus,
            buttons: {
                "Ok": function () {
                    $('#' + that.option.modalName).bootstrapModal("closeModal");
                    eval(func);
                }
            },
            buttonClass: {
                "Ok": "btn btn-blue"
            },
            classes: {
                "modal": "modal alert-modal"
            }
            //buttonsmodal: {
            //    "Ok": "modal",
            //},
        });
    }

    bValidate.prototype.ExecValidateEvent = function () {
        var ObjTarget = this.option;
        var ObjMessage = new Array();
        var that = this;
        var FocusId = '';
        var ObjElement = {};
        var bln = true;
        var elementsSelector = elementEnum.join(':visible,');
        elementsSelector = elementsSelector.substr(0, elementsSelector.length - 1);

        this.$el.find(elementsSelector).each(function (index, element) {
            if ($(element).hasClass("mandatory") && $(element).data("element-type") == "button-file") {
                if ($(element).siblings("[data-element-type='file']").data("filePath") == "" || $(element).siblings("[data-element-type='file']").data("filePath") == null || $(element).siblings("[data-element-type='file']").data("filePath") == undefined) {
                    ObjMessage.push("Upload the file");
                }
            }
            else if ($(element).hasClass("mandatory") && $(element).val() == "" && ((element.type.indexOf("text") > -1) || (element.type.indexOf("password") > -1))) {
                ObjMessage.push("Enter the " + $(element).data('validate-name'));
            }
            else if ($(element).hasClass("mandatory") && $(element).data("defaultval") != undefined && (element.type.indexOf("select") > -1) && $(element).val() == $(element).data('defaultval')) {
               
                ObjMessage.push("Select the " + $(element).data('validate-name'));
            }
            else if ($(element).hasClass("mandatory") && (element.type.indexOf("radio") > -1) && ($("[name=" + $(this).attr("name") + "]:checked").val() == undefined || $("[name=" + $(this).attr("name") + "]:checked").val() == "")) {
                ObjMessage.push("Select the " + $(element).data('validate-name'));
            }
            else if ($(element).hasClass("mandatory") && (element.type.indexOf("checkbox") > -1) && ($("[name=" + $(this).attr("name") + "]:checked").val() == undefined || $("[name=" + $(this).attr("name") + "]:checked").val() == "")) {
                ObjMessage.push("Select the " + $(element).data('validate-name'));
            }
            else if ($(element).data("type") == 'float' || ObjElement.Type == 'int') {
                ObjMessage.push(that.CheckNumeric(that.ExtractValidationElement('', element, that)));
            }
            else if ($(element).data("type") == 'email') {
                ObjMessage.push(that.CheckEmail(that.ExtractValidationElement('', element, that)));
            }
            else if (($(element).hasClass("autocomplete") || $(element).data("element-type") == "autoComplete") && !$(element).hasClass("get-only-text")) {
                var selectedVal = $(element).data($(element).data("value-field"));
                if ($(element).val() != "" && (selectedVal == undefined || selectedVal == null || selectedVal == "")) {
                    ObjMessage.push("Select " + $(element).data('validate-name') + " from smart text");
                }
            }
            else if ($(element).data("groupname") != undefined && $(element).data("groupname") != "" && $(element).data("groupname") != null) {
                var bbllnn = [true];
                that.$el.find("[data-groupname='" + $(element).data("groupname") + "']").each(function () {
                    if ($(this).val() != $(element).val() && $(this).val() != "") {
                        bbllnn.push(false);
                    }
                });
                for (var inti = 0; inti < bbllnn.length; inti++) {
                    vaal = bbllnn[inti];
                    if (!vaal) {
                        ObjMessage.push(that.option.RuntimeMessage);
                        break;
                    }
                }
            }
            if (ObjMessage[(ObjMessage.length - 1)] != undefined && ObjMessage[(ObjMessage.length - 1)] != "" && FocusId == '') {
                FocusId = element.id;
            }
            if (ObjTarget.ValidateType == "1" && ObjMessage[(ObjMessage.length - 1)] != undefined && ObjMessage[(ObjMessage.length - 1)] != "") {
                ObjElement = { Element: element, strMessage: ObjMessage[(ObjMessage.length - 1)] }
                that.ErrorMessage(ObjElement);
                bln = false;
                return false;
            }
            else if (ObjTarget.ValidateType == "2" && ((index + 1) == that.$el.find(elementsSelector).length)) {
                ObjElement = { Element: element, strMessage: ObjMessage.join("<br >") }
                that.ErrorMessage(ObjElement);
                bln = false;
                return false;
            }
        });
        return bln;
    }

    bValidate.prototype.ErrorMessage = function (ObjElement) {
        var that = this;
        var strfunction = '';
        //if (ObjElement.Type != undefined && ObjElement.Type != "") {
        ObjElement.strMessage = this.option.EventMessage ? this.option.EventMessage + '. ' + ObjElement.strMessage : ObjElement.strMessage;
        //}

        if (this.option.RuntimeClear) {
            strfunction = $(ObjElement.Element).val('');
        }

        if (this.option.RuntimeAlertType == "alert" || this.option.AlertType == "alert") {
            this.CallPopup(ObjElement.strMessage, ObjElement.Element.id, strfunction)
        }
        else if (this.option.RuntimeAlertType == "notify" || this.option.AlertType == "notify") {
            $.notifyClose("all");
            $.notify(ObjElement.strMessage, $.extend({}, {
                newest_on_top: true,
                allow_dismiss: true,
                timer: 2000,
                placement: {
                    from: "bottom",
                    align: "right"
                },
                animate: {
                    enter: 'animated bounceIn',
                    exit: 'animated bounceOut'
                },
                onShown: function () {
                    $(ObjElement.Element).focus();
                },
                type: 'danger',
                allow_duplicates: false,
                delay: 0,
            }, this.option.notifyOptions));
        }
        else if (this.option.RuntimeAlertType == "span" || this.option.AlertType == "span") {
            that.$el.find(".spnError").remove();
            $(ObjElement.Element).focus();
            $(ObjElement.Element).after("<span class='spnError alert-danger' style='text-align:justify'>" + ObjElement.strMessage + "</span>");
        }
    }

    $.fn.bnbValidate = function (option) {
        //var opt = $.extend({}, bValidate.defaults, options);
        //new bValidate(this, opt);

        var value,
           args = Array.prototype.slice.call(arguments, 1);
        $(this).each(function () {
            var $this = $(this),
            data = $this.data('bnb.validate'),
            options;

            if (typeof option === 'string') {
                //if ($.inArray(option, Element.allowedMethods) < 0)
                //    throw new Error("Unknown method: " + option);
                if (!data)
                    return;
                if (option === 'destroy') {
                    $this.removeData('bnb.validate');
                    return;
                }
                value = data[option].apply(data, args);
            }
            options = $.extend({}, bValidate.defaults, $this.data(), typeof option === 'object' && option);
            if (!data)
                $(this).data("bnb.validate", new bValidate(this, options));
        });
        return typeof value === 'undefined' ? this : value;
    }

}(jQuery))


// Calendar view start
"use strict";
Date.prototype.getWeek = function (iso8601) {
    if (iso8601) {
        var target = new Date(this.valueOf());
        var dayNr = (this.getDay() + 6) % 7;
        target.setDate(target.getDate() - dayNr + 3);
        var firstThursday = target.valueOf();
        target.setMonth(0, 1);
        if (target.getDay() != 4) {
            target.setMonth(0, 1 + ((4 - target.getDay()) + 7) % 7);
        }
        return 1 + Math.ceil((firstThursday - target) / 604800000); // 604800000 = 7 * 24 * 3600 * 1000
    } else {
        var onejan = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this.getTime() - onejan.getTime()) / 86400000) + onejan.getDay() + 1) / 7);
    }
};
Date.prototype.getMonthFormatted = function () {
    var month = this.getMonth() + 1;
    return month < 10 ? '0' + month : month;
};
Date.prototype.getDateFormatted = function () {
    var date = this.getDate();
    return date < 10 ? '0' + date : date;
};

if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined' ? args[number] : match;
        });
    };
}
if (!String.prototype.formatNum) {
    String.prototype.formatNum = function (decimal) {
        var r = "" + this;
        while (r.length < decimal)
            r = "0" + r;
        return r;
    };
}

(function ($) {

    var defaults = {
        // Container to append the tooltip
        tooltip_container: 'body',
        // Width of the calendar
        width: '100%',
        // Initial view (can be 'month', 'week', 'day')
        view: 'month',
        // Initial date. No matter month, week or day this will be a starting point. Can be 'now' or a date in format 'yyyy-mm-dd'
        day: 'now',
        // Day Start time and end time with time intervals. Time split 10, 15 or 30.
        time_start: '06:00',
        time_end: '22:00',
        time_split: '30',
        // Source of events data. It can be one of the following:
        // - URL to return JSON list of events in special format.
        //   {success:1, result: [....]} or for error {success:0, error:'Something terrible happened'}
        //   events: [...] as described in events property description
        //   The start and end variables will be sent to this url
        // - A function that received the start and end date, and that
        //   returns an array of events (as described in events property description)
        // - An array containing the events
        events_source: '',
        // Static cache of events. If set to true, events will only be loaded once.
        // Useful if response is not constrained by date.
        events_cache: false,
        // Set format12 to true if you want to use 12 Hour format instead of 24 Hour
        format12: false,
        am_suffix: "AM",
        pm_suffix: "PM",
        // Path to templates should end with slash /. It can be as relative
        // /component/bootstrap-calendar/tmpls/
        // or absolute
        // http://localhost/component/bootstrap-calendar/tmpls/
        tmpl_path: 'tmpls/',
        tmpl_cache: true,
        classes: {
            months: {
                inmonth: 'cal-day-inmonth',
                outmonth: 'cal-day-outmonth',
                saturday: 'cal-day-weekend',
                sunday: 'cal-day-weekend',
                holidays: 'cal-day-holiday',
                today: 'cal-day-today'
            },
            week: {
                workday: 'cal-day-workday',
                saturday: 'cal-day-weekend',
                sunday: 'cal-day-weekend',
                holidays: 'cal-day-holiday',
                today: 'cal-day-today'
            }
        },
        // ID of the element of modal window. If set, events URLs will be opened in modal windows.
        modal: null,
        //	modal handling setting, one of "iframe", "ajax" or "template"
        modal_type: "iframe",
        //	function to set modal title, will be passed the event as a parameter
        modal_title: null,
        views: {
            year: {
                slide_events: 1,
                enable: 1
            },
            month: {
                slide_events: 1,
                enable: 1
            },
            week: {
                enable: 1
            },
            day: {
                enable: 1
            }
        },
        merge_holidays: false,
        display_week_numbers: true,
        weekbox: true,
        //shows events which fits between time_start and time_end
        show_events_which_fits_time: false,
        // Headers defined for ajax call
        headers: {},

        // ------------------------------------------------------------
        // CALLBACKS. Events triggered by calendar class. You can use
        // those to affect you UI
        // ------------------------------------------------------------
        onAfterEventsLoad: function (events) {
            // Inside this function 'this' is the calendar instance
        },
        onBeforeEventsLoad: function (next) {
            // Inside this function 'this' is the calendar instance
            next();
        },
        onAfterViewLoad: function (view) {
            // Inside this function 'this' is the calendar instance
        },
        onAfterModalShown: function (events) {
            // Inside this function 'this' is the calendar instance
        },
        onAfterModalHidden: function (events) {
            // Inside this function 'this' is the calendar instance
        },
        // -------------------------------------------------------------
        // INTERNAL USE ONLY. DO NOT ASSIGN IT WILL BE OVERRIDDEN ANYWAY
        // -------------------------------------------------------------
        events: [],
        templates: {
            year: '',
            month: '',
            week: '',
            day: ''
        },
        stop_cycling: false,

        //thamu
        data: [],
        bootstrapColumns: [],
        _eventsObj: {
            data: [],
            columns: [],

            startDateColumn: {
                column: undefined,
                format: 103
            },
            endDateColumn: {
                column: undefined,
                format: 103
            },
            EventColor: {
                column: undefined,
                value: [],
                color: []
            },
            urlColumn: "",
            idColumn: "",
            defaultView: "month",
            templatePath: 'tmpls/',

            enable: ["next", "today", "prev", "year", "month", "week", "day"],
            nextText: "Next",
            todayText: "Today",
            prevText: "Previous",
            yearText: "Year",
            monthText: "Month",
            weekText: "Week",
            dayText: "Day",

            nextClass: "btn btn-primary",
            todayClass: "btn btn-default",
            prevClass: "btn btn-primary",
            yearClass: "btn btn-warning",
            monthClass: "btn btn-warning",
            weekClass: "btn btn-warning",
            dayClass: "btn btn-warning",

            titleAlign: "left",
            buttonAlign: "right",

            onprevClick: function () { },
            ontodayClick: function () { },
            onnextClick: function () { },

            onyearClick: function () { },
            onmonthClick: function () { },
            onweekClick: function () { },
            ondayClick: function () { },
        }
    };

    var defaults_extended = {
        first_day: 2,
        week_numbers_iso_8601: false,
        holidays: {
            // January 1
            '01-01': "New Year's Day",
            // Third (+3*) Monday (1) in January (01)
            '01+3*1': "Birthday of Dr. Martin Luther King, Jr.",
            // Third (+3*) Monday (1) in February (02)
            '02+3*1': "Washington's Birthday",
            // Last (-1*) Monday (1) in May (05)
            '05-1*1': "Memorial Day",
            // July 4
            '04-07': "Independence Day",
            // First (+1*) Monday (1) in September (09)
            '09+1*1': "Labor Day",
            // Second (+2*) Monday (1) in October (10)
            '10+2*1': "Columbus Day",
            // November 11
            '11-11': "Veterans Day",
            // Fourth (+4*) Thursday (4) in November (11)
            '11+4*4': "Thanksgiving Day",
            // December 25
            '25-12': "Christmas"
        }
    };

    var strings = {
        error_noview: 'Calendar: View {0} not found',
        error_dateformat: 'Calendar: Wrong date format {0}. Should be either "now" or "yyyy-mm-dd"',
        error_loadurl: 'Calendar: Event URL is not set',
        error_where: 'Calendar: Wrong navigation direction {0}. Can be only "next" or "prev" or "today"',
        error_timedevide: 'Calendar: Time split parameter should divide 60 without decimals. Something like 10, 15, 30',

        no_events_in_day: 'No events in this day.',

        title_year: '{0}',
        title_month: '{0} {1}',
        title_week: 'week {0} of {1}',
        title_day: '{0} {1} {2}, {3}',

        week: 'Week {0}',
        all_day: 'All day',
        time: 'Time',
        events: 'Events',
        before_time: 'Ends before timeline',
        after_time: 'Starts after timeline',

        m0: 'January',
        m1: 'February',
        m2: 'March',
        m3: 'April',
        m4: 'May',
        m5: 'June',
        m6: 'July',
        m7: 'August',
        m8: 'September',
        m9: 'October',
        m10: 'November',
        m11: 'December',

        ms0: 'Jan',
        ms1: 'Feb',
        ms2: 'Mar',
        ms3: 'Apr',
        ms4: 'May',
        ms5: 'Jun',
        ms6: 'Jul',
        ms7: 'Aug',
        ms8: 'Sep',
        ms9: 'Oct',
        ms10: 'Nov',
        ms11: 'Dec',

        d0: 'Sunday',
        d1: 'Monday',
        d2: 'Tuesday',
        d3: 'Wednesday',
        d4: 'Thursday',
        d5: 'Friday',
        d6: 'Saturday'
    };

    var browser_timezone = '';
    try {
        if ($.type(window.jstz) == 'object' && $.type(jstz.determine) == 'function') {
            browser_timezone = jstz.determine().name();
            if ($.type(browser_timezone) !== 'string') {
                browser_timezone = '';
            }
        }
    }
    catch (e) {
    }

    function buildEventsUrl(events_url, data) {
        var separator, key, url;
        url = events_url;
        separator = (events_url.indexOf('?') < 0) ? '?' : '&';
        for (key in data) {
            url += separator + key + '=' + encodeURIComponent(data[key]);
            separator = '&';
        }
        return url;
    }

    function getExtentedOption(cal, option_name) {
        var fromOptions = (cal.options[option_name] != null) ? cal.options[option_name] : null;
        var fromLanguage = (cal.locale[option_name] != null) ? cal.locale[option_name] : null;
        if ((option_name == 'holidays') && cal.options.merge_holidays) {
            var holidays = {};
            $.extend(true, holidays, fromLanguage ? fromLanguage : defaults_extended.holidays);
            if (fromOptions) {
                $.extend(true, holidays, fromOptions);
            }
            return holidays;
        }
        else {
            if (fromOptions != null) {
                return fromOptions;
            }
            if (fromLanguage != null) {
                return fromLanguage;
            }
            return defaults_extended[option_name];
        }
    }

    function getHolidays(cal, year) {
        var hash = [];
        var holidays_def = getExtentedOption(cal, 'holidays');
        for (var k in holidays_def) {
            hash.push(k + ':' + holidays_def[k]);
        }
        hash.push(year);
        hash = hash.join('|');
        if (hash in getHolidays.cache) {
            return getHolidays.cache[hash];
        }
        var holidays = [];
        $.each(holidays_def, function (key, name) {
            var firstDay = null, lastDay = null, failed = false;
            $.each(key.split('>'), function (i, chunk) {
                var m, date = null;
                if (m = /^(\d\d)-(\d\d)$/.exec(chunk)) {
                    date = new Date(year, parseInt(m[2], 10) - 1, parseInt(m[1], 10));
                }
                else if (m = /^(\d\d)-(\d\d)-(\d\d\d\d)$/.exec(chunk)) {
                    if (parseInt(m[3], 10) == year) {
                        date = new Date(year, parseInt(m[2], 10) - 1, parseInt(m[1], 10));
                    }
                }
                else if (m = /^easter(([+\-])(\d+))?$/.exec(chunk)) {
                    date = getEasterDate(year, m[1] ? parseInt(m[1], 10) : 0);
                }
                else if (m = /^(\d\d)([+\-])([1-5])\*([0-6])$/.exec(chunk)) {
                    var month = parseInt(m[1], 10) - 1;
                    var direction = m[2];
                    var offset = parseInt(m[3]);
                    var weekday = parseInt(m[4]);
                    switch (direction) {
                        case '+':
                            var d = new Date(year, month, 1 - 7);
                            while (d.getDay() != weekday) {
                                d = new Date(d.getFullYear(), d.getMonth(), d.getDate() + 1);
                            }
                            date = new Date(d.getFullYear(), d.getMonth(), d.getDate() + 7 * offset);
                            break;
                        case '-':
                            var d = new Date(year, month + 1, 0 + 7);
                            while (d.getDay() != weekday) {
                                d = new Date(d.getFullYear(), d.getMonth(), d.getDate() - 1);
                            }
                            date = new Date(d.getFullYear(), d.getMonth(), d.getDate() - 7 * offset);
                            break;
                    }
                }
                if (!date) {
                    warn('Unknown holiday: ' + key);
                    failed = true;
                    return false;
                }
                switch (i) {
                    case 0:
                        firstDay = date;
                        break;
                    case 1:
                        if (date.getTime() <= firstDay.getTime()) {
                            warn('Unknown holiday: ' + key);
                            failed = true;
                            return false;
                        }
                        lastDay = date;
                        break;
                    default:
                        warn('Unknown holiday: ' + key);
                        failed = true;
                        return false;
                }
            });
            if (!failed) {
                var days = [];
                if (lastDay) {
                    for (var date = new Date(firstDay.getTime()) ; date.getTime() <= lastDay.getTime() ; date.setDate(date.getDate() + 1)) {
                        days.push(new Date(date.getTime()));
                    }
                }
                else {
                    days.push(firstDay);
                }
                holidays.push({ name: name, days: days });
            }
        });
        getHolidays.cache[hash] = holidays;
        return getHolidays.cache[hash];
    }

    getHolidays.cache = {};

    function warn(message) {
        if ($.type(window.console) == 'object' && $.type(window.console.warn) == 'function') {
            window.console.warn('[Bootstrap-Calendar] ' + message);
        }
    }

    function Calendar(params, context) {
        this.options = $.extend(true, { position: { start: new Date(), end: new Date() } }, defaults, params);
        this.setLanguage(this.options.language);
        this.context = context;

        context.css('width', this.options.width).addClass('cal-context');

        this.view();
        return this;
    }

    Calendar.prototype.setOptions = function (object) {
        $.extend(this.options, object);
        if ('language' in object) {
            this.setLanguage(object.language);
        }
        if ('modal' in object) {
            this._update_modal();
        }
    }

    Calendar.prototype.setLanguage = function (lang) {
        if (window.calendar_languages && (lang in window.calendar_languages)) {
            this.locale = $.extend(true, {}, strings, calendar_languages[lang]);
            this.options.language = lang;
        } else {
            this.locale = strings;
            delete this.options.language;
        }
    }

    Calendar.prototype._render = function () {
        this.context.html('');
        this._loadTemplate(this.options.view);
        this.stop_cycling = false;

        var data = {};
        data.cal = this;
        data.day = 1;
        //data.data = this.options.data;
        //data.columns = this.options.columns;

        // Getting list of days in a week in correct order. Works for month and week views
        if (getExtentedOption(this, 'first_day') == 1) {
            data.days_name = [this.locale.d1, this.locale.d2, this.locale.d3, this.locale.d4, this.locale.d5, this.locale.d6, this.locale.d0]
        } else {
            data.days_name = [this.locale.d0, this.locale.d1, this.locale.d2, this.locale.d3, this.locale.d4, this.locale.d5, this.locale.d6]
        }

        // Get all events between start and end
        var start = parseInt(this.options.position.start.getTime());
        var end = parseInt(this.options.position.end.getTime());

        data.events = this.getEventsBetween(start, end);

        switch (this.options.view) {
            case 'month':
                break;
            case 'week':
                this._calculate_hour_minutes(data);
                break;
            case 'day':
                this._calculate_hour_minutes(data);
                break;
        }

        data.start = new Date(this.options.position.start.getTime());
        //thamu
        var dd = new Date(this.options.position.start);
        data.end = new Date(dd.setDate(dd.getDate() + 1));
        //thamu
        data.lang = this.locale;

        this.context.append(this.options.templates[this.options.view](data));
        this._update();
    };

    Calendar.prototype._format_hour = function (str_hour) {
        var hour_split = str_hour.split(":");
        var hour = parseInt(hour_split[0]);
        var minutes = parseInt(hour_split[1]);

        var suffix = '';

        if (this.options.format12) {
            if (hour < 12) {
                suffix = this.options.am_suffix;
            }
            else {
                suffix = this.options.pm_suffix;
            }

            hour = hour % 12;
            if (hour == 0) {
                hour = 12;
            }
        }

        return hour.toString().formatNum(2) + ':' + minutes.toString().formatNum(2) + suffix;
    };

    Calendar.prototype._format_time = function (datetime) {
        return this._format_hour(datetime.getHours() + ':' + datetime.getMinutes());
    };

    Calendar.prototype._calculate_hour_minutes = function (data) {
        var $self = this;
        var time_split = parseInt(this.options.time_split);
        var time_split_count = 60 / time_split;
        var time_split_hour = Math.min(time_split_count, 1);

        if (((time_split_count >= 1) && (time_split_count % 1 != 0)) || ((time_split_count < 1) && (1440 / time_split % 1 != 0))) {
            $.error(this.locale.error_timedevide);
        }

        var time_start = this.options.time_start.split(":");
        var time_end = this.options.time_end.split(":");

        data.hours = (parseInt(time_end[0]) - parseInt(time_start[0])) * time_split_hour;
        var lines = data.hours * time_split_count - parseInt(time_start[1]) / time_split;
        var ms_per_line = (60000 * time_split);

        var start = new Date(this.options.position.start.getTime());
        start.setHours(time_start[0]);
        start.setMinutes(time_start[1]);
        var end = new Date(this.options.position.end.getTime() - (86400000));
        end.setHours(time_end[0]);
        end.setMinutes(time_end[1]);

        data.all_day = [];
        data.by_hour = [];
        data.after_time = [];
        data.before_time = [];
        $.each(data.events, function (k, e) {
            var s = new Date(parseInt(e.start));
            var f = new Date(parseInt(e.end));

            e.start_hour = $self._format_time(s);
            e.end_hour = $self._format_time(f);

            if (e.start < start.getTime()) {
                warn(1);
                e.start_hour = s.getDate() + ' ' + $self.locale['ms' + s.getMonth()] + ' ' + e.start_hour;
            }

            if (e.end > end.getTime()) {
                warn(1);
                e.end_hour = f.getDate() + ' ' + $self.locale['ms' + f.getMonth()] + ' ' + e.end_hour;
            }

            if (!$self.options.show_events_which_fits_time) {
                if (e.start <= start.getTime() && e.end >= end.getTime()) {
                    data.all_day.push(e);
                    return;
                }

                if (e.end < start.getTime()) {
                    data.before_time.push(e);
                    return;
                }

                if (e.start > end.getTime()) {
                    data.after_time.push(e);
                    return;
                }
            } else {
                if (e.start < start.getTime()) {
                    data.before_time.push(e);
                    return;
                }

                if (e.end > end.getTime()) {
                    data.after_time.push(e);
                    return;
                }

                if (e.start < start.getTime() && e.end < end.getTime()) {
                    data.all_day.push(e);
                    return;
                }
            }

            var event_start = start.getTime() - e.start;

            if (event_start >= 0) {
                e.top = 0;
            } else {
                e.top = Math.abs(event_start) / ms_per_line;
            }

            var lines_left = Math.abs(lines - e.top);
            var lines_in_event = (e.end - e.start) / ms_per_line;
            if (event_start >= 0) {
                lines_in_event = (e.end - start.getTime()) / ms_per_line;
            }

            e.lines = lines_in_event;
            if (lines_in_event > lines_left) {
                e.lines = lines_left;
            }

            data.by_hour.push(e);
        });
    };

    Calendar.prototype._hour_min = function (hour) {
        var time_start = this.options.time_start.split(":");
        var time_split = parseInt(this.options.time_split);
        var in_hour = 60 / time_split;
        return (hour == 0) ? (in_hour - (parseInt(time_start[1]) / time_split)) : in_hour;
    };

    Calendar.prototype._hour = function (hour, part) {
        var time_start = this.options.time_start.split(":");
        var time_split = parseInt(this.options.time_split);
        var h = "" + (parseInt(time_start[0]) + hour * Math.max(time_split / 60, 1));
        var m = "" + time_split * part;

        return this._format_hour(h.formatNum(2) + ":" + m.formatNum(2));
    };

    Calendar.prototype._week = function (event) {
        this._loadTemplate('week-days');
        var t = {};
        var start = parseInt(this.options.position.start.getTime());
        var end = parseInt(this.options.position.end.getTime());
        var events = [];
        var self = this;
        var first_day = getExtentedOption(this, 'first_day');

        $.each(this.getEventsBetween(start, end), function (k, event) {
            var eventStart = new Date(parseInt(event.start));
            eventStart.setHours(0, 0, 0, 0);
            var eventEnd = new Date(parseInt(event.end));

            event.start_day = new Date(parseInt(eventStart.getTime())).getDay();
            if (first_day == 1) {
                event.start_day = (event.start_day + 6) % 7;
            }
            if ((eventEnd.getTime() - eventStart.getTime()) <= 86400000) {
                event.days = 1;
            } else {
                event.days = ((eventEnd.getTime() - eventStart.getTime()) / 86400000);
            }

            if (eventStart.getTime() < start) {

                event.days = event.days - ((start - eventStart.getTime()) / 86400000);
                event.start_day = 0;
            }

            event.days = Math.ceil(event.days);

            if (event.start_day + event.days > 7) {
                event.days = 7 - (event.start_day);
            }

            events.push(event);
        });
        t.events = events;
        t.cal = this;
        return self.options.templates['week-days'](t);
    }

    Calendar.prototype._month = function (month) {
        this._loadTemplate('year-month');

        var t = { cal: this };
        var newmonth = month + 1;
        t.data_day = this.options.position.start.getFullYear() + '-' + (newmonth < 10 ? '0' + newmonth : newmonth) + '-' + '01';
        t.month_name = this.locale['m' + month];

        var curdate = new Date(this.options.position.start.getFullYear(), month, 1, 0, 0, 0);
        t.start = parseInt(curdate.getTime());
        t.end = parseInt(new Date(this.options.position.start.getFullYear(), month + 1, 1, 0, 0, 0).getTime());
        t.events = this.getEventsBetween(t.start, t.end);
        return this.options.templates['year-month'](t);
    }

    Calendar.prototype._day = function (week, day) {
        this._loadTemplate('month-day');

        var t = { tooltip: '', cal: this };
        var cls = this.options.classes.months.outmonth;

        var firstday = this.options.position.start.getDay();
        if (getExtentedOption(this, 'first_day') == 2) {
            firstday++;
        } else {
            firstday = (firstday == 0 ? 7 : firstday);
        }

        day = (day - firstday) + 1;
        var curdate = new Date(this.options.position.start.getFullYear(), this.options.position.start.getMonth(), day, 0, 0, 0);

        // if day of the current month
        if (day > 0) {
            cls = this.options.classes.months.inmonth;
        }
        // stop cycling table rows;
        var daysinmonth = (new Date(this.options.position.end.getTime() - 1)).getDate();
        if ((day + 1) > daysinmonth) {
            this.stop_cycling = true;
        }
        // if day of the next month
        if (day > daysinmonth) {
            day = day - daysinmonth;
            cls = this.options.classes.months.outmonth;
        }

        cls = $.trim(cls + " " + this._getDayClass("months", curdate));

        if (day <= 0) {
            var daysinprevmonth = (new Date(this.options.position.start.getFullYear(), this.options.position.start.getMonth(), 0)).getDate();
            day = daysinprevmonth - Math.abs(day);
            cls += ' cal-month-first-row';
        }

        var holiday = this._getHoliday(curdate);
        if (holiday !== false) {
            t.tooltip = holiday;
        }

        t.data_day = curdate.getFullYear() + '-' + curdate.getMonthFormatted() + '-' + (day < 10 ? '0' + day : day);
        t.cls = cls;
        t.day = day;

        t.start = parseInt(curdate.getTime());
        t.end = parseInt(t.start + 86400000);
        t.events = this.getEventsBetween(t.start, t.end);
        return this.options.templates['month-day'](t);
    }


    Calendar.prototype._getHoliday = function (date) {
        var result = false;
        $.each(getHolidays(this, date.getFullYear()), function () {
            var found = false;
            $.each(this.days, function () {
                if (this.toDateString() == date.toDateString()) {
                    found = true;
                    return false;
                }
            });
            if (found) {
                result = this.name;
                return false;
            }
        });
        return result;
    };

    Calendar.prototype._getHolidayName = function (date) {
        var holiday = this._getHoliday(date);
        return (holiday === false) ? "" : holiday;
    };

    Calendar.prototype._getDayClass = function (class_group, date) {
        var self = this;
        var addClass = function (which, to) {
            var cls;
            cls = (self.options.classes && (class_group in self.options.classes) && (which in self.options.classes[class_group])) ? self.options.classes[class_group][which] : "";
            if ((typeof (cls) == "string") && cls.length) {
                to.push(cls);
            }
        };
        var classes = [];
        if (date.toDateString() == (new Date()).toDateString()) {
            addClass("today", classes);
        }
        var holiday = this._getHoliday(date);
        if (holiday !== false) {
            addClass("holidays", classes);
        }
        switch (date.getDay()) {
            case 0:
                addClass("sunday", classes);
                break;
            case 6:
                addClass("saturday", classes);
                break;
        }

        addClass(date.toDateString(), classes);

        return classes.join(" ");
    };

    Calendar.prototype.view = function (view) {
        if (view) {
            if (!this.options.views[view].enable) {
                return;
            }
            this.options.view = view;
        }

        this._init_position();
        this._loadEvents();
        this._render();

        this.options.onAfterViewLoad.call(this, this.options.view);
    };

    Calendar.prototype.navigate = function (where, next) {
        var to = $.extend({}, this.options.position);
        if (where == 'next') {
            switch (this.options.view) {
                case 'year':
                    to.start.setFullYear(this.options.position.start.getFullYear() + 1);
                    break;
                case 'month':
                    to.start.setMonth(this.options.position.start.getMonth() + 1);
                    break;
                case 'week':
                    to.start.setDate(this.options.position.start.getDate() + 7);
                    break;
                case 'day':
                    to.start.setDate(this.options.position.start.getDate() + 1);
                    break;
            }
        } else if (where == 'prev') {
            switch (this.options.view) {
                case 'year':
                    to.start.setFullYear(this.options.position.start.getFullYear() - 1);
                    break;
                case 'month':
                    to.start.setMonth(this.options.position.start.getMonth() - 1);
                    break;
                case 'week':
                    to.start.setDate(this.options.position.start.getDate() - 7);
                    break;
                case 'day':
                    to.start.setDate(this.options.position.start.getDate() - 1);
                    break;
            }
        } else if (where == 'today') {
            to.start.setTime(new Date().getTime());
        }
        else {
            $.error(this.locale.error_where.format(where))
        }
        this.options.day = to.start.getFullYear() + '-' + to.start.getMonthFormatted() + '-' + to.start.getDateFormatted();
        this.view();
        if (_.isFunction(next)) {
            next();
        }
    };

    Calendar.prototype._init_position = function () {
        var year, month, day;

        if (this.options.day == 'now') {
            var date = new Date();
            year = date.getFullYear();
            month = date.getMonth();
            day = date.getDate();
        } else if (this.options.day.match(/^\d{4}-\d{2}-\d{2}$/g)) {
            var list = this.options.day.split('-');
            year = parseInt(list[0], 10);
            month = parseInt(list[1], 10) - 1;
            day = parseInt(list[2], 10);
        }
        else {
            $.error(this.locale.error_dateformat.format(this.options.day));
        }

        switch (this.options.view) {
            case 'year':
                this.options.position.start.setTime(new Date(year, 0, 1).getTime());
                this.options.position.end.setTime(new Date(year + 1, 0, 1).getTime());
                break;
            case 'month':
                this.options.position.start.setTime(new Date(year, month, 1).getTime());
                this.options.position.end.setTime(new Date(year, month + 1, 1).getTime());
                break;
            case 'day':
                this.options.position.start.setTime(new Date(year, month, day).getTime());
                this.options.position.end.setTime(new Date(year, month, day + 1).getTime());
                break;
            case 'week':
                var curr = new Date(year, month, day);
                var first;
                if (getExtentedOption(this, 'first_day') == 1) {
                    first = curr.getDate() - ((curr.getDay() + 6) % 7);
                }
                else {
                    first = curr.getDate() - curr.getDay();
                }
                this.options.position.start.setTime(new Date(year, month, first).getTime());
                this.options.position.end.setTime(new Date(year, month, first + 7).getTime());
                break;
            default:
                $.error(this.locale.error_noview.format(this.options.view))
        }
        return this;
    };

    Calendar.prototype.getTitle = function () {
        var p = this.options.position.start;
        switch (this.options.view) {
            case 'year':
                return this.locale.title_year.format(p.getFullYear());
                break;
            case 'month':
                return this.locale.title_month.format(this.locale['m' + p.getMonth()], p.getFullYear());
                break;
            case 'week':
                return this.locale.title_week.format(p.getWeek(getExtentedOption(this, 'week_numbers_iso_8601')), p.getFullYear());
                break;
            case 'day':
                return this.locale.title_day.format(this.locale['d' + p.getDay()], p.getDate(), this.locale['m' + p.getMonth()], p.getFullYear());
                break;
        }
        return;
    };

    Calendar.prototype.getYear = function () {
        var p = this.options.position.start;
        return p.getFullYear();
    };

    Calendar.prototype.getMonth = function () {
        var p = this.options.position.start;
        return this.locale['m' + p.getMonth()];
    };

    Calendar.prototype.getDay = function () {
        var p = this.options.position.start;
        return this.locale['d' + p.getDay()];
    };

    Calendar.prototype.isToday = function () {
        var now = new Date().getTime();

        return ((now > this.options.position.start) && (now < this.options.position.end));
    }

    Calendar.prototype.getStartDate = function () {
        return this.options.position.start;
    }

    Calendar.prototype.getEndDate = function () {
        return this.options.position.end;
    }

    Calendar.prototype._loadEvents = function () {
        var self = this;
        var source = null;
        if ('events_source' in this.options && this.options.events_source !== '') {
            source = this.options.events_source;
        }
        else if ('events_url' in this.options) {
            source = this.options.events_url;
            warn('The events_url option is DEPRECATED and it will be REMOVED in near future. Please use events_source instead.');
        }
        var loader;
        switch ($.type(source)) {
            case 'function':
                loader = function () {
                    return source(self.options.position.start, self.options.position.end, browser_timezone);
                };
                break;
            case 'array':
                loader = function () {
                    return [].concat(source);
                };
                break;
            case 'string':
                if (source.length) {
                    loader = function () {
                        var events = [];
                        var d_from = self.options.position.start;
                        var d_to = self.options.position.end;
                        var params = { from: d_from.getTime(), to: d_to.getTime(), utc_offset_from: d_from.getTimezoneOffset(), utc_offset_to: d_to.getTimezoneOffset() };

                        if (browser_timezone.length) {
                            params.browser_timezone = browser_timezone;
                        }
                        $.ajax({
                            url: buildEventsUrl(source, params),
                            dataType: 'json',
                            type: 'GET',
                            async: false,
                            headers: self.options.headers,
                        }).done(function (json) {
                            if (!json.success) {
                                $.error(json.error);
                            }
                            if (json.result) {
                                events = json.result;
                            }
                        });
                        return events;
                    };
                }
                break;
        }
        if (!loader) {
            $.error(this.locale.error_loadurl);
        }
        this.options.onBeforeEventsLoad.call(this, function () {
            if (!self.options.events.length || !self.options.events_cache) {
                self.options.events = loader();
                self.options.events.sort(function (a, b) {
                    var delta;
                    delta = a.start - b.start;
                    if (delta == 0) {
                        delta = a.end - b.end;
                    }
                    return delta;
                });
            }
            self.options.onAfterEventsLoad.call(self, self.options.events);
        });
    };

    Calendar.prototype._templatePath = function (name) {
        if (typeof this.options.tmpl_path == 'function') {
            return this.options.tmpl_path(name)
        }
        else {
            return this.options.tmpl_path + name + '.html';
        }
    };

    Calendar.prototype._loadTemplate = function (name) {
        if (this.options.templates[name]) {
            return;
        }
        var self = this;
        $.ajax({
            url: self._templatePath(name),
            dataType: 'html',
            type: 'GET',
            async: false,
            cache: this.options.tmpl_cache
        }).done(function (html) {
            self.options.templates[name] = _.template(html);
        });
    };

    Calendar.prototype._update = function () {
        var self = this;

        $('*[data-toggle="tooltip"]').tooltip({ container: this.options.tooltip_container });

        $('*[data-cal-date]').click(function () {
            var view = $(this).data('cal-view');
            self.options.day = $(this).data('cal-date');
            self.view(view);
        });
        $('.cal-cell').dblclick(function () {
            var view = $('[data-cal-date]', this).data('cal-view');
            self.options.day = $('[data-cal-date]', this).data('cal-date');
            self.view(view);
        });

        this['_update_' + this.options.view]();

        this._update_modal();

    };

    Calendar.prototype._update_modal = function () {
        var self = this;

        $('a[data-event-id]', this.context).unbind('click');

        if (!self.options.modal) {
            return;
        }

        var modal = $(self.options.modal);

        if (!modal.length) {
            return;
        }

        var ifrm = null;
        if (self.options.modal_type == "iframe") {
            ifrm = $(document.createElement("iframe"))
                .attr({
                    width: "100%",
                    frameborder: "0"
                });
        }

        $('a[data-event-id]', this.context).on('click', function (event) {
            event.preventDefault();
            event.stopPropagation();

            var url = $(this).attr('href');
            var id = $(this).data("event-id");
            var event = _.find(self.options.events, function (event) {
                return event.id == id
            });

            if (self.options.modal_type == "iframe") {
                ifrm.attr('src', url);
                $('.modal-body', modal).html(ifrm);
            }

            if (!modal.data('handled.bootstrap-calendar') || (modal.data('handled.bootstrap-calendar') && modal.data('handled.event-id') != event.id)) {
                modal.off('show.bs.modal')
                    .off('shown.bs.modal')
                    .off('hidden.bs.modal')
                    .on('show.bs.modal', function () {
                        var modal_body = $(this).find('.modal-body');
                        switch (self.options.modal_type) {
                            case "iframe":
                                var height = modal_body.height() - parseInt(modal_body.css('padding-top'), 10) - parseInt(modal_body.css('padding-bottom'), 10);
                                $(this).find('iframe').height(Math.max(height, 50));
                                break;

                            case "ajax":
                                $.ajax({
                                    url: url, dataType: "html", async: false, success: function (data) {
                                        modal_body.html(data);
                                    }
                                });
                                break;

                            case "template":
                                self._loadTemplate("modal");
                                //	also serve calendar instance to underscore template to be able to access current language strings
                                modal_body.html(self.options.templates["modal"]({ "event": event, "calendar": self }))
                                break;
                        }

                        //	set the title of the bootstrap modal
                        if (_.isFunction(self.options.modal_title)) {
                            modal.find(".modal-title").html(self.options.modal_title(event));
                        }
                    })
                    .on('shown.bs.modal', function () {
                        self.options.onAfterModalShown.call(self, self.options.events);
                    })
                    .on('hidden.bs.modal', function () {
                        self.options.onAfterModalHidden.call(self, self.options.events);
                    })
                    .data('handled.bootstrap-calendar', true).data('handled.event-id', event.id);
            }
            modal.modal('show');
        });
    };

    Calendar.prototype._update_day = function () {
        $('#cal-day-panel').height($('#cal-day-panel-hour').height());
    };

    Calendar.prototype._update_week = function () {
    };

    Calendar.prototype._update_year = function () {
        this._update_month_year();
    };

    Calendar.prototype._update_month = function () {
        this._update_month_year();

        var self = this;

        if (this.options.weekbox == true) {
            var week = $(document.createElement('div')).attr('id', 'cal-week-box');
            var start = this.options.position.start.getFullYear() + '-' + this.options.position.start.getMonthFormatted() + '-';
            self.context.find('.cal-month-box .cal-row-fluid')
                .on('mouseenter', function () {
                    var p = new Date(self.options.position.start);
                    var child = $('.cal-cell1:first-child .cal-month-day', this);
                    var day = (child.hasClass('cal-month-first-row') ? 1 : $('[data-cal-date]', child).text());
                    p.setDate(parseInt(day));
                    day = (day < 10 ? '0' + day : day);
                    week.html(self.locale.week.format(self.options.display_week_numbers == true ? p.getWeek(getExtentedOption(self, 'week_numbers_iso_8601')) : ''));
                    week.attr('data-cal-week', start + day).show().appendTo(child);
                })
                .on('mouseleave', function () {
                    week.hide();
                });

            week.click(function () {
                self.options.day = $(this).data('cal-week');
                self.view('week');
            });
        }


        self.context.find('a.event').mouseenter(function () {
            $('a[data-event-id="' + $(this).data('event-id') + '"]').closest('.cal-cell1').addClass('day-highlight dh-' + $(this).data('event-class'));
        });
        self.context.find('a.event').mouseleave(function () {
            $('div.cal-cell1').removeClass('day-highlight dh-' + $(this).data('event-class'));
        });
    };

    Calendar.prototype._update_month_year = function () {
        if (!this.options.views[this.options.view].slide_events) {
            return;
        }
        var self = this;
        var activecell = 0;
        var downbox = $(document.createElement('div')).attr('id', 'cal-day-tick').html('<i class="icon-chevron-down glyphicon glyphicon-chevron-down"></i>');

        self.context.find('.cal-month-day, .cal-year-box .span3')
            .on('mouseenter', function () {
                if ($('.events-list', this).length == 0) {
                    return;
                }
                if ($(this).children('[data-cal-date]').text() == self.activecell) {
                    return;
                }
                downbox.show().appendTo(this);
            })
            .on('mouseleave', function () {
                downbox.hide();
            })
            .on('click', function (event) {
                if ($('.events-list', this).length == 0) {
                    return;
                }
                if ($(this).children('[data-cal-date]').text() == self.activecell) {
                    return;
                }
                $(".opened").removeClass("opened");
                $(this).addClass("opened");
                showEventsList(event, downbox, slider, self);
            });

        var slider = $(document.createElement('div')).attr('id', 'cal-slide-box');
        slider.hide().click(function (event) {
            event.stopPropagation();
        });

        this._loadTemplate('events-list');

        downbox.click(function (event) {
            $(".opened").removeClass("opened");
            $(this).parent().addClass("opened");
            showEventsList(event, $(this), slider, self);
        });
    };

    Calendar.prototype.getEventsBetween = function (start, end) {
        var events = [];
        $.each(this.options.events, function () {
            if (this.start == null) {
                return true;
            }
            var event_end = this.end || this.start;
            if ((parseInt(this.start) < end) && (parseInt(event_end) > start)) {
                events.push(this);
            }
        });
        return events;
    };
    //thamu
    Calendar.prototype._dayCount = function (start, end) {
        var events = [];
        $.each(this.options.events, function () {
            if (this.start == null) {
                return true;
            }
            var event_end = this.end || this.start;
            if ((parseInt(this.start) < end) && (parseInt(event_end) > start)) {
                events.push(this);
            }
        });
        return events.length;
    }
    //thamu
    var getDate = function (row, obj, blnReturnTimeStamp) {
        if (obj == undefined || row === undefined || row.length == 0)
            return;
        var value = getValue(row, obj.column);
        if (value == "" || value === undefined)
            return;
        if (obj.format == 101) {
            var arr = value.split('/');
            if (blnReturnTimeStamp) {
                return new Date(arr[2] + '-' + arr[0] + '-' + arr[1]).getTime();
            }
            return new Date(arr[2] + '-' + arr[0] + '-' + arr[1]);
        }
        else if (obj.format == 103) {
            var arr = value.split('/');
            if (blnReturnTimeStamp)
                return new Date(arr[2] + '-' + arr[1] + '-' + arr[0]).getTime();
            return new Date(arr[2] + '-' + arr[1] + '-' + arr[0]);
        }
    }
    var getValue = function (objRow, columnName) {
        return objRow[columnName];
    }
    Calendar.prototype.getDataBetween = function (start, end) {
        var Data = [];
        var self = this;
        $.each(this.options._eventsObj.data, function (index, objRow) {
            if (getDate(objRow, self.options._eventsObj.startDateColumn, true) == null) {
                return true;
            }
            var event_end = getDate(objRow, self.options._eventsObj.endDateColumn, true) || getDate(objRow, self.options._eventsObj.startDateColumn, true);
            if ((parseInt(getDate(objRow, self.options._eventsObj.startDateColumn, true)) < end) && (parseInt(event_end) > start)) {
                Data.push(objRow);
            }
        });
        return Data;
    };

    function showEventsList(event, that, slider, self) {

        event.stopPropagation();

        var that = $(that);
        var cell = that.closest('.cal-cell');
        var row = cell.closest('.cal-before-eventlist');
        var tick_position = cell.data('cal-row');

        that.fadeOut('fast');

        slider.slideUp('fast', function () {
            var event_list = $('.events-list', cell);
            slider.html(self.options.templates['events-list']({
                cal: self,
                events: self.getEventsBetween(parseInt(event_list.data('cal-start')), parseInt(event_list.data('cal-end'))),
                //thamu
                data: self.getDataBetween(parseInt(event_list.data('cal-start')), parseInt(event_list.data('cal-end'))),
                columns: self.options.bootstrapColumns,
            }));
            row.after(slider);
            self.activecell = $('[data-cal-date]', cell).text();
            $('#cal-slide-tick').addClass('tick' + tick_position).show();
            slider.slideDown('fast', function () {
                $('body').one('click', function () {
                    slider.slideUp('fast');
                    self.activecell = 0;
                });
            });
        });

        // Wait 400ms before updating the modal & attach the mouseenter&mouseleave(400ms is the time for the slider to fade out and slide up)
        setTimeout(function () {
            $('a.event-item').mouseenter(function () {
                $('a[data-event-id="' + $(this).data('event-id') + '"]').closest('.cal-cell1').addClass('day-highlight dh-' + $(this).data('event-class'));
            });
            $('a.event-item').mouseleave(function () {
                $('div.cal-cell1').removeClass('day-highlight dh-' + $(this).data('event-class'));
            });
            self._update_modal();
        }, 400);
    }

    function getEasterDate(year, offsetDays) {
        var a = year % 19;
        var b = Math.floor(year / 100);
        var c = year % 100;
        var d = Math.floor(b / 4);
        var e = b % 4;
        var f = Math.floor((b + 8) / 25);
        var g = Math.floor((b - f + 1) / 3);
        var h = (19 * a + b - d - g + 15) % 30;
        var i = Math.floor(c / 4);
        var k = c % 4;
        var l = (32 + 2 * e + 2 * i - h - k) % 7;
        var m = Math.floor((a + 11 * h + 22 * l) / 451);
        var n0 = (h + l + 7 * m + 114)
        var n = Math.floor(n0 / 31) - 1;
        var p = n0 % 31 + 1;
        return new Date(year, n, p + (offsetDays ? offsetDays : 0), 0, 0, 0);
    }

    $.fn.calendar = function (params) {
        return new Calendar(params, this);
    }
}(jQuery));

(function ($) {
    var defaluts = {
        data: [],
        bootstrapColumns: [],
        columns: [],

        startDateColumn: {
            column: undefined,
            format: 103,
        },
        endDateColumn: {
            column: undefined,
            format: 103,
        },
        EventColor: {
            column: undefined,
            value: [],
            color: []
        },
        urlColumn: "",
        idColumn: "",
        fieldClass: "event-important",
        defaultView: "month",
        templatePath: 'tmpls/',

        enable: ["next", "today", "prev", "year", "month", "week", "day"],
        nextText: "Next",
        todayText: "Today",
        prevText: "Previous",
        yearText: "Year",
        monthText: "Month",
        weekText: "Week",
        dayText: "Day",

        nextClass: "btn btn-primary",
        todayClass: "btn btn-default",
        prevClass: "btn btn-primary",
        yearClass: "btn btn-warning",
        monthClass: "btn btn-warning",
        weekClass: "btn btn-warning",
        dayClass: "btn btn-warning",

        titleAlign: "left",
        buttonAlign: "right",



        onprevClick: function () { },
        ontodayClick: function () { },
        onnextClick: function () { },

        onyearClick: function () { },
        onmonthClick: function () { alert("sdfd") },
        onweekClick: function () { },
        ondayClick: function () { },
    };
    var columnDefauls = {
        columnSplit: ";",
        blnWithColumnTitle: false,
        valueTitleSplit: ":",
        field: undefined,
        title: undefined
    };
    var event_classes = {
        "red": "event-important",
        "blue": "event-info",
        "green": "event-success",
        "yellow": "event-warning",
        "gray": "",
        "black": "event-inverse",
        "pink": "event-special"
    }
    var buttonEnum = {
        next: "<button type='button' data-type='navigate'></button>",
        prev: "<button type='button' data-type='navigate'></button>",
        today: "<button type='button' data-type='navigate'></button>",
        year: "<button type='button' data-type='view'></button>",
        month: "<button type='button' data-type='view'></button>",
        week: "<button type='button' data-type='view'></button>",
        day: "<button type='button' data-type='view'></button>",
    }

    //functions
    var getValue = function (objRow, columnName) {
        return objRow[columnName];
    }

    var getDate = function (row, obj, blnReturnTimeStamp) {
        if (obj == undefined || row === undefined || row.length == 0)
            return;
        var value = getValue(row, obj.column);
        if (value == "" || value === undefined)
            return;
        if (obj.format == 101) {
            var arr = value.split('/');
            if (blnReturnTimeStamp) {
                return new Date(arr[2] + '-' + arr[0] + '-' + arr[1]).getTime();
            }
            return new Date(arr[2] + '-' + arr[0] + '-' + arr[1]);
        }
        else if (obj.format == 103) {
            var arr = value.split('/');
            if (blnReturnTimeStamp)
                return new Date(arr[2] + '-' + arr[1] + '-' + arr[0]).getTime();
            return new Date(arr[2] + '-' + arr[1] + '-' + arr[0]);
        }
    }

    var EnableCalendarView = function (target, option) {
        this.$el = $(target);
        this.$el_ = this.$el.clone();
        this.options = option;
        this._events = [];
        this.initDiv();
        this.init();
    }
    EnableCalendarView.prototype.initDiv = function () {
        var that = this;
        this.pageHeader = $(
            [
                '<div style="margin-top: 0px !important;">',
                    '<div>',
                        '<h3 class="title"></h3>',
                        '<div class="buttons"></div>',
                    '</div>',
                    '<div class="col-xs-12 calendar"></div>',
                '</div>',
            ].join(''));
        this.buttons = this.pageHeader.find(".buttons");
        this.buttons.addClass("pull-" + this.options.buttonAlign);
        this.calendarDiv = this.pageHeader.find(".calendar");
        this.title = this.pageHeader.find(".title");
        this.title.addClass("pull-" + this.options.titleAlign);
        this.$el.append(this.pageHeader);

        if (that.options.enable.length > 0) {
            that.options.enable.forEach(function (value) {
                var button = $(buttonEnum[value]);
                button.html(that.options[value + "Text"]);
                button.addClass(that.options[value + "Class"]);
                button.click(function () {
                    that.calendar[$(this).data("type")](value);
                    that.options["on" + value + "Click"].call(that);
                });
                that.buttons.append(button);
            });
        }
    };

    EnableCalendarView.prototype.setColor = function (row) {
        if (row === undefined || row.length == 0)
            return;
        if (this.options.EventColor === undefined || !this.options.EventColor.hasOwnProperty("column") || !this.options.EventColor.hasOwnProperty("value") || !this.options.EventColor.hasOwnProperty("color"))
            return event_classes["yellow"];
        var value;
        for (var intLoop = 0; intLoop < this.options.EventColor.value.length; intLoop++) {
            if (row[this.options.EventColor.column] == this.options.EventColor.value[intLoop]) {
                value = event_classes[this.options.EventColor.color[intLoop].toLowerCase()];
                break;
            }
        }
        return value;
    }
    EnableCalendarView.prototype.init = function () {
        if (this.options.data.length <= 0)
            throw new Error("Data is empty");
        if (!this.options.columns === undefined)
            throw new Error("events columns field is empty");
        if (this.options.startDateColumn.column === undefined)
            throw new Error("Start Date Column is reqired");
        var that = this, strTitle = "";


        that.options.data.forEach(function (row) {
            strTitle = "";
            that.options.columns.forEach(function (column) {
                column = $.extend({}, columnDefauls, column);
                if (column.blnWithColumnTitle) {
                    strTitle += column.title + column.valueTitleSplit + row[column.field] + column.columnSplit;
                }
                else {
                    strTitle += row[column.field] + column.columnSplit;
                }
            });
            that._events.push(
                {
                    "title": strTitle,//strTitle.trim().substring(0, strTitle.length - 1),
                    "start": getDate(row, that.options.startDateColumn, true).toString(),
                    "end": (getDate(row, that.options.endDateColumn, true) === undefined ? getDate(row, that.options.startDateColumn, true) + 7200000 : getDate(row, that.options.endDateColumn, true)).toString(),
                    "url": getValue(row, that.options.urlColumn),
                    "class": that.setColor(row),
                    "id": getValue(row, that.options.idColumn)
                });
        });
        var options = {
            columns: that.options.columns,
            bootstrapColumns: that.options.bootstrapColumns.length > 0 ? that.options.bootstrapColumns : that.options.columns,
            _eventsObj: that.options,
            events_source: that._events,
            view: that.options.defaultView,
            tmpl_path: that.options.templatePath,
            tmpl_cache: false,
            day: (new Date().getFullYear()) + "-" + (new Date().getMonth() + 1) + "-" + (new Date().getDate()),
            time_start: '00:00',
            time_end: '24:00',
            onAfterViewLoad: function (view) {
                that.title.text(this.getTitle());

                // $('#divCalendar .page-header h3').text(this.getTitle());
                // $('#divCalendar .btn-group button').removeClass('active');
                // $('button[data-calendar-view="' + view + '"]').addClass('active');
            },
        };
        this.calendar = that.calendarDiv.calendar(options);
    };
    $.fn.enableCalendarView = function (option) {
        var obj = $.extend(true, defaluts, option);
        return new EnableCalendarView(this, obj);
    }
}(jQuery));
// Calendar view end

(function ($, window) {
    $.fn.contextMenu = function (settings) {
        return this.each(function () {
            // Open context menu
            $(this).on("contextmenu", function (e) {
                // return native menu if pressing control                
                if (e.ctrlKey) return;

                //open menu
                var $menu = $(settings.menuSelector)
                    .data("invokedOn", $(e.target))
                    .show()
                    .css({
                        position: "absolute",
                        left: getMenuPosition(e.clientX, 'width', 'scrollLeft'),
                        top: getMenuPosition(e.clientY, 'height', 'scrollTop')
                    })
                    .off('click')
                    .on('click', 'a', function (e) {
                        $menu.hide();

                        var $invokedOn = $menu.data("invokedOn");
                        var $selectedMenu = $(e.target);

                        settings.menuSelected.call(this, $invokedOn, $selectedMenu);
                    });

                return false;
            });

            //make sure menu closes on any click
            $('body').click(function () {
                $(settings.menuSelector).hide();
            });
        });

        function getMenuPosition(mouse, direction, scrollDir) {
            var win = $(window)[direction](),
                scroll = $(window)[scrollDir](),
                menu = $(settings.menuSelector)[direction](),
                position = mouse + scroll;

            // opening menu would pass the side of the page
            if (mouse + menu > win && menu < mouse)
                position -= menu;

            return position;
        }
    };
})(jQuery, window);
