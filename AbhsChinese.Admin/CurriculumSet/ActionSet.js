
//获取元素距离页面左侧位置
function getElementPageLeft(element) {
    var actualLeft = element.offsetLeft;
    var parent = element.offsetParent;
    while (parent != null) {
        actualLeft += parent.offsetLeft + (parent.offsetWidth - parent.clientWidth) / 2;
        parent = parent.offsetParent;
    }
    return actualLeft;
}

//获取元素距离页面顶部位置
function getElementPageTop(element) {
    var actualTop = element.offsetTop;
    var parent = element.offsetParent;
    while (parent != null) {
        actualTop += parent.offsetTop + (parent.offsetHeight - parent.clientHeight) / 2;
        parent = parent.offsetParent;
    }
    return actualTop;
}
function randomString(len) {
    len = len || 32;
    var $chars = 'abcdefghijklmnopqrstuvwxyz0123456789';   
    var maxPos = $chars.length;
    var pwd = '';
    for (i = 0; i < len; i++) {
        pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
    }
    return pwd;
}
$(function () {
    $("#backBtn").click(function () {
        window.location.href = "/CurriculumSet/ActionOptions.html";
    });
    $("#closeBtn").click(function () {
        var frame = top.frames["ActionSet"];
        if (frame) {
            $(frame).hide();
        }
    });
    //如果父级有chapter 并且还是我们的章节对象才执行下面设置方法.
    if (top.CurriculumInfo && top.CurriculumInfo.chapterSet instanceof top.ChapterSet) {
        //判断有没有制作框,有制作框的情况下会添加制作框的基本动作事件
        var makeBox = $("#MakeBox");
        if (makeBox.length > 0) {
            //检测编辑区域大小,防止无法操作
            if ($(".SetBox").height() < 620 || $(".SetBox").width() < 1286) {
                $(".SetBox").css({ "background-color": "#fff" }).html("<p style='position: absolute;top: 50%;margin-top:-20px; text-align:center;width:100%'>您的操作区域过小,不适合编辑.<br/>您可以尝试关闭后设置全屏后再次尝试.</p>");
                return;
            }
            var topheight = getElementPageTop(makeBox[0]);
            var leftwidth = getElementPageLeft(makeBox[0]);
            //计算鼠标位置
            $("#InnerArea").mousemove(function (e) {
                var _this = $(this)[0];
                var x = e.pageX - leftwidth - 153;
                var y = e.pageY - topheight - 20;
                $(".mouse-info").text("x:" + x + ",y:" + y);
            })
        }
    }
    else {
        $(".SetBox").css({ "background-color": "#fff" }).html("<p style='position: absolute;top: 50%;margin-top:-20px; text-align:center;width:100%'>请不要再外部打开此界面,这样会造成无法编辑.</p>");
        return;
    }
})