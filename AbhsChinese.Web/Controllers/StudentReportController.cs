using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Web.Models.Common;
using AbhsChinese.Web.Models.SubjectReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace AbhsChinese.Web.Controllers
{
    public class StudentReportController : BaseController
    {
        public ActionResult StudentReportList(int active=0)
        {
            StudentReportBll studentReportBll = new StudentReportBll();
            StudyReportCount reportCount = new StudyReportCount();
            //课程报告数量            
            reportCount.LessonCount = studentReportBll.StuCourseReportCount(GetCurrentUser().StudentId);
            //任务和练习报告数量
            List<Yw_StudentTask> studentTask = studentReportBll.StuTaskReportCount(GetCurrentUser().StudentId);
            //任务报告数量
            reportCount.TaskCount = studentTask.Where(s => s.Yuk_TaskType == (int)StudyTaskTypeEnum.老师课后任务 || s.Yuk_TaskType==(int)StudyTaskTypeEnum.系统课后任务).Count();
            //练习报告数量
            reportCount.PracticeCount = studentTask.Where(s => s.Yuk_TaskType == (int)StudyTaskTypeEnum.课后练习).Count();
            reportCount.Active = active;
            return View(reportCount);
        }

        public ActionResult GetStudentReport(int pageIndex, int reportType=0)
        {
            StudentReportBll studentReportBll = new StudentReportBll();
            var device = "";
            device = clsCommon.CheckAgent();
            PagingObject paging = new PagingObject();
            paging.PageIndex = pageIndex;
            if (device.Contains("iPad"))
            {
                paging.PageSize = 12;
            }
            else
            {
                paging.PageSize = 12;
            }
            var models = studentReportBll.GetStudentReport(paging, GetCurrentUser().StudentId, reportType);
            return Json(new { Data = models, PageSize = paging.PageSize, TotalRecord = paging.TotalCount }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowTaskReport(int taskId,string path,int source=0,int origin=0)
        {
            StudentReportBll studentReportBll = new StudentReportBll();
            var taskReport = studentReportBll.GetByStudentTask(GetCurrentUser().StudentId, taskId);
            SumReportViewModel viewModels = taskReport.ConvertTo<SumReportViewModel>();

            var courseInfo = studentReportBll.GetStuTaskReportById(taskId);
            viewModels.TaskId = taskId;
            viewModels.StudentName = GetCurrentUser().Name;
            viewModels.ReportType = courseInfo.ReportType;
            viewModels.ReportTypeStr = courseInfo.ReportTypeStr;
            viewModels.CourseName = courseInfo.Ycs_Name;
            viewModels.LessonName = courseInfo.Ycl_Name;
            viewModels.PraticeCount = courseInfo.PraticeCount;
            viewModels.Source = source;
            viewModels.Path = path;
            viewModels.Origin = origin;
            return View(viewModels);
        }

        public ActionResult ShowStudyReport(int lessonProcessId, string path,int source = 0)
        {
            StudentReportBll studentReportBll = new StudentReportBll();
            DtoStudentReport studyReport = studentReportBll.GetByStuLesAnswer(GetCurrentUser().StudentId, lessonProcessId);
            StudentBll studentBll = new StudentBll();
            var stuName = studentBll.GetStudent(GetCurrentUser().StudentId).Bst_Name;
            studyReport.StudyAdvice = string.Format(StudentStudyReport(studyReport.Evaluate), stuName, studyReport.ImporveCount, studyReport.ImporveKnow, studyReport.GoodKnow);
            SumReportViewModel viewModels = studyReport.ConvertTo<SumReportViewModel>();

            var courseInfo = studentReportBll.GetStuCourseReportById(lessonProcessId);

            viewModels.StudentName = GetCurrentUser().Name;
            viewModels.ReportType = courseInfo.ReportType;
            viewModels.ReportTypeStr = courseInfo.ReportTypeStr;
            viewModels.CourseName = courseInfo.Ycs_Name;
            viewModels.LessonName = courseInfo.Ycl_Name;
            viewModels.PraticeCount = courseInfo.PraticeCount;
            viewModels.Source = source;
            viewModels.Path = path;
            return View(viewModels);
        }
        
        /// <summary>
        /// 练习报告的统计部分
        /// </summary>
        /// <returns></returns>
        public ActionResult GetByStudentTask(int taskId)
        {
            StudentReportBll studentReportBll = new StudentReportBll();
            var models = studentReportBll.GetByStudentTask(GetCurrentUser().StudentId, taskId);
            return Json(new SuccessJsonResponse(models), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 练习报告的题目部分
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetSubjectsToPractice(int taskId, int pageIndex)
        {
            StudentReportBll bll = new StudentReportBll();
            var answer = bll.GetReportSubject(GetCurrentUser().StudentId, taskId, pageIndex);

            List<ReportVm> vms = new List<ReportVm>();
            for (int i = 0; i < answer.Item1.Count; i++)
            {
                StudentAnswerBase answerobj = answer.Item1[i];
                if (answer.Item2.ContainsKey(answerobj.SubjectId) && answer.Item3.ContainsKey(answerobj.SubjectId))
                {
                    vms.Add(SubjectReportVmFactory.Create(answer.Item2[answerobj.SubjectId], answer.Item1[i], answer.Item3[answerobj.SubjectId]));
                }
            }
            return Json(new PageJsonResponse<List<ReportVm>>() { Data = vms,PageSize=answer.Item4,TotalCount=answer.Item5 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StudentTaskReport()
        {
            return View();
        }
        
        private string StudentStudyReport(EvaluateEnum evaluate)
        {
            var path = Server.MapPath("/App_Data/");
            XElement xe = XElement.Load($"{path}/StudyAdvice.xml");
            IEnumerable<XElement> elements = from ele in xe.Elements("advice")
                                             select ele;
            List<StudyAdviceModel> adviceList = new List<StudyAdviceModel>();
            var advice = elements.Where(s => s.Attribute("type").Value._ToInt32() == (int)evaluate).OrderBy(d => Guid.NewGuid()).FirstOrDefault();
            return advice.Value;
        }
    }
}