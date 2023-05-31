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
    
    if (window.CropperJs) {
        window.CropperJs.destroy();
    }
    
    window.CropperJs = new Cropper(imgEl, {
        viewMode: 1,
        dragMode: 'move',
        aspectRatio: 1,
    })
};

window.CropperJsCrop = () => {
    if (!window.CropperJs) throw new Error('window.CropperJs is not initialized');
    
    // обрезанное изображение
    const result = window.CropperJs.getCroppedCanvas({
        width: 300,
        height: 300,
        imageSmoothingQuality: 'low',
        
    }).toDataURL();

    // удаляем исходное изображение
    const cropperImgEl = document.getElementById('img-' + window.CropperJs.container.id);
    if (cropperImgEl) window.CropperJs.container.removeChild(cropperImgEl);
    
    window.CropperJs.destroy();
    
    return result;
};