//$('#editBtn').css("visibility: hidden;");
var btn = document.getElementsByTagName('input');

/*btn[0].addEventListener('click', function () {
    //document.getElementById('szczegoly').load('~/Script/test.html');

    //var test = document.createElement('testowy');
    //test.src = '~/Scripts/test.html';
    //test.acync = true;
    //document.getElementById('szczegoly').appendChild(test);

    //document.getElementById('szczegoly').innerHTML = '<object type="text/html" data="/Scripts/test.html"></object>'
    //$('#szczegoly').load("~/Scripts/test.html");
    
});*/



$('#addBtn').on('click', addAddContent);
//$('#editBtn').on('click', loadEditContent);
//$('#editBtn').click(loadEditContent);

//document.addEventListener('click', loadEditContent);

function addAddContent() {
    /*$.ajax("/Scripts/test.html", {
        success: function (response) {
            //var test = response.getElementById('addContent');
            //var t = $('#addContent').load(response);

            //$('#szczegoly').html(response);
            //$('#szczegoly').html($(response).find('#addContent').html());
            //$('szczegoly').html(response.'#addContent');  


        }
    });*/
    $.ajax({
        url: "/Scripts/test.html",
        context: document.body,
        success: function (response) {
            $('#szczegoly').load('/Scripts/test.html #addContent');
        }
    });
}

function addEditContent() {
    /*$.ajax("/Scripts/test.html", {
        success: function (response) {
            $('#szczegoly').html(response);
        }
    })*/
    //$('#szczegoly').html('');
    $.ajax({
        url: "/Scripts/test.html",
        context: document.body,
        success: function (response) {
            //$('#szczegoly').load('/Scripts/test.html #editContent');
            $('#szczegolyZaj').load('#editContent');
        }
    });

}


function loadEditContent2() {

    /*$.ajax({
        url: "/Scripts/edytujZajeciaPraktyczne.html",
        context: document.body,
        success: function (response) {
            //document.getElementById('szczegolyZaj').innerHTML = response;
        }
    });*/
    var kur = $('#kur').text();
    var ins = $('#ins').text();
    $('#szczegolyZaj').load('/Scripts/edytujZajeciaPraktyczne.html #edit');
    $('#kursantTxt').val(kur);
    $('#instruktorTxt').val(ins);
    console.log(kur);
    console.log(ins);
   

    //$('#szczegoly').load('/Scripts/edytujZajeciaPraktyczne.html');
    /*$.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Panel/GetZajeciaPraktyczne5',
        data: { idZajec: $(this).attr('zp') },
        success: function (data) {

            

            //$('#szczegolyZaj')
            var d = data.wynik;
            var str = d.id + " " + d.data + " " + convertDate(d.godzStart) + " " + convertDate(d.godzKoniec) +
                "<br />Kursant: " + d.kursant.uzytkownik.pierwszeImie + " " + d.kursant.uzytkownik.nazwisko +
                "<br />Instruktor: " + d.instruktor.uzytkownik.pierwszeImie + " " + d.instruktor.uzytkownik.nazwisko +
                "<br />Pojazd: ";
            $('#szczegolyZaj').html(str);
            console.log(data.wynik);
        }

    });*/

    

}