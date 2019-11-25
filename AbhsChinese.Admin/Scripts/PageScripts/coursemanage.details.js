Dropzone.options.dropz = {
    url: 'http://192.168.3.242:8089/Files/Upload',
    paramName: "file", // The name that will be used to transfer the file
    maxFilesize: 2, // MB
    acceptedFiles: '.jpg,.png,.jpeg',
    autoProcessQueue: true,
    addRemoveLinks: true,
    thumbnailWidth: 525,
    thumbnailHeight: 357,
    dictRemoveFileConfirmation: '123',
    previewsContainer: '#preview',
    dictRemoveFile: '删除封面',
    params: {
        subject: 'abhstest',
        directory: 'CoverImage'
    },
    headers: {
        "Cache-Control": ""
    },
    dictDefaultMessage: "<strong>拖动文件至此或者点击上传</strong></br><strong>只能上传.jpg或者.png格式文件</strong>",
    init: function () {
        //选择完文件后，隐藏上传组件
        this.on("addedfile", function (file) {
            $('#dropz').hide();
        });
        //点击删除文件，触发删除事件
        this.on("removedfile", function (file) {
            //从数据库中删除保存的url
            $.ajax({
                type: "GET",
                url: "/CourseManage/RemoveCoverImage",
                data: { courseId: $('#courseId').val() },
                success: function (response) {
                    layer.msg(response.ErrorMsg, { icon: response.State === true ? 1 : 5 });
                    if (response.State === true) {
                        $('#dropz').show();
                    }
                }
            });
        });
        //当文件上传成功后
        this.on("success", function (file, data) {
            if (data.State === true && data.FileUrl) {
                //将url保存到数据库
                $.ajax({
                    type: "POST",
                    url: "/CourseManage/AddCoverImage",
                    data: { courseId: $('#courseId').val(), CoverImage: data.FileUrl },
                    success: function (response) {
                        if (response.State === true) {
                            $('.dz-remove').show();
                        }
                    }
                });
            }
        });
        //上传文件异常
        this.on("error", function (file, data) {
            layer.msg(data, { icon: 5 });
            $('#preview').empty();
            $('#dropz').show();
        });
        //缩略图生成的事件，用于判断文件的尺寸
        this.on("thumbnail", function (file) {
            if (file.width != 525 || file.height != 357) {
                file.errorWH()
            }
            else {
                file.acceptDimensions();
            }
        });
    },
    accept: function (file, done) {//重写accept方法
        //正常上传，可调用file.acceptDimensions();
        file.acceptDimensions = done;
        //file.rejectDimensions = function () {
        //    done("Invalid dimension.");
        //};
        //图片大小不对时，调用的方法
        file.errorWH = function () {
            done("只能上传525*357大小的图片！");
        }
    }
};

//重写confirm用于自定义确认提示信息
Dropzone.confirm = function (question, accepted, rejected) {
    layer.confirm('确定要删除此封面吗？', { btn: ['确定', '取消'], title: "提示" },
           function (index) {
               layer.close(index);
               return accepted();
           }, function () {
               if (rejected != null) {
                   return rejected();
               }
           });
};

function removeCoverImage() {
    layer.confirm('确定要删除此封面吗？', { btn: ['确定', '取消'], title: "提示" },
           function (index) {
               layer.close(index);
               $.ajax({
                   type: "GET",
                   url: "/CourseManage/RemoveCoverImage",
                   data: { courseId: $('#courseId').val() },
                   success: function (response) {
                       layer.msg(response.ErrorMsg, { icon: response.State === true ? 1 : 5 });
                       if (response.State === true) {
                           $('#dropz').show();
                           $('#preview').empty();
                       }
                   }
               });
           });
}

$(function () {
    loadSchoolLeves();
    loadData($('#courseId').val());
    initTabs();
    initUeditor();
    $('a[href="#tab-1"]').tab('show');
});

function enableValidate() {
    $('#frm_pricing').validate();
}

function initTabs() {
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        let tab = $(e.target).next().val();
        if (tab === 'logs') {
            initLogTable();
        }
    })
}

//加载学校级别
function loadSchoolLeves() {
    $.getJSON('/School/GetSchoolLevelForSelect', {}, function (response) {
        if (response) {
            let template = $('#div_template_schoolLevel').html();
            let html = '';
            $.each(response, function (i, o) {
                let temp = template.replace('$levelName', o.value)
                                   .replace(/\$index/g, i)
                                   .replace('$value', o.key);
                html += temp;
            });
            $('#div_schoolLevelsContainer').html(html);
        }
    });
}

function readonlyBottom() {
    $('input[type="number"]').prop('disabled', true);
    ueditorIntroduction.ready(function () {
        this.setDisabled('fullscreen');
    });
    ueditorArrange.ready(function () {
        this.setDisabled('fullscreen');
    });
}

