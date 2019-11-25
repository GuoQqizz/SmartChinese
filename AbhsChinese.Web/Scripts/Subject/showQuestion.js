///
/// dom:元素
/// data:提的数据
var choiceStr = ['A', 'B', 'C', 'D', 'E', 'F'];

function showQuestion(dom, data, showJudgeError) {
    showJudgeError = showJudgeError || false;
    if (data.SubjectType) {
        switch (data.SubjectType) {
            case 1: //选择题
                choiceQuestion(dom, data)
                break;
            case 2: //判断题
                judge(dom, data, showJudgeError)
                break;
            case 3: //填空题
                exercise(dom, data)
                break;
            case 4: //选择填空
                multipleChoice(dom, data);
                break;
            case 5: //连线题
                match(dom, data);
                break;
            case 6: //主观题

                break;
            case 7: //圈点批注标色
                punctuationMark(dom, data)
                break;
            case 8: //圈点批注断句
                pausesReading(dom, data)
                break;
            default:
                console.error("类型错误");
        }
    }
    //选择题
    function choiceQuestion(dom, data) {
        var html = "";
        for (var i = 0; i < data.Options.length; i++) {
            if (data.OptionType) {
                html += "<div class='choicelist ans" + data.Options[i].Key + "'>" + choiceStr[i] + '. <span>' + data.Options[i].Text +
					"</span></div>";
            } else {
                html += "<div class='choicelistimg ans" + data.Options[i].Key + "'>";
                html += "<div class='choitem itemcenter ans" + data.Options[i].Key + "'>";
                html += "<img src='" + data.Options[i].Text + "' />";
                html += "</div>";
                html += "<div class='info infoans" + data.Options[i].Key + "'>";
                html += choiceStr[i];
                html += "</div>";
                html += "</div>";
            }
        }
        $(dom).html(html);
        for (var i = 0; i < data.StudentAnswer.length; i++) {
            if (data.OptionType) {
                if ($.inArray(data.StudentAnswer[i], data.Answer) > -1) {
                    $(dom).find('.ans' + data.StudentAnswer[i]).find('span').addClass('co479e93');
                } else {
                    $(dom).find('.ans' + data.StudentAnswer[i]).find('span').addClass('cof47463');
                }
            } else {
                if ($.inArray(data.StudentAnswer[i], data.Answer) > -1) {
                    $(dom).find('.ans' + data.StudentAnswer[i]).addClass('selectright');
                } else {
                    $(dom).find('.ans' + data.StudentAnswer[i]).addClass('errorselect');
                }
            }

        }
    }
    //判断题
    function judge(dom, data, showJudgeError) {

        var html = "";
        if (data.StudentAnswer == 0 && data.Answer == 0) {
            if (!showJudgeError) {
                html += "<div class='judgelist fl' style='color:#666'><img src='/Images/StudentInfo/duig2.png' /><span>正确</span></div>";
            }
            html += "<div class='judgelist fl' style='color:#479e93'><img src='/Images/StudentInfo/xg1.png' /><span>错误</span></div>";
        } else if (data.StudentAnswer == 1 && data.Answer == 1) {
            html += "<div class='judgelist fl' style='color:#479e93'><img src='/Images/StudentInfo/duig1.png' /><span>正确</span></div>";
            if (!showJudgeError) {
                html += "<div class='judgelist fl' style='color:#666'><img src='/Images/StudentInfo/xg.png' /><span>错误</span></div>";
            }
        } else if (data.StudentAnswer == 1 && data.Answer == 0) {
            html += "<div class='judgelist fl' style='color:#f47463'><img src='/Images/StudentInfo/duig.png' /><span>正确</span></div>";
            html += "<div class='judgelist fl' style='color:#666'><img src='/Images/StudentInfo/xg.png' /><span>错误</span></div>";
        } else if (data.StudentAnswer == 0 && data.Answer == 1) {
            html += "<div class='judgelist fl' style='color:#666'><img src='/Images/StudentInfo/duig2.png' /><span>正确</span></div>";
            html += "<div class='judgelist fl' style='color:#f47463'><img src='/Images/StudentInfo/xg2.png' /><span>错误</span></div>";
        } else {
            html += "<div class='judgelist fl' style='color:#666'><img src='/Images/StudentInfo/duig2.png' /><span>正确</span></div>";
            html += "<div class='judgelist fl' style='color:#666'><img src='/Images/StudentInfo/xg.png' /><span>错误</span></div>";
        }
        $(dom).html(html);
    }
    //填空题
    function exercise(dom, data) {
        var str = data.Stem;
        var arr = str.split("{:}");
        var html = "";
        for (var i = 0; i < arr.length; i++) {
            html += arr[i];
            if (i < arr.length - 1) {
                var answer = null;
                for (var o = 0; o < data.StudentAnswer.length; o++) {
                    if (data.StudentAnswer[o].Index == i) {
                        answer = data.StudentAnswer[o];
                        break;
                    }
                }
                if (answer) {
                    html += '<span class="' + (answer.Score == 1 ? "co479e93" : "cof47463") + '">(&nbsp;&nbsp;' + answer.Text +
						'&nbsp;&nbsp;)</span>';
                } else {
                    html += '<span>(&nbsp;&nbsp;&nbsp;&nbsp;)</span>';
                }
            }
        }
        $(dom).html(html);
    }
    //选择填空
    function multipleChoice(dom, data) {
        var str = data.Stem;
        var arr = str.split("{:}");
        var html = "";
        for (var i = 0; i < arr.length; i++) {
            html += arr[i];
            if (i < arr.length - 1) {
                var thisanswer = null;
                for (var sa = 0; sa < data.StudentAnswer.length; sa++) {
                    if (data.StudentAnswer[sa][0] == i) {
                        thisanswer = data.StudentAnswer[sa];
                    }
                }
                if (thisanswer) //如果这个空有答案
                {
                    if (anws(data.Answer, thisanswer)) {
                        for (var iii = 0; iii < data.Options.length; iii++) {
                            if (data.Options[iii].Key == thisanswer[1]) {
                                if (data.OptionType) {
                                    html += '(&nbsp;<span class="co479e93">&nbsp;' + data.Options[iii].Text + '&nbsp;</span>&nbsp;)';
                                } else {
                                    html += '(&nbsp;<span class="co479e93"><img src="' + data.Options[iii].Text + '" /></span>&nbsp;)';
                                }
                            }
                        }
                    } else if (!anws(data.Answer, thisanswer)) {
                        for (var iii = 0; iii < data.Options.length; iii++) {
                            if (data.Options[iii].Key == thisanswer[1]) {
                                if (data.OptionType) {
                                    html += '(&nbsp;<span class="cof47463">&nbsp;' + data.Options[iii].Text + '&nbsp;</span>&nbsp;)';
                                } else {
                                    html += '(&nbsp;<span class="cof47463"><img src="' + data.Options[iii].Text + '" /></span>&nbsp;)';
                                }
                            }
                        }
                    }
                } else //如果这个空没有答案
                {
                    html += '(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)';
                }
            }
        }
        $(dom).html(html);
    }
    //连线题
    function match(dom, data) {
        let html = "";
        let array = [];
        let left = data.LeftOptions;
        let right = data.RightOptions;
        for (var i = 0; i < left.length; i++) {
            array.push([left[i].Text, left[i].Key]);
            array.push([right[i].Text, right[i].Key]);
        }
        //添加连线题选项
        for (var i = 0; i < array.length; i++) {
            var d = array[i];
            var top = Math.floor(i / 2) * 61;
            if (i % 2 == 0) {
                html += '<div style="top:' + top + 'px" class="option questitem leftoption' + d[1] + '">';
                if (data.LeftOptionType) {
                    html += d[0];
                } else { //如果左边是图片
                    html += "<img src ='" + d[0] + "' />";
                }
                html += '</div>';
            } else {
                html += '<div style="top:' + top + 'px" class="option questitem rightoption' + d[1] + '">';
                if (data.RightOptionType) { //如果左边是图片
                    html += d[0];
                } else {
                    html += "<img src ='" + d[0] + "' />";

                }
                html += '</div>';
            }

        }
        //结束连线题选项
        $(dom).html(html);
        var hgt = (Math.floor(array.length / 2)) * 61;
        $(dom).css({
            'height': hgt + 'px'
        });
        //遍历学生答案
        for (var i = 0; i < data.StudentAnswer.length; i++) {
            var sa = data.StudentAnswer[i]; //学生划线
            var isTrue = false; //假设答案错误
            for (var o = 0; o < data.Answer.length; o++) //循环判断是否正确
            {
                var a = data.Answer[o]; //正确答案连线
                if (sa[0] == a[0]) //如果左边是同一个选项
                {
                    isTrue = sa[1] == a[1]; //右边选项是否正确
                    break;
                }
            }
            var leftDom = $(dom).find(".leftoption" + sa[0]);
            var rightDom = $(dom).find(".rightoption" + sa[1]);
            drawLine(leftDom, rightDom, dom, isTrue);
        }
    }
    //圈点批注
    function punctuationMark(dom, data) {
        var contentStr = "";
        for (var i = 0; i < data.Content.length; i++) {
            var char = data.Content[i];
            if (/^\n$/.test(char)) {
                contentStr += "<span class='answerNormalText' style='display:none;'></span><br />";
            } else if (/\r/.test(char)) {
                contentStr += "<span class='answerNormalText'></span>";
            } else if (/ /.test(char)) {
                contentStr += "<span class='answerNormalText'>&nbsp;</span>";
            } else {
                contentStr += "<span class='answerNormalText'>" + char + "</span>";
            }
        }
        $(dom).html(contentStr);
        for (var i = 0; i < data.StudentAnswer.length; i++) {
            if (anws(data.Answer, data.StudentAnswer[i])) {
                for (var ii = data.StudentAnswer[i][0]; ii < data.StudentAnswer[i][0] + data.StudentAnswer[i][1]; ii++) {
                    $(dom).find('span').eq(ii).addClass('trueans');
                }
            } else if (!anws(data.Answer, data.StudentAnswer[i])) {
                for (var ii = data.StudentAnswer[i][0]; ii < data.StudentAnswer[i][0] + data.StudentAnswer[i][1]; ii++) {
                    $(dom).find('span').eq(ii).addClass('falseans');
                }
            }
        }
    }
    //圈点断句
    function pausesReading(dom, data) {
        var contentStr = "";
        for (var i = 0; i < data.Content.length; i++) {
            var char = data.Content[i];
            if (/^\n$/.test(char)) {
                contentStr += "<span class='answerNormalText' style='display:none;'></span><br />";
            }
            else if (/\r/.test(char)) {
                contentStr += "<span class='answerNormalText'></span>";
            }
            else if (/ /.test(char)) {
                contentStr += "<span class='answerNormalText'>&nbsp;</span>";
            }
            else {
                contentStr += "<span class='answerNormalText'>" + char + "</span>";
            }
        }
        $(dom).html(contentStr);
        for (var i = 0; i < data.StudentAnswer.length; i++) {
            if ($.inArray(data.StudentAnswer[i], data.Answer) > -1) {
                $(dom).find('span').eq(data.StudentAnswer[i]).append('<em class="transwer">/</em>');
            } else if ($.inArray(data.StudentAnswer[i], data.Answer) < 0) {
                $(dom).find('span').eq(data.StudentAnswer[i]).append('<em class="falseswer">/</em>');
            }
        }
    }
}
//title
function showQuestionTitle(dom, data) {
    if (data.SubjectType) {
        switch (data.SubjectType) {
            case 1: //选择题
                choiceQuestion(dom, data);
                break;
            case 2: //判断题
                judge(dom, data)
                break;
            case 3: //填空题

                break;
            case 4: //选择填空

                break;
            case 5: //连线题
                match(dom, data);
                break;
            case 6: //主观题
                break;
            case 7: //圈点批注标色
                punctuationMark(dom, data)
                break;
            case 8: //圈点批注断句
                pausesReading(dom, data);
                break;
            default:
                console.error("类型错误");
        }
    }
    //选择题题目
    function choiceQuestion(dom, data) {
        var html = "";
        if (data.StemType) {
            html += "<div class = 'questiontitle'>" + data.Stem + "</div>";
        } else { //如果题干是图片
            html += "<div class='questiontitle1'><img src ='" + data.Stem + "' /></div>";
        }
        dom.html(html);
    }
    //判断题题目
    function judge(dom, data) {
        var html = "";
        if (data.StemType) {
            html += "<div class = 'questiontitle'>" + data.Stem + "</div>";
        } else { //如果题干是图片
            html += "<div class='questiontitle1'><img src ='" + data.Stem + "' /></div>";
        }
        dom.html(html);
    }
    //连线题题目
    function match(dom, data) {
        var html = "";
        if (data.StemType) {
            html += "<div class = 'questiontitle'>" + data.Stem + "</div>";
        } else { //如果题干是图片
            html += "<div class='questiontitle1'><img src ='" + data.Stem + "' /></div>";
        }
        dom.html(html);
    }

    function punctuationMark(dom, data) {
        var html = "";
        if (data.StemType) {
            html += "<div class = 'questiontitle'>" + data.Stem + "</div>";
        } else { //如果题干是图片
            html += "<div class='questiontitle1'><img src ='" + data.Stem + "' /></div>";
        }
        console.log('html', html)
        dom.html(html);
    }

    function pausesReading(dom, data) {
        var html = "";
        if (data.StemType) {
            html += "<div class = 'questiontitle'>" + data.Stem + "</div>";
        } else { //如果题干是图片
            html += "<div class='questiontitle1'><img src ='" + data.Stem + "' /></div>";
        }
        dom.html(html);
    }
}

