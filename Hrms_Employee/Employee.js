$(document).ready(function () {
    $("[id$=txt_bankname]").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: 'Employee_Profile.aspx/bank_auto',
                type: "POST",
                dataType: "Json",
                contentType: "application/json; charset=utf-8",
                data: "{ 'bank': '" + request.term + "'}",
                success: function (data) {
                    if (data.d.length > 0) {
                        response($.map(data.d, function (item) {
                            return {
                                val: item.split('/')[0],
                                label: item.split('/')[1]
                            }
                        }));
                    }
                    else {
                        response([{ label: 'No Records Found', val: -1 }]);
                    }
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        change: function (e, ui) {
            if (!(ui.item)) e.target.value = "";
        },
        select: function (e, i) {
            $("[id$=txt_bankname]").val(i.item.label);
            bankselect(i.item.val);
            return false;
        },
        minLength: 0
    });
});

function bankselect(id) {
    $.ajax({
        url: 'Employee_Profile.aspx/bankselect',
        type: "POST",
        dataType: "Json",
        contentType: "application/json; charset=utf-8",
        data: "{ 'bankid': '" + id + "'}",
        success: function (data)
        {       
            if (data.d.length > 0) 
            {
                var item = data.d[0].split('/');
                   $("[id$=txt_bankcode]").val(item[0]);
                   $("[id$=txt_bankname]").val(item[1]);
                    $("[id$=txt_branchname]").val(item[2]);
                    $("[id$=txt_actype]").val(item[3]);
                    $("[id$=txt_micrcode]").val(item[4]);
                    $("[id$=txt_ifsccode]").val(item[5]);
                    $("[id$=txt_address]").val(item[6]);
                    $("[id$=txt_otherinfo]").val(item[7]);
           }
        }
    });
}