using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Dto.Request.Subject;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.AbhsException;

namespace AbhsChinese.Admin.Controllers
{
    public class QuestionController : BaseController
    {

        [HttpGet]
        public ActionResult GetGroupedSubjects(DtoQuestionSearch search)
        {
            var subjectGroup = new SubjectGroupBll().GetBySubjectId(search.Id);
            IList<SubjectViewModel> list = new List<SubjectViewModel>();
            if (subjectGroup != null
                && !string.IsNullOrWhiteSpace(subjectGroup.Ysg_RelSubjectId))
            {
                var ids = subjectGroup.Ysg_RelSubjectId.Split(new char[] { ',' },
                     StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s));

                SubjectBll bll = new SubjectBll();
                var subjects = bll.GetSubjectsByIds(ids);
                if (subjects != null && subjects.Count > 0)
                {
                    list = subjects.Select(s => SubjectViewModel.Create(s)).ToList();
                }
            }

            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);

            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CancelRelation(int subjectId, int relationSubjectId)
        {
            JsonSimpleResponse response = null;
            try
            {
                new SubjectGroupBll().CancelRelation(subjectId, relationSubjectId);
                response = new SuccessJsonResponse();
            }
            catch (AbhsException ex)
            {
                response = new JsonSimpleResponse();
                response.State = false;
                response.ErrorCode = ex.ErrorCode;
                response.ErrorMsg = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddRelation(int subjectId, int relationSubjectId)
        {
            JsonSimpleResponse response = null;
            try
            {
                new SubjectGroupBll().AddRelation(subjectId, relationSubjectId, CurrentUserID);
                response = new SuccessJsonResponse();
            }
            catch (AbhsException ex)
            {
                response = new JsonSimpleResponse();
                response.State = false;
                response.ErrorCode = ex.ErrorCode;
                response.ErrorMsg = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddQuestion(int id, SubjectTypeEnum subjectType)
        {
            subjectType = SubjectTypeEnum.判断题;
            QuestionInputModel question = null;

            return View(subjectType.ToString(), question);
        }

        [HttpGet]
        public JsonResult GetMyQuestions(DtoQuestionSearch search)
        {
            //if (!search.SubjectStatus.HasValue || (int)search.SubjectStatus.Value == 0)
            //{
            //    search.SubjectStatus= SubjectStatusEnum.
            //}
            SubjectBll bll = new SubjectBll();
            IList<Yw_Subject> subjects = bll.GetSubjects(search);

            IEnumerable<SubjectViewModel> list =
                subjects.Select(s => SubjectViewModel.Create(s));
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);

            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuestions(DtoQuestionSearch search)
        {
            //此处只需要合格状态的数据
            search.SubjectStatus = SubjectStatusEnum.合格;
            SubjectBll bll = new SubjectBll();
            IList<Yw_Subject> subjects = bll.GetSubjects(search);

            IEnumerable<SubjectViewModel> list =
                subjects.Select(s => SubjectViewModel.Create(s));
            var table = AbhsTableFactory.Create(list, search.Pagination.TotalCount);

            return Json(table, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ListApprovedQuestions()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListQuestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReEdit(int subjectId)
        {
            SubjectBll bll = new SubjectBll();
            Yw_SubjectProcess process = bll.GetNextProcess(
                subjectId,
                CurrentUserID,
                SubjectStatusEnum.编辑中,
                SubjectActionEnum.重新编辑);
            bll.ReEdit(process);

            return Json(new SuccessJsonResponse());
        }
    }
}