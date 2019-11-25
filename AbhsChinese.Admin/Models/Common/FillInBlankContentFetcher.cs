using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;

namespace AbhsChinese.Admin.Models.Common
{
    public class FillInBlankContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            FillInBlank subject = sub as FillInBlank;
            Yw_FillInBlankContent derivedContent = null;
            if (content == null)
            {
                derivedContent = new Yw_FillInBlankContent();
                derivedContent.Ysc_CreateTime = DateTime.Now;
                derivedContent.Ysc_Creator = currentUser;
                derivedContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                derivedContent = content as Yw_FillInBlankContent;
            }
            derivedContent.Ysc_Editor = currentUser;
            derivedContent.Ysc_Explain = subject.Explain;
            derivedContent.Ysc_Content_Obj = new FillInBlankContentObj
            {
                Stem = UeditorContentFactory.Blank(subject.Name)
            };
            derivedContent.Ysc_Answer_Obj = new FillInBlankAnswerObj
            {
                Correct = subject.Correct.Blanks,
                Other = subject.Other.Blanks,
                Perfect = subject.Perfect.Blanks
            };
            derivedContent.Ysc_UpdateTime = DateTime.Now;
            return derivedContent;
        }
    }
}