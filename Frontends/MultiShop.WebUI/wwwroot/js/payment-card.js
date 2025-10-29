document.addEventListener('DOMContentLoaded', function () {
    const cardInput = document.querySelector('.card-number-input');
    const cardDisplay = document.querySelector('.card-number-display');
    const holderInput = document.querySelector('.card-holder-input');
    const holderDisplay = document.querySelector('.card-holder-display');
    const monthInput = document.querySelector('.month-input');
    const yearInput = document.querySelector('.year-input');
    const expDisplay = document.querySelector('.exp-display');

    if (cardInput) {
        cardInput.addEventListener('input', e => {
            let val = e.target.value.replace(/\D/g, '').substring(0, 16);
            e.target.value = val.replace(/(.{4})/g, '$1 ').trim();
            cardDisplay.textContent = e.target.value || '0000 0000 0000 0000';
        });
    }

    if (holderInput) {
        holderInput.addEventListener('input', e => {
            holderDisplay.textContent = e.target.value || 'Ad Soyad';
        });
    }

    const updateExp = () => {
        const m = monthInput?.value || 'AA';
        const y = yearInput?.value ? yearInput.value.slice(-2) : 'YY';
        expDisplay.textContent = `${m} / ${y}`;
    };

    monthInput?.addEventListener('change', updateExp);
    yearInput?.addEventListener('change', updateExp);

    document.getElementById('btnBackToAddress')?.addEventListener('click', () => {
        fetch('/Order/GetAddressSelectionPartial')
            .then(r => r.text())
            .then(html => {
                document.querySelector('section.payment-step').outerHTML = html;
                window.scrollTo({ top: 0, behavior: 'smooth' });
            });
    });
});
