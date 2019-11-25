using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;

namespace AbhsChinese.Admin.Models.Common
{
    public class TrueFalseContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            TrueFalse subject = sub as TrueFalse;
            Yw_TrueFalseContent derivedContent = null;
            if (content == null)
            {
                derivedContent = new Yw_TrueFalseContent();
                derivedContent.Ysc_CreateTime = DateTime.Now;
                derivedContent.Ysc_Creator = currentUser;
                derivedContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                derivedContent = content as Yw_TrueFalseContent;
            }
            derivedContent.Ysc_Editor = currentUser;
            derivedContent.Ysc_Explain = subject.Explain;
            derivedContent.Ysc_Content_Obj = new TrueFalseContentObj
            {
                StemType = (int)subject.StemType,
                Stem = UeditorContentFactory.FetchUrl(subject.Name, subject.StemType)
            };
            derivedContent.Ysc_Answer_Obj = new TrueFalseAnswerObj
            {
                Answer = subject.Answer
            };
            derivedContent.Ysc_UpdateTime = DateTime.Now;

            return derivedContent;
        }
    }
}