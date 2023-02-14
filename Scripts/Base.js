
window.alert = function (msg, classes, callback) {
    if (classes == undefined || classes == null || classes == "") {
        classes = "normal";
    }
    if (typeof classes == "function") {
        callback = classes;
    }
    if ($("#alert-message.alert-message").length == 0) {
        var divAlert = $([
            "<div id='alert-message' class='alert-message " + classes + "'>",
            "<div class='alert-message-dialog'>",
            "<div class='alert-message-content'>",
            "<div class='alert-message-header'><h3><img src='/hrms/Images/info_primary.png'>&nbsp;E-PAY Message</h3></div>",
            "<div class='alert-message-body'></div>",
            "<div class='alert-message-footer'>",
            "<button class='alert-message-ok-button btn btn-primary'>OK</button>",
            "</div>",
            "</div>",
            "</div>",
            "</div>",
        ].join(''));
        divAlert.find(".alert-message-body").html(msg);

        divAlert.find(".alert-message-ok-button").click(function () {
            $(".alert-message-backdrop,#alert-message.alert-message").remove();
            if (typeof callback == "function") {
                callback.call();
            }
        });
        $(document.body).append(divAlert).append("<div class='alert-message-backdrop'></div>");
    }
}

window.confirm = function (msg, classes, successCallback, failedCallback) {
    if (classes == undefined || classes == null || classes == "") {
        classes = "normal";
    }
    if (typeof classes == "function") {
        failedCallback = successCallback;
        successCallback = classes;
    }

    if ($("#alert-message.alert-message").length == 0) {

        var divAlert = $([
            "<div id='alert-message' class='alert-message " + classes + "'>",
            "<div class='alert-message-dialog'>",
            "<div class='alert-message-content confirm'>",
            "<div class='alert-message-header'><h3><img src='/hrms/Images/info_danger.png'>&nbsp;E-PAY Confirmation</h3></div>",
            "<div class='alert-message-body'></div>",
            "<div class='alert-message-footer1'>",
            "<button class='alert-message-yes-button btn btn-primary'>Yes</button>&nbsp;&nbsp;",
            "<button class='alert-message-no-button btn btn-default '>No</button>",
            "</div>",
            "</div>",
            "</div>",
            "</div>",
        ].join(''));
        divAlert.find(".alert-message-body").html(msg);

        divAlert.find(".alert-message-yes-button").click(function () {

            $(".alert-message-backdrop,#alert-message.alert-message").remove();
            if (typeof successCallback == "function") {
                successCallback.call();
                return true;
            }
        });
        divAlert.find(".alert-message-no-button").click(function () {
            $(".alert-message-backdrop,#alert-message.alert-message").remove();
            if (typeof failedCallback == "function") {
                failedCallback.call();
                return false;
            }
        });
        $(document.body).append(divAlert).append("<div class='alert-message-backdrop'></div>");

    }

}

$('[id$="=txt_monyear"]').datetimepicker({
    changeMonth: true,
    changeYear: true,
    format: "dd/mm/yyyy",
    language: "tr"
});

