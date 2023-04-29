import '../css/app.scss';

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