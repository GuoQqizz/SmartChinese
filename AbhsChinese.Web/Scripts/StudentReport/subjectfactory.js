function factory(subject) {
    switch (subject.SubjectType) {
        case 1://选择题
            chooseQues(subject);
            break;
        case 2://判断题
            judgeQues(subject);
            break;
        case 3://填空题
            exerciseQues(subject);
            break;
        case 4://选择填空
            multipleQues(subject);
            break;
        case 5://连线题
            matchQues(subject);
            break;
        case 6://主观题
            subjQues(subject);
            break;
        case 7://圈点批注标色
            markQues(subject);
            break;
        case 8://圈点批注断句
            mark2Ques(subject);
            break;
        default:
            break;
    }
}

//选择题
function chooseQues(subject) {
    var choiceStr = ['A', 'B', 'C', 'D', 'E', 'F'];
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    var score = "";
    var strHtml = "";
    if (subject.StemType == 0 && subject.OptionType == 1) {
        //题干是图片，选项是文字
        strHtml += `<div class="practsummary"><div class="choicequestion">`;
        strHtml += `<div class="choicebox choicehasimg" style="padding-top: 20px;overflow: hidden;">`
        strHtml += `<div class="choiceimg fl"><div class="questiontitlebox" id="titlebox${subject.SubjectId}"></div></div>`
        strHtml += `<div class="choicect fl" id="subbox${subject.SubjectId}"></div>`
        strHtml += `</div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ans${subject.SubjectId}"></div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
        strHtml += `</div></div>`;
        $(".practsummarybox").append(strHtml);

        var html1 = '';
        for (var i = 0; i < subject.Answer.length; i++) {
            for (var ii = 0; ii < subject.Options.length; ii++) {
                if (subject.Options[ii].Key == subject.Answer[i]) {
                    html1 += '<i>' + choiceStr[ii] + '</i>'
                }
            }
        }
        $(`#ans${subject.SubjectId}`).html(html1);

        showQuestionTitle($(`#titlebox${subject.SubjectId}`), subject);
        showQuestion($(`#subbox${subject.SubjectId}`), subject);

    }
    else if (subject.StemType == 1 && subject.OptionType == 0) {
        //题干是文字，选项是图片
        strHtml += `<div class="practsummary"><div class="choicequestion">`;
        strHtml += `<div class="questiontitlebox"><div class="questiontitle">${subject.Stem}</div></div>`
        strHtml += `<div class="choiceboximg" id="subbox${subject.SubjectId}"></div>`
        strHtml += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ans${subject.SubjectId}"></div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
        strHtml += `</div></div>`;
        $(".practsummarybox").append(strHtml);

        var html1 = '';
        for (var i = 0; i < subject.Answer.length; i++) {
            for (var ii = 0; ii < subject.Options.length; ii++) {
                if (subject.Options[ii].Key == subject.Answer[i]) {
                    html1 += '<i>' + choiceStr[ii] + '</i>'
                }
            }
        }
        $(`#ans${subject.SubjectId}`).html(html1);
        showQuestion($(`#subbox${subject.SubjectId}`), subject);
    }
    else if (subject.StemType == 0 && subject.OptionType == 0) {
        //题干是图片并且选项也是图片
        strHtml += `<div class="practsummary"><div class="choicequestion">`;
        strHtml += `<div class="questiontitlebox" id="titlebox${subject.SubjectId}"></div>`
        strHtml += `<div class="choiceboximg" id="subbox${subject.SubjectId}"></div>`
        strHtml += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ans${subject.SubjectId}"></div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
        strHtml += `</div></div>`;

        $(".practsummarybox").append(strHtml);

        var html1 = '';
        for (var i = 0; i < subject.Answer.length; i++) {
            for (var ii = 0; ii < subject.Options.length; ii++) {
                if (subject.Options[ii].Key == subject.Answer[i]) {
                    html1 += '<i>' + choiceStr[ii] + '</i>'
                }
            }
        }
        $(`#ans${subject.SubjectId}`).html(html1);
        showQuestionTitle($(`#titlebox${subject.SubjectId}`), subject);
        showQuestion($(`#subbox${subject.SubjectId}`), subject);
    }
    else if (subject.StemType == 1 && subject.OptionType == 1) {
        //题干和选项都是文字
        strHtml += `<div class="practsummary"><div class="choicequestion">`;
        strHtml += `<div class="questiontitlebox"><div class="questiontitle">${subject.Stem}</div></div>`
        strHtml += `<div class="choicebox" id="subbox${subject.SubjectId}"></div>`
        strHtml += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ans${subject.SubjectId}"></div></div>`;
        strHtml += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
        strHtml += `</div></div>`;
        $(".practsummarybox").append(strHtml);

        var html1 = '';
        for (var i = 0; i < subject.Answer.length; i++) {
            for (var ii = 0; ii < subject.Options.length; ii++) {
                if (subject.Options[ii].Key == subject.Answer[i]) {
                    html1 += '<i>' + choiceStr[ii] + '</i>'
                }
            }
        }
        $(`#ans${subject.SubjectId}`).html(html1);
        showQuestion($(`#subbox${subject.SubjectId}`), subject);
    }
}

