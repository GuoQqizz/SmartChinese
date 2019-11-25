$(function () {
    //可使用课程
    $(".course>div>div>.iCheck-helper").click(function () {
        var course = $('input[name="rdoCourse"]:checked').val();
        if (course == 1) {
            $(".div_grade").hide();
            $(".div_type").hide();
            $(".div_course").hide();
            $(".line").hide();
            $("#Grade").attr("disabled",true)
            $("#CourseType").attr("disabled", true)
            $("#CourseId_text").attr("disabled", true)
        }
        if (course == 2) {
            $("select[name='Grade']").val(null).trigger("change");
            $("select[name='CourseType']").val(null).trigger("change");
            $(".div_grade").show();
            $(".div_type").show();
            $(".div_course").hide();
            $(".line").show();
            $("#Grade").removeAttr("disabled")
            $("#CourseType").removeAttr("disabled");
            $("#CourseId_text").attr("disabled", true)

            $("#Grade").removeClass("error");
            $("#Grade-error").hide();
            $("#CourseType").removeClass("error");
            $("#CourseType-error").hide();
        }
        if (course == 3) {
            $(".div_grade").hide();
            $(".div_type").hide();
            $(".div_course").show();
            $(".line").show();
            $("#CourseId_text").removeAttr("disabled")
            $("#Grade").attr("disabled", true)
            $("#CourseType").attr("disabled", true)
        }
    });
    //使用门槛
    $(".amountlimit>div>div>.iCheck-helper").click(function () {
        var amountlimit = $('input[name="rdoOrderAmountLimit"]:checked').val();
        if (amountlimit == 1) {
            $(".input_amount").removeAttr("disabled");

        }
        else {
            $(".input_amount").attr("disabled", true);
            $(".input_amount").next().children().hide();
            $(".input_amount").val("");
            $(".input_amount").removeClass("input-error");
        }
    });
    //有效期
    $(".div_expire>div>div>.iCheck-helper").click(function () {
        var expire = $('input[name="rdoExpireType"]:checked').val();
        if (expire == 3) {
            $(".input-group.date").addClass("notclick");
            $(".input_date").attr("disabled", true);
            $(".input_day").attr("disabled", true);
            $(".input_date").val("");
            $(".input_day").val("");
            let isDate = $('.input_date').valid();
            if (isDate == false) {
                $('.input_date').removeClass('error');
                $("#datepicker1-error").hide();
            }
            let isDay = $('.input_day').valid();
            if (isDay == false) {
                $('.input_day').removeClass('error');
                $("#ExpireDayCount-error").hide();
            }
        }
        if (expire == 1) {
            $(".input-group.date").removeClass("notclick");
            $(".input_date").removeAttr("disabled");
            $(".input_day").attr("disabled", true);
            $(".input_day").val("");
            let isDay = $('.input_day').valid();
            if (isDay == false) {
                $('.input_day').removeClass('error');
                $("#ExpireDayCount-error").hide();
            }
        }
        if (expire == 2) {
            $(".input-group.date").addClass("notclick");
            $(".input_day").removeAttr("disabled");
            $(".input_date").attr("disabled", true);
            $(".input_date").val("");
            let isDate = $('.input_date').valid();
            if (isDate == false) {
                $('.input_date').removeClass('error');
                $("#datepicker1-error").hide();
            }
        }

    });
    //适用校区
    $(".div_school>div>div>.iCheck-helper").click(function () {
        var school = $('input[name="rdoSchool"]:checked').val();
        if (school == 1) {
            $(".input_school").removeAttr("disabled");
        }
        else {
            $(".input_school").val(null).trigger("change");
            $(".input_school").attr("disabled", true);
            $(".input_school .sp_result_area ").hide();
            let isSch = $('.input_school').valid();
            if (isSch == false) {
                $('.input_school').removeClass('error');
                $("#schoolId_text-error").hide();
            }
        }
    });
});


$('#schoolId').selectPage({
    showField: 'name',
    keyField: 'id',
    pageSize: 2,
    //关闭向下的三角尖按钮
    dropButton: false,
    data: '/CashVoucher/GetSchool',
    inputDelay: 0.2,
    beforeSend: function (e) {
        console.log(e);
        if ($("#schoolId_text").val() == "") {
            e.abort();
        }
    },
    params: function () { return {}; },
    eAjaxSuccess: function (d) {
        return d.Data;
    }
});

$('#CourseId').selectPage({
    showField: 'name',
    keyField: 'id',
    pageSize: 2,
    dropButton: false,
    data: '/CashVoucher/GetCourses',
    inputDelay: 0.2,
    beforeSend: function (e) {
        console.log(e);
        if ($("#CourseId_text").val() == "") {
            e.abort();
        }
    },
    params: function () { return {}; },
    eAjaxSuccess: function (d) {
        return d.Data;
    }
});

$(".input-group.date").datepicker({
   autoclose: true,
   todayBtn: "linked",
   todayHighlight: true,
   /*汉化*/
   language: "cn",
   /*日期格式*/
   format: "yyyy-mm-dd"
});
$(".input-group.date input").on('change', function () {
    $(this).valid();
});
$('.input_amount').on('change', function () {
    var isTrue = $(this).valid();
    if (isTrue == false) {
        $(this).addClass("input-error");
    }
    else {
        $(this).removeClass("input-error");
    }
});

