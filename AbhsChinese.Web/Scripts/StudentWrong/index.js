let localPage = 1;
$(function () {
    renderHtml();
    loadQuery();
    loadData();
    registerEvent();
});

function renderHtml() {
    renderDate();
    setDate(getFirstDayOfMonth(), getLastDayOfMonth());
}
//渲染时间控件
function renderDate() {
    dtRange = laydate.render({
        elem: '#dateRange',
        range: '~', //或 range: '~' 来自定义分割字符
        value: '',
        //calendar: true,
        done: function (value, date, endDate) {
            $('.dt-select').removeClass('active');
            $('.dt-select:eq(0)').addClass('active');
            $('#dateRange').val(value);//控件应该是在done方法执行完之后才更新dom的值，所以在此处手动更新下，或者放在延迟方法里
            loadData(1);
            //setTimeout(loadData, 100);
        }
    });
}
//设置时间
function setDate(dtStart, dtEnd) {
    let val = '';
    if (dtStart && dtEnd) {
        val = `${dtStart} ~ ${dtEnd}`;
    }
    $('#dateRange').val(val);
}
function loadQuery() {
    try {
        let toPage = window.localStorage.getItem('topath') || '';
        toPage = toPage.toLocaleLowerCase();
        let currentPage = window.location.pathname.toLowerCase();
        if (toPage == currentPage || toPage == currentPage + '/index') {
            let query = window.localStorage.getItem('query') || '';
            if (query) {
                query = JSON.parse(query);
                if (query['range']) {
                    $('#dateRange').val(query['range']);
                }
                if (query['index']) {
                    $(`.dt-select:eq(${query['index']})`).addClass('active').siblings().removeClass('active');
                }
                if (query['page']) {
                    localPage = query['page']
                }
            }
        }
    } catch (e) {

    }
}
//保存查询参数
function saveQuery() {
    let range = $('#dateRange').val();
    try {
        let query = {
            range: range,
            page: localPage,
            index: $('.dt-select.active').index()
        };
        window.localStorage.setItem('query', JSON.stringify(query));
        window.localStorage.setItem('topath', '');
    } catch (e) {

    }
}
//加载数据
function loadData(page) {
    page = page || localPage;
    localPage = page;
    let range = $('#dateRange').val();
    let start = '', end = '';
    if (range) {
        start = range.split('~')[0];
        end = range.split('~')[1];
    }
    $('.errpbox').html('');
    $('.pagebox').hide();
    saveQuery(page);
    $.ajax({
        url: listDataUrl,
        type: 'POST',
        data: {
            BeginDate: start,
            EndDate: end,
            Pagination: {
                PageIndex: page,
                PageSize: pageSize,
            },
        },
        success: function (res) {
            if (res && res.ErrorCode == 0 && res.Data.length > 0) {
                let books = res.Data;
                let inner = ''
                for (let i = 0; i < books.length; i++) {
                    let tp = $('#boxItemTmp').html();
                    let book = books[i];
                    tp = tp.replace('{{className}}', book.Yws_Status == 1 ? 'active' : '')
                            .replace('{{DetailUrl}}', detailUrl + '?bookId=' + book.Ywb_Id)
                            .replace('{{CourseName}}', book.CourseName)
                            .replace('{{CourseLessonName}}', book.CourseLessonName ? '-' + book.CourseLessonName : '')
                            .replace('{{CreateTimeStr}}', book.CreateTimeStr)
                            .replace('{{SourceStr}}', book.SourceStr)
                            .replace('{{Yws_WrongKnowledgeCount}}', book.Yws_WrongKnowledgeCount)
                            .replace('{{Yws_RemoveCount}}', book.Yws_RemoveCount)
                            .replace('{{Yws_WrongCount}}', book.Yws_WrongCount - book.Yws_RemoveCount)
                    inner += tp;
                }
                $('.errpbox').html(inner);
                initPage(res.TotalRecord, page);
            } else {
                initPage(0, page);
                $('.errpbox').html($('#boxNodataTmp').html());
            }
        },
        error: function (err) {
            console.error(err);
            $('.errpbox').html($('#boxErrorTmp').html());
        }
    })
}
//渲染分页
function initPage(total, page) {
    //total = 15;
    let pages = Math.ceil(total / pageSize);
    if (pages > 1) {
        $('.pagebox').show();
        parger($(".pagebox"), pages, page, loadData);
    } else {
        $('.pagebox').hide();
    }

}
//注册事件
function registerEvent() {
    $('.dt-select').on('click', function () {
        var type = $(this).data('type');
        switch (type) {
            case 1://全部
                dtStart = '';
                dtEnd = '';
                break;
            case 2://本月
                dtStart = getFirstDayOfMonth();
                dtEnd = getLastDayOfMonth();
                break;
            case 3://本周
                dtStart = getFirstDayOfWeek();
                dtEnd = getLastDayOfWeek();
                break;
            default:
                dtStart = getFirstDayOfYear();
                dtEnd = timeFormat(new Date());
        }
        $(this).siblings().removeClass('active');
        $(this).addClass('active');
        setDate(dtStart, dtEnd);
        loadData(1);
    })
}