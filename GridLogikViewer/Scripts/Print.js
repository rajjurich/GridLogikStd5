printDivCSS = new String('<link href="myprintstyle.css" rel="stylesheet" type="text/css">')
function printDiv(divId) {
    window.frames["print_frame"].document.body.innerHTML = printDivCSS + document.getElementById(divId).innerHTML;
    window.frames["print_frame"].window.focus();
    window.frames["print_frame"].window.print();
}