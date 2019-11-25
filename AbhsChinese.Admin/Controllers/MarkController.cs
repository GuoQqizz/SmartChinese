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
    public class MarkController : SubjectController
    {
        [HttpGet]
        public ActionResult EditMarkSubject(int id = 0)
        {
            return View(id);
        }

        [HttpPost]
        [ActionName("EditMarkSubject")]
        [ValidateInput(false)]
        public ActionResult EditMarkSubjectConfirm(
            Mark subject)
        {
            var id = SaveSubject(subject);

            return Json(new SuccessJsonResponse(new { id = id, button = subject.Button }));
        }

        /// <summary>
        /// 获取圈点批注-标色详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult GetQuestionInfo(int id)
        {
            return PartialView(id);
        }

        [HttpGet]
        public ActionResult ViewMarkSubject(int id = 0)
        {
            return View(id);
        }

        protected override QuestionInputModel ConvertToDerived(
            Yw_SubjectContent content)
        {
            var inputModel = new Mark();
            var c = content as Yw_SubjectMarkContent;
            inputModel.Color = c.Ysc_Content_Obj.Color;
            inputModel.Content = c.Ysc_Content_Obj.Content;
            inputModel.Alignment = c.Ysc_Content_Obj.Alignment;
            inputModel.StemType = (UeditorType)c.Ysc_Content_Obj.StemType;
            inputModel.Name = UeditorContentFactory.RestoreContent(
                c.Ysc_Content_Obj.Stem,
                inputModel.StemType);
            return inputModel;
        }

        protected override QuestionInputModel ConvertToDerived2(Yw_SubjectContent content)
        {
            throw new NotImplementedException();
        }

        protected override ContentFetcher GetContentFetcher()
        {
            return new MarkContentFetcher();
        }
    }
}