const selectCallendar = document.getElementById('selectCallendar');
const practicalTableOfDay = document.getElementById('practicalTableOfDay');
const practicalOnDay = document.getElementById('practicalOnDay');
const practicalDataModal = document.getElementById('practicalDataModal');
const addPracticalModal = document.getElementById('addPracticalModal');
const practicalStatus = document.getElementById('practicalStatus');

let monthAndYear = document.getElementById("monthAndYear");

let months = ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
    "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"];

var tabHours = [['07:00-09:00', 'godz07'], ['09:00-11:00', 'godz09'], ['11:00-13:00', 'godz11'],
    ['13:00-15:00', 'godz13'], ['15:00-17:00', 'godz15']];

var today = new Date();
var selectDate = new Date(today);

window.onload = function(){    
    createDiv2(today);
    //createPracticalTableOfDay();

    if (practicalStatus.value != 2) {
        document.getElementById('course').querySelector('input').disabled = true;
        document.getElementById('course').querySelector('input').value = 0;
        document.getElementById('endTime').querySelector('input').disabled = true;
        document.getElementById('endTime').querySelector('input').value = '';
    }
}

window.onclick = function (event) {
    if (event.target == practicalDataModal) {
        practicalDataModal.style.display = 'none';
    }
    if (event.target == addPracticalModal) {
        addPracticalModal.style.display = 'none';
    }
}

function previous() {
    var newDate = new Date(selectDate.getFullYear(), (selectDate.getMonth()), selectDate.getDate() - 1);
    selectDate = newDate;
    createDiv2(newDate);
}

function now() {
    selectDate = today;
    createDiv2(today);
}

function next() {
    var newDate = new Date(selectDate.getFullYear(), (selectDate.getMonth()), selectDate.getDate()+1);
    selectDate = newDate;
    createDiv2(newDate);
}


selectCallendar.addEventListener('change', () => {
    var selectDateFromCallendar = new Date(selectCallendar.value);
    selectDate = selectDateFromCallendar;

    createDiv2(selectDateFromCallendar);
});


function createDiv2(date) {

    practicalOnDay.innerHTML = '';
    createPractical();

    var year = date.getFullYear();
    var month = date.getMonth()
    var day = date.getDate();
    var dayOfWeek = date.getDay();

    monthAndYear.innerHTML = day + " " + months[month] + " " + year;
    dayOfWeek == 0 ? monthAndYear.style.color = 'red' : monthAndYear.style.color = 'black';

    //document.getElementById("test123").innerHTML = '';
    var wybData = year + "-" + sprNumberData(month+1) + "-" + sprNumberData(day);
    selectDay = day;

    var grupa1 = [];
    var grupa2 = [];
    var grupa3 = [];
    var grupa4 = [];
    var grupa5 = [];

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/GetZajeciaPraktyczne4',
        data: { wybranaData: wybData },
        success: function (data) {
            var d = data.wynik;

            for (var k = 0; k < d.length; k = k + 1) {

                var numer = d[k].startTime.substring(0, 2);

                if (numer == '07') { grupa1.push(d[k]); }
                else if (numer == '09') { grupa2.push(d[k]); }
                else if (numer == '11') { grupa3.push(d[k]); }
                else if (numer == '13') { grupa4.push(d[k]); }
                else if (numer == '15') { grupa5.push(d[k]); }

            }

            $.ajax({
                url: "/StaticFiles/harmonogram.html",
                success: function (response) {
                    $('#test123').html($($.parseHTML(response)).html());

                    /*setData('07', 0, grupa1);
                    setData('09', 6, grupa2);
                    setData('11', 12, grupa3);
                    setData('13', 18, grupa4);
                    setData('15', 24, grupa5);*/

                    setData2(tabHours[0][1], 0, grupa1);
                    setData2(tabHours[1][1], 1, grupa2);
                    setData2(tabHours[2][1], 2, grupa3);
                    setData2(tabHours[3][1], 3, grupa4);
                    setData2(tabHours[4][1], 4, grupa5);

                    /*$('.addZajPrakBtn').click(loadAddZajPrak);

                    if (grupa1.length >= 6) { $('#07').prop('disabled', 'disabled'); }
                    if (grupa2.length >= 6) { $('#09').prop('disabled', 'disabled'); }
                    if (grupa3.length >= 6) { $('#11').prop('disabled', 'disabled'); }
                    if (grupa4.length >= 6) { $('#13').prop('disabled', 'disabled'); }
                    if (grupa5.length >= 6) { $('#15').prop('disabled', 'disabled'); }*/

                    //$('#backCalendarBtn').click(backToCalendar);

                }
            });


        }


    });


}

function sprNumberData(val) {
    if (val < 10) { return "0" + val; }
    else { return val; }
}

