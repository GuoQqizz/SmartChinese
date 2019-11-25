var pageIndex = 1;
var studyTaskId = $('#studyTaskId').val();


var allDataArr = [];
var submitData = { "id": studyTaskId, "answers": [], "useTime": 0, "type": 1 };
var startime, endTime;
var maxNum = 0;
var TotalRecord = 0;
$(function () {
    $(".bodycontent").show();
    startime = new Date().getTime();
    if (document.body.clientWidth < 1280) {
        var str = '<meta name="viewport" content="width=device-width,initial-scale=0.8,minimum-scale=0.8,maximum-scale=0.8,user-scalable=no" />';
        $("head").append(str);
    }
    $('.contentleft').height($('.contentright').height());
    $('.loginpage').height(document.documentElement.clientHeight);
    getDataToHtml();

    $(document).scroll(function () {
        var t = $(window).scrollTop();
        var H = $(".contentTitle").offset().top;
        if (t > H) {
            $(".textOrder").addClass("fixed");
            $("#hbox").show();
        }
        else {
            $(".textOrder").removeClass("fixed");
            $("#hbox").hide();
        }

    })

    $(".nextBtn").click(function () {
        var len = $(".contentCenter .contentCenterList").length;
        $(".preBtn").show();
        if (maxNum < TotalRecord) {
            getDataToHtml();
        } else {
            $(".contentCenter .contentCenterList").removeClass("active");
            $(".textOrder .allOrderList").removeClass("active");
            pageIndex++;
            if (pageIndex > len) {
                $(".nextBtn").hide();
                $(".submitBtn").show();
            } else if (pageIndex == len) {
                $(".preBtn").show();
                $(".submitBtn").hide();
            }
            $(".contentCenter .contentCenterList").eq(pageIndex - 2).addClass("active");
            $(".textOrder .allOrderList").eq(pageIndex - 2).addClass("active");
            $('html,body').animate({ scrollTop: 0 }, 500);
        }
    })
    $(".preBtn").click(function () {
        pageIndex--;
        $(".nextBtn").show();
        $(".contentCenter .contentCenterList").removeClass("active");
        $(".textOrder .allOrderList").removeClass("active");
        if (pageIndex > 2) {
            $(".submitBtn").hide();
        } else if (pageIndex <= 2) {
            $(".submitBtn").hide();
            $(".preBtn").hide();
        }
        $(".contentCenter .contentCenterList").eq(pageIndex - 2).addClass("active");
        $(".textOrder .allOrderList").eq(pageIndex - 2).addClass("active");
        $('html,body').animate({ scrollTop: 0 }, 500);
    })

    $(".submitBtn").click(function () {
        endTime = new Date().getTime();
        submitData.useTime = Math.ceil((endTime - startime) / 1000);
        $.each(allDataArr, function (i, obj) {
            switch (obj.SubjectType) {
                case 1:
                    getOptionAnswer(i + 1, obj)
                    break;
                case 2:
                    getJudgmentAnswer(i + 1, obj)
                    break;
                case 3:
                    getFillAnswer(i + 1, obj)
                    break;
                case 4:
                    getOptionFillAnswer(i + 1, obj)
                    break;
                case 5:
                    getLinkingAnswer(i + 1, obj)
                    break;
                case 6:
                    getSubjectiveAnswer(i + 1, obj)
                    break;
                case 7:
                    getAnnotationAnswer(i + 1, obj)
                    break;
                case 8:
                    getAnnotation2Answer(i + 1, obj)
                    break;
            }
        });
        //保存答题的结果
        savePraticeResult(submitData);
    })

})

