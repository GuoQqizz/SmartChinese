var flag = 0;
var obj=null;
$(function () {
    $("#switchCode").click(function () {
        $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
    });

    $('.codepopsub').click(function () {
        code($(this));
    });

    $('.closepaydepopimg').click(function () {
        $('.codewindow').hide();
        $('.codeerror').hide();
    })

    function isPoneAvailable(val) {
        var myreg = /^[1][3,4,5,6,7,8,9][0-9]{9}$/;
        if (!myreg.test(val)) {
            return false;
        } else {
            return true;
        }
    }
    $('.feachBtn').click(function () {
        feach($(this));
    });

    function feach(_this)
    {
        let phone = $('.mobilevalue').val().trim();
        let authCode = $('#txt_Code').val().trim();
        if (phone == '') {
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的手机号');
            $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
            return;
        }
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

        obj = _this;
    }

    function code(_self) {
        if (obj.attr('status') == '0') {
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

        obj.html("正在发送...");
        _self.html("发送中...");
        _self.off("click");
        
        $.ajax({
            type: "post",
            async: true,
            url: "/Sms/SendValidCode",
            data: {
                phone: phone,
                authCode: authCode,
                type:1
            },
            success: function (data) {
                if (data.State == 1) {
                    obj.off('click');
                    $('.codewindow').hide();
                    $('.codeerror').hide();

                    obj.parent().next().children().hide();
                    var num = 61;
                    var timer = setInterval(function () {
                        if (num  > 0) {
                            num--;
                            obj.html("重新发送(" + num + ")").attr("status", "0");
                            obj.css('color', '#707070');
                        } else {
                            obj.html("重新获取").removeAttr("status", '1');
                            obj.css('color', '#48b0a7');
                            clearInterval(timer);
                            obj.on('click', function () { feach(obj) });
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
                    obj.css('color', '#48b0a7')
                    obj.html('发送验证码');
                }
                if (data.ErrorCode == 10007) {
                    obj.html("发送验证码");
                    $('.codeerror').find('span').html('验证码错误');
                    $('.codeerror').show();
                    $("#imgcode").attr("src", "/Login/GetAuthCode?time=" + Math.random());
                }
                _self.html("确定");
                _self.on("click", function () { code(_self)});
                flag = 0;
            },
            error: function () {
                //$('.codewindow').hide();
                //$('.codeerror').hide();

                flag = 0;
                obj.html("发送验证码");
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
    $('.selectempty').change(function () {
        $(this).next('.successtip').show();
    })
    $(".checkmobile").on("input propertychange", function () {
        if (isPoneAvailable($(this).val().trim())) {
            $(this).next('.successtip').show();
        } else {
            $(this).next('.successtip').hide();
        }
    })
    $(".passwordvalue1").on("input propertychange", function () {
        $('.passwordvalue1').parents('.logininp').next('.errormsg').find('div').hide();
    })
    document.onkeydown = function (e) { // 回车提交表单
        // 兼容FF和IE和Opera
        var theEvent = window.event || e;
        var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
        if (code == 13) {
            register();
        }
    }
    //点击注册
    $('.register_act').click(function () {
        register();
    });

    function register() {
        var data = {};
        var lock = 1;
        var mobile = $('.mobilevalue').val().trim();
        var name = $('.namevalue').val().trim();
        var password = $('.passwordvalue').val().trim();
        var password1 = $('.passwordvalue1').val().trim();
        var classs = $('.classvalue').val();
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
            $('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入验证码');
            $('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
            lock = 0;
        }
        if (name.length > 0) {
            $('.namevalue').parents('.logininp').next('.errormsg').find('div').hide();
        } else {
            $('.namevalue').parents('.logininp').next('.errormsg').find('div').show();
            lock = 0;
        }
        if (classs != undefined) {
            $('.classvalue').parents('.logininp').next('.errormsg').find('div').hide();
        } else {
            $('.classvalue').parents('.logininp').next('.errormsg').find('div').show();
            lock = 0;
        }
        data.Name = name;
        data.Grade = classs;
        data.Phone = mobile;
        data.SmsCode = code;
        data.Password = password;
        data.ConfirmPassword = password1;
        data.Source = 1;//1表示注册来源是学生注册
        data.PassportType = 1;//1表示账号类型是手机
        console.log(data);
        if (lock == 0) {
            return false;
        }

        var _self = $(this);
        _self.html("注册中...");
        _self.attr('disabled', true);
        $.ajax({
            type: "post",
            async: true,
            url: "/Login/Register",
            data: data,
            success: function (data) {
                _self.html('注册');
                _self.removeAttr('disabled');
                if (data.ErrorCode == 10004) {
                    $('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('验证码错误');
                    $('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
                }
                else if (data.ErrorCode == -5) {
                    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入正确的手机号');
                    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
                }
                else if (data.ErrorCode == -6) {
                    $('.codevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('请输入验证码');
                    $('.codevalue').parents('.logininp').next('.errormsg').find('div').show();
                }
                else if (data.ErrorCode == -7) {
                    $('.namevalue').parents('.logininp').next('.errormsg').find('div').show();
                }
                else if (data.ErrorCode == -8) {
                    $('.classvalue').parents('.logininp').next('.errormsg').find('div').show();
                }
                else if (data.ErrorCode == -9) {
                    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('手机号已存在');
                    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
                }
                else if (data.ErrorCode == 1) {
                    _self.html("注册成功");
                    _self.attr('disabled', true);

                    layer.msg("注册成功",
                            { icon: 1, time: 1000 },
                            function () {
                                location.href = '/LearningCenter/Index';
                            });
                }
                else if (data.ErrorCode == -1) {
                    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').children('span').html('网络连接超时，请稍后重试');
                    $('.mobilevalue').parents('.logininp').next('.errormsg').find('div').show();
                }
            },
            error: function () {
                _self.html('注册');
                _self.removeAttr('disabled');
                layer.msg('网络连接超时，请稍后重试',{icon:2,time:2000});
            },
        })
    }

    $('.hideerror').focus(function () {
        $(this).parents('.logininp').next('.errormsg').find('div').hide();
    })
    $('.hideerrorselect').change(function () {
        $(this).parents('.logininp').next('.errormsg').find('div').hide();
    })
});

$("#txt_Code").on("input propertychange", function () {
    $('.codeerror').hide();
})