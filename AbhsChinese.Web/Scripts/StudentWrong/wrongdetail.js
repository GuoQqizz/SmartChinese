
$(function () {
    renderHtml();
    loadData();
    registerEvent();
})

function renderHtml() {
    let index = wrongSubjectIds.indexOf(currentId);
    if (index == 0) {
        $('.prev-wrong').addClass('noprev');
    }
    $('#objkno').hide();
}
function loadData() {
    //subject-hard
    //knowledge-name
    //Yws_KnowledgeId = 1;
    //getdata(data1);

    $.ajax({
        url: detailDataUrl,
        type: 'POST',
        data: { wrongSubjectId: currentId },
        success: function (res) {
            //console.log(res);
            if (res && res.State && res.Data) {
                let hard = '';
                let knowledgeName = '';
                let report = res.Data.Report;
                let knowledge = res.Data.KnowledgeInfo;
                if (report) {
                    hard = report.DifficultyStr;
                    if (knowledge && knowledge != 'null') {
                        if (knowledge.Url != 'null') {
                            report.Topicexplanation = knowledge.Url;
                            report.MediaType = knowledge.Yme_MediaType;
                        }
                        if (knowledge.Ykl_Name != '') {
                            knowledgeName = knowledge.Ykl_Name;
                        }
                    }
                    //console.log(report);
                    //report.MediaType = 2;
                    //report.Topicexplanation = "/Images/audio.mp3";
                    getdata(report);
                }
                $('.subject-hard').html(hard);
                $('.knowledge-name').html(knowledgeName);
            }
        },
        //error: function (err) {
        //    console.error(err);
        //}
    })
}
function registerEvent() {
    //解析
    $('#objanl').click(function () {
        $('#step2').show();
        var _offsetTop = $('#step1').height();
        console.log(_offsetTop)
        $("#objectbox").animate({
            scrollTop: _offsetTop + 25
        }, 300);
    })
    //知识点
    $('#objkno').click(function () {
        $('#step3').show();
        var _offsetTop = $('#step1').height();
        var _offsetTop1 = $('#step2').height();
        console.log(_offsetTop)
        $("#objectbox").animate({
            scrollTop: _offsetTop + _offsetTop1 + 45
        }, 300);
    })
    //上一题
    $('.prev-wrong').on('click', function () {
        let index = wrongSubjectIds.indexOf(currentId);
        if (index > 0) {
            let id = wrongSubjectIds[index - 1];
            window.location.href = detailUrl + id;
        }

    });
    //下一题
    $('.next-wrong').on('click', function () {
        let index = wrongSubjectIds.indexOf(currentId);
        let toUrl = bookUrl;
        if (index < wrongSubjectIds.length - 1) {
            let id = wrongSubjectIds[index + 1];
            toUrl = detailUrl + id;
        } else {
            window.localStorage.setItem('topath', bookUrl);
        }
        window.location.href = toUrl;
    });
}

function again(wrongSubjectId, status) {
    //console.log(wrongSubjectId, status);
    window.location.href = clearWrongUrl + wrongSubjectId;
    //return false;
}

function getdata(data) {
    if (data.SubjectType) {
        switch (data.SubjectType) {
            case 1: //选择题
                choiceQuestion1(data)
                break;
            case 2: //判断题
                judge1(data)
                break;
            case 3: //填空题
                exercise1(data)
                break;
            case 4: //选择填空
                multipleChoice1(data);
                break;
            case 5: //连线题
                match1(data);
                break;
            case 6: //主观题
                essayquestion1(data)
                break;
            case 7: //圈点批注标色
                punctuationMark1(data)
                break;
            case 8: //圈点批注断句
                pausesReading1(data)
                break;
            default:
                console.error("类型错误");
        }
    }
    if (data.MediaType == 2) {
        $('audio').audioPlayer();
    }
}
function getStr(arg) {
    if (!arg || arg == 'null') {
        return '';
    } else {
        return arg;
    }
}

