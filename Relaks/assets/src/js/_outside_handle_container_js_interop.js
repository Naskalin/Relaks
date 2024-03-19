window.OutsideClickHandler = (objectReference, data) => {
    const elementId = data.id;
    const excludedId = data.excludedId;
    const el = document.getElementById(elementId);
    
    if (!el) throw new Error(`OutsideClickHandler Error, el ${elementId} not found`);
    window.addEventListener("click", async (e) => {
        if (e.target.id === excludedId || e.target.closest(`#${excludedId}`)) return;
        if (!el.contains(e.target)) {
            await objectReference.invokeMethodAsync("InvokeClickOutside");
        }
    });
}