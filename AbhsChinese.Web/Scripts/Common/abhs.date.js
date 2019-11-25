//获取当前时间格式为yyyy-MM-dd
function getNowDate() {
    var Y = new Date().getFullYear();
    var M = new Date().getMonth() + 1;
    var D = new Date().getDate();
    if (M < 10) {
        M = '0' + M
    }
    if (D < 10) {
        D = '0' + D
    }

    return Y + '-' + M + '-' + D;
}
//获取传入日期的前一天日期或后一天日期；date为传入日期yyyy-MM-dd；day为-1表示前一天，day为1表示后一天，传几就是几天
function getProAndNextDate(date, day) {
    var dd = new Date(date);
    dd.setDate(dd.getDate() + day);
    var y = dd.getFullYear();
    var m = dd.getMonth() + 1 < 10 ? "0" + (dd.getMonth() + 1) : dd.getMonth() + 1;
    var d = dd.getDate() < 10 ? "0" + dd.getDate() : dd.getDate();
    return y + "-" + m + "-" + d;
};
//根据传入日期判断为周几
function getWeekDay(data) {
    var weekday = new Array(7);
    weekday[0] = "周日";
    weekday[1] = "周一";
    weekday[2] = "周二";
    weekday[3] = "周三";
    weekday[4] = "周四";
    weekday[5] = "周五";
    weekday[6] = "周六";
    var myDate = new Date(Date.parse(data));
    return weekday[myDate.getDay()]
}

function CurrentDate() {
    var now = new Date();
    var year = now.getFullYear(); //得到年份
    var month = now.getMonth();//得到月份
    var date = now.getDate();//得到日期
    var day = now.getDay();//得到周几
    var hour = now.getHours();//得到小时
    var minu = now.getMinutes();//得到分钟
    var sec = now.getSeconds();//得到秒
    var MS = now.getMilliseconds();//获取毫秒
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    if (MS < 100) MS = "0" + MS;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    //week = arr_week[day];
    var time = "";
    //time = year + "-" + month + "-" + date + "-" + " " + hour + ":" + minu + ":" + sec + " " + week;
    time = year + "-" + month + "-" + date + " " + hour + ":" + minu + ":" + sec;
    return time;
}

function ConvertDate(now) {
    var year = now.getFullYear(); //得到年份
    var month = now.getMonth();//得到月份
    var date = now.getDate();//得到日期
    var day = now.getDay();//得到周几
    var hour = now.getHours();//得到小时
    var minu = now.getMinutes();//得到分钟
    var sec = now.getSeconds();//得到秒
    var MS = now.getMilliseconds();//获取毫秒
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    if (MS < 100) MS = "0" + MS;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    //week = arr_week[day];
    var time = "";
    //time = year + "-" + month + "-" + date + "-" + " " + hour + ":" + minu + ":" + sec + " " + week;
    time = year + "-" + month + "-" + date + "&nbsp;&nbsp;&nbsp;&nbsp;" + hour + ":" + minu;
    return time;
}

function ConvertShortDate(now) {
    var year = now.getFullYear(); //得到年份
    var month = now.getMonth();//得到月份
    var date = now.getDate();//得到日期
    var day = now.getDay();//得到周几
    var hour = now.getHours();//得到小时
    var minu = now.getMinutes();//得到分钟
    var sec = now.getSeconds();//得到秒
    var MS = now.getMilliseconds();//获取毫秒
    var week;
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    if (MS < 100) MS = "0" + MS;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    //week = arr_week[day];
    var time = "";
    //time = year + "-" + month + "-" + date + "-" + " " + hour + ":" + minu + ":" + sec + " " + week;
    time = hour + ":" + minu;
    return time;
}

/**
 * 获取指定日期的周的第一天、月的第一天、季的第一天、年的第一天
 * @param date new Date()形式，或是自定义参数的new Date()
 * @returns 返回值为格式化的日期，yy-mm-dd
 */
//日期格式化，返回值形式为yy-mm-dd
function timeFormat(date) {
    if (!date || typeof (date) === "string") {
        this.error("参数异常，请检查...");
    }
    var y = date.getFullYear(); //年
    var m = date.getMonth() + 1; //月
    var d = date.getDate(); //日
    if (m < 10) {
        m = '0' + m;
    }
    if (d < 10) {
        d = '0' + d;
    }
    return y + "-" + m + "-" + d;
}

//获取这周的周一
function getFirstDayOfWeek(date) {
    date = date || new Date();
    var weekday = date.getDay() || 7; //获取星期几,getDay()返回值是 0（周日） 到 6（周六） 之间的一个整数。0||7为7，即weekday的值为1-7

    date.setDate(date.getDate() - weekday + 1);//往前算（weekday-1）天，年份、月份会自动变化
    return timeFormat(date);
}
//获取这周的周日
function getLastDayOfWeek(date) {
    date = date || new Date();
    var weekday = date.getDay() || 7; //获取星期几,getDay()返回值是 0（周日） 到 6（周六） 之间的一个整数。0||7为7，即weekday的值为1-7

    date.setDate(date.getDate() - weekday  + 7);//往前算（weekday-1）天，年份、月份会自动变化
    return timeFormat(date);
}

//获取当月第一天
function getFirstDayOfMonth(date) {
    date = date || new Date();
    date.setDate(1);
    return timeFormat(date);
}
//获取当前月的最后一天
function getLastDayOfMonth(date) {
    date = date || new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var d = new Date(year, month, 0);
    //d.setDate(d.getDate());
    return timeFormat(d);
}
//获取月的天数
function mGetDate() {
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var d = new Date(year, month, 0);
    return d.getDate();
}

//获取当季第一天
function getFirstDayOfSeason(date) {
    date = date || new Date();
    var month = date.getMonth();
    if (month < 3) {
        date.setMonth(0);
    } else if (2 < month && month < 6) {
        date.setMonth(3);
    } else if (5 < month && month < 9) {
        date.setMonth(6);
    } else if (8 < month && month < 11) {
        date.setMonth(9);
    }
    date.setDate(1);
    return timeFormat(date);
}

//获取当年第一天
function getFirstDayOfYear(date) {
    date = date || new Date();
    date.setDate(1);
    date.setMonth(0);
    return timeFormat(date);
}