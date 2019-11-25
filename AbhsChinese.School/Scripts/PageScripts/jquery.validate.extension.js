//为jquery.validate.js插件添加手机号码验证
jQuery.validator.addMethod("isMobile", function (value, element, opt) {
    //console.log(opt);
    var length = value.length;
    //var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    var mobile = /^^1[3456789]\d{9}$/;
    return this.optional(element) || (length == 11 && mobile.test(value));
}, "请正确填写您的手机号码");

//为jquery.validate.js插件添加密码长度验证
jQuery.validator.addMethod("pwdlength", function (value, element, opt) {
    console.log(opt);
    let limit = opt || 6;
    var length = value.length;
    if (length > 0) {
        return length > limit - 1
    } else {
        return true;
    }
}, "最少输入6位密码");


////type取值
//学生未注册 = 1,
//学生已注册 = 2,
//学校教师未注册 = 3,
//学校教师已注册 = 4,
//系统教师未注册 = 5,
//系统教师已注册 = 6,
//学校教师手机号未注册验证

//老师账号未注册
$.validator.addMethod("validSchoolTeacherAccount", function (value) {
    var returnVal = true;
    //var id = @Model.Yoh_Id; //&&id==0
    var req;
    if (value != '') {
        if (req != null) {
            req.abort();
        }
        req = $.ajax({
            url: '/Account/CheckAccount',
            data: { "phone": value, "type": 3 },//3
            mode: "abort",
            type: "POST",
            async: false,
            success: function (data) {
                returnVal = data.State;
            },
            error: function (xhr) {
                returnVal = false;
            }
        });
    }
    return returnVal;
}, '该账号已经被占用');

//学生账号未注册验证
$.validator.addMethod("validStudentAccount", function (value) {
    var returnVal = true;
    var req;

    if (value != '') {
        if (req != null) {
            req.abort();
        }
        req = $.ajax({
            url: '/Account/CheckAccount',
            data: { "phone": value, "type": 1 },
            mode: "abort",
            type: "POST",
            async: false,
            success: function (data) {
                returnVal = data.State;
            },
            error: function (xhr) {
                returnVal = false;
            }
        });
    }
    return returnVal;
}, '该账号已经被占用');

//学生账号已注册验证
$.validator.addMethod("validStudentAccountRegistered", function (value) {
    var returnVal = true;
    var req;
    if (value != '') {
        if (req != null) {
            req.abort();
        }
        req = $.ajax({
            url: '/Account/CheckAccount',
            data: { "phone": value, "type": 2 },
            mode: "abort",
            type: "POST",
            async: false,
            success: function (data) {
                returnVal = data.State;
            },
            error: function (xhr) {
                returnVal = false;
            }
        });
    }
    return returnVal;
}, '该账号未注册');


