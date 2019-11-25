//为jquery.validate.js插件添加手机号码验证
jQuery.validator.addMethod("isMobile", function (value, element) {
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
//zh-cn
jQuery.validator.addMethod("noZhCn", function (value, element) {
    var ch_reg = /^[u4E00-u9FA5{u-z}]+$/;
    return this.optional(element) || (ch_reg.test(value));
}, "不可输入汉字");

// 全空格验证
$.validator.addMethod("isBlank", function (value, element) {
    var blank = /^[ ]*$/;
    return this.optional(element) || !(blank.test(value));
}, "不能输入空格");