var facetimes = 10;
var obj = null;
var flag = 0;
var meth = 1;
$(function () {
    let active = $("#active").val().trim();
    if (active == 1) {
        facetimes = 10;
        //启动摄像头
        $('#face-box').show();
        Webcam.set({
            width: 334,
            height: 251,
            flip_horiz: true,
            noStream: function (err) {
                console(err);
            }
        });
        Webcam.attach('#my_camera');
    }
});

//登录方式切换
$('.loginpage .logintype .typeitem').click(function () {
    $('.loginpage .logintype .typeitem').show();
    $(this).hide();
    $('.loginc').find('.itembox').hide();
    $('.loginc').find('.itembox').eq($(this).index()).show();
    var method = $(this).attr('status');//获取登录方式  1：账号密码登录 2：人脸识别登录 3：短信登录
    meth = method;
    if (method == 2) {
        facetimes = 10;
        $('.faceboxc').show();
        $('.faceboxc1').hide();
        //启动摄像头
        $('#face-box').show();
        Webcam.set({
            width: 334,
            height: 251,
            flip_horiz: true,
            noStream: function (err) {
                //console(err);
            }
        });
        Webcam.attach('#my_camera');
    }
    else {
        facetimes = 0;
        Webcam.dispatch('#my_camera');
    }
});

//人脸登录
function Facelogin() {
    //facetimes为0，停止扫描
    if (facetimes == 0) {
        $('.faceboxc').hide();
        $('.faceboxc1').show();
        return;
    }
    //拍照发送图片信息
    Webcam.snap(function (data_uri) {
        $('.pane').addClass('move')
        $('.faceimg1').attr('src', data_uri);
        var img = data_uri.split(',')[1];

        facetimes--;
        $.ajax({
            type: "post",
            url: "/Login/FaceLogin",
            data: {
                "ImgPath": img,
                "ImgType": "BASE64"
            },
            success: function (data) {
                if (data.State == true) {
                    $('.faceboxc').hide();
                    $('.faceboxc2').show();
                    location.href = "/LearningCenter/Index";
                    return;
                }
                else {
                    if (data.ErrorCode = -1) {
                        setTimeout(Facelogin, 500);//未识别成功
                    }
                    else if (data.ErrorCode == -2) {
                        setTimeout(Facelogin, 1000);//频率过快  停止1s
                    }
                }
            },
            error: function () {
                setTimeout(Facelogin, 500);//未识别成功
            },
            async: true
        })
    });
}

//重新扫描人脸，再次验证
$('.startface').click(function () {
    facetimes = 10;
    $('.faceboxc').show();
    $('.faceboxc1').hide();
    Facelogin();
});

//手机验证
function isPhoneAvailable(val) {
    var myreg = /^[1][3,4,5,6,7,8,9][0-9]{9}$/;
    if (!myreg.test(val)) {
        return false;
    } else {
        return true;
    }
}

