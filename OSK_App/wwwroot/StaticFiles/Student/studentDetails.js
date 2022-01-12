const acceptPayBtn = document.getElementById('acceptPayBtn');
const acceptPaymentForm = document.getElementById('acceptPaymentForm');
const showUserDataBtn = document.getElementById('showUserDataBtn');
const hiddenUserDataBtn = document.getElementById('hiddenUserDataBtn');
const addPaymentBtn = document.getElementById('addPaymentBtn');
//const cancelPaymentBtn = document.getElementById('cancelPaymentBtn');
const userData = document.getElementById('userData');
const costCourse = document.getElementById('costCourse');
const typePayments = document.getElementById('typePayments');
const errorCostMsg = document.getElementById('errorCostMsg');
const showPaymentDataBtn = document.getElementById('showPaymentDataBtn');
const hiddenPaymentDataBtn = document.getElementById('hiddenPaymentDataBtn');
const studentPaymemts = document.getElementById('studentPaymemts');
const showStudentCourseBtn = document.getElementById('showStudentCourseBtn');
const hiddenStudentCourseBtn = document.getElementById('hiddenStudentCourseBtn');
const studentCourses = document.getElementById('studentCourses');
const addCourseBtn = document.getElementById('addCourseBtn');
const listCourses = document.getElementById('listCourses');
const category = document.getElementById('category');
const addStudentCourseBtn = document.getElementById('addStudentCourseBtn');
const errorCourseMsg = document.getElementById('errorCourseMsg');
const studentCourseDetailsBtn = document.getElementById('studentCourseDetailsBtn');
const showStudentCourseModal = document.getElementById('showStudentCourseModal');

const courseDetailBtn = document.getElementById('courseDetailBtn');
const practicalBtn = document.getElementById('practicalBtn');
const coursePage = document.getElementById('coursePage');
const practicalPage = document.getElementById('practicalPage');

showUserDataBtn.style.width = '60px';
hiddenUserDataBtn.style.width = '60px';
showPaymentDataBtn.style.width = '60px';
hiddenPaymentDataBtn.style.width = '60px';

window.onload = function () {
    checkAceptPaymentSelect();
    GetCourseName(1);

}


// WYSWIETLENIE OKIENKA DLA PRZYJECIA WPLATY
var acceptPaymentModal = document.getElementById('acceptPaymentModal');
var closeAcceptPayment = document.getElementsByClassName("close")[2];
var courseInfo = document.getElementById('courseInfo');
var extraHour = document.getElementById('extraHour');
var amountHours = document.getElementById('amountHours');
var totalCost = document.getElementById('totalCost');

acceptPayBtn.addEventListener('click', () => {
    //acceptPaymentForm.style.display = 'block';
    acceptPaymentModal.style.display = "block";
});

closeAcceptPayment.onclick = function () {
    costCourse.value = 0;
    errorCostMsg.innerHTML = ' ';
    typePayments.value = 1;

    acceptPaymentModal.style.display = "none";
    courseInfo.style.display = "block";
    extraHour.style.display = "none";
}

function checkAceptPaymentSelect() {
    if (typePayments.value == 1) {
        courseInfo.style.display = "block";
        extraHour.style.display = "none";
    }
    else if (typePayments.value == 2) {
        courseInfo.style.display = "none";
        extraHour.style.display = "block";
    }
    errorCostMsg.innerHTML = ' ';
}

typePayments.addEventListener('change', () => {
    checkAceptPaymentSelect();
});

amountHours.addEventListener('change', function () {
    totalCost.value = amountHours.value * 160;
});

// WYSWIETLENIE OKIENKA DLA DODANIA KURSU
const addStudentCourseModal = document.getElementById('addStudentCourseModal');
var closeAddStudentCourse = document.getElementsByClassName("close")[0];

addCourseBtn.addEventListener('click', () => {
    addStudentCourseModal.style.display = 'block';
});

closeAddStudentCourse.addEventListener('click', () => {
    addStudentCourseModal.style.display = 'none';
    errorCourseMsg.innerHTML = ' ';
});

// WYSWIETLENIE OKIENKA DLA SZCZEGOLOW KURSU
var closeShowStudentCourse = document.getElementsByClassName("close")[1];

if (studentCourseDetailsBtn != null) {
    studentCourseDetailsBtn.addEventListener('click', () => {
        showStudentCourseModal.style.display = 'block';
        coursePage.style.display = 'block';
    });
}

closeShowStudentCourse.addEventListener('click', () => {
    showStudentCourseModal.style.display = 'none';
});

courseDetailBtn.addEventListener('click', () => {
    coursePage.style.display = 'block';
    practicalPage.style.display = 'none';
});

practicalBtn.addEventListener('click', () => {
    coursePage.style.display = 'none';
    practicalPage.style.display = 'block';
});

