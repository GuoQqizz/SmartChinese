///加载数据方法
function Load(data) {

    console.log("curriculum data is ", data);
    if (!data.State) {
        console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
        $("#LoadingText").text(data.ErrorMsg);
        return;
    }
    else {
        data = data.Data;
        $(".CurriculumTitle").text(data.name);
    }
    var processid = data.processid || 0;//如果没有审批id赋值为0
    var ChapterStatus = data.status;//课时状态 未编辑 = 1,制作中,待审批,审批中,合格,不合格
    var ChapterEdit = ChapterStatus == 1 || ChapterStatus == 2;//判断课时编辑状态,
    //隐藏加载框
    $("#LoadingBox").hide();
    $("#LoadingText").hide();
    //生成动作html
    function actionHtml(action) {
        switch (action.type) {
            case "setTitle": return setTitleHtml(action);
            case "setBackground": return setBackgroundHtml(action);
            case "xiaoAiSay": return xiaoAiSayHtml(action);
            case "xiaoAiChange": return xiaoAiChangeHtml(action);
            case "insertImg": return insertImgHtml(action);
            case "insertText": return insertTextHtml(action);
            case "setWaitMillisecond": return setWaitMillisecondHtml(action);
            case "moveDom": return moveDomHtml(action);
            case "scaleDom": return scaleDomHtml(action);
            case "twinkleDom": return twinkleDomHtml(action);
            case "hideDom": return hideDomHtml(action);
            case "studyAudio": return studyAudioHtml(action);
            case "studyVideo": return studyVideoHtml(action);
            case "studyArticle": return studyArticleHtml(action);
            case "studyRecitation": return studyRecitationHtml(action);
            case "studyFastReadEasy": return studyFastReadEasyHtml(action);
            case "studyFastRead": return studyFastReadHtml(action);
            case "studyAnnotation": return studyAnnotationHtml(action);
            case "studyAnnotation2": return studyAnnotation2Html(action);
            case "studyJudgment": return studyJudgmentHtml(action);
            case "studyLinking": return studyLinkingHtml(action);
            case "studyOption": return studyOptionHtml(action);
            case "studyOptionFill": return studyOptionFillHtml(action);
            case "studyFill": return studyFillHtml(action);
            case "studySubjective": return studySubjectiveHtml(action);
            case "studyCalligraphy": return studyCalligraphyHtml(action);
            default: return "<li id='action_" + action.actionId + "'>未知动作</li>";


        }
    }
    //用于标题和文字的方法,用于吧特殊标记改为对应字符
    function SetText(str) {
        str = str.replace(/<br\/>/g, "\n");
        str = str.replace(/&lt;/g, "<");
        str = str.replace(/&gt;/g, ">");
        str = str.replace(/&nbsp;/g, " ");
        return str;
    }
    //根据课时状态,判断是否添加动作操作按钮(注:所有的生成动作的html方法都需要,用这个方法添加编辑删除按钮)
    function makeActionBtn() {
        var str = ""
        if (ChapterEdit) {
            str += "<i class='SetBtn glyphicon glyphicon-edit'></i>";
            str += "<i class='DeleteBtn glyphicon glyphicon-remove'></i>";
        }
        return str;
    }
    //#region--------------动作生成html方法--------------------------

    //生成设置标题动作
    function setTitleHtml(action) {
        var str = "<li type='setTitle' id='action_" + action.actionId + "' class='Action setTitle'>";
        str += "<p class='text'>设置标题:" + SetText(action.text).ellipsis(8, "...") + "</p>";
        str += "<p class='intype'>进入效果:";
        switch (action.intype) {
            case "none": str += "无效果"; break;
            case "top": str += "顶部飞入"; break;
            case "left": str += "左侧飞入"; break;
            case "right": str += "右侧飞入"; break;
            case "bottom": str += "底部飞入"; break;
            default: str += "无效果"; break;
        }
        str += "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成设置背景动作
    function setBackgroundHtml(action) {
        var str = "<li type='setBackground' id='action_" + action.actionId + "' class='Action setBackground'>";
        str += "<p class='text'>设置背景:" + (action.bgType == "color" ? "纯颜色" : "图片") + "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //小艾说
    function xiaoAiSayHtml(action) {
        var str = "<li type='xiaoAiSay' id='action_" + action.actionId + "' class='Action xiaoAiSay'>";
        str += "<p class='text'>小艾说:" + action.medianame + "</p>";
        //str += "<i class='PlayBtn glyphicon glyphicon-play' audiosrc='" + action.src + "'></i>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //小艾变
    function xiaoAiChangeHtml(action) {
        var str = "<li type='xiaoAiChange' id='action_" + action.actionId + "' class='Action xiaoAiChange'>";
        str += "<p class='text'>小艾“变”:" + action.imgId + "(" + action.imgName + ")</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成插入图片动作
    function insertImgHtml(action) {
        var str = "<li type='insertImg' id='action_" + action.actionId + "' class='Action insertImg'>";
        str += "<p class='text'>插入图片:" + action.imgId + "</p>";
        str += "<p class='intype'>进入效果:";
        switch (action.intype) {
            case "none": str += "无效果"; break;
            case "top": str += "顶部飞入"; break;
            case "left": str += "左侧飞入"; break;
            case "right": str += "右侧飞入"; break;
            case "bottom": str += "底部飞入"; break;
            default: str += "无效果"; break;
        }
        str += "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成插入文本动作
    function insertTextHtml(action) {
        var str = "<li type='insertText' id='action_" + action.actionId + "' class='Action insertText'>";
        str += "<p class='text'>插入文本:" + SetText(action.text).ellipsis(8, "...") + "</p>";
        str += "<p class='intype'>进入效果:";
        switch (action.intype) {
            case "none": str += "无效果"; break;
            case "top": str += "顶部飞入"; break;
            case "left": str += "左侧飞入"; break;
            case "right": str += "右侧飞入"; break;
            case "bottom": str += "底部飞入"; break;
            default: str += "无效果"; break;
        }
        str += "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成等待动作
    function setWaitMillisecondHtml(action) {
        var str = "<li type='setWaitMillisecond' id='action_" + action.actionId + "' class='Action setWaitMillisecond'>";
        str += "<p class='text'>等待:" + action.stop + "ms</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成移动元素动作
    function moveDomHtml(action) {
        var str = "<li type='moveDom' id='action_" + action.actionId + "' class='Action moveDom'>";
        str += "<p class='text'>移动:" + action.objectId + "元素</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成缩放元素动作
    function scaleDomHtml(action) {
        var str = "<li type='scaleDom' id='action_" + action.actionId + "' class='Action scaleDom'>";
        str += "<p class='text'>缩放:" + action.objectId + "元素</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成闪烁元素动作
    function twinkleDomHtml(action) {
        var str = "<li type='twinkleDom' id='action_" + action.actionId + "' class='Action twinkleDom'>";
        str += "<p class='text'>闪烁:" + action.objectId + "元素</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成隐藏元素动作
    function hideDomHtml(action) {
        var str = "<li type='hideDom' id='action_" + action.actionId + "' class='Action hideDom'>";
        str += "<p class='text'>隐藏:" + action.objectId + "元素</p>";
        str += "<p class='intype'>隐藏效果:";
        switch (action.outtype) {
            case "none": str += "无效果"; break;
            case "top": str += "顶部飞出"; break;
            case "left": str += "左侧飞出"; break;
            case "right": str += "右侧飞出"; break;
            case "bottom": str += "底部飞出"; break;
            default: str += "无效果"; break;
        }
        str += "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成音频动作
    function studyAudioHtml(action) {
        var str = "<li type='studyAudio' id='action_" + action.actionId + "' class='Action studyAudio'>";
        str += "<p class='text'>音频:" + action.medianame + "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成视频动作
    function studyVideoHtml(action) {
        var str = "<li type='studyVideo' id='action_" + action.actionId + "' class='Action studyVideo'>";
        str += "<p class='text'>视频:" + action.medianame + "</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成图文动作
    function studyArticleHtml(action) {
        var str = "<li type='studyArticle' id='action_" + action.actionId + "' class='Action studyArticle'>";
        str += "<p class='text'>图文</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成朗读动作
    function studyRecitationHtml(action) {
        var str = "<li type='studyRecitation' id='action_" + action.actionId + "' class='Action studyRecitation'>";
        str += "<p class='text'>朗读</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成快速阅读-简单动作
    function studyFastReadEasyHtml(action) {
        var str = "<li type='studyFastReadEasy' id='action_" + action.actionId + "' class='Action studyFastReadEasy'>";
        str += "<p class='text'>快速阅读-简单</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成快速阅读动作
    function studyFastReadHtml(action) {
        var str = "<li type='studyFastRead' id='action_" + action.actionId + "' class='Action studyFastRead'>";
        str += "<p class='text'>快速阅读</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成圈点批注动作
    function studyAnnotationHtml(action) {
        var str = "<li type='studyAnnotation' id='action_" + action.actionId + "' class='Action studyAnnotation'>";
        str += "<p class='text'>圈点批注-标色</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成圈点批注动作
    function studyAnnotation2Html(action) {
        var str = "<li type='studyAnnotation2' id='action_" + action.actionId + "' class='Action studyAnnotation2'>";
        str += "<p class='text'>圈点批注-断句</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成判断题动作
    function studyJudgmentHtml(action) {
        var str = "<li type='studyJudgment' id='action_" + action.actionId + "' class='Action studyJudgment'>";
        str += "<p class='text'>判断题</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成连线题动作
    function studyLinkingHtml(action) {
        var str = "<li type='studyLinking' id='action_" + action.actionId + "' class='Action studyLinking'>";
        str += "<p class='text'>连线题</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成选择题动作
    function studyOptionHtml(action) {
        var str = "<li type='studyOption' id='action_" + action.actionId + "' class='Action studyOption'>";
        str += "<p class='text'>选择题</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成选择填空动作
    function studyOptionFillHtml(action) {
        var str = "<li type='studyOptionFill' id='action_" + action.actionId + "' class='Action studyOptionFill'>";
        str += "<p class='text'>选择填空题</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成填空题动作
    function studyFillHtml(action) {
        var str = "<li type='studyFill' id='action_" + action.actionId + "' class='Action studyFill'>";
        str += "<p class='text'>填空题</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成主观题动作
    function studySubjectiveHtml(action) {
        var str = "<li type='studySubjective' id='action_" + action.actionId + "' class='Action studySubjective'>";
        str += "<p class='text'>主观题</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //生成田字格写字动作
    function studyCalligraphyHtml(action) {
        var str = "<li type='studyCalligraphy' id='action_" + action.actionId + "' class='Action studyCalligraphy'>";
        str += "<p class='text'>田字格写字</p>";
        str += makeActionBtn();
        str += "</li>";
        return str;
    }
    //#regionEnd--------------动作生成html方法--------------------------






    //存储本页的所有动作id,防止重复
    var actionids = [];
    //生成actionid
    function MakeActionId() {
        var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var str = chars.substr(Math.floor(Math.random() * 26), 1);//先从前26个字母中选出一个
        for (var i = 1; i < 6; i++) {
            str += chars.substr(Math.floor(Math.random() * 36), 1);//再从整个字符串中随机出5个
        }

        var has = false;//假设暂时没有这个id
        for (var id in actionids) {
            if (actionids[id] == str) {//如果有相同的id记录并退出循环
                has = true;
                break;
            }
        }
        if (has) {//如果有相同的字符,再次随机
            return MakeActionId();
        } else {//否则返回当前内容
            return str;
        }

    }

    //课程信息,用于子界面调用
    window.CurriculumInfo = {
        //课程数据对象
        chapterSet: new ChapterSet(data),
        //当前选择页
        selectPage: null,
        //当前选择动作
        selectStep: null,
        //选中的动作
        selectAction: null,
        //添加动作
        AddAction: function AddAction(action) {
            action.actionId = MakeActionId();
            console.log("new actionid", action.actionId);
            CurriculumInfo.selectStep.addAction(action);
            $(".Step.selected").find(".Actions").append(actionHtml(action));

            $(".Step.selected").find(".StepBtn").html(StepBtnLoad(CurriculumInfo.selectStep));//重新绑定步骤按钮
            BindStep();
            //重新绘制
            CurriculumInfo.selectPage.draw(CurriculumInfo.selectStep.stepNum);
            BindAction();
        },
        //设置动作
        SetAction: function SetAction(action) {
            if (true) {
                //循环设置页面中的对象属性
                for (var i in action) {
                    CurriculumInfo.selectAction[i] = action[i];
                }

                var sAction = $(".Step.selected").find(".Actions li").eq(CurriculumInfo.selectAction.actionNum);//获取动作的标签
                sAction.after(actionHtml(action));
                sAction.remove();
                CurriculumInfo.selectPage.draw(CurriculumInfo.selectStep.stepNum);
                BindAction();
            }
        },
    }
    //用于在本页使用的课时设置
    var set = CurriculumInfo.chapterSet;

    //添加页码
    if (set.pages.length > 0) {
        var str = "";
        for (var p in set.pages) {
            str += '<div id="page_' + set.pages[p].id + '" class="PageThumbnail" ><div class="thumbnail" style="background:url(' + set.pages[p].thumbnail + ');"></div><p class="pargerBox approveType' + set.pages[p].approveType + '">第<span class="pagenum">' + (parseInt(p) + 1) + '</span>页/共<span class="pagecount">' + set.pages.length + '</span>页</p></div>';
        }
        $("#Pages").find(".PageThumbnail").remove();
        $("#Pages").width((set.pages.length + 1) * 180);
        $("#Pages").prepend(str);
        $("#Pages").find(".PageThumbnail").first().addClass("selected");
        if (ChapterEdit) {
            $("#Pages").find(".PageThumbnail").first().append("<b class='deletePage'>×</b><p class='move'><i class='moveleft glyphicon glyphicon-arrow-left'></i><i class='moveright glyphicon glyphicon-arrow-right'></i></p>");
        }
        CurriculumInfo.selectPage = set.getPage(0);
        LoadPageSteps(0);
        BindSelected();
    }


    if (ChapterEdit) {
        //如果是编辑状态,添加编辑事件
        $("#AddPage").click(function () {
            var _this = this;
            var index = $(this).index();
            if (CurriculumInfo.selectPage) {
                if (!CurriculumInfo.selectPage.isSave && !confirm("本页还没有保存,确认继续切换页?")) {//如果没有保存,并且取消切换页
                    return;//不再进行下面的步骤
                }
            }
            $.ajax({
                url: "/Curriculum/AddPage",
                type: "post",
                data: { page: { courseId: data.courseId, lessonId: data.id, pageNum: index + 1 } },
                dataType: "json",
                async: false,
                success: function (data) {
                    if (!data.State) {
                        console.error("addpage", "error:", data.ErrorCode, "message", data.ErrorMsg);
                    }
                    else {
                        //创建对象
                        CurriculumInfo.selectPage = new Page();
                        CurriculumInfo.selectPage.id = data.Data;
                        //添加对象
                        set.addPage(CurriculumInfo.selectPage);
                        LoadPageSteps(CurriculumInfo.selectPage.pageNum);
                        $(".PageThumbnail").removeClass("selected").find(".deletePage").remove();
                        $(".PageThumbnail").removeClass("selected").find(".move").remove();
                        $(".PageThumbnail .pagecount").html(set.pages.length);
                        $(_this).before("<div class='PageThumbnail selected'><div class=\"thumbnail\"></div><p class=\"pargerBox\">第<span class=\"pagenum\">" + set.pages.length + "</span>页/共<span class=\"pagecount\">" + set.pages.length + "</span>页</p><b class='deletePage'>×</b><p class='move'><i class='moveleft glyphicon glyphicon-arrow-left'></i><i class='moveright glyphicon glyphicon-arrow-right'></i></p></div>");
                        $(_this).addClass("selected");
                        var width = (set.pages.length + 1) * 180;
                        if (width < $(".PageInnerBox").width()) {
                            width = $(".PageInnerBox").width();
                        }
                        $("#Pages").width(width + "px");
                        $(".PageBox").scrollLeft(width - $(".PageBox").width());
                        BindSelected();
                    }
                },
                error: function (e) {
                    console.error(e);
                }
            });

        });
        //添加步骤
        $("#AddStep").click(function () {
            if (CurriculumInfo.selectPage) {
                var step = new Step();
                CurriculumInfo.selectPage.addStep(step);
                CurriculumInfo.selectStep = step;
                $(".Step").removeClass("selected").find(".StepBtn").html("");
                var str = '<div class="Step selected">';
                str += '<div class="StepTitleBox">';
                str += '<span class="StepTitle">步骤' + (step.stepNum + 1) + '</span>';
                str += '<p class="StepBtn">';
                str += '<i class="stepMoveUp glyphicon glyphicon-arrow-up"></i><i class="stepMoveDown glyphicon glyphicon-arrow-down"></i><i class="addAction glyphicon glyphicon-plus-sign"></i><i class="deleteStep glyphicon glyphicon-remove-sign"></i>';
                str += '</p>';
                str += '</div>';
                str += '<ul class="Actions">';
                str += '</ul>';
                str += '</div>';
                $(".StepList").append(str);
                BindStep()
            }
            else {
                alert("还没有选择讲义页");
            }
        });

        $("#OverBtn").html("提交审批").click(function () {
            if (!CurriculumInfo.selectPage.isSave) {
                if (!confirm("本页未保存,确定提交审批么?"))
                {
                    return;
                }
            }
            if (set.pages.length > 0) {
                $.ajax({
                    url: "/Curriculum/SetChapterStatus",
                    type: "post",
                    dataType: "json",
                    data: { chapterid: data.id, status: 3 },
                    success: function (data) {
                        if (!data.State) {
                            console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
                            alert("修改失败");
                        }
                        if (opener && opener.lessonTable) {
                            opener.lessonTable.reload();
                        }
                        window.location.reload();
                    },
                    error: function (e) {

                    }
                });
            }
            else {
                alert("请添加最少一个讲义页.");
            }
        });

        //设置名称
        $("#pageRemark").keyup(function () {
            if (CurriculumInfo.selectPage) {
                CurriculumInfo.selectPage.name = $(this).val();
            }
        })

        //保存按钮点击事件
        $("#SavePage").click(function () {
            $("#LoadingBox").show();
            $("#LoadingText").text("正在保存...").show();


            if (CurriculumInfo.selectPage) {
                $(".Step").last().click();
                html2canvas(document.querySelector("#ShowBox"), {
                    width: 1066,
                    height: 600,
                    //proxy: '/FileManage/GetUrlFile', //使用未改动的html2canvas版本
                    useCORS: true,//跨域
                }).then(function (c) {
                    var canvas = document.createElement("canvas");
                    canvas.style.position = "absolute";
                    canvas.style.top = "100px";
                    canvas.width = 160;
                    canvas.height = 90;
                    canvas.style.width = "160px";
                    canvas.style.height = "90px";
                    var ctx = canvas.getContext("2d");
                    ctx.drawImage(c, 0, 0, c.width, c.height, 0, 0, 160, 90);
                    CurriculumInfo.selectPage.thumbnail = canvas.toDataURL();
                    c.remove();
                    canvas.remove();
                    $(".PageThumbnail.selected .thumbnail").css({ "background-image": "url(" + CurriculumInfo.selectPage.thumbnail + ")" });
                    var d = JSON.stringify(CurriculumInfo.selectPage);
                    console.log("getdata", d);
                    $.ajax({
                        url: "/Curriculum/SetPage",
                        type: "post",
                        data: { pagestr: d },
                        dataType: "json",
                        async: false,
                        success: function (data) {
                            if (!data.State) {
                                console.error("提交数据", "error:", data.ErrorCode, "message", data.ErrorMsg);
                            }
                            else {
                                CurriculumInfo.selectPage.isSave = true;
                                console.log("success", data.Data);
                                $("#LoadingText").text("保存成功").fadeOut(1000);
                                $("#LoadingBox").fadeOut(1000);
                            }

                        },
                        error: function (e) {
                            console.error(e);
                            $("#LoadingText").text("保存失败").fadeOut(1000);
                            $("#LoadingBox").fadeOut(1000);
                        }
                    })
                });
            }
        });

    }
    else { //如果不能编辑则删除编辑按钮
        $("#AddPage").remove();
        $("#AddStep").remove();
        if (ChapterStatus == 5 || ChapterStatus == 6) {
            $("#OverBtn").html("重新编辑").click(function () {
                if (confirm("确定要重新编辑么?这样会需要重新进行审批才能在学生端使用.")) {
                    $.ajax({
                        url: "/Curriculum/SetChapterStatus",
                        type: "post",
                        dataType: "json",
                        data: { chapterid: data.id, status: 2 },
                        success: function (data) {
                            if (!data.State) {
                                console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
                                alert("修改失败");
                            }
                            window.location.reload();
                        },
                        error: function (e) {

                        }
                    });
                }
            });
        }
        else {//审批中不显示审批信息
            $("#OverBtn").html(ChapterStatus == 3 ? "待审批" : "审批中").attr("disabled", true);
            $("#myTabs li").eq(1).hide();
        }

        $("#pageRemark").attr("disabled", true);
        $("#SavePage").remove();
    }

    //选择讲义
    $(".PageBox").on("click", ".PageThumbnail", function () {
        if (!$(this).hasClass("selected")) {//如果选中的不是本页
            if (CurriculumInfo.selectPage && !CurriculumInfo.selectPage.isSave && !confirm("本页还没有保存,确认继续切换页?")) {//如果没有保存,并且取消切换页
                return;//不再进行下面的步骤
            }
            var index = $(this).index();
            CurriculumInfo.selectPage = set.getPage(index);
            $(".PageThumbnail").removeClass("selected").find(".deletePage").remove();
            $(".PageThumbnail").find(".move").remove();
            $(this).addClass("selected");
            if (ChapterEdit) {//如果是编辑状态添加移动删除按钮
                $(this).prepend("<b class='deletePage'>×</b><p class='move'><i class='moveleft glyphicon glyphicon-arrow-left'></i><i class='moveright glyphicon glyphicon-arrow-right'></i></p>");
            }
            BindSelected();
            LoadPageSteps(index);

        }
    });
    //绑定页事件
    function BindSelected() {

        $(".deletePage").off("click");
        $(".deletePage").on("click", function () {
            if (confirm("确认删除当前页么?")) {
                var index = $(this).parent().index();
                var _this = this;
                $.ajax({
                    url: "/Curriculum/DeletePage",
                    type: "post",
                    data: { pageid: set.getPage(index).id },
                    dataType: "json",
                    success: function (data) {
                        if (data.State) {
                            set.deletePage(index);
                            $(".PageThumbnail").removeClass("selected").find(".move").remove();
                            $(_this).parent().remove();
                            if (set.pages.length > 0) {
                                if (index == set.pages.length) {
                                    index--;
                                }
                                $(".PageThumbnail").eq(index).addClass("selected").prepend("<b class='deletePage'>×</b><p class='move'><i class='moveleft glyphicon glyphicon-arrow-left'></i><i class='moveright glyphicon glyphicon-arrow-right'></i></p>");
                                CurriculumInfo.selectPage = set.getPage(index);
                                LoadPageSteps(CurriculumInfo.selectPage.pageNum);
                            }
                            else {
                                //$(".PageThumbnail").last().addClass("selected").prepend("<b class='deletePage'>×</b><p class='move'><i class='moveleft glyphicon glyphicon-arrow-left'></i><i class='moveright glyphicon glyphicon-arrow-right'></i></p>");
                                CurriculumInfo.selectPage = null;
                                $("#pageNum").text(0);
                                $("#pageRemark").val("没有选中任何页");
                                $(".StepList").html("");
                            }

                            $(".PageThumbnail .pagecount").html(set.pages.length);
                            $(".PageThumbnail .pagenum").each(function (i, v) {
                                $(this).html(i + 1);
                            })

                            BindSelected();

                            var width = (set.pages.length + 1) * 180;
                            $("#Pages").width(width + "px");                           
                        }
                        else {
                            alert("删除失败");
                        }
                    },
                    error: function (e) {
                        console.error(e);
                    }
                })
            }
            return false;
        });
        $(".moveleft").off("click");
        $(".moveleft").on("click", function () {
            var _this = this;
            var index = $(_this).parent().parent().index();
            if (index > 0) {
                $(".PageThumbnail").eq(index - 1).before($(_this).parent().parent());
                var page = set.getPage(index);
                set.movePage(index, index - 1);
                $("#pageNum").text(index)
                BindSelected();

                MovePage(page.id, index, function () {
                    alert("移动失败");
                    $(".PageThumbnail").eq(index).after($(_this).parent().parent());
                    set.movePage(index - 1, index);
                    $("#pageNum").text(index);
                    BindSelected();
                });
                return false;
            }
        });
        $(".moveright").off("click");
        $(".moveright").on("click", function () {
            var _this = this;
            var index = $(_this).parent().parent().index();
            if (index < $(".PageThumbnail").length - 1) {
                $(".PageThumbnail").eq(index + 1).after($(_this).parent().parent());
                var page = set.getPage(index);
                set.movePage(index, index + 1);
                $("#pageNum").text(index + 2);
                BindSelected();
                MovePage(page.id, index + 2, function () {
                    alert("移动失败");
                    $(".PageThumbnail").eq(index).before($(_this).parent().parent());
                    set.movePage(index + 1, index);
                    $("#pageNum").text(index + 2)
                    BindSelected();
                });
                return false;
            }
        });
        function MovePage(id, index, callerrorback) {

            $(".PageThumbnail .pagecount").html(set.pages.length);
            $(".PageThumbnail .pagenum").each(function (i, v) {
                $(this).html(i + 1);
            })
            $.ajax({
                url: "/Curriculum/MovePage",
                type: "post",
                data: { unitid: id, index: index },
                dataType: "json",
                success: function (data) {
                    if (!data.State) {
                        console.error("提交数据", "error:", data.ErrorCode, "message", data.ErrorMsg);
                        if (typeof (callerrorback) == "function") {
                            callerrorback(data);
                            $(".PageThumbnail .pagecount").html(set.pages.length);
                            $(".PageThumbnail .pagenum").each(function (i, v) {
                                $(this).html(i + 1);
                            })
                        }
                    }
                    else {
                        console.log("success", data.Data);
                    }

                },
                error: function (e) {
                    callerrorback(e);
                    console.error(e);
                }
            })
        }

    }

    ////选择的步骤
    //var selectStep;

    //加载页面步骤
    function LoadPageSteps(pageNum) {
        var page = set.getPage(pageNum);/*这个地方可能要改为后台请求*/

        $(".StepList").html("<p class='StepLoading'>正在加载步骤数据...</p>");
        $.ajax({
            url: "/Curriculum/GetPage",
            type: "post",
            data: { pageid: page.id, processid: processid },
            dataType: "json",
            success: function (data) {
                console.log("page data is ", data);
                if (!data.State) {
                    console.error("error:", data.ErrorCode, "message", data.ErrorMsg);
                    return;
                }
                else {
                    data = data.Data;
                }
                $("#ApproveBox").html("<p><label>审批结果类型:</label>" + (data.approveType == 2 ? "<span style='color:#ff0000'>错误</span>" : data.approveType == 3 ? "<span style='color:#ff6a00'>疑问</span>" : "<span style='color:#000000'>正常</span>") + "</p><p><label>审批意见:</label><br/>" + (data.approve || "无") + "</p>");

                actionids = [];//重置当前拥有的动作id数组
                for (var s in data.steps) {//循环所有步骤中的动作记录id
                    var step = data.steps[s];
                    for (var a in step.actions) {
                        actionids.push(step.actions[a].actionId);
                    }
                }
                console.log("actionids", actionids);

                page = CurriculumInfo.selectPage = set.pages[pageNum] = new Page(data);
                console.log("page  is ", page);
                page.selecter = "#ShowInfoBox";
                page.draw(page.steps.length - 1);
                $("#pageNum").text(pageNum + 1);
                $("#pageRemark").val(page.name);
                $(".StepList").html("");

                for (var i in page.steps) {
                    var step = page.steps[i];
                    var str = '<div class="Step">';
                    str += '<div class="StepTitleBox">';
                    str += '<span class="StepTitle">步骤' + (step.stepNum + 1) + '</span>';
                    str += '<p class="StepBtn">';
                    str += ' </p>';
                    str += '</div>';
                    str += '<ul class="Actions">';
                    for (var a in step.actions) {
                        str += actionHtml(step.actions[a]);
                    }
                    str += '</ul>';
                    str += '</div>';
                    $(".StepList").append(str);
                }
                var selected = $(".Step").last().addClass("selected");
                CurriculumInfo.selectStep = page.getStep(page.steps.length - 1);
                if (ChapterEdit && CurriculumInfo.selectStep) {//如果是编辑状态,添加编辑按钮
                    selected.find(".StepBtn").html(StepBtnLoad(CurriculumInfo.selectStep));
                }
                BindStep();
            },
            error: function () {

            }
        })
    }


    //绑定步骤事件
    function BindStep() {
        //步骤选择
        $(".Step").off("click");
        $(".Step").on("click", function () {
            $(this).addClass("selected").siblings().removeClass("selected").find(".StepBtn").html("");
            CurriculumInfo.selectPage = new Page(CurriculumInfo.selectPage);
            CurriculumInfo.selectStep = CurriculumInfo.selectPage.getStep($(this).index());
            CurriculumInfo.selectPage.draw(CurriculumInfo.selectStep.stepNum);

            if (ChapterEdit) {//如果是编辑状态添加按钮
                $(this).find(".StepBtn").html(StepBtnLoad(CurriculumInfo.selectStep));
            }
            BindStep();
            return false;
        });
        //如果编辑状态,绑定事件
        if (ChapterEdit) {
            //上移步骤
            $(".stepMoveUp").off("click");
            $(".stepMoveUp").on("click", function () {
                var step = $(this).parents(".Step");
                var index = step.index();
                if (index > 0) {
                    CurriculumInfo.selectPage = new Page(CurriculumInfo.selectPage);
                    step.find(".StepTitle").text("步骤" + index);
                    $(".Step").eq(index - 1).before(step).find(".StepTitle").text("步骤" + (index + 1));
                    CurriculumInfo.selectPage.moveStep(index, index - 1);
                    CurriculumInfo.selectStep = CurriculumInfo.selectPage.getStep(index - 1);
                    CurriculumInfo.selectPage.draw(CurriculumInfo.selectStep.stepNum);
                    BindStep();
                }
                return false;
            })
            //下移步骤
            $(".stepMoveDown").off("click");
            $(".stepMoveDown").on("click", function () {
                var step = $(this).parents(".Step");
                var index = step.index();
                if (index < $(".Step").length - 1) {
                    CurriculumInfo.selectPage = new Page(CurriculumInfo.selectPage);
                    step.find(".StepTitle").text("步骤" + (index + 2));
                    $(".Step").eq(index + 1).after(step).find(".StepTitle").text("步骤" + (index + 1));
                    CurriculumInfo.selectPage.moveStep(index, index + 1);
                    CurriculumInfo.selectStep = CurriculumInfo.selectPage.getStep(index + 1);
                    CurriculumInfo.selectPage.draw(CurriculumInfo.selectStep.stepNum);
                    BindStep();
                }
                return false;
            })
            //创建动作
            $(".addAction").off("click");
            $(".addAction").on("click", function () {
                var step = $(this).parents(".Step");
                CurriculumInfo.selectAction = null;//清空选中的动作
                $("#ActionSet").attr("src", "/CurriculumSet/ActionOptions.html").show();
                return false;

            })
            //删除步骤
            $(".deleteStep").off("click");
            $(".deleteStep").on("click", function () {
                var step = $(this).parents(".Step");
                var index = step.index();
                var nexts = step.nextAll();
                step.remove();

                CurriculumInfo.selectPage = new Page(CurriculumInfo.selectPage);
                CurriculumInfo.selectPage.deleteStep(index);
                nexts.each(function (i, n) {
                    $(n).find(".StepTitle").text("步骤" + ($(n).index() + 1));
                });
                if (CurriculumInfo.selectPage.steps.length > 0) {//如果大于0 则更改数据,否则清空数据
                    if (index == CurriculumInfo.selectPage.steps.length) {//如果删除的是最后一个,则需要吧选中项前移一位
                        index--;
                    }
                    $(".Step").eq(index).addClass("selected").find(".StepBtn").html('<i class="stepMoveUp glyphicon glyphicon-arrow-up"></i><i class="stepMoveDown glyphicon glyphicon-arrow-down"></i><i class="addAction glyphicon glyphicon-plus-sign"></i><i class="deleteStep glyphicon glyphicon-remove-sign"></i>');
                    CurriculumInfo.selectStep = CurriculumInfo.selectPage.getStep(index);
                    CurriculumInfo.selectPage.draw(index);
                }
                else {
                    CurriculumInfo.selectStep = null;
                    CurriculumInfo.selectPage.draw(-1);//-1相当于清空
                }

                BindStep();
                return false;
            })
            //绑定动作事件
            BindAction();
        }
    }
    //绑定动作事件
    function BindAction() {
        $(".SetBtn").off("click");
        $(".SetBtn").on("click", function () {
            var thisAction = $(this).parents("li.Action");
            var type = thisAction.attr("type");
            CurriculumInfo.selectAction = CurriculumInfo.selectStep.getAction(thisAction.index());//设置选中的动作
            switch (type) {
                case "setTitle":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/SetTitle.html").show();
                    break;
                case "setBackground":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/SetBackground.html").show();
                    break;
                case "xiaoAiSay":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/XiaoAiSay.html").show();
                    break;
                case "xiaoAiChange":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/XiaoAiChange.html").show();
                    break;
                case "insertImg":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/InsertImg.html").show();
                    break;
                case "insertText":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/InsertText.html").show();
                    break;
                case "setWaitMillisecond":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/SetWaitMillisecond.html").show();
                    break;
                case "moveDom":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/MoveDom.html").show();
                    break;
                case "scaleDom":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/ScaleDom.html").show();
                    break;
                case "twinkleDom":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/TwinkleDom.html").show();
                    break;
                case "hideDom":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/HideDom.html").show();
                    break;
                case "studyAudio":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyAudio.html").show();
                    break;
                case "studyVideo":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyVideo.html").show();
                    break;
                case "studyArticle":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyArticle.html").show();
                    break;
                case "studyRecitation":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyRecitation.html").show();
                    break;
                case "studyFastReadEasy":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyFastReadEasy.html").show();
                    break;
                case "studyFastRead":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyFastRead.html").show();
                    break;
                case "studyAnnotation":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyAnnotation.html").show();
                    break;
                case "studyAnnotation2":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyAnnotation2.html").show();
                    break;
                case "studyJudgment":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyJudgment.html").show();
                    break;
                case "studyLinking":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyLinking.html").show();
                    break;
                case "studyOption":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyOption.html").show();
                    break;
                case "studyOptionFill":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyOptionFill.html").show();
                    break;
                case "studyFill":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyFill.html").show();
                    break;
                case "studySubjective":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudySubjective.html").show();
                    break;
                case "studyCalligraphy":
                    $("#ActionSet").attr("src", "/CurriculumSet/ActionSet/StudyCalligraphy.html").show();
                    break;
                default:
                    console.error("还未实现这个动作的编辑");
                    break;
            }
            CurriculumInfo.selectPage.isSave = false;
            return false;
        });

        $(".DeleteBtn").off("click");
        $(".DeleteBtn").on("click", function () {
            var thisAction = $(this).parents("li.Action");
            var thisStep = $(this).parents("div.Step");
            $("#" + thisAction.attr("id").split('_')[1]).remove();
            CurriculumInfo.selectStep.deleteAction(thisAction.index());
            thisAction.remove();
            thisStep.find(".StepBtn").html(StepBtnLoad(CurriculumInfo.selectStep));
            BindStep();
            CurriculumInfo.selectPage.draw(CurriculumInfo.selectStep.stepNum);
            return false;
        });
    }

    //浏览本页
    $("#SkimPage").click(function () {
        //ajax 调用 加入 radis中,返回key 调用学生端界面

        $("#LoadingBox").show();
        $("#LoadingText").text("正在生成浏览").show();

        if (CurriculumInfo.selectPage) {
            var d = JSON.stringify(CurriculumInfo.selectPage);
            console.log(d);
            $.ajax({
                url: "/Curriculum/ShowPage",
                type: "post",
                data: { pagestr: d },
                dataType: "json",
                success: function (data) {
                    if (!data.State) {
                        console.error("提交数据", "error:", data.ErrorCode, "message", data.ErrorMsg);
                    }
                    else {
                        CurriculumInfo.selectPage.isSave = true;
                        console.log("success", data.Data);
                        $("#LoadingText").text("正在跳转浏览...");
                        $("#LoadingBox").fadeOut(1000);
                        var iframe = document.getElementById("ShowPage");
                        iframe.onload = function () {
                            $("#LoadingText").hide();
                            $("#LoadingBox").hide();
                        };
                        iframe.src = data.Data;
                        // $("#ShowPage").attr("src", data.Data)
                        $(".showPageBox").show();
                    }

                },
                error: function (e) {
                    console.error(e);
                    $("#LoadingText").text("保存失败").fadeOut(1000);
                    $("#LoadingBox").fadeOut(1000);
                }
            })
        }
    });
    $("#closeBtn").click(function () {
        $(".showPageBox").hide();
        var iframe = document.getElementById("ShowPage");
        iframe.src = "";
    });
    //根据步骤数据判断是否有添加按钮
    function StepBtnLoad(step) {
        var actions = step.actions;
        var hasOnly = false;
        for (var i in actions) {
            var action = actions[i];
            switch (action.type) {
                case "studyAudio"://音频
                case "studyVideo"://视频
                case "studyArticle"://图文
                case "studyRecitation"://朗读
                case "studyFastReadEasy"://快速阅读-简单
                case "studyFastRead"://快速阅读
                case "studyAnnotation"://圈点批注
                case "studyAnnotation2"://圈点批注
                case "studyJudgment"://判断题
                case "studyLinking"://连线题
                case "studyOption"://选择题
                case "studyOptionFill"://选择填空
                case "studyFill"://填空题
                case "studySubjective"://主观题
                    hasOnly = true;
                    break;
            }
            if (hasOnly) {
                break;
            }
        }
        var htmlstr = '<i class="stepMoveUp glyphicon glyphicon-arrow-up"></i>';
        htmlstr += '<i class="stepMoveDown glyphicon glyphicon-arrow-down"></i>';
        if (!hasOnly) {
            htmlstr += '<i class="addAction glyphicon glyphicon-plus-sign"></i>';
        }
        htmlstr += '<i class="deleteStep glyphicon glyphicon-remove-sign"></i>';
        return htmlstr;
    }
}