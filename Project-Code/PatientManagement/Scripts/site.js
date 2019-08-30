//Function to validate message from.
function onchangeevent() {
    $("#noticeMsg").hide();
    var msgvalue = jQuery.trim($("#doctorMsg").val());
    if ((msgvalue.length == 0)) {
        $("#btnSubmit").attr("disabled", true);
        document.getElementById("errorMsg").innerHTML = "Please enter your message.";
    }
    else {
        $('#btnSubmit').attr("disabled", false);
        document.getElementById("errorMsg").innerHTML = " ";

    }
}

//Function to validate Appointment date from.

//Function to validate message from.
function CheckForm() {
    var treatment = jQuery.trim($("#treatment").val());
    var treatmentBill = jQuery.trim($("#treatmentBill").val());

    if ((treatment.length != 0 && treatmentBill.length != 0)) {
        $('#btnSubmit').attr("disabled", false);
        document.getElementById("errorMsg").innerHTML = " ";
    }
    else {
        $("#btnSubmit").attr("disabled", true);
        document.getElementById("errorMsg").innerHTML = "Please enter your message.";


    }
}

