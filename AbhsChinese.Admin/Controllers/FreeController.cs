using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class FreeController : SubjectController
    {
        [HttpGet]
        public ActionResult EditFreeSubject(int id = 0)
        {
            return View(id);
        }

        [HttpPost]
        [ActionName("EditFreeSubject")]
        [ValidateInput(false)]
        public ActionResult EditFreeSubjectConfirm(
            Free subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        [HttpGet]
        public ActionResult ViewFreeSubject(int id = 0)
        {
            return View(id);
        }

        /// <summary>
        /// 获取主观题详情
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
            var inputModel = new Free();
            var c = content as Yw_SubjectFreeContent;
            inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
            inputModel.Name = UeditorContentFactory.RestoreContent(
                c.Ysc_Content_Obj.Stem,
                inputModel.StemType);
            inputModel.Answer = c.Ysc_Answer_Obj.Answer;
            inputModel.ScoreRules = c.Ysc_Content_Obj.ScoreRules;
            return inputModel;
        }

        protected override ContentFetcher GetContentFetcher()
        {
            return new FreeContentFetcher();
        }

        protected override QuestionInputModel ConvertToDerived2(Yw_SubjectContent content)
        {
            throw new NotImplementedException();
        }
    }
}