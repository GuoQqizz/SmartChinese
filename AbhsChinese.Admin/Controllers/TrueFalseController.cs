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
    public class TrueFalseController : SubjectController
    {
        [HttpGet]
        public ActionResult EditTrueFalseSubject(int id = 0)
        {
            return View(id);
        }

        [HttpGet]
        public ActionResult EditTrueFalseSubject2(int id = 0)
        {
            return View(id);
        }

        [HttpPost]
        [ActionName("EditTrueFalseSubject")]
        [ValidateInput(false)]
        public ActionResult EditTrueFalseSubjectConfirm(TrueFalse subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        [HttpGet]
        public ActionResult ViewTrueFalseSubject(int id = 0)
        {
            return View(id);
        }

        /// <summary>
        /// 获取判断题详情
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
            TrueFalse inputModel = new TrueFalse();
            Yw_TrueFalseContent c = content as Yw_TrueFalseContent;
            inputModel.Answer = c.Ysc_Answer_Obj.Answer;
            inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
            inputModel.Name = UeditorContentFactory.RestoreContent(
                c.Ysc_Content_Obj.Stem,
                inputModel.StemType);
            return inputModel;
        }

        protected override ContentFetcher GetContentFetcher()
        {
            return new TrueFalseContentFetcher();
        }

        protected override QuestionInputModel ConvertToDerived2(Yw_SubjectContent content)
        {
            TrueFalse inputModel = new TrueFalse();
            if (content != null)
            {
                Yw_TrueFalseContent c = content as Yw_TrueFalseContent;
                inputModel.Answer = c.Ysc_Answer_Obj.Answer;
                inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
                inputModel.Name = UeditorContentFactory.RestoreContent(
                    c.Ysc_Content_Obj.Stem,
                    inputModel.StemType);
            }
            else
            {
                inputModel.StemType = UeditorType.Text;
                inputModel.Name = string.Empty;
                inputModel.Answer = 1;
            }
            return inputModel;
        }
    }
}