window.TextareaAutosize = (querySelector) => {
    const el = document.querySelector(querySelector);
    if (!el) return;
    
    el.setAttribute("style", "height:" + (!el.value ? 38 : el.scrollHeight) + "px;overflow-y:hidden;");
    // el.setAttribute("style", "height:" + (!el.value ? 38 : el.scrollHeight) + "px;overflow-y:hidden;");
    el.addEventListener('input', () => {
        el.style.height = 0 + 'px';
        el.style.height = el.scrollHeight + "px";
    })
}