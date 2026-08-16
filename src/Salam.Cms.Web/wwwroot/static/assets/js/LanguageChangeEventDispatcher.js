const items = document.querySelectorAll('.dropdown__content__item');
items.forEach(item => {
    item.addEventListener('click', e => {
        const newLang = e.currentTarget.dataset.value;
        const langEvent = new CustomEvent('language-change', {
            detail: { locale: newLang },
            bubbles: true,
            composed: true
        });
        e.currentTarget.dispatchEvent(langEvent);
        console.log("Event Dispatched", langEvent);
    });
});