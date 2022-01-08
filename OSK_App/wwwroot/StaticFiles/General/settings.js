const expandBtn = document.getElementsByClassName('expandBtn');
const rollUpBtn = document.getElementsByClassName('rollUpBtn');
const items = document.getElementsByClassName('item');
const select = document.getElementsByClassName('select');
const optionsToClick = document.getElementsByClassName('optionsToClick');
var clicked = [];

window.onload = function () {

    for (let i = 0; i < document.getElementsByClassName('optionsToClick').length; i++) { clicked.push(false); }

    for (let i = 0; i < rollUpBtn.length; i++) {
        expandBtn[i].style.display = 'none';
        rollUpBtn[i].style.display = 'none';
    }

    for (let i = 0; i < clicked.length; i++) {
        optionsToClick[i].addEventListener('click', () => {
            if (!clicked[i]) {
                items[i].style.display = 'block';
                clicked[i] = true;
            }
            else {
                items[i].style.display = 'none';
                clicked[i] = false;
            }
        });
    }

    for (let i = 0; i < expandBtn.length; i++) {
        expandBtn[i].addEventListener('click', () => {
            expandBtn[i].style.display = 'none';
            rollUpBtn[i].style.display = 'block';
            items[i].style.display = 'block';
            //items[i].classList.add = '.selectedData';
        });
    }

    for (let i = 0; i < rollUpBtn.length; i++) {
        rollUpBtn[i].addEventListener('click', () => {
            expandBtn[i].style.display = 'block';
            rollUpBtn[i].style.display = 'none';
            items[i].style.display = 'none';
            //items[i].classList.remove = '.selectedData';
        });
    }

    setClick();

}

function setClick() {

    select[0].addEventListener('click', () => { userData(); });
    select[1].addEventListener('click', () => { changePaddwordForm(); });

    select[2].addEventListener('click', () => { addStudentHelp(); });
    select[3].addEventListener('click', () => { editStudentHelp(); });
    select[4].addEventListener('click', () => { alert('Usuwanie'); });
    select[5].addEventListener('click', () => { alert('Szczegóły kursanta'); });
    select[6].addEventListener('click', () => { alert('Przypisanie do kursu'); });

    select[7].addEventListener('click', () => { alert('Dodanie'); });
    select[8].addEventListener('click', () => { alert('Edycja'); });
    select[9].addEventListener('click', () => { alert('Usuwanie'); });
    select[10].addEventListener('click', () => { alert('Szczegóły pracownika'); });

    select[11].addEventListener('click', () => { alert('Dodanie'); });
    select[12].addEventListener('click', () => { alert('Edycja'); });
    select[13].addEventListener('click', () => { alert('Usuwanie'); });
    select[14].addEventListener('click', () => { alert('Szczegóły pojazdu'); });

    select[15].addEventListener('click', () => { alert('Dodanie'); });
    select[16].addEventListener('click', () => { alert('Edycja'); });
    select[17].addEventListener('click', () => { alert('Usuwanie'); });
    select[18].addEventListener('click', () => { alert('Odwołanie zajęcia'); });

    select[19].addEventListener('click', () => { alert('Dodanie'); });
    select[20].addEventListener('click', () => { alert('Edycja'); });
    select[21].addEventListener('click', () => { alert('Lista uczestników'); });

    select[22].addEventListener('click', () => { alert('Przyjmowanie wpłaty'); });
    select[23].addEventListener('click', () => { alert('Edycja'); });
    select[24].addEventListener('click', () => { alert('Usuwanie'); });

}

function userData() {
    document.getElementById('userData').style.display = 'block';
    document.getElementById('otherView').style.display = 'none';
    document.getElementById('otherView').innerHTML = '';
}

function changePaddwordForm() {
    document.getElementById('userData').style.display = 'none';
    document.getElementById('otherView').innerHTML = '';
    document.getElementById('otherView').style.display = 'block';

    $.ajax({
        url: "/StaticFiles/SettingsView/changePassword.html",
        type: 'GET',
        success: function (response) {
            $('#otherView').html($($.parseHTML(response)).html());
        }
    });

}

function addStudentHelp() {
    document.getElementById('userData').style.display = 'none';
    document.getElementById('otherView').innerHTML = '';
    document.getElementById('otherView').style.display = 'block';

    $.ajax({
        url: "/StaticFiles/SettingsView/addStudentHelp.html",
        type: 'GET',
        success: function (response) {
            $('#otherView').html($($.parseHTML(response)).html());
        }
    });

}

function editStudentHelp() {
    document.getElementById('userData').style.display = 'none';
    document.getElementById('otherView').innerHTML = '';
    document.getElementById('otherView').style.display = 'block';

    $.ajax({
        url: "/StaticFiles/SettingsView/editStudentHelp.html",
        type: 'GET',
        success: function (response) {
            $('#otherView').html($($.parseHTML(response)).html());
        }
    });

}
