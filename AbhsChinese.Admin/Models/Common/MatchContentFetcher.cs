using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbhsChinese.Admin.Models.Common
{
    public class MatchContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            Match subject = sub as Match;
            Yw_MatchContent selectContent = null;
            if (content == null)
            {
                selectContent = new Yw_MatchContent();
                selectContent.Ysc_CreateTime = DateTime.Now;
                selectContent.Ysc_Creator = currentUser;
                selectContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                selectContent = content as Yw_MatchContent;
            }
            selectContent.Ysc_Editor = currentUser;
            selectContent.Ysc_Explain = subject.Explain;
            List<SubjectOption> left = new List<SubjectOption>();
            if (subject.Title != null)
            {
                left = subject.Title.Select((t, i) => new SubjectOption
                {
                    Text = UeditorContentFactory.FetchUrl(t, subject.TitleOptionContentType),
                    Key = i
                }).ToList();
            }
            List<SubjectOption> right = new List<SubjectOption>();
            if (subject.Answer != null)
            {
                right = subject.Answer.Select((t, i) => new SubjectOption
                {
                    Text = UeditorContentFactory.FetchUrl(t, subject.AnswertOptionContentType),
                    Key = i
                }).ToList();
            }
            selectContent.Ysc_Content_Obj = new MatchContentObj
            {
                Stem = UeditorContentFactory.FetchUrl(subject.Name, subject.StemType),
                LeftOptions = left,
                RightOptions = right,
                StemType = (int)subject.StemType,
                LeftOptionContentType = (int)subject.TitleOptionContentType,
                RightOptionContentType = (int)subject.AnswertOptionContentType
            };
            List<int[]> dics = new List<int[]>();

            if (subject.LinedAnswers != null)
            {
                for (int i = 0; i < subject.LinedAnswers.Count; i++)
                {
                    int[] d = new int[2] { i, subject.LinedAnswers[i] };

                    dics.Add(d);
                }
            }
            selectContent.Ysc_Answer_Obj = new MatchAnswerObj
            {
                Answers = dics
            };
            selectContent.Ysc_UpdateTime = DateTime.Now;

            return selectContent;
        }
    }
}