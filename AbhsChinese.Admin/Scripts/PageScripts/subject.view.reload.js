function reloadData(data, subjectType) {
    switch (subjectType) {
        case SubjectTypeEnum.选择题:
            reloadMultipleChoiceSubjectData(data);
            break;
        case SubjectTypeEnum.判断题:
            reloadTrueFalseSubjectData(data);
            break;
        case SubjectTypeEnum.填空题:
            reloadFillBlankSubjectData(data);
            break;
        case SubjectTypeEnum.选择填空:
            reloadMultipleChoiceFillBlankSubjectData(data);
            break;
        case SubjectTypeEnum.连线题:
            reloadMatchSubjectData(data);
            break;
        case SubjectTypeEnum.主观题:
            reloadFreeSubjectData(data);
            break;
        case SubjectTypeEnum.圈点批注标色:
            reloadMarkSubjectData(data);
            break;
        case SubjectTypeEnum.圈点批注断句:
            reloadMark2SubjectData(data);
            break;
        default:
            break;
    }
}

function reloadMultipleChoiceSubjectData(datasource) {
    //第一个tab的数据
    let data = {
        Name: datasource.Name,
        Answers: datasource.Answers,
        Options: datasource.Options,
        Display: datasource.Display,
        Random: datasource.Random,
        Mark: datasource.Mark,
        Explain: datasource.Explain
    };

    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Answers") {
            let options = ['A', 'B', 'C', 'D'];
            let html = [];
            $.each(propertyValue, function (index, value) {
                html.push(options[value]);
            });
            val = html.join(', ');
        } else if (propertyName === "Options") {
            $.each(propertyValue, function (index, value) {
                let inputName = 'Options[' + index.toString() + ']';
                $('p[name="' + inputName + '"]').html(value);
            });
            return true;
        } else if (propertyName === "Display") {
            let html = propertyValue === 0 ? '不显示' : '显示';
            val = html;
        } else if (propertyName === "Random") {
            let html = propertyValue === 0 ? '顺序排列' : '随机排列';
            val = html;
        } else if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });
}

function reloadTrueFalseSubjectData(datasource) {
    let data = {
        Answer: datasource.Answer,
        Name: datasource.Name,
        Mark: datasource.Mark
    };

    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Answer") {
            val = propertyValue === 1 ? '正确' : '错误';
        } else if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });
}

function reloadFillBlankSubjectData(datasource) {
    let data = {
        Correct: datasource.Correct,
        Perfect: datasource.Perfect,
        Other: datasource.Other,
        Name: datasource.Name,
        Mark: datasource.Mark
    };
    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Correct") {
            renderAnswers(propertyName, propertyValue);
        } else if (propertyName === "Perfect") {
            renderAnswers(propertyName, propertyValue);
        } else if (propertyName === "Other") {
            renderAnswers(propertyName, propertyValue);
        } else if (propertyName === "Name") {
            //val = propertyValue.replace(reg, '__');
        } else if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });
}
function renderAnswers(propertyName, propertyValue) {
    let $correct = $('#div_' + propertyName.toLowerCase());
    let html = '';
    $.each(propertyValue.Blanks, function (index, value) {
        html += '<p class="form-control-plaintext">$blank&nbsp;&nbsp;&nbsp;$text</p>';
        let blank = '空' + (index + 1).toString();
        html = html.replace('$blank', blank)
                   .replace('$text', value);
    });
    $correct.html(html);
}

function reloadMultipleChoiceFillBlankSubjectData(datasource) {
    let data = {
        Options: datasource.Options,
        Goptions: datasource.Goptions,
        Name: datasource.Name,
        Mark: datasource.Mark,
        Explain: datasource.Explain
    };
    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Options") {
            let html = '';
            $.each(propertyValue, function (index, value) {
                html += '<div class="form-group row"><label class="col-2 offset-1 col-form-label text-right">答案' + (index + 1) + '</label><div class="col-9 p-down">' + value + '</div></div>';
            });
            $('#div_answers').html(html);
            return true;
        } else if (propertyName === "Goptions") {
            let html = '';
            $.each(propertyValue, function (index, value) {
                html += '<div class="form-group row"><label class="col-2 offset-1 col-form-label text-right">干扰项' + (index + 1) + '</label><div class="col-9 p-down">' + value + '</div></div>';
            });
            $('#div_wronganswers').html(html);
            return true;
        } else if (propertyName === "Name") {
            //val = propertyValue.replace(reg, '__');
        } else if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });
}

