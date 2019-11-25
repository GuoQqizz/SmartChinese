; $(function () {
    if ($('#historySearch').val()) {
        restorePage($('#historySearch').val());
    } else {
        getCourses(1);
    }
});
const pageSize = 6;

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

let fullUrlToLoadData = '';
function practiceAfterClass(returnCount, courseId, a) {
    let canPractice = $(a).parents('div.info2').data('canPractice');
    if (returnCount > canPractice) {
        return;
    }

    let path = window.encodeURIComponent(fullUrlToLoadData);
    let parameterObj = {
        courseId: courseId,
        returnCount: returnCount,
        path: path
    };
    let parameterString = convertToParameterString(parameterObj);
    window.location.href = '/StudyTask/PracticeSubjects?' + parameterString;
}

function restorePage(historySearch) {
    let parameterObj = JSON.parse(historySearch);

    let url = '/StudentPractice/GetCoursesAttended';
    let pageIndex = parameterObj.Pagination.PageIndex;
    let pageSize = parameterObj.Pagination.PageSize;
    let parameters = {
        'pagination.pageIndex': pageIndex,
        'pagination.pageSize': pageSize
    };

    $.getJSON(url, parameters)
        .done(function (response) {
            displayData(response.Data);
            processData(response.Data);
            
            fullUrlToLoadData = '/StudentPractice/ListCourses?' + convertToParameterString(parameters);
            
            let pageCount = Math.ceil(response.TotalRecord / pageSize); //总页数
            if (pageCount > 1) {
                $('.pagebox').show();
                parger($(".pagebox"), pageCount, pageIndex, getCourses);
            } else {
                $('.pagebox').hide();
            }
        });
}

function processData(data) {
    if (data && data.length > 0) {
        let searches = '';
        let courseIds = '';
        $.each(data, function (i, o) {
            courseIds += `courseIds=${o.CourseId}&`;

            searches += `searches[${i}].CourseId=${o.CourseId}&searches[${i}].NextLessonIndex=${o.NextLessonIndex}&`;
        });

        searches = searches.substring(0, searches.length - 1);
        $.getJSON('/StudentPractice/GetSubjectsToPractice', searches).done(function (res) {
            let data = res.Data;
            $.each(data, function (i, o) {
                let $div = $('#div-' + o.CourseId);
                $div.data('canPractice', o.TotalSubjectCount);
                let $as = $('#div-' + o.CourseId + ' a');
                $.each($as, function (ai, a) {
                    let $a = $(a);
                    let toPratice = $a.data('toPractice');
                    if (toPratice <= o.TotalSubjectCount) {
                        $a.removeClass('defaultbtn');
                    }
                });
            });
        });
        courseIds = courseIds.substring(0, courseIds.length - 1);
        $.getJSON('/StudentPractice/GetTotalSubjectsPracticed', courseIds)
            .done(function (r) {
                let data = r.Data;
                $.each(data, function (i, o) {
                    $('#span-' + o.CourseId).text(o.TotalSubjectCount);
                });
            });
    }
}

function getCourses(page) {
    let url = '/StudentPractice/GetCoursesAttended';

    let parameters = {
        'pagination.pageIndex': page,
        'pagination.pageSize': pageSize
    };
    $.getJSON(url, parameters)
        .done(function (response) {
            let data = response.Data;
            displayData(data);
            processData(data);
            if (data && data.length > 0) {

                fullUrlToLoadData = '/StudentPractice/ListCourses?' + convertToParameterString(parameters);
            }

            let pageCount = Math.ceil(response.TotalRecord / pageSize); //总页数
            if (pageCount > 1) {
                $('.pagebox').show();
                parger($(".pagebox"), pageCount, page, getCourses);
            } else {
                $('.pagebox').hide();
            }
        });
}
const courseType = {
    1: '同步课',
    2: '专项课',
    3: '复习课',
    4: '整本书阅读课',
    5: '文学史课'
};

/*把数据显示到页面
 *@author zhangchunyu
 *@date 2019-11-7
 *@param{Object}pageData 一个数组，封装着列表需要的数据
 */
function displayData(pageData) {
    if (pageData && pageData.length > 0) {
        $('.pageimgerror').hide();
        let trsHtml = '';
        let template = getLessonTemplate();
        $.each(pageData, function (i, o) {
            let templateObj = TemplateDataObject.create(o);
            trsHtml += fillTemplateWithData(template, templateObj);
        });
        $('div.afterclasspage').html(trsHtml);
    } else {
        $('.pageimgerror').show();
    }
}

/*获取显示数据的html模板
 *@author zhangchunyu
 *@date 2019-11-7
 */
function getLessonTemplate() {
    return document.getElementById('lesson-template').innerHTML;
}

/*封装列表的数据的类
 *@author zhangchunyu
 *@date 2019-11-6
 */
function TemplateDataObject() {
    this.CourseName = '';
    this.CourseType = '';
    this.CourseId = '';
    this.DisplayCourseType = '';
    this.LessonCount = '';
    //this.SchoolName = '';
    this.ClassName = '';
    this.DisplayGrade = '';
}

const courseGrade = {
    1: "一年级",
    2: "二年级",
    4: "三年级",
    8: "四年级",
    16: "五年级",
    32: "六年级",
    64: "初一",
    128: "初二",
    256: "初三",
    512: "高一",
    1024: "高二",
    2048: "高三"
}

TemplateDataObject.create = function (obj) {
    let self = new TemplateDataObject();

    $.each(self, function (n, v) {
        let rn = n.replace('Display', '');
        let type = obj[rn];

        switch (n) {
            case 'DisplayGrade':
                self[n] = courseGrade[type];
                break;
            case 'DisplayCourseType':
                self[n] = courseType[type];
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

/*把对象中的数据填充到模板中
 *@author zhangchunyu
 *@date 2019-11-7
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
 *@date 2019-11-7
 *@param{String}propertyName 属性名字
 *@return {String} 返回：/{{propertyName}}/g
 */
function getRegExpByPropertyName(propertyName) {
    var reg = new RegExp(`{{\s*${propertyName}\s*}}`, 'g');
    return reg;
}