//后台获取数据插入页面
function getDataToHtml() {
    $.ajax({
        type: "get",
        url: $('#urlToLoadSubjects').val(),
        data: {
            studyTaskId: studyTaskId,
            pageIndex: pageIndex
        },
        success: function (data) {
            if (data.State) {
                $(".contentCenterList").removeClass("active");
                //$(".textName").html("七年级上作文培优课程-第2课时课后任务 （共" + data.TotalRecord + "题）");
                $('#span-subjectCount').text(data.TotalRecord);
                TotalRecord = data.TotalRecord;
                var data = data.Data;
                allDataArr = allDataArr.concat(data);
                var str = "<div class='allOrderList active clearfloat'>"
                var newData = [];
                $.each(data, function (i, obj) {
                    obj.id = generateId(pageIndex, i);
                    str += "<div class='orderList fl' id='orderList_" + obj.id + "'>" + obj.id + "</div>"
                    if (i == data.length - 1) {
                        maxNum = obj.id;
                    }
                    if (obj.SubjectType == 3) {
                        var arr = obj.Stem.split("{:}");
                        obj.StemArr = arr;
                        var lenArr = [];
                        $.each(arr, function (j, res) {
                            if (j == arr.length - 1) {
                                lenArr.push(0);
                            } else {
                                var len = obj.Answer.Perfect[j].split("|")[0].length;
                                if (len <= 5) {
                                    len = 5;
                                } else {
                                    len = Math.ceil(len / 5) * 5;
                                }
                                lenArr.push(24 * len);
                            }
                        });
                        obj.lenArr = lenArr;
                    } else if (obj.SubjectType == 4) {
                        var arr = obj.Stem.split("{:}");
                        obj.StemArr = arr;
                    }
                    newData.push(obj);
                });
                str += "</div>";
                $(".textOrder .allOrderList").removeClass("active");
                $(".textOrder").append(str);
                var datas = {
                    list: newData,
                    TotalRecord: TotalRecord
                };
                var html = template('ScriptDiv', datas);
                var contentCenterList = document.createElement("div");
                contentCenterList.classList.add("contentCenterList");
                contentCenterList.classList.add("active");
                contentCenterList.innerHTML = html;
                $(".contentCenter").append(contentCenterList);
                $('html,body').animate({
                    scrollTop: 0
                }, 500);
                pageIndex++;
                if (maxNum < TotalRecord) {
                    $(".nextBtn").show();
                } else {
                    $(".nextBtn").hide();
                    $(".submitBtn").show();
                }
                $(".textOrder .orderList").click(function () {
                    var num = $(this).html();
                    $('html,body').animate({
                        scrollTop: $("#contentList_" + num).offset().top - 80
                    }, 500);
                })
                renderSubject(contentCenterList);
            }
        }
    });
}

////////////////////////////////////错题消除和课后练习


