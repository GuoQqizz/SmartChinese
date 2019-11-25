// 导航栏
$(document).ready(function () {
    $(".header .navlist a:eq(3)").addClass("active").siblings().removeClass('active');
});

//防止一段时间内重复调用方法
//delay：毫秒
//方法执行后delay毫秒内不会再执行此方法，delay毫秒后方可再次触发
//如果禁用期内多次触发方法，中间N次都会被忽略，delay后也不会自动触发
function throttle(fun, delay) {
    let last, deferTimer
    return function (args) {
        let that = this
        let _args = arguments
        let now = +new Date()
        if (last && now < last + delay) {
            //console.log("ignore");
        } else {
            last = now
            fun.apply(that, _args)
        }
    }
}


//防止一段时间内重复调用方法
//delay：毫秒
//方法执行后delay毫秒内不会再执行此方法，delay毫秒后方可再次触发
//如果禁用期内多次触发方法，中间N次都会被忽略，但是delay后会自动再触发一次
function throttle2(fun, delay) {
    let last, deferTimer
    return function (args) {
        let that = this
        let _args = arguments
        let now = +new Date()
        if (last && now < last + delay) {
            clearTimeout(deferTimer)
            deferTimer = setTimeout(function () {
                last = now
                fun.apply(that, _args)
            }, delay)
        } else {
            last = now
            fun.apply(that, _args)
        }
    }
}

function getGradeName(grade) {
    var gn = "";
    switch (grade) {
        case 1:
            gn = "一年级";
            break;
        case 2:
            gn = "二年级";
            break;
        case 4:
            gn = "三年级";
            break;
        case 8:
            gn = "四年级";
            break;
        case 16:
            gn = "五年级";
            break;
        case 32:
            gn = "六年级";
            break;
        case 64:
            gn = "初一";
            break;
        case 128:
            gn = "初二";
            break;
        case 256:
            gn = "初三";
            break;
        case 512:
            gn = "高一";
            break;
        case 1024:
            gn = "高二";
            break;
        case 2048:
            gn = "高三";
            break;
    }
    return gn;
}
function getCourseTypeName(courseType) {
    var ctn = "";
    switch (courseType) {
        case 1:
            ctn = "同步课";
            break;
        case 2:
            ctn = "专项课";
            break;
        case 3:
            ctn = "复习课";
            break;
        case 4:
            ctn = "整本书阅读";
            break;
        case 5:
            ctn = "文学史";
            break;
    }
    return ctn;
}