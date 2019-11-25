; $(function () {
    loadData();
    registerButtonEvent();
});

let table = null;
let align = 'center';
let columns = [
    { label: '编号', name: 'Id', align: align, width: 50 },
    {
        label: '封面', name: 'CoverImageFull', align: align, width: 100, formatter: function (index, value, row) {
            if (!value) {
                return '';
            }
            return `<img src="${value}" class="img-info" />`;
        }
    },
    {
        label: '课程名称', name: 'Name', align: 'left', width: 300, formatter: function (index, value, row) {
            if (value) {
                return `<div style="width:300px" class="short-text">${value}</div>`;
            } else {
                return '';
            }
        }
    },
    { label: '类型', name: 'CourseTypeText', align: align, width: 100 },
    { label: '年级', name: 'GradeText', align: align, width: 80 },
    { label: '负责人', name: 'OwnerName', align: align, width: 80 },
    {
        label: '合格课时', name: 'ApprovalLesCount', align: 'center;', width: 100,
        formatter: function (index, value, row) {
            return `<span>${value.lenWithZero(2)}&nbsp;(共${row.LessonCount.lenWithZero(2)}课时)</span>`;
        }
    },
    {
        label: '课程定价(￥)', name: 'Price', align: align, width: 160,
        formatter: function (index, value, row) {
            let html = '<ul class="clearfix" style="text-align:left;">$lis</ul>';
            let lis = '';
            $.each(row.Price, function (n, v) {
                lis += `<li style="list-style: none;">${n}:<span style="float:right;">${v.toFixed(1)}</span></li>`;
            });
            html = html.replace('$lis', lis);
            return html;
        }
    },
    { label: '状态', name: 'StatusText', align: align, width: 80 },
    {
        label: '操作', name: '', align: align, width: 80,
        formatter: function (index, value, row) {
            let url = '/CourseManage/Details/' + row.Id;
            return '<a href="' + url + '">操作</a>';
        }
    }
];
//为列表加载数据
function loadData() {
    table = $('#table_list').table({
        ajax: {
            url: '/CourseManage/GetCourses'
        },
        columns: columns,
        searchBox: '#form_search'
    });
}

const activeClass = 'btn-primary';
const defaultClass = 'btn-default';

//全部 待上架 已上架等按钮的事件
function registerButtonEvent() {
    $('button[name="btn_state"]').on('click', function (e) {
        e.preventDefault();
        //如果当前按钮不是选中状态
        let $self = $(this);
        if (!$self.hasClass(activeClass)) {
            resetFilterCondition($self);
            highlightButton($self);
        }
    });
}

//高亮显示当前按钮
function highlightButton($button) {
    $button.addClass(activeClass).removeClass(defaultClass)
        .siblings('.' + activeClass).removeClass(activeClass).addClass(defaultClass);
}
function resetFilterCondition($button) {
    let $form = $('#form_search');
    let $checkboxes = $form.find(':checkbox.search-hide');

    let state = $button.data('state');
    if (state === -1) {
        $.each($checkboxes, function (i, o) {
            $(o).prop('checked', true);
        });
    } else {
        $.each($checkboxes, function (i, o) {
            let val = parseInt(o.value);
            if (val === state) {
                $(o).prop('checked', true);
            } else {
                $(o).prop('checked', false);
            }
        });
    }

    $form.find('.search').click();
}