//判断题
function judgeQues(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    var strHtml = "";
    strHtml += `<div class="practsummary"><div class="choicequestion">`;
    strHtml += `<div id="titlebox${subject.SubjectId}" class="questiontitlebox"></div>`;
    strHtml += `<div id="subbox${subject.SubjectId}" class="questjudge"></div>`;
    strHtml += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ansbox${subject.SubjectId}"></div></div>`;
    strHtml += `<div id="anabox${subject.SubjectId}" class="questiontitle2 refanswer"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
    strHtml += `</div></div>`;
    $(".practsummarybox").append(strHtml);
    //题干
    showQuestionTitle($(`#titlebox${subject.SubjectId}`), subject);
    showQuestion($(`#subbox${subject.SubjectId}`), subject);

    //题目内容
    //showQuestion($(`#subbox${subject.SubjectId}`), subject);
    //参考答案
    subject.StudentAnswer = subject.Answer;
    if (subject.StudentAnswer == 0 && subject.Answer == 0) {
        $(`#ansbox${subject.SubjectId}`).html("<div class='judgelist fl' style='color:#479e93'><img src='/Images/StudentInfo/xg1.png' /><span>错误</span></div>");
    } else if (subject.StudentAnswer == 1 && subject.Answer == 1) {
        $(`#ansbox${subject.SubjectId}`).html("<div class='judgelist fl' style='color:#479e93'><img src='/Images/StudentInfo/duig1.png' /><span>正确</span></div>");
    } else if (subject.StudentAnswer == 1 && subject.Answer == 0) {
        $(`#ansbox${subject.SubjectId}`).html("<div class='judgelist fl' style='color:#f47463'><img src='/Images/StudentInfo/duig.png' /><span>正确</span></div>");
    } else if (subject.StudentAnswer == 0 && subject.Answer == 1) {
        $(`#ansbox${subject.SubjectId}`).html("<div class='judgelist fl' style='color:#f47463'><img src='/Images/StudentInfo/xg2.png' /><span>错误</span></div>");
    }
}

//填空题
function exerciseQues(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    let strHtml3 = "";
    strHtml3 += `<div class="practsummary"><div class="choicequestion">`;
    strHtml3 += `<div id="titlebox${subject.SubjectId}" class="questiontitle questiontitle3"></div>`;
    strHtml3 += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml3 += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml3 += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ansbox${subject.SubjectId}"></div></div>`;
    strHtml3 += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
    strHtml3 += `</div></div>`;
    $(".practsummarybox").append(strHtml3);

    showQuestion($(`#titlebox${subject.SubjectId}`), subject);

    var html1 = '';
    for (var i = 0; i < subject.Answer.Perfect.length; i++) {
        if (i!=0) { //subject.Answer.Perfect.length == i + 1
            html1 += '&nbsp;；&nbsp;</i>';
        }
        html1 += '<i>' + subject.Answer.Perfect[i].replace(/\|/g,"&nbsp;或&nbsp;");
    }
    $(`#ansbox${subject.SubjectId}`).html(html1);
}

//选择填空
function multipleQues(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    let strHtml4 = "";
    strHtml4 += `<div class="practsummary"><div class="choicequestion exercisebox">`;
    strHtml4 += `<div class="questiontitlebox"><div id="titlebox${subject.SubjectId}" class="questiontitle nobackground"></div></div>`;
    strHtml4 += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml4 += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml4 += `<div class="questiontitle2 refanswer"><div class="fl">参考答案：</div><div class="info" style="color: #479e93;" id="ansbox${subject.SubjectId}"></div></div>`;
    strHtml4 += `<div class="questiontitle2  refanswer refanswer2"><div class="fl">题目解析：</div><div class="info" style="color: #8c601b;" id = "anabox${subject.SubjectId}"></div></div>`;
    strHtml4 += `</div></div>`;
    $(".practsummarybox").append(strHtml4);

    //题干
    showQuestion($(`#titlebox${subject.SubjectId}`), subject);

    //参考答案
    subject.StudentAnswer = subject.Answer;
    showQuestion($(`#ansbox${subject.SubjectId}`), subject);

    showQuestion($(`#anabox${subject.SubjectId}`), subject);
}

