var btnobj = null;
var flag = 0;
$(function () {
    $("#switchCode").click(function () {
        $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
    });

    $('.codepopsub').click(function () {
        code($(this));
    })
    $('.closepaydepopimg').click(function () {
        $('.codewindow').hide();
        $('.codeerror').hide();
    })
});

$('.feachBtn').click(function () {
    feach($(this));
});

function feach(_this) {
    let phone = $('.mobilevalue').val().trim();
    if (isPoneAvailable(phone)) {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的手机号');
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        return;
    }

    //刷新验证码
    $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());

    //图文验证码弹框
    $(".codewindow").show();
    $("#txt_Code").val("");

    btnobj = _this;
}

function code(_self) {
    if (btnobj.attr('status') == '0') {
        return false;
    }
    getcode(_self);
};

//获取验证码
function getcode(_self) {
    if (flag == 1) {
        return;
    }
    flag = 1;

    let phone = $('.mobilevalue').val().trim();
    let authCode = $('#txt_Code').val().trim();
    btnobj.html("正在发送...");

    _self.html('发送中...');
    _self.off('click');

    $.ajax({
        type: "post",
        async: true,
        url: "/Sms/SendValidCode",
        data: {
            phone: phone,
            authCode: authCode,
            type: 3
        },
        success: function (data) {
            if (data.State == 1) {
                btnobj.off('click');
                $('.codewindow').hide();
                $('.codeerror').hide();

                btnobj.parent().next().children().hide();
                var num = 61;
                var timer = setInterval(function () {
                    if (num > 0) {
                        num--;
                        btnobj.html("重新发送(" + num + ")").attr("status", "0");
                        btnobj.css('color', '#707070');
                    } else {
                        btnobj.html("重新获取").removeAttr("status", '1');
                        btnobj.css('color', '#48b0a7');
                        clearInterval(timer);
                        btnobj.on('click', function () { feach(btnobj); });
                    }
                }, 1000);
            }
            if (data.ErrorCode == 10000) {
                //$('.codewindow').hide();
                //$('.codeerror').hide();
                $('.codeerror').show();
                $('.codeerror').find('span').html('网络连接超时，请稍后重试');

                //$('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
                //$('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
                btnobj.css('color', '#48b0a7')
                btnobj.html('发送验证码');
            }
            if (data.ErrorCode == 10007) {
                btnobj.html("发送验证码");
                $('.codeerror').find('span').html('验证码错误');
                $('.codeerror').show();
                $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
            }
            _self.html("确定");
            _self.on("click", function () { code(_self) });
            flag = 0;
        },
        error: function () {
            //$('.codewindow').hide();
            //$('.codeerror').hide();

            flag = 0;
            btnobj.html("发送验证码");
            //$('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
            //$('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
            $('.codeerror').show();
            $('.codeerror').find('span').html('网络连接超时，请稍后重试');
            _self.html("确定");
            _self.on("click", function () { code(_self) });
        },
    });
}

$(".passwordvalue").on("input propertychange", function () {
    if ($(this).val().trim().length < 9 || $(this).val().trim().length > 32) {
        $(this).next('.successtip').hide();
    } else {
        $(this).next('.successtip').show();
    }
})
$(".passwordvalue1").on("input propertychange", function () {
    $('.passwordvalue1').parents('.logininp').next('.errormsg').find('div').hide();
})
$(".checkempty").on("input propertychange", function () {
    var max = $(this).attr('max');
    var min = $(this).attr('min');
    var lth = $(this).attr('lth');
    if (lth > 0) {
        console.log(lth);
        if ($(this).val().trim().length == lth) {
            $(this).next('.successtip').show();
        } else {
            $(this).next('.successtip').hide();
        }
    } else {
        if ($(this).val().trim().length < max && $(this).val().trim().length > min) {
            $(this).next('.successtip').show();
        } else {
            $(this).next('.successtip').hide();
        }
    }
})
$(".checkmobile").on("input propertychange", function () {
    if (isPoneAvailable($(this).val().trim())) {
        $(this).next('.successtip').show();
    } else {
        $(this).next('.successtip').hide();
    }
})
$(".mobilevalue").on("input propertychange", function () {
    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
})
function isPoneAvailable(val) {
    var myreg = /^[1][3,4,5,6,7,8,9][0-9]{9}$/;
    if (!myreg.test(val)) {
        return false;
    } else {
        return true;
    }
}

$('.btnSave').click(function () {
    save($(this));
})
function save(_this) {
    var data = {};
    var lock = 1;
    var mobile = $('.mobilevalue').val().trim();
    var password = $('.passwordvalue').val().trim();
    var password1 = $('.passwordvalue1').val().trim();
    var code = $('.codevalue').val().trim();
    if (password == password1) {
        $('.passwordvalue1').parents('.logininp').next('.errormsg').find('div').hide();
    }
    else {
        $('.passwordvalue1').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (isPoneAvailable(mobile)) {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的手机号');
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (password.length > 5) {
        $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (code.length == 4) {
        $('.codevalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的验证码');
        $('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    data.Phone = mobile;
    data.SmsCode = code;
    data.Password = password;
    data.ConfirmPassword = password1;

    if (lock == 0) {
        return false;
    }

    _this.html("提交中...");
    _this.off('click');

    layer.msg('提交中。。。', { time: 500 });
    $.ajax({
        type: "post",
        async: true,
        url: "/Login/UpdatePwd",
        data: data,
        success: function (data) {
            if (data.ErrorCode == 10004) {
                $('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('验证码错误');
                $('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
            }
            if (data.ErrorCode == 1) {
                layer.msg("操作成功",
                        { icon: 1, time: 1000 },
                        function () {
                            location.href = '/Login/LoginIndex';
                        });
            }
            _this.html("确定");
            _this.on('click', function () { save(_this) });
        },
        error: function () {
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
            //layer.msg('网络连接超时，请稍后重试', { icon: 2, time: 2000 });
        },
    });
}
$("#txt_Code").on("input propertychange", function () {
    $('.codeerror').hide();
})