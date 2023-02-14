var CurrentRoleAccess = {};
var icon = "";

var allcookies = document.cookie;
var cookiearray = allcookies.split(';');
for (var c = 0; c < cookiearray.length; c++) {
    if (cookiearray[c].split('=')[0] == " Roleid") {
        CurrentRoleAccess.RoleId = cookiearray[c].split('=')[1];
    }
}
//if (window.location.host != "localhost") {
//    $(document).ready(function () {
//        var bootstrapButton = $.fn.button.noConflict();
//        $.fn.bootstrapBtn = bootstrapButton;
//        for (var i = 0; i < 6; i++) {
//            var dat = "";

//            if (i == 0) {
//                dat = "A"

//            }
//            else if (i == 1) {
//                dat = "B"

//            }
//            else if (i == 2) {
//                dat = "C"

//            }
//            else if (i == 3) {
//                dat = "D"

//            }
//            else if (i == 4) {
//                dat = "E"

//            }
//            else if (i == 5) {
//                dat = "F"

//            }

//            $.ajax({
//                type: "POST",
//                dataType: 'Json',
//                contentType: "application/json; charset=utf-8",
//                url: "../Company_Home.aspx/RoleMenuRetrieve",//local
//                //url: "http://"+window.location.hostname+"/hrms/Company_Home.aspx/RoleMenuRetrieve",    //web
//                data: JSON.stringify({ intRoleId: CurrentRoleAccess.RoleId, strMenuCode: dat }),
//                success: function (data) {
//                    var obj = JSON.parse(data.d);
//                    if (obj.length > 0) {
//                        buildMenu($('#menu' + obj[0].MenuCode), obj, 0);
//                        // $('#menu' + obj[0].MenuCode).menu();
//                    }
//                    else {
//                        $('#menu' + dat).css("display", "none");
//                    }
//                },
//                error: function (msg) {

//                    //alert("session expired please login again!");
//                    //setTimeout(function () {
//                    //    location.replace("http://" + window.location.hostname + "/Hrms_Publish/Login.aspx");
//                    //}, 2000);
//                }
//            });


//        }
//        function buildMenu(parent, items, id, l, val) {
//            $.each(items, function (a, b) {
//                if (id == 0) {
//                    //var obj = JSON.parse(b);
//                    var obj = b;

//                    //for (var i = 0; i < obj.length; i++) {

//                    if (obj.lstMenu && obj.lstMenu.length > 0) {

//                        if (obj.MenuName == " Setup") {

//                            icon = "fa fa-wrench fa-fw"
//                        }
//                        else if (obj.MenuName == " Settings") {

//                            icon = "fa fa-gear fa-fw"
//                        }
//                        else if (obj.MenuName == " Masters") {

//                            icon = "fa fa-sitemap fa-fw"
//                        }
//                        else if (obj.MenuName == "Time Attendance") {

//                            icon = "fa fa-clock-o fa-fw "
//                        }
//                        else if (obj.MenuName == " Payroll") {

//                            icon = "fa fa-money fa-fw"
//                        }
//                        else if (obj.MenuName == " Reports") {

//                            icon = "fa fa-credit-card fa-fw"
//                        }


//                        var li = $('<a id="' + b.MenuId + '"   href="' + obj.MenuLink + '"><i class="' + icon + '"></i>' + obj.MenuName + '<span class="fa arrow"></span></a>');
//                    }
//                    else {
//                        var li = $('<li><a id="' + b.MenuId + '"  href="' + obj.MenuLink + '">' + obj.MenuName + '<span class="caret"></span></a></li>');
//                    }
//                    li.appendTo(parent);
//                    if (obj.lstMenu && obj.lstMenu.length > 0) {
//                        var ul = $('<ul class="nav nav-second-level"></ul>');
//                        ul.appendTo(parent);
//                        buildMenu(ul, obj.lstMenu, 1, 0);
//                    }
//                    //}
//                }
//                else {
//                    //var li = $('<li ><a  tabindex="-1" id="' + b.MenuId + '" runat="server" href="' + b.MenuLink + '">' + b.MenuName + '</a></li>');
//                    if (b.lstMenu && b.lstMenu.length > 0) {
//                        var li = $('<li ><a id="' + b.MenuId + '" href="' + b.MenuLink + '">' + b.MenuName + '<span class="fa arrow"></span></a></li>');
//                    }
//                    else {
//                        var li = $('<li><a  id="' + b.MenuId + '" href="' + b.MenuLink + '">' + b.MenuName + '</a></li>');
//                    }
//                    li.appendTo(parent);
//                    if (b.lstMenu && b.lstMenu.length > 0) {
//                        var ul = $('<ul class="nav nav-third-level "></ul>');
//                        ul.appendTo(li);
//                        buildMenu(ul, b.lstMenu, 1, 0);
//                    }
//                }

