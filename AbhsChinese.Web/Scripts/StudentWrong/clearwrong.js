//var pageIndex = 1;
//var studyTaskId = 10004;
$(function () {
    loadData(Yws_Status);
    registerEvent();
});
function registerEvent() {
    $('.backBtn').on('click', function () {
        //history.go(-1);
        window.location.href = wrongSubjectUrl + wrongSubjectId + '&r=' + new Date().getTime();
    });

}

//后台获取数据插入页面
function loadData(status) {
    console.log(status);
    if (status == 3) {
        loadRelationData();
    } else {
        loadSubjectData();
    }
}
//获取重做数据
function loadSubjectData() {
    $.ajax({
        type: "get",
        url: subjectDetailUrl,
        data: {
            wrongSubjectId: wrongSubjectId
        },
        success: function (res) {
            if (res.State) {
                var data = res.Data;
                renderSubjectDetail(data);
            }
        },
        //error: function (err) {
        //    console.error(err);
        //}
    });
}
//重做结果提交
let canSubmitQuestionAnswerStatus = true;
function submitQuestionAnswer(data) {
    //console.log(data)
    if (canSubmitQuestionAnswerStatus) {
        submitQuestionAnswerStatus = false;
        let toStatus = data.rtar == 5 ? 3 : 2;
        //if (data.rtar == 5) {
        $.ajax({
            url: clearWrongUrl,
            data: { bookId: wrongBookId, wrongSubjectId: wrongSubjectId, toStatus: toStatus },//2 消除中,3 重做做对,4:关联做对
            type: 'POST',
            success: function (res) {
                console.log(res);
                //goNext(false);
            },
            error: function (err) {
                console.log(err);
            },
            complete: function () {
                canSubmitQuestionAnswerStatus = true;
                goNext(false);
            }
        })
        //} else {
        //    goNext(false);
        //}
    }


}
//获取关联题目数据
function loadRelationData() {
    $.ajax({
        type: "get",
        url: subjectRelationUrl,
        data: {
            wrongSubjectId: wrongSubjectId,
        },
        success: function (data) {
            if (data.State) {
                $(".contentCenterList").removeClass("active");
                $(".textName").html("全部答对才可消除错题 （共" + data.TotalRecord + "题）");
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
                        var sops = [];
                        var styles = [];
                        var optionStr = "";
                        $.each(obj.Options, function (j, res) {
                            sops.push(res.Key);
                            var len = res.length;
                            if (len <= 5) {
                                styles.push("position: absolute;left:" + ((780 - 122 * len) / 5 + j * 122) + "px;top:0;")
                            } else {
                                var num = (780 - 122 * 5) / 6;
                                styles.push("position: absolute;left:" + ((j % 5 + 1) * num + (j % 5) * 122) + "px;top:" + parseInt(j / 5) * 76 + ";")
                            }
                        });
                        obj.sops = sops;
                        obj.styles = styles;
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
                $('html,body').animate({ scrollTop: 0 }, 500);
                pageIndex++;
                if (maxNum < TotalRecord) {
                    $(".nextBtn").show();
                } else {
                    $(".nextBtn").hide();
                    $(".submitBtn").show();
                    $(".preBtn").show();
                }
                $(".textOrder .orderList").click(function () {
                    var num = $(this).html();
                    $('html,body').animate({ scrollTop: $("#contentList_" + num).offset().top - 80 }, 500);
                })
                renderSubject(contentCenterList);
            }
        }
    });
}
//关联题目结果提交
let canDoSubmit = true;
function doSubmit(submitData) {
    if (canDoSubmit) {
        canDoSubmit = false;
        //console.log(submitData.answers);
        let wrongCount = submitData.answers.length - submitData.answers.filter(a=>a.rtar == 5).length
        let right = wrongCount == 0;
        let toStatus = right ? 4 : 3;
        let isError = false;


        $.ajax({
            url: clearWrongUrl,
            data: { bookId: wrongBookId, wrongSubjectId: wrongSubjectId, toStatus: toStatus },
            type: 'POST',
            success: function (res) {
                console.log(res);
                if (right && res.ErrorCode != 0 && !res.State) {
                    isError = false;
                }
            },
            //error: function (err) {
            //    console.error(err);
            //    isError = true;
            //},
            complete: function () {
                closeShadow();
                canDoSubmit = true;
                showResult(wrongCount, isError);
            }
        })
    }
}
//展示关联题作答结果
function showResult(wrongCount, isError) {
    $(".textMask").show();
    $(document).scrollTop(0);
    $('body').css('overflow', 'hidden');
    if (isError) {
        $(".resultState").addClass("resultState_wrong");
        $(".resultStateTitle").html("服务器开小差了...");
    } else {
        if (wrongCount == 0) {
            $(".resultState").addClass("resultState_right");
            $(".resultStateTitle").addClass("resultStateTitle_right");
            $(".resultStateTitle").html("祝贺你，错题消除成功！");
        } else {
            $(".resultState").addClass("resultState_wrong");
            $(".resultStateTitle").html("做错<span id='wrongNumber'>" + wrongCount + "</span>题，错题未消除！");
        }
    }
    $(".textMaskContentSureBtn,.textMaskCloseBtn").click(function () {
        goNext(wrongCount == 0);
    })
}
//跳转下一题
function goNext(right) {
    let goto = wrongSubjectUrl;
    if (right) {
        let index = wrongSubjectIds.indexOf(wrongSubjectId);
        if (index < wrongSubjectIds.length - 1) {
            goto += wrongSubjectIds[index + 1]
        } else if (wrongSubjectIds.length > 1) {
            goto += wrongSubjectIds[0];
        } else {
            goto = wrongBookUrl;
            window.localStorage.setItem('topath', wrongBookUrl);
        }
    } else {
        goto += wrongSubjectId;
    }
    if (goto.indexOf('?') > -1) {
        goto += '&r=' + new Date().getTime();
    } else {
        goto += '?r=' + new Date().getTime();
    }
    window.location.href = goto;
}

