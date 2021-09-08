const postData = async (url = '', data = {}, headers = { 'Content-Type': 'application/json' }) => {
    const response = await fetch(url, {
        method: 'POST',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: headers,
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    });
    return response.json();
}

const element = (tagName, attributes, children) => {
    const ele = document.createElement(tagName);
    if (attributes) {
        for (const attribute in attributes) {
            ele.setAttribute(attribute, attributes[attribute]);
        }
    }

    if (children) {
        for (let i = 0; i < children.length; i++) {
            const child = children[i];
            if (typeof child === 'string') {
                if (child === '&times;') {
                    ele.innerHTML = child;
                    break;
                }

                ele.appendChild(document.createTextNode(child));
            } else {
                ele.appendChild(child);
            }
        }
    }

    return ele;
}

const div = (attributes, children) => element('div', attributes, children);

const createModal = () => {
    return div({ class: 'modal', tabindex: -1, role: 'dialog' }, [
        div({ class: 'modal-dialog', role: 'document' }, [
            div({ class: 'modal-content' }, [
                div({ class: 'modal-header' }, [
                    element('h5', { class: 'modal-title' }, ['Results']),
                    element('button', { class: 'close', 'data-dismiss': 'modal', 'aria-label': 'Close' }, [
                        element('span', { 'aria-hidden': true }, ['&times;'])
                    ])
                ]),
                div({ class: 'modal-body' }, [
                    element('p', null, ['Result Text'])
                ]),
                div({ class: 'modal-footer' }, [
                    element('button', { class: 'btn btn-secondary', 'data-dismiss': 'modal' }, ['Close'])
                ])
            ])
        ])
    ]);
}

const registerEvents = () => {
    const __form = document.querySelector('form');

    __form.addEventListener('submit', e => {
        e.preventDefault();

        const __deck = document.querySelector('.deck');
        const __cards = __deck.querySelectorAll('.card');

        const __postData = [];

        __cards.forEach((__card) => {
            __postData.push({ "suit": __card.dataset.cardSuit, "value": __card.dataset.cardValue });
        });

        postData(__form.getAttribute('action'), __postData, {
            'Content-Type': 'application/json',
            'RequestVerificationToken': __form.querySelector('input[type=hidden][name="__RequestVerificationToken"]').value
        })
            .then(data => {
                let modalEle = $('.modal');

                modalEle.find('.modal-body').text(`You have a ${data.type}`);
                modalEle.modal({
                    backdrop: 'static',
                    show: true
                });
                console.log(data);
            });

        //console.log(__cards);
    });

    document.body.appendChild(createModal());
}

window.addEventListener('load', registerEvents);