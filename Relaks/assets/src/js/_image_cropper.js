import Cropper from 'cropperjs';

window.CropperJsInit = (data) => {
    const blockEl = document.getElementById(data['blockId']);
    if (!blockEl) throw new Error(`Cropper block not found`);

    const imgId = 'img-' + data['blockId'];
    let imgEl = document.getElementById(imgId);
    if (!imgEl) {
        imgEl = document.createElement('img');
        imgEl.setAttribute('id', imgId);
        blockEl.appendChild(imgEl);
    }
    imgEl.setAttribute('src', data['imgSrc']);

    // show preview
    const previewEl = document.getElementById(`preview-${data['blockId']}`);
    if (previewEl) {
        previewEl.style.cssText = 'overflow: hidden;width: 150px; height: 150px;display: block;border: 1px solid var(--bs-border-color);'
    }
    
    if (window.CropperJs) {
        window.CropperJs.destroy();
    }
    
    window.CropperJs = new Cropper(imgEl, {
        viewMode: 1,
        dragMode: 'move',
        aspectRatio: 1,
        preview: previewEl,
    })
};

window.CropperJsCrop = () => {
    if (!window.CropperJs) throw new Error('window.CropperJs is not initialized');
    
    // обрезанное изображение
    const result = window.CropperJs.getCroppedCanvas({
        width: 300,
        height: 300,
        imageSmoothingQuality: 'high',
        
    }).toDataURL('image/webp', .95);

    // hide preview
    const previewEl = document.getElementById(`preview-${window.CropperJs.container.id}`);
    if (previewEl) {
        previewEl.style.display = 'none';
    }

    // удаляем исходное изображение
    const cropperImgEl = document.getElementById('img-' + window.CropperJs.container.id);
    if (cropperImgEl) window.CropperJs.container.removeChild(cropperImgEl);
    
    window.CropperJs.destroy();
    
    return result;
};