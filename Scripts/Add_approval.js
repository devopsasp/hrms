var CurrentForm = {};


$('#Timepic').datetimepicker({
        format: 'HH:mm'
    });


function save() {
    CurrentForm.FormId = $("#Form_name").val().trim();
    CurrentForm.FormName = $("#Form_name option:selected").text();
    CurrentForm.Lavel = $("#Hierarchy").val().trim();
    CurrentForm.Time = $("#Timepic").val().trim();
 

    if (CurrentForm.FormId == 0) {
        $("#Form_name").focus();
        alert("Select the Form Name.");
        return false;
    }

    if (CurrentForm.Level == 0) {
        $("#Hierarchy").focus();
        alert("Select the Approvals Hierarchy.");
        return false;
    }

    if (CurrentForm.Time == "") {   
        $("#Timepic").focus();
        alert("Select Down Time");
        return false;
    }

    var allcookies = document.cookie;
    var cookiearray = allcookies.split(';');
    for (var c = 0; c < cookiearray.length; c++) {
        if (cookiearray[c].split('=')[0] == " userid") {
            CurrentForm.Userid = cookiearray[c].split('=')[1];
        }
    }
    function clear() {
        CurrentForm.FormId = $("#Form_name").clear();
        CurrentForm.FormName = $("#Form_name option:selected").clear();
        CurrentForm.Lavel = $("#Hierarchy").clear();
        CurrentForm.Time = $("#Timepic").clear();
        return false;
    }
    $.ajax({
        type: "POST",
        dataType: 'Json',
        contentType: "application/json; charset=utf-8",
        url: 'Add_approval.aspx/FormSave',
        data: JSON.stringify({Form:CurrentForm}),
        success: function (msg) {
            if (msg == "1") {                         
                alert("Approvals saved successfully");
            }
            else (msg == "0")
            {
                alert("Approvals is already exists.");
            }
      
        },
        error: function (msg) {
           
            alert("Error saving book details!");

        }
    });

}
