import '../css/app.scss';
import './_image_cropper.js';

window.SetThemeColorMode = (modeName) => {
    try {
        const html = document.getElementsByTagName('html')[0];
        html.setAttribute('data-bs-theme', modeName);
        return true;
    } catch (e) {
        console.log(e);
    }
    
    return false;
}

// const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
// const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))