//            });

//        }

//    });

//}
//else {

    $(document).ready(function () {
        var bootstrapButton = $.fn.button.noConflict();
        $.fn.bootstrapBtn = bootstrapButton;
        for (var i = 0; i < 6; i++) {
            var dat = "";
            if (i == 0) {
                dat = "A"
            }
            else if (i == 1) {
                dat = "B"
            }
            else if (i == 2) {
                dat = "C"
            }
            else if (i == 3) {
                dat = "D"
            }
            else if (i == 4) {
                dat = "E"
            }
            else if (i == 5) {
                dat = "F"
            }


            $.ajax({
                type: "POST",
                dataType: 'Json',
                contentType: "application/json; charset=utf-8",
                //url: "http://"+window.location.hostname+"/hrms/Company_Home.aspx/RoleMenuRetrieve",    //web

                url: "http://" + window.location.hostname + "/Hrms_Publish/Company_Home.aspx/RoleMenuRetrieve",    //web
                data: JSON.stringify({ intRoleId: CurrentRoleAccess.RoleId, strMenuCode: dat }),
                success: function (data) {
                    var obj = JSON.parse(data.d);
                    if (obj.length > 0) {
                        buildMenu($('#menu' + obj[0].MenuCode), obj, 0);
                        // $('#menu' + obj[0].MenuCode).menu();
                    }
                    else {
                        $('#menu' + dat).css("display", "none");
                    }
                },
                error: function (msg) {
                    //alert("session expired please login again!");
                    //setTimeout(function () { 
                    //    location.replace("http://" + window.location.hostname + "/Hrms_Publish/Login.aspx");
                    //}, 2000);
                }
            });

           

        }
        function buildMenu(parent, items, id, l, val) {
            $.each(items, function (a, b) {
                if (id == 0) {
                    //var obj = JSON.parse(b);
                    var obj = b;

                    //for (var i = 0; i < obj.length; i++) {

                    if (obj.lstMenu && obj.lstMenu.length > 0) {


                        if (obj.MenuName == " Setup") {

                            icon = "fa fa-wrench fa-fw"
                        }
                        else if (obj.MenuName == " Settings") {

                            icon = "fa fa-gear fa-fw"
                        }
                        else if (obj.MenuName == " Masters") {

                            icon = "fa fa-sitemap fa-fw"
                        }
                        else if (obj.MenuName == "Time Attendance") {

                            icon = "fa fa-clock-o fa-fw "
                        }
                        else if (obj.MenuName == " Payroll") {

                            icon = "fa fa-money fa-fw"
                        }
                        else if (obj.MenuName == " Reports") {

                            icon = "fa fa-credit-card fa-fw"
                        }

                var li = $('<a id="' + b.MenuId + '"   href="' + obj.MenuLink + '"><i class="' + icon + '"></i>' + obj.MenuName + '<span class="fa arrow"></span></a>');
                    }
                    else {
                        var li = $('<li><a id="' + b.MenuId + '"  href="/Hrms_Publish' + obj.MenuLink + '">' + obj.MenuName + '<span class="caret"></span></a></li>');
                    }
                    li.appendTo(parent);
                    if (obj.lstMenu && obj.lstMenu.length > 0) {
                        var ul = $('<ul class="nav nav-second-level"></ul>');   
                        ul.appendTo(parent);
                        buildMenu(ul, obj.lstMenu, 1, 0);
                    }
                    //}
                }
                else {
                    //var li = $('<li ><a  tabindex="-1" id="' + b.MenuId + '" runat="server" href="' + b.MenuLink + '">' + b.MenuName + '</a></li>');
                    if (b.lstMenu && b.lstMenu.length > 0) {
                        var li = $('<li ><a id="' + b.MenuId + '" href="' + b.MenuLink + '">' + b.MenuName + '<span class="fa arrow"></span></a></li>');
                    }
                    else {
                        var li = $('<li><a  id="' + b.MenuId + '" href="/Hrms_Publish' + b.MenuLink + '">' + b.MenuName + '</a></li>');
                    }
                    li.appendTo(parent);
                    if (b.lstMenu && b.lstMenu.length > 0) {
                        var ul = $('<ul class="nav nav-third-level "></ul>');
                        ul.appendTo(li);
                        buildMenu(ul, b.lstMenu, 1, 0);
                    }
                }

            });

        }

    });

/*}*/

//C:\HRMS\js\Menu_lode.js