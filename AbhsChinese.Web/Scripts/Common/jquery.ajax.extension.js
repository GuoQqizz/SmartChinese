
$.ajaxSetup({
    error: function (err) {
        console.error(err);
        window.location.href = '/error/index';
    }
});

$(document).ajaxComplete(function (event, XMLHttpRequest) {//防止自定义ajax中重写complete方法。可以给document挂载ajaxComplete事件,会在complete方法后面执行,处理全局逻辑
    console.log(arguments);
    try {
        var status = XMLHttpRequest.status;
        if (status == 500) {
            window.top.location.href = "/error/index";
        }
    } catch (e) {
    }
});