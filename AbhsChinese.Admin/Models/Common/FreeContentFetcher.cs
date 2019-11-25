using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;

namespace AbhsChinese.Admin.Models.Common
{
    public class FreeContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            Free subject = sub as Free;
            Yw_SubjectFreeContent selectContent = null;
            if (content == null)
            {
                selectContent = new Yw_SubjectFreeContent();
                selectContent.Ysc_CreateTime = DateTime.Now;
                selectContent.Ysc_Creator = currentUser;
                selectContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                selectContent = content as Yw_SubjectFreeContent;
            }
            selectContent.Ysc_Editor = currentUser;
            selectContent.Ysc_Explain = subject.Explain;
            selectContent.Ysc_Answer_Obj = new SubjectFreeAnswerObj
            {
                Answer = subject.Answer
            };
            selectContent.Ysc_Content_Obj = new SubjectFreeContentObj
            {
                ScoreRules = subject.ScoreRules,
                Stem = UeditorContentFactory.FetchUrl(subject.Name, subject.StemType),
                StemType = (int)subject.StemType
            };
            selectContent.Ysc_UpdateTime = DateTime.Now;

            return selectContent;
        }
    }
}