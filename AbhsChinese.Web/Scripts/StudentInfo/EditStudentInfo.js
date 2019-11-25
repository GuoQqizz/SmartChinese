$(function () {
    laydate.render({
        elem: '#Birthday', //指定元素,
        max: getNowFormatDate()
    });
});
function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
        + " " + date.getHours() + seperator2 + date.getMinutes()
        + seperator2 + date.getSeconds();
    return currentdate;
}
$('.radiobox').click(function () {
    $(this).parent().find('.radiobox').removeClass('radioboxsel');
    $(this).addClass('radioboxsel');
})
//图片裁切
$('.closeCropperbox').click(function () {
    layer.closeAll();
})
$('.filimg').click(function () {
    layer.open({
        type: 1,
        title: false,
        area: ['600px', '750px'],
        skin: 'layui-layer-demo', //样式类名
        closeBtn: 0, //不显示关闭按钮
        // anim: 2,
        shadeClose: true, //开启遮罩关闭
        content: $('.Cropperbox')
    });
})
var $image = $("#image")
$($image).cropper({
    aspectRatio: 9/9,//1.618
    preview: ".small",
    viewModel: 2,
    crop: function (e) {
        console.log(e);
    }
});
var $inputImage = $("#inputImage");
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
            showMessage("请选择一张图片.");
        }
    });
} else {
    $inputImage.addClass("hide");
}

$("#download").click(function () {
    var url = $image.cropper("getDataURL");
    $('.headPhoto').attr('src', url);
    $("#headPhoto").val(url);
    layer.closeAll();
});

$("#zoomIn").click(function () {
    $image.cropper("zoom", 0.1);
});

$("#zoomOut").click(function () {
    $image.cropper("zoom", -0.1);
});

$("#rotateLeft").click(function () {
    $image.cropper("rotate", 45);
});

$("#rotateRight").click(function () {
    $image.cropper("rotate", -45);
});

$("#setDrag").click(function () {
    $image.cropper("setDragMode", "crop");
});