///  TESTOWA WERSJA ///

let today = new Date();
let currentMonth = today.getMonth();
let currentYear = today.getFullYear();
let selectYear = document.getElementById("year");
let selectMonth = document.getElementById("month");
let selectDay;

let months = ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
    "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"];

let monthAndYear = document.getElementById("monthAndYear");
showCalendar(currentMonth, currentYear);
/**/addClickedEvent(currentMonth, currentYear);

/////

function addClickedEvent(curMonth, curYear) {

    let daysInMonth = 32 - new Date(curYear, curMonth, 32).getDate();
    //alert(daysInMonth.toString());

    for (let i = 0; i < daysInMonth; i++) {
        var test = document.getElementById("day" + (i + 1));
        test.addEventListener("click", function () {
            /*alert("clicked " + (i+1));*/
            //var t1 = document.getElementById("test123");
            //t1.innerHTML = ("test " + (i + 1));
            //t1.style = "/*background-color:#999;*//*height:200px;*/";


            // !!!!!!!
            // Tu powinno być z Ajax
            //createDiv();

            createDiv2((i + 1), (curMonth + 1), currentYear);

            //DZIAŁA
            //testoweDivy();
            //testoweDivy2();

            //document.getElementById('card').removeChild(document.getElementById('calendar'));
            //$('#calendar').remove();
            $('#calendar-body').html('');
            $('#dayOfWeek').hide();


            //add(); //dodaje event clicked do kazdego pola

            /// Wybranie wszystkich zajec dla danego dnia

            /*console.log((i + 1) + " " + (curMonth + 1) + " " + curYear);
            */


            //add2((i + 1), (curMonth + 1), currentYear);

            ///

            for (let j = 0; j < daysInMonth; j++) {
                //var div = document.getElementById("day" + (j + 1));
                if (today.getDate() == j + 1) {
                    //div.style = "background-color:#D9EDF7;";
                }
                else {
                    //div.style = "background-color:#FFF;";
                }
            }

            //var selectDay = document.getElementById("day" + (i + 1));
            //selectDay.style = "background-color:#CCC;";
        });
    }

}

function add() {
    for (let i = 0; i < 30; i++) {
        //document.querySelector('.a')[i].addEventListener('click', function () {
        /*document.getElementById('zaj'+(i+1)).addEventListener('click', function () {
            //console.log("clicked" + (i + 1));
            console.log("clicked " + (i + 1) + " " + document.getElementById("zaj" + (i + 1)).innerHTML);
        });*/
        /*if ((i + 1) % 2 == 0) {
            document.getElementById('zaj' + (i + 1)).addEventListener('click', getAjax);
        }
        else {
            document.getElementById('zaj' + (i + 1)).addEventListener('click', function () { getJson((i+1)) });
        }*/

        document.getElementById('zaj' + (i + 1)).addEventListener('click', function () { getSzczegolyZajec((i + 1)) });

    }
}

/// !!! Wazne
function createDiv2(day, month, year) {
    document.getElementById("test123").innerHTML = '';
    var wybData = year + "-" + sprNumberData(month) + "-" + sprNumberData(day);
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

                //var numer = convertDate(d[k].godzStart)[0] + convertDate(d[k].godzStart)[1];
                var numer = d[k].startTime.substring(0, 2);

                if (numer == '07') {
                    grupa1.push(d[k]);
                }
                else if (numer == '09') {
                    grupa2.push(d[k]);
                }
                else if (numer == '11') {
                    grupa3.push(d[k]);
                }
                else if (numer == '13') {
                    grupa4.push(d[k]);
                }
                else if (numer == '15') {
                    grupa5.push(d[k]);
                }

            }

            $.ajax({
                url: "/StaticFiles/harmonogram.html",
                success: function (response) {
                    $('#test123').html($($.parseHTML(response)).html());

                    setData('07', 0, grupa1);
                    setData('09', 6, grupa2);
                    setData('11', 12, grupa3);
                    setData('13', 18, grupa4);
                    setData('15', 24, grupa5);

                    $('.addZajPrakBtn').click(loadAddZajPrak);

                    if (grupa1.length >= 6) {
                        $('#07').prop('disabled', 'disabled');
                    }
                    if (grupa2.length >= 6) {
                        $('#09').prop('disabled', 'disabled');
                    }
                    if (grupa3.length >= 6) {
                        $('#11').prop('disabled', 'disabled');
                    }
                    if (grupa4.length >= 6) {
                        $('#13').prop('disabled', 'disabled');
                    }
                    if (grupa5.length >= 6) {
                        $('#15').prop('disabled', 'disabled');
                    }

                    $('#backCalendarBtn').click(backToCalendar);

                }
            });

        }
    });

}

function convertDate(val) {
    var t = val;
    var s = t.split(/-|:|T/g).slice(0, 5)
    var c = parseInt(s[3]) < 10 ? '0' + parseInt(s[3]) : parseInt(s[3]);

    var str = c + ':' + s[4];

    //console.log(s[1] + '/' + s[2] + '/' + s[0] + '  ' + c + ':' + s[4] + ' ' + f);
    return str;
}

