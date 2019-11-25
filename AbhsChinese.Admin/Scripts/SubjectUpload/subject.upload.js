; (function ($) {
    $.fn.extend({
        subjectUpload: function (option) {
            let $container = this;
            if (!$container.hasClass('.subject-upload')) {
                $container.addClass('.subject-upload');
            }

            let html = '<span class="subject-thumbnail"></span><button type="button" class="btn btn-default">选择文件</button><button type="button" class="btn btn-danger" style="display:none;">删除</button><div class="subject-file"></div>';
            $container.html(html);

            let $selectButton = $container.find('.btn-default');
            let $deleteButton = $container.find('.btn-danger');

            $selectButton.off('click').on('click', function () {
                let $selfBtn = $(this);

                let $file = $('<input type="file" name="file" style="display:none;" />');
                $file.off('change').on('change', function () {
                    let file = this.files[0];
                    if (file) {
                        var reader = new FileReader();
                        reader.readAsDataURL(file);
                        reader.onload = function (e) {
                            var img = new Image();
                            img.onload = function (e) {
                                if (option.beforeUpload) {
                                    let result = option.beforeUpload({ width: img.width, height: img.height });
                                    debugger;
                                    if (result === false) {
                                        return;
                                    }
                                }

                                uploadFile(option.url, file, option.parameters, function (response) {
                                    if (response && response.State === true) {
                                        $deleteButton.show();
                                        $selfBtn.hide();
                                        $container.find('.subject-thumbnail').html(img);
                                    }
                                });
                            };
                            img.src = e.target.result;
                        }
                    }
                });
                $container.find('.subject-file').html($file);
                $file.trigger('click');
            });

            $deleteButton.off('click').on('click', function () {
                let $selfBtn = $(this);

                layer.confirm('确定要删除图片吗？', {
                    icon: 2,
                    title: '提示',
                    yes: function (index) {
                        $selectButton.show();
                        $container.find('.subject-thumbnail').empty();
                        $selfBtn.hide();
                        $container.find('.subject-file').empty();
                        layer.close(index);
                    },
                    cancel: function (index, layero) {
                        layer.close(index);
                    }
                });

            });
        }
    });
})(jQuery);

function uploadFile(url, file, parameters, callback) {
    let data = new FormData();
    $.each(parameters, function (pn, pv) {
        data.append(pn, pv);
    });
    data.append('fileName', file.name);
    data.append('file', file);
    //$.ajax({
    //    url: url,
    //    data: data,
    //    type: 'POST',
    //    processData: false,
    //    contentType: false,
    //    success: function (response) {
    //        callback(response);
    //    }
    //});
    callback({ State: true });
}