function renderSubject(contentCenterList) {
    //判断题
    $(contentCenterList).find(".judgmentOption").each(function () {
        doJudgment($(this))
    })
    //选择题
    $(contentCenterList).find(".allOption").each(function () {
        doOption($(this))
    })
    //填空题
    $(contentCenterList).find(".fillStem").each(function () {
        doFill($(this))
    })
    //选择填空题
    $(contentCenterList).find(".optionFillOptionList").each(function () {
        doOptionFill($(this))
    })
    //连线题

    $(contentCenterList).find(".linkingPart").each(function () {
        doLinking($(this))
    })
    //主观题
    $(contentCenterList).find(".subjectiveTextarea").each(function () {
        doSubjective($(this))
    })
    //圈点批注-标色
    $(contentCenterList).find(".AnnotationContents").each(function () {
        doAnnotation($(this))
    })
    //圈点批注-断句
    $(contentCenterList).find(".Annotation2Contents").each(function () {
        doAnnotation2($(this))
    })
}
//判断题答题
function doJudgment(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var isChoose = false;
    var commonId = parentId.split("_")[1];
    parentDom.find(".judgmentOptions").click(function () {
        $("#orderList_" + commonId).addClass("active");
        isChoose = true;
        $(this).addClass('active').siblings().removeClass('active');
    })
}
//选择题答题
function doOption(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    var thisData;
    $.each(allDataArr, function (i, val) {
        if (val.id == commonId) {
            thisData = val;
        }
    });
    var myanswer = [];
    parentDom.find(".OptionList").click(function () {
        $("#orderList_" + commonId).addClass("active");
        if (thisData.Answer.length > 1) {
            if ($(this).hasClass('active')) {
                $(this).removeClass('active');
                var index = myanswer.indexOf(Number($(this).attr("optionId")));
                if (index > -1) {
                    myanswer.splice(index, 1);
                }
            } else {
                $(this).addClass('active');
                myanswer.push(Number($(this).attr("optionId")));
            }
            if (myanswer.length == 0) {
                $("#orderList_" + commonId).removeClass("active");
            }
        } else {
            if (!$(this).hasClass('active')) {
                myanswer = [];
                parentDom.find(".OptionList").removeClass("active");
                $(this).addClass('active');
                myanswer.push(Number($(this).attr("optionId")));
            }
        }
    })
}
function doFill(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    var myanswer = [];
    parentDom.find(".fillStem input").each(function (i, val) {
        myanswer[i] = '';
        $(this).on("input propertychange", function () {
            $("#orderList_" + commonId).addClass("active");
            if ($(this).val().trim() !== "") {
                var myVal = $(this).val().trim();
                myanswer[i] = myVal;
            } else {
                myanswer[i] = "";
            }
            var isNull = true;
            //$.each(myanswer, function (i, val) {
            //    if (val !== "") {
            //        isNull = false;
            //    }
            //})
            isNull = myanswer.every(a=>a === '');
            if (isNull) {
                $("#orderList_" + commonId).removeClass("active");
            }
        });
    })
}
//选择填空答题
function doOptionFill(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    var _copy = null;
    parentDom.find(".FillOptionList").on("mousedown touchstart", function (e) {
        e.preventDefault();
        if ($(this).css("opacity") == 0) {
            return
        }
        var touchs;
        if (event.touches) {
            touchs = event.touches[0];
        } else {
            touchs = event;
        }
        var x = touchs.pageX;
        var y = touchs.pageY;
        //复制自己并加入定位,去除外边距
        _copy = $(this).clone().css({ "position": "absolute", "zIndex": "999", "margin": "0px" });
        _copy.attr("stypeId", "stypeId" + commonId);
        //添加到 body中
        $("body").append(_copy);
        //计算x,y值
        var _left = x - _copy.width() / 2;
        var _top = y - _copy.height() / 2;
        //定位copy;
        _copy.css({ "top": _top + "px", "left": _left + "px" });
        //自己的元素变成透明
        $(this).css({ "opacity": 0 });
    })
    $(window).on("mousemove touchmove", function (e) {
        e.preventDefault();
        if (_copy) {
            var touchs;
            if (event.touches) {
                touchs = event.touches[0];
            } else {
                touchs = event;
            }
            var x = touchs.pageX;
            var y = touchs.pageY;
            //计算x,y值
            var _left = x - _copy.width() / 2;
            var _top = y - _copy.height() / 2;
            //定位copy;
            _copy.css({ "top": _top + "px", "left": _left + "px" });
        }
    })
    $(window).on("mouseup touchend", function (e) {
        if (_copy) {
            e.preventDefault();
            var touchs;
            if (event.touches) {
                touchs = event.changedTouches[0];
            } else {
                touchs = event;
            }
            var x = touchs.pageX;
            var y = touchs.pageY;
            var inDom = null;
            parentDom.find('.OptionFillStemList').each(function (j, dom) {
                var $this = $(this)[0];
                //每个题干选项的左上角位置
                var x1 = getElementPageLeft($this);
                var y1 = getElementPageTop($this);
                //每个题干选项的右下角位置
                var x2 = x1 + $(this).width();
                var y2 = y1 + $(this).height();
                //判断拖拽元素中心坐标是否在题干选项范围之内
                if ((x1 <= x && x <= x2) && (y1 <= y && y <= y2)) {
                    $("#orderList_" + commonId).addClass("active");
                    inDom = $(this);
                }
            });
            if (inDom) {
                _that = _copy;
                //如果题干选项内容为空，即没有拖拽的选项插入进去
                if (inDom.children().length > 0) {
                    var oldOptionId = inDom.find(".newfillBox").attr("optionId");
                    parentDom.find(".FillOptionList[optionId='" + oldOptionId + "']").css({ "opacity": "1" });
                }
                //获取拖拽选项的基本信息
                var optionId = _that.attr("optionId");
                var texts = _that.html();
                //重新生成一个元素其信息与拖拽元素的信息相同插入题干选项内
                var creatStr = "<div optionId=" + optionId + " class='newfillBox'>" + texts + "</div>";
                inDom.html(creatStr);
                inDom.css("top", 0);
            } else {
                var optionId = _copy.attr("optionId");
                parentDom.find(".FillOptionList[optionId='" + optionId + "']").css({ "opacity": "1" });
            }
            _copy.remove();
            _copy = null;
        }
    });

}
//连线题答题
function doLinking(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    parentDom.find(".linkingPart").onLine({
        parentDom: parentDom,
        commonId: commonId
    });
}
//主观题答题
function doSubjective(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    parentDom.find(".subjectiveTextarea").on("input propertychange", function () {
        if ($(this).val().trim() !== "") {
            $("#orderList_" + commonId).addClass("active");
        } else {
            $("#orderList_" + commonId).removeClass("active");
        }
    });
}
//圈点批注-标色答题
function doAnnotation(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    var mousedown = false, //是否点击过程中
		_start = 0, //开始位置下标
		_end = 0, //结束位置下标
		_left = 0, //此次操作最左侧位置下标
		_right = 0; //此次操作最右侧位置下标
    var steplist = [];
    var steps = [];

    function drow() {
        var start = _start;
        var end = _end;
        if (start > end) {
            var i = end;
            end = start;
            start = i;
        }
        var indexdoms = dom.children();
        indexdoms.removeClass('specialText');
        //遍历去除所有的曾经选中过的元素(再次选中的元素)
        $(".reselected").each(function () {
            var _index = $(this).index();//获取自己的下标
            if (_left <= _index && _index <= _right) {//如果在最左侧和最右侧之间
                for (var o = 0; o < steplist.length; o++) {//遍历选中的元素
                    if (steplist[o] == _index) {//如果选中的元素与数组中的一致
                        steplist.splice(o, 1);//删除元素
                        break;//退出循环
                    }
                }
            }
        });
        $(".reselected").removeClass('reselected');

        for (var i in steplist) {
            indexdoms.eq(steplist[i]).addClass('specialText');
        } //遍历其他步骤中选择到的数据
        while (start <= end) {
            var thisDom = indexdoms.eq(start++);
            if (thisDom.hasClass('specialText')) {
                thisDom.addClass('reselected');
            } else {
                thisDom.addClass('specialText');
            }
        }; //遍历所有开始位置到结束位置的元素
    }
    dom.on('mousedown touchstart', '.normalText', function (e) {
        mousedown = true;
        var _index = $(this).index();
        if ($(this).hasClass('specialText')) {
            _index = getInArrFirst(splitArray(steplist), _index)[0];
        }
        _start = _end = _left = _right = _index;
        $(this).addClass('specialText');
    });
    dom.on('mousemove touchmove', '.normalText', function (e) {
        var _index = $(this).index();
        if (mousedown && _end != _index) {
            if (_index > _right) { //如果位置大于最右侧位置,填充最右侧位置
                _right = _index;
            } else if (_index < _left) { //如果位置小于最左侧位置,填充最左侧位置
                _left = _index;
            }
            _end = _index;
            drow();
        }
    });

    $(window).on('mouseup touchend', function (e) {
        if (mousedown) {
            mousedown = false;
            steplist = [];
            dom.find(".specialText").each(function () {
                steplist.push($(this).index());
            });
            if (steps.length > 0) { //如果不是第一次操作
                var last = steps[steps.length - 1]; //获取上一次的数据
                var same = true; //假设与上次操作相同
                if (same = last.length == steplist.length) { //如果长度相同
                    for (var i in last) {
                        if (last[i] != steplist[i]) {
                            same = false;
                            break;
                        }
                    }
                }
                if (same) {
                    return;
                }
            }
            steps.push(steplist.concat([]));
            if (steps.length>0) {
                $("#orderList_" + commonId).addClass("active");
            }
        }
    });
    parentDom.find(".revokeBtn").click(function () {
        if (steps.length > 0) {
            var del = steps.pop();
            var indexdoms = dom.children();
            indexdoms.removeClass("specialText reselected");
            if (steps.length > 0) {
                steplist = steps[steps.length - 1];
                for (var i in steplist) {
                    indexdoms.eq(steplist[i]).addClass("specialText");
                }
            } else {
                steplist = [];
                $("#orderList_" + commonId).removeClass("active");
            }
        }
    });
}

