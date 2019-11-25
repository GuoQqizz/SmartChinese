; $(function () {
    getCourses();
});

function getCourses() {
    let url = '/StudentPractice/GetCoursesAttended';

    $.getJSON(url, null)
        .done(function (response) {
            let data = response.Data;
            let searches = '';
            let courseIds = '';
            let html = '';
            $.each(data, function (i, o) {
                html += createDom(o);
                console.log(html);
                courseIds += `courseIds=${o.CourseId}&`;

                searches += `searches[${i}].CourseId=${o.CourseId}&searches[${i}].NextLessonIndex=${o.NextLessonIndex}&`;
            });

            searches = searches.substring(0, searches.length - 1);
            $.getJSON('/StudentPractice/GetSubjectsToPractice', searches).done(function (res) {
                let data = res.Data;
                $.each(data, function (i, o) {
                    $('#div_subjectCount_' + o.CourseId).text(o.TotalSubjectCount);
                });
            });
            courseIds = courseIds.substring(0, courseIds.length - 1);
            $.getJSON('/StudentPractice/GetTotalSubjectsPracticed', courseIds)
                .done(function (r) {
                    let data = r.Data;
                    $.each(data, function (i, o) {
                        $('#div_practiceCount_' + o.CourseId).text(o.TotalSubjectCount);
                    });
                });
            $('#ul_courses').html(html);
        });
}
let courseType = {
    1: '同步课',
    2: '专项课',
    3: '复习课',
    4: '整本书阅读课',
    5: '文学史课'
};
function createDom(obj) {
    return `<li><div>${courseType[obj.CourseType]}</div><div>${obj.CourseName}</div><div id="div_subjectCount_${obj.CourseId}"></div><div id="div_practiceCount_${obj.CourseId}">0</div></li>`;
}