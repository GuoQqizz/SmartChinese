const studyTaskStatus = {
    1: { status: { text: '未完成', css: '' }, button: { text: '练习', css: '', url: '/StudyTask/Practice' } },
    2: { status: { text: '未完成', css: '' }, button: { text: '练习', css: '', url: '/StudyTask/Practice' } },
    //2: { status: { text: '已超时', css: 'colf47463' }, button: { text: '练习', css: 'col808080', url: 'javascript:void(0);' } },
    3: { status: { text: '已完成', css: 'col50b6b0' }, button: { text: '查看', css: '', url: '/StudentReport/ShowTaskReport' } }
}
const pageSize = 12;

$(function () {
    if ($('#historySearch').val()) {
        restorePage($('#historySearch').val());
    } else {
        loadData(1);
    }

    loadTotalRecordByStatus();

    registerFilterButtonEvent();
    registerRefreshButtonEvent();
});

function restorePage(historySearch) {
    let parameterObj = JSON.parse(historySearch);
    let loadUrl = $(':hidden[name="loadUrl"]').val();
    let pageIndex = parameterObj.Pagination.PageIndex;
    let taskStatus = parameterObj.TaskStatus;
    let pageSize = parameterObj.Pagination.PageSize;

    let loadDataParamer = {
        'pagination.pageIndex': pageIndex,
        'pagination.pageSize': pageSize,
        taskStatus: taskStatus
    };
    $.getJSON(loadUrl, loadDataParamer).done(function (response) {
        displayData(response.Data);
        if (response && response.Data && response.Data.length > 0) {
            let $button = $(`.selectbox a[data-task-status="${taskStatus}"]`);
            $button.addClass('active').siblings().removeClass('active');
        }

        fullUrlToLoadData = '/StudyTask/ListStudyTasks?' + convertToParameterString(loadDataParamer);

        let pageCount = Math.ceil(response.TotalRecord / pageSize); //总页数
        if (pageCount > 1) {
            $('.pagebox').show();
            parger($(".pagebox"), pageCount, pageIndex, loadData);
        } else {
            $('.pagebox').hide();
        }
    });
}

/*分别未完成、已完成、已超时数据的总条数
 *@author zhangchunyu
 *@date 2019-11-6
 */
function loadTotalRecordByStatus() {
    let parameters = {
        taskStatus: '1,2,3'
    };

    let loadUrl = $(':hidden[name="loadUrlOfStatus"]').val();
    $.getJSON(loadUrl, parameters,
        function (response) {
            if (response && response.Data) {
                displayDataOnButton(response.Data);
                console.log(location);
            }
        });
}

let fullUrlToLoadData = '';
function toPractice(studyTaskId, studentTaskId, status, viewUrl) {

    let parameterObj = null;

    if (status === 3) {//状态是已完成
        parameterObj = {
            taskId: studentTaskId,
            source: 1//source是指开场动画 0：有开场动画，1：无开场动画
        };
    } else {
        parameterObj = {
            studyTaskId: studyTaskId,
            studentTaskId: studentTaskId,
            taskType: $('#studyTaskType').val()
        };
    }
    parameterObj['path'] = window.encodeURIComponent(fullUrlToLoadData);
    parameterObj['origin'] = 1;//表示由课后任务列表跳转
    let parameterString = convertToParameterString(parameterObj);

    window.location.href = viewUrl + '?' + parameterString;
}

/*将未完成、已完成、已超时数据的总条数显示到按钮上
 *@author zhangchunyu
 *@date 2019-11-7
 */
function displayDataOnButton(groupedTotalRecord) {
    let total = 0;
    let totalOfUnComplete = 0;
    $.each(groupedTotalRecord, function (i, o) {
        if (o.Status === 1 || o.Status === 2) {
            totalOfUnComplete += o.TotalRecord;
        }
        $(`a[data-task-status="${o.Status}"]`).find('span').text(`(${o.TotalRecord})`);
        total += o.TotalRecord;
    });
    $(`a[data-task-status="1,2"]`).find('span').text(`(${totalOfUnComplete})`);
    $('#a-bll').find('span').text(`(${total})`);
}

/*为过滤按钮注册事件
 *@author zhangchunyu
 *@date 2019-11-6
 */
function registerFilterButtonEvent() {
    $('.taskpage .selectbox a').click(function (e) {
        e.stopPropagation();

        let $self = $(this);
        $self.addClass('active').siblings().removeClass('active');
        loadData(1);

    });
}

/*为刷新按钮注册事件
 *@author zhangchunyu
 *@date 2019-11-6
 */
function registerRefreshButtonEvent() {
    $('span.refurbish').on('click', function (e) {
        e.stopPropagation();

        $('#a-bll').click();
    });
}

/*为页面加载数据
 *@author zhangchunyu
 *@date 2019-11-6
 */
