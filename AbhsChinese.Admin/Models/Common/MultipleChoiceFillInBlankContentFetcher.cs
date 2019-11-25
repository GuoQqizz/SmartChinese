using AbhsChinese.Admin.Models.Question;
using AbhsChinese.Bll;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Common
{
    public class MultipleChoiceFillInBlankContentFetcher : ContentFetcher
    {
        protected override Yw_SubjectContent GetContent(
            QuestionInputModel sub,
            SubjectBll bll,
            int currentUser,
            Yw_SubjectContent content)
        {
            MultipleChoiceFillInBlank subject = sub as MultipleChoiceFillInBlank;
            MultipleChoiceFillInBlankContent selectContent = null;
            if (content == null)
            {
                selectContent = new MultipleChoiceFillInBlankContent();
                selectContent.Ysc_CreateTime = DateTime.Now;
                selectContent.Ysc_Creator = currentUser;
                selectContent.Ysc_SubjectType = subject.SubjectType;
            }
            else
            {
                selectContent = content as MultipleChoiceFillInBlankContent;
            }
            selectContent.Ysc_Editor = currentUser;
            selectContent.Ysc_Explain = subject.Explain;
            List<int[]> anses = new List<int[]>();
            if (subject.Options != null)
            {
                for (int i = 0; i < subject.Options.Count; i++)
                {
                    int[] ds = new int[2] { i, i };
                    anses.Add(ds);
                }
            }
            selectContent.Ysc_Answer_Obj = new MultipleChoiceFillInBlankAnswerObj
            {
                Answers = anses
            };

            //答案的选项
            List<SubjectOption> subjectOptions = new List<SubjectOption>();
            if (subject.Options != null)
            {
                for (int i = 0; i < subject.Options.Count; i++)
                {
                    subjectOptions.Add(new SubjectOption
                    {
                        Key = i,
                        Text = UeditorContentFactory.FetchUrl(subject.Options[i],
                                                              subject.ContentType)
                    });
                }
            }

            //干扰项
            List<SubjectOption> subjectGOptions = new List<SubjectOption>();
            if (subject.Goptions != null)
            {
                for (int i = 0; i < subject.Goptions.Count; i++)
                {
                    subjectGOptions.Add(new SubjectOption
                    {
                        Key = i + 100,//干扰项的key设置值
                        Text = UeditorContentFactory.FetchUrl(subject.Goptions[i],
                                                              subject.ContentType)
                    });
                }
            }
            selectContent.Ysc_Content_Obj = new MultipleChoiceFillInBlankContentObj
            {
                Stem = UeditorContentFactory.Blank(subject.Name),
                ContentType = (int)subject.ContentType,
                SubjectOptions = subjectOptions,
                SubjectGOptions = subjectGOptions,
                //GPositions = gps
            };

            selectContent.Ysc_UpdateTime = DateTime.Now;

            return selectContent;
        }
    }
}