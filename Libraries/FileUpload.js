/************************************************************************************************
* Project Name 		    : Learning
* Module Name		    : 
* File Name 		    : fileUpload.js 
* File Type     		: JavaScript 
* Created by 			: Thamotharan <thamotharan@bowandbaan.com>, <thamotharan.tk@gamil.com>
* Created date		    : 
* Discription           : To upload the file with drag and drop
* Powered By			: Bow and Baan Technology Solutions Private Limited

Modification  History			
* Modified by 			:
* Modified date 		:
* Modification	 1		: 
*************************************************************************************************/

/********************************* how to user it **************************************

1) Add following to use 
* fileUploader.ashx,
* jquery.min.js,
* bootstrap.min.js,
* bootstrap.min.css,
* fileUpload.css

2) How to chage normal div to fileupload div
$("#divId or .divClass").fileUpload({objects});

3) Definitions of DEFALUTS
* contextMenuId     : this id for view div when right click the div
* fileAllowed       : if you set value 1 then allowed only upload and view only one value.
* viewImageperRow   : that view image in one row MAXIMUM (12) & MINIMUM (0)
* files             : that have file details that is link with FILE_DEFAULTS
* header            : what you want to view header title
* fileExtension     : which Extensions are allow to upload
* fileSavePath      : when save the uploaded file (URL Path ex:uploads/newfolder/)
* uploadFileHeight  : height of the upload files
* uploadFileWidth   : width of the upload files
* onAdd             : what do after upload the file(s)
* onDelete          : what do after delete the uploaded file
* onRename          : what do after rename the file

4) Definitions of FILE_DEFAULTS
* fileId                  : set the file id value
* fileName                : set the file name
* filePath                : Image path
* width                   : width of the uploaded file 
* height                  : height of the uploaded file 
* caption                 : are you want to show the caption?
* visible                 : are you want to view the image?
* fileImgCaptionIndicator : image caption Indicator path

5) Functions 
$("#divId or .divClass").fileUpload({functionname, arguments});  
* fileRemove          : remove the files. Argument was array contains index of div. Each div have attr (data-index)
* fileAdd             : Add the files. Argument was JSON array with FILE_DEFAULTS
* fileRename          : modify the file name. arguments (index:__whichvalue__, value:__newname__)
* getData             : get the file values of specific div (no arguments)
****************************************************************************************/

