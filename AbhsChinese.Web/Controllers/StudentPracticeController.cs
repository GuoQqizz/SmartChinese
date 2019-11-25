using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Request.StudyPractice;
using AbhsChinese.Web.Models.Common;
using AbhsChinese.Web.Models.StudentPractice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StudentPractice = AbhsChinese.Domain.Dto.Response.StudentPractice;

namespace AbhsChinese.Web.Controllers
{
    /// <summary>
    /// 课后练习
    /// </summary>
    public class StudentPracticeController : BaseController
    {
        /// <summary>
        /// 学生上过课的课程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCoursesAttended(DtoCoursesSearch search)
        {
            search.StudentId = GetCurrentUser().StudentId;
            StudentStudyBll bll = new StudentStudyBll();
            IList<StudentPractice.DtoCourse> courses = bll.GetCoursesAttended(search);
            var result = courses.ConvertTo<IList<CourseListItemVm>>();

            var table = AbhsTableFactory.Create(result, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据课程获取有多少道练习题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSubjectsToPractice(IList<DtoSubjectSearch> searches)
        {
            CourseSubjectRelBll bll = new CourseSubjectRelBll();
            var sch = searches.Select(s => new Tuple<int, int>(s.CourseId, s.NextLessonIndex));
            IList<StudentPractice.DtoSubjectCountToPractice> courses =
                bll.GetSubjectCountOfStudyPractice(sch);

            //将每个课时的练习题按课程分组，得到某个课程的所有能做的练习题
            var result = courses.GroupBy(s => s.CourseId)
                .Select(g => new { CourseId = g.Key, TotalSubjectCount = g.Count() });
            return Json(new SuccessJsonResponse(result), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据课程获取练习过得题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTotalSubjectsPracticed(IList<int> courseIds)
        {
            StudentPracticeBll bll = new StudentPracticeBll();
            IList<StudentPractice.DtoSubjectCountToPractice> subjects =
               bll.GetTotalSubjectsPracticed(courseIds, GetCurrentUser().StudentId);

            //将每个课时的练习过得题按课程分组，得到某个课程的所有练习过的练习题
            var result = subjects.GroupBy(s => s.CourseId)
                 .Select(g => new { CourseId = g.Key, TotalSubjectCount = g.Sum(ss => ss.SubjectCount) });
            return Json(new SuccessJsonResponse(result), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 学生购买的课程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListCourses(DtoCoursesSearch historySearch)
        {
            string condition = string.Empty;
            if (historySearch != null && historySearch.Pagination.PageIndex > 1)
            {
                condition = JsonConvert.SerializeObject(historySearch);
            }
            ViewBag.HistorySearch = condition;
            return View();
        }
    }
}