var limit = 0;
; (function ($) {
    $('.select2.form-control').select2({
        width: '100%',
        allowClear: true,
        minimumResultsForSearch: Infinity
    }).on('change', function (e) {
        $(this).valid();
    });

    $('.input_amount').on('change', function () {
        let isTrue = $(this).valid();
        if (isTrue == false) {
            $(this).addClass("input-error");
        }
        else {
            $(this).removeClass("input-error");
        }
    });

    $('#submitForm').validate({
        ignore:'disabled',
        errorPlacement: function (error, element) {
            console.log(error);
            debugger;
            if (element.is(".input_amount") ) {
                $(".input_amount").addClass("input-error");
                error.appendTo(element.next());
            }
            else if(element.is(".input_day"))
            {
                error.insertAfter(element.parent());
            }
            else if (element.is(".input_school") || element.is(".input_course") || element.is(".input_date") || element.is(".input_relatedCourseId")) {
                error.appendTo(element.parent().parent());
            }
            else {
                error.insertAfter(element);
            }
        },
        rules: {
            Name: {
                required: true,
                maxlength: 50,
                isBlank: true
            },
            PublishCount: {
                required: true
            },
            Amount: {
                required: true
            },
            OrderAmountLimit: {
                required: true
            },
            DatePick: {
                required: true
            },
            schoolId_text: {
                required: true
            },
            CourseId_text: {
                required: true
            },
            RelatedCourseId_text: {
                required: true
            },
            Grade: {
                required:true
            },
            CourseType: {
                required:true
            },
            ExpireDayCount: {
                required:true
            }
        },
        messages: {
            Name: {
                required: "请输入名称",
                maxlength: "长度不能大于50个字符"
            },
            PublishCount: {
                required: "请输入发行量"
            },
            Amount: {
                required: "请输入面额"
            },
            OrderAmountLimit: {
                required: "请输入使用门槛"
            },
            DatePick: {
                required: "请选择日期"
            },
            schoolId_text: {
                required: "请选择适用校区"
            },
            CourseId_text: {
                required: "请选择课程"
            },
            RelatedCourseId_text: {
                required: "请选择可赠券课程"
            },
            Grade: {
                required: "请选择适用年级"
            },
            CourseType: {
                required: "请选择课程类型"
            },
            ExpireDayCount: {
                required: "请输入到期天数"
            }
        },
        submitHandler: function (form) {
            ajaxSubmit();
        }
    });

    $("#btn_SignIn").click(function () {
        if (limit == 1)
        {
            return;
        }
        limit = 1;
        ajaxSubmit();
    });

})(jQuery);


function ajaxSubmit() {
    var id = $("#Id").val();
    var name = $("#Name").val();
    var publishCount = $("#PublishCount").val();
    var amount = $("#Amount").val();
    var limitByPerson = $("#LimitByPerson").val();
    var remark = $("#Remark").val();
    var voucherType = $("#VoucherType").val();
    var relatedCourseId = $("#RelatedCourseId").val();

    //使用门槛
    var amountlimit = 0;
    var amountlimitType = $('input[name="rdoOrderAmountLimit"]:checked').val();
    if (amountlimitType == 1) {
        amountlimit = $("#OrderAmountLimit").val();
    }
    //有效期
    var expireType = $('input[name="rdoExpireType"]:checked').val();
    var expireDate = $("#datepicker1").val() == "" ? "1900-01-01" : $("#datepicker1").val();
    var expireDay = $("#ExpireDayCount").val() == "" ? 0 : $("#ExpireDayCount").val();
    //适用校区
    var schoolId = 0;
    var school = $('input[name="rdoSchool"]:checked').val();
    if (school == 1) {
        schoolId = $("#schoolId").val();
    }
    //可使用课程
    var applyScopeType = $('input[name="rdoCourse"]:checked').val();
    var grade = $("#Grade").val();
    var courseType = $("#CourseType").val();
    var courseid = $("#CourseId").val();

    var cou = $('#CourseId').val();
    if (cou == "") {
        $('#CourseId_text').val("");
    }

    var sch = $('#schoolId').val();
    if (sch == "") {
        $('#schoolId_text').val("");
    }

    var rel = $('#RelatedCourseId').val();
    if (rel == "") {
        $('#RelatedCourseId_text').val("");
    }
    

    if (!$("#submitForm").valid()) {
        if (amountlimit === "") {
            $('.input_amount').addClass('input-error');
        }
        limit = 0;
        return false;
    }

    loadingFlag = layer.msg('加载中，请稍候……', { icon: 16, shade: 0.01, shadeClose: false, time: 60000 });
    $.ajax({
        url: "/CashVoucher/AddVoucher",
        type: "post",
        data: {
            Id:id,
            Name: name,
            VoucherType: voucherType,
            PublishCount: publishCount,
            Amount: amount,
            LimitByPerson: limitByPerson,
            OrderAmountLimit: amountlimit,
            ExpireType: expireType,
            ExpireDate: expireDate,
            expireDay: expireDay,
            SchoolType: school,
            SchoolId: schoolId,
            ApplyScopeType: applyScopeType,
            Grade: grade,
            CourseType: courseType,
            Courseid: courseid,
            Remark: remark,
            RelatedCourseId: relatedCourseId
        },
        dataType: "json",
        success: function (obj) {
            layer.close(loadingFlag);
            if (obj.State) {
                layer.msg(obj.ErrorMsg, { icon: 1, time: 500 }, function () { limit = 0;location.href = '/CashVoucher/CashVoucherList'; });
            }
            else {
                layer.alert(obj.ErrorMsg, { icon: 5 }, function () { limit = 0; location.href = '/CashVoucher/CashVoucherList'; });
            }
            
        },
        error: function (data) {
            limit = 0;
            layer.close(loadingFlag);
            layer.alert(obj.ErrorMsg, { icon: 5 }, function () { limit = 0; location.href = '/CashVoucher/CashVoucherList'; });
        }
    });
}