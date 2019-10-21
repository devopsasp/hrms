var CurrentRoleAccess = {
};
var CurrentAccess = {
};

var rol;

function lode() {
    //CheckSesion(CurrentUser.WebPath);
    //if (CurrentLoggedUser.UserId == null || CurrentLoggedUser.UserId == undefined || CurrentLoggedUser.UserId == "") {
    ////    Loginalert("Your session expired. Sorry for inconvenience.");
    //    return;
    //}
    //else {
    //    SetRights();
    RoleLoad();
    rol = -1;
    RoleAccessLoad(rol);
   
    //}
};

function SetRights() {
    CurrentAccess.RoleId = CurrentLoggedUser.RoleId;
    CurrentAccess.MenuId = 3;

    ShowLoader();
    $.ajax({
        type: 'POST',
        url: 'PMS_Master.asmx/RoleAccess',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ ra: CurrentAccess }),
        async: true,
        success: function (msg) {
            if (msg.d.ViewVisible == true) {
                if (msg.d.SaveVisible == true) {
                    $("#btnSave").css("visibility", "visible");
                }
                else {
                    $("#btnSave").css("visibility", "hidden");
                }
                if (msg.d.DeleteVisible == true) {
                    $("#btnDelete").css("visibility", "visible");
                }
                else {
                    $("#btnDelete").css("visibility", "hidden");
                }
            }
            else {
                Loginalert("You don't have access for this page.");
            }
            HideLoader();
        },
        error: function (msg) {
            alert("Error in set rights!");
            HideLoader();
        }
    });
}

function RoleLoad() {
    $.ajax({
        type: 'POST',
        url: 'user_access.aspx/RoleRetrieve',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async: true,
        success: function (msg) {
            Roles = $.extend(true, [], msg.d);
            if (Roles.length > 0) {
                RoleRetrieve(Roles);
            }
        },
        error: function (msg) {
            alert("Failed loading Role.");
        }
    });
}

function RoleRetrieve(Roles) {
    var Role = $("[id*=sltRole]");
    Role.empty().append('<option selected="selected" value="0">--Select--</option>');
    $.each(Roles, function () {
        Role.append($("<option></option>").val(this['RoleId']).html(this['RoleName']));
    });
}

function CheckAll(ival) {
    var f = document.getElementById("tblRoleAccess");

    if (f != null) {
        for (var j = 0; j < f.rows.length; j++) {
            if (f.getElementsByTagName("input").item(j).type == "checkbox") {

                var row = ival + j;
                if ($('input[id="' + row + '"' + ']') && $('input[id="' + row + '"' + ']').prop("disabled") == false) {
                    $('input[id="' + row + '"' + ']').prop("checked", $('input[id="' + ival + '"' + ']').prop("checked"));
                }
            }
        }
    }
}


function Checkgroup(ival) {
    var f = document.getElementById("tblRoleAccess");
    var s,e;
    if (ival =="chkView1") {
        s = 2;
        e = 5;
    }
    else if(ival == "chkView6")
    {
        s = 7;
        e = 14;
    }
    else if(ival == "chkView7"){
        s = 9;
        e = 14;
    }
    else if(ival == "chkView15"){
        s = 16;
        e = 34;
    }
    else if(ival == "chkView16"){
        s = 17;
        e = 18;
    }
    else if(ival == "chkView19"){
        s = 20;
        e = 25;
    }
    else if (ival == "chkView26") {
        s = 27;
        e = 29;
    }
    else if (ival == "chkView35") {
        s = 36;
        e = 44;
    }
    else if (ival == "chkView36") {
        s = 37;
        e = 39;
    }
    else if(ival == "chkView45"){
        s = 46;
        e = 57;
    }
    else if (ival == "chkView46") {
        s = 48;
        e = 51;
    }
    else if (ival == "chkView47") {
        s = 53;
        e = 54;
    }
    else if (ival == "chkView58") {
        s = 59;
        e = 63;
    }
    if (f != null) {
        for (var j = s; j <=e; j++) {
            if (f.getElementsByTagName("input").item(j).type == "checkbox") {

                var row = "chkView" + j;
                if ($('input[id="' + row + '"' + ']') && $('input[id="' + row + '"' + ']').prop("disabled") == false) {
                    $('input[id="' + row + '"' + ']').prop("checked", $('input[id="' + ival + '"' + ']').prop("checked"));
                }
            }
        }
    }
}

