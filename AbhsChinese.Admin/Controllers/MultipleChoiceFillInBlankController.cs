using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class MultipleChoiceFillInBlankController : SubjectController
    {
        [HttpGet]
        public ActionResult EditMultipleChoiceFillInBlankSubject(int id = 0)
        {
            return View(id);
        }

        [HttpPost]
        [ActionName("EditMultipleChoiceFillInBlankSubject")]
        [ValidateInput(false)]
        public ActionResult EditMultipleChoiceFillInBlankSubjectConfirm(
            MultipleChoiceFillInBlank subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        [HttpGet]
        public ActionResult ViewMultipleChoiceFillInBlankSubject(int id = 0)
        {
            return View(id);
        }

        /// <summary>
        /// 获取选择填空题详情
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
            var inputModel = new MultipleChoiceFillInBlank();
            var c = content as MultipleChoiceFillInBlankContent;
            inputModel.AnswerIndexes = new List<int>();
            UeditorType type = (UeditorType)c.Ysc_Content_Obj.ContentType;
            inputModel.Options = c.Ysc_Content_Obj.SubjectOptions.Select(o =>
                UeditorContentFactory.RestoreContent(o.Text, type)).ToList();
            inputModel.Goptions = c.Ysc_Content_Obj.SubjectGOptions.Select(o =>
                UeditorContentFactory.RestoreContent(o.Text, type)).ToList();
            inputModel.Name = c.Ysc_Content_Obj.Stem;
            inputModel.ContentType = (UeditorType)c.Ysc_Content_Obj.ContentType;
            //inputModel.GPositions = new List<int>();
            //foreach (var item in c.Ysc_Content_Obj.GPositions)
            //{
            //    inputModel.GPositions.Add(item.Key);
            //}
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
            return new MultipleChoiceFillInBlankContentFetcher();
        }

        protected override QuestionInputModel ConvertToDerived2(Yw_SubjectContent content)
        {
            throw new NotImplementedException();
        }
    }
}