//图文验证码切换
$("#switchCode").click(function () {
    $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
});
//关闭图文验证码
$('.closepaydepopimg').click(function () {
    $('.codewindow').hide();
    $('.codeerror').hide();
});
//发送短信按钮点击
$('.feachBtn').click(function () {
    feach($(this));
});
//打开图文验证码弹框
function feach(_this) {
    let phone = $('.mobilevalue').val().trim();
    if (isPhoneAvailable(phone)) {
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

    $('#btnok2').hide();
    $('#btnok1').show();

    obj = _this;
}
//图文验证码确定
$('#btnok1').click(function () {
    code($(this));
});
function code(_self) {
    if (obj.attr('status') == '0') {
        return false;
    }
    getcode(_self);
};
//获取短信验证码
function getcode(_self) {
    if (flag == 1) {
        return;
    }
    flag = 1;

    let phone = $('.mobilevalue').val().trim();
    let authCode = $('#txt_Code').val().trim();
    if (phone == '') {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').html('请输入正确的手机号码');
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        return;
    }
    _self.children().html("发送中...");
    _self.off('click');

    obj.html("正在发送...");
    $.ajax({
        type: "post",
        async: true,
        url: "/Sms/SendValidCode",
        data: {
            phone: phone,
            authCode: authCode,
            type: 2
        },
        success: function (data) {
            if (data.State == 1) {
                obj.off('click');
                $('.codewindow').hide();
                $('.codeerror').hide();

                obj.parent().next().children().hide();
                var num = 61;
                var timer = setInterval(function () {
                    if (num > 0) {
                        num--;
                        obj.html("重新发送(" + num + ")").attr("status", "0");
                        obj.css('color', '#707070');
                    } else {
                        obj.html("重新获取").removeAttr("status", '1');
                        obj.css('color', '#48b0a7');
                        clearInterval(timer);
                        obj.on('click', function () { feach(obj); });
                    }
                }, 1000);
            }
            if (data.ErrorCode == 10000) {
                //$('.codewindow').hide();
                //$('.codeerror').hide();
                //$('.smsCode').parents('.logininp').next('.errormsg').find('.errortext').children('span').html('网络连接超时，请稍后重试');
                $('.codeerror').find('span').html('网络连接超时，请稍后重试');
                $('.codeerror').show();
                obj.css('color', '#48b0a7')
                obj.html('发送验证码');
            }
            if (data.ErrorCode == 10007) {
                obj.html("发送验证码");
                $('.codeerror').find('span').html('验证码错误');
                $('.codeerror').show();
                $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
            }
            _self.children().html("确定");
            _self.on('click', function () { code(_self) });
            flag = 0;
        },
        error: function () {
            //$('.codewindow').hide();
            //$('.codeerror').hide();
            $('.codeerror').find('span').html('网络连接超时，请稍后重试');
            $('.codeerror').show();

            _self.children().html("确定");
            _self.on('click', function () { code(_self) });

            flag = 0;
            obj.html("发送验证码");
        },
    })
}

//点击登录
$('#btnlogin1').click(function () {
    login($(this));
});
document.onkeydown = function (e) { // 回车提交表单
    // 兼容FF和IE和Opera
    var theEvent = window.event || e;
    var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
    if (code == 13) {
        if (meth == 1)//1为账号密码登录
        {
            login($('#btnlogin1'));
        }
        else if (meth == 3) { //3为手机验证码登录
            mobileLogin($('.login_act1'));
        }
    }
}
function login(_this) {
    var data = {};
    var lock = 1;
    var mobile = $('.mobilevalue1').val().trim();
    var password = $('.passwordvalue').val();
    if (isPhoneAvailable(mobile)) {
        $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $("#errorTip").html("请输入正确的手机号码");
        $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (password.length > 5) {
        $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    data.PassportKey = mobile;
    data.Password = password;
    if (lock == 0) {
        return false;
    }

    var _self = _this;
    _self.html("登录中...");
    _self.attr('disabled', true);

    $.ajax({
        type: "post",
        async: true,
        url: "/Login/CheckLogin",
        data: data,
        success: function (data) {
            _self.html("登录");
            _self.removeAttr('disabled');

            if (data.ErrorCode == 1) {
                _self.html("登录成功");
                _self.attr('disabled', true);
                layer.msg("登录成功",
                        { icon: 1, time: 500 },
                        function () {
                            location.href = "/LearningCenter/Index";
                        });
            }
            else if (data.ErrorCode == 10000) {
                $("#errorTip").html("网络连接超时，请稍后重试");
                $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
            }
            else if (data.ErrorCode == 0) {
                $("#errorTip").html("用户名或密码错误");
                $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
            }
            else if (data.ErrorCode == 3) {
                $("#errorTip").html("用户名或密码错误");
                $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
                $('#btnlogin1').hide();
                $('#btnlogin2').show();
            }
        },
        error: function () {
            _self.html("登录");
            _self.removeAttr('disabled');

            $("#errorTip").html("网络连接超时，请稍后重试");
            $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
        },
    });
}

//连续三次输错密码之后弹出图文验证码
$('#btnlogin2').click(function () {
    var lock = 1;
    var mobile = $('.mobilevalue1').val().trim();
    var password = $('.passwordvalue').val();
    if (isPhoneAvailable(mobile)) {
        $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $("#errorTip").html("请输入正确的手机号码");
        $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (password.length > 5) {
        $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (lock == 0) {
        return false;
    }

    //刷新验证码
    $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());

    //显示图文验证码
    $(".codewindow").show();
    $("#txt_Code").val("");
    $('#btnok2').show();
    $('#btnok1').hide();
});

//输错三次之后图文验证码的确定
$('#btnok2').click(function () {
    if ($("#txt_Code").val().trim() == "") {
        $('.codeerror').find('span').html('验证码错误');
        $('.codeerror').show();
        return;
    }
    var info = {};
    info.PassportKey = $('.mobilevalue1').val().trim();
    info.Password = $('.passwordvalue').val();
    info.SmsCode = $("#txt_Code").val().trim();

    var _self = $(this).children();
    _self.html("登录中...");
    _self.attr('disabled', true);
    $.ajax({
        type: "post",
        async: true,
        url: "/Login/CheckLogin",
        data: info,
        success: function (data) {
            _self.html("登录");
            _self.removeAttr('disabled');
            $('.codewindow').hide();
            $('.codeerror').hide();

            if (data.ErrorCode == 1) {
                _self.html("登录成功");
                _self.attr('disabled', true);

                layer.msg("登录成功",
                        { icon: 1, time: 500 },
                        function () {
                            location.href = "/LearningCenter/Index";
                        });
            }
            else if (data.ErrorCode == 10000) {
                $("#errorTip").html("网络连接超时，请稍后重试");
                $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
            }
            else if (data.ErrorCode == 10007) {
                $('.codewindow').show();
                $('.codeerror').find('span').html('验证码错误');
                $('.codeerror').show();
                $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
            }
            else if (data.ErrorCode == 0) {
                $("#errorTip").html("用户名或密码错误");
                $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
            }
            else if (data.ErrorCode == 3) {
                $("#errorTip").html("用户名或密码错误");
                $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
                $('#btnlogin1').hide();
                $('#btnlogin2').show();
            }
        },
        error: function () {
            $('.codewindow').hide();
            $('.codeerror').hide();

            _self.html("登录");
            _self.removeAttr('disabled');

            $("#errorTip").html("网络连接超时，请稍后重试");
            $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').show();
        },
    })
})

//手机验证码登录
$('.login_act1').click(function () {
    mobileLogin($(this));
});

function mobileLogin(_this) {
    var data = {};
    var lock = 1;
    var mobile = $('.mobilevalue').val().trim();
    var code = $('.smsCode').val().trim();
    if (isPhoneAvailable(mobile)) {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    if (code.length == 4) {
        $('.smsCode').parents('.logininp').next('.errormsg').find('div').hide();
    } else {
        $('.smsCode').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入验证码');
        $('.smsCode').parents('.logininp').next('.errormsg').find('div').show();
        lock = 0;
    }
    data.Phone = mobile;
    data.SmsCode = code;
    if (lock == 0) {
        return false;
    }
    var _self = _this;
    _self.html("登录中...");
    _self.attr('disabled', true);
    $.ajax({
        type: "post",
        async: true,
        url: "/Login/MobileCodeLogin",
        data: data,
        success: function (data) {
            _self.html("登录");
            _self.removeAttr('disabled');
            if (data.ErrorCode == 1) {
                layer.msg("登录成功",
                        { icon: 1, time: 500 },
                        function () {
                            location.href = "/LearningCenter/Index";
                        });
            }
            else if (data.ErrorCode == 10004) {
                $('.smsCode').parents('.logininp').next('.errormsg').find('div').children('span').html('验证码错误');
                $('.smsCode').parents('.logininp').next('.errormsg').find('div').show();
            }
            else {
                $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('该手机号不存在，请先注册');
                $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
            }
        },
        error: function () {
            _self.html("登录");
            _self.removeAttr('disabled');
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        },
    });
}

$(".smsCode").on("input propertychange", function () {
    $('.smsCode').parents('.logininp').next('.errormsg').find('div').hide();
});

$(".mobilevalue").on("input propertychange", function () {
    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
});

$(".mobilevalue1").on("input propertychange", function () {
    $("#errorTip").html("请输入正确的手机号码");
    $('.mobilevalue1').parents('.logininp').next('.errormsg').find('div').hide();
    if (isPhoneAvailable($(this).val().trim())) {
        $(this).next('.successtip').show();
    } else {
        $(this).next('.successtip').hide();
    }
});

$(".passwordvalue").on("input propertychange", function () {
    $('.passwordvalue').parents('.logininp').next('.errormsg').find('div').hide();
    if ($(this).val().trim().length < 9 || $(this).val().trim().length > 32) {
        $(this).next('.successtip').hide();
    } else {
        $(this).next('.successtip').show();
    }
});

$("#txt_Code").on("input propertychange", function () {
    $('.codeerror').hide();
});

function closeVideo() {
    $('.chosewindow2').hide();
    facetimes = 0;
    Webcam.dispatch('#my_camera');
    $('.itembox').eq(0).show();
    $('.itembox').eq(1).hide();
    $('.typeitem').eq(0).hide();
    $('.typeitem').eq(1).show();
}