window.onclick = function (event) {
    if (event.target == acceptPaymentModal) {
        costCourse.value = 0;
        errorCostMsg.innerHTML = ' ';
        typePayments.value = 1;

        acceptPaymentModal.style.display = "none";
        courseInfo.style.display = "block";
        extraHour.style.display = "none";
    }
    if (event.target == addStudentCourseModal) {
        addStudentCourseModal.style.display = 'none';
        errorCourseMsg.innerHTML = ' ';
    }
    if (event.target == showStudentCourseModal) {
        showStudentCourseModal.style.display = 'none';
    }
}





category.addEventListener('change', () => {
    GetCourseName(category.value);
});

showStudentCourseBtn.addEventListener('click', () => {
    studentCourses.style.display = 'block';
    showStudentCourseBtn.style.display = 'none';
    hiddenStudentCourseBtn.style.display = 'block';
});

hiddenStudentCourseBtn.addEventListener('click', () => {
    studentCourses.style.display = 'none';
    showStudentCourseBtn.style.display = 'block';
    hiddenStudentCourseBtn.style.display = 'none';
});

addStudentCourseBtn.addEventListener('click', () => {
    //console.log(category.value + " " + listCourses.value);

    var isCorrect = false;

    var listOfStudentCourses = studentCourses.getElementsByClassName('courseData');
    if (listOfStudentCourses.length == 0) {
        if (listCourses.length == 0) {
            errorCourseMsg.innerHTML = 'Nie ma kursu dla wybranej kategorii';
            isCorrect = false;
        }
        else {
            isCorrect = true;
        }
    }
    else {
        for (let i = 0; i < listOfStudentCourses.length; i++) {
            if (category[category.value - 1].text == listOfStudentCourses[i].getElementsByTagName('td')[1].textContent) {
                errorCourseMsg.innerHTML = 'Kursant jest przypisany do wybranej kategorii';
                isCorrect = false;
            }
            else {
                if (listCourses.length == 0) {
                    errorCourseMsg.innerHTML = 'Nie ma kursu dla wybranej kategorii';
                    isCorrect = false;
                }
                else {
                    isCorrect = true;
                }
            }
            i = listOfStudentCourses.length;
        }
    }

    if (isCorrect) {
        var userID = document.getElementById('userID').value;
        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: '/Kurs/SetStudentCourse',
            data: { categoryID: category.value, courseID: listCourses.value, studentID: userID },
            success: function (data) {
                if (data.result) {
                    window.location.reload(true);
                }
                else {
                    //
                }
            }
        });
    }

});



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

showPaymentDataBtn.addEventListener('click', () => {
    hiddenPaymentDataBtn.style.display = 'block';
    showPaymentDataBtn.style.display = 'none';
    studentPaymemts.style.display = 'block';
});

hiddenPaymentDataBtn.addEventListener('click', () => {
    hiddenPaymentDataBtn.style.display = 'none';
    showPaymentDataBtn.style.display = 'block';
    studentPaymemts.style.display = 'none';
});

addPaymentBtn.addEventListener('click', () => {
    var userID = document.getElementById('userID').value;
    var isCorrect = false;

    if (typePayments.value == 1) {
        if (costCourse.value > 0 && costCourse.value != '' && costCourse.value != ' ')
            isCorrect = true;
    }
    else if (typePayments.value == 2) {
        if (totalCost.value > 0 && totalCost.value != '' && totalCost.value != ' ' && amountHours.value > 0 && amountHours.value !='')
            isCorrect = true;
    }

    if (isCorrect) {
        var costValue = typePayments.value == 1 ? costCourse.value : totalCost.value;

        $.ajax({
            type: 'POST',
            dataType: 'JSON',
            url: '/Wplata/AcceptPayment',
            data: { studentID: userID, cost: costValue, typePaimentID: typePayments.value },
            success: function (data) {
                if (data.isSave == true) {
                    window.location.reload(true);
                }
                else {
                    console.log(data.isSave);
                }
            }
        });
    }
    else {
        errorCostMsg.innerHTML = 'Podany koszt jest nieprawdłowy';
    }

});


/////// FUNKCJE

function GetCourseName(val) {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Kurs/GetCourseCategory',
        data: { categoryId: val },
        success: function (data) {
            var array = data.wynik;

            $('#listCourses option').remove();

            for (var i = 0; i < array.length; i++) {
                var option = document.createElement('option');
                option.value = array[i].id;
                option.innerHTML = array[i].name;
                $('#listCourses').append(option);
            }

        }
    });
}

function GetCourseDetailForStudent() {
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        url: '/Kurs/GetCourseCategory',
        data: { categoryId: val },
        success: function (data) {
            var array = data.wynik;

            $('#listCourses option').remove();

            for (var i = 0; i < array.length; i++) {
                var option = document.createElement('option');
                option.value = array[i].id;
                option.innerHTML = array[i].name;
                $('#listCourses').append(option);
            }

        }
    });
}
