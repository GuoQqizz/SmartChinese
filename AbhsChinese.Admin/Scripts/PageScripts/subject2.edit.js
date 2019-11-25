let editorOfExplain = null;
let editorOfName = null;

//const regOfGuid = /[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}/g;

///*用正则提取字符串中所有的guid
// *@author zhangchunyu
// *@date 2019-11-7
// *@param{String}content 字符串
// *@return {Array} 一个guid的数组
// */
//function fetchGuids(content) {
//    if (!content) {
//        return [];
//    }

//    let matches = content.match(regOfGuid);
//    return matches;
//}

/*从服务器获取页面显示的数据
 *@author zhangchunyu
 *@date 2019-11-18
 *@return {Object} 封装数据的对象
 */
function getDataFromServer() {
    let pageData = null;

    $.ajax({
        type: 'GET',
        url: $('#loadUrl').val(),
        async: false,
        data: { id: $('#subjectId').val() },
        success: function (response) {
            if (response) {
                pageData = response.Data;
            }
        }
    });

    return pageData;
}

/*把公共部分数据显示到页面
 *@author zhangchunyu
 *@date 2019-11-18
 *@param{String}content 字符串
 */
function displayCommonData(commonPageData) {
    $('#switch_stemType').prop('checked', commonPageData.StemType === 1);
    setImageOrText(commonPageData.StemType === 1);

    $('#name').val(commonPageData.Name);
    $('#explain').val(commonPageData.Explain);
    $('select[name="grade"]').append(new Option(commonPageData.GradeText, commonPageData.Grade, true, true));
    $('select[name="difficulty"]').append(new Option(commonPageData.DifficultyText, commonPageData.Difficulty, true, true));
    $('[name="keywords"]').val(commonPageData.Keywords);

    editorOfName = UE.getEditor('name');
    editorOfExplain = UE.getEditor('explain');

    $('.subject-upload').subjectUpload({
        beforeUpload: function (fileInfo) {
        }
    });

    initICheck();
    initSelect2();
    initTagsInput();
}

/*启用数据验证
 *@author zhangchunyu
 *@date 2019-11-8
 */
function enableValidate() {
    $("#form").validate({
        onsubmit: false,
        rules: {
            name: {
                required: true
            },
            grade: {
                required: true
            },
            difficulty: {
                required: true
            },
            keywords: {
                required: true
            }
        }
    });
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

function changeType(checkbox) {
    let checked = checkbox.checked;
    setImageOrText(checked);
}

function setImageOrText(checked) {
    $('.custom-tab-' + checked.toString()).show();
    $('.custom-tab-' + (!checked).toString()).hide();
}


//初始化tagsinput组件
function initTagsInput() {
    let $input = $('input[name="keywords"]');
    $input.tagsinput({
        tagClass: 'label label-primary',
        maxChars: 10,
        trimValue: true
    });

    $input.on('beforeItemAdd', function (event) {
        if (event.item && event.item.length > 10) {
            event.cancel = true;
            layer.msg('最长10个字符', { icon: 5 });
        }
    });
}

function initSelect2() {
    $('.select2.form-control').select2({
        width: '100%',
        allowClear: true,
        minimumResultsForSearch: Infinity
    }).on('change', function (e) {
        $(this).valid();
    });
}

function initICheck() {
    $('.ichecks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
}


/*手动提交表单
 *@author zhangchunyu
 *@date 2019-11-18
 *@param{int}button 0:保存按钮 1:提交按钮
 */
function manuallySubmitForm(button) {
    var data = {
        NextStatus: $('#nextStatus').val(),
        Button: button,
        Difficulty: $('select[name="difficulty"]').find("option:selected").val(),
        Explain: editorOfExplain.getContent(),
        Grade: $('select[name="grade"]').find("option:selected").val(),
        Id: $('#subjectId').val(),
        Keywords: $('input[name="keywords"]').val(),
        Knowledge: '',
        Knowledges: '',
        Name: '',
        PlainName: $('#plainName').val(),
        SubjectStatus: '',
        Random: $(':checked[name="random"]').val(),
        StemType: $('#switch_stemType').prop('checked') ? 1 : 0,
        ContentType: $('#switch_content_answer').prop('checked') ? 1 : 0,
        Answers: [],
        Display: $(':checked[name="display"]').val(),
        Options: []
    };

    if (data.StemType === 1) {
        data['Name'] = editorOfName.getContent();
    }
    if (data.ContentType === 1) {
        let $options = $('input[name="options"]');
        $.each($options, function (oi, oo) {
            data['Options'].push(oo.value);
        });
    } else {
        let $options = $('img[name="options"]');
        $.each($options, function (oi, oo) {
            data['Options'].push(oo.src);
        });
    }

    let $checkboxes = $(':checked[name="answers"]');
    $.each($checkboxes, function (ci, co) {
        data['Answers'].push(co.value);
    });

    debugger;
    $.ajax({
        type: 'POST',
        url: $('#submitUrl').val(),
        data: data,
        success: function (response) {

        }
    });
}
function submitFormWithoutValidate() {
    manuallySubmitForm(0);
}

function submitForm() {
    manuallySubmitForm(1);
}
