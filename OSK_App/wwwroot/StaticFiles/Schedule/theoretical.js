const addTheoreticalBtn = document.getElementById('addTheoretical');
const addTheoreticalModal = document.getElementById('addTheoreticalModal');

window.onclick = function (event) {
    if (event.target == addTheoreticalModal) {
        addTheoreticalModal.style.display = 'none';
    }

    getListOfEmployees();

}

addTheoreticalBtn.addEventListener('click', () => {
    addTheoreticalModal.style.display = 'block';
});

document.getElementsByClassName('close')[0].addEventListener('click', () => {
    addTheoreticalModal.style.display = 'none';
});

function getListOfEmployees() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Zajecia/GetOsobyZajeciaPraktyczne',
        data: {},
        success: function (data) {
            var instruktorzy = data.listaInstruktorow;

            for (var i = 0; i < instruktorzy.length; i++) {
                var option = document.createElement('option');
                option.value = instruktorzy[i].userID;
                option.innerHTML = instruktorzy[i].user.firstName + " " + instruktorzy[i].user.surname;
                $('#employeeSelect').append(option);
            }


        }
    });
}