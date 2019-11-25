using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Course;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class CoverImageInputModel
    {
        public int CourseId { get; set; }
        public string CoverImage { get; set; }
    }
    public class CourseManageController : BaseController
    {
        [HttpGet]
        public ActionResult GetCourses(DtoCurriculumSearch search)
        {
            CourseBll bll = new CourseBll();
            IList<DtoCourseListItem> entities = bll.GetManageCourses(search);

            PropertyNamePrefixAction action = PropertyNamePrefixAction.Remove;
            IEnumerable<CourseViewModel> list =
                entities.Select(s => s.ConvertTo<CourseViewModel>(action));

            return Table(list, search.Pagination.TotalCount);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Pricing(CoursePricingInputModel input)
        {
            CourseBll bll = new CourseBll();
            input.CoverIamge = UeditorContentFactory.FetchUrl(
                input.CoverIamge, UeditorType.Image);
            DtoCoursePricing price = input.ConvertTo<DtoCoursePricing>();
            bll.Pricing(price, CurrentUserID);
            return Json(new SuccessJsonResponse());
        }

        public ActionResult ListCourses()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StopTrading(int courseId)
        {
            CourseBll bll = new CourseBll();
            var course = bll.GetCourse(courseId);
            if (course.Ycs_Status == (int)CourseStatusEnum.已上架)
            {
                bll.UpdateStatus(
                    courseId,
                    CourseStatusEnum.已下架,
                    CourseActionEnum.下架,
                    CurrentUserID);
                return Json(new SuccessJsonResponse());
            }
            else
            {
                return Json(new JsonSimpleResponse
                {
                    State = false,
                    ErrorMsg = "只有已上架的课程才能下架"
                });
            }
        }

        [HttpGet]
        public ActionResult Preview(int courseId)
        {
            string t = DateTime.Now.Ticks.ToString();
            string sign = Encrypt.MD5Hash(t, "8j0pf");
            string url = ConfigurationManager.AppSettings["StudentClientDomain"];
            string fullUrl = url + "Login/StudentLogin";
            return Json(new SuccessJsonResponse(new
            {
                Parameters = new
                {
                    T = t,
                    Sign = sign,
                    CourseId = courseId,
                },
                PreviewUrl = fullUrl
            }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上架
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StartTrading(int courseId)
        {
            CourseBll bll = new CourseBll();
            var course = bll.GetCourse(courseId);
            if (course.Ycs_Status == (int)CourseStatusEnum.待上架)
            {
                bll.UpdateStatus(
                    courseId,
                    CourseStatusEnum.已上架,
                     CourseActionEnum.上架,
                    CurrentUserID);
                return Json(new SuccessJsonResponse());
            }
            else
            {
                return Json(new JsonSimpleResponse
                {
                    State = false,
                    ErrorMsg = "只有待上架的课程才能上架"
                });
            }
        }

        [HttpPost]
        public ActionResult AddCoverImage(CoverImageInputModel input)
        {
            CourseBll bll = new CourseBll();
            string imgUrl = UeditorContentFactory.FetchUrl(
                input.CoverImage, UeditorType.Image);
            bll.AddCoverImage(input.CourseId, imgUrl);
            return Json(new SuccessJsonResponse());
        }

        [HttpGet]
        public ActionResult RemoveCoverImage(CoverImageInputModel input)
        {
            CourseBll bll = new CourseBll();
            bll.RemoveCoverImage(input.CourseId);
            return Json(new SuccessJsonResponse(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDetails(int id)
        {
            CourseBll bll = new CourseBll();
            var dtoDetails = bll.GetDetailOfManagements(id);
            var vm = dtoDetails.ConvertTo<CurriculumDetailsViewModel>(
                PropertyNamePrefixAction.Remove);
            vm.CoverImage = UeditorContentFactory.RestoreUrl(vm.CoverImage, UeditorType.Image);
            SuccessJsonResponse response = new SuccessJsonResponse { Data = vm };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        private JsonResult Table<T>(IEnumerable<T> data, int totalRecord)
            where T : class, new()
        {
            var result = AbhsTableFactory.Create(data, totalRecord);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 课程定价详情
        /// </summary>
        /// <param name="curriculumID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(id);
        }
    }
}