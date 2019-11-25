var facestatus = 0;
var timer;

$(function () {
    IsBindFaceLogin();
});

//页面tab切换
$('.loginbox .tab .tablist').click(function () {
    $('.loginbox .tab .tablist').removeClass('active');
    $(this).addClass('active');
    $('.logincontent').find('.itemlist').hide();
    $('.logincontent').find('.itemlist').eq($(this).index()).show();
    let status = $(this).attr('status');
    if (status == 1) {
        $('#stepCode').val("");
        IsBindFaceLogin();
    }
    else if (status == 2) {
        $('#changeCode').val("");
        $('.feachBtn').html("发送验证码");
        $('.cbox').hide();
        $('.changemobile_step1').show();
        $('.changeother').hide();
    }
    else if (status == 3) {
        $('#oldPwd').val("");
        $('#newPwd').val("");
        $('#newPwd2').val("");
        $('.cbox').hide();
        $('.changepass_step1').show(); 
        $('.changepass_step2').hide();
    }
    else if (status == 4) {
        $('#findcode').val("");
        $('.feachBtn').html("发送验证码");
        $('.cbox').hide();
        $('.findpass_step1').show(); 
        $('.findstep').hide();
    }
    else if(status==5)
    {
        initTable(1, 7);
    }
})

//判断是否已经绑定人脸识别
function IsBindFaceLogin() {
    $.ajax({
        url: '/SecurityCenter/IsBindFaceLogin',
        type: 'post',
        dataType: 'json',
        success: function (data) {
            if (data.State == true) {
                $('.cbox').hide();
                $('.step4').show();
            }
            else {
                $('.cbox').hide();
                $('.step1').show();
            }
        },
        async:true
    });
}

//人脸识别手机验证
$('.stepfun1').click(function () {
    facestatus = 0;
    let code = $('#stepCode').val().trim();
    if (code == "") {
        return;
    }
    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/ValidateMobile",
        data: {
            "Phone": $('#originalmobile').val().trim(),
            "SmsCode": $('#stepCode').val().trim(),
        },
        success: function (data) {
            if (data.State == 1) {
                clearInterval(timer);
                $('.feachBtn').css('color', '#48b0a7');
                $('.step1').hide();
                $('.step2').show();
                //清空输入框
                $('#stepCode').val("");
                $('#face-box').show();

                //短信验证通过后启动摄像头
                Webcam.set({
                    width: 337,
                    height: 251,
                    flip_horiz: true,
                    noStream: function (err) {
                        console(err);
                    }
                });
                Webcam.attach('#my_camera');
            }
            else {
                $('.faceerror').show();
                $('.faceerror').find('.codemsg').html('验证码不正确');
                $('.faceerror').find('.codemsg').show();
            }
        },
        error: function () {
            $('.faceerror').show();
            $('.faceerror').find('.codemsg').html('网络连接超时，请稍后重试');
            $('.faceerror').find('.codemsg').show();
        },
    });
});
//人脸识别短信验证通过后下一步截取图片  检测人脸是否可用
$('.stepfun2').click(function () {
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
                if (data.State == true) {
                    $('#imgstatus').attr('src', '/Images/StudentInfo/success.png');
                    $('.texttip').html('人脸样本采集成功');
                    $('.stepfun3').show();
                    $('.stepfun3_1').hide();
                }
                else {
                    $('#imgstatus').attr('src', '/Images/StudentInfo/error.png');
                    $('.texttip').html('检测失败，请重新采集');
                    $('.stepfun3').hide();
                    $('.stepfun3_1').show();
                }
                $('.step3 .faceimg').attr('src', data_uri);
                $('.step2').hide();
                $('.step3').show();
            },
            error: function () {
                $('.texttip').html('网络连接超时，请稍后重试');
                $('.stepfun3').hide();
                $('.stepfun3_1').show();
            },
            async: false
        });
    });
});
//返回采集页面，重新采集
$('.stepfun3_1').click(function () {
    $('.step3').hide();
    $('.step2').show();
})
//发送人脸信息
$('.stepfun3').click(function () {
    var faceimg = $('.faceimg').attr('src').split(',')[1];
    let url = "/SecurityCenter/BindFace";
    if (facestatus > 0)
    {
        url = "/SecurityCenter/UpdateFace";
    }
    $.ajax({
        type: "post",
        url: url,
        data: {
            "imgpath": faceimg,
            "imgType": "BASE64"
        },
        success: function (data) {
            if (data.ErrorCode == 1) {
                $('.step3').hide();
                $('.step4').show();
            }
            else {
                $('#imgstatus').attr('src', '/Images/StudentInfo/error.png');
                $('.texttip').html('绑定失败，请重试');
                $('.stepfun3').hide();
                $('.stepfun3_1').show();
            }
        },
        error: function () {
            $('#imgstatus').attr('src', '/Images/StudentInfo/error.png');
            $('.texttip').html('绑定失败，请重试');
            $('.stepfun3').hide();
            $('.stepfun3_1').show();
        },
        async: true
    });
});
//重新绑定人脸识别
$('.stepfun4').click(function () {
    facestatus = 1;
    $('.step4').hide();
    $('.step2').show();
    $('#face-box').show();

    Webcam.set({
        width: 337,
        height: 251,
        flip_horiz: true,
        noStream: function (err) {
            console(err);
        }
    });
    Webcam.attach('#my_camera');
});

