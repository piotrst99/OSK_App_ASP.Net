const selectRangeDates = document.getElementById('selectRangeDates');
const beginDateSelect = document.getElementById('beginDateSelect');
const endDateSelect = document.getElementById('endDateSelect');
const reportData = document.getElementById('reportData');

// https://artskydj.github.io/jsPDF/docs/index.html
// https://parall.ax/products/jspdf

selectRangeDates.addEventListener('click', () => {
    /*var doc = new jsPDF({
        orientation: 'p',
        format: 'a4',
        unit:'mm',
        putOnlyUsedFonts: true
    });*/

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Pracownik/GetEmployeeDataToReport',
        data: { id: $('#employeeID').val(), beginDate: beginDateSelect.value, endDate: endDateSelect.value },
        success: function (data) {

            console.log(data.result);
            refreshPracticalDetailsForReport(beginDateSelect.value, endDateSelect.value);

            let doc = new jsPDF();
            let date = new Date();
            let dateStr = date.getFullYear() + "-" + sprNumberData(date.getMonth() + 1) + "-" + sprNumberData(date.getDate()) + " " +
                sprNumberData(date.getHours()) + ":" + sprNumberData(date.getMinutes()) + ":" + sprNumberData(date.getSeconds());

            doc.setFontSize(30); doc.text(10, 20, 'Raport pracownika');
            doc.setFontSize(20); doc.text(10, 35, $('#employeePersonal').find('tr > td')[0].textContent + " " + $('#employeePersonal').find('tr > td')[2].textContent );
            doc.setFontSize(15); doc.text(10, 45, "Od " + beginDateSelect.value + " do " + endDateSelect.value);
            doc.setFontSize(20); doc.text(10, 65, 'Informacje o jazdach');
            doc.setFontSize(12); doc.text(10, 75, 'Liczba zrealizowanych jazd: ' + data.result[0]);
            doc.setFontSize(12); doc.text(10, 85, 'Liczba anulowanych jazd: ' + data.result[1]);
            doc.setFontSize(10); doc.text(10, 280, 'OSK App - Wydrukowano przez: ' + $('#userName').text() + " - " + dateStr );

            doc.save('Raport-' + $('#employeePersonal').find('tr > td')[0].textContent + " " + $('#employeePersonal').find('tr > td')[2].textContent +'.pdf');

            /*$.ajax({
                url: "/StaticFiles/EmployeeDetails/EmployeeWorkReport.html",
                type: 'GET',
                success: function (response) {
                    $('#reportData').html($($.parseHTML(response)).html());
                    $('#reportData').find('#dateRange').text("Od " + beginDateSelect.value + " do " + endDateSelect.value);
                    $('#reportData').find('#employeeName').text($('#employeePersonal').find('tr > td')[0].textContent + " " + $('#employeePersonal').find('tr > td')[2].textContent);
                    $('#countComplete').text('Liczba zrealizowanych jazd: '+data.result[0]);
                    $('#countUncomplete').text('Liczba anulowanych jazd: '+data.result[1]);
                    //reportData.style.display = 'block';
                    let source = $('#reportData').html();

                    let specialElementHandlers = {
                        '#reportData': function (element, renderer) {
                            return true;
                        }
                    };

                    doc.fromHTML(source, 15, 15, {
                        /*'width': 170,
                        //'elementHandlers': specialElementHandlers
                    //});

                    doc.fromHTML(reportData, 15, 15, {
                        'width': 170,
                        'elementHandlers': specialElementHandlers
                    });

                    //doc.save('sample-document.pdf');

                    $('#reportData').html('');

                }
            });*/


        }
    });

});

function sprNumberData(val) {
    if (val < 10) { return "0" + val; }
    else { return val; }
}