function loadData(page) {
    let data = {
        'pagination.pageIndex': page,
        'pagination.pageSize': pageSize,
        taskStatus: $('.selectbox a.active').data('taskStatus')
    };

    let loadUrl = $(':hidden[name="loadUrl"]').val();
    $.getJSON(loadUrl, data,
        function (response) {
            displayData(response.Data);
            if (response && response.Data && response.Data.length > 0) {
                fullUrlToLoadData = '/StudyTask/ListStudyTasks?' + convertToParameterString(data);
            }
            let pageCount = Math.ceil(response.TotalRecord / pageSize); //总页数
            if (pageCount > 1) {
                $('.pagebox').show();
                parger($(".pagebox"), pageCount, page, loadData);
            } else {
                $('.pagebox').hide();
            }

        });
}

/*把对象中的数据显示到页面
 *@author zhangchunyu
 *@date 2019-11-6
 *@param{Object}pageData 页面数据
 */
function displayData(pageData) {
    if (pageData) {
        let trsHtml = '';
        if (pageData.length === 0) {
            trsHtml = $('#tr-template-0').html();
        } else {
            let trTemplate = getTrHtmlTemplate();
            $.each(pageData, function (i, o) {
                let templateObj = TrHtmlTemplateDataObject.create(o);
                trsHtml += fillTemplateWithData(trTemplate, templateObj);
            });
        }
        $('.tasktable>tbody').html(trsHtml);
    }

    //if (pageData && pageData.length) {
    //    let trsHtml = '';
    //    let trTemplate = getTrHtmlTemplate();
    //    $.each(pageData, function (i, o) {
    //        let templateObj = TrHtmlTemplateDataObject.create(o);
    //        trsHtml += fillTemplateWithData(trTemplate, templateObj);
    //    });
    //    $('.tasktable>tbody').html(trsHtml);
    //}
}

/*获取tr模板的内容，用于生成tr
 *@author zhangchunyu
 *@date 2019-11-6
 *@return {String} 一段<tr><td></td><td></td></tr>代码
 */
function getTrHtmlTemplate() {
    return document.getElementById('tr-template').innerHTML;
}

/*把对象中的数据填充到模板中
 *@author zhangchunyu
 *@date 2019-11-6
 *@param{String}template 模板：一段html代码
 *@param{Object}obj 封装模板中需要展示的数据
 *@return {String} 填充好数据的html代码
 */
function fillTemplateWithData(template, obj) {
    let result = template;
    $.each(obj, function (n, v) {
        let reg = getRegExpByPropertyName(n);
        result = result.replace(reg, v);
    });

    return result;
}

/*根据传入的对象的属性的名字，生成一个特定的正则表达式
 *@author zhangchunyu
 *@date 2019-11-6
 *@param{String}propertyName 属性名字
 *@return {String} 返回：/{{propertyName}}/g
 */
function getRegExpByPropertyName(propertyName) {
    var reg = new RegExp(`{{\s*${propertyName}\s*}}`, 'g');
    return reg;
}

/*封装tr的数据的类
 *@author zhangchunyu
 *@date 2019-11-6
 */
function TrHtmlTemplateDataObject() {
    this.StudyTaskId = 0;
    this.StudentTaskId = 0;
    this.DisplayStartTime = '';
    this.LessonIndex = '';
    this.DisplayFinishTime = '';
    this.SubjectCount = '';
    this.Status = '';
    this.DisplyStatus = '';
    this.ViewUrl = '';
    this.SpanCss = '';
    this.ButtonCss = '';
    this.ButtonText = '';
    this.CourseName = '';
}

TrHtmlTemplateDataObject.create = function (obj) {
    let self = new TrHtmlTemplateDataObject();

    $.each(self, function (n, v) {
        let status = obj['Status'];
        switch (n) {
            case 'ButtonText':
                self[n] = studyTaskStatus[status].button.text;
                break;
            case 'SpanCss':
                self[n] = studyTaskStatus[status].status.css;
                break;
            case 'ButtonCss':
                self[n] = studyTaskStatus[status].button.css;
                break;
            case 'DisplyStatus':
                self[n] = studyTaskStatus[status].status.text;
                break;
            case 'ViewUrl':
                self[n] = studyTaskStatus[status].button.url;
                break;
            default:
                let value = obj[n];
                if (!value && value !== 0) {
                    return true;
                }
                self[n] = value;
                break;
        }
    });

    return self;
}

function convertToParameterString(parameterObj) {
    if (Object.prototype.toString.call(parameterObj) !== '[object Object]') {
        throw new Error('parameterObj必须是对象');
    }
    let parameters = [];
    $.each(parameterObj, function (pn, pv) {
        parameters.push(pn + '=' + pv);
    });
    //添加此参数，阻止浏览器缓存
    parameters.push('r=' + new Date().getTime());
    return parameters.join('&');
}