function initSelectPageComponent() {
    $('#knowledge').selectPage({
        showField: 'Text',
        keyField: 'Key',
        pageSize: 10,
        eClose: function (event) {
            if (!event.elem.hidden.val()) {
                event.elem.combo_input.val('')
            }
        },
        data: '/Select2/GetKernelKnowledgePoint',
        inputDelay: 0.5,
        beforeSend: function (e) {
            let selfId = $('#knowledge_text').val();
            if (!selfId) {
                e.abort();
            } else {
                let secIdStr = $('#knowledges_text').val();
                if (secIdStr) {
                    let secIds = secIdStr.split(',');
                    $.each(secIds, function (i, o) {
                        if (o === selfId) {
                            layer.msg('该知识点已经作为次知识点', {
                                icon: 5
                            });
                            e.abort();
                            return false;
                        }
                    });

                }
            }
        },
        eAjaxSuccess: function (data) {
            return data;
        }
    });

    $('#knowledges').selectPage({
        showField: 'Text',
        keyField: 'Key',
        pageSize: 10,
        data: '/Select2/GetSecondaryKnowledgePoints',
        inputDelay: 0.5,
        beforeSend: function (e) {
            let kelId = $('#knowledge').val();
            console.log(kelId);
            if (!kelId) {
                layer.msg('请先选择主知识点', {
                    icon: 5
                });
                $('#knowledges_text').val('');
                e.abort();
            }
            let selfId = $('#knowledges_text').val();
            if (!selfId) {
                e.abort();
            } else {

                if (kelId === selfId) {
                    layer.msg('该知识点已经作为主知识点', {
                        icon: 5
                    });
                    $('#knowledges_text').val('');
                    e.abort();
                }
            }
        },
        params: function () { return { kernelKonwledgePointId: $('#knowledge').val() }; },
        eAjaxSuccess: function (data) {
            return data;
        },
        multiple: true,
        eClose: function (event) {
            if (!event.elem.hidden.val()) {
                event.elem.combo_input.val('')
            }
        }
    });
}

function setBasicData(data) {
    $('select[name="grade"]').select({
        id: data.Grade,
        text: data.GradeText
    });
    $('select[name="difficulty"]').select({
        id: data.Difficulty,
        text: data.DifficultyText
    });

    if (data.Knowledge) {
        $('#knowledge').val(data.Knowledge);
    }
    if (data.Knowledges) {
        $('#knowledges').val(data.Knowledges);
    }
    initSelectPageComponent();

    $('input[name="keywords"]').tagsinput('add', data.Keywords);
}