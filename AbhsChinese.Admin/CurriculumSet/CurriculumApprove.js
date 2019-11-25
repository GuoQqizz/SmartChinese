///加载数据方法
function Load(data) {

    parent.$("#Seach").click();
    console.log("curriculum data is ", data);
    if (!data.State) {
        console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
        $("#LoadingText").text(data.ErrorMsg);
        return;
    }
    else {
        data = data.Data;
        $(".CurriculumTitle").text(data.name);
        if (data.status != 4) {//课时状态 未编辑 = 1,制作中,待审批,审批中,合格,不合格
            $("#LoadingText").text("当前课时不再审批状态");
            $("#NotOKBtn").attr("disabled", true);
            $("#IsOkBtn").attr("disabled", true);
            $(".PageBox").remove();
            return;
        }
    }
    var processid = data.processid || 0;//如果没有审批id赋值为0
    //隐藏加载框
    $("#LoadingBox").hide();
    $("#LoadingText").hide();


    //用于在本页使用的课时设置
    var set = new ChapterSet(data);
    //添加页码
    if (set.pages.length > 0) {
        var str = "";
        for (var p in set.pages) {
            str += '<div id="page_' + set.pages[p].id + '" class="PageThumbnail" ><div class="thumbnail" style="background:url(' + set.pages[p].thumbnail + ');"></div><p class="pargerBox approveType' + set.pages[p].approveType + '">第<span class="pagenum">' + (parseInt(p) + 1) + '</span>页/共<span class="pagecount">' + set.pages.length + '</span>页</p></div>';
        }
        $("#Pages").find(".PageThumbnail").remove();
        $("#Pages").width((set.pages.length) * 180);
        $("#Pages").prepend(str);
    }

    $("#NotOKBtn").click(function () {
        $.ajax({
            url: "/Curriculum/SetChapterStatus",
            type: "post",
            dataType: "json",
            data: { chapterid: data.id, status: 6 },
            success: function (data) {
                if (!data.State) {
                    console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
                    alert("修改失败");
                }

                if (opener && opener.lessonTable) {
                    opener.lessonTable.reload();
                }
                window.close();
            },
            error: function (e) {

            }
        });
    });

    $("#IsOkBtn").click(function () {
        $.ajax({
            url: "/Curriculum/SetChapterStatus",
            type: "post",
            dataType: "json",
            data: { chapterid: data.id, status: 5 },
            success: function (data) {
                if (!data.State) {
                    console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
                    alert("修改失败");
                }

                if (opener || opener.lessonTable) {
                    opener.lessonTable.reload();
                }
                window.close();
            },
            error: function (e) {

            }
        });
    });
    //选择讲义
    $(".PageBox").on("click", ".PageThumbnail", function () {
        if (!$(this).hasClass("selected")) {//如果选中的不是本页
            var index = $(this).index();
            $(".PageThumbnail").removeClass("selected").find(".deletePage").remove();
            $(".PageThumbnail").find(".move").remove();
            $(this).addClass("selected");
            var page = set.getPage(index);
            ////新处理方式,从学生端获取界面
            console.log("url", page.approveAddress);
            $("#ShowInfoBox").attr("src", page.approveAddress);
            $.ajax({
                url: "/Curriculum/GetPage",
                type: "post",
                data: { pageid: page.id, processid: processid },
                dataType: "json",
                success: function (data) {
                    //console.log('page data is', data);
                    if (data.State) {
                        data = data.Data;
                        $("input[name=ApproveType]").prop("checked", false);
                        $("input[name=ApproveType][value=" + (data.approveType || 1) + "]").prop("checked", true);
                        $("#ApproveText").val(data.approve || "");
                        $("#SaveBtn").on("click", function () {
                            $.ajax({
                                url: "/Curriculum/SetUnitProcess",
                                type: "post",
                                data: { courseId: data.courseId, lessonId: data.lessonId, processId: processid, unitId: data.id, status: parseInt($("input[name='ApproveType']:checked").val()), remark: $("#ApproveText").val() },
                                dataType: "json",
                                success: function (data) {
                                    if (data.State) {
                                        console.info("保存成功");
                                    }
                                    else {
                                        console.error("errorCode", data.ErrorCode, "errormsg", data.ErrorMsg);
                                    }
                                },
                                error: function (e) { }
                            });
                        });
                    }
                    else {
                        alert(data.ErrorMsg);
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            })

            ////旧的处理方式,本地页面处理
            ////隐藏播放按钮,并移除事件
            //$("#PlayBtns").hide();
            //$("#lastBtn").off("click");
            //$("#playBtn").off("click").attr("title", "播放").children().removeClass("glyphicon-pause").addClass("glyphicon-play");
            //$("#nextBtn").off("click");
            //$("#SaveBtn").off("click");
            //$.ajax({
            //    url: "/Curriculum/GetPage",
            //    type: "post",
            //    data: { pageid: page.id, processid: processid },
            //    dataType: "json",
            //    success: function (data) {
            //        console.log('page data is', data);
            //        if (data.State) {
            //            data = data.Data;
            //            $("input[name=ApproveType]").prop("checked", false);
            //            $("input[name=ApproveType][value=" + (data.approveType || 1) + "]").prop("checked", true);
            //            $("#ApproveText").val(data.approve || "");
            //            ShowPage(data);
            //            $("#SaveBtn").on("click", function () {
            //                $.ajax({
            //                    url: "/Curriculum/SetUnitProcess",
            //                    type: "post",
            //                    data: { courseId: data.courseId, lessonId: data.lessonId, processId: processid, unitId: data.id, status: parseInt($("input[name='ApproveType']:checked").val()), remark: $("#ApproveText").val() },
            //                    dataType: "json",
            //                    success: function (data) {
            //                        if (data.State) {
            //                            console.info("保存成功");
            //                        }
            //                        else {
            //                            console.error("errorCode", data.ErrorCode, "errormsg", data.ErrorMsg);
            //                        }
            //                    },
            //                    error: function (e) { }
            //                });
            //            });
            //        }
            //        else {
            //            alert(data.ErrorMsg);
            //        }
            //    },
            //    error: function (e) {
            //        console.error(e);
            //    }
            //})
        }
    });
    function ShowPage(data) {
        var showindex = 0, isStop = true;
        $("#PlayBtns").show();
        $("#lastBtn").on("click", function () {
            if (showindex > 0) {
                showindex--;
                Play();
            }
            return false;
        });
        $("#playBtn").on("click", function () {
            if ($(this).children().hasClass("glyphicon-play")) {
                $(this).children().removeClass("glyphicon-play").addClass("glyphicon-pause");
                $(this).attr("title", "暂停");
                isStop = false;
            }
            else {
                $(this).children().removeClass("glyphicon-pause").addClass("glyphicon-play");
                $(this).attr("title", "播放");
                isStop = true;
            }
            Play();
            return false;
        });
        $("#nextBtn").on("click", function () {
            if (showindex < data.steps.length) {
                showindex++;
                Play();
            }
            return false;
        });
        function Play() {
            var page = new Page(data);
            page.selecter = "#ShowInfoBox";
            $(page.selecter).html("");
            $(page.selecter).parent().css({ "background-color": "", "background-image": "" });
            if (isStop) {
                page.draw(showindex);
            }
            else {
                page.show(showindex, function () {
                    if (!isStop) {
                        if (showindex < page.steps.length - 1) {
                            showindex++;
                            Play();
                        }
                        else {
                            $("#playBtn").click();
                        }
                    }
                })
            }
        }
        $("#playBtn").click();
    }
    $(".PageThumbnail").first().click();
}