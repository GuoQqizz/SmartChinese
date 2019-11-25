using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Linq;

namespace AbhsChinese.Admin.Models.Common
{
    public class MultipleChoiceContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            MultipleChoice subject = sub as MultipleChoice;
            Yw_SubjectSelectContent selectContent = null;
            if (content == null)
            {
                selectContent = new Yw_SubjectSelectContent();
                selectContent.Ysc_CreateTime = DateTime.Now;
                selectContent.Ysc_Creator = currentUser;
                selectContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                selectContent = content as Yw_SubjectSelectContent;
            }
            selectContent.Ysc_Editor = currentUser;
            selectContent.Ysc_Explain = subject.Explain;
            if (subject.ContentType == UeditorType.Image)
            {
                subject.Options = subject.Options.Select(o =>
                    UeditorContentFactory.FetchUrl(o, subject.ContentType)).ToList();
            }
            var options = subject.Options
                .Select((o, i) => new SubjectOption { Text = o, Key = i })
                .ToList();

            selectContent.Ysc_Answer_Obj = new SubjectSelectAnswerObj
            {
                Answers = subject.Answers
            };
            string stem = UeditorContentFactory.FetchUrl(subject.Name, subject.StemType);
            selectContent.Ysc_Content_Obj = new SubjectSelectContentObj
            {
                StemType = (int)subject.StemType,
                Stem = stem,
                ContentType = (int)subject.ContentType,
                Options = options,
                Display = subject.Display,
                Random = subject.Random
            };
            selectContent.Ysc_UpdateTime = DateTime.Now;

            return selectContent;
        }
    }
}