//Function to validate message from.
function onchangeevent() {
    $("#noticeMsg").hide();
    var msgvalue = $('#doctorMsg').val();
    if (msgvalue == '') {
        $("#btnSubmit").attr("disabled", true);
        document.getElementById("errorMsg").innerHTML = "Please enter your message.";
    }
    else {
        $('#btnSubmit').attr("disabled", false);
        document.getElementById("errorMsg").innerHTML = " ";

    }
}