function setData(nr, val, dane) {
    for (var i = 0; i < dane.length; i++) {
        /*var t = document.createElement('a');
        t.setAttribute('id', dane[i].id);
        t.innerHTML = dane[i].kursant.uzytkownik.pierwszeImie + " " + dane[i].kursant.uzytkownik.nazwisko;
        $('#test123').find('#hr' + nr).find('#zaj' + (val + i + 1)).html(t);*/

        /// Dziala
        //console.log(dane[i]);
        $('#test123').find('#hr' + nr).find('#zaj' + (val + i + 1)).html(dane[i].student.user.firstName+ " " + dane[i].student.user.surname);
        $('#test123').find('#hr' + nr).find('#zaj' + (val + i + 1)).attr('zp', dane[i].id);


        //document.getElementById('zaj' + (val + i + 1)).htm = t.inn;

        /*$('#test123').find('#hr' + nr).find('#zaj' + (val + i + 1)).click(() => {
            alert($('#zaj' + (val + i + 1)).find('a').val());
        });*/

        /*('click', function () {
            alert($(this).val());
        });*/

        $('#zaj' + (val + i + 1)).click(function () {
            //alert($(this).find('a').attr());
            //console.log($('#zaj' + (val + i + 1)).find('a').text());    
            //console.log($(this).attr('zp'));

            //var el = document.createElement('div');
            //el.setAttribute('id', 'daneZajPrak');
            //$('#szczegolyZaj').html(el);
            $('#szczegolyZaj').load('/StaticFiles/edytujZajeciaPraktyczne.html #info');
            $('#szczegolyZaj').attr('zp', $(this).attr('zp'));

            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/Zajecia/GetZajeciaPraktyczne5',
                data: { idZajec: $(this).attr('zp') },
                success: function (data) {
                    var d = data.wynik;
                    

                    /*var str = d.id + " " + d.data + " " + convertDate(d.godzStart) + " " + convertDate(d.godzKoniec) +
                        "<br />Kursant: " + d.kursant.uzytkownik.pierwszeImie + " " + d.kursant.uzytkownik.nazwisko +
                        "<br />Instruktor: " + d.instruktor.uzytkownik.pierwszeImie + " " + d.instruktor.uzytkownik.nazwisko +
                        "<br />Pojazd: ";*/
                    $('#kur').text(d.student.user.firstName+ " " + d.student.user.surname);
                    $('#ins').text(d.employee.user.firstName + " " + d.employee.user.surname);
                    //$('#szczegolyZaj').html(str);
                    //console.log(data.wynik);
                    //document.getElementById('kur').innerHTML = d.kursant.uzytkownik.pierwszeImie + " " + d.kursant.uzytkownik.nazwisko;
                    //document.getElementById('editBtn').addEventListener('click', loadEditContent);
                    //$('#editBtn').on('click',loadEditContent);
                    $('#editBtn').click(loadEditContent);
                }

            });
            //document.getElementById('editBtn').addEventListener('click', loadEditContent);
            //$('#editBtn').click(loadEditContent);

            //document.getElementById('editBtn').style.visibility = "visible";



        });

    }
    //$('#editBtn').css("visibility: visible;");
}

function setData2(nr, val, dane) {
    //
}


function getSzczegolyZajec(val) {
    var dane = val % 2 == 0 ? 1 : 2;
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/GetZajeciaPraktyczne2',
        data: { val: dane },
        success: function (data) {
            document.getElementById('szczegoly').innerHTML = data.someProperty;
            console.log(data.someProperty);
            console.log()
        }
    });

}

function add2(day, month, year) {
    for (let i = 0; i < 30; i++) {
        //document.getElementById('zaj' + (i + 1)).addEventListener('click', function () { getSzczegolyZajec2((day, month, year)) });
        document.getElementById('zaj' + (i + 1)).addEventListener('click', function () {
            console.log(day + " " + month + " " + year);

            //var wybData = day + "-" + month + "-" + year;
            //var wybData = day + "." + month + "." + year;
            var wybData = year + "-" + sprNumberData(month) + "-" + sprNumberData(day);
            var selectValue = i % 2 == 0 ? 1 : 2;

            $.ajax({
                type: 'POST',
                dataType: 'JSON',
                url: '/Zajecia/GetZajeciaPraktyczne3',
                data: { wybranaData: wybData, val: selectValue },
                //data: { day: day, month: month, year: year },
                //data: { wybranaData: wybDat },
                success: function (data) {
                    document.getElementById('szczegoly').innerHTML = data.someProperty;
                    console.log(data.someProperty);
                    //console.log()
                }
            });

        })
    }

}

function sprNumberData(val) {
    if (val < 10) {
        return "0" + val;
    }
    else {
        return val;
    }
}


function getSzczegolyZajec2(day, month, year) {
    //console.log(day + " " + month + " " + year);
    //var dane = val % 2 == 0 ? 1 : 2;
    //var wybDat = year + "-" + month+ "-" + day;
    console.log(day + " " + month + " " + year);

    var wybData = year + "-" + month + "-" + day;

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/GetZajeciaPraktyczne3',
        //data: { wybranaData: },
        //data: { day: day, month: month, year: year },
        data: { wybranaData: wybData },
        success: function (data) {
            document.getElementById('szczegoly').innerHTML = data.someProperty;
            console.log(data.someProperty);
            //console.log()
        }
    });
}


