using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class MatchController : SubjectController
    {
        [HttpGet]
        public ActionResult EditMatchSubject(int id = 0)
        {
            return View(id);
        }

        [HttpPost]
        [ActionName("EditMatchSubject")]
        [ValidateInput(false)]
        public ActionResult EditMatchSubjectConfirm(
            Match subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        [HttpGet]
        public ActionResult ViewMatchSubject(int id = 0)
        {
            return View(id);
        }

        /// <summary>
        /// 获取连线题详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult GetQuestionInfo(int id)
        {
            return PartialView(id);
        }

        protected override QuestionInputModel ConvertToDerived(
            Yw_SubjectContent content)
        {
            var inputModel = new Match();
            var c = content as Yw_MatchContent;
            inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
            inputModel.TitleOptionContentType = (UeditorType)c.Ysc_Content_Obj.LeftOptionContentType;
            inputModel.AnswertOptionContentType = (UeditorType)c.Ysc_Content_Obj.RightOptionContentType;
            inputModel.Title = c.Ysc_Content_Obj.LeftOptions.Select(s =>
                UeditorContentFactory.RestoreContent(s.Text, inputModel.TitleOptionContentType)).ToList();
            inputModel.LinedAnswers = c.Ysc_Answer_Obj.Answers.Select(s => s[1]).ToList();
            inputModel.Answer = c.Ysc_Content_Obj.RightOptions.Select(s =>
                UeditorContentFactory.RestoreContent(s.Text, inputModel.AnswertOptionContentType)).ToList();
            inputModel.Name = UeditorContentFactory.RestoreContent(
                c.Ysc_Content_Obj.Stem,
                inputModel.StemType);
            return inputModel;
        }

        protected override ContentFetcher GetContentFetcher()
        {
            return new MatchContentFetcher();
        }

        protected override QuestionInputModel ConvertToDerived2(Yw_SubjectContent content)
        {
            throw new NotImplementedException();
        }
    }
}