function loadData(courseId) {
    if (!courseId) {
        return;
    }
    $.getJSON('/CourseManage/GetDetails', { id: courseId }, function (response) {
        let data = response.Data;
        if (data.CoverImage) {
            let imgHtml = `<div class="dz-preview dz-processing dz-image-preview dz-success dz-complete"><div class="dz-image"><img data-dz-thumbnail="" style="width: 525px;height: 357px;" src="${data.CoverImage}"></div><a class="dz-remove" href="javascript:undefined;" onclick="removeCoverImage()" data-dz-remove="">删除封面</a></div>`;
            $('#preview').html(imgHtml);
            $('#dropz').hide();
        }

        if (data.Status === 1) {//未定价
            $('#btnOk').show();
        } else if (data.Status === 2) {//待上架
            $('#btnOk').show();
            $('#btnStartTrading').prop('disabled', false);
        } else if (data.Status === 3) {//已上架
            $('#btnStopTrading').prop('disabled', false);
            readonlyBottom();
        } else if (data.Status === 4) {//已下架
            readonlyBottom();
        }
        let dataOfTable = {
            Id: data.Id,
            Name: data.Name,
            CourseTypeText: data.CourseTypeText,
            GradeText: data.GradeText,
            LessonCount: data.LessonCount,
            OwnerName: data.OwnerName,
            Teachers: data.Teachers,
            Description: data.Description,
            ResourceGroupName: data.ResourceGroupName,
            StatusText: data.StatusText
        };
        if (data.ApprovedLessons) {
            let val = [];
            $.each(data.ApprovedLessons, function (i, o) {
                val.push(`第${o}课时`);
            });
            dataOfTable.ApprovedLessons = val.join('， ');
        }
        $.each(dataOfTable, function (n, v) {
            if (v) {
                //if ($('td[name="' + n + '"] div.ellipsis-text').length > 0) {
                //    $('td[name="' + n + '"] div.ellipsis-text').text(v).attr("title", v);
                //} else {
                //    $('[name="' + n + '"]').text(v);
                //}
                let $ele = $('[name="' + n + '"]');
                $ele.text(v);
                if ($ele.hasClass('short-text')) {
                    $ele.attr("title", v);
                }
            }
        });

        if (data.Pricings) {
            $.each(data.Pricings, function (i, o) {
                $('input[name="pricings[' + i + '].price"]').val(o.Price);
            });
        }
        if (data.Introduction) {
            ueditorIntroduction.ready(function () {
                this.setContent(data.Introduction)
            });
        }
        if (data.Arrange) {
            ueditorArrange.ready(function () {
                this.setContent(data.Arrange)
            });
        }

        enableValidate();
    });
}

function initLogTable() {
    let align = 'center';
    let columns = [
        { label: '操作日期', name: 'CreateTimeStr', align: align },
        { label: '操作人', name: 'OperatorName', align: align },
        { label: '操作类型', name: 'StatusStr', align: align },
        { label: '操作记录', name: 'Remark', align: "left" }
    ];
    $('#table_logs').table({
        ajax: {
            url: '/Curriculum/GetLessonLog',
            type: "post"
        },
        columns: columns,
        searchBox: '#form_search_logs'
    });
}


const ueditorOptions = {
    toolbars: [[
        'source',
        'bold', 'italic', 'forecolor',
        'rowspacingtop', 'rowspacingbottom',
        'fontfamily', 'fontsize',
        'indent',
        'justifyleft', 'justifycenter', 'justifyright',
        'simpleupload'
    ]],
    elementPathEnabled: false,
    wordCount: false,
    enableAutoSave: false,
    enableContextMenu: false,
    initialFrameHeight: 650,
    autoHeightEnabled: false,
    retainOnlyLabelPasted: true
}

let ueditorIntroduction = null;
let ueditorArrange = null;
function initUeditor() {
    ueditorIntroduction = UE.getEditor('introduction', ueditorOptions);
    ueditorArrange = UE.getEditor('arrange', ueditorOptions);
}

function savePrice(button) {
    let $form = $(button).parents('form');

    if (!$form.valid()) {
        return;
    }

    let url = $form.attr('action');
    let data = $form.serialize();

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (reponse) {
            let icon = reponse.State ? 1 : 5;
            layer.msg(reponse.ErrorMsg, { icon: icon }, function () {
                window.location.href = '/CourseManage/ListCourses';
            });
            //if (reponse.State === true) {
            //    $('td[name="StatusText"]').text("待上架");
            //}
        }
    });
}

function stopTrading(courseId) {
    $.ajax({
        type: "POST",
        url: '/CourseManage/StopTrading',
        data: { courseId: courseId },
        success: function (reponse) {
            let icon = reponse.State ? 1 : 5;
            layer.msg(reponse.ErrorMsg, { icon: icon }, function () {
                if (reponse.State === true) {
                    //$('td[name="StatusText"]').text("已下架");

                    //$('#btnStopTrading').prop('disabled', true);
                    window.location.href = '/CourseManage/ListCourses';
                }
            });

        }
    });
}

function startTrading(courseId) {
    let $form = $('#btnOk').parents('form');

    if (!$form.valid()) {
        return;
    }

    let url = $form.attr('action');
    let data = $form.serialize();

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (reponse) {
            if (reponse.State === true) {
                $.ajax({
                    type: "POST",
                    url: '/CourseManage/StartTrading',
                    data: { courseId: courseId },
                    success: function (reponse) {
                        let icon = reponse.State ? 1 : 5;
                        layer.msg(reponse.ErrorMsg, { icon: icon }, function () {
                            if (reponse.State === true) {
                                //$('td[name="StatusText"]').text("已上架");

                                //$('#btnStartTrading').prop('disabled', true);
                                //$('#btnStopTrading').prop('disabled', false);
                                //$('#btnOk').hide();
                                //readonlyBottom();
                                window.location.href = '/CourseManage/ListCourses';
                            }
                        });

                    }
                });
            } else {
                layer.msg(reponse.ErrorMsg, { icon: 5 });
            }
        }
    });



}

function preview(courseId) {
    let url = $('#urlToGetPreviewParameters').val();
    var previewWindow = window.open();
    $.getJSON(url, { courseId: courseId }, function (response) {
        if (response && response.State === true) {
            let parameters = response.Data.Parameters;
            let paramArr = [];
            $.each(parameters, function (pn, pv) {
                paramArr.push(pn + '=' + pv);
            });
            parameters = paramArr.join('&');
            let urlToPreview = response.Data.PreviewUrl;
            debugger;
            previewWindow.location.href = urlToPreview + '?' + parameters;
        }
    });
}