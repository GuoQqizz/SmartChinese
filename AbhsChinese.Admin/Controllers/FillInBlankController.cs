using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class FillInBlankController : SubjectController
    {
        [HttpGet]
        public ActionResult EditFillInBlankSubject(int id = 0)
        {
            return View(id);
        }

        [HttpPost]
        [ActionName("EditFillInBlankSubject")]
        [ValidateInput(false)]
        public ActionResult EditFillInBlankSubjectConfirm(FillInBlank subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        [HttpGet]
        public ActionResult ViewFillInBlankSubject(int id = 0)
        {
            return View(id);
        }

        /// <summary>
        /// 获取填空题详情
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
            var inputModel = new FillInBlank();
            var c = content as Yw_FillInBlankContent;
            inputModel.Perfect.Blanks = c.Ysc_Answer_Obj.Perfect;
            inputModel.Correct.Blanks = c.Ysc_Answer_Obj.Correct;
            inputModel.Other.Blanks = c.Ysc_Answer_Obj.Other;
            inputModel.Name = c.Ysc_Content_Obj.Stem;
            return inputModel;
        }

        protected override string FormatStem(string stem)
        {
            return UeditorContentFactory.RestoreBlank(stem);
        }

        protected override string FormatReadonlyStem(string stem)
        {
            return UeditorContentFactory.FormatReadonlyStem(stem);
        }

        protected override ContentFetcher GetContentFetcher()
        {
            return new FillInBlankContentFetcher();
        }

        protected override QuestionInputModel ConvertToDerived2(Yw_SubjectContent content)
        {
            throw new NotImplementedException();
        }
    }
}