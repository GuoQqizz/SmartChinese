; (function ($) {
    $.fn.extend({
        question: function (option) {
            let self = this;
            let template = loadTemplate('/Content/abhsSwichery/abhsswicherytemplate.html');
            template = replace(template, option.id, /\$\$\$/g);
            self.append(template);

            //初始化ueditor
            UE.getEditor(option.id, ueConfig).ready(function () {
                //let $textarea = $(this.textarea);
                //$textarea.on('change', function () {
                //    //进行数据验证
                //    $textarea.parents('form').validate().element($textarea);
                //});

                //读源码可知，ueditor给它的window属性注册了blur事件，没有给自己注册blur事件
                this.addListener('aftersetvaluetotextarea', function () {
                    self.find('label[for="' + option.id + '"]').hide();
                });
            });

            //点击switchery切换视图
            let $delButton = self.find('button[name="btn_delete"]');
            let $uploadButton = self.find('button[name="btn_scan"]');
            let $checkbox = self.find('#switch_' + option.id);
            let uploader = self.find('div[name="second_view"]');
            let $img = uploader.find('img');
            let $file = self.find('input[type="file"]');
            uploader.extend({
                setHide: function () {
                    this.hide();
                    this.initDefault();
                },
                setUploaded: function (url) {
                    $img.attr('src', url);//为图片设置默认显示的图片
                    $uploadButton.hide();
                    $delButton.show();
                },
                initDefault: function () {
                    $img.attr('src', '/Images/5.png');//为图片设置默认显示的图片
                    $uploadButton.show();
                    $delButton.hide();
                    $file.val('');
                }
            });
            $checkbox.on('change', function () {
                let chk = this;
                UE.getEditor(option.id).ready(function () {
                    if (chk.checked) {
                        uploader.setHide();
                        this.setShow();
                    } else {
                        this.setHide();
                        uploader.show();
                    }
                    self.find('label[for="' + option.id + '"]').hide();
                });
            });

            //文件上传
            $file.on('change', function () {
                let files = this.files;
                let data = new FormData();
                data.append('file', files[0]);
                data.append('subject', 'abhstest');
                data.append('fileName', files[0].fileName);
                data.append('directory', 'test');
                $.ajax({
                    url: 'http://192.168.3.242:8089/Files/Upload',
                    data: data,
                    type: 'POST',
                    processData: false,
                    contentType: false,
                    success: function (response) {
                        if (response.State) {
                            UE.getEditor(option.id).textarea.value = response.FileUrl;
                            $(UE.getEditor(option.id).textarea).change();
                            uploader.setUploaded(response.FileUrl);
                        } else {
                            alert('失败');
                        }
                    }
                });
            });
            //删除文件
            $delButton.on('click', function () {
                uploader.initDefault();
                UE.getEditor(option.id).textarea.value = '';
            });
            //选择文件并上传
            $uploadButton.on('click', function () {
                $file.click();
            });

            self[0].dataset.ueditorId = option.id;
            return self;
        },
        setValue: function (data) {
            let self = this;
            let ueditorId = self[0].dataset.ueditorId;
            //点击switchery切换视图
            let $delButton = self.find('button[name="btn_delete"]');
            let $uploadButton = self.find('button[name="btn_scan"]');
            let $uploader = self.find('div[name="second_view"]');
            let $img = $uploader.find('img');
            $uploader.extend({
                setUploaded: function (url) {
                    $img.attr('src', url);//为图片设置默认显示的图片
                    $uploadButton.hide();
                    $delButton.show();
                }
            });
            let ue = UE.getEditor(ueditorId);
            let type = data.type;
            let $checkbox = self.find('#switch_' + ueditorId);
            if (type) {//文本模式
                ue.setContent(data.content, false);
                $checkbox.prop('checked', true);
            } else {
                //ue.ready(function () {
                //    this.setHide();
                //});
                //$uploader.show();
                $uploader.setUploaded(data.content);
                $checkbox.prop('checked', false);
                $checkbox.change();
            }
        }
    });
})(jQuery);

function loadTemplate(url) {
    let template = null;
    $.ajax({
        type: "GET",
        async: false,
        url: url,
        success: function (html) {
            template = html;
        }
    });
    return template;
}

function replace(str, value, reg) {
    if (!reg) {
        return str;
    }
    return str.replace(reg, value);
}