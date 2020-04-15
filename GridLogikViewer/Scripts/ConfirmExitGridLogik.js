////
//var formmodified = 0; //varibale to check whether form has been modified or not
//function confirmexitmsg(form, msgtext) {
//    this.init = function () {
//        var formname = form; //form tag object
//        $(form + ' *').change(function () {
//            formmodified = 1;
//        });

//        window.onbeforeunload = confirmExit;
//        function confirmExit() {
//            if (formmodified == 1) {
//                return msgtext; //message to be shown
//            }
//        }
//    }

//    this.init();
//}


//

//To change Base Address please modify below variable


//var BaseAddress = "http://localhost/EToolsAPI/api/"; // added on 17/12/2015
//
//var BaseAddress = "http://" + window.location.hostname + "/EToolsAPI/api/";
var BaseAddress = "http://" + window.location.hostname + "/GridLogik.API/api/";

//var DeleteMessage = "Record marked for Deletion. Continue? ";
var DeleteMessage = "Record selected for Deletion. Continue? ";
var envisageURL = "/GridLogik.Viewer";
var NoDataMessage = "No Data Found";


var formmodified = 0; //varibale to check whether form has been modified or not
function confirmexitmsg(form) {
    this.init = function () {
        var formname = form; //form tag object
        $('input:not(:button,:submit),' + form + ' *').keypress(function () {
            formmodified = 1;
        });

        window.onbeforeunload = confirmExit;
        function confirmExit() {
            if (formmodified == 1) {
                //var confirmationMessage = 'It looks like you have been editing something. '
                //           + 'If you leave before saving, your changes will be lost.';
                var confirmationMessage = 'Entered Data may not be saved, Are you sure you want to leave?';

                return confirmationMessage; //message to be shown
            }
        }
    }

    this.init();
}

function GetFinaldate(textdate, DBDateFormat, Dateformat) {

    var dateAr = textdate.split('/');
    var finalDate = textdate;
    if (DBDateFormat == "dd/mm/yy" && Dateformat == "mm/dd/yy") {
        finalDate = dateAr[1] + '-' + dateAr[0] + '-' + dateAr[2];
    }
    else if (DBDateFormat == "mm/dd/yy" && Dateformat == "dd/mm/yy") {
        finalDate = dateAr[1] + '-' + dateAr[0] + '-' + dateAr[2];
    }
    return finalDate;
}

function MinusOneDayDate(textdate, DBDateFormat, Dateformat) {
    var dateAr = textdate.split('/');
    var finalDate = textdate;
    if (DBDateFormat == "dd/mm/yy" && Dateformat == "mm/dd/yy") {
        finalDate = dateAr[1] + '-' + dateAr[0] + '-' + dateAr[2];
    }
    else if (DBDateFormat == "mm/dd/yy" && Dateformat == "dd/mm/yy") {
        finalDate = dateAr[1] + '-' + (parseInt(dateAr[0]) - 1).toString() + '-' + dateAr[2];
    }
    return finalDate;
}

function GetClientdate(textdate, DBDateFormat) {
    var finalDate = textdate;

    if (textdate != null && textdate != "") {
        var dateAr = textdate.split('-');

        if (DBDateFormat == "dd/mm/yy") {
            finalDate = dateAr[2] + '-' + dateAr[1] + '-' + dateAr[0];
        }
        else if (DBDateFormat == "mm/dd/yy") {
            finalDate = dateAr[1] + '-' + dateAr[2] + '-' + dateAr[0];
        }
    }
    return finalDate;
}

function ShowMessage(msgType, msg) {
    if (msgType == "Success") {
        $("#lblSuccessMsg").text(msg)
        $("#alert-success").slideDown();
        $("html, body").animate({ scrollTop: 0 }, "slow");

        window.setTimeout(function () {
            $(".alert-success").slideUp(500, function () {
                $(this).hide();
            });
        }, 4000);
    }
    else if (msgType == "Error") {
        $("#lblErrorMsg").text(msg)
        $("#alert-error").slideDown();
        $("html, body").animate({ scrollTop: 0 }, "slow");

        window.setTimeout(function () {
            $(".alert-error").slideUp(500, function () {
                $(this).hide();
            });
        }, 4000);
    }
}

$(document).on("keypress", ".number", function (e) {

    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {

        return false;
    }

});

$(document).on("keypress", ".decimal", function (evt) {
    // ;
    //if the letter is not decimal then don't type anything
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46 && charCode != 37)
        return false;
    else {
        var len = $(this).val().length;
        var index = $(this).val().indexOf('.');

        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var CharAfterdot = (len + 1) - index;
            if (CharAfterdot > 3) {
                return false;
            }
        }

    }
    return true;

});

