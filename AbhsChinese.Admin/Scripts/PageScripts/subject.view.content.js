function loadData(subjectId, subjectType) {
    let controller = '';
    let callback = null;
    switch (subjectType) {
        case 1:
            callback = reloadMultipleChoiceSubjectData;
            controller = 'MultipleChoice';
            break;
        case 2:
            callback = reloadTrueFalseSubjectData;
            controller = 'TrueFalse';
            break;
        case 3:
            callback = reloadFillBlankSubjectData;
            controller = 'FillInBlank';
            break;
        case 4:
            callback = reloadMultipleChoiceFillBlankSubjectData;
            controller = 'MultipleChoiceFillInBlank';
            break;
        case 5:
            callback = reloadMatchSubjectData;
            controller = 'Match';
            break;
        case 6:
            callback = reloadFreeSubjectData;
            controller = 'Free';
            break;
        case 7:
            callback = reloadMarkSubjectData;
            controller = 'Mark';
            break;
        case 8:
            callback = reloadMark2SubjectData;
            controller = 'Mark2';
            break;
        default:
            break;
    }

    $.getJSON('/' + controller + '/GetReadonlyDetails', { id: subjectId }, function (response) {
        if (response.State) {
            let data = response.Data;
            callback(data);
        }
    });
}

