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



window.downloadAll = () => {

    const links = [
        'http://localhost:5000/2023/6/12/uogj2dml.ayz.jpg',
        'http://localhost:5000/2023/8/8/zvp3xfqj.ori.jpg',
    ];
    
    let link = document.createElement('a');

    link.setAttribute('download', null);
    link.style.display = 'none';

    document.body.appendChild(link);

    for (let i = 0; i < links.length; i++) {
        link.setAttribute('href', links[i]);
        link.click();
    }

    document.body.removeChild(link);
}