function RoleAccessLoad(val) {
    if (val == -1) {
        CurrentRoleAccess.RoleId = 0;
    }
    else {
        CurrentRoleAccess.RoleId = val;
        rol = val;
    }

    $.ajax({
        type: 'POST',
        url: 'user_access.aspx/RoleAccessRetrieve',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data:JSON.stringify({ roleAccess: CurrentRoleAccess }),
        async: true,
        success: function (msg) {
            RoleAccess = $.extend(true, [], msg.d);
            if (RoleAccess.length > 0) {
                RoleAccessRetrieve(RoleAccess);
            }
        },
        error: function (msg) {
            alert("Failed loading Role Access.");
        }
    });
}

function RoleAccessRetrieve(RoleAccess) {

    $("#tblRoleAccess").bootstrapTable("destroy");

    $(RoleAccess).each(function (index, obj) {

        if (index == RoleAccess.length - 1) {
            var advancedColumnsVisibility = ($(document).width() > 992);

            var _columns = [
                {
                    field: "",
                    title: "#",
                    titleTooltip: "# ",
                    minWidth: "50px",
                    align: "center"
                },
                {
                    field: "MenuId",
                    title: "Menu Id ",
                    titleTooltip: "Menu Id"
                }, {
                    field: "MenuName",
                    title: "Menu Name",
                    titleTooltip: "Menu Name "
                },
               {
                   field: "ViewVisible",
                   title: "View",
                   titleTooltip: "View ",
                   checkbox: false
               },
               {
                   field: "SaveVisible",
                   title: "Save",
                   titleTooltip: "Save ",
                   checkbox: false
               },
               {
                   field: "DeleteVisible",
                   title: "Delete",
                   titleTooltip: "Delete ",
                   checkbox: false
               },
                {
                    field: "ReminderVisible",
                    title: "Reminder",
                    titleTooltip: "Reminder ",
                    checkbox: false
                },
               {
                   field: "ViewChecked",
                   title: "View",
                   titleTooltip: "View "
               },
               {
                   field: "SaveChecked",
                   title: "Save",
                   titleTooltip: "Save "
               },
               {
                   field: "DeleteChecked",
                   title: "Delete",
                   titleTooltip: "View ",
                   checkbox: false
               },
               {
                   field: "ReminderChecked",
                   title: "Reminder",
                   titleTooltip: "Reminder "
               }
            ];

            $("#tblRoleAccess").bootstrapTable({
                data: RoleAccess,
                striped: true,
                showColumns: false,
                showRefresh: false,
                showToggle: false,
                search: false,
                columns: _columns,
                detailView: false,
                onPostBody: AfterBindRoleAccess
            });
        }
    });
}

