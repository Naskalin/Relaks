window.ScrollToId = (selector, yOffset = 0) => {
    const el = document.getElementById(selector);
    const y = el.getBoundingClientRect().top + window.scrollY + yOffset;
    window.scrollTo({top: y, behavior: 'smooth'});
}