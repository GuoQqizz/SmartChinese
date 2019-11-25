/*分页方法
    jQdom jq对象 选择器 或 dom对象
    totelPage 总条数
    page 页码
    callback 回调
*/
function parger(jQdom, totelPage, page, callback) {
    var html = "";
    if (jQdom.jquery) {
        jQdom = $(jQdom);
    }
    if (totelPage > 1) {
        var array = [];
        for (var i = 1; i <= totelPage; i++) {
            array.push(i);
        }
        html += "<a href=\"#\" data-page=\"1\">&lt;</a>";
        var l, r;
        if (array.length > 5) {//如果超过5页
            if (page - 3 > 0 && page + 2 < totelPage) {
                array = array.slice(page - 3, page + 2);
                l = r = true;
            }
            else if (page - 3 > 0) {
                array = array.slice(totelPage - 5);
                l = true;
            }
            else if (page + 2 < totelPage) {
                array = array.slice(0, 5);
                r = true;
            }
        }
        if (l) {
            html += "<a href=\"#\" data-page=\"" + (array[0] - 1) + "\">...</a>";
        }
        for (var o = 0; o < array.length; o++) {
            if (array[o] == page) {
                html += `<span class="active">${array[o]}</span>`;
            }
            else {
                html += `<a href=\"#\" data-page="${array[o]}">${array[o]}</a>`;
            }
        }
        if (r) {
            html += "<a href=\"#\" data-page=\"" + (array[array.length - 1] + 1) + "\">...</a>";
        }
        html += "<a href=\"#\" data-page=\"" + totelPage + "\">&gt;</a>";
    }
    if (!jQdom.hasClass("pagebox")) {
        jQdom.addClass("pagebox");
    }
    jQdom.html(html);
    if (typeof (callback) == "function") {
        jQdom.find("a").click(function () {
            var page = $(this).data("page");
            callback(page);
        })
    }
}