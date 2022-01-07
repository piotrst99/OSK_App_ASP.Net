//const studentParticipantsBtn = document.getElementById('studentParticipantsBtn');
const studentParticipantsBtn = document.getElementsByClassName('studentParticipantsBtn');
const studentParticipantsModal = document.getElementById('studentParticipantsModal');

window.onload = function () {
    for (let i = 0; i < studentParticipantsBtn.length; i++) {
        studentParticipantsBtn[i].addEventListener('click', () => {
            studentParticipantsModal.style.display = 'block';
            var id = document.getElementsByTagName('tr')[i + 1].getElementsByTagName('td')[6].querySelector('span').textContent;

            getStudentParticipants(id);

        });
    }
}

window.onclick = function (event) {
    if (event.target == studentParticipantsModal) {
        studentParticipantsModal.style.display = 'none';
    }
}

document.getElementsByClassName('close')[0].addEventListener('click', () => {
    studentParticipantsModal.style.display = 'none';
});


function getStudentParticipants(courseID) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Kurs/GetStudentParticipants',
        data: { id: courseID },
        success: function (data) {
            //console.log(data);

            //document.getElementById('participantsTable').removeChild('member');

            var members = document.getElementsByClassName('member');

            if (members.length > 0) { for (let i = members.length - 1; i >= 0; i--) members[i].remove() };
            //if (members.length > 0) { for (let i = 0; i < members.length-1; i++) members[members.length-1-i].remove() };

            var listOfMembers = data;
            listOfMembers.forEach((element, index, array) => {
                
                addMemberToTable(element);
            });

        }
    });
}

function addMemberToTable(el) {
    var tr = document.createElement('tr'); tr.className = 'member';
    var td = document.createElement('td'); td.innerHTML = el.student; tr.appendChild(td);
    var tdClone = td.cloneNode(true); tdClone.innerHTML = el.startDate; tr.appendChild(tdClone);
    tdClone = td.cloneNode(true); tdClone.innerHTML = el.endDate; tr.appendChild(tdClone);
    tdClone = td.cloneNode(true); tdClone.innerHTML = el.memberStatus; tr.appendChild(tdClone);
    document.getElementById('participantsTable').appendChild(tr);
}