$('.clface').click(function () {
    $('.step3').hide();
    $('.step2').show();
})
$('.stepfun5').click(function () {
    $('.step4').hide();
    $('.step5').show();
})
$('.stepfun6').click(function () {
    let code = $('#code').val().trim();
    if (code == "") {
        return;
    }
    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/DeleteFace",
        data: {
            "Phone": $('#originalmobile').val().trim(),
            "SmsCode": $('#code').val().trim()
        },
        success: function (data) {
            if (data.State == 1) {
                clearInterval(timer);
                $('.feachBtn').css('color', '#48b0a7');
                $('.step5').hide();
                $('.step6').show();
            }
            else {
                $(".closeerror").show();
                $(".closeerror").find('.codemsg').html('验证码不正确');
                $(".closeerror").find('.codemsg').show();
            }
        },
        error: function () {
            $(".closeerror").show();
            $(".closeerror").find('.codemsg').html('网络连接超时，请稍后重试');
            $(".closeerror").find('.codemsg').show();
        },
    });
})
$("#stepCode").on("input propertychange", function () {
    $(".faceerror").hide();
})
$("#code").on("input propertychange", function () {
    $(".closeerror").hide();
})
//开启人脸识别发送短信验证码
$("#stepBtn").click(function () {
    let phone = $("#originalmobile").val().trim();
    let _this = $(this);
    SendSms(phone, _this);
});
//关闭人脸识别发送短信验证码
$("#stepBtn2").click(function () {
    let phone = $("#originalmobile").val().trim();
    let _this = $(this);
    SendSms(phone, _this);
});
$(".stepfun7").click(function () {
    $(".step6").hide();
    $(".step1").show();
});
function Facelogin() { };
//人脸识别部分结束

//手机号码校验
function isPoneAvailable(val) {
    var myreg = /^[1][3,4,5,6,7,8,9][0-9]{9}$/;
    if (!myreg.test(val)) {
        return false;
    } else {
        return true;
    }
}
//更换手机开启
$('.mobilefun1').click(function () {
    let code = $('#changeCode').val().trim();
    if (code == "") {
        return;
    }
    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/ValidateMobile",
        data: {
            "Phone": $('#originalmobile').val().trim(),
            "SmsCode": $('#changeCode').val().trim()
        },
        success: function (data) {
            if (data.State == 1) {
                clearInterval(timer);
                $('.feachBtn').css('color', '#48b0a7');
                $('.changemobile_step1').hide();
                $('.changemobile_step2').show();
            }
            else {
                $(".changeolderror").show();
                $(".changeolderror").find('.codemsg').html('验证码不正确');
                $(".changeolderror").find('.codemsg').show();
            }
        },
        error: function () {
            $(".changeolderror").show();
            $(".changeolderror").find('.codemsg').html('网络连接超时，请稍后重试');
            $(".changeolderror").find('.codemsg').show();
        },
    });
})

