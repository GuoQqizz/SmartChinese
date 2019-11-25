onmessage = function (e) {
    var begin = new Date();
    while (new Date - begin < e.data) { };
    postMessage("timeover at" + new Date());
    close();
}