// 添加连线
function drawLine(startObj, endObj, objdemo, anws) {
    //console.log(startObj.html(), endObj.html());
    var html = "";
    var y_start = Number(startObj.css("top").replace("px", "")) + startObj.height() / 2;
    var x_start = Number(startObj.css("left").replace("px", "")) + startObj.width();
    var y_end = Number(endObj.css("top").replace("px", "")) + endObj.height() / 2;
    var x_end = Number(endObj.css("left").replace("px", ""));
    var deg = 0;
    if (y_start == y_end) // 画横线
    {
        if (x_start > x_end) {
            var t = x_start;
            x_start = x_end;
            x_end = t
            deg = 180;
        }
        length = Math.abs(x_end - x_start);
    } else if (x_start == x_end) // 画竖线
    {
        deg = 90;
        if (y_start > y_end) {
            var t = y_start;
            y_start = y_end;
            y_end = t
            deg = 270;
        }
        length = Math.abs(y_end - y_start);
    } else { // 画线旋转角度
        var lx = x_end - x_start;
        var ly = y_end - y_start;
        var length = Math.sqrt(lx * lx + ly * ly); //计算连线长度
        var c = 360 * Math.atan2(ly, lx) / (2 * Math.PI); //弧度值转换为角度值
        c = c <= -90 ? (360 + c) : c; //负角转换为正角
        deg = c;
    }
    if (anws) {
        html = "<div class='rotate default' style='background:#5ba597;top:" + y_start + "px;left:" + (x_start + 1) +
			"px;width:" + length + "px;transform:rotate(" + deg + "deg)'>" +
			"<i class='arrow-img'></i>" +
			"<i class='con-img'></i></div>";
    } else {
        startObj.addClass('erroritem');
        endObj.addClass('erroritem');
        html = "<div class='rotate right' style='background:#f47463;top:" + y_start + "px;left:" + (x_start + 1) +
			"px;width:" + length + "px;transform:rotate(" + deg + "deg)'>" +
			"<i class='arrow-img'></i>" +
			"<i class='con-img'></i></div>";
    }
    objdemo.append(html);
}
function anws(array, element) {
    for (var i = 0; i < array.length; i++) {
        if (array[i].length === element.length) {
            for (var ii = 0; ii < element.length; ii++) {
                if (element[ii] != array[i][ii]) {
                    break;
                }
                if (ii == (element.length - 1)) {
                    return true;
                }
            }
        }
    }
    return false;
}