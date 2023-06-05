import Sortable from 'sortablejs';

// var el = document.getElementById('js-sortable');
// var sortable = Sortable.create('js-sortable');

window.SortableJsInit = (data) => {
    console.log(data['elementId'])
    const el = document.getElementById(data['elementId']);
    if (!el) throw new Error(`Sortable element not found`);
    window.SortableJs = Sortable.create(el);

    // const imgId = 'img-' + data['blockId'];
    // let imgEl = document.getElementById(imgId);
    // if (!imgEl) {
    //     imgEl = document.createElement('img');
    //     imgEl.setAttribute('id', imgId);
    //     blockEl.appendChild(imgEl);
    // }
    // imgEl.setAttribute('src', data['imgSrc']);
    //
    // // show preview
    // const previewEl = document.getElementById(`preview-${data['blockId']}`);
    // if (previewEl) {
    //     previewEl.style.cssText = 'overflow: hidden;width: 150px; height: 150px;display: block;border: 1px solid var(--bs-border-color);'
    // }
    //
    // if (window.CropperJs) {
    //     window.CropperJs.destroy();
    // }
    //
    // window.CropperJs = new Cropper(imgEl, {
    //     viewMode: 1,
    //     dragMode: 'move',
    //     aspectRatio: 1,
    //     preview: previewEl,
    // })
};