using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Code.Extension;
using AbhsChinese.Domain.AbhsException;
using AbhsChinese.Domain.Dto.Request.StudyTask;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Web.Models.Common;
using AbhsChinese.Web.Models.StudyTask;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AbhsChinese.Web.Controllers
{
    public class StudyTaskController : BaseController
    {
        /// <summary>
        /// 管理后台获取驰声数据校验方法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetSeverSig(string str, string key)
        {
            if (Encrypt.MD5Hash(str, "jzxd") == key)
            {
                return GetChiShengSig();
            }
            else
            {
                throw new Exception("GetSeverSig校验失败");
            }
        }

        /// <summary>
        /// 学生学习界面返回驰声数据校验方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSig()
        {
            return GetChiShengSig();
        }

        [HttpGet]
        public ActionResult GetStudyTasks(DtoStudyTaskSearch search)
        {
            search.StudentId = GetCurrentUser().StudentId;
            search.TaskType = StudyTaskTypeEnum.系统课后任务;
            StudentPracticeBll bll = new StudentPracticeBll();
            var entities = bll.GetStudyTasks(search);
            var list = entities.ConvertTo<IList<StudyTaskListViewModel>>();
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 练习课后任务的练习题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSubjectsToPractice(int studyTaskId, int pageIndex)
        {
            StudentPracticeBll bll = new StudentPracticeBll();
            int totalCount = 0;
            var result = bll.GetTaskSubject(studyTaskId, pageIndex, out totalCount);

            SubjectBll subjectBll = new SubjectBll();
            var subjectIds = result.Select(s => s.SubjectId);
            var subjects = subjectBll.GetSubjectsByIds(subjectIds);

            foreach (var item in result)
            {
                //为vm的MainKnowledgeId属性赋值
                var subject = subjects.FirstOrDefault(s => s.Ysj_Id == item.SubjectId);
                item.KnowledgeId = subject.Ysj_MainKnowledgeId;
            }

            return Json(
                new AbhsTableJsonResponse(result, totalCount),
                JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetTotalRecordByStatus(DtoStudyTaskSearch search)
        {
            search.StudentId = GetCurrentUser().StudentId;
            search.TaskType = StudyTaskTypeEnum.系统课后任务;
            StudentPracticeBll bll = new StudentPracticeBll();
            var entities = bll.GetTotalRecordByStatus(search);
            return Json(new SuccessJsonResponse(entities), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ListStudyTasks(DtoStudyTaskSearch historySearch)
        {
            string condition = string.Empty;
            if (historySearch.TaskStatus.HasValue())
            {
                condition = JsonConvert.SerializeObject(historySearch);
            }
            ViewBag.HistorySearch = condition;
            return View();
        }

        /// <summary>
        /// 练习课后任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Practice(
            int studyTaskId,
            int studentTaskId,
            StudyTaskTypeEnum taskType,
            string path)
        {
            ViewBag.TaskType = taskType;
            ViewBag.StudentTaskId = studentTaskId;
            ViewBag.Path = path;
            var bll = new StudentPracticeBll();
            string pageTitle = string.Empty;
            DtoStudyTaskListItem item = bll.GetCourseLessonNameByStudyTask(studentTaskId);
            if (taskType == StudyTaskTypeEnum.课后练习)
            {
                pageTitle = $"{item.CourseName}课后练习";
            }
            else
            {
                pageTitle = $"{item.CourseName}-第{item.LessonIndex}课时课后任务";
            }
            ViewBag.PageTitle = pageTitle;

            return View(studyTaskId);
        }

        /// <summary>
        /// 练习题目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PracticeSubjects(
            int courseId,
            int returnCount,
            string path)
        {
            StudentPracticeBll bll = new StudentPracticeBll();
            Yw_StudentTask task = bll.GeneratePractice(GetCurrentUser().StudentId, courseId, returnCount);

            return RedirectToAction(
                nameof(Practice),
                new
                {
                    StudyTaskId = task.Yuk_TaskId,
                    StudentTaskId = task.Yuk_Id,
                    TaskType = (int)StudyTaskTypeEnum.课后练习,
                    Path = path
                });
        }

        /// <summary>
        /// 提交当页数据
        /// </summary>
        /// <param name="dataStr"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SavePracticeResult(string dataStr, StudyTaskTypeEnum taskType)
        {
            try
            {
                JObject data = JsonConvert.DeserializeObject(dataStr) as JObject;

                int studyTaskId = data["id"].Value<int>();
                int useTime = data["useTime"].Value<int>();
                List<StudentAnswerBase> answers = new List<StudentAnswerBase>();

                JArray jAnswers = data["answers"] as JArray;
                foreach (var jAnswer in jAnswers)
                {
                    var answer = SubjectAnswerObjectBuilder.TranslateAnswer(jAnswer.ToString());
                    answers.Add(answer);
                }

                StudentPracticeBll bll = new StudentPracticeBll();
                bll.SaveResult(
                    GetCurrentUser().StudentId,
                    studyTaskId,
                    useTime,
                    answers,
                    taskType);
                return Json(new SuccessJsonResponse());
            }
            catch (AbhsException ex)
            {
                if (ex.ErrorCode == (int)ErrorCodeEnum.StudentTaskStatusInvalid)
                {
                    JsonSimpleResponse res = new JsonSimpleResponse();
                    res.State = false;
                    res.ErrorMsg = "你已经完成该练习";
                    return Json(res);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 返回驰声校验json数据
        /// </summary>
        /// <returns></returns>
        private JsonResult GetChiShengSig()
        {
            string appKey = "155891795800000f";
            string secretKey = "ef59bf9e88da71ec7c341a50a2e723dd";
            string timestamp = DateTime.Now.ToTimestamp();

            var buffer = Encoding.UTF8.GetBytes(appKey + timestamp + secretKey);
            var data = SHA1.Create().ComputeHash(buffer);

            var sb = new StringBuilder();
            foreach (var t in data)
            {
                sb.Append(t.ToString("x2"));
            }
            var result = sb.ToString();

            return Json(new
            {
                timestamp = timestamp,
                sig = result
            });
        }
    }
}