function reloadMatchSubjectData(datasource) {
    let data = {
        Title: datasource.Title,
        Mark: datasource.Mark,
        Name: datasource.Name,
        Answer: datasource.Answer,
        LinedAnswers: datasource.LinedAnswers,
        Explain: datasource.Explain
    };
    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Title") {
            let lefts = data.Title;
            let rights = data.Answer;
            let html = '';
            let template = '<div class="col-6">$left</div><div class="col-6">$right</div>';
            $.each(lefts, function (i, o) {
                let rightO = '';
                if (i < rights.length) {
                    rightO = rights[i];
                }
                let partialHtml = template.replace('$left', o).replace('$right', rightO);
                html += partialHtml;
            });

            $('#div_content').append(html);
            return true;
        } else if (propertyName === "LinedAnswers") {
            //let dataOfFirstCols = data.Title;
            //let firstTdObjs = createFirstTdOfRow(dataOfFirstCols);
            //let restTdObjs = getRestTds(firstTdObjs.length, data.Answer, propertyValue);
            //render(firstTdObjs, restTdObjs);
            let lefts = data.Title;
            let rights = data.Answer;
            let html = '';
            let template = '<div class="col-6">$left</div><div class="col-6">$right</div>';
            $.each(lefts, function (i, o) {
                let indexOfRightO = propertyValue[i];
                let rightO = rights[indexOfRightO];
                let partialHtml = template.replace('$left', o).replace('$right', rightO);
                html += partialHtml;
            });

            $('#table_answers').append(html);
            return true;
        } else if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });

}
function createFirstTdOfRow(datas) {
    let tdObjs = [];

    if (datas) {
        $.each(datas, function (i, o) {
            let tdObj = generateTdObj(i, 0, o);
            tdObjs.push(tdObj);
        });
    }

    return tdObjs;
}
function generateTdObj(rowIndex, colIndex, text) {
    let rowNum = rowIndex ? rowIndex : -1;
    let colNum = colIndex ? colIndex : -1;
    let txt = text ? text : '';
    let tdObj = {
        rowIndex: rowNum,
        colIndex: colNum,
        text: txt,
        checkedCss: '<i class="fa fa-check-square-o"></i>',
        checked: false,
        getHtml: function () {
            let html = '';
            if (this.checked) {
                html = '<td>' + this.checkedCss + this.text + '</td>';
            } else {
                html = '<td>' + this.text + '</td>';
            }
            return html;
        }
    };
    return tdObj;
}
function getRestTds(rowsCount, datas, selecteds) {
    debugger;
    let groupedTdObjs = [];
    for (var i = 0; i < rowsCount; i++) {
        let tdObjs = [];
        let indexOfChecked = selecteds[i];
        $.each(datas, function (index, o) {
            let tdObj = generateTdObj(i, index, o);
            if (indexOfChecked === index) {
                tdObj.checked = true;
            }
            tdObjs.push(tdObj);
        });
        groupedTdObjs.push(tdObjs);
    }
    return groupedTdObjs;
}
function render(firstTdObjs, restTdObjs) {
    if (!firstTdObjs) {
        return;
    }

    let html = '';
    for (var i = 0; i < firstTdObjs.length; i++) {
        let template = '<tr>$firstTd$restTds</tr>';

        let firstTd = firstTdObjs[i].getHtml();

        let restTds = '';
        $.each(restTdObjs[i], function (index, tdObj) {
            let td = tdObj.getHtml();
            restTds += td;
        });

        let tr = template.replace('$first', firstTd).replace('$restTds', restTds);
        html += tr;
    }

    let $table_answers = $('#table_answers');
    $table_answers.html(html);
}

function reloadFreeSubjectData(data) {
    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });
}
function reloadMarkSubjectData(datasource) {
    let data = {
        Color: datasource.Color,
        Mark: datasource.Mark,
        Name: datasource.Name,
        Content: datasource.Content
    };
    $.each(data, function (propertyName, propertyValue) {
        let val = propertyValue;
        if (propertyName === "Color") {
            $(':radio[value="' + propertyValue + '"]').prop('checked', true);
            return true;
        } else if (propertyName === "Mark") {
            $.each(propertyValue, function (i, o) {
                let $checkbox = $('#mark' + o);
                $checkbox.prop('checked', true);
                $checkbox.next().addClass('abhs-warn');
            });
            return true;
        }
        $('p[name="' + propertyName + '"]').html(val);
    });
}
function reloadMark2SubjectData(data) {
    reloadMarkSubjectData(data);
}


