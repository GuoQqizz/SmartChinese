const regOfGuid = /[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}/g;

/*用正则提取字符串中所有的guid
 *@author zhangchunyu
 *@date 2019-11-7
 *@param{String}content 字符串
 *@return {Array} 一个guid的数组
 */
function fetchGuids(content) {
    if (!content) {
        return [];
    }

    let matches = content.match(regOfGuid);
    return matches;
}

/*启用数据验证
 *@author zhangchunyu
 *@date 2019-11-8
 *@param{String}content 字符串
 *@return {Array} 一个guid的数组
 */
function enableValidate() {
    $("#form").validate({
        onsubmit: false,
        ignore: "",
        rules: {
            name: {
                required: true
            },
            answer: {
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

function submitForm(needValid) {
    let $form = $('#form');
    let $hidden = $form.find(':hidden[name="button"]');
    if (needValid)//需要进行数据校验
    {
        $hidden.val(2);
        if (!$form.valid()) {//但是校验没通过
            return false;
        }
    } else {
        $hidden.val(1);
    }
    $form.submit();
}

function changeType(checkbox) {
    ueditorName.ready(function () {
        this.setContent('');
        this.textarea.value = '';
        $('#plainName').val('');
    });
    toggleUeditorType(ueditorName, checkbox.checked);
}


function toggleUeditorType(ueditor, isText) {
    let parentDiv = $('div#' + ueditor.key);
    if (isText) {//文本模式
        $.each(classesOfTextUeditor, function (index, className) {
            parentDiv.find('div.' + className).attr('style', '');
        });
        parentDiv.find('div.' + classOfImageUeditor).attr('style', 'display:none !important;');
    } else {//图片模式
        $.each(classesOfTextUeditor, function (index, className) {
            parentDiv.find('div.' + className).attr('style', 'display:none !important;');
        });
        parentDiv.find('div.' + classOfImageUeditor).attr('style', '');
    }
}

function setContent(ueditor, content) {
    if (content) {
        ueditor.ready(function () {
            this.setContent(content);
        });
    }
}

let factoryUeditor = null;
//为ueditor注册blur事件，当触发时，获取纯文本存到hidden中
//subject表的Name字段需要纯文本的ueditor内容
function replaceFormat(ueditor) {
    ueditor.addListener('blur', function () {
        let content = this.getContent();
        let plainttxt = '';
        let imgReg = /<img/g;
        if (content.match(imgReg)) {
            return;
        }
        let reg = /<input.{0,}?>/g;
        plainttxt = content.replace(reg, '__');
        factoryUeditor.setContent(plainttxt);
        plainttxt = factoryUeditor.getContentTxt();
        $('#plainName').val(plainttxt);
    });
}

$(function () {
    factoryUeditor = UE.getEditor('factory', {
        toolbars: [[]],
        elementPathEnabled: false,
        wordCount: false,
        enableAutoSave: false,
        enableContextMenu: false,
        initialFrameHeight: 80
    });

    initTagsInput();
    registerEventToTagsinput();
});

function registerEventToTagsinput() {
    $('.tagsinput').on('beforeItemAdd', function (event) {
        if (event.item && event.item.length > 10) {
            event.cancel = true;
            layer.msg('最长10个字符', { icon: 5 });
        }
    });
}
function initTagsInput() {
    $('input[name="keywords"]').tagsinput({
        tagClass: 'label label-primary',
        maxChars: 10,
        trimValue: true
    });
}

//给隐藏域赋值，该隐藏域用于处理ueditor的内容
function setPlainNameData(data) {
    $('#plainName').val(data.PlainName);
}

function formatContent(content, isText) {
    let result = content;
    if (isText === true) {
        factoryUeditor.setContent(content);
        result = factoryUeditor.getContentTxt();
    }
    return result;
}