function AfterBindRoleAccess() {
    var tempId = '0';
    $("#tblRoleAccess").find("tr").each(function (index, element) {
        if (index == 0) {
            $('th[data-field^="MenuName"]')[index].style.textAlign = "center";
            if ($("#thView").length == 0)
                $(element).append("<th id='thView' style='text-align: center; '><label for='chkView' ><input type='checkbox' id='chkView' onclick='CheckAll(this.id)' />&nbsp;View</label></th>");
            if ($("#thSave").length == 0)
                $(element).append("<th id='thSave' style='text-align: center; '><label for='chkSave'><input type='checkbox' id='chkSave'  onclick='CheckAll(this.id)' />&nbsp;Save</label></th>");
            if ($("#thDelete").length == 0)
                $(element).append("<th id='thDelete' style='text-align: center; '><label for='chkDelete'><input type='checkbox' id='chkDelete' onclick='CheckAll(this.id)' />&nbsp;Delete</label></th>");
           tempId = '1';
        }
        else if (RoleAccess.length > 0) {

            var ViewVisible = "";
            var SaveVisible = "";
            var DeleteVisible = "";
  

            if ($(element).find("td:nth-of-type(4)").html().toString() == "true") {
                ViewVisible = "";
            }
            else {
                ViewVisible = "disabled style='display:none' ";
            } if ($(element).find("td:nth-of-type(5)").html().toString() == "true") {
                SaveVisible = "";
            }
            else {
                SaveVisible = "disabled style='display:none' ";
            } if ($(element).find("td:nth-of-type(6)").html().toString() == "true") {
                DeleteVisible = "";
            }
            else {
                DeleteVisible = "disabled style='display:none' ";
            } 

            var ViewChecked = "";
            var SaveChecked = "";
            var DeleteChecked = "";
          

            if (rol != -1) {
                if ($(element).find("td:nth-of-type(8)").html().toString() == "true") {
                    ViewChecked = "checked";
                }
                else {
                    ViewChecked = "";
                } if ($(element).find("td:nth-of-type(9)").html().toString() == "true") {
                    SaveChecked = "checked";
                }
                else {
                    SaveChecked = "";
                } if ($(element).find("td:nth-of-type(10)").html().toString() == "true") {
                    DeleteChecked = "checked";
                }
                else {
                    DeleteChecked = "";
                } 
            }

            tempId = '1';
            $(element).append(
                "<td style='min-width: 90px;' align='center'><label for='chkView" + index + "' style='display:none;' >test</label><input type='checkbox' id='chkView" + index + "'" + ViewVisible + ViewChecked + " onclick='Checkgroup(this.id,0)'/></td>"
            +
            "<td style='min-width: 90px;' align='center'><label for='chkSave" + index + "' style='display:none;' >test</label><input type='checkbox' id='chkSave" + index + "'" + SaveVisible + SaveChecked + " onclick='CheckView(this.id,1)'  /></td>"
             +
            "<td style='min-width: 90px;' align='center'><label for='chkDelete" + index + "' style='display:none;' >test</label><input type='checkbox' id='chkDelete" + index + "'" + DeleteVisible + DeleteChecked + " onclick='CheckView(this.id,2)' /></td>"
           );
        }
        if (tempId == '1') {
            document.getElementById("tblRoleAccess").rows[index].cells[0].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[1].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[3].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[4].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[5].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[6].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[7].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[8].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[9].style.display = "none";
            document.getElementById("tblRoleAccess").rows[index].cells[10].style.display = "none";
        }
    });
}

function RoleAccessRetrieval(id) {
    if ($('#' + id).val().trim() != "") {
        RoleAccessLoad($('#' + id).val());
    }
    else {
        rol = -1;
        RoleAccessLoad(rol);
    }
}

function ConfirmClear() {
                ClearRoleAccess();
  
}

function ClearRoleAccess() {
    
    $("#hdnRoleId").val("0");
    $("#sltRole").val("0");
    rol = -1;
    RoleAccessLoad(rol);
    setTimeout(function () { $("#sltRole").css("background-color", "#ffffff").focus() }, 500);
    location.reload();
}

