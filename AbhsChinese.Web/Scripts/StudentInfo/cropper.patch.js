function initCropper(container) {
    var $image = $(container).find(".image-crop > img");

    $image.cropper({
        aspectRatio: 1.618,
        preview: ".img-preview",
        done: function (data) {
            // Output the result data for cropping image.
        }
    });

    var $inputImage = $(container).find("#inputImage");
    if (window.FileReader) {
        $inputImage.change(function () {
            var fileReader = new FileReader(),
                    files = this.files,
                    file;

            if (!files.length) {
                return;
            }

            file = files[0];

            if (/^image\/\w+$/.test(file.type)) {
                fileReader.readAsDataURL(file);
                fileReader.onload = function () {
                    $inputImage.val("");
                    $image.cropper("reset", true).cropper("replace", this.result);
                };
            } else {
                showMessage("Please choose an image file.");
            }
        });
    } else {
        $inputImage.addClass("hide");
    }

    $(container).find('label[name="upload"]').click(function () {
        let imgBase64 = $image.cropper("getDataURL");
        let imgBase64Array = imgBase64.split(';base64,');
        if (imgBase64Array.length !== 2) {
            layer.msg('未找到文件', { icon: 5 });
            return false;
        }
        $.ajax({
            url: '/FileManage/UploadBase64',
            type: 'POST',
            data: {
                content: imgBase64Array[1]
            },
            success: function (response) {
                if (response) {
                    $('#profilePicture').val(response);
                } else {
                    layer.msg('上传失败', { icon: 5 });
                }
            }
        });
    });

    //$("#zoomIn").click(function () {
    //    $image.cropper("zoom", 0.1);
    //});

    //$("#zoomOut").click(function () {
    //    $image.cropper("zoom", -0.1);
    //});

    //$("#rotateLeft").click(function () {
    //    $image.cropper("rotate", 45);
    //});

    //$("#rotateRight").click(function () {
    //    $image.cropper("rotate", -45);
    //});
}

function disposeCropper(container) {
    $(container).find(".image-crop > img").cropper('destroy');
    $(container).find('label[name="upload"]').off('click');
    $(container).find("#inputImage").off('change');
}