function setData2(nr, val, dane) {
    //for (var i = 0; i < dane.length; i++) {
    for (var i = 0; i < 6; i++) {

        if (i < dane.length) {
            $('#practicalOnDay').find('#' + nr).find('#zaj' + ((val * 6) + i + 1)).html(dane[i].student.user.firstName + " " + dane[i].student.user.surname);
            $('#practicalOnDay').find('#' + nr).find('#zaj' + ((val * 6) + i + 1)).attr('zp', dane[i].id);

            //document.getElementById('#zaj' + ((val * 6) + i + 1)).removeEventListener('click');
            //console.log(document.getElementById('#zaj' + ((val * 6) + i + 1)));//.removeEventListener('click');


            //$('#zaj' + ((val * 6) + i + 1)).click(function () { return false;});
            //$('#zaj' + ((val * 6) + i + 1)).unbind();

            $('#zaj' + ((val*6) + i + 1)).click(function () {
                $('#szczegolyZaj').attr('zp', $(this).attr('zp'));

                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/Zajecia/GetZajeciaPraktyczne5',
                    data: { idZajec: $(this).attr('zp') },
                    success: function (data) {
                        var d = data.wynik;

                        //console.log(d);


                        $('#PracticID').text(d.id);
                        $('#date').text(d.data);
                        $('#startTime').text(d.startTime);
                        $('#student').text(d.student.user.firstName + " " + d.student.user.surname);
                        $('#employee').text(d.employee.user.firstName + " " + d.employee.user.surname);
                        $('#course > :input').val(d.course);
                        practicalDataModal.style.display = 'block';

                        //$('#editBtn').click(loadEditContent);
                    }

                });

            });

        }
        else {
            $('#zaj' + ((val * 6) + i + 1)).click(function () {

                $('#plannedDate').text(selectDate.getFullYear() + '-' + sprNumberData(selectDate.getMonth() + 1) + '-' +
                    sprNumberData(selectDate.getDate()));
                //console.log(i);
                $('#plannedStartTime').text(tabHours[val][0].substring(0,5));

                $.ajax({
                    type: 'POST',
                    dataType: 'JSON',
                    url: '/Zajecia/GetOsobyZajeciaPraktyczne',
                    data: {},
                    success: function (data) {
                        var kursanci = data.listaKursantow;
                        var instruktorzy = data.listaInstruktorow;

                        $('#studentSelect').empty();
                        $('#employeeSelect').empty();

                        for (var i = 0; i < kursanci.length; i++) {
                            var option = document.createElement('option');
                            option.value = kursanci[i].userID;
                            option.innerHTML = kursanci[i].user.firstName + " " + kursanci[i].user.surname;
                            $('#studentSelect').append(option);
                        }

                        for (var i = 0; i < instruktorzy.length; i++) {
                            var option = document.createElement('option');
                            option.value = instruktorzy[i].userID;
                            option.innerHTML = instruktorzy[i].user.firstName + " " + instruktorzy[i].user.surname;
                            $('#employeeSelect').append(option);
                        }


                    }
                });

                addPracticalModal.style.display = 'block';

            });
        }


    }
}

addPracticalDataBtn.addEventListener('click', () => {
    
    var wybData = $('#plannedDate').text();
    var godzina = $('#plannedStartTime').text();
    var kursantId = $('#studentSelect').val();
    var instruktorId = $('#employeeSelect').val();

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/setZeciaPraktyczne',
        data: { data: wybData, kursantID: kursantId, instruktorID: instruktorId, godzStart: godzina },
        success: function (data) {
            if (data.czyDodano) {
                //createDiv2(selectDay, (selectMonth), selectYear);
                createDiv2(selectDate);
                $('#szczegolyZaj').html('');
            }
            else {
                console.log(data.t);
            }
        }
    });

    addPracticalModal.style.display = 'none';

});

removePracticalDataBtn.addEventListener('click', () => {

    usunZajeciaPraktyczne($('#PracticID').text());
    practicalDataModal.style.display = 'none';

    //console.log($('#PracticID').text());
        
    /*$.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/deleteZajeciaPraktyczne',
        data: { idZajPrak: id },
        success: function (data) {
            if (data.czyUsunieto) {
                //createDiv2(selectDay, (selectMonth), selectYear);
                createDiv2(selectDate);
                $('#szczegolyZaj').html('');
            }
        }
    });*/
});

practicalStatus.addEventListener('change', function () {
    if (practicalStatus.value != 2) {
        document.getElementById('course').querySelector('input').disabled = true;
        document.getElementById('course').querySelector('input').value = 0;
        document.getElementById('endTime').querySelector('input').disabled = true;
        document.getElementById('endTime').querySelector('input').value = '';
    }
    else {
        document.getElementById('course').querySelector('input').disabled = false;
        document.getElementById('endTime').querySelector('input').disabled = false;
    }
});

document.getElementsByClassName('close')[0].addEventListener('click', () => {
    addPracticalModal.style.display = 'none';
});

document.getElementsByClassName('close')[1].addEventListener('click', () => {
    practicalDataModal.style.display = 'none';
});