!function ($) {
    var intLoop = 0;
    var fileFilter = function (files, extensions) {
        var strFileName;
        var strfileExtension;
        var returnFiles = new Array();
        for (intLoop = 0; intLoop < files.length; intLoop++) {
            strFileName = files[intLoop].name;
            strfileExtension = strFileName.replace(/^.*\./, '').toLowerCase();
            if (extensions.indexOf(strfileExtension) >= 0) {
                returnFiles.push(files[intLoop]);
            }
        }
        return returnFiles;
    };

    var getFileNameWithDate = function (filename) {
        var strFileName = "";
        var strFileExtension = "";
        strFileName = filename.substr(0, filename.lastIndexOf('.'));
        strFileExtension = filename.split(".").pop();
        // return strFileName + getDate() + "." + strFileExtension;
        return strFileName + "." + strFileExtension;
    };

    var getDate = function () {
        now = new Date();
        year = "" + now.getFullYear();
        month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
        day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
        hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
        minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
        second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
        return day + "" + month + "" + year + "" + hour + "" + minute + "" + second;
    };

    var getColsm = function (intNum) {
        var intMax = 12;
        var intOuput = Math.floor(intMax / intNum);
        if (intOuput <= 0 || intOuput == NaN || intOuput == Infinity) {
            intOuput = 1;
        }
        return 'col-sm-' + intOuput;

    };

    var doesFileExist = function (strUrl) {
        var returnValue = "";
        var xhr = new XMLHttpRequest();
        xhr.open('HEAD', strUrl, false);
        xhr.send();
        if (xhr.status != "200") {
            returnValue = xhr.statusText;
        }
        return returnValue;
    };


    var fileUpload = function (el, options) {
        this.options = options;
        this.$el = $(el);
        this.$el_ = this.$el.clone();
        this.init();
    };
    fileUpload.prototype.selectedIndex = [];
    fileUpload.DEFAULTS = {
        contextMenuId: undefined,
        fileAllowed: undefined,
        viewImageperRow: 5,
        files: [],
        header: 'File Upload',
        fileExtension: ["jpg", "png", "jpeg", "gif"],
        fileSavePath: '/Uploads/',
        uploadFileHeight: 210,
        uploadFileWidth: 210,
        onAdd: function (files) {
            return false;
        },
        onDelete: function (file) {
            return false;
        },
        onRename: function (file) {
            return false;
        }
    };
    fileUpload.FILE_DEFAULTS = {
        fileId: 0,
        fileName: '',
        filePath: undefined,
        width: undefined,
        height: undefined,
        caption: false,
        visible: true,
        fileImgCaptionIndicator: ""
    };
    /**********************************************************************************
    File upload render section 
    **********************************************************************************/
    fileUpload.prototype.init = function () {
        this.initContainer();
        this.initToolbar();
        this.initData();
        //this.initContentText();
        this.initModal();
        this.initFiles(true);
    };

    fileUpload.prototype.initData = function (files) {
        this.files = files || this.options.files;
        this.options.files = this.files;
    };
    //-------------- render container --------------------
    fileUpload.prototype.initContainer = function () {
        var that = this;

        this.$container = $([
           // '<div class="fileupload-header panel-heading">',
            //"<h3 class='h3'>" + that.options.header + "</h3>",
           // '</div>',
            '<div class="col-lg-12 panel-body">',
              '<div class="fileupload-toolbar">',
                '</div>',
            '<div class="fileupload-files">',
                '</div>',
            '</div>'
        ].join(''));
        //$(this.$el).addClass("panel panel-default");
        this.$el.append(this.$container);
        this.$fileUploadContentHeader = this.$container.find(".fileupload-header");
        this.$fileUploadContainer = this.$container.find(".fileupload-container");
        this.$fileUploadFiles = this.$container.find(".fileupload-files");
        this.$fileToolbar = this.$container.find(".fileupload-toolbar");

        this.$container.on('dragenter', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $(this).addClass("uploadBg");
        });
        this.$container.on('dragover', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $(this).addClass("uploadBg");
        });
        this.$container.on('dragleave', function (e) {
            e.stopPropagation();
            e.preventDefault();
            $(this).removeClass("uploadBg");
        });

        this.$container.on('drop', function (e) {
            e.preventDefault();
            var files = e.originalEvent.dataTransfer.files;
            if (that.options.fileExtension == undefined) {
                return;
            }
            files = fileFilter(files, that.options.fileExtension);
            if (files.length > 0) {
                //that.fileAdd({ 'index': 0, 'files': that.files_upload(files) });
                //that.fileAdd({ 'index': 0, 'files':  });
                that.files_upload(files);
            }
        });

        this.$container.on('click', function (event) {
            var targetClass = $(event.target).parents(".file-upload-imgThumbnail").attr("class");
            var actionbarClass = $(event.target).parents(".app-actions-bar").attr("class");
            var confirmModalClass = $(event.target).parents(".fileupload-modal-confirm").attr("class");
            var renameModalClass = $(event.target).parents(".fileupload-modal-rename").attr("class");
            if (targetClass == undefined && actionbarClass == undefined && renameModalClass == undefined && confirmModalClass == undefined) {
                $(this).find("div").removeClass("img-selected");
                that.$fileDelete.css("display", "none");
                that.$fileRename.css("display", "none");
                that.selectedIndex = [];
            }
        });
    };

    //-------------------- render toolbar ---------------------------
    fileUpload.prototype.initToolbar = function () {
        var that = this;
        this.$tools = $([
                "<div class='app-actions-bar col-sm-12'>",
                    //"<img alt='Add Image' class='tool-bar-fileadd' title='Add Image' src='../Images//ActionBar/add.jpg' height='50px' width='50px' />",
                    "<img alt='Add Image' class='tool-bar-fileremove' title='Add Image' src='../Images//ActionBar/delete-icon.png' height='50px' width='50px' />",
                    //"<img alt='Add Image' class='tool-bar-filerename' title='Add Image' src='../Images//ActionBar/Rename.png' height='50px' width='50px' />",
                    that.options.fileAllowed == 1 ?
                        "<input type='file' name='tool-bar-fileup' class='tool-bar-fileup' />" :
                        "<input type='file' name='tool-bar-fileup[]' class='tool-bar-fileup' multiple />",
                "</div>"
        ].join(''));
        this.$fileToolbar.append(this.$tools);
        this.$fileAdd = this.$tools.find(".tool-bar-fileadd");
        this.$fileDelete = this.$tools.find(".tool-bar-fileremove");
        this.$fileRename = this.$tools.find(".tool-bar-filerename");
        this.$fileOpen = this.$tools.find(".tool-bar-fileup");

        this.$fileDelete.css("display", "none");
        this.$fileRename.css("display", "none");
        this.$fileOpen.css("display", "none");

        this.$fileAdd.on("click", function (event) {
            that.$fileOpen.trigger("click");
        });
        this.$fileDelete.on("click", function (event) {
            //that.$confirmModal.find(".modal-body").text("Are you sure to delete " + that.selectedIndex.length + " items?");
            //that.$confirmModal.modal("show");
            $("#dialog_new").dialog(
            {
                autoOpen: false,
                show: "blind",
                width: 350,
                hide: "explode",
                modal: true,
                closeOnEscape: false,
                dialogClass: 'no-close',
                title: "Message..!!",
                buttons: {
                    "Yes": function () {
                        var strFilePaths = '';
                        if (that.selectedIndex.length > 0) {

                            var fileSelected = that.selectedIndex;
                            for (intLoop = 0; intLoop < that.selectedIndex.length ; intLoop++) {
                                strFilePaths += that.files[that.selectedIndex[intLoop]].fileName + ',';
                            }

                            ShowLoader();
                            $.ajax({
                                type: "POST",
                                url: 'frmDeathDetails.aspx/FileDelete',
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                processData: false,
                                data: JSON.stringify({ strFilePath: strFilePaths }),
                                beforeSend: function () {
                                    $("#loading").show();
                                },
                                complete: function () {
                                    $("#loading").hide();
                                },
                                success: function (msg) {
                                    if (msg.d == 1) {
                                        that.fileRemove(that.selectedIndex);
                                        that.selectedIndex = [];
                                        $(".img-selected").removeClass("img-selected");
                                        that.imgClickCheck();
                                        $('#flpPM').val('');
                                        Calldialog("Deleted Successfully.");
                                        HideLoader();
                                    }
                                },
                                error: function (data) {
                                    that.selectedIndex = [];
                                    Calldialog("Error Deleting image");
                                    HideLoader();
                                }
                            });
                        }
                        $("#dialog_new").dialog("close");
                        return true;
                    },
                    "No": function () {
                        $("#dialog_new").dialog("close");
                        return false;
                    }
                }
            });

            $("#dialog_new").text("Are you sure you want to delete?").dialog("open");
            return false;
        });
        this.$fileRename.on("click", function (event) {
            that.$renameModal_txt.val(that.options.files[that.selectedIndex[0]]['fileName']);
            $(that.$renameModal).modal("show");
        });

        this.$fileOpen.on("change", function (event) {
            var fileArr = fileFilter(this.files, that.options.fileExtension);
            //that.fileAdd({ 'index': 0, 'files':  });
            that.files_upload(fileArr);
        });
    };

    fileUpload.prototype.ReRendering = function (imagArr) {
        this.fileAdd({ 'index': 0, 'files': imagArr });
    };
    //--------------------- render contentText -----------------------
    //fileUpload.prototype.initContentText = function () {
    //    var that = this;

    //    this.$contextText = $([
    //        "<ul id= 'file-upload-context-menu' class='dropdown-menu' role='menu' style='display: none'>",
    //            '<li><a href="javascript:void(0)"  tabindex="-1">Add</a></li>',
    //        '</ul>'
    //    ].join(''));

    //    if ($("#file-upload-context-menu").length == 0) {
    //        $('body').append(this.$contextText);
    //    }

    //    $(that.$container).contextMenu({
    //        menuSelector: this.options.contextMenuId != undefined ? this.options.contextMenuId : "#file-upload-context-menu",
    //        menuSelected: function (invokedOn, selectedMenu) {
    //            if (selectedMenu.text() == "Add") {
    //                that.$fileAdd.trigger("click");
    //            }
    //        }
    //    });
    //};

    //--------------------- render modal ---------------------
    fileUpload.prototype.initModal = function () {
        var that = this;
        this.$modals = $([
            "<div class='fileupload-modals'>",
                "<div class='modal fade fileupload-modal-rename'>",
                    "<div class='modal-dialog'>",
                        "<div class='modal-content'>",
                            "<div class='modal-header'>",
                                "<button class='close' data-dismiss='modal'>&times;</button>",
                                "<h3 class='h3'>Rename Message</h3>",
                            "</div>",
                            "<div class='modal-body'>",
                                "<p>Please Enter New name for the item:</p>",
                                "<input type='text' class='form-control' name='fileupload-rename-modal-input' />",
                                "<div class='fileupload-rename-modal-err'><label class='field-validation-error'></label></div>",
                            "</div>",
                            "<div class='modal-footer'>",
                                "<button type='button' class='btn btn-default' data-dismiss='modal'>Cancel</button>",
                                "<button type='button' class='btn btn-primary' name='filupload-rename-modal-btnok'>Ok</button>",
                             "</div>",
                         "</div>",
                    "</div>",
                "</div>",
                //"<div class='modal fade fileupload-modal-confirm'>",
                //    "<div class='modal-dialog'>",
                //        "<div class='modal-content'>",
                //            "<div class='modal-header'>",
                //                "<button class='close' data-dismiss='modal'>&times;</button>",
                //                 "<h3 class='h3'>Confirm Message</h3>",
                //            "</div>",
                //            "<div class='modal-body'>",
                //            "</div>",
                //            "<div class='modal-footer'>",
                //                '<button type="button" class="btn btn-primary" name="fileupload-confirm-modal-btnok">',
                //                    '<span class="glyphicon glyphicon-ok"></span>&nbsp; Yes',
                //                 '</button>',
                //                 '<button type="button" class="btn btn-danger" data-dismiss="modal">',
                //                    '<span class="glyphicon glyphicon-remove"></span>&nbsp; No',
                //                  '</button>',
                //            '</div>',
                //        '</div>',
                //    '</div>',
                //'</div>',
             '</div>'
        ].join(''));
        this.$container.append(this.$modals);
        this.$renameModal = this.$modals.find(".fileupload-modal-rename");
        //this.$confirmModal = this.$modals.find(".fileupload-modal-confirm");
        this.$renameModal_txt = this.$modals.find("input[name='fileupload-rename-modal-input']");
        this.$renameModal_ok = this.$modals.find('button[name="filupload-rename-modal-btnok"]');
        //this.$confirmModal_ok = this.$modals.find('button[name="fileupload-confirm-modal-btnok"]');
        this.$renameModal_txtErr = this.$modals.find(".fileupload-rename-modal-err");

        that.$renameModal_txtErr.css("display", "none");

        //this.$confirmModal_ok.on("click", function (event) {
        //    //that.fileRemove(that.selectedIndex);
        //    //that.selectedIndex = [];
        //    //$(".img-selected").removeClass("img-selected");
        //    //that.imgClickCheck();

        //    //that.$confirmModal.modal("hide");
        //    //that.imgClickCheck();
        //    var strFilePaths = '';
        //    if (that.selectedIndex.length > 0) {

        //        var fileSelected = that.selectedIndex;
        //        for (intLoop = 0; intLoop < that.selectedIndex.length ; intLoop++) {
        //            strFilePaths += that.files[that.selectedIndex[intLoop]].fileName + ',';
        //            //var filecount = $('#hdnFileCount').val();
        //            //filecount = parseInt(filecount) - 1;
        //            //$('#hdnFileCount').val(filecount);
        //        }

        //        ShowLoader();
        //        $.ajax({
        //            type: "POST",
        //            url: 'frmDeathDetails.aspx/FileDelete',
        //            contentType: 'application/json; charset=utf-8',
        //            dataType: 'json',
        //            processData: false,
        //            data: JSON.stringify({ strFilePath: strFilePaths }),
        //            beforeSend: function () {
        //                $("#loading").show();
        //            },
        //            complete: function () {
        //                $("#loading").hide();
        //            },
        //            success: function (msg) {
        //                if (msg.d == 1) {
        //                    //FilesLoad();
        //                    that.fileRemove(that.selectedIndex);
        //                    that.selectedIndex = [];
        //                    $(".img-selected").removeClass("img-selected");
        //                    that.imgClickCheck();

        //                    that.$confirmModal.modal("hide");
        //                    that.imgClickCheck();
        //                    HideLoader();
        //                    //FilesLoad();
        //                    //FilesLoad();
        //                }
        //            },
        //            error: function (data) {
        //                newUploadedFiles = [];
        //                HideLoader();
        //            }
        //        });
        //    }
        //});
        this.$renameModal_txt.keypress(function (event) {
            if (that.checkValue({ 'field': 'fileName', 'value': $(this).val() }) != undefined) {
                that.$renameModal_txtErr.find("label").text("You enter value already exists!");
                that.$renameModal_txtErr.css("display", "block");
            }
            else {
                that.$renameModal_txtErr.find("label").text("");
                that.$renameModal_txtErr.css("display", "none");
            }
        });
        this.$renameModal_ok.on("click", function (event) {
            if (that.$renameModal_txt.val() != "") {
                that.$renameModal_txtErr.find("label").text("");
                that.$renameModal_txtErr.css("display", "none");
                that.fileRename({ index: that.selectedIndex[0], value: that.$renameModal_txt.val() });
                that.$renameModal.modal("hide");
            }
            else {
                that.$renameModal_txtErr.find("label").text("Please Enter image name fields!");
                that.$renameModal_txtErr.css("display", "block");
            }
        });

    };

    //---------------------- render files -----------------------
    fileUpload.prototype.initFiles = function (reload) {
        if (reload) {
            this.destroyDesign();
        }
        var file = [],
            that = this;
        this.$filesHtml = $([
            "<div class='thumbnail file-upload-imgThumbnail'>",
                "<div class='file-upload-img'>",
                    "<img />",
                "</div>",
                //"<div class='file-upload-img-caption col-sm-12'>",
                //    "<div class='file-upload-img-caption-indicator col-sm-2'>",
                //        "<img />",
                //     "</div>",
                //     "<div class='file-upload-img-caption-text col-sm-10'>",
                //        "<label></label>",
                //     "</div>",
                //"</div>",
            "</div>"
        ].join(''));

        this.$fileImg = this.$filesHtml.find(".file-upload-img");
        //this.$fileImgCaption = this.$filesHtml.find(".file-upload-img-caption");
        //this.$fileImgCaptionIndicator = this.$filesHtml.find(".file-upload-img-caption-indicator");
        //this.$fileImgCaptionText = this.$filesHtml.find(".file-upload-img-caption-text");

        if (!that.options.files.length) {
            return;
        }

        $.each(that.options.files, function (i, files) {
            that.$filesHtml.attr("data-index", i);
            files[i] = $.extend({}, fileUpload.FILE_DEFAULTS, files);
            that.$filesHtml.addClass(getColsm(that.options.viewImageperRow));

            that.$fileImg.find("img").attr("width", files[i]['width'] != undefined ? files[i]['width'] + "px" : "100%");
            that.$fileImg.find("img").attr("height", files[i]['height'] != undefined ? files[i]['height'] + "px" : "100px");
            that.$fileImg.find("img").attr("src", files[i]['filePath']);
            //that.$fileImgCaptionIndicator.find("img").attr("src", files[i].fileImgCaptionIndicator);
            //files[i]['caption'] ? that.$fileImgCaptionText.children("label").text(files[i]['fileName']) : that.$fileImgCaptionText.children("label").text("");

            files[i]['visible'] ? that.$filesHtml.clone().appendTo(that.$fileUploadFiles) : ""

        });

        that.$fileUploadFiles.find('.file-upload-imgThumbnail').on("click", function (event) {
            if (!event.ctrlKey) {
                that.$fileUploadFiles.find('div').removeClass("img-selected");
                that.selectedIndex = [];
                $(this).addClass("img-selected");
                that.selectedIndex.push($(this).attr("data-index"));
                that.imgClickCheck();
            }
            //else {
            //    if ($(this).hasClass("img-selected")) {
            //        $(this).removeClass("img-selected");
            //        var arrIndex = that.selectedIndex.indexOf($(this).attr("data-index"));
            //        that.selectedIndex.splice(arrIndex, 1);
            //        that.imgClickCheck();
            //    }
            //    else {
            //        $(this).addClass("img-selected");
            //        that.selectedIndex.push($(this).attr("data-index"));
            //        that.imgClickCheck();
            //    }
            //}
        });
    };

    /**********************************************************************************
    destroy section 
    **********************************************************************************/
    fileUpload.prototype.destroy = function () {
        this.$container.remove();
        this.options = [];
    }
    fileUpload.prototype.reset = function () {
        this.$container.remove();
        this.options = fileUpload.DEFAULTS;
        this.options.files = [];
        this.init();
    }

    //---------------------- destroy design ----------------------
    fileUpload.prototype.destroyDesign = function () {
        $("#" + $(this.$el).attr("id") + " .file-upload-imgThumbnail").remove();
    }

    /********************************************************************************
    functions 
    **********************************************************************************/
    fileUpload.prototype.getData = function () {
        return this.options.files;
    }
    //---------------------- file add ----------------------
    fileUpload.prototype.fileAdd = function (params) {
        that = this;
        if (!params.hasOwnProperty('index') || !params.hasOwnProperty('files')) {
            return;
        }

        if (params.files.length > 1) {
            $.each(params.files, function (i, file) {
                that.options.files.splice(params.index, 0, file);
            });
        }
        else {
            that.options.files.splice(params.index, 0, params.files[0]);
        }
        that.trigger('onAdd', params.files);
        this.initFiles(true);

    };
    //---------------------- file remove ----------------------
    fileUpload.prototype.fileRemove = function (params) {
        var that = this;
        if (params.length == 0) {
            return;
        }
        else {
            var arrFiles = that.options.files;
            for (var intLoop = 0; intLoop < params.length; intLoop++) {
                that.trigger('onDelete', that.options.files[params[intLoop]]);
                arrFiles.splice(parseInt(params[intLoop]), 1);
                for (var inti = 0; inti < params.length; inti++) {
                    params[inti] = parseInt(params[inti] - 1);
                }
            }
            that.options.files = arrFiles;
        }
        this.initFiles(true);
    };
    fileUpload.prototype.fileRename = function (params) {
        var that = this;
        if (!params.hasOwnProperty('index') || !params.hasOwnProperty('value')) {
            return;
        }
        this.options.files[params.index].fileName = params.value;
        this.trigger('onRename', that.options.files[that.selectedIndex[0]]);
        this.initFiles(true);
    }
    //---------------------- check image select ----------------------
    fileUpload.prototype.imgClickCheck = function () {
        var that = this;
        if (that.$fileUploadFiles.find('.img-selected').length > 1) {
            that.$fileRename.css("display", "none");
        }
        else if (that.$fileUploadFiles.find('.img-selected').length == 1) {
            that.$fileDelete.css("display", "block");
            that.$fileRename.css("display", "block");
        }
        else if (that.$fileUploadFiles.find('.img-selected').length == 0) {
            that.$fileDelete.css("display", "none");
            that.$fileRename.css("display", "none");
        }
    };
    //---------------------- check the value ----------------------
    fileUpload.prototype.checkValue = function (params) {
        if (!params.hasOwnProperty('field') || !params.hasOwnProperty('value')) {
            return;
        }
        var that = this;
        for (var intLoop = 0; intLoop < that.options.files.length; intLoop++) {
            var file = that.options.files[intLoop];
            if (!file.hasOwnProperty(params.field)) {
                continue;
            }

            if ($.inArray(file[params.field], params.value) != -1) {
                return intLoop;
            }
        }
    };
    //---------------------- get the refer text ----------------------
    fileUpload.prototype.getReferText = function () {
        if (this.options.referTextId.indexOf("#") == 0) {
            return $(this.options.referTextId).val();
        }
        else {
            return this.options.referTextId;
        }
    };
    //---------------------- get the path of save file ----------------------
    fileUpload.prototype.getPath = function (intIdentity) {
        var that = this;
        var returnValue = "";
        if (that.options.fileSavePath == undefined) {
            return;
        }
        if (intIdentity == 1) {
            if (that.options.fileSavePath.indexOf("___REFERTEXT___") >= 0) {
                returnValue = that.options.fileSavePath.replace("___REFERTEXT___", that.getReferText());
            }
            else {
                returnValue = that.options.fileSavePath;
            }
        }
        else {
            var pathArr = that.options.fileSavePath.split('\\');
            for (var intLoop = 0; intLoop < pathArr.length; intLoop++) {
                if (intLoop == 0) {
                    returnValue = pathArr[intLoop];
                }
                else {
                    returnValue = returnValue + "/" + pathArr[intLoop];
                }
            }
            if (that.options.fileSavePath.indexOf("___REFERTEXT___") >= 0) {
                returnValue = returnValue.replace("___REFERTEXT___", that.getReferText());
            }
        }
        return returnValue;
    };
    //---------------------- upload the files ----------------------
    fileUpload.prototype.files_upload = function (fileArr) {
        if ($('#txtTagno').val() != '') {
            ShowLoader();
            var that = this, newUploadedFiles = "";
            if (window.FormData != undefined) {
                newUploadedFiles = new Array();
                var allFiles = new FormData();
                for (var i = 0; i < fileArr.length; i++) {

                    var fileName = $('#hdnFileCount').val();
                    if (parseInt(fileName) == 0) {
                        fileName = 1;
                        $('#hdnFileCount').val(fileName);
                    }
                    else {
                        fileName = parseInt(fileName) + 1;
                        $('#hdnFileCount').val(fileName);
                    }
                    var fil = fileArr[i].name;
                    var fi = fil.split('.');
                    fil = fileName + '.' + fi[1];

                    allFiles.append(getFileNameWithDate(fil), fileArr[i], that.getPath(1) + getFileNameWithDate(fil));

                    var strFileName = getFileNameWithDate(fil);
                    var strFilePath = '..' + that.getPath(2) + getFileNameWithDate(fil);
                    newUploadedFiles.push(JSON.parse('{"fileId":"0","fileName":"' + strFileName + '","filePath":"' + strFilePath + '","caption":true,"height":' + that.options.uploadFileHeight + ',"width":' + that.options.uploadFileWidth + '}'));
                }
                var value = doesFileExist("../fileUploader.ashx");

                if (value == "") {
                    $.ajax({
                        type: "POST",
                        url: "../fileUploader.ashx",
                        contentType: false,
                        processData: false,
                        data: allFiles,
                        beforeSend: function () {
                            $("#loading").show();
                        },
                        complete: function () {
                            $("#loading").hide();
                        },
                        success: function (data) {
                            if (data == "Success") {
                                that.ReRendering(newUploadedFiles);
                                HideLoader();
                                return newUploadedFiles;
                            }
                        },
                        error: function (data) {
                            newUploadedFiles = [];
                            HideLoader();
                        }
                    });
                }
                else {
                    Calldialog("Your url file " + value);
                    newUploadedFiles = new Array();
                    HideLoader();
                }

            }
            else {
                Calldialog("Sorry Form Data Not Supported Please use the Another Browser");
                newUploadedFiles = new Array();
                HideLoader();
            }
            // return newUploadedFiles;
        }
        else {
            Calldialog("Enter the tag no.");
            return false;
        }
    };

    //---------------------- trigger (call) the function ----------------------
    fileUpload.prototype.trigger = function (name) {
        var args = Array.prototype.slice.call(arguments, 1);
        this.options[name].apply(this.options, args);
        //this.$el.trigger($.Event(name), args);
    }
    var allowedMethods = [
        "fileAdd",
        "fileRemove",
        "getData",
        "destroy",
        "reset"
    ];
    $.fn.fileUpload = function (option) {
        var value,
            args = Array.prototype.slice.call(arguments, 1);
        this.each(function () {
            var $this = $(this),
            files = $this.data('fileupload'),
            options = $.extend({}, fileUpload.DEFAULTS, $this.data(),
                    typeof option === 'object' && option);

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw new Error("Unknown method: " + option);
                }

                if (!files) {
                    return;
                }
                value = files[option].apply(files, args);
                if (option === 'destroy') {
                    $this.removeData('fileupload');
                }
            }
            if (!files) {
                $this.data('fileupload', (files = new fileUpload(this, options)));
            }
        });
        return typeof value === 'undefined' ? this : value;
    }
}(jQuery);

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
