using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response;
using AbhsChinese.Domain.Entity;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public abstract class SubjectController : BaseController
    {
        [HttpPost]
        public ActionResult Approve(ApproveInputModel approve)
        {
            SubjectBll bll = new SubjectBll();
            var subjectEntity = bll.GetSubject(approve.SubjectId);
            Yw_SubjectProcess nextProcess = new Yw_SubjectProcess();
            nextProcess.Ysp_Action = (int)SubjectActionEnum.审批;
            nextProcess.Ysp_CreateTime = DateTime.Now;
            nextProcess.Ysp_Id = 0;
            nextProcess.Ysp_Operator = CurrentUserID;
            nextProcess.Ysp_Remark = "";
            nextProcess.Ysp_Status = (int)approve.NextStatus;
            nextProcess.Ysp_SubjectId = approve.SubjectId;
            subjectEntity.Ysj_Status = nextProcess.Ysp_Status;
            if (approve.Mark != null && approve.Mark.Count > 0)
            {
                string mark = string.Join(",", approve.Mark);
                nextProcess.Ysp_Mark = mark;
            }
            bll.Approve(subjectEntity, nextProcess);
            return Json(new SuccessJsonResponse());
        }

        [HttpGet]
        public ActionResult GetDetails(int id)
        {
            SubjectBll bll = new SubjectBll();
            DtoSubject subject = bll.GetSubjectDetails(id);
            QuestionInputModel derived = ConvertToDerived(subject.Content);
            derived.Difficulty = subject.Difficulty;
            derived.Explain = subject.Content.Ysc_Explain;
            derived.Grade = subject.Grade;
            derived.Id = subject.Id;
            derived.Mark = subject.Mark;
            derived.SubjectStatus = (SubjectStatusEnum)subject.Status;
            derived.Keywords = subject.Keywords;
            derived.Knowledge = subject.MainKnowledge;
            derived.KnowledgeText = subject.MainKnowledgeName;
            derived.Knowledges = string.Join(",", subject.Knowledges);
            derived.KnowledgeTexts = subject.KnowledgeNames;
            derived.PlainName = subject.Name;
            derived.Name = FormatStem(derived.Name);
            var response = new SuccessJsonResponse(derived);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDetails2(int id = 0)
        {
            SubjectBll bll = new SubjectBll();
            DtoSubject subject = bll.GetSubjectDetails2(id);
            QuestionInputModel derived = ConvertToDerived2(subject?.Content);
            if (subject != null)
            {
                derived.Difficulty = subject.Difficulty;
                derived.Explain = subject.Content.Ysc_Explain;
                derived.Grade = subject.Grade;
                derived.Id = subject.Id;
                derived.Mark = subject.Mark;
                derived.SubjectStatus = (SubjectStatusEnum)subject.Status;
                derived.Keywords = subject.Keywords;
                derived.Knowledge = subject.MainKnowledge;
                derived.KnowledgeText = subject.MainKnowledgeName;
                derived.Knowledges = string.Join(",", subject.Knowledges);
                derived.KnowledgeTexts = subject.KnowledgeNames;
                derived.PlainName = subject.Name;
                derived.Name = FormatStem(derived.Name);
            }
            else
            {
                derived.Difficulty = 0;
                derived.Explain = string.Empty;
                derived.Grade = 0;
                derived.Id = 0;
                derived.Mark = new List<string>();
                derived.SubjectStatus = SubjectStatusEnum.编辑中;
                derived.Keywords = string.Empty;
                derived.Knowledge = 0;
                derived.KnowledgeText = string.Empty;
                derived.Knowledges = string.Empty;
                derived.KnowledgeTexts = new List<string>();
                derived.PlainName = string.Empty;
                derived.Name = string.Empty;
            }
            var response = new SuccessJsonResponse(derived);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        protected virtual string FormatStem(string stem)
        {
            return stem;
        }

        protected virtual string FormatReadonlyStem(string stem)
        {
            return stem;
        }

        [HttpGet]
        public ActionResult GetReadonlyDetails(int id)
        {
            SubjectBll bll = new SubjectBll();
            DtoSubject subject = bll.GetSubjectDetails(id);
            QuestionInputModel derived = ConvertToDerived(subject.Content);
            derived.Difficulty = subject.Difficulty;
            derived.Explain = subject.Content.Ysc_Explain;
            derived.Grade = subject.Grade;
            derived.Id = subject.Id;
            derived.Mark = subject.Mark;
            derived.SubjectStatus = (SubjectStatusEnum)subject.Status;
            derived.Keywords = subject.Keywords;
            derived.Knowledge = subject.MainKnowledge;
            derived.KnowledgeText = subject.MainKnowledgeName;
            derived.Knowledges = string.Join(",", subject.Knowledges);
            derived.KnowledgeTexts = subject.KnowledgeNames;
            derived.PlainName = subject.Name;
            derived.Name = FormatReadonlyStem(derived.Name);
            var response = new SuccessJsonResponse(derived);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPartialViewOfSubject(string viewName, string name)
        {
            return PartialView(viewName, name);
        }

        [HttpGet]
        public ActionResult ViewApprovedSubject(int id = 0, int subjectType = 0)
        {
            ViewBag.SubjectType = subjectType;
            return View(id);
        }

        protected abstract QuestionInputModel ConvertToDerived(
            Yw_SubjectContent content);

        protected abstract QuestionInputModel ConvertToDerived2(
            Yw_SubjectContent content);

        protected abstract ContentFetcher GetContentFetcher();

        protected int SaveSubject(QuestionInputModel subject)
        {
            SubjectBll bll = new SubjectBll();

            //题目信息
            Yw_Subject subjectEntity = null;
            Action<Yw_Subject> saveSubjectMethod =
                 SubjectFetcher.Fetch(subject, bll, CurrentUserID, out subjectEntity);

            //题目内容
            Yw_SubjectContent content = null;
            ContentFetcher fetcher = GetContentFetcher();
            Action<Yw_SubjectContent> saveContentMethod = fetcher.Fetch(
                subject,
                bll,
                CurrentUserID,
                out content);

            //要删除的关键词
            IList<int> keywordIdsToDelete = null;
            //要添加的关键词
            IList<Yw_SubjectIndex> keywordsToAdd = KeywordFetcher.Fetch(
                subject,
                bll,
                CurrentUserID,
                out keywordIdsToDelete);

            //主知识点
            Yw_SubjectKnowledge mainKnowledgeEntity = null;
            Action<Yw_SubjectKnowledge> saveKnowledgeMethod = MainKnowledgeFetcher.Fetch(
                    subject,
                    bll,
                    CurrentUserID,
                    out mainKnowledgeEntity);

            //次级知识点
            //需要删除的次级知识点
            IEnumerable<int> idsOfknowledgeToDelete = null;
            //需要添加的次级知识点
            IEnumerable<Yw_SubjectKnowledge> knowledgesToAdd =
                SecondaryKnowledgeFetcher.Fetch(
                    subject,
                    bll,
                    CurrentUserID,
                    out idsOfknowledgeToDelete);

            Yw_SubjectProcess process = null;
            if (subject.Button == FormSubmitButton.Submit)
            {
                process = bll.GetNextProcess(
                    subject.Id,
                    CurrentUserID,
                    subject.NextStatus,
                    SubjectActionEnum.提交);
                subjectEntity.Ysj_Status = process.Ysp_Status;
            }

            return bll.SaveSubject(saveSubjectMethod, subjectEntity,
                saveContentMethod, content,
                saveKnowledgeMethod, mainKnowledgeEntity,
                keywordsToAdd, keywordIdsToDelete,
                knowledgesToAdd, idsOfknowledgeToDelete,
                process, subject.Button);
        }
    }
}