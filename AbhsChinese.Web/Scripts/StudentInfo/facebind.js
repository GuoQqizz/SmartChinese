var facetimes = 10;
var btnObj = null;
var flag = 0;
$(function () {
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

    $("#switchCode").click(function () {
        $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
    });
});
function Facelogin() { };

$('.collface').click(function () {
    Webcam.snap(function (data_uri) {
        $('.pane').addClass('move')
        var img = data_uri.split(',')[1];
        var faceimg = data_uri.split(',')[1];

        $.ajax({
            type: "post",
            url: "/SecurityCenter/FaceCheck",
            data: {
                "imgpath": faceimg,
                "imgType": "BASE64"
            },
            success: function (data) {
                debugger;
                if (data.State == true) {
                    $('#imgstatus').attr('src', '/Images/StudentInfo/success.png');
                    $('.texttip').html('人脸样本采集成功');
                    $('.nextstep_1').hide();
                    $('.nextstep').show();
                }
                else {
                    $('#imgstatus').attr('src', '/Images/StudentInfo/error.png');
                    $('.texttip').html('检测失败，请重新采集');
                    $('.nextstep_1').show();
                    $('.nextstep').hide();
                }
                $('.faceimg1').attr('src', data_uri);
                $('.faceboxc').hide();
                $('.faceboxc1').show();
            },
            error: function () {
                $('#imgstatus').attr('src', '/Images/StudentInfo/error.png');
                $('.texttip').html('检测失败，请重新采集');
                $('.nextstep_1').show();
                $('.nextstep').hide();
            },
            async: false
        });
    });
})
//发送短信验证码
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
    $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
    //图文验证码弹框
    $(".codewindow").show();
    $("#txt_Code").val("");

    btnObj = _this;
}
$('.codepopsub').click(function () {
    code($(this));
})
function code(_self) {
    if (btnObj.attr('status') == '0') {
        return false;
    }
    getcode(_self);
};
function getcode(_self) {
    if (flag == 1) {
        return;
    }
    flag = 1;

    let phone = $('.mobilevalue').val().trim();
    let authCode = $('#txt_Code').val().trim();
    if (phone == '') {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的手机号码');
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        return;
    }
    _self.html('发送中...');
    _self.off('click');

    btnObj.html("正在发送...");
    $.ajax({
        type: "post",
        async: true,
        url: "/Sms/SendValidCode",
        data: {
            phone: phone,
            authCode: authCode,
            type:2
        },
        success: function (data) {
            if (data.State == 1) {
                btnObj.off('click');
                $('.codewindow').hide();
                $('.codeerror').hide();

                btnObj.parent().next().children().hide();
                var num = 61;
                var timer = setInterval(function () {
                    if (num > 0) {
                        num--;
                        btnObj.html("重新发送(" + num + ")").attr("status", "0");
                        btnObj.css('color', '#707070');
                    } else {
                        btnObj.html("重新获取").removeAttr("status", '1');
                        btnObj.css('color', '#48b0a7');
                        clearInterval(timer);
                        btnObj.on('click', function () { feach(btnObj); });
                    }
                }, 1000);
            }
            if (data.ErrorCode == -1) {
                $('.codewindow').hide();
                $('.codeerror').hide();

                $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('用户名不存在，请先注册');
                $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
                btnObj.css('color', '#48b0a7')
                btnObj.html('发送验证码');
            }
            if (data.ErrorCode == 10000) {
                //$('.codewindow').hide();
                //$('.codeerror').hide();
                $('.codeerror').show();
                $('.codeerror').find('span').html('网络连接超时，请稍后重试');
                //$('.smsCode').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
                //$('.smsCode').parents('.logininp').next('.errormsg').find('div').show();
                btnObj.css('color', '#48b0a7')
                btnObj.html('发送验证码');
            }
            if (data.ErrorCode == 10007) {
                btnObj.html("发送验证码");
                $('.codeerror').show();
                $('.codeerror').find('span').html('验证码错误');
                $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
            }

            _self.html('确定');
            _self.on("click", function () { code(_self)});
            flag = 0;
        },
        error: function () {
            //$('.codewindow').hide();
            //$('.codeerror').hide();
            $('.codeerror').show();
            $('.codeerror').find('span').html('网络连接超时，请稍后重试');
            flag = 0;
            obj.html("发送验证码");
            //$('.smsCode').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
            //$('.smsCode').parents('.logininp').next('.errormsg').find('div').show();
        },
    })
}

$('.btnbind').click(function () {
    bind($(this));
})

function bind(_this){
    var mobile = $(".mobilevalue").val().trim();
    var smsCode = $(".smsCode").val().trim();
    var lock = 0;
    if (!isPoneAvailable(mobile)) {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的手机号码');
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
        lock = 1;
    } else {
        $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
    }
    if (smsCode == "") {
        $('.smsCode').parents('.logininp').next('.errormsg').find('div').show();
        lock = 1;
    } else {
        $('.smsCode').parents('.logininp').next('.errormsg').find('div').hide();
    }
    if (lock == 1) {
        return;
    }
    _this.html("绑定中...");
    _this.off("click");

    var faceimg = $('.faceimg1').attr('src').split(',')[1];
    $.ajax({
        type: "post",
        url: "/Login/BindFace",
        data: {
            "Image": faceimg,
            "ImageType": "BASE64",
            "Phone": mobile,
            "SmsCode": smsCode
        },
        success: function (data) {
            if (data.ErrorCode == 1) {
                location.href = '/Login/FaceBindSuccess';
            }
            else {
                $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('绑定失败，请稍后再试');
                $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
            }
            _this.html("绑定");
            _this.on("click", function () { bind (_this)});
        },
        error: function () {
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
            _this.html("绑定");
            _this.on("click", function () { bind(_this) });
        },
        async: false
    });
}
//手机号码校验
function isPoneAvailable(val) {
    var myreg = /^[1][3,4,5,6,7,8,9][0-9]{9}$/;
    if (!myreg.test(val)) {
        return false;
    } else {
        return true;
    }
}
$('.nextstep').click(function () {
    $('.faceboxc1').hide();
    $('.faceboxc2').show();
});
$('.startface').click(function () {
    $('.faceboxc').show();
    $('.faceboxc1').hide();
})
$('.clface').click(function () {
    $('.faceboxc1').hide();
    $('.faceboxc').show();
})
//返回采集页面，重新采集
$('.nextstep_1').click(function () {
    $('.faceboxc1').hide();
    $('.faceboxc').show();
})
$(".mobilevalue").on("input propertychange", function () {
    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').hide();
})
$(".smsCode").on("input propertychange", function () {
    $('.smsCode').parents('.logininp').next('.errormsg').find('div').hide();
})
$('.closepaydepopimg').click(function () {
    $('.codewindow').hide();
    $('.codeerror').hide();
})
$("#txt_Code").on("input propertychange", function () {
    $('.codeerror').hide();
})

function closeVideo() {
    $('.chosewindow2').hide();
    location.href = '/Login/LoginIndex?active=0';
}