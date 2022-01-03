var roleID = document.getElementById('roleID');
var nrInstructor = document.getElementById('nrInstructor');
nrInstructor.disabled = true;

window.onload = function () {
    if (roleID.value == 2) {
        nrInstructor.disabled = false;
    }
    else {
        nrInstructor.disabled = true;
    }
}

roleID.addEventListener("change", () => {
    if (roleID.value == 2) {
        nrInstructor.disabled = false;
    }
    else {
        nrInstructor.disabled = true;
    }
});