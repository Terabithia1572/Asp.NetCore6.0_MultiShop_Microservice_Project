// wwwroot/cardshop/card.js
(function () {
    function mount() {
        var numInput = document.querySelector('.card-number-input');
        var nameInput = document.querySelector('.card-holder-input');
        var monthInput = document.querySelector('.month-input');
        var yearInput = document.querySelector('.year-input');
        var cvvInput = document.querySelector('.cvv-input');

        if (!numInput || !nameInput || !monthInput || !yearInput || !cvvInput) return;

        var numberBox = document.querySelector('.card-number-box');
        var nameBox = document.querySelector('.card-holder-name');
        var monthBox = document.querySelector('.exp-month');
        var yearBox = document.querySelector('.exp-year');
        var cvvBox = document.querySelector('.cvv-box');
        var front = document.querySelector('.front');
        var back = document.querySelector('.back');

        numInput.addEventListener('input', function (e) {
            var val = e.target.value.replace(/\D/g, '').substring(0, 16);
            numberBox.textContent = val ? val.replace(/(.{4})/g, '$1 ').trim() : '################';
        });

        nameInput.addEventListener('input', function (e) {
            nameBox.textContent = e.target.value || 'Ad Soyad';
        });

        monthInput.addEventListener('input', function (e) {
            monthBox.textContent = e.target.value || 'AA';
        });

        yearInput.addEventListener('input', function (e) {
            yearBox.textContent = (e.target.value || '').toString().slice(-2) || 'YY';
        });

        cvvInput.addEventListener('mouseenter', function () {
            if (front && back) {
                front.style.transform = 'perspective(1000px) rotateY(-180deg)';
                back.style.transform = 'perspective(1000px) rotateY(0deg)';
            }
        });
        cvvInput.addEventListener('mouseleave', function () {
            if (front && back) {
                front.style.transform = 'perspective(1000px) rotateY(0deg)';
                back.style.transform = 'perspective(1000px) rotateY(180deg)';
            }
        });
        cvvInput.addEventListener('input', function (e) {
            if (cvvBox) cvvBox.textContent = e.target.value;
        });
    }

    // Dışarıdan çağrılabilsin:
    window.mountPaymentCard = function () {
        // partial DOM’a girsin, sonra bağla
        setTimeout(mount, 0);
    };

    // Sayfa ilk yüklemesinde (partial zaten sayfadaysa) bağla:
    if (document.readyState !== 'loading') window.mountPaymentCard();
    else document.addEventListener('DOMContentLoaded', window.mountPaymentCard);
})();