////// !!!!!!
function next() {
    currentYear = (currentMonth === 11) ? currentYear + 1 : currentYear;
    currentMonth = (currentMonth + 1) % 12;
    showCalendar(currentMonth, currentYear);
    /**/addClickedEvent(currentMonth, currentYear);
}

function previous() {
    currentYear = (currentMonth === 0) ? currentYear - 1 : currentYear;
    currentMonth = (currentMonth === 0) ? 11 : currentMonth - 1;
    showCalendar(currentMonth, currentYear);
    /**/addClickedEvent(currentMonth, currentYear);
}

function jump() {
    currentYear = parseInt(selectYear.value);
    currentMonth = parseInt(selectMonth.value);
    showCalendar(currentMonth, currentYear);
    /**/addClickedEvent(currentMonth, currentYear);
}

function showCalendar(month, year) {
    $('#test123').html('');

    let firstDay = (new Date(year, month)).getDay();
    let daysInMonth = 32 - new Date(year, month, 32).getDate();

    let tbl = document.getElementById("calendar-body"); // body of the calendar

    // clearing all previous cells
    tbl.innerHTML = "";

    // filing data about month and in the page via DOM.
    monthAndYear.innerHTML = months[month] + " " + year;
    selectYear.value = year;
    selectMonth.value = month;

    // creating all cells
    let date = 1;
    for (let i = 0; i < 6; i++) {
        // creates a table row
        let row = document.createElement("tr");

        //creating individual cells, filing them up with data.
        for (let j = 0; j < 7; j++) {
            if (i === 0 && j < firstDay) {
                let cell = document.createElement("td");
                let cellText = document.createTextNode("");
                cell.appendChild(cellText);





                row.appendChild(cell);
            }
            else if (date > daysInMonth) {
                break;
            }

            else {
                let cell = document.createElement("td");
                let cellText = document.createTextNode(date);
                if (date === today.getDate() && year === today.getFullYear() && month === today.getMonth()) {
                    cell.classList.add("bg-info");
                } // color today's date
                cell.appendChild(cellText);


                //cell.classList.add("test" + date);

                cell.id = "day" + date;

                //cell.addEventListener("click")

                /*t1 = document.getElementsByClassName("test" + date).addEventListener("click", function () {
                    alert("clicked " + date);
                });*/

                /*t1.addEventListener("click", function () {
                    alert("clicked " + date);
                });*/

                /*cell.addEventListener("click", function () {
                    //var t = document.getElementsByClassName("test1").innerHTML;
                    //alert("clicked " + document.getElementsByClassName("test1").innerHTML);
                    //alert("clicked " + t);
                    //alert("clicked");
                    
                });*/




                row.appendChild(cell);
                date++;
            }


        }

        tbl.appendChild(row); // appending each row into calendar body.
    }

    //addClickedEvent(date);

    /*for (let i = 0; i < 6; i++) {
        for (let j = 0; j < 7; j++) {
            if (i === 0 && j < firstDay) {
                // puste


            }
            else if (date > daysInMonth) {
                break;
            }
            else {
                // watosci

                //alert(document.getElementsByTagName('tr')[i].getElementsByTagName('td')[j].innerHTML);
            }
        }
    }*/


}

/////////////////////////////////
/////////////////////////////////
/////////////////////////////////
/////////////////////////////////

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

function usunZajeciaPraktyczne(id) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/deleteZajeciaPraktyczne',
        data: { idZajPrak: id },
        success: function (data) {
            if (data.czyUsunieto) {
                createDiv2(selectDay, (currentMonth + 1), currentYear);
                $('#szczegolyZaj').html('');
            }
        }
    });
}

function dodajZajeciePraktyczne() {
    var wybData = currentYear + "-" + sprNumberData(currentMonth + 1) + "-" + sprNumberData(selectDay);
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
                createDiv2(selectDay, (currentMonth + 1), currentYear);
                $('#szczegolyZaj').html('');
            }
            else {
                console.log(data.t);
            }
        }
    });

}


function backToCalendar() {
    showCalendar(currentMonth, currentYear);
    addClickedEvent(currentMonth, currentYear);
    $('#dayOfWeek').show();
}



//////////////////

// DZIAŁA
function testoweDivy() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/Test',
        success: function (data) {
            $('#szczegolyZaj').html(data.value);
        }
    });
}


function testoweDivy2() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/GetZajeciaPraktyczne4',
        success: function (data) {
            $('#szczegolyZaj').html(data.value);
        }
    });
}

const selectCallendar = document.getElementById('selectCallendar');

selectCallendar.addEventListener('change', () => {
    //console.log(selectCallendar.value);
    //console.log(e.target.value);
    
    var date = new Date(selectCallendar.value);
    var year = date.getFullYear();
    var month = date.getMonth()+1;
    var day = date.getDate();

    //console.log(date.getFullYear());
    //console.log(date.getMonth()+1);
    //
    createDiv2(day, month, year);

});