$('.mobilefun2').click(function () {
    let code = $('#newphonecode').val().trim();
    let mobile = $('#newPhoneNum').val().trim();
    if (code == "" || mobile == "") {
        return;
    }
    if (!isPoneAvailable(mobile)) {
        $("#phoneStr").html("手机号不正确");
        $(".changephoneerror").show();
        return;
    }

    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/ChangeMobile",
        data: {
            "Phone": $('#newPhoneNum').val().trim(),
            "SmsCode": $('#newphonecode').val().trim(),
        },
        success: function (data) {
            if (data.State == true) {
                clearInterval(timer);
                $('.feachBtn').css('color', '#48b0a7');
                //返回成功，修改页面手机号码
                let phone = $('#newPhoneNum').val().trim();
                $('#originalmobile').val(phone);
                let reg = new RegExp("(\\d{3})\\d{4}(\\d{4})");
                var pp = phone.replace(reg, "$1****$2");
                $(".newphone").html(pp);
                $('.changemobile_step2').hide();
                $('.changemobile_step3').show();
            }
            else if(data.State==-1)
            {
                $(".changenewerror").show();
                $("#changeid").html('验证码不正确');
                $("#changeid").show();
            }
            else {
                $("#phoneStr").html("该手机号已存在");
                $(".changephoneerror").show();
            }
        },
        error: function () {
            $("#phoneStr").html("网络连接超时，请稍后重试");
            $(".changephoneerror").show();
        },
    });
});
$("#newPhoneNum").on("input propertychange", function () {
    $(".changephoneerror").hide();
})
$("#changebtn").click(function () {
    let phone = $("#originalmobile").val().trim();
    let _this = $(this);
    SendSms(phone, _this);
});
$("#newphonebtn").click(function () {
    let phone = $("#newPhoneNum").val().trim();
    let _this = $(this);
    SendSms(phone, _this);
});
$("#changeCode").on("input propertychange", function () {
    $(".changeolderror").hide();
})
$("#newphonecode").on("input propertychange", function () {
    $(".changenewerror").hide();
});
$(".mobilefun3").click(function () {
    $('.cbox').find('input').val('');
    $('.changemobile_step1').show();
    $('.changemobile_step3').hide();
});
//更换手机部分关闭

//修改密码部分开启
$('.passfun1').click(function () {
    let oldPwd = $("#oldPwd").val().trim();
    let newPwd = $("#newPwd").val().trim();
    let newPwd2 = $("#newPwd2").val().trim();
    if (oldPwd == "" || newPwd == "" || newPwd2 == "") {
        return;
    }
    if (newPwd != newPwd2) {
        $("#pwddiv2").show();
        $("#pwderror2").show();
        return;
    }
    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/UpdatePassword",
        data: {
            "oldPassword": oldPwd,
            "newPassword": newPwd,
        },
        success: function (data) {
            if (data.ErrorCode == 1) {
                $('.changepass_step1').hide();
                $('.changepass_step2').show();
            }
            else if (data.ErrorCode == -1) {
                $("#olddiv").show();
                $("#oldpwderror").find('span').html('原始密码输入错误');
                $("#oldpwderror").find('span').show();
                $("#oldpwderror").show();
            }
            else
            {
                $("#olddiv").show();
                $("#oldpwderror").find('span').html('网络连接超时，请稍后重试');
                $("#oldpwderror").find('span').show();
                $("#oldpwderror").show();
            }
        },
        error: function (data) {
            $("#olddiv").show();
            $("#oldpwderror").find('span').html('网络连接超时，请稍后重试');
            $("#oldpwderror").find('span').show();
            $("#oldpwderror").show();
            //layer.msg("连接服务器失败");

        },
    });
})
$("#oldPwd").on("input propertychange", function () {
    $("#olddiv").hide();
})
$("#newPwd2").on("input propertychange", function () {
    $("#pwddiv2").hide();
})
$('.passfun2').click(function () {
    $('.changepass_step1').find('input[type="password"]').val('');
    $('.changepass_step1').show();
    $('.changepass_step2').hide();
})
//修改密码部分结束

//找回密码开始
$("#findbtn").click(function () {
    let phone = $("#originalmobile").val().trim();
    let _this = $(this);
    SendSms(phone, _this);
})
$('.findpassfun1').click(function () {
    let code = $('#findcode').val().trim();
    if (code == "") {
        return;
    }
    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/ValidateMobile",
        data: {
            "Phone": $('#originalmobile').val().trim(),
            "SmsCode": $('#findcode').val().trim()
        },
        success: function (data) {
            if (data.State == 1) {
                clearInterval(timer);
                $('.feachBtn').css('color', '#48b0a7');
                $('.findpass_step1').hide();
                $('.findpass_step2').find('input').val('');
                $('.findpass_step2').show();
            }
            else {
                $('.findcodeerror').show();
                $('.findcodeerror').find('.codemsg').html('验证码错误');
                $('.findcodeerror').find('.codemsg').show();
            }
        },
        error: function () {
            $('.findcodeerror').show();
            $('.findcodeerror').find('.codemsg').html('网络连接超时，请稍后重试');
            $('.findcodeerror').find('.codemsg').show();
        },
    });
})
$('.findpassfun2').click(function () {
    let pwd1 = $('#newPwd3').val().trim();
    let pwd2 = $('#newPwd4').val().trim();
    if (pwd1 == "" || pwd2 == "") {
        return;
    }
    if (pwd1 != pwd2) {
        $('.findpwderror').show();
        $('.findpwderror').find('.codemsg').html('两次密码输入不一致');
        $('.findpwderror').find('.codemsg').show();
        return;
    }
    $.ajax({
        type: "post",
        async: true,
        url: "/SecurityCenter/FindPassword",
        data: {
            "password": pwd1
        },
        dataType:'json',
        success: function (data) {
            if (data.State == true) {
                $('.findpass_step2').hide();
                $('.findpass_step3').show();
            }
            else {
                $('.findpwderror').show();
                $('.findpwderror').find('.codemsg').html('网络连接超时，请稍后重试');
                $('.findpwderror').find('.codemsg').show();
            }
        },
        error: function () {
            $('.findpwderror').show();
            $('.findpwderror').find('.codemsg').html('网络连接超时，请稍后重试');
            $('.findpwderror').find('.codemsg').show();
        },
    });
});

