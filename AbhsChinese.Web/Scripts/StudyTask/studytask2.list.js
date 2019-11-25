; $(function () {
    initFilterbar();
    loadDataToTable();
});

function initFilterbar() {
    $('.filterbar').filterbar({
        propertyNameToSubmit: 'taskStatus',
        buttons: [
            { text: '未完成', value: 1 },
            { text: '已完成', value: 2 },
            { text: '已超时', value: 3 }
        ]
    }, function (parameters) {
        $('#form_search .search').click();
    });
}

const studyTaskStatus = {
    1: { status: { text: '未完成', className: '' }, button: { text: '练习', className: 'text-info', url: '/StudyTask/Practice' } },
    2: { status: { text: '已超时', coclassNamelor: 'text-danger' }, button: { text: '练习', className: '', url: 'javascript:void(0);' } },
    3: { status: { text: '已完成', className: 'text-info' }, button: { text: '查看', className: 'text-info', url: '/StudyTask/ViewReport' } }
}

function loadDataToTable() {
    let columns = [
        { label: '时间', name: 'DisplayStartTime', align: 'center;width:15%;' },
        {
            label: '对应课程', name: '', align: 'left', formatter: function (index, value, row) {
                return `<span class="title">${row.CourseName}-${row.LessonName}课后任务</span>`;
            }
        },
        { label: '截止时间', name: 'DisplayFinishTime', align: 'center;width:15%;' },
        { label: '题目数量', name: 'SubjectCount', align: 'center;width:5%;' },
        {
            label: '状态', name: 'Status', align: 'center;width:5%;', formatter: function (index, value, row) {
                let statusInfo = studyTaskStatus[row.Status].status;
                return `<span class="${statusInfo.className}">${statusInfo.text}</span>`;
            }
        },
        {
            label: '操作', name: '', align: 'center;width:10%;',
            formatter: function (index, value, row) {
                let button = studyTaskStatus[row.Status].button;
                button.url += `?studyTaskId=${row.StudyTaskId}`;
                return `<a href="${button.url}" class="${button.className}">${button.text}</a>`;
            }
        }
    ];
    $('#div_table').table({
        ajax: {
            url: '/StudyTask/GetStudyTasks',
            type: 'GET'
        },
        columns: columns,
        searchBox: '#form_search'
    });
}