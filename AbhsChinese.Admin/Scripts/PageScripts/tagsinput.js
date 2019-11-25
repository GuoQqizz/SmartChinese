$(function () {
    //CheckBox 样式
    var elem = document.querySelector('.js-switch');
    var switchery = new Switchery(elem, { color: '#1AB394' });

    //关键词选择设置
    $('.tagsinput').tagsinput({
        tagClass: 'label label-primary',
        maxCharsOnBlur: true,
        maxChars: 10,
        trimValue: true
    });
    $('.tagsinput').on('itemAdded', function (event) {
        $(".divkey").removeClass("input-key");
    });
    $('.tagsinput').on('change', function (event) {
        $(this).valid();
    });
    $('.tagsinput').on('itemRemoved', function (event) {
        var isExists = $(this).valid();
        if (isExists == false) {
            $(".divkey").addClass("input-key");
        }
    });

    $('#ajaxForm').validate({
        ignore: '',
        errorPlacement: function (error, element) {
            if (element.is(".tagsinput")) {
                $(".divkey").addClass("input-key");
                error.appendTo(element.parent().parent());
            }
            else {
                $("#selectfiles").addClass("select-error");
                error.insertAfter(element);
            }
        },
        rules: {
            Name: {
                required: true,
                maxlength: 50,
                isBlank:true
            },
            TextType: {
                required: true
            },
            MediaType: {
                required: true
            },
            Grade: {
                required: true
            },
            Key: {
                required: true
            },
            Content: {
                required: true
            },
            Url: {
                required: true
            }
        },
        messages: {
            Name: {
                required: "请输入名称",
                maxlength: "长度不能大于50个字符"
            },
            TextType: {
                required: "请选择文本类型"
            },
            MediaType: {
                required: "请选择多媒体类型"
            },
            Grade: {
                required: "请选择年级"
            },
            Key: {
                required: "请输入关键词"
            },
            Content: {
                required: "请输入内容"
            },
            Url: {
                required: "请选择资源"
            }
        },
        submitHandler: function (form) {
            ajaxSubmit();
        }
    });
})