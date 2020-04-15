

//Required Field Validation

$.validator.unobtrusive.adapters.addSingleVal("requiredcustom", "otherpropertyname");
$.validator.unobtrusive.adapters.add("requiredcustom", ["otherpropertyname"], function (options) {
    options.rules["requiredcustom"] = "#" + options.params.otherpropertyname;
    options.messages["requiredcustom"] = options.message;
});


//Minimum value required
$.validator.unobtrusive.adapters.addSingleVal("custminlength", "minlengthvalue");
jQuery.validator.addMethod('custminlength', function (value, element, params) {
    var minval = params.minlengthvalue;
    if (value.length > 0) {
        if (value.length < parseInt(minval)) {
            return false;
        }
        return true;
    }
    else
        return true;
}, '');

jQuery.validator.unobtrusive.adapters.add('custminlength', ["minlengthvalue"], function (options) {

    options.rules['custminlength'] = options.params;
    options.messages['custminlength'] = options.message;
});

//Maximum value required
$.validator.unobtrusive.adapters.addSingleVal("custmaxlength", "maxlengthvalue");
jQuery.validator.addMethod('custmaxlength', function (value, element, params) {
    var maxval = params.maxlengthvalue;
   
    var length = params.maxlengthvalue;
    $("#" + element.id).prop("maxlength", parseInt(maxval));
    if (value.length > 0) {
        if (value.length > parseInt(maxval)) {
            return false;
        }
        else {
            return true;
        }
    }
    else
        return true;


}, '');

jQuery.validator.unobtrusive.adapters.add('custmaxlength', ["maxlengthvalue"], function (options) {

    options.rules['custmaxlength'] = options.params;
    //options.messages['custmaxlength'] = options.message;
});


//Regular Expression Validation
$.validator.unobtrusive.adapters.addSingleVal("regexp", "regextype");
jQuery.validator.addMethod('regexp', function (value, element, params) {
    var type = params.regextype;
  
    if (true) {
        var regex = new RegExp(type.replace("(%body%)", "\'"));    
        return this.optional(element) || regex.test(value);
    }

    //Alphabets
    //var regex = /^[a-zA-Z]*$/    
    //Digits
    //var regex = /^[0-9]+$/;
    //Alphanumeric
    //var regex = /^[a-zA-Z][a-zA-Z0-9]+$/;
  
    return true;

}, '');

jQuery.validator.unobtrusive.adapters.add('regexp', ["regextype"], function (options) {

    options.rules['regexp'] = options.params;
    options.messages['regexp'] = options.message;
});

//Check Padding 

$.validator.unobtrusive.adapters.addSingleVal("pad", "maxlengthvalue");
$.validator.unobtrusive.adapters.addSingleVal("pad", "ispaddingrequired");
jQuery.validator.addMethod('pad', function (value, element, params) {
  
    var Elevalue = $("#" + element.id).val();
    var maxval = params.maxlengthvalue;
    if (Elevalue.length > 0) {
        if (Elevalue.length < parseInt(maxval)) {

            if (params.ispaddingrequired == "Y") {

                //Pad value start
                $("#" + element.id).blur(function () {
                    var finalval = $("#" + element.id).val();
                    $("#" + element.id).val(finalval.lpad("0", parseInt(maxval)));
                    return true;
                });
                //Pad value end
                return true;
            }
            else
                return true;
            
        }
        else
            return true;
    }
    else
        return true;
}, '');

jQuery.validator.unobtrusive.adapters.add('pad', ["ispaddingrequired", "maxlengthvalue"], function (options) {
  
    options.rules['pad'] = options.params;
    //options.messages['pad'] = options.message;
});


//Left Pad function
    String.prototype.lpad = function (padString, length) {
            var str = this;
            while (str.length < length)
                str = padString + str;
            return str;
    }

//DateGreater than check
    //$.validator.addMethod("dategreaterthan", function (value, element, params) {
    //    

    //    return Date.parse(value) > Date.parse((params.otherpropertyname).val());
//});

    $.validator.unobtrusive.adapters.addSingleVal("dategreaterthan", "otherpropertyname");
    $.validator.unobtrusive.adapters.addSingleVal("dategreaterthan", "dateformat");
    jQuery.validator.addMethod('dategreaterthan', function (value, element, params) {
       // 
        var ToDate = value;
        var FromDate = $("#"+params.otherpropertyname).val();
        var format = params.dateformat.trim().toUpperCase();
        var ddFrom, mmFrom, yyFrom, ddTo, mmTo, yyTo = "";
       
        if (format == "DD/MM/YY")
        {
            ddFrom = FromDate.substr(0, 2);
            mmFrom = FromDate.substr(3, 2);
            yyFrom = FromDate.substr(6, 4);
            ddTo = ToDate.substr(0, 2);
            mmTo = ToDate.substr(3, 2);
            yyTo = ToDate.substr(6, 4);
        }
        else if (format == "MM/DD/YY")
        {
            mmFrom = FromDate.substr(0, 2);
            ddFrom = FromDate.substr(3, 2);
            yyFrom = FromDate.substr(6, 4);
            mmTo = ToDate.substr(0, 2);
            ddTo = ToDate.substr(3, 2);
            yyTo = ToDate.substr(6, 4);
        }

        var StartDate = new Date(yyFrom, mmFrom - 1, ddFrom); //   yy,month,dd
        var EndDate = new Date(yyTo, mmTo - 1, ddTo);
     
        if (StartDate > EndDate) {
            return false;
        }
        return true;

    }, '');

    $.validator.unobtrusive.adapters.add("dategreaterthan", ["otherpropertyname", "dateformat"], function (options) {
    
        options.rules["dategreaterthan"] = options.params;
        //options.rules["dategreaterthan"] = "#" + options.params.otherpropertyname;
        options.messages["dategreaterthan"] = options.message;
    });

  