function renderKnowledge(topicexplanation, mediaType) {
    let res = '';
    if (knowledgeId && topicexplanation) {
        res += '<div class="step3" id="step3">';
        res += '<div class="objtitle">题目知识点讲解</div>';
        if (mediaType == 2) {//2 音频 3 视频
            res += '<div class="videoimg audiobox">';
            res += '<div id="wrapper">';
            res += '<audio preload="auto" controls>';
            res += '<source src="' + topicexplanation + '">';
            //res += '<source src="' + topicexplanation + '">';
            //res += '<source src="' + topicexplanation + '">';
            res += '</audio>';
            res += '</div>';
            res += '</div>';

        } else if (mediaType == 3) {
            res += '<div class="videoimg videibox">';
            res += '<video src="' + topicexplanation + '" controls="controls"></video>';
            res += '</div>';
        }
        res += '</div>';
        $('#objkno').show();
    } else {
        $('#objkno').hide()
    }
    return res;
}
//选择题
function choiceQuestion1(data) {
    var choiceStr = ['A', 'B', 'C', 'D', 'E', 'F'];
    if (!data.StemType && data.OptionType) {
        var html = '';
        html += '<div class="typechoicebox">';
        html += '<div class="titlebj">选择题</div>';
        html += '<div class="tsbox">';
        html += '<div class="fl" id="mactchboxtitle"></div>';
        html += '<div class="" id="mainobject"></div>';
        html += '</div>';
        html += '<div class="step2" id="step2">';
        html += '<div class="objtitle"><div class="fl">参考答案</div><div id="mainobject1"></div></div>';
        html += '<div class="mainobj mainobj1"></div>';
        html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
            '</div></div>';
        html += '</div>';
        html += renderKnowledge(data.Topicexplanation, data.MediaType);
        //if (Yws_KnowledgeId && data.Topicexplanation) {
        //    html += '<div class="step3" id="step3">';
        //    html += '<div class="objtitle">题目知识点讲解</div>';
        //    html += '<div class="videoimg">';
        //    html += '<video crossorigin="anonymous" src="' + data.Topicexplanation + '" controls="controls"></video>';
        //    html += '</div>';
        //    html += '</div>';
        //    $('#objkno').show();
        //} else {
        //    $('#objkno').hide()
        //}
        html += '</div>';
        $('#objectbox').html(html);
        var html1 = '';
        for (var i = 0; i < data.Answer.length; i++) {
            for (var ii = 0; ii < data.Options.length; ii++) {
                if (data.Options[ii].Key == data.Answer[i]) {
                    html1 += '<i>' + choiceStr[ii] + '</i>'
                }
            }
        }
        $('#mainobject1').html(html1);
        showQuestionTitle($('#mactchboxtitle'), data)
        showQuestion($('#mainobject'), data);
    } else {
        var html = '';
        html += '<div class="typechoicebox">';
        html += '<div class="step1" id="step1">';
        html += '<div class="titlebj">选择题</div>';
        if (data.StemType) {
            html += '<div class="objtitle1" id="mactchboxtitle"></div>';
        } else {
            html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
        }
        html += '<div class="mainobj mainobj2" id="mainobject"></div>';
        html += '</div>';
        html += '<div class="step2" id="step2">';
        html += '<div class="objtitle"><div class="fl">参考答案</div><div id="mainobject1"></div></div>';
        html += '<div class="mainobj mainobj1"></div>';
        html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
            '</div></div>';
        html += '</div>';
        html += renderKnowledge(data.Topicexplanation, data.MediaType);
        //if (Yws_KnowledgeId && data.Topicexplanation) {
        //    html += '<div class="step3" id="step3">';
        //    html += '<div class="objtitle">题目知识点讲解</div>';
        //    html += '<div class="videoimg">';
        //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
        //    html += '</div>';
        //    html += '</div>';
        //    $('#objkno').show();
        //} else {
        //    $('#objkno').hide()
        //}

        html += '</div>';
        $('#objectbox').html(html);
        var html1 = '';
        for (var i = 0; i < data.Answer.length; i++) {
            for (var ii = 0; ii < data.Options.length; ii++) {
                if (data.Options[ii].Key == data.Answer[i]) {
                    html1 += '<i>' + choiceStr[ii] + '</i>'
                }
            }
        }
        $('#mainobject1').html(html1);
        showQuestionTitle($('#mactchboxtitle'), data)
        showQuestion($('#mainobject'), data);
    }

}
//判断题
function judge1(data) {
    var html = '';
    html += '<div class="typejudge1">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">判断题</div>';
    if (data.StemType) {
        html += '<div class="objtitle1" id="mactchboxtitle"></div>';
    } else {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    }
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle">参考答案</div>';
    html += '<div class="mainobj mainobj1"  id="mainobject1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '</div>';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //    $('#objkno').show();
    //} else {
    //    $('#objkno').hide()
    //}
    html += '</div>';
    $('#objectbox').html(html);
    showQuestionTitle($('#mactchboxtitle'), data)
    showQuestion($('#mainobject'), data);
    var data1 = data;
    data1.StudentAnswer = data.Answer;
    showQuestion($('#mainobject1'), data1, true);
}
//填空
function exercise1(data) {
    var html = '';
    html += '<div class="typeexercise">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">填空题</div>';
    // if (data.StemType) {
    html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    // } else {
    //    html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    // }
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle"><div class="fl">参考答案</div><div id="mainobject1"></div></div>';
    html += '<div class="mainobj mainobj1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '</div>';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //    $('#objkno').show();
    //} else {
    //    $('#objkno').hide()
    //}
    html += '</div>';
    $('#objectbox').html(html);
    showQuestion($('#mactchboxtitle'), data);
    var html1 = '';
    for (var i = 0; i < data.Answer.Perfect.length; i++) {
        if (data.Answer.Perfect.length == i + 1) {
            html1 += '<i>' + data.Answer.Perfect[i] + '</i>';
        } else {
            html1 += '<i>' + data.Answer.Perfect[i] + '、</i>';
        }
    }
    $('#mainobject1').html(html1);
}
//选择填空
function multipleChoice1(data) {
    var html = '';
    html += '<div class="typemultipleChoice">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">选择填空</div>';
    if (data.StemType) {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    } else {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    }
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle"><div class="fl">参考答案</div><div id="mainobject1"></div></div>';
    html += '<div class="mainobj mainobj1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '</div>';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //    $('#objkno').show();
    //} else {
    //    $('#objkno').hide()
    //}
    html += '</div>';
    $('#objectbox').html(html);
    showQuestion($('#mactchboxtitle'), data);
    var data1 = data;
    data1.StudentAnswer = data.Answer;
    showQuestion($('#mainobject1'), data1);
}
//连线题
function match1(data) {
    var html = '';
    html += '<div class="typematchbox">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">连线题</div>';
    if (data.StemType) {
        html += '<div class="objtitle1" id="mactchboxtitle"></div>';
    } else {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    }
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle">参考答案</div>';
    html += '<div class="mainobj mainobj1"  id="mainobject1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '</div>';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //    $('#objkno').show();
    //} else {
    //    $('#objkno').hide()
    //}
    html += '</div>';
    $('#objectbox').html(html);
    showQuestionTitle($('#mactchboxtitle'), data)
    showQuestion($('#mainobject'), data);
    var data1 = data;
    data1.StudentAnswer = data.Answer;
    showQuestion($('#mainobject1'), data1);
}
//主观题
function essayquestion1(data) {
    var html = '';
    html += '<div class="typeessayquestion">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">主观题</div>';
    if (data.StemType) {
        html += '<div class="objtitle1" id="mactchboxtitle">' + data.Stem + '</div>';
    } else {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"><img src="' + data.Stem + '" /></div>';
    }
    html += '<div class="objtitle1 text"><div class="fl">您的作答</div><div class="questiontext">' + data.StudentAnswer + '</div></div>';
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle1 text"><div class="fl">参考答案</div><div class="questiontext">' + data.Answer + '</div></div>';
    html += '<div class="mainobj mainobj1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">评分标准</span><div class="abaltext">' + data.Standard +
        '</div></div>';
    html += '</div>';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //    $('#objkno').show();
    //} else {
    //    $('#objkno').hide()
    //}
    html += '</div>';
    $('#objectbox').html(html);
}
//圈点标注
function punctuationMark1(data) {
    var html = '';
    html += '<div class="typepunctuationMark">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">圈点批注标色</div>';
    if (data.StemType) {
        html += '<div class="objtitle1" id="mactchboxtitle"></div>';
    } else {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    }
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle"><div class="fl">参考答案</div><div id="mainobject1"></div></div>';
    html += '<div class="mainobj mainobj1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '</div>';
    html += '<div class="step3" id="step3">';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //}
    html += '</div>';
    $('#objectbox').html(html);
    showQuestionTitle($('#mactchboxtitle'), data);
    showQuestion($('#mainobject'), data);
    var data1 = data;
    data1.StudentAnswer = data.Answer;
    showQuestion($('#mainobject1'), data1);
}
//断句
function pausesReading1(data) {
    var html = '';
    html += '<div class="typepausesReading">';
    html += '<div class="step1" id="step1">';
    html += '<div class="titlebj">圈点批注断句</div>';
    if (data.StemType) {
        html += '<div class="objtitle1" id="mactchboxtitle"></div>';
    } else {
        html += '<div class="objtitle1 noimg" id="mactchboxtitle"></div>';
    }
    html += '<div class="mainobj mainobj2" id="mainobject"></div>';
    html += '</div>';
    html += '<div class="step2" id="step2">';
    html += '<div class="objtitle"><div class="fl">参考答案</div><div id="mainobject1"></div></div>';
    html += '<div class="mainobj mainobj1"></div>';
    html += '<div class="objtitle analysisbox"><span class="fl">题目解析</span><div class="abaltext">' + getStr(data.Analysis) +
        '</div></div>';
    html += '</div>';
    html += renderKnowledge(data.Topicexplanation, data.MediaType);
    //if (Yws_KnowledgeId && data.Topicexplanation) {
    //    html += '<div class="step3" id="step3">';
    //    html += '<div class="objtitle">题目知识点讲解</div>';
    //    html += '<div class="videoimg">';
    //    html += '<video src="' + data.Topicexplanation + '" controls="controls"></video>';
    //    html += '</div>';
    //    html += '</div>';
    //    $('#objkno').show();
    //} else {
    //    $('#objkno').hide()
    //}
    html += '</div>';
    $('#objectbox').html(html);
    showQuestionTitle($('#mactchboxtitle'), data);
    showQuestion($('#mainobject'), data);
    var data1 = data;
    data1.StudentAnswer = data.Answer;
    showQuestion($('#mainobject1'), data1);
}

