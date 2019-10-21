(function ($) {
    'use strict';
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
        excel: 'MS-Excel',
        xlsx: 'MS-Excel (OpenXML)',
        //powerpoint: 'MS-Powerpoint',
        pdf: 'PDF'
    };

    $.extend($.fn.bootstrapTable.defaults, {
        showExport: false,
        pagination: false,
        exportDataType: 'basic', // basic, all, selected
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
        export: 'glyphicon-export icon-share'
    });

    $.extend($.fn.bootstrapTable.locales, {
        formatExport: function () {
            return 'Export data';
        }
    });
    $.extend($.fn.bootstrapTable.defaults, $.fn.bootstrapTable.locales);

    var BootstrapTable = $.fn.bootstrapTable.Constructor,
        _initToolbar = BootstrapTable.prototype.initToolbar;

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
                            sprintf('<i class="%s %s"></i> ', this.options.iconsPrefix, this.options.icons.export),
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