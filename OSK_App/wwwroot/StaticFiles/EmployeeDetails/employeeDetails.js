const showUserDataBtn = document.getElementById('showUserDataBtn');
const hiddenUserDataBtn = document.getElementById('hiddenUserDataBtn');
const userData = document.getElementById('userData');
const role = document.getElementById('role');
const showEmployeeDataBtn = document.getElementById('showEmployeeDataBtn');
const hiddenEmployeeDataBtn = document.getElementById('hiddenEmployeeDataBtn');
const employeeData = document.getElementById('employeeData');
//const editEmployeeDriverBtn = document.getElementsByClassName('editEmployeeDriverBtn');
const editPracticalDataModal = document.getElementById('editPracticalDataModal');
const savePracticalDataBtn = document.getElementById('savePracticalDataBtn');

showUserDataBtn.style.width = '60px';
hiddenUserDataBtn.style.width = '60px';

window.onload = function () {
    if (role.value == "Sekretariat") {
        $('#employeeDriver').remove();
    }
    else {
        $('#employeeDiv').remove();
    }

    getCategories();

    addEditButtonClick();

    document.getElementById('selectDate').addEventListener('change', () => {
        //alert(document.getElementById('selectDate').value);
        refreshPracticalDetails(document.getElementById('selectDate').value);
    });

}

document.getElementsByClassName('close')[0].addEventListener('click', () => {
    editPracticalDataModal.style.display = 'none';
});

window.onclick = function (event) {
    if (event.target == editPracticalDataModal) {
        editPracticalDataModal.style.display = 'none';
    }
}

showUserDataBtn.addEventListener('click', () => {
    showUserDataBtn.style.display = 'none';
    hiddenUserDataBtn.style.display = 'block';
    userData.style.display = 'block';
});

hiddenUserDataBtn.addEventListener('click', () => {
    hiddenUserDataBtn.style.display = 'none';
    showUserDataBtn.style.display = 'block';
    userData.style.display = 'none';
});

showEmployeeDataBtn.addEventListener('click', () => {
    employeeData.style.display = 'block';
    showEmployeeDataBtn.style.display = 'none';
    hiddenEmployeeDataBtn.style.display = 'block';
});

hiddenEmployeeDataBtn.addEventListener('click', () => {
    employeeData.style.display = 'none';
    showEmployeeDataBtn.style.display = 'block';
    hiddenEmployeeDataBtn.style.display = 'none';
});

savePracticalDataBtn.addEventListener('click', () => {
    var endTime = document.getElementById('practicalEditTable').getElementsByTagName('tr')[2].getElementsByTagName('td')[0].querySelector('input').value;
    var category = document.getElementById('practicalEditTable').getElementsByTagName('tr')[4].getElementsByTagName('td')[0].textContent;
    var course = document.getElementById('practicalEditTable').getElementsByTagName('tr')[5].getElementsByTagName('td')[0].querySelector('input').value;
    var status = document.getElementById('practicalEditTable').getElementsByTagName('tr')[6].getElementsByTagName('td')[0].querySelector('select').value;
    var ID = document.getElementById('practicalEditTable').getAttribute('pracID');

    let json = { endTime: endTime, category: category, course: course, statusID: status, practicalID: ID };
    //console.log(json);
    //console.log(ID + " " + endTime + " " + course + " " + status + " " + category);

    var isOK = false;

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/PratcicalCourse/SavePratcialData',
        data: { practicalJsonData: JSON.stringify(json)  },
        success: function (data) {
            if (data.result) {
                editPracticalDataModal.style.display = 'none';
                refreshPracticalDetails(1);
            }
        }
    });

});

//////////////

function addEditButtonClick() {

    var btn = document.getElementsByClassName('editEmployeeDriverBtn');

    //console.log(btn.length);

    for (let i = 0; i < btn.length; i++) {
        btn[i].addEventListener('click', () => {
            //alert('clicked');
            var date = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[1].textContent;
            var startTime = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[2].textContent;
            var endTime = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[3].textContent;
            var student = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[4].textContent;
            var category = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[5].textContent;
            var course = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[6].textContent;
            var status = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[7].textContent;
            var ID = document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[8].querySelector('span').textContent;

            document.getElementById('practicalEditTable').setAttribute('pracID', ID);

            document.getElementById('practicalEditTable').getElementsByTagName('tr')[0].getElementsByTagName('td')[0].innerHTML = date;
            document.getElementById('practicalEditTable').getElementsByTagName('tr')[1].getElementsByTagName('td')[0].innerHTML = startTime;
            document.getElementById('practicalEditTable').getElementsByTagName('tr')[2].getElementsByTagName('td')[0].querySelector('input').value = endTime;
            document.getElementById('practicalEditTable').getElementsByTagName('tr')[3].getElementsByTagName('td')[0].innerHTML = student;
            document.getElementById('practicalEditTable').getElementsByTagName('tr')[4].getElementsByTagName('td')[0].innerHTML = category;
            document.getElementById('practicalEditTable').getElementsByTagName('tr')[5].getElementsByTagName('td')[0].querySelector('input').value = course;
            //document.getElementById('practicalEditTable').getElementsByTagName('tr')[6].getElementsByTagName('td')[0].innerHTML = status;

            editPracticalDataModal.style.display = 'block';
        });
    }
}

