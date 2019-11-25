
$(document).ready(function () {

    //选课筛选条件展开折叠
    $('.showselection').click(function () {
        if ($(this).attr('r') == '1') {
            $(this).attr('r', '0');
            $(this).html('展开<img src="/Images/Course/s.png" />')
            $('.seleciton').stop(true, false).animate({
                "height": $('.seleciton').find('.selectbox').eq(0).height() + 17
            })
        } else {
            $(this).attr('r', '1');
            var newh = $('.seleciton').css('height', 'auto').height();
            $('.seleciton').height($('.seleciton').find('.selectbox').eq(0).height() + 17);
            console.log('newh', newh);
            $(this).html('收起<img src="/Images/Course/hide.png" />')
            $('.seleciton').stop(true, false).animate({
                "height": newh + 'px'
            })
        }
    });

    //初始化筛选条件
    $("div.selectbox div.listitem a[data-grade='" + searchData.Grade + "']").parent().addClass('active').siblings().removeClass('active');
    $("div.selectbox div.listitem a[data-ct='" + searchData.CourseType + "']").parent().addClass('active').siblings().removeClass('active');
    $("div.selectbox div.listitem a[data-orderby='" + searchData.OrderBy + "']").parent().addClass('active').siblings().removeClass('active');
    //点击筛选条件重新加载课程
    $("div.selectbox div.listitem a").click(function () {
        $(this).parent().addClass('active').siblings().removeClass('active');
        if ($(this).attr("data-grade")) {
            searchData.Grade = $(this).attr("data-grade");
        }
        if ($(this).attr("data-ct")) {
            searchData.CourseType = $(this).attr("data-ct");
        }
        if ($(this).attr("data-orderby")) {
            searchData.OrderBy = $(this).attr("data-orderby");
        }

        searchData.Pagination.PageIndex = 1;
        searchData.HasMore = true;
        loadCourse();
    });

    var canLoadCourse = true;
    function loadCourse() {
        if (searchData.HasMore && canLoadCourse) {
            canLoadCourse = false;
            $.ajax({
                url: "/Course/GetCourses",
                type: "POST",
                dataType: "json",
                data: searchData,
                success: function (result) {
                    if (result.State) {
                        if (searchData.Pagination.PageIndex == 1) {
                            //初始化或者更新了筛选条件，需要移除原有数据
                            $("div.goodslist .goodsitem").remove();
                            if (result.Data.DataList.length == 0) {
                                $("div.goodslist .nogoods").show();
                                $("div.goodslist .moredata").hide();
                            }
                        }
                        if (result.Data.DataList != undefined & result.Data.DataList != null && result.Data.DataList.length > 0) {
                            $("div.goodslist .nogoods").hide();
                            $(renderCourse(result.Data.DataList, result.Data.IncludePrice)).insertBefore("div.goodslist .moredata");
                            //图片加载失败时用默认图替换
                            $('div.goodslist div.imgdiv img').imagesLoaded()
                                .fail(function (instance) {
                                    $.each(instance.images, function (index, image) {
                                        if (!image.isLoaded) {
                                            $(image.img).attr("src", "/Images/Course/defaultimg.jpg");
                                        }
                                    });
                                });

                            searchData.Pagination.PageIndex += 1;
                        }
                        searchData.HasMore = result.Data.HasMore;
                        if (searchData.HasMore) {
                            $("div.goodslist .moredata").show();
                        } else {
                            $("div.goodslist .moredata").hide();
                        }
                    }
                },
                complete: function (XMLHttpRequest, textStatus) {
                    canLoadCourse = true;
                }
            });
        }
    }
    //呈现课程列表信息
    function renderCourse(items, isIncludePrice) {
        var lines = [];
        $.each(items, function (index, item) {
            var currentLine = parseInt(index / 3);
            lines[currentLine] = lines[currentLine] || false;
            if (item.BaseVoucherPrice > 0 || item.SchoolVoucherPrice > 0) {
                lines[currentLine] = true;
            }
        });

        var content = "";
        $.each(items, function (index, item) {
            var name = item.CourseName + "（共" + item.LessonCount + "课时）";
            content += "<div class=\"goodsitem\">";
            content += "	<a class=\"goodsbox\" href=\"/Course/Detail/" + item.CourseId + "\" target=\"_self\">";
            content += "	    <div class=\"imgdiv\"><img src=\"" + item.CourseImage + "\" /></div>";
            if (isIncludePrice) {
                var payable = item.CoursePrice - (item.BaseVoucherPrice + item.SchoolVoucherPrice);
                payable = payable > 0 ? payable : 0;
                content += "	    <div class=\"goodsinfo\">";
                content += "	        <div class=\"fl\">";
                content += "	            <span class=\"pirce\"><em>￥</em>" + item.CoursePrice + "</span>";
                content += "	            <span class=\"pirce1\">&nbsp;￥" + payable + "<img src=\"/Images/Course/zh.png\" /></span>";
                content += "	        </div>";
                content += "	        <div class=\"soldnum\">已售:" + item.SellCount + "</div>";
                content += "	    </div>";
            }
            content += "	    <div class=\"goodslabel\">";
            content += "	        <div class=\"fl\">";
            content += "	            <span class=\"clas\">" + getGradeName(item.Grade) + "</span>";
            content += "	            <span class=\"claty\">" + getCourseTypeName(item.CourseType) + "</span>";
            content += "	        </div>";
            content += "	        <div class=\"goodsname line1\" title=\"" + name + "\">&nbsp;&nbsp;" + name + "</div>";
            content += "	    </div>";
            if (isIncludePrice && lines[parseInt(index / 3)]) {
                content += "	    <div class=\"conpouboxconfig\">";
                if (item.BaseVoucherPrice > 0) {
                    content += "	        <span class=\"zbq\"><label><em>总部券￥" + item.BaseVoucherPrice + "</em></label></span>&nbsp;&nbsp;";
                }
                if (item.SchoolVoucherPrice > 0) {
                    content += "	        <span class=\"xqq\"><label><em>校区券￥" + item.SchoolVoucherPrice + "</em></label></span>";
                }
                content += "	    </div>";
            }
            content += "	</a>";
            content += "</div>";
        });
        return content;
    }
    //加载更多
    $("div.goodslist .moredata").click(function () {
        loadCourse();
    });
    $("div.goodslist .moredata").triggerHandler("click");

    $(window).scroll(function (event) {
        var wScrollY = window.scrollY; // 当前滚动条位置  
        var wInnerH = window.innerHeight; // 设备窗口的高度（不会变）  
        var bScrollH = document.body.scrollHeight; // 滚动条总高度
        if (document.body.clientWidth < 1900) {
            //console.log(wScrollY, wInnerH, bScrollH)
            if (wScrollY + wInnerH >= bScrollH * 0.8) {
                //console.log("触发底部");
                $("div.goodslist .moredata").triggerHandler("click");
            }
        } else {
            if (wScrollY + wInnerH >= bScrollH) {
                //console.log("触发底部");
                $("div.goodslist .moredata").triggerHandler("click");
            }
        }
    });

    //顶部广告
    var swiper = new Swiper('.swiper-container1', {
        spaceBetween: 0,
        autoplay: {
            delay: 5000,
            stopOnLastSlide: false,
            disableOnInteraction: true,
        },
        effect: 'fade',
        pagination: {
            el: '.swiper-pagination1',
            clickable: true,
        },
        observer: true,
        observeParents: true,
    });

    //点击顶部广告
    $("a.courseListAd").click(function () {
        var courseId = $(this).attr("data-courseId");
        var aid = $(this).attr("data-aid");
        var that = this;
        $.ajax({
            url: "/Course/HitMessage",
            type: "POST",
            dataType: "json",
            data: { courseId: courseId, aid: aid },
            complete: function (XMLHttpRequest, textStatus) {
                $(that).unbind().attr("href", $(that).attr("data-href"))
                $(that)[0].click();
            }
        });
    });
});