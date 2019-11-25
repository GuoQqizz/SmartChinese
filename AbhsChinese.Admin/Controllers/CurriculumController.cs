using Abhs.Service.Client;
using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Course;
using AbhsChinese.Admin.Models.CurriculumSetViewModel;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    /// <summary>
    /// 课程控制器
    /// </summary>
    public class CurriculumController : BaseController
    {
        #region 管理
        /// <summary>
        /// 课程管理视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            return View();
        }

        /// <summary>
        /// 课程详情
        /// </summary>
        /// <param name="curriculumID"></param>
        /// <returns></returns>
        public ActionResult Info(string curriculumID)
        {
            return View();
        }

        #endregion

        #region 审批

        /// <summary>
        /// 课时审批列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Approve()
        {
            return View();
        }

        /// <summary>
        /// 课程审批
        /// </summary>
        /// <param name="curriculumID">课时id</param>
        /// <returns></returns>
        public ActionResult ChapterApprove(int id = 0)
        {
            LessonBll bll = new LessonBll();
            var data = bll.Select(id);
            if (data != null && data.Status == (int)LessonStatusEnum.待审批)
            {
                bll.UpdateLessonStatus(data.ID, LessonStatusEnum.审批中, base.CurrentUserID);
            }
            return View(id);
        }

        #endregion

        #region 制作
        /// <summary>
        /// 课程制作列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Make()
        {
            return View();
        }
        /// <summary>
        /// 课程制作详情视图动作
        /// </summary>
        /// <returns></returns>
        public ActionResult MakeInfo(int id = 0)
        {
            CourseBll bll = new CourseBll();
            var model = bll.GetDetails(id);

            var vm = model.ConvertTo<CurriculumDetailsViewModel>(
                PropertyNamePrefixAction.Remove);
            vm.CoverImage = UeditorContentFactory.RestoreUrl(
                vm.CoverImage, UeditorType.Image);
            if (model != null)
            {
                return View(vm);
            }
            else
            {
                return View(new CurriculumDetailsViewModel());
            }
        }
        /// <summary>
        /// 制作课时视图
        /// </summary>
        /// <param name="chapterID">课时id</param>
        /// <returns></returns>
        public ActionResult MakeChapter(int id = 0)
        {
            LessonBll bll = new LessonBll();
            var data = bll.Select(id);
            if (data != null && data.Status == (int)LessonStatusEnum.未编辑)
            {
                bll.UpdateLessonStatus(data.ID, LessonStatusEnum.制作中, base.CurrentUserID);
            }
            return View(id);
        }
        #endregion

        #region 课程课时相关查询接口
        /// <summary>
        /// 查询课程数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCurriculums(DtoCurriculumSearch search)
        {
            CourseBll bll = new CourseBll();
            IList<DtoCourseListItem> entities = bll.GetCourses(search);

            PropertyNamePrefixAction action = PropertyNamePrefixAction.Remove;
            IEnumerable<CourseViewModel> list =
                entities.Select(s => s.ConvertTo<CourseViewModel>(action));
            return Json(AbhsTableFactory.Create(list, search.Pagination.TotalCount));
        }
        /// <summary>
        /// 查询审批课时数据
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetApproveLessons(DtoApproveLessonSearch search)
        {
            LessonBll bll = new LessonBll();
            List<DtoLessonApprove> list = bll.GetLessonApproveByPage(search);
            return Json(AbhsTableFactory.Create(list, search.Pagination.TotalCount));
        }
        /// <summary>
        /// 查询课程课时日志
        /// </summary>
        /// <param name="courseid"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetLessonLog(int courseid, DtoSearch search)
        {
            LessonBll bll = new LessonBll();
            var list = bll.GetLessonLogByPage(courseid, search);
            return Json(AbhsTableFactory.Create(list, search.Pagination.TotalCount));

        }
        #endregion

        #region 审批&制作视图需要接口
        /// <summary>
        /// 根据课时id获取数据
        /// </summary>
        /// <param name="chapterid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetChapter(int chapterid)
        {
            LessonBll lbll = new LessonBll();

            var lesson = lbll.Select(chapterid);
            if (lesson != null)
            {
                LessonUnitBll bll = new LessonUnitBll();
                ChapterSet set = new ChapterSet();
                set.courseId = lesson.CourseId;
                set.id = lesson.ID;
                set.name = lesson.Name;
                set.setTime = lesson.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                set.pages = new List<Page>();
                set.status = lesson.Status;
                set.processid = lesson.ProcessId;
                var units = bll.SelectUnitsByLesson(set.id, set.processid);
                if (units != null)
                {
                    set.pages = units.Select(u => new Page
                    {
                        courseId = u.CourseId,
                        lessonId = u.LessonId,
                        id = u.Id,
                        name = u.Name,
                        pageNum = u.Index,
                        thumbnail = string.IsNullOrEmpty(u.Screenshot) ? "" : (ConfigurationManager.AppSettings["OssHostUrl"] + u.Screenshot),
                        approveType = u.ApproveStatus
                    }).ToList();
                }
                return Json(new JsonResponse<ChapterSet> { State = true, ErrorCode = 0, ErrorMsg = "", Data = set });
            }
            else
            {
                return Json(new JsonResponse<ChapterSet> { State = false, ErrorCode = 0, ErrorMsg = "没有此数据", Data = null });
            }
        }
        /// <summary>
        /// 获取讲义数据
        /// </summary>
        /// <param name="pageid">审批id</param>
        /// <param name="processid">审批编号</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetPage(int pageid, int processid)
        {
            LessonUnitBll bll = new LessonUnitBll();
            var unit = bll.SelectUnit(pageid, processid);
            var page = new Page();
            page.courseId = unit.CourseId;
            page.lessonId = unit.LessonId;
            page.id = unit.Id;
            page.name = unit.Name;
            page.pageNum = unit.Index;
            ////定义媒体资源id数组和文本资源id数组
            List<int> mediaids = new List<int>(), textids = new List<int>();
            page.steps = unit.Steps.Select(s => new Models.CurriculumSetViewModel.Step
            {
                id = s.id,
                stepNum = s.StepNum,
                actions = s.Actions.Select(a => ActionTranslator.DataToViewData(a, mediaids, textids)).ToList()
            }).ToList();
            //如果媒体id或文本id有值的话
            if (mediaids.Count > 0 || textids.Count > 0)
            {
                ResourceBll rbll = new ResourceBll();
                var medias = rbll.GetMediaObjectByIdList(mediaids.Distinct().ToList());//获取所有的媒体对象字典
                var texts = rbll.GetTextObjectByIdList(textids.Distinct().ToList());//获取文本对象字典
                page.steps.ForEach((s) =>
                {
                    s.actions.ForEach((a) =>
                    {
                        ActionTranslator.SetViewDataMedia(a, medias, texts);//设置动作的媒体属性
                    });//遍历每一个动作
                });//遍历每一个步骤
            }
            page.approveType = unit.ApproveStatus;
            page.approve = unit.Approve;
            return Json(new JsonResponse<Page> { State = true, ErrorCode = 0, ErrorMsg = "", Data = page });
        }
        /// <summary>
        /// 添加讲义页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddPage(Page page)
        {
            LessonUnitBll bll = new LessonUnitBll();
            //修改数据
            int id = bll.Add(new DtoLessonUnit
            {
                CourseId = page.courseId,
                LessonId = page.lessonId,
                Name = "",
                Screenshot = "",
                Steps = null,
                Index = page.pageNum,
            });
            return Json(new JsonResponse<int> { State = true, ErrorCode = 0, ErrorMsg = "", Data = id });
        }
        /// <summary>
        /// 设置讲义数据
        /// </summary>
        /// <param name="pagestr"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]//不做html验证
        public JsonResult SetPage(string pagestr)
        {
            LessonUnitBll bll = new LessonUnitBll();
            Page pageInfo = new Page();
            #region 解析数据
            JObject obj = JsonConvert.DeserializeObject(pagestr) as JObject;
            string base64Str = obj["thumbnail"].ToString().Split(',')[1];
            pageInfo.id = Convert.ToInt32(obj["id"]);
            pageInfo.pageNum = Convert.ToInt32(obj["pageNum"]);
            pageInfo.name = obj["name"].ToString();

            string thumbnailPath = "UnitThumbnail";//文件路径
            string thumbnailFileName = $"unit{pageInfo.id}.png";//文件名称

            new Thread(() =>
            {
                byte[] arr = Convert.FromBase64String(base64Str);
                MemoryStream ms = new MemoryStream(arr);
                FileManageClient client = new FileManageClient(ConfigurationManager.AppSettings["uploadUrl"]);
                var response = client.UploadCoverage(thumbnailFileName, ms, ConfigurationManager.AppSettings["OssSubject"], thumbnailPath, true);
            }).Start();//开启线程上传缩略图片

            pageInfo.thumbnail = $"/{thumbnailPath}/{thumbnailFileName}";//拼接存储路径
            pageInfo.steps = new List<Models.CurriculumSetViewModel.Step>();

            JArray steps = JsonConvert.DeserializeObject(obj["steps"].ToString()) as JArray;
            foreach (var s in steps)
            {
                Models.CurriculumSetViewModel.Step step = new Models.CurriculumSetViewModel.Step();
                step.stepNum = Convert.ToInt32(s["stepNum"]);
                step.actions = new List<ActionBase>();

                JArray actions = JsonConvert.DeserializeObject(s["actions"].ToString()) as JArray;
                foreach (var a in actions)
                {
                    step.actions.Add(ActionTranslator.JsonToViewData(a.ToString()));
                }
                pageInfo.steps.Add(step);
            }

            #endregion 解析数据
            int coins = 0;
            var stepsData = pageInfo.steps.Select(s => new Domain.JsonEntity.UnitStep.Step()
            {
                id = s.id,
                StepNum = s.stepNum,
                Actions = s.actions.Select(a => ActionTranslator.ViewDataToData(a, ref coins)).ToList()
            }).ToList();
            //修改数据
            bll.Update(new DtoLessonUnit
            {
                Id = pageInfo.id,
                Name = pageInfo.name,
                Screenshot = pageInfo.thumbnail,
                Status = 0,
                Index = pageInfo.pageNum,
                Steps = stepsData,
                Coins = coins
            });
            return Json(new JsonResponse<string> { State = true, ErrorCode = 0, ErrorMsg = "", Data = "" });
        }

        /// <summary>
        /// 返回浏览路径
        /// </summary>
        /// <param name="pagestr"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]//不做html验证
        public JsonResult ShowPage(string pagestr)
        {
            LessonUnitBll bll = new LessonUnitBll();
            Page pageInfo = new Page();
            JObject obj = JsonConvert.DeserializeObject(pagestr) as JObject;
            pageInfo.id = Convert.ToInt32(obj["id"]);
            pageInfo.pageNum = Convert.ToInt32(obj["pageNum"]);
            pageInfo.name = obj["name"].ToString();
            pageInfo.steps = new List<Models.CurriculumSetViewModel.Step>();
            JArray steps = JsonConvert.DeserializeObject(obj["steps"].ToString()) as JArray;
            foreach (var s in steps)
            {
                Models.CurriculumSetViewModel.Step step = new Models.CurriculumSetViewModel.Step();
                step.stepNum = Convert.ToInt32(s["stepNum"]);
                step.actions = new List<ActionBase>();
                JArray actions = JsonConvert.DeserializeObject(s["actions"].ToString()) as JArray;
                foreach (var a in actions)
                {
                    step.actions.Add(ActionTranslator.JsonToViewData(a.ToString()));
                }
                pageInfo.steps.Add(step);
            }

            int coins = 0;
            var stepsData = pageInfo.steps.Select(s => new Domain.JsonEntity.UnitStep.Step()
            {
                id = s.id,
                StepNum = s.stepNum,
                Actions = s.actions.Select(a => ActionTranslator.ViewDataToData(a, ref coins)).ToList()
            }).ToList();
            //保存数据到Radis中
            bll.SaveUnitToRadis(new DtoLessonUnit
            {
                Id = pageInfo.id,
                Index = pageInfo.pageNum,
                Coins = coins,
                Steps = stepsData,
                Status = 0,
            });
            string key = Encrypt.EncryptQueryString($"{new Random().Next(0, 10000)}_{pageInfo.id}_{0}_{0}");
            return Json(new JsonResponse<string> { State = true, ErrorCode = 0, ErrorMsg = "", Data = $"{ConfigurationManager.AppSettings["StudentClientDomain"]}LearningCenter/LessonApprove?ApproveKey={key}" });
        }
        /// <summary>
        /// 移动页码
        /// </summary>
        /// <param name="unitid"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MovePage(int unitid, int index)
        {
            LessonUnitBll bll = new LessonUnitBll();
            bll.MoveUnit(unitid, index);
            return Json(new JsonResponse<string> { State = true, ErrorCode = 0, ErrorMsg = "", Data = "" });
        }
        /// <summary>
        /// 删除页码
        /// </summary>
        /// <param name="pageid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeletePage(int pageid)
        {
            LessonUnitBll bll = new LessonUnitBll();
            bll.Delete(pageid);
            return Json(new JsonResponse<string> { State = true, ErrorCode = 0, ErrorMsg = "", Data = "" });
        }
        /// <summary>
        /// 更新课时状态状态
        /// </summary>
        /// <param name="chapterid">课时id</param>
        /// <param name="status">要修改的状态</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetChapterStatus(int chapterid, LessonStatusEnum status)
        {
            LessonBll bll = new LessonBll();
            int processId = bll.UpdateLessonStatus(chapterid, status, base.CurrentUserID);
            return Json(new JsonResponse<int> { State = processId != 0, ErrorCode = 0, ErrorMsg = "", Data = processId });

        }
        /// <summary>
        /// 设置单元审批信息
        /// </summary>
        /// <param name="courseId">课程id</param>
        /// <param name="lessonId">课时id</param>
        /// <param name="processId">审批id</param>
        /// <param name="unitId">单元id</param>
        /// <param name="status">状态</param>
        /// <param name="remark">审批内容</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetUnitProcess(int courseId, int lessonId, int processId, int unitId, int status, string remark)
        {
            LessonUnitBll bll = new LessonUnitBll();
            int i = bll.SetUnitApprove(new Yw_CourseLessonUnitProcess()
            {
                Yup_CourseId = courseId,
                Yup_LessonId = lessonId,
                Yup_LessonProcessId = processId,
                Yup_UnitId = unitId,
                Yup_Status = status,
                Yup_Remark = remark,
                Yup_Operator = base.CurrentUserID

            });
            return Json(new JsonResponse<int> { State = true, ErrorCode = 0, ErrorMsg = "", Data = i });
        }
        #endregion
    }
}