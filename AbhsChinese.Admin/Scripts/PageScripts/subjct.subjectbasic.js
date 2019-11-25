function renderSubjectBasicData(data) {
    //为顶部的table添加数据
    let baiscData = {
        GradeText: data.GradeText,
        DifficultyText: data.DifficultyText,
        Keywords: data.Keywords,
        KnowledgeText: data.KnowledgeText,
        KnowledgeTexts: data.KnowledgeTexts
    };
    $.each(baiscData, function (name, value) {
        let val = value;
        $('p[name="' + name + '"]').html(val);
    });
}