//圈点批注-断句答题
function doAnnotation2(dom) {
    var parentId = dom.parents(".contentList").attr("id");
    var parentDom = $("#" + parentId);
    var commonId = parentId.split("_")[1];
    var myAnswer = [];
    parentDom.find(".Annotation2Contents .Annotation2NormalText").click(function () {
        if (!$(this).hasClass("truncation")) {
            $(this).addClass("truncation");
            var index = $(this).index();
            myAnswer.push(index);
            $("#orderList_" + commonId).addClass("active");
        }
    });

    parentDom.find(".revokeBtn").click(function () {
        if (myAnswer.length > 0) {
            var num = myAnswer[myAnswer.length - 1];
            parentDom.find(".Annotation2Contents .Annotation2NormalText").eq(num).removeClass("truncation");
            myAnswer.pop(myAnswer.length - 1);
            if (myAnswer.length == 0) {
                $("#orderList_" + commonId).removeClass("active");
            }
        }
    })
}

function getListPair(box) {
    var index;
    var leftVal, rightVal, nowVal;
    var pair = parseInt(box.attr("pair"));
    var sum = pair * 2;
    var pairA = new Array();
    var rightPart1 = box.find(".showleft");
    var rightPart2 = box.find(".showright");
    var size = rightPart1.find(".showitem").length;
    for (var i = 0; i < size; i++) {
        if (rightPart1.find(".showitem").eq(i).hasClass("addstyle") == true) {
            nowVal = rightPart1.find(".showitem").eq(i).attr("pair");
            leftVal = parseInt(nowVal) * 2;
            pairA[leftVal] = i;
        }
    }
    for (var i = 0; i < size; i++) {
        if (rightPart2.find(".showitem").eq(i).hasClass("addstyle") == true) {
            nowVal = rightPart2.find(".showitem").eq(i).attr("pair");
            rightVal = parseInt(nowVal) * 2 + 1;
            pairA[rightVal] = i;
        }
    }
    var idStr = [];
    var idAttr = [];
    for (var i = 0; i < sum; i++) {
        if (typeof pairA[i] != "undefined") {
            if (i % 2 == 0) {
                idStr.push(Number(rightPart1.find(".showitem").eq(pairA[i]).attr("thisId")));
            }
            else {
                idStr = idStr.concat([Number((rightPart2.find(".showitem").eq(pairA[i]).attr("thisId")))]);
                idAttr.push(idStr);
                idStr = [];
            }
        }
    }
    return idAttr;
}

