//节流
function throttle(func, delay) {
    delay = delay || 650;
    let last = 0;
    return function (...args) {
        let now = Date.now()
        if (now - last > delay) {
            last = now;
            func.apply(this, args);
        }
    }
}

//防抖
function debounce(func, delay) {
    delay = delay || 650;
    let timer = 0;
    return function (...args) {
        //console.log(arg);
        if (timer) {
            clearTimeout(timer);
        }
        timer = setTimeout(function () {
            func.apply(this, args)
        }, delay);
    }
}




//发短信事件
//type取值
//学生未注册 = 1,
//学生已注册 = 2,
//学校教师未注册 = 3,
//学校教师已注册 = 4,
//系统教师未注册 = 5,
//系统教师已注册 = 6,
//其他 = 7, 其他不判断,直接发
function sendMsg(e, type) {
    let timeSpan = 60;
    let sendStatus = 1;//1 发送中,2成功，3失败
    let i = 0;
    let second = timeSpan + 2;
    let phoneClassName = $(e).data('from') || 'sms-phone';
    let phone = $('.' + phoneClassName).val();
    if (!vaildatePhone(phone) || !$('.' + phoneClassName).valid()) {
        if (window.layer) {
            var index = window.layer.msg('请输入正确的手机号',
                        { time: 1500 },
                        function () { window.layer.close(index); });
        } else {
            alert('请输入正确的手机号');
        }
        return false;
    }
    $(e).attr({ "disabled": "disabled" });
    $.ajax({
        url: '/Sms/SendValidCode',
        type: 'POST',
        data: { phone: phone, type: type },
        success: function (data) {
            let success = data.Status == 1;
            let msg = success ? "发送成功" : data.ErrorMsg;
            sendStatus = success ? 2 : 3;
            if (window.layer) {
                setTimeout(function () {
                    var index = window.layer.msg(msg,
                                { time: 1500 },
                                function () { window.layer.close(index); });
                }, 2000)

            } else {
                alert(msg);
            }
        },
        error: function (err) {
            sendStatus = 3;
            console.log(err);
            if (window.layer) {
                var index = window.layer.alert("异常，请重试",
                 { icon: 2 },
                 function () { window.layer.close(index); });
            }
        }
    })

    countDown();
    i = setInterval(countDown, 1000);
    function countDown() {
        switch (sendStatus) {
            case 1:
                $(e).html(`发送中...`);
                break
            case 2:
                if (second <= timeSpan) {
                    $(e).html(`${second}s后重发`);
                }
                if (second == 0) {
                    clearInterval(i);
                    $(e).removeAttr("disabled").html("发送验证码");
                }
                second--;
                break;
            case 3:
                $(e).removeAttr("disabled").html(`重发验证码`);
                break;
            default:

        }

    }
}

function vaildatePhone(phone) {
    var mobile = /^^1[3456789]\d{9}$/;
    return phone && phone.length == 11 && mobile.test(phone);
}

window.loading = function () {
    let loadingFlag;
}
loading.showLoading = function (msg) {
    msg = msg || '正在读取数据，请稍候……'
    if (window.layer) {
        //注意，layer.msg默认3秒自动关闭，如果数据加载耗时比较长，需要设置time
        this.loadingFlag = window.layer.msg(msg, { icon: 16, shade: 0.01, shadeClose: false, time: 10000 });
    }
}
loading.closeLoading = function () {
    if (window.layer && this.loadingFlag) {
        window.layer.close(this.loadingFlag);
    }
}

//数组去重
if (!Array.prototype.unique) {
    Array.prototype.unique = function () {
        var $this = this;
        if (!Array.isArray($this)) {
            console.log('type error!')
            return [];
        }
        var array = [];
        for (var i = 0; i < $this.length; i++) {
            if (array.indexOf($this[i]) === -1) {
                array.push($this[i])
            }
        }
        return array;
    }
}

//阻止默认事件函数
function stopDefault(e) {
    if (e && e.preventDefault)
        e.preventDefault();
    else
        window.event.returnValue = false; //兼容IE
}