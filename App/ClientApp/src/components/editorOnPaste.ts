export const editorOnPaste = (evt: any, editorRef: any) => {
    // Let inputs do their thing, so we don't break pasting of links.
    if (evt.target.nodeName === 'INPUT') return
    let text, onPasteStripFormattingIEPaste
    evt.preventDefault()
    evt.stopPropagation()
    if (evt.originalEvent && evt.originalEvent.clipboardData.getData) {
        text = evt.originalEvent.clipboardData.getData('text/plain')
        editorRef.value?.runCmd('insertText', text)
    }
    else if (evt.clipboardData && evt.clipboardData.getData) {
        text = evt.clipboardData.getData('text/plain')
        editorRef.value?.runCmd('insertText', text)
    }
    else { // @ts-ignore
        if (window.clipboardData && window.clipboardData.getData) {
                if (!onPasteStripFormattingIEPaste) {
                    onPasteStripFormattingIEPaste = true
                    editorRef.value?.runCmd('ms-pasteTextOnly', text)
                }
                onPasteStripFormattingIEPaste = false
            }
    }
}