function loadAddZajPrak() {
    //console.log($(this).attr('id'));

    var godz = $(this).attr('id');

    $.ajax({
        url: "/StaticFiles/edytujZajeciaPraktyczne.html",
        success: function (response) {
            $('#szczegolyZaj').html($($.parseHTML(response)).filter('#add').html());

            $('#godzZaj').html(godz);

            $('#addBtn').click(function () {
                dodajZajeciePraktyczne();
            });

        }
    });

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/GetOsobyZajeciaPraktyczne',
        data: {},
        success: function (data) {
            var kursanci = data.listaKursantow;
            var instruktorzy = data.listaInstruktorow;

            for (var i = 0; i < kursanci.length; i++) {
                var option = document.createElement('option');
                option.value = kursanci[i].userID;
                option.innerHTML = kursanci[i].user.firstName + " " + kursanci[i].user.surname;
                $('#kursantTxt').append(option);
            }

            for (var i = 0; i < instruktorzy.length; i++) {
                var option = document.createElement('option');
                option.value = instruktorzy[i].userID;
                option.innerHTML = instruktorzy[i].user.firstName + " " + instruktorzy[i].user.surname;
                $('#instruktorTxt').append(option);
            }


        }
    });

}



function loadEditContent() {
    var kur = document.getElementById('kur').innerHTML;
    var ins = document.getElementById('ins').innerHTML;

    $.ajax({
        url: "/StaticFiles/edytujZajeciaPraktyczne.html",
        success: function (response) {
            $('#szczegolyZaj').html($($.parseHTML(response)).filter('#edit').html());
            document.getElementById('kursantTxt').value = kur;
            document.getElementById('instruktorTxt').value = ins;

            $('#deleteBtn').click(function () {
                usunZajeciaPraktyczne($('#szczegolyZaj').attr('zp'));
            });
        }
    });

}

function dodajZajeciePraktyczne() {
    /*var date = new Date(selectCallendar.value);

    var selectYear = selectDate.getFullYear();
    var selectMonth = selectDate.getMonth() + 1;
    var selectDay = selectDate.getDate();*/

    var wybData = selectDate.getFullYear() + "-" + sprNumberData(selectDate.getMonth()+1) + "-" + sprNumberData(selectDate.getDate());
    var godzina = $('#godzZaj').text() + ":00";
    var kursantId = $('#kursantTxt').val();
    var instruktorId = $('#instruktorTxt').val();
    //alert(wybData);
    //console.log(wybData + " " + godzina + " " + kursantId + " " + instruktorId);

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/setZeciaPraktyczne',
        data: { data: wybData, kursantID: kursantId, instruktorID: instruktorId, godzStart: godzina },
        success: function (data) {
            if (data.czyDodano) {
                //createDiv2(selectDay, (selectMonth), selectYear);
                createDiv2(selectDate);
                $('#szczegolyZaj').html('');
            }
            else {
                console.log(data.t);
            }
        }
    });

}

function usunZajeciaPraktyczne(id) {

    /*var date = new Date(selectCallendar.value);

    var selectYear = date.getFullYear();
    var selectMonth = date.getMonth() + 1;
    var selectDay = date.getDate();*/

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/deleteZajeciaPraktyczne',
        data: { idZajPrak: id },
        success: function (data) {
            if (data.czyUsunieto) {
                //createDiv2(selectDay, (selectMonth), selectYear);
                createDiv2(selectDate);
                $('#szczegolyZaj').html('');
            }
        }
    });
}


function createPracticalTableOfDay() {
    var practicalTable = document.createElement('table');

    for (let i = 0; i < 5; i++) {
        var tr = document.createElement('tr');

        for (let j = 0; j < 7; j++) {
            var td = document.createElement('td');
            td.innerHTML = 'test';
            tr.appendChild(td);

            //td.addEventListener('click', () => { alert('clicked') });
            if (j != 0) {
                //td.className = 'itemTable';
                td.classList.add('itemTable');
                td.style.backgroundColor = '#71aab0';
                td.style.cursor = 'pointer';
            }
            else {
                td.classList.add('itemTable2');
            }
        }

        practicalTable.appendChild(tr);
    }

    practicalTable.className = 'col-sm-12 table table-light';
    

    practicalTableOfDay.appendChild(practicalTable);
}


function createPractical() {
    practicalOnDay.classList.add('container');
    practicalOnDay.style.overflow = 'auto';

    for (let i = 0; i < 5; i++) {
        var divRow = document.createElement('div');
        divRow.classList.add('row');
        divRow.id = tabHours[i][1];

        for (let j = 0; j < 7; j++) {
            var divCol = document.createElement('div');
            if (j != 0) {
                divCol.classList.add('col');
                if (i != 0) {
                    divCol.innerHTML = '+';
                    //divCol.innerHTML = (j + i * 6);
                    //divCol.setAttribute('zaj', (j + i * 6));
                    divCol.id = 'zaj' + (j + i * 6);
                } 
                else {
                    divCol.innerHTML = '+';
                    //divCol.innerHTML = (j + i * 7);
                    //divCol.setAttribute('zaj', (j + i * 7));
                    divCol.id = 'zaj' + (j + i * 7);
                }
                //divCol.setAttribute('zp', (j + i * 6));
                divCol.classList.add('itemTable');

            }
            else {
                divCol.innerHTML = tabHours[i][0];
                divCol.classList.add('itemTable2');
            }

            divRow.appendChild(divCol);

        }

        //divRow.innerHTML = 'werwe';
        practicalOnDay.appendChild(divRow);
    }


}








