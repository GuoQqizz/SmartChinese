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
    public class MarkContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            Mark subject = sub as Mark;
            Yw_SubjectMarkContent selectContent = null;
            if (content == null)
            {
                selectContent = new Yw_SubjectMarkContent();
                selectContent.Ysc_CreateTime = DateTime.Now;
                selectContent.Ysc_Creator = currentUser;
                selectContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                selectContent = content as Yw_SubjectMarkContent;
            }
            selectContent.Ysc_Editor = currentUser;
            selectContent.Ysc_Explain = subject.Explain;
            selectContent.Ysc_Content_Obj = new SubjectMarkContentObj
            {
                Alignment = subject.Alignment,
                Color = subject.Color,
                Content = subject.Content,
                Stem = UeditorContentFactory.FetchUrl(subject.Name, subject.StemType),
                StemType = (int)subject.StemType
            };

            selectContent.Ysc_Answer_Obj = new SubjectMarkAnswerObj
            {
                Answers = GenareteAnswers(subject.Content)
            };
            selectContent.Ysc_UpdateTime = DateTime.Now;

            return selectContent;
        }

        public List<int[]> GenareteAnswers(string content)
        {
            //{:鹅}路上有只{:狗}，房子，汽{:山羊}车{:鸭子}
            //鹅路上有只狗，房子，汽山羊车鸭子
            //[0,1]   [5,1]      [11,2] [14,2]正确答案

            //[0,1]   [8,1]      [17,2] [23,2]
            //  0        1          2       3


            List<int[]> answers = new List<int[]>();
            if (!string.IsNullOrWhiteSpace(content))
            {
                string pattern = @"\{\:[^{}]+[\}]{1}";

                Regex rx = new Regex(pattern);
                var result = rx.Matches(content);
                int num = 0;
                foreach (System.Text.RegularExpressions.Match item in result)
                {
                    int start = item.Index - (3 * num);
                    int length = item.Length - 3;
                    int[] answer = new int[2] { start, length };
                    answers.Add(answer);
                    num++;
                }
            }

            return answers;
        }
    }
}