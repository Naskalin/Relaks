import 'arrows-svg'

// check overflow
// https://github.com/anseki/leader-line/issues/252

// const structureLines = [];
// let lineEls = [];
//
// window.UpdateStructureLines = () => {
//     setTimeout(() => {
//         structureLines.forEach(line => {
//             line.position();
//         })
//     }, 10);
// }
//
// window.InitializeStructureGroups = () => {
//     const structureContainerEl = document.getElementById('js-structures-container');
//     if (!structureContainerEl) throw new Error('js-structures-container not found');
//
//     const zoneEl = document.getElementById('js-structures-zone');
//     if (!zoneEl) throw new Error('js-structures-zone not found');
//
//     // initialize arrows
//     const childEls = document.querySelectorAll('[data-structure-group-arrow-parent-id]')
//     childEls.forEach(childEl => {
//         const parentId = childEl.getAttribute('data-structure-group-arrow-parent-id');
//         structureLines.push(
//             new LeaderLine({
//                 start: document.getElementById(parentId),
//                 end: childEl,
//                 startSocket: 'bottom',
//                 endSocket: 'top',
//                 path: 'straight',
//                 color: 'rgba(108,117,125,0.5)',
//                 size: 3,
//             })
//         );
//     });
//
//     // перемещает стрелки внутрь нашего контейнера
//     lineEls = document.querySelectorAll('.leader-line');
//     lineEls.forEach(line => structureContainerEl.appendChild(line));
//
//     // перерисовываем стрелки при скроле
//     structureContainerEl.addEventListener('scroll', AnimEvent.add(function() {
//         structureLines.forEach(line => line.position())
//     }), false);
// }

let storeArrowEls = [];

window.RemoveStructureArrows = () => {
    storeArrowEls.forEach(arrow => arrow.clear());
}
// https://github.com/sasza2/arrows
window.InitializeStructureArrows = () => {
    // очищаем временное хранилище стрелок
    storeArrowEls = [];
    const structureContainerEl = document.getElementById('js-structures-container');

    // initialize arrows
    const childEls = document.querySelectorAll('[data-structure-group-arrow-parent-id]')
    childEls.forEach(childEl => {
        const parentId = childEl.getAttribute('data-structure-group-arrow-parent-id');

        const arrow = arrowCreate({
            head: 'none',
            from: {
                node: document.getElementById(parentId),
                direction: 'bottom',
                translation: [0, 0],
            },
            to: {
                node: childEl,
                direction: 'top',
                translation: [0, 0],
            },
        })

        storeArrowEls.push(arrow);
        structureContainerEl.appendChild(arrow.node);
    });
}