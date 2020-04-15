function ConvertYYYYMMDDHHmmss(dates) {
    var hours;
    var DateArr;
    var dFormat = $("#MainServerDate").val();
    dFormat = dFormat.ltrim();
    dFormat = dFormat.rtrim();
    dates = dates.ltrim();
    dates = dates.rtrim();

    var jsDate = new Array();
    jsDate[jsDate.length] = "2016";//yyyy 0
    jsDate[jsDate.length] = "01";//mm 1
    jsDate[jsDate.length] = "01";//dd 2
    jsDate[jsDate.length] = "00";//hh 3
    jsDate[jsDate.length] = "00";//mm 4
    jsDate[jsDate.length] = "00";//ss 5


    var date = dates.split(' ')[0];
    var dateFormat = dFormat.split(' ')[0];
    if (dates.split(' ').length >= 1) {
        hours = dates.split(' ')[1];
    } else {
        hours = "00:00:00";
    }
    if (dateFormat == "")
        dateFormat = "mm/dd/yy";

    if (date.indexOf("/") > 0) {
        while (date.indexOf("/") > 0) {
            date = date.replace("/", "-");
        }
    }
    if (dateFormat.indexOf("/") > 0) {
        while (dateFormat.indexOf("/") > 0) {
            dateFormat = dateFormat.replace("/", "-");
        }
    }

    if (dateFormat.toUpperCase() == "DD-MM-YY" || dateFormat.toUpperCase() == "DD-MM-YYYY") {
        DateArr = date.split('-');
        jsDate[0] = DateArr[2];//yyyy 0
        jsDate[1] = parseInt(DateArr[1]) - 1;//mm 1
        jsDate[2] = DateArr[0];//dd 2
    } else if (dateFormat.toUpperCase() == "MM-DD-YY" || dateFormat.toUpperCase() == "MM-DD-YYYY") {
        DateArr = date.split('-');
        jsDate[0] = DateArr[2];//yyyy 0
        jsDate[1] = parseInt(DateArr[0]) - 1;//mm 1
        jsDate[2] = DateArr[1];//dd 2
    } else if (dateFormat.toUpperCase() == "YY-MM-DD" || dateFormat.toUpperCase() == "YYYY-MM-DD") {
        DateArr = date.split('-');
        jsDate[0] = DateArr[0];//yyyy 0
        jsDate[1] = parseInt(DateArr[1]) - 1;//mm 1
        jsDate[2] = DateArr[2];//dd 2
    }
    if (hours.indexOf(":") > 0) {
        while (hours.indexOf(":") > 0) {
            hours = hours.replace(":", "-");
        }
    }
    var hhmmss = hours.split('-');
    if ((dates.split(' ')[2] == 'am' || dates.split(' ')[2] == 'AM') && hhmmss[0] == 12) {
        hhmmss[0] = "00";
    }
    if (hhmmss.length == 4 || hhmmss.length == 3) {
        jsDate[3] = hhmmss[0];//hh 3
        jsDate[4] = hhmmss[1];//mm 4
        jsDate[5] = hhmmss[2];//ss 5
    } else if (hhmmss.length == 2) {
        jsDate[3] = hhmmss[0];//hh 3
        jsDate[4] = hhmmss[1];//mm 4
        jsDate[5] = "00";//ss 5
    } if (hhmmss.length == 1) {
        jsDate[3] = hhmmss[0];//hh 3
        jsDate[4] = "00";//mm 4
        jsDate[5] = "00";//ss 5
    }
    return jsDate;
}
String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}