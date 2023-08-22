// check overflow
// https://github.com/anseki/leader-line/issues/252

const structureLines = [];
let lineEls = [];

window.UpdateStructureLines = () => {
    // перерисовываем стрелки
    const structureContainerEl = document.getElementById('js-structures-container');

    console.log(structureContainerEl.getBoundingClientRect())
    setTimeout(() => {
        structureLines.forEach(line => {
            line.position();
        })
    }, 10);

    // lineEls.forEach(lineEl => {
    //     if (checkOverflow(structureContainerEl, lineEl)) {
    //         lineEl.style.visibility = 'hidden';
    //         lineEl.style.opacity = '0';
    //     } else {
    //         lineEl.style.visibility = 'visible';
    //         lineEl.style.opacity = '1';
    //     }
    // });
}

window.InitializeStructureGroups = () => {
    const structureContainerEl = document.getElementById('js-structures-container');
    if (!structureContainerEl) throw new Error('js-structures-container not found');

    const zoneEl = document.getElementById('js-structures-zone');
    if (!zoneEl) throw new Error('js-structures-zone not found');

    // initialize arrows
    const childEls = document.querySelectorAll('[data-structure-group-arrow-parent-id]')
    childEls.forEach(childEl => {
        const parentId = childEl.getAttribute('data-structure-group-arrow-parent-id');
        structureLines.push(
            new LeaderLine({
                container: structureContainerEl,
                start: document.getElementById(parentId),
                end: childEl,
                startSocket: 'bottom',
                endSocket: 'top',
                path: 'straight',
                color: 'rgba(108,117,125,0.5)',
                size: 3,
            })
        );
    });

    // перемещает стрелки внутрь нашего контейнера
    lineEls = document.querySelectorAll('.leader-line');
    lineEls.forEach(line => structureContainerEl.appendChild(line));

    // перерисовываем стрелки при скроле
    structureContainerEl.addEventListener('scroll', AnimEvent.add(function() {
        structureLines.forEach(line => line.position())
    }), false);
}

// function checkOverflow(container, elem) {
//     const elemRect = elem.getBoundingClientRect(),
//         parentRect = container.getBoundingClientRect();
//     return elemRect.top < parentRect.top || elemRect.bottom > parentRect.bottom ||
//         elemRect.left < parentRect.left || elemRect.right > parentRect.right;
// }