function SaveRoleAccess() {
    CurrentRoleAccess.RoleId = $("#sltRole").val().trim();
    CurrentRoleAccess.RoleAccessData = '';

    if (CurrentRoleAccess.RoleId == "" || CurrentRoleAccess.RoleId == "0") {
        $("#sltRole").css("background-color", "#f6d6d5").focus();
        alert("Select any one option for the role");
        //alert("Select one role from the role.");
        return false;
    }

    var tbl = document.getElementById("tblRoleAccess");
    if (tbl != null) {
        var t = 0;
        for (var i = 1; i < tbl.rows.length; i++) {
            if (i == 1) {
                CurrentRoleAccess.RoleAccessData = tbl.rows[i].cells[1].innerText + '^';
            }
            else {
                CurrentRoleAccess.RoleAccessData = CurrentRoleAccess.RoleAccessData + tbl.rows[i].cells[1].innerText + '^';
            }
            for (var j = 0; j < tbl.rows[i].cells.length; j++) {
                var row = '';

                var view = 0;
                if (j == 11 || j == 12 || j == 13 || j == 14) {
                    if (j == 11) {
                        row = "chkView" + i
                    }
                    else if (j == 12) {
                        row = "chkSave" + i
                    }
                    else if (j == 13) {
                        row = "chkDelete" + i
                    }
                    

                    if ($('input[id="' + "chkView" + i + '"' + ']') && $('input[id="' + "chkView" + i + '"' + ']').prop("disabled") == false) {
                        if ($('input[id="' + "chkView" + i + '"' + ']').prop("checked") == true) {
                            view = 1;
                        }
                    }
                    if ($('input[id="' + "chkSave" + i + '"' + ']') && $('input[id="' + "chkSave" + i + '"' + ']').prop("disabled") == false && view == 0) {
                        if ($('input[id="' + "chkSave" + i + '"' + ']').prop("checked") == true) {
                            view = 1;
                        }
                    }
                    if ($('input[id="' + "chkDelete" + i + '"' + ']') && $('input[id="' + "chkSave" + i + '"' + ']').prop("disabled") == false && view == 0) {
                        if ($('input[id="' + "chkDelete" + i + '"' + ']').prop("checked") == true) {
                            view = 1;
                        }
                    }
                

                    if (j != 11) {
                        if ($('input[id="' + row + '"' + ']') && $('input[id="' + row + '"' + ']').prop("disabled") == false) {
                            if ($('input[id="' + row + '"' + ']').prop("checked") == true) {
                                CurrentRoleAccess.RoleAccessData = CurrentRoleAccess.RoleAccessData + 1 + '^';
                            }
                            else {
                                CurrentRoleAccess.RoleAccessData = CurrentRoleAccess.RoleAccessData + 0 + '^';
                            }
                        }
                        else {
                            CurrentRoleAccess.RoleAccessData = CurrentRoleAccess.RoleAccessData + 0 + '^';
                        }
                    }
                    else {
                        CurrentRoleAccess.RoleAccessData = CurrentRoleAccess.RoleAccessData + view + '^';
                    }
                }
            }
            CurrentRoleAccess.RoleAccessData = CurrentRoleAccess.RoleAccessData + '~'
        }
    }

    var allcookies = document.cookie;
    var cookiearray = allcookies.split(';');
    for (var i = 2; i < cookiearray.length;i++) {

        value = cookiearray[i].split('=')[1];
        CurrentRoleAccess.LogUserId = value
    }
    $.ajax({
        type: 'POST',
        url: 'user_access.aspx/RoleAccessSave',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ strRoleAccessData: CurrentRoleAccess }),
        async: true,
        success: function (msg) {
            if (msg.d == "1") {
                $("#sltRole").css("background-color", "#ffffff").focus();
                ClearRoleAccess();
                RoleAccessLoad(rol);
                
                alert("Access for the role is save successfully.");
            }
            else {
                $("#sltRole").css("background-color", "#ffffff").focus();
           
                alert("Error saving Role Access!");
            }
        },
        error: function (msg) {
            $("#sltRole").css("background-color", "#ffffff").focus();
            alert("Error saving Role Access!");
          
        }
    });
}

function CheckView(id, val) {
    var view = false;
    if ($('input[id="' + id + '"' + ']').prop("checked") == true) {

        if (val == 1) {
            view = id.replace('chkSave', 'chkView');
        }
        else if (val == 2) {
            view = id.replace('chkDelete', 'chkView');
        }    
        $('input[id="' + view + '"' + ']').prop("checked", $('input[id="' + id + '"' + ']').prop("checked"));
    }
}

