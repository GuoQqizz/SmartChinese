/*
全局的ajax访问，处理ajax清求未登录
执行顺序
success->error->custom complete(ajax({}))  ->ajaxComplete(全局事件)
              ->global complete(ajaxSetup)
*/
$.ajaxSetup({
    complete: function (XMLHttpRequest, textStatus) {
        try {
            var status = XMLHttpRequest.status;
            var errorCode = XMLHttpRequest.responseJSON.ErrorCode;
            //console.log(errorCode);
            if (errorCode == 100) {
                //alert("请登录!");
                //如果超时就处理 ，指定要跳转的页面(比如登陆页)
                window.location.replace("/login/index");
            }
        } catch (e) {
        }
    },
    global: true,
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

function generateSuccessFunc(func, type) {
    type = type || 1;//1:alert 2:msg
    return function (data) {
        loading.showLoading();
        console.log(data);
        if (data.ErrorCode == 0) {
            if (data.State) {
                if (type == 1) {
                    window.layer.alert(data.ErrorMsg,
                   { icon: 1 },
                  func)
                } else if (type == 2) {
                    window.layer.msg(data.ErrorMsg,
                  { time: 1500 },
                 func)
                } else {
                    func();
                }
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