//获取判断题答案
function getJudgmentAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var myanswer = parentDom.find('.judgmentOptions.active').index();
    var answer = data.Answer;
    var allCount = 0;
    if (myanswer > -1) {
        if (myanswer == data.Answer) {
            allCount = 5;
        } else {
            allCount = 1;
        }
    } else {
        allCount = 0;
    }
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点ID
    thisSubjectAnswerData.sanw = myanswer;//答案
    submitData.answers.push(thisSubjectAnswerData);
}
//获取选择题答案
function getOptionAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var sops = [];
    $.each(data.Options, function (i, obj) {
        sops.push(obj.Key);
    })
    var myanswer = [];
    parentDom.find(".OptionList.active").each(function () {
        myanswer.push(Number($(this).attr("optionId")));
    })
    myanswer.sort(sortNumber);
    var answer = data.Answer.sort(sortNumber);
    if (myanswer.length > 0) {
        if (myanswer.toString() == answer.toString()) {
            allCount = 5;
        } else {
            allCount = 1;
        }
    } else {
        allCount = 0;
    }
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myanswer;//答案
    thisSubjectAnswerData.sops = sops;//选项顺序
    submitData.answers.push(thisSubjectAnswerData);
}
//获取填空题答案
function getFillAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var myanswer = [];
    var allCount = 0;
    var count = 0;
    var trueAnswer = data.Answer;
    parentDom.find('.fillStem input').each(function (i, val) {
        if ($(this).val().trim() !== "") {
            var myVal = $(this).val().trim();
            var jsonDom = {};
            jsonDom.Indx = i;
            jsonDom.Text = myVal;
            if (trueAnswer.Perfect[i].trim().indexOf(myVal) != -1) {
                count += 1;
                jsonDom.Scor = 1;
            } else if (trueAnswer.Correct[i] && trueAnswer.Correct[i].trim().indexOf(myVal) != -1) {
                count += 0.8;
                jsonDom.Scor = 0.8;
            } else if (trueAnswer.Other[i] && trueAnswer.Other[i].trim().indexOf(myVal) != -1) {
                count += 0.6;
                jsonDom.Scor = 0.6;
            } else {
                count += 0;
                jsonDom.Scor = 0;
            }
            myanswer.push(jsonDom)
        }
    });
    allCount = Math.round(5 * count / trueAnswer.Perfect.length);
    if (allCount == 0) {
        allCount = 1;
    }
    if (myanswer.length == 0) {
        allCount = 0;
    }
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myanswer;//答案
    submitData.answers.push(thisSubjectAnswerData);
}
//获取选择填空题答案
function getOptionFillAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var sops = [];
    $.each(data.Options, function (i, obj) {
        sops.push(obj.Key);
    })
    var myanswer = [];
    var hasNewDom = parentDom.find(".OptionFillStemList .newfillBox");
    if (hasNewDom.length > 0) {
        hasNewDom.each(function (i, thisDom) {
            var arr = [];
            arr.push(i);
            arr.push(Number($(this).attr("optionId")));
            myanswer.push(arr);
        })
    }
    var num = 0;
    $.each(data.Answer, function (i, Answer) {
        $.each(myanswer, function (j, val) {
            if (myanswer[j][0] == data.Answer[i][0] && myanswer[j][1]== data.Answer[i][1]) {
                num++;
            }
        })
    })
    var allCount = Math.round(5 * (num / data.Answer.length));
    if (allCount == 0) {
        allCount = 1;
    }
    if (myanswer.length == 0) {
        allCount = 0;
    }
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myanswer;//答案
    thisSubjectAnswerData.sops = sops;//选项顺序
    console.log(thisSubjectAnswerData)
    submitData.answers.push(thisSubjectAnswerData);
}
//获取连线题答案
function getLinkingAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var myAnwser = getListPair(parentDom.find(".linkingPart"));
    var slos = [];
    var sros = [];
    for (var s in data.LeftOptions) {
        slos.push(data.LeftOptions[s].Key)
    }
    for (var m in data.RightOptions) {
        sros.push(data.RightOptions[m].Key)
    }
    var allCount = 0;
    var num = 0;
    if (myAnwser.length == 0) {
        allCount = 0;
    } else {
        for (var i = 0; i < myAnwser.length; i++) {
            var str1 = JSON.stringify(myAnwser[i]);
            var str2 = JSON.stringify(data.Answer);
            if (str2.indexOf(str1) > -1) {
                num++
            }
        }
        allCount = Math.round(5 * num / data.Answer.length);
        if (allCount == 0) {
            allCount = 1;
        }
    }
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myAnwser;//答案
    thisSubjectAnswerData.slos = slos;//左边顺序
    thisSubjectAnswerData.sros = sros;//左边顺序
    submitData.answers.push(thisSubjectAnswerData);
}
//获取主观题答案
function getSubjectiveAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var myanswer = parentDom.find(".subjectiveTextarea").val();
    var allCount = 0;
    if (myanswer !== "") {
        allCount = 5;
    } else {
        allCount = 0;
    }
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myanswer;//答案
    submitData.answers.push(thisSubjectAnswerData);
}
//获取圈点批注-标色答案
function getAnnotationAnswer(num, data) {
    var parentDom = $("#contentList_" + num);
    var myanswer = [];
    var allCount = 0;
    var specialLen = parentDom.find(".specialText").length;
    if (specialLen>0) {
        parentDom.find(".specialText").each(function () {
            myanswer.push($(this).index());
        });
        var myanswer = splitArray(myanswer);
        for (var i in myanswer) {
            var a = myanswer[i];
            myanswer[i] = [a[0], a.length];
        }
    }
    if (myanswer.length > 0) {
        for (var i = 0; i < data.Answer.length; i++) {
            for (var j = 0; j < myanswer.length; j++) {
                if (myanswer[j][0] == data.Answer[i][0] && myanswer[j][1] == data.Answer[i][1]) {
                    allCount++;
                }
            }
        }
        if (myanswer.length > data.Answer.length) {
            allCount -= (0.5 * (myanswer.length - data.Answer.length))
        }
        allCount = Math.round(5 * (allCount / data.Answer.length));
        if (allCount <= 0) {
            allCount = 1;
        }
    } else {
        allCount = 0;
    }
    console.log(myanswer)
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myanswer;//答案
    submitData.answers.push(thisSubjectAnswerData);
}

