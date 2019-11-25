using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Web.Models.Course;
using AbhsChinese.Web.Models.StudentInfo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class CourseController : BaseController
    {
        private string defaultCourseImg = "/Images/Course/defaultimg.jpg";
        /// <summary>
        /// 课程中心-课程列表
        /// </summary>
        /// <returns></returns>
        // GET: Course/Select
        public ActionResult Select()
        {
            CourseSearch search = new CourseSearch()
            {
                Grade = 0,
                CourseType = 0,
                OrderBy = 1,
                Pagination = new PagingObject() { PageIndex = 1, PageSize = 9 },
                HasMore = true
            };
            AdvertisingBll bll = new AdvertisingBll();
            //广告列表
            List<DtoAdvertisingIndex> AdvertisingList = bll.GetAdvertisingForIndex();

            CourseSelectViewModel model = new CourseSelectViewModel()
            {
                StudentId = GetCurrentUser().StudentId,
                SearchInfo = JsonConvert.SerializeObject(search),
                AdvertisingList = AdvertisingList?.Select(a => new CourseSelectAdvertisingViewModel()
                {
                    ImageUrl = a.Bad_ImageUrlShow,
                    LinkUrl = a.Bad_ReferId == 0 ? a.Bad_Url : Url.Action("Detail", "Course", new { id = a.Bad_ReferId, aid = a.Bad_Id }),
                    CourseId = a.Bad_ReferId,
                    Ad_Id = a.Bad_Id
                }).ToList()
            };

            return View(model);
        }

        /// <summary>
        /// 课程中心-课程列表（加载课程列表）
        /// </summary>
        /// <param name="search">筛选条件、分页信息</param>
        /// <returns></returns>
        public JsonResult GetCourses(CourseSearch search)
        {
            CourseBll bll = new CourseBll();
            DtoCourseSelectCondition condition = new DtoCourseSelectCondition();
            condition.Grade = search.Grade;
            condition.CourseType = search.CourseType;
            condition.OrderBy = (DtoCourseSelectCondition.DtoCourseSelectOrderEnum)search.OrderBy;
            condition.StudentId = GetCurrentUser().StudentId;
            PagingObject paging = search.Pagination;

            //是否包含价格
            bool includePrice = false;
            List<DtoSelectCenterResult> entities = bll.GetCourseForSelectCenter(condition, paging, out includePrice);
            IEnumerable<CourseViewModel> list = entities.Select(s => {
                var model = s.ConvertTo<CourseViewModel>();
                if (string.IsNullOrEmpty(model.CourseImage))
                {
                    model.CourseImage = defaultCourseImg;
                }
                return model;
            });
            bool hasMore = paging.TotalCount > search.Pagination.PageIndex * search.Pagination.PageSize;

            return Json(new JsonResponse<CourseSet> { State = true, ErrorCode = 0, ErrorMsg = "", Data = new CourseSet() { DataList = list, HasMore = hasMore, IncludePrice = includePrice } }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 课程中心-课程详情
        /// </summary>
        /// <param name="id">课程Id</param>
        /// <returns></returns>
        public ActionResult Detail(int id = 0, int aid = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Select");
            }
            CourseBll bll = new CourseBll();
            int studentId = GetCurrentUser().StudentId;
            DtoSelectCenterCourseDetailResult course = bll.GetCourseDetailForSelectCenter(id, studentId);
            CourseDetailViewModel model = course.ConvertTo<CourseDetailViewModel>();
            if (string.IsNullOrEmpty(model.CourseImage))
            {
                model.CourseImage = defaultCourseImg;
            }
            model.StudentId = studentId;
            model.Ad_Id = aid;
            return View(model);
        }
        /// <summary>
        /// 课程中心-课程详情-后台预览
        /// </summary>
        /// <param name="id">课程Id</param>
        /// <returns></returns>
        public ActionResult PreviewCourse(int id = 0)
        {
            CourseBll bll = new CourseBll();
            int studentId = GetCurrentUser().StudentId;
            DtoSelectCenterCourseDetailResult course = bll.GetCourseDetailForPreview(id, studentId);
            CourseDetailViewModel model = course.ConvertTo<CourseDetailViewModel>();
            model.StudentId = studentId;
            if (string.IsNullOrEmpty(model.CourseImage))
            {
                model.CourseImage = defaultCourseImg;
            }
            return View(model);
        }

        public JsonResult HitMessage(int courseId = 0, int aid = 0)
        {
            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    AdvertisingBll advertisingBll = new AdvertisingBll();
                    advertisingBll.SendHitMessage(aid);
                });
            }
            catch (Exception)
            {
            }
            return new JsonResult() { Data = AjaxResponse.Success() };
        }
    }
}