//全局的ajax访问，处理ajax清求未登录
$.ajaxSetup({
    complete: function (XMLHttpRequest, textStatus) {
        var status = XMLHttpRequest.status;
        var errorCode = XMLHttpRequest.responseJSON.ErrorCode;
        if (errorCode == 100) {
            //alert("请登录!");
            //如果超时就处理 ，指定要跳转的页面(比如登陆页)
            window.location.replace("/login/index");
        }
        if (window.localComplete && window.localComplete instanceof Function) {
            window.localComplete();
        }
    }
});


$(document).ajaxComplete(function (event, XMLHttpRequest) {//防止自定义ajax中重写complete方法。可以给document挂载ajaxComplete事件,会在complete方法后面执行,处理全局逻辑
    //console.log(arguments);
    try {
        var status = XMLHttpRequest.status;
        if (status == 500) {
            window.top.location.href = "/error/index";
        }
    } catch (e) {
    }
});
function generateErrorFunc(func) {
    return function (err) {
        loading.showLoading();
        console.error(err);
        if (!func) {
            func = function () { window.layer.close(index); }
        }
        var index = window.layer.alert("异常，请重试",
             { icon: 2 }, //0 警告 1 成功 2 错误
             func);
    }
}

function generateSuccessFunc(func) {
    return function (data) {
        loading.showLoading();
        console.log(data);
        if (data.ErrorCode == 0) {
            if (data.State) {
                window.layer.msg(data.ErrorMsg,
                    { time: 1500 },
                   func)
            } else {
                var index = window.layer.alert(data.ErrorMsg,
                    { icon: 0 },
                    function () { window.layer.close(index); });
            }
        } else {
            let msg = '异常，请重试';
            if (data.ErrorCode <= 10000) {
                msg = data.ErrorMsg;
            }
            var index = window.layer.alert(msg,
                    { icon: 0 },
                    function () { window.layer.close(index); });
        }

    }
}