//获取圈点批注-断句答案
function getAnnotation2Answer(num, data) {
    var parentDom = $("#contentList_" + num);
    var myanswer = [];
    var chooseDom = parentDom.find(".Annotation2NormalText");
    chooseDom.each(function (i, val) {
        if ($(this).hasClass("truncation")) {
            myanswer.push(i);
        }
    });
    var allCount = 0;
    if (myanswer.length > 0) {
        for (var i = 0; i < data.Answer.length; i++) {
            for (var j = 0; j < myanswer.length; j++) {
                if (myanswer[j] == data.Answer[i]) {
                    allCount++;
                }
            }
        }
        if (myanswer.length > data.Answer.length) {
            allCount -= (0.5 * (myanswer.length - data.Answer.length))
        }
        allCount = Math.round(5 * (allCount / data.Answer.length));
        if (allCount <= 0) {
            allCount = 1;
        }
    } else {
        allCount = 0;
    }
    //把答案放进整体数组里
    var thisSubjectAnswerData = {};
    thisSubjectAnswerData.type = data.SubjectType;//题目类型 
    thisSubjectAnswerData.sbId = data.SubjectId;//题目id
    thisSubjectAnswerData.rtar = allCount;//获得星数
    thisSubjectAnswerData.scon = 0;//题目金币数(非第一次学习,则为0)
    thisSubjectAnswerData.rcon = 0;//得到金币数
    thisSubjectAnswerData.kdge = data.KnowledgeId;//知识点id
    thisSubjectAnswerData.sanw = myanswer;//答案
    submitData.answers.push(thisSubjectAnswerData);
}

