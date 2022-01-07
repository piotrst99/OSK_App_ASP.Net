const showPracticalCancelBtn = document.getElementById('showPracticalCancelBtn');
const hiddenPracticalCancelBtn = document.getElementById('hiddenPracticalCancelBtn');
//const practicalCancel = document.getElementById('practicalCancel');
const practicalCancel = $('#practicalCancel');

showPracticalCancelBtn.addEventListener('click', () => {
    practicalCancel.css('display', 'block');
    showPracticalCancelBtn.style.display = 'none';
    hiddenPracticalCancelBtn.style.display = 'block';
});

hiddenPracticalCancelBtn.addEventListener('click', () => {
    practicalCancel.css('display', 'none');
    showPracticalCancelBtn.style.display = 'block';
    hiddenPracticalCancelBtn.style.display = 'none';
});

function getPracticalToCancel() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/PratcicalCourse/GetPracticalToCancel',
        success: function (data) {
            var result = data.result;
            console.log(result);

            /*result.forEach((element, index, array) => {
                setCancelPracticalData2(element)
                //addMemberToTable(element);
            });*/

            setCancelPracticalData(result);

        }
   });
}

function setCancelPracticalData(tab) {
    if (tab.length > 0) {
        /*tab.forEach((element, index, array) => {
            console.log(element.studentID);

            var tr = document.createElement('tr'); tr.className = 'cancelData';
            var td = document.createElement('td'); td.innerHTML = element.Data; tr.appendChild(td);
            //var tdClone = td.cloneNode(true); tdClone.innerHTML = el.startDate; tr.appendChild(tdClone);
            //tdClone = td.cloneNode(true); tdClone.innerHTML = el.endDate; tr.appendChild(tdClone);
            //tdClone = td.cloneNode(true); tdClone.innerHTML = el.memberStatus; tr.appendChild(tdClone);
            document.getElementById('cancelPracticalTable').appendChild(tr);

        });*/

        for (let i = 0; i < tab.length; i++) {
            var tr = document.createElement('tr'); tr.className = 'cancelData'; tr.setAttribute('idPractical', tab[i].id);
            var td = document.createElement('td'); td.innerHTML = (i + 1); tr.appendChild(td);
            var tdClone = td.cloneNode(true); tdClone.innerHTML = tab[i].date; tr.appendChild(tdClone);
            var tdClone = td.cloneNode(true); tdClone.innerHTML = tab[i].startTime; tr.appendChild(tdClone);
            var tdClone = td.cloneNode(true); tdClone.innerHTML = tab[i].student; tr.appendChild(tdClone);
            var tdClone = td.cloneNode(true); tdClone.innerHTML = tab[i].employee; tr.appendChild(tdClone);
            var tdClone = td.cloneNode(true); tdClone.innerHTML = tab[i].category; tr.appendChild(tdClone);

            var iTag = document.createElement('i'); iTag.className = 'icon-ok-1';
            var yesBtn = document.createElement('button'); yesBtn.setAttribute('idPractical', tab[i].id);
            yesBtn.appendChild(iTag); yesBtn.classList = 'acceptBtn btn btn-sm btn-success ';
            
            var iTag2 = document.createElement('i'); iTag2.className = 'icon-cancel-1';
            var noBtn = document.createElement('button'); noBtn.appendChild(iTag2);  noBtn.classList = 'deniedBtn btn btn-sm btn-danger';

            yesBtn.style = 'margin-right: 10px; margin-left:15%'; noBtn.style = 'margin-left: 10px; margin-right:15%';
            var tdClone = td.cloneNode(true); tdClone.classList = 'buttons';
            //tdClone.appendChild(yesBtn); tdClone.appendChild(noBtn); tr.appendChild(tdClone);
            tr.appendChild(yesBtn); tr.appendChild(noBtn);
            //var tdClone = td.cloneNode(true); tdClone.innerHTML = 'nic'; tr.appendChild(tdClone);
            document.getElementById('cancelPracticalTable').appendChild(tr);

        }

        setAcceptAndDeniedButtons();

    }
    else {
        //
    }
}


function setAcceptAndDeniedButtons() {
    var acceptBtn = document.getElementsByClassName('acceptBtn');
    var deniedBtn = document.getElementsByClassName('deniedBtn');

    for (let i = 0; i < acceptBtn.length; i++) {
        acceptBtn[i].addEventListener('click', () => {
            var id = document.getElementById('cancelPracticalTable').getElementsByClassName('cancelData')[i].getAttribute('idPractical');
            var date = document.getElementById('cancelPracticalTable').getElementsByClassName('cancelData')[i].getElementsByTagName('td')[1].textContent;
            //console.log(document.getElementById('cancelPracticalTable').getElementsByClassName('cancelData')[i].getAttribute('idPractical'));
            //console.log(data);
            acceptCancelPractical(id, date);
            
        });
    }

    for (let i = 0; i < deniedBtn.length; i++) {
        deniedBtn[i].addEventListener('click', () => {
            var id = document.getElementById('cancelPracticalTable').getElementsByClassName('cancelData')[i].getAttribute('idPractical');
            //console.log(document.getElementById('cancelPracticalTable').getElementsByClassName('cancelData')[i].getAttribute('idPractical'));
            deniedCancelPractical(id);
        });
    }

}

function acceptCancelPractical(val, date) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        data: { id: val },
        url: '/PratcicalCourse/AcceptCancelPractical',
        success: function (result) {
            if (result) {
                var practicalToCancel = document.getElementsByClassName('cancelData');
                if (practicalToCancel.length > 0) { for (let i = practicalToCancel.length - 1; i >= 0; i--) practicalToCancel[i].remove() };
                getPracticalToCancel();

            }

            var dateInCallendar = selectDate.getFullYear() + "-" + sprNumberData(selectDate.getMonth() + 1) + "-" +
                sprNumberData(selectDate.getDate());
            console.log(date + " " + dateInCallendar);

            if (date == dateInCallendar) {
                createDiv2(selectDate);
            }

        }
    });


}

function deniedCancelPractical(val) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        data: { id: val },
        url: '/PratcicalCourse/DeniedCancelPractical',
        success: function (result) {
            if (result) {
                var practicalToCancel = document.getElementsByClassName('cancelData');
                if (practicalToCancel.length > 0) { for (let i = practicalToCancel.length - 1; i >= 0; i--) practicalToCancel[i].remove() };

                getPracticalToCancel();
            }
        }
    });
}


/*function setCancelPracticalData2(el) {
    var tr = document.createElement('tr'); tr.className = 'cancelData';
    var td = document.createElement('td'); td.innerHTML = ''; tr.appendChild(td);
    var tdClone = td.cloneNode(true); tdClone.innerHTML = el.data; tr.appendChild(tdClone);
    var tdClone = td.cloneNode(true); tdClone.innerHTML = el.startTime; tr.appendChild(tdClone);
    var tdClone = td.cloneNode(true); tdClone.innerHTML = el.studentID; tr.appendChild(tdClone);
    var tdClone = td.cloneNode(true); tdClone.innerHTML = el.category; tr.appendChild(tdClone);
    var yesBtn = document.createElement('button'); yesBtn.innerHTML = 'O';
    var noBtn = document.createElement('button'); noBtn.innerHTML = 'X';
    var tdClone = td.cloneNode(true); tr.appendChild(yesBtn); tr.appendChild(noBtn);
    document.getElementById('cancelPracticalTable').appendChild(tr);
}*/