//连线题
function matchQues(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    let strHtml5 = "";
    strHtml5 += `<div class="practsummary"><div class="choicequestion">`;
    strHtml5 += `<div id="titlebox${subject.SubjectId}" class="questiontitlebox"></div>`;
    strHtml5 += `<div id="subbox${subject.SubjectId}" class="questionmian questionmianfun"></div>`
    strHtml5 += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml5 += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml5 += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info answ" ><div class="questionmian questionmianfun1" id="ansbox${subject.SubjectId}"></div></div></div>`;
    strHtml5 += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
    strHtml5 += `</div></div>`;
    $(".practsummarybox").append(strHtml5);
    //题干
    showQuestionTitle($(`#titlebox${subject.SubjectId}`), subject);
    //题目内容
    showQuestion($(`#subbox${subject.SubjectId}`), subject);
    //参考答案
    subject.StudentAnswer = subject.Answer;
    showQuestion($(`#ansbox${subject.SubjectId}`), subject);
}

//主观题
function subjQues(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    let strHtml6 = "";
    strHtml6 += `<div class="practsummary"><div class="choicequestion">`;
    strHtml6 += `<div class="questiontitlebox" id="titlebox${subject.SubjectId}"></div>`;
    strHtml6 += `<div class="questiontitle2" style="padding-top: 0;"><div class="fl">您的作答：</div><div class="info co479e93">${subject.StudentAnswer}</div></div>`;
    strHtml6 += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml6 += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml6 += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info"><span style="color: #8c601b;">${subject.Answer}</span></div></div>`;
    strHtml6 += `<div class="questiontitle2"><div class="fl">评分标准：</div><div class="info"><span style="color: #8c601b;">${subject.Standard}</span></div></div>`;
    strHtml6 += `</div></div>`;
    $(".practsummarybox").append(strHtml6);
    if (subject.StemType == 0) {
        $(`#titlebox${subject.SubjectId}`).html(`<div class="questiontitle1"><img src="${subject.Stem}"/></div>`);
    }
    else {
        $(`#titlebox${subject.SubjectId}`).html(`<div class="questiontitle questiontitle3" style="margin-bottom: 10px;">${subject.Stem}</div>`);
    }
}

//圈点批注标色
function markQues(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    let strHtml7 = "";
    strHtml7 += `<div class="practsummary"><div class="choicequestion">`;
    strHtml7 += `<div class="questiontitlebox" id="titlebox${subject.SubjectId}"></div>`;

    strHtml7 += `<div class="quescont"><div class="quescontlist1" id="subbox${subject.SubjectId}"></div></div>`;

    strHtml7 += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml7 += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml7 += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ansbox${subject.SubjectId}"></div></div>`;
    strHtml7 += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
    strHtml7 += `</div></div>`;
    $(".practsummarybox").append(strHtml7);

    //题干
    showQuestionTitle($(`#titlebox${subject.SubjectId}`), subject);
    //题目内容
    showQuestion($(`#subbox${subject.SubjectId}`), subject);
    //参考答案
    subject.StudentAnswer = subject.Answer;
    showQuestion($(`#ansbox${subject.SubjectId}`), subject);
}

//圈点批注断句
function mark2Ques(subject) {
    let imgStr = "";
    for (var i = 0; i < subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars.png" />';
    }
    for (var i = 0; i < 5 - subject.ResultStars; i++) {
        imgStr += '<img src="/Images/StudentInfo/stars-num.png" />';
    }
    let strHtml8 = "";
    strHtml8 += `<div class="practsummary"><div class="choicequestion">`;
    strHtml8 += `<div class="questiontitlebox" id="titlebox${subject.SubjectId}"></div>`;

    strHtml8 += `<div class="quescont"><div class="quescontlist" id="subbox${subject.SubjectId}"></div></div>`;

    strHtml8 += `<div class="questiontitle2"><div class="fl">本题难度：</div><div class="info">${subject.DifficultyStr}</div></div>`;
    strHtml8 += `<div class="questiontitle2"><div class="fl">本题得分：</div><div class="info sore">${imgStr}</div></div>`;
    strHtml8 += `<div class="questiontitle2"><div class="fl">参考答案：</div><div class="info co479e93" id="ansbox${subject.SubjectId}"></div></div>`;
    strHtml8 += `<div class="questiontitle2"><div class="fl">题目解析：</div><div class="info"><span style="color: #8c601b;">${subject.Analysis == null ? "无" : subject.Analysis}</span></div></div>`;
    strHtml8 += `</div></div>`;
    $(".practsummarybox").append(strHtml8);
    //题干
    showQuestionTitle($(`#titlebox${subject.SubjectId}`), subject);
    //题目内容
    showQuestion($(`#subbox${subject.SubjectId}`), subject);
    //参考答案
    subject.StudentAnswer = subject.Answer;
    showQuestion($(`#ansbox${subject.SubjectId}`), subject);
}