function getCategories() {
    var listPracticalsID = [];
    var count = document.getElementsByClassName('practicals');
    for (let i = 0; i < count.length; i++) {
        listPracticalsID.push(document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[8].querySelector('span').innerHTML);
    }

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/PratcicalCourse/GetCategiriesForPracticalDetails',
        data: { tab: listPracticalsID },
        success: function (data) {
            var resultTab = data.result;
            for (let i = 0; i < resultTab.length; i++) {
                document.getElementsByClassName('practicals')[i].getElementsByTagName('td')[5].innerHTML = resultTab[i];
            }
        }
    });

}

function refreshPracticalDetails(val) {
    var employeeId = document.getElementById('employeeID').value;
    var today = new Date();
    var beginDate = '', endDate = '';

    if (val == 2) {
        beginDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
        endDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
    }
    else if (val == 3) {
        beginDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate() - 1);
        endDate = today.getFullYear() + "-" + correctDateValue(today.getMonth()+1) + "-" + correctDateValue(today.getDate());
    }
    else if (val == 4) {
        beginDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate() - 7);
        endDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
    }
    else if (val == 5) {
        var nextWeek = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 7);
        beginDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
        endDate = nextWeek.getFullYear() + "-" + correctDateValue((nextWeek.getMonth()) + 1) + "-" + correctDateValue(nextWeek.getDate());
    }
    else if (val == 6) {
        beginDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1 - 1) + "-" + correctDateValue(today.getDate());
        endDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
    }
    else if (val == 7) {
        var nextMonth = new Date(today.getFullYear(), today.getMonth()+1, today.getDate());
        beginDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
        endDate = nextMonth.getFullYear() + "-" + correctDateValue((nextMonth.getMonth()) + 1) + "-" + correctDateValue(nextMonth.getDate());
    }
    else if (val == 8) {
        beginDate = (today.getFullYear() - 1) + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
        endDate = today.getFullYear() + "-" + correctDateValue((today.getMonth()) + 1) + "-" + correctDateValue(today.getDate());
    }

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/PratcicalCourse/GetPracticalForEmployee',
        data: { id: employeeId, beginDate: beginDate, endDate: endDate },
        success: function (data) {
            console.log(data.result);
            var resultData = data.result;
            var practicalData = document.getElementById('employeeDriver').getElementsByTagName('tr');

            for (let i = practicalData.length - 1; i > 0; i--) practicalData[i].remove();

            if (resultData.length > 0) {
                for (let i = 0; i < resultData.length; i++) {
                    var tr = document.createElement('tr'); tr.className = 'practicals';
                    var td = document.createElement('td'); td.innerHTML = (i + 1); tr.appendChild(td);

                    var tdClone = td.cloneNode(true); tdClone.innerHTML = resultData[i].data; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = resultData[i].startTime; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = resultData[i].endTime; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = resultData[i].student.user.firstName + " " + resultData[i].student.user.surname; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = ''; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = resultData[i].course; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = resultData[i].practicalStatus.name; tr.appendChild(tdClone);
                    var tdClone = td.cloneNode(true); tdClone.innerHTML = '';

                    var span = document.createElement('span');
                    span.innerHTML = resultData[i].id; span.setAttribute('hidden', false);

                    var btn = document.createElement('button');
                    btn.innerHTML = 'Edytuj'; btn.type = 'button'; btn.className = 'btn btn-primary btn-sm editEmployeeDriverBtn';

                    tdClone.innerHTML += span.outerHTML + btn.outerHTML;
                    tr.appendChild(tdClone);

                    document.getElementById('employeeDriver').appendChild(tr);
                }
                getCategories();
            }


            addEditButtonClick();

        }
    });

    
}


function correctDateValue(val) {
    if (val == 0) {
        return '12'
    }
    else {
        return parseInt(val) < 10 ? '0' + parseInt(val) : parseInt(val);
    }
}