$(function () {
    //$('input[id$=Link3]').click(function (evt) {
    $('input[id$=txtaddress1]').keyup(function (evt) {
        alert("Working");
        evt.preventDefault();
        $('input[id$=panelText]').slideToggle('slow');
    });
});

$(function () {
    $('input[id$=txtaddress12]').keyup(function () {
        alert("Workn");
        var txtClone = $(this).val();
        $('input[id$=txtaddress11]').val(txtClone);
    });
});

$('[id$=myButton]').click(function () { alert('button clicked'); });
