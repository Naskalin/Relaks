window.TextareaAutosize = (querySelector) => {
    const el = document.querySelector(querySelector);
    if (!el) return;

    const rows = parseInt(el.rows) || 1;
    const minHeight = 38 * rows;
    function getHeight()
    {
        return minHeight > el.scrollHeight ? minHeight : el.scrollHeight;
    }
    
    el.setAttribute("style", "height:" + (!el.value ? minHeight : getHeight()) + "px;overflow-y:hidden;");
    
    el.addEventListener('input', () => {
        el.style.height = 0 + 'px';
        el.style.height = getHeight() + "px";
    })
}