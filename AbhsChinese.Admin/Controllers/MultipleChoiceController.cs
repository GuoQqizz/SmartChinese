using AbhsChinese.Admin.Models.Common;
using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AbhsChinese.Admin.Controllers
{
    public class MultipleChoiceController : SubjectController
    {
        [HttpGet]
        public ActionResult EditMultipleChoiceSubject(int id = 0)
        {
            return View("EditMultipleChoiceSubject", id);
        }

        [HttpGet]
        public ActionResult EditMultipleChoiceSubject2(int id = 0)
        {
            return View("EditMultipleChoiceSubject2", id);
        }

        [HttpPost]
        [ActionName("EditMultipleChoiceSubject")]
        [ValidateInput(false)]
        public ActionResult EditMultipleChoiceSubjectConfirm(MultipleChoice subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        /// <summary>
        /// 获取选择题详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult GetQuestionInfo(int id)
        {
            return PartialView(id);
        }

        [HttpGet]
        public ActionResult ViewMultipleChoiceSubject(int id = 0)
        {
            return View(id);
        }

        protected override QuestionInputModel ConvertToDerived(
            Yw_SubjectContent content)
        {
            var inputModel = new MultipleChoice();
            var c = content as Yw_SubjectSelectContent;
            inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
            inputModel.ContentType = (UeditorType)c.Ysc_Content_Obj.ContentType;
            inputModel.Display = c.Ysc_Content_Obj.Display;
            UeditorType contentType = (UeditorType)c.Ysc_Content_Obj.ContentType;
            inputModel.Options = c.Ysc_Content_Obj.Options
                .Select(o => UeditorContentFactory.RestoreContent(o.Text, contentType)).ToList();
            inputModel.Random = c.Ysc_Content_Obj.Random;
            UeditorType t = (UeditorType)c.Ysc_Content_Obj.StemType;
            string name = UeditorContentFactory.RestoreContent(c.Ysc_Content_Obj.Stem, t);
            inputModel.Name = name;
            inputModel.Answers = c.Ysc_Answer_Obj.Answers;
            return inputModel;
        }

        protected override QuestionInputModel ConvertToDerived2(
            Yw_SubjectContent content)
        {
            var inputModel = new MultipleChoice();
            if (content != null)
            {
                var c = content as Yw_SubjectSelectContent;
                inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
                inputModel.ContentType = (UeditorType)c.Ysc_Content_Obj.ContentType;
                inputModel.Display = c.Ysc_Content_Obj.Display;
                UeditorType contentType = (UeditorType)c.Ysc_Content_Obj.ContentType;
                inputModel.Options = c.Ysc_Content_Obj.Options
                    .Select(o => UeditorContentFactory.RestoreContent(o.Text, contentType)).ToList();
                inputModel.Random = c.Ysc_Content_Obj.Random;
                UeditorType t = (UeditorType)c.Ysc_Content_Obj.StemType;
                string name = UeditorContentFactory.RestoreContent(c.Ysc_Content_Obj.Stem, t);
                inputModel.Name = name;
                inputModel.Answers = c.Ysc_Answer_Obj.Answers;
            }
            else
            {
                inputModel.StemType = UeditorType.Text;
                inputModel.ContentType = UeditorType.Text;
                inputModel.Display = 1;
                inputModel.Options = new List<string>();
                inputModel.Random = 1;
                inputModel.Name = string.Empty;
                inputModel.Answers = new List<int>();
            }

            return inputModel;
        }

        protected override ContentFetcher GetContentFetcher()
        {
            return new MultipleChoiceContentFetcher();
        }
    }
}