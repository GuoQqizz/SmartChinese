using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Course;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    
    /// <summary>
    /// 课程控制器
    /// </summary>
    public class CourseController : BaseController
    {
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }


        [HttpPost, ActionName("AddCourse")]
        public ActionResult AddCourseConfirm(CourseInputModel input)
        {
            Domain.Dto.Request.DtoCourse course = input.ConvertTo<Domain.Dto.Request.DtoCourse>();
            course.CurrentUser = CurrentUserID;

            CourseBll bll = new CourseBll();
            bll.AddCourse(course);

            return Json(new SuccessJsonResponse(Url.Action(nameof(ListCurriculums))));
        }

        /// <summary>
        /// 课程详情
        /// </summary>
        /// <param name="curriculumID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(id);
        }

        [HttpGet]
        /// <returns></returns>
        public ActionResult GetCurriculums(DtoCurriculumSearch search)
        {
            CourseBll bll = new CourseBll();
            IList<DtoCourseListItem> entities = bll.GetCourses(search);

            PropertyNamePrefixAction action = PropertyNamePrefixAction.Remove;
            IEnumerable<CourseViewModel> list =
                entities.Select(s => s.ConvertTo<CourseViewModel>(action));
            return Table(list, search.Pagination.TotalCount);
        }

        [HttpGet]
        public ActionResult GetDetails(int id)
        {
            CourseBll bll = new CourseBll();
            var dtoDetails = bll.GetDetails(id);
            var vm = dtoDetails.ConvertTo<CurriculumDetailsViewModel>(
                PropertyNamePrefixAction.Remove);
            vm.CoverImage = UeditorContentFactory.RestoreUrl(
                vm.CoverImage, UeditorType.Image);
            SuccessJsonResponse response = new SuccessJsonResponse { Data = vm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取课程下的课时
        /// </summary>
        /// <param name="id">课程的id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetLessons(int courseId)
        {
            LessonBll bll = new LessonBll();
            var lessons = bll.GetLessons(courseId);
            var result = lessons.ConvertTo<IList<LessonViewModel>>();
            SuccessJsonResponse response = new SuccessJsonResponse { Data = result };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CloseCourse(int courseId)
        {
            CourseBll bll = new CourseBll();
            var course = bll.GetCourse(courseId);
            if (course.Ycs_Status == (int)CourseStatusEnum.已上架)
            {
                return Json(new JsonSimpleResponse
                {
                    State = false,
                    ErrorMsg = "已上架的课程不能关闭"
                });
            }
            bll.UpdateStatus(
                    courseId,
                    CourseStatusEnum.已关闭,
                    CourseActionEnum.关闭,
                    CurrentUserID);
            return Json(new SuccessJsonResponse());
        }

        [HttpPost]
        public ActionResult DeleteCourse(int courseId)
        {
            CourseBll bll = new CourseBll();
            var course = bll.GetCourse(courseId);
            if (course.Ycs_Status != (int)CourseStatusEnum.已关闭)
            {
                return Json(new JsonSimpleResponse
                {
                    State = false,
                    ErrorMsg = "只有已关闭的课程才能删除"
                });
            }
            bll.UpdateStatus(
                    courseId,
                    CourseStatusEnum.已删除,
                    CourseActionEnum.删除,
                    CurrentUserID);
            return Json(new SuccessJsonResponse());
        }

        [HttpPost]
        public ActionResult ReopenCourse(int courseId)
        {
            CourseBll bll = new CourseBll();
            bll.ReopenCourse(courseId, CurrentUserID);
            return Json(new SuccessJsonResponse());
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetLogs(int courseId)
        {
            CourseBll bll = new CourseBll();
            var lessons = bll.GetLogs(courseId);
            var result = lessons.ConvertTo<IList<LessonViewModel>>();
            SuccessJsonResponse response = new SuccessJsonResponse { Data = result };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 课程管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ListCurriculums()
        {
            return View();
        }

        private JsonResult Table<T>(IEnumerable<T> data, int totalRecord)
            where T : class, new()
        {
            var result = AbhsTableFactory.Create(data, totalRecord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}