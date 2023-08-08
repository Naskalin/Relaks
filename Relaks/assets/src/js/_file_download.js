window.triggerFileDownload = (fileName, url) => {
    const anchorEl = document.createElement('a');
    anchorEl.href = url;
    anchorEl.download = fileName ?? '';
    anchorEl.click();
    anchorEl.remove();
}

// window.triggerFileDownloadMultiple = (urlWithNames) => {
//    
//     // const anchorEl = document.createElement('a');
//     // var zip = new JSZip();
//     // let count = 0;
//     // Object.keys(urlWithNames).forEach(url => {
//     //     JSZipUtils.getBinaryContent(url, function (err, data) {
//     //         if(err) {
//     //             throw err; // or handle the error
//     //         }
//     //         zip.file(urlWithNames[url], data, {binary:true});
//     //         count++;
//     //         if (count === Object.keys(urlWithNames).length) {
//     //             zip.generateAsync({type:'blob'}).then(function(content) {
//     //                 saveAs(content, 'test.zip');
//     //             });
//     //         }
//     //     });
//     // })
//     //
//     // anchorEl.remove();
// }