//数组去重
function unique(arr) {
    if (!Array.isArray(arr)) {

        return;
    }
    arr = arr.sort()
    var arrry = [arr[0]];
    for (var i = 1; i < arr.length; i++) {
        if (arr[i] !== arr[i - 1]) {
            arrry.push(arr[i]);
        }
    }
    return arrry;
}
//将数组进行正序排列,并返回一个二维数组,二维数组中的每个数组中的值都是连续的
function splitArray(arr) {
    arr = arr.sort((a, b) => a - b);
    var arrs = [];
    if (arr.length > 0) {
        var _start = arr[0];
        var _arr = [];
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == _start++) {
                _arr.push(arr[i]);
            } else {
                arrs.push(_arr);
                _arr = [arr[i]];
                _start = arr[i] + 1;
            }
        }
        arrs.push(_arr);
    }
    return arrs;
}
//上个方法的辅助方法,获取数组中包含次数字的数组
function getInArrFirst(arrs, index) {
    for (var i in arrs) {
        var arr = arrs[i];
        for (var a in arr) {
            if (arr[a] == index) {
                return arr;
            }
        }
    }
}
//获取元素距离页面左侧位置
function getElementPageLeft(element) {
    var actualLeft = element.offsetLeft;
    var parent = element.offsetParent;
    while (parent != null) {
        actualLeft += parent.offsetLeft + (parent.offsetWidth - parent.clientWidth) / 2;
        parent = parent.offsetParent;
    }
    return actualLeft;
}
//获取元素距离页面顶部位置
function getElementPageTop(element) {
    var actualTop = element.offsetTop;
    var parent = element.offsetParent;
    while (parent != null) {
        actualTop += parent.offsetTop + (parent.offsetHeight - parent.clientHeight) / 2;
        parent = parent.offsetParent;
    }
    return actualTop;
}
//数组排序
function sortNumber(a, b) {
    return a - b
}
//给数据添加ID
function generateId(pageIndex, index) {
    return parseInt((pageIndex - 1) * 10) + parseInt((index + 1));
}
//判断是不是PC端
function IsPC() {
    var userAgentInfo = navigator.userAgent;
    var Agents = ["Android", "iPhone",
                "SymbianOS", "Windows Phone",
                "iPad", "iPod"];
    var flag = true;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
        }
    }
    return flag;
}
//将数组连续的几个进行组合，保留第一个和新数组长度
function formatArr(Arr) {
    var stepsArr = Arr;
    var newArr = [];
    var formatArr = [];
    for (var i = 0; i < stepsArr.length; i++) {
        newArr = newArr.concat(stepsArr[i])
    }
    newArr.sort(sortNumber);
    newArr = splitArr(newArr);

    for (var j = 0; j < newArr.length; j++) {
        var arr = [];
        arr.push(newArr[j][0]);
        arr.push(newArr[j].length);
        formatArr.push(arr);
    }
    return formatArr;
}