$(document).on("keypress", ".alpha", function (e) {
    //  ;

    //if the letter is not alphabet or numeric then don't type anything
    var regex = new RegExp("^[a-zA-Z0-9]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    var charCode = (e.which) ? e.which : e.keyCode
    if ((charCode == 8) || charCode == 46 || charCode == 39 || charCode == 9) {
        return true;
    }
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;

});

$(document).on("keypress", ".alphaUnderScope", function (e) {
    //  ;

    //if the letter is not alphabet or numeric then don't type anything
    var regex = new RegExp("^[a-zA-Z0-9_]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    var charCode = (e.which) ? e.which : e.keyCode
    if ((charCode == 8) || charCode == 46 || charCode == 39 || charCode == 9) {
        return true;
    }
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;

});


$(document).on("keypress", ".alphaspace", function (e) {

    //  ;

    var charCode = (e.which) ? e.which : e.keyCode
    if ((charCode == 8) || charCode == 46 || charCode == 39 || charCode == 9) {
        return true;
    }
    //if the letter is not alphabet or whitespace then don't type anything
    if ($(this).val().length == 0 && e.charCode == 32) {
        e.preventDefault();
        return false;
    }


    var regex = new RegExp("[a-zA-Z ]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;

});

$(document).on("keypress", ".alphanumspace", function (e) {
    var charCode = (e.which) ? e.which : e.keyCode
    if ((charCode == 8) || charCode == 46 || charCode == 39 || charCode == 9) {
        return true;
    }
    //if the letter is not alphabet or whitespace then don't type anything
    if ($(this).val().length == 0 && e.charCode == 32) {
        e.preventDefault();
        return false;
    }


    var regex = new RegExp("^[a-zA-Z0-9. ]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;

});

$(document).on("keypress", ".alphanumspacecolon", function (e) {
    var charCode = (e.which) ? e.which : e.keyCode
    //if ((charCode == 8) || charCode == 46 || charCode == 37 || charCode == 39 || charCode == 9) {
    if ((charCode == 8)  || charCode == 39 || charCode == 9) {
        return true;
    }
    //if the letter is not alphabet or whitespace then don't type anything
    if ($(this).val().length == 0 && e.charCode == 32) {
        e.preventDefault();
        return false;
    }


    // var regex = new RegExp("^[a-zA-Z0-9_:.]+$");
    var regex = new RegExp("^[a-zA-Z0-9_]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;

});

$(document).on("keypress", ".alphanumspaceunderscore", function (e) {
    var charCode = (e.which) ? e.which : e.keyCode

    if ($(this).val().length == 0 && charCode == 32) {
        e.preventDefault();
        return false;
    }
    
    if ((charCode == 8) || charCode == 39 || charCode == 9 || charCode == 32) {
        return true;
    }
    //if the letter is not alphabet or whitespace then don't type anything
   


    // var regex = new RegExp("^[a-zA-Z0-9_:.]+$");
    var regex = new RegExp("^[a-zA-Z0-9_ ]+$");
    var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (regex.test(str)) {
        return true;
    }

    e.preventDefault();
    return false;

});
$(document).on("keypress", ".time", function (e) {
    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 58)) {

        return false;
    }

    if ($(this).val().length == 1) {
        $(this).val($(this).val().substr(0, 1) + String.fromCharCode(e.which).toString() + ':');
        return false;
    }

});

//added by Daniyal on 17/03/2016 to replace default alert with custom alert

//window.alert = function (message, redirecttourl, clickreset, msgtype) {

//    if (msgtype == 'E') {
//        $.alert({
//            //title: 'Alert!',
//            title: $("h2").text(),
//            content: message,
//            confirmButtonClass: 'btn-danger',
//            animation: 'left',
//            closeAnimation: 'right',
//            backgroundDismiss: false,
//            redirecturl: redirecttourl,
//            resetclick: clickreset,
//            icon: 'fa fa-times'

//        });
//    }
//    else if (msgtype == 'S') {

//        $.alert({
//            //title: 'Alert!',
//            title: $("h2").text(),
//            content: message,
//            confirmButtonClass: 'btn-success',
//            animation: 'left',
//            closeAnimation: 'right',
//            backgroundDismiss: false,
//            redirecturl: redirecttourl,
//            resetclick: clickreset,
//            icon: 'fa fa-check'


//        });
//    }
//    else if (msgtype == 'M') {

//        $.alert({
//            //title: 'Alert!',
//            title: $("h2").text(),
//            content: message,
//            confirmButtonClass: 'btn-info',
//            animation: 'left',
//            closeAnimation: 'right',
//            backgroundDismiss: false,
//            redirecturl: redirecttourl,
//            resetclick: clickreset,
//            icon: 'fa fa-info-circle'


//        });
//    }

//    return false;
//};

//window.confirm = function (message) {

//    $.confirm({
//        title: $("h2").text(),
//        content: message,
//        confirmButtonClass: 'btn-success',
//        cancelButtonClass: 'btn-danger',
//        confirmButton: 'Yes',
//        cancelButton: 'No',
//        animation: 'left',
//        closeAnimation: 'right',
//        backgroundDismiss: false,
//        icon: 'fa fa-exclamation-triangle',
//        confirm: function () {
//            this.close();
//            deleterecord();
//            return true;
//        },
//        cancel: function () {
//            this.close();
//            return false;
//        }
//    });
//}