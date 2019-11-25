using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AbhsChinese.Admin.Models.Common
{
    public class Mark2ContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            Mark2 subject = sub as Mark2;
            Yw_SubjectMark2Content selectContent = null;
            if (content == null)
            {
                selectContent = new Yw_SubjectMark2Content();
                selectContent.Ysc_CreateTime = DateTime.Now;
                selectContent.Ysc_Creator = currentUser;
                selectContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                selectContent = content as Yw_SubjectMark2Content;
            }
            selectContent.Ysc_Editor = currentUser;
            selectContent.Ysc_Explain = subject.Explain;
            selectContent.Ysc_Content_Obj = new SubjectMark2ContentObj
            {
                Alignment = subject.Alignment,
                Color = subject.Color,
                Content = subject.Content,
                Stem = UeditorContentFactory.FetchUrl(subject.Name, subject.StemType),
                StemType = (int)subject.StemType
            };

            selectContent.Ysc_Answer_Obj = new SubjectMark2AnswerObj
            {
                Answers = GenareteAnswers(subject.Content)
            };
            selectContent.Ysc_UpdateTime = DateTime.Now;

            return selectContent;
        }

        public List<int> GenareteAnswers(string content)
        {
            List<int> answers = new List<int>();
            if (!string.IsNullOrWhiteSpace(content))
            {
                string pattern = @"[/]";

                Regex rx = new Regex(pattern);
                var result = rx.Matches(content);
                int num = 0;
                foreach (System.Text.RegularExpressions.Match item in result)
                {
                    int start = item.Index - num - 1;
                    answers.Add(start);
                    num++;
                }
            }

            return answers;
        }
    }
}