function testData() {
    //选择题
    var data1 = {
        "Options": [{
            "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png",
            "Key": 0
        }, {
            "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png",
            "Key": 1
        }, {
            "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png",
            "Key": 2
        }, {
            "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png",
            "Key": 3
        }], //选择题
        "OptionType": 0, //选项类型1文字0图片
        "Stem": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png", //题干
        "StemType": 0, //题干类型图片或者文字类型1文字0图片
        "StudentAnswer": [0, 1, 3], //学生答案
        "Answer": [1, 2], //正确答案
        "Analysis": "<p>sfs</p>", //解析
        "SubjectType": 1, //题目类型                 1：选择题 2：判断题 3：填空题 4：选择填空 5：连线题 6：主观题 7：圈点批注标色 8：圈点批注断句
        "ResultStars": 2, //本题所得星
        "Difficulty": 1, //难度
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'
    }
    //判断题
    var data2 = {
        "Stem": "请做出正确的判断",
        "StemType": 1,
        "StudentAnswer": 0,
        "Answer": 0,
        "Analysis": "<p>sfsf</p>",
        "SubjectType": 2,
        "ResultStars": 5,
        "Difficulty": 2,
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'
    }
    //填空题
    var data3 = {
        "Stem": "<p>sf<em>sfssss</em>ssss{:}ss<strong>ss{:}sssssss{:}ssssss{:}</strong>{:}sf<em>sfssss</em>ssss{:}ss<strong>ss{:}sssssss{:}ssssss{:}</strong>{:}</p>",
        "StemType": 1,
        "StudentAnswer": [{
            "Indx": 0,
            "Text": '第一个空',
            "Scor": 1
        }, {
            "Indx": 1,
            "Text": "第二个空",
            "Scor": 0.8
        }, {
            "Indx": 4,
            "Text": "第三个空",
            "Scor": 0
        }],
        "Answer": {
            "Perfect": ["2", "tr", "ok", "不错", '真不多'],
            "Correct": ["21", "tr2", "ok2"],
            "Other": ["", "", ""],
        },
        "Analysis": '就五个空格',
        "SubjectType": 3,
        "ResultStars": 5,
        "Difficulty": 0,
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'
    }
    //选择填空
    var data4 = {
        "Options": [{
            "Text": "ni",
            "Key": 0
        }, {
            "Text": "hao",
            "Key": 1
        }, {
            "Text": "ma",
            "Key": 2
        }],
        "OptionType": 1,
        "Stem": "<p>11111{:}<em>1111111</em>{:}2<strong>2{:}3333333</strong></p>",
        "StemType": 1,
        "StudentAnswer": [
            [0, 0],
            [1, 1],
            [2, 2]
        ],
        "Answer": [
            [0, 1],
            [1, 0],
            [2, 2]
        ],
        "Analysis": "<p>11111111</p>",
        "SubjectType": 4,
        "ResultStars": 4,
        "Difficulty": 2,
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'

    }
    //连线题
    var data5 = {
        "LeftOptions": [{
            "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/cdd4b3c443b64728a611f202c826d449.png",
            "Key": 1
        },
            {
                "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png",
                "Key": 0
            },
            {
                "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/749feca4ab194bf18ec8aaa33d73159d.png",
                "Key": 2
            }
        ],
        "RightOptions": [{
            "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/79127f59f0a7442ab6dbeef4ba3828c7.png",
            "Key": 0
        },
            {
                "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/16e0f97ed21149b580a569dd56e43327.png",
                "Key": 1
            },
            {
                "Text": "https://abhstest.oss-cn-beijing.aliyuncs.com/QuestionDatabase/16e0f97ed21149b580a569dd56e43327.png",
                "Key": 2
            }
        ],
        "StudentAnswer": [
            [1, 1],
            [0, 0],
            [2, 2]
        ],
        "LeftOptionType": 0,
        "RightOptionType": 0,
        "SubjectId": 10155,
        "KnowledgeId": 10010,
        "Stem": "将汉字和拼音连到一",
        "StemType": 1,
        "Answer": [
            [1, 1],
            [0, 2],
            [2, 0]
        ],
        "Analysis": "<p>最强大脑</p>",
        "SubjectType": 5,
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'
    }
    //主观题
    var data6 = {
        "AnswerType": 0,
        "Standard": "爱上剪短发了就爱了书法家李会",
        "StandardType": 0,
        "Stem": "爱上剪短发了就爱了书法家李会",
        "StemType": 1,
        "StudentAnswer": "爱上剪短发了就爱了书法家李会计奥斯卡来得及看见奥斯卡据了解读会计奥斯卡来得及看见奥斯卡据了解读会计奥斯卡来得及看见奥斯卡据了解读书卡解放东路角色了",
        "Answer": "rytrtrtr",
        "Analysis": null,
        "SubjectType": 6,
        "ResultStars": 3,
        "Difficulty": 0
    }
    var data7 = {
        "Color": 0,
        "Content": "爱上剪短发了就爱了书法家李会计奥斯卡来得及看见奥斯卡据了解读会计奥斯卡来得及看见奥斯卡据了解读会计奥斯卡来得及看见奥斯卡据了解读书卡解放东路角色了",
        "OptionType": 0,
        "Stem": "圈出重点部分",
        "StemType": 1,
        "StudentAnswer": [
            [3, 2],
            [20, 4],
            [7, 3]
        ],
        "Answer": [
            [7, 3],
            [20, 5]
        ],
        "Analysis": '此题目易于得分',
        "SubjectType": 7,
        "ResultStars": 5,
        "Difficulty": 0,
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'
    }
    var data8 = {
        "Color": 0,
        "Content": "爱上剪短发了就爱了书法家李会计奥斯卡来得及看见奥斯卡据了解读会计奥斯卡来得及看见奥斯卡据了解读会计奥斯卡来得及看见奥斯卡据了解读书卡解放东路角色了",
        "OptionType": 0,
        "Stem": "/QuestionDatabase/817f072888b84a988ef75d08be221fc6.jpg",
        "StemType": 0,
        "StudentAnswer": [1, 3, 7, 9, 11],
        "Answer": [1, 6],
        "Analysis": null,
        "SubjectType": 8,
        "ResultStars": 2,
        "Difficulty": 0,
        "Topicexplanation": 'https://www.w3school.com.cn/i/movie.ogg'
    }
}