$(".findpassfun3").click(function () {
    $(".findpass_step3").hide();
    $(".findpass_step1").show();
    $("#findbtn").html("发送验证码");
    clearInterval(timer);
    $("#findcode").val("");

})
$("#findcode").on("input propertychange", function () {
    $('.findcodeerror').hide();
})
$("#newPwd4").on("input propertychange", function () {
    $('.findpwderror').hide();
})
//找回密码部分结束

//登录记录
function initTable(pageindex,pagesize) {
    $.ajax({
        type: "get",
        async: false,
        url: "/SecurityCenter/GetStudentLogin?random=" + Math.random(),
        data: {
            pageIndex: pageindex,
            pageSize: pagesize
        },
        success: function (obj) {
            $("#table_records").empty();
            
            var str = "";
            for (var i = 0; i < obj.Data.length; i++) {
                str += '<tr>';
                str += '<td>' + obj.Data[i].LoginTimeStr + '</td>';
                str += '<td>' + obj.Data[i].Bsg_LoginIp + '</td>';
                str += '<td>' + obj.Data[i].Bsg_LoginArea + '</td>';
                str += '<td>' + obj.Data[i].LoginDevice + '</td>';
                str += '</tr>';
            }
            $("#table_records").append(str);
            var pages = Math.ceil(obj.TotalRecord / pagesize); //得到总页数
            //调用分页
            laypage({
                cont: 'pagelogin',
                pages: pages,
                curr: pageindex,
                prev: '<',
                next: '>',
                jump: function (obj, first) {
                    if (!first) { //一定要加此判断，否则初始时会无限刷新
                        pageindex = obj.curr;
                        initTable(pageindex,pagesize);
                       }
                }
            })
        }
    });
}

var limit = 0;
function SendSms(phone, obj) {
    if (limit == 1)
    {
        return;
    }
    limit = 1;
    obj.html("正在发送...");
    obj.parent().next().children().hide();
    $.ajax({
        type: "post",
        async: true,
        url: "/Sms/SendValidCode",
        data: {
            phone: phone,
            isValidate: true,
            type:3
        },
        success: function (data) {
            if (data.State == 1) {
                obj.off('click');
                obj.parent().next().children().hide();
                var num = 61;
                timer = setInterval(function () {
                    if (num > 0) {
                        num--;
                        obj.html("重新发送(" + num + ")").attr("status", "0");
                        obj.css('color', '#707070');
                    } else {
                        obj.html("重新发送").removeAttr("status", '1');
                        obj.css('color', '#48b0a7');
                        clearInterval(timer);
                        obj.on('click', function () { SendSms(phone,obj)});
                    }
                }, 1000);
            }
            if (data.ErrorCode == 10000) {
                obj.parent().next().find('.codemsg').html('网络连接超时，请稍后重试');
                obj.parent().next().show();
                obj.parent().next().children().show();
                obj.parent().next().find('.codemsg').show();
                obj.css('color', '#48b0a7')
                obj.html('发送验证码');
            }
            if(data.ErrorCode==10002)
            {
                obj.parent().next().find('.codemsg').html('发送过于频繁，请稍后再试');
                obj.parent().next().show();
                obj.parent().next().children().show();
                obj.parent().next().find('.codemsg').show();
                obj.css('color', '#48b0a7')
                obj.html('发送验证码');
            }
            limit = 0;
        },
        error: function () {
            obj.html('发送验证码');
            obj.parent().next().find('.codemsg').html('网络连接超时，请稍后重试');
            obj.parent().next().show();
            obj.parent().next().children().show();
            obj.parent().next().find('.codemsg').show();
            limit = 0;
        },
    })
}

function closeVideo() {
    $('.chosewindow2').hide();
    IsBindFaceLogin();
    $("#stepCode").val("");
    $('.error').hide();
}