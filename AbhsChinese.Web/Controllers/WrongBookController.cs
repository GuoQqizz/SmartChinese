using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Request.StudentWrong;
using AbhsChinese.Domain.Dto.Response.Student;
using AbhsChinese.Domain.Entity.StudentWrong;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Web.Models.Common;
using AbhsChinese.Web.Models.StuWrong;
using AbhsChinese.Web.Models.SubjectReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Css.Extensions;

namespace AbhsChinese.Web.Controllers
{
    public class WrongBookController : BaseController
    {
        public int StudentId => GetCurrentUser().StudentId;//10000
        // GET: WrongBook
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult History()
        {
            return View();
        }
        /// <summary>
        /// 错题列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WrongBookList(DtoStudentWrongSearch search)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            search.StudentId = GetCurrentUser().StudentId;
            var list = wrongBookBll.GetBookList(search);
            #region 错误传pageinde处理
            if ((list == null || list.Count == 0) && search.Pagination.PageIndex > 1 && search.Pagination.TotalCount > 0)
            {
                search.Pagination.PageIndex = 1;
                list = wrongBookBll.GetBookList(search);
            }
            #endregion
            #region testdata
            //list = new List<DtoStudentWrongBookInfo>();
            //for (int i = 0; i < 2; i++)
            //{
            //    list.Add(new DtoStudentWrongBookInfo()
            //    {
            //        CourseLessonName = "CourseLessonName",
            //        CourseName = "CourseName",
            //        Ywb_Id = 1,
            //        Yws_CreateTime = DateTime.Now,
            //        Yws_Source = i % 4 == 0 ? 1 : i % 4,
            //        Yws_Status = i % 2 == 0 ? 1 : 2,
            //        Yws_RemoveCount = 1,
            //        Yws_WrongCount = 3,
            //        Yws_WrongKnowledgeCount = 1,
            //    });
            //}
            //search.Pagination.TotalCount = 20;
            #endregion

            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);
            return Json(table, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BookDetail(int bookId)
        {

            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            var ids = wrongBookBll.GetSubjectIdsByBookId(StudentId, bookId);
            if (ids == null || ids.Count == 0)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("WrongDetail", new { wrongSubjectId = ids.FirstOrDefault() });
        }
        /// <summary>
        /// 错题详情视图
        /// </summary>
        /// <param name="wrongSubjectId"></param>
        /// <returns></returns>
        public ActionResult WrongDetail(int wrongSubjectId)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            var wrongSubject = wrongBookBll.GetWrongSubject(wrongSubjectId, true);
            if (CheckWrongSubject(wrongSubject))
            {
                //wrongSubject.WrongSubjectIds = wrongBookBll.GetSubjectIdsByBookId(StudentId, wrongSubject.Yws_WrongBookId);
                return View(wrongSubject);
            }
            return RedirectToAction("Index");
            //return View();
        }

        /// <summary>
        /// 错题详情vm
        /// </summary>
        /// <param name="wrongSubjectId"></param>
        /// <returns></returns>
        public ActionResult WrongDetailVm(int wrongSubjectId)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            KnowledgeBll knowledgeBll = new KnowledgeBll();
            Tuple<StudentAnswerBase, Yw_SubjectContent, Yw_Subject, Yw_StudentWrongSubject> wrongSubjectVm = wrongBookBll.GetWorngSubjectVm(GetCurrentUser().StudentId, wrongSubjectId);
            if (wrongSubjectVm == null)
            {
                return SimpleResult(false);
            }
            WrongSubjectVm vm = new WrongSubjectVm();
            if (wrongSubjectVm.Item1.KnowledgeId > 0)
            {
                vm.KnowledgeInfo = knowledgeBll.GetMediaByKnowledgeId(wrongSubjectVm.Item1.KnowledgeId);
            }
            vm.Report = SubjectReportVmFactory.Create(wrongSubjectVm.Item2, wrongSubjectVm.Item1, wrongSubjectVm.Item3);
            return Json(new SuccessJsonResponse(vm), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DoAgain(int wrongSubjectId)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            var wrongSubject = wrongBookBll.GetWrongSubject(wrongSubjectId, false);
            if (CheckWrongSubject(wrongSubject))
            {
                return View(wrongSubject);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ClearWrong(int wrongSubjectId)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            var wrongSubject = wrongBookBll.GetWrongSubject(wrongSubjectId, true);
            if (CheckWrongSubject(wrongSubject))
            {
                return View(wrongSubject);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ClearWrong(int bookId, int wrongSubjectId, StudyWrongStatusEnum toStatus)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            var res = wrongBookBll.ClearWrongSubject(bookId, wrongSubjectId, toStatus);
            return SimpleResult(res);
        }
        /// <summary>
        /// 获取题目详情
        /// </summary>
        /// <param name="wrongSubjectId"></param>
        /// <returns></returns>
        public ActionResult SubjectDetail(int wrongSubjectId)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            SubjectBll subjectBll = new SubjectBll();

            var wrongSubject = wrongBookBll.GetWrongSubject(wrongSubjectId);
            if (CheckWrongSubject(wrongSubject))
            {
                var subjectContent = subjectBll.GetCompleteContentsOfSubject(new List<int> { wrongSubject.Yws_WrongSubjectId }).FirstOrDefault();
                return Json(new SuccessJsonResponse(subjectContent), JsonRequestBehavior.AllowGet);
            }
            return SimpleResult(false);
        }
        /// <summary>
        /// 获取关联题目详情
        /// </summary>
        /// <param name="wrongSubjectId"></param>
        /// <returns></returns>
        public ActionResult SubjectRelation(int wrongSubjectId)
        {
            StudentWrongBookBll wrongBookBll = new StudentWrongBookBll();
            SubjectBll subjectBll = new SubjectBll();
            var wrongSubject = wrongBookBll.GetWrongSubject(wrongSubjectId);
            if (CheckWrongSubject(wrongSubject))
            {
                var subjectList = subjectBll.GetSubjectForWrongClear(wrongSubject.Yws_WrongSubjectId);
                var contentList = subjectBll.GetCompleteContentsOfSubject(subjectList.Select(s => s.Ysj_Id).ToList());
                contentList.ForEach(s =>
                {
                    s.KnowledgeId = subjectList.Where(sub => sub.Ysj_Id == s.SubjectId).Select(sub => sub.Ysj_MainKnowledgeId).FirstOrDefault();
                });
             //   contentList = contentList.Take(1).ToList();
                return Json(
               new AbhsTableJsonResponse(contentList, contentList.Count),
               JsonRequestBehavior.AllowGet);
            }
            return SimpleResult(false);
        }
        /// <summary>
        /// 清除错题
        /// </summary>
        /// <param name="bookId">错题本Id</param>
        /// <param name="wrongSubjectId">错题本详情id</param>
        /// <param name="toStatus" cref="StudyWrongStatusEnum">清除状态</param>
        /// <returns></returns>


        private bool CheckWrongSubject(DtoStudentWrongSubjectInfo model)
        {
            if (model == null || model.Yws_StudentId != StudentId)
            {
                return false;
            }
            return true;
        }
    }
}