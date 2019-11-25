$(document).ready(function () {

    //切换课程详情和课程安排
    $('.goodsbottom div.tablist').on('click', function () {
        $(this).addClass('active').siblings().removeClass('active');
        if ($(this).hasClass("courseIntroduction")) {
            $("#courseIntroductionPanel").show();
            $("#courseArrangePanel").hide();
        } else {
            $("#courseIntroductionPanel").hide();
            $("#courseArrangePanel").show();
        }
    })

    var navH = $("#tabbox").offset().top;
    //console.log(navH);
    $(document).scroll(function () {
        //获取滚动条的滑动距离
        var scroH = $(this).scrollTop();
        // console.log(scroH);
        //滚动条的滑动距离大于等于定位元素距离浏览器顶部的距离，就固定，反之就不固定
        if (scroH >= (navH - 77)) {
            $('#tabbox').addClass('pro-detail-hd-fixed').find('.btns').show();
            $(".tabcontbox").css("padding-top", "73px");
        } else if (scroH < navH) {
            $('#gobuybox').hide();
            $('#tabbox').removeClass('pro-detail-hd-fixed').find('.btns').hide();
            $(".tabcontbox").css("padding-top", "0px");
        }
    });
    $("#gobuybtn").hover(function () {
        $('#gobuybox').show();
    }, function () {
        $('#gobuybox').hide();
    });

    //打开绑定学校弹窗
    $('.bindschool').click(function () {
        $("#bindSchoolPanel").show();
    })
    var isvalidTeacherPhone = false;//手机号是否为有效的校区老师手机号
    var lastEnteredPhone = "";//记录上次请求的手机号，防止同一手机号重复请求
    $("#txtBindSchoolPhone").on('keyup blur', throttle2(function (event) {
        //console.log("change");
        var mobile = $.trim($("#txtBindSchoolPhone").val());
        if (/^[1][3-9][0-9]{9}$/.test(mobile)) {
            if (lastEnteredPhone != mobile) {
                $("#bindSchoolName").text("");
                $("#bindSchoolErrorTip").text("");
                isvalidTeacherPhone = false;
                $.ajax({
                    url: '/Student/GetSchoolName',
                    type: "post",
                    data: { phone: mobile },
                    dataType: 'json',
                    success: function (result) {
                        lastEnteredPhone = mobile;
                        if (result.State == true) {
                            isvalidTeacherPhone = true;
                            $("#bindSchoolName").text(result.Data);
                        }
                        else {
                            isvalidTeacherPhone = false;
                            $("#bindSchoolErrorTip").text("手机号码不正确，请核对。");
                        }
                    }
                });
            }
        } else {
            $("#bindSchoolName").text("");
            lastEnteredPhone = mobile;//防止输对了然后继续输入导致的错误，删除多余位后不再去服务器校验
            if (mobile.length >= 11) {
                $("#bindSchoolErrorTip").text("请检查手机号是否正确。");
            }
        }
    }, 100));
    $('#btnBindSchool').click(function () {
        if (isvalidTeacherPhone) {
            var mobile = $.trim($("#txtBindSchoolPhone").val());
            $.ajax({
                url: '/Student/BindSchool',
                type: "post",
                data: { phone: mobile },
                dataType: 'json',
                success: function (result) {
                    if (result.State == true) {
                        $("#bindSchoolPanel").hide();
                        $("#bindSchoolResultPanel").show();
                        $("#applyBindSchoolTip").text("已申请" + result.Data.Bsl_SchoolName + "，等待校长审批");
                    }
                    else {
                        $("#applyBindSchoolTip").text("");
                        $("#bindSchoolErrorTip").text("绑定失败，请稍后再试。");
                    }
                }
            });
        }
    });
    //关闭绑定学校弹窗
    $('#bindSchoolPanel img.closepaydepopimg').click(function () {
        $("#bindSchoolPanel").hide();
        $("#txtBindSchoolPhone").val("")
        $("#bindSchoolName").text("");
        $("#bindSchoolErrorTip").text("");
    });
    $('#btnCloseBindSchool,#bindSchoolResultPanel img.closepaydepopimg').click(function () {
        $("#bindSchoolResultPanel").hide();
    });
});