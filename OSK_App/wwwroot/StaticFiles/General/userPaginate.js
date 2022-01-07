const countOfPaginations = document.getElementsByClassName('tablePage').length;
const previousBtn = document.getElementById('previous');
const nextBtn = document.getElementById('next');
const numberPage = document.getElementById('numberPage');
var actualPaginate = 1;

console.log(countOfPaginations);

window.onload = () => {
    document.getElementById('page1').hidden = false;
    if (countOfPaginations == 1) { nextBtn.disabled = true; }
    previousBtn.disabled = true;
    numberPage.innerHTML = "Strona "+actualPaginate + " / " + countOfPaginations;

    previousBtn.addEventListener('click', () => {
        actualPaginate -= 1;
        numberPage.innerHTML = "Strona " +actualPaginate + " / " + countOfPaginations;
        nextBtn.disabled = false;
        document.getElementById('page'+actualPaginate).hidden = false;
        document.getElementById('page'+(actualPaginate+1)).hidden = true;
        if (actualPaginate == 1) { previousBtn.disabled = true; }
    });

    nextBtn.addEventListener('click', () => {
        actualPaginate += 1;
        numberPage.innerHTML = "Strona " +actualPaginate + " / " + countOfPaginations;
        previousBtn.disabled = false;
        document.getElementById('page' + actualPaginate).hidden = false;
        document.getElementById('page' + (actualPaginate - 1)).hidden = true;
        if (actualPaginate == countOfPaginations) { nextBtn.disabled = true; }
    });

}
