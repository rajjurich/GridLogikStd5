$(document).ready(function () {
    var seriesArr = [];
    var addedHeaders = [];
    var tblid = $("#diaTblName").val();
    var tblname = "#" + tblid;
    if (tblid != null) {
        $(tblname).dataTable(
                      {
                          pageLength: 10,
                          "scrollX": true
                      });
        var valueArr = [];
        var index = [];
        var sortedData = [];
        $(document).mousedown(function () {
            $(tblname + " td").removeClass("highlighted");
            $("#average").text('Avg');
            $('#Maximum').text('Max');
            $('#Minimum').text('Min');
            $("#Sum").text('Sum');
            seriesArr = [];
            valueArr = [];
            index = [];
        });
        var isMouseDown = false, isHighlighted;
        var a = 0;
        var current = 0;
        var previous = 0;
        var counter = 1;

        $(document).on('mousedown', tblname + ' td', function () {

            $(tblname + " td").removeClass("highlighted");
            isMouseDown = true;
            if (currentIndex > 1)
                $(this).addClass("highlighted");
            if (currentIndex > 1) {
                if (!isNaN(Number($(this).text()))) {
                    index.push($(this).index());
                    //var data = { name: $(this).index(), data: valueArr }
                    sortedData[$(this).index()] = valueArr;
                    a = Number(a) + Number($(this).text());
                    $('#Minimum,#Maximum,#Sum,#average').text(a);
                    var $th = $(this).closest('table').find('th').eq($(this).index());
                    valueArr.push($th[0].textContent.trim() + '$' + Number($(this).text()));
                }
            }
            isHighlighted = $(this).hasClass("highlighted");
            return false;
        })
        .on('mouseover', tblname + ' td', function () {
            currentIndex = $(this).index();
            if (isMouseDown) {

                if (!$(this).hasClass("highlighted") && currentIndex > 1) {

                    if (!isNaN(Number($(this).text()))) {
                        if ($.inArray($(this).index(), index) != 0) {
                            index.push($(this).index());
                        }
                        sortedData[$(this).index()] = valueArr;
                        a = Number(a) + Number($(this).text());
                        var $th = $(this).closest('table').find('th').eq($(this).index());
                        valueArr.push($th[0].textContent.trim() + '$' + Number($(this).text()));

                        if (Number($('#Minimum').text()) < Number($(this).text())) {
                            if (Number($('#Maximum').text()) < Number($(this).text())) {
                                $('#Maximum').text(Number($(this).text()));
                            }
                        }
                        else {
                            $('#Minimum').text(Number($(this).text()));
                        }
                    }
                    else {
                        valueArr.push(Number(0));
                    }
                    counter = counter + 1;
                    $("#Sum").text(Number(a).toFixed(2));
                    $("#average").text((a / counter).toFixed(2));
                }
                if (currentIndex > 1)
                    $(this).toggleClass("highlighted", isHighlighted);
            }
        });

        $(document)
          .mouseup(function () {
              isMouseDown = false;
              a = 0;
              counter = 1;
              index = [];
          });
        //new code end

        $(tblname + ' thead th').each(function (index) {
            $(this).click(function () {
                SelectColumn(index, tblid);
            });
        });
        $("#graph").mousedown(function () {
            var valuesArrs = [];
            var SelcolDates = [];
            var columnList = [];
            var SelcolumnList = [];
            var TableRow = $('table' + tblname).find('tbody').find('tr');
            $(tblname + ' thead th').each(function (index) {
                columnList.push($(this).text().trim());
            });

            for (var i = 0; i < TableRow.length; i++) {
                var Tdcount = 0;
                for (var j = 0; j < TableRow[i].cells.length; j++) {
                    var subtd = TableRow[i].cells[j].className;
                    if (subtd.includes("highlighted")) {
                        var colName = columnList[j];
                        SelcolumnList.push(colName);
                        var Vl = TableRow[i].cells[j].innerText;
                        valuesArrs.push(colName + "$" + Vl);
                        if (Tdcount == 0) {
                            var dat = TableRow[i].cells[0].innerText;
                            SelcolDates.push(dat);
                        }
                        Tdcount++;
                    }
                }
            }
            var values = [];
            $.each(valuesArrs, function (key, value) {
                if (value != 0) {
                    var dataArray = value.split("$");
                    var seriesOptions2 = [];
                    seriesOptions2.push(dataArray[dataArray.length - 1]);
                    if (values.length > 0) {
                        $.each(values, function (key1, value1) {
                            if ((values[key1].name != dataArray[0]) && ($.inArray(dataArray[0], addedHeaders) == -1)) {
                                var series = { name: dataArray[0], data: seriesOptions2 }
                                values.push(series);
                                addedHeaders.push(dataArray[0]);
                            }
                            else if (values[key1].name == dataArray[0]) {
                                values[key1].data.push(dataArray[dataArray.length - 1]);
                            }
                        });
                    }
                    else {
                        var series = { name: dataArray[0], data: seriesOptions2 }
                        values.push(series);
                    }
                }
            });

            addedHeaders = [];
            var uniquecolumnHeader = SelcolumnList.filter(function (itm, i, SelcolumnList) {
                return i == SelcolumnList.indexOf(itm);
            });

            var sortedCol = $(tblname).dataTable().fnSettings().aaSorting[0][0];
            var sortedDir = $(tblname).dataTable().fnSettings().aaSorting[0][1];
            var str = $(tblname).dataTable().fnSettings().aoColumns[sortedCol].sTitle;
            if (str.toLowerCase().indexOf("date") >= 0 && sortedDir.toLowerCase() == 'desc') {
                SelcolDates = SelcolDates.reverse();//change for date desc
            }

            if (uniquecolumnHeader.length > 0) {
                box = new ajaxLoader(this, { classOveride: 'blue-loader', bgColor: '#000' });
                for (var ser = 0; ser < uniquecolumnHeader.length; ser++) {
                    var seriesOptions2 = values[ser].data.map(Number);    ////convert String Array into Number Array
                    if (str.toLowerCase().indexOf("date") >= 0 && sortedDir.toLowerCase() == 'desc') {
                        seriesOptions2 = seriesOptions2.reverse();  //change for date desc
                    }
                     var dataArr = [];
                    for (var hdr = 0; hdr < seriesOptions2.length; hdr++) {
                        var dts = ConvertYYYYMMDDHHmmss(SelcolDates[hdr]);
                        var data = [Date.UTC(dts[0], dts[1], dts[2], dts[3], dts[4], dts[5]), seriesOptions2[hdr]];
                        dataArr.push(data);
                    }
                    var series = { name: uniquecolumnHeader[ser], data: dataArr }; //Create Series Array with name and data property
                    seriesArr.push(series);
                }
                lineChart(SelcolDates, seriesArr);
                seriesArr = [];
                valueArr = [];
                index = [];
                valuesArrs = [];
                SelcolDates = [];
                columnList = [];
                SelcolumnList = [];
                $("#chart_dialog").dialog('open');
                if (box) box.remove();
            }
            else {
                alert("Please select atleast one value", '', false, 'E')
            }
        });
        
    }
});
function lineChart(dateArr, seriesArr) {
    var chart = new Highcharts.Chart({
        chart: {
            type: 'spline',
            zoomType: 'x',
            renderTo: 'chart_container',
            spacingLeft: 0,
            marginRight:120,
	    height: 550,
	    spacingBottom: 3,
            events: {
                load: function () {
                    $(window).resize();
                }
            }
        },
        title: {
            text: '',
            x: -20 //center
        },
        subtitle: {
            text: '',
            x: -20
        },
        xAxis: {
            type: 'datetime',
            title: {
                text: 'Date'
            }
        },
        yAxis: {
            title: {
                text: 'Value'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            valueSuffix: 'value'
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: seriesArr
    });
}
  