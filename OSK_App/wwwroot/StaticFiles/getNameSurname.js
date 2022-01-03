var userSettings = document.getElementById('userSettings');
//var userId = userSettings.getAttribute('userId').value;
var userId = $('#userSettings').attr('userId');
var userName = document.getElementById('userName');

$.ajax({
    type: 'POST',
    dataType: 'JSON',
    url: '/Panel/GetNameEmployee',
    data: { id: userId },
    success: function (data) {
        userName.innerHTML = data.wynik;
    }
});