const structureLines = [];

window.UpdateStructureLines = () => {
    const structureContainerEl = document.getElementById('js-structures-container');
    
    setTimeout(() => {
        structureLines.forEach(line => line.position())
    }, 10);
}

window.InitializeStructureGroups = () => {
    const structureContainerEl = document.getElementById('js-structures-container');
    if (!structureContainerEl) throw new Error('js-structures-container not found');
    
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

    const lineEls = document.querySelectorAll('.leader-line');
    lineEls.forEach(line => structureContainerEl.appendChild(line));

    structureContainerEl.addEventListener('scroll', AnimEvent.add(function() {
        // window.UpdateStructureLines();
        structureLines.forEach(line => line.position())
    }), false);
}

// // https://javascript.info/mouse-drag-and-drop
// https://www.w3schools.com/howto/howto_js_draggable.asp
// window.initializeDraggableZone = () => {
//     const draggableZoneEl = document.querySelector("[data-js-draggable-zone]");
//     if (draggableZoneEl) {
//         const parentEl = draggableZoneEl.parentElement;
//         if (!parentEl) {
//             throw new Error('initializeDraggableZone parentEl not found');
//         }
//         parentEl.style.width = `${parentEl.offsetWidth}px`;
//         parentEl.style.height = `${parentEl.offsetHeight}px`;
//        
//         draggableZoneEl.onmousedown = function(event) {
//
//             let shiftX = event.clientX - draggableZoneEl.getBoundingClientRect().left;
//             let shiftY = event.clientY - draggableZoneEl.getBoundingClientRect().top;
//             draggableZoneEl.style.position = 'absolute';
//             draggableZoneEl.style.zIndex = 1000;
//             // document.body.append(draggableZoneEl);
//
//             moveAt(event.pageX, event.pageY);
//
//             function moveAt(pageX, pageY) {
//                 draggableZoneEl.style.left = pageX - shiftX + 'px';
//                 draggableZoneEl.style.top = pageY - shiftY + 'px';
//             }
//
//             function onMouseMove(event) {
//                 moveAt(event.pageX, event.pageY);
//             }
//
//             document.addEventListener('mousemove', onMouseMove);
//
//             draggableZoneEl.onmouseup = function() {
//                 document.removeEventListener('mousemove', onMouseMove);
//                 draggableZoneEl.onmouseup = null;
//             };
//
//         };
//
//         draggableZoneEl.ondragstart = function() {
//             return false;
//         };
//
//     }   
// }