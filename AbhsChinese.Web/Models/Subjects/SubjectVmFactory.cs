using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AbhsChinese.Web.Models.Subjects
{
    public static class SubjectVmFactory
    {
        public static SubjectVm Create(Yw_SubjectContent content)
        {
            Check.IfNull(content, nameof(content));

            SubjectVm vm = null;
            SubjectTypeEnum subjectType = (SubjectTypeEnum)content.Ysc_SubjectType;
            switch (subjectType)
            {
                case SubjectTypeEnum.选择题:
                    Yw_SubjectSelectContent select = content as Yw_SubjectSelectContent;
                    var random = select.Ysc_Content_Obj.Random;
                    var mOptions = select.Ysc_Content_Obj.Options;
                    if (random == 1)//随机显示
                    {
                        mOptions = mOptions.OrderBy(c => Guid.NewGuid()).ToList();
                    }
                    vm = new MultipleChoiceSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = select.Ysc_Explain,
                        Answer = select.Ysc_Answer_Obj.Answers,
                        StemType = select.Ysc_Content_Obj.StemType,
                        Stem = select.Ysc_Content_Obj.Stem,
                        Options = mOptions,
                        OptionType = select.Ysc_Content_Obj.ContentType,
                        SubjectType = select.Ysc_SubjectType,
                        Display = select.Ysc_Content_Obj.Display
                    };
                    break;

                case SubjectTypeEnum.判断题:
                    Yw_TrueFalseContent trueFalse = content as Yw_TrueFalseContent;
                    vm = new TrueFalseSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = trueFalse.Ysc_Explain,
                        Answer = trueFalse.Ysc_Answer_Obj.Answer,
                        StemType = trueFalse.Ysc_Content_Obj.StemType,
                        Stem = trueFalse.Ysc_Content_Obj.Stem,
                        SubjectType = trueFalse.Ysc_SubjectType
                    };
                    break;

                case SubjectTypeEnum.填空题:
                    Yw_FillInBlankContent fillInBlank = content as Yw_FillInBlankContent;
                    string blankStem = UeditorContentFactory.Blank(
                        fillInBlank.Ysc_Content_Obj.Stem);
                    vm = new FillInBlankSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = fillInBlank.Ysc_Explain,
                        Answer = new
                        {
                            Perfect = fillInBlank.Ysc_Answer_Obj.Perfect,
                            Correct = fillInBlank.Ysc_Answer_Obj.Correct,
                            Other = fillInBlank.Ysc_Answer_Obj.Other
                        },
                        StemType = (int)UeditorType.Text,
                        Stem = blankStem,
                        SubjectType = fillInBlank.Ysc_SubjectType
                    };
                    break;

                case SubjectTypeEnum.选择填空:
                    MultipleChoiceFillInBlankContent multipleChoiceFillInBlank = content as MultipleChoiceFillInBlankContent;
                    List<SubjectOption> options = new List<SubjectOption>();
                    options.AddRange(multipleChoiceFillInBlank.Ysc_Content_Obj.SubjectOptions);
                    options.AddRange(multipleChoiceFillInBlank.Ysc_Content_Obj.SubjectGOptions);
                    UeditorType contentType =
                        (UeditorType)multipleChoiceFillInBlank.Ysc_Content_Obj.ContentType;
                    RestoreUrl(options, contentType);
                    options = options.OrderBy(c => Guid.NewGuid()).ToList();
                    string mBlankStem = UeditorContentFactory.Blank(
                        multipleChoiceFillInBlank.Ysc_Content_Obj.Stem);
                    vm = new MultipleChoiceFillInBlankSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = multipleChoiceFillInBlank.Ysc_Explain,
                        Answer = multipleChoiceFillInBlank.Ysc_Answer_Obj.Answers,
                        StemType = (int)UeditorType.Text,
                        Stem = mBlankStem,
                        Options = options,
                        OptionType = multipleChoiceFillInBlank.Ysc_Content_Obj.ContentType,
                        SubjectType = multipleChoiceFillInBlank.Ysc_SubjectType
                    };
                    break;

                case SubjectTypeEnum.连线题:
                    Yw_MatchContent match = content as Yw_MatchContent;

                    var contentObj = match.Ysc_Content_Obj;

                    var leftOptions = new List<SubjectOption>();
                    leftOptions.AddRange(contentObj.LeftOptions);
                    RestoreUrl(leftOptions, (UeditorType)contentObj.LeftOptionContentType);
                    leftOptions = leftOptions.OrderBy(c => Guid.NewGuid()).ToList();

                    var rightOptions = new List<SubjectOption>();
                    rightOptions.AddRange(contentObj.RightOptions);
                    RestoreUrl(rightOptions, (UeditorType)contentObj.RightOptionContentType);
                    rightOptions = rightOptions.OrderBy(c => Guid.NewGuid()).ToList();
                    vm = new MatchSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = match.Ysc_Explain,
                        Answer = match.Ysc_Answer_Obj.Answers,
                        StemType = contentObj.StemType,
                        Stem = contentObj.Stem,
                        LeftOptions = leftOptions,
                        RightOptions = rightOptions,
                        SubjectType = match.Ysc_SubjectType,
                        LeftOptionType = match.Ysc_Content_Obj.LeftOptionContentType,
                        RightOptionType = match.Ysc_Content_Obj.RightOptionContentType
                    };
                    break;

                case SubjectTypeEnum.主观题:
                    Yw_SubjectFreeContent free = content as Yw_SubjectFreeContent;
                    string freeAnswer = free.Ysc_Answer_Obj.Answer;
                    if (free.Ysc_Content_Obj.AnswerType == (int)UeditorType.Image)
                    {
                        freeAnswer = ConfigurationManager.AppSettings["OssHostUrl"]
                            + freeAnswer;
                    }

                    string freeStandard = free.Ysc_Content_Obj.ScoreRules;
                    if (free.Ysc_Content_Obj.ScoreRulesType == (int)UeditorType.Image)
                    {
                        freeStandard = ConfigurationManager.AppSettings["OssHostUrl"]
                            + freeStandard;
                    }

                    string freeStem = free.Ysc_Content_Obj.Stem;
                    if (free.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
                    {
                        freeStem = ConfigurationManager.AppSettings["OssHostUrl"]
                            + freeStem;
                    }
                    vm = new FreeSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = free.Ysc_Explain,
                        Answer = freeAnswer,
                        AnswerType = free.Ysc_Content_Obj.AnswerType,
                        StemType = free.Ysc_Content_Obj.StemType,
                        Stem = freeStem,
                        SubjectType = free.Ysc_SubjectType,
                        Standard = freeStandard,
                        StandardType = free.Ysc_Content_Obj.ScoreRulesType
                    };
                    break;

                case SubjectTypeEnum.圈点批注标色:
                    Yw_SubjectMarkContent mark = content as Yw_SubjectMarkContent;
                    string stem = mark.Ysc_Content_Obj.Stem;
                    if (mark.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
                    {
                        stem = ConfigurationManager.AppSettings["OssHostUrl"]
                            + stem;
                    }
                    string markContent = mark.Ysc_Content_Obj.Content?
                        .Replace("{:", string.Empty)
                        .Replace("{：", string.Empty)
                        .Replace("}", string.Empty);

                    vm = new MarkSubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = mark.Ysc_Explain,
                        Answer = mark.Ysc_Answer_Obj.Answers,
                        StemType = mark.Ysc_Content_Obj.StemType,
                        Stem = stem,
                        SubjectType = mark.Ysc_SubjectType,
                        Content = markContent,
                        Color = mark.Ysc_Content_Obj.Color
                    };
                    break;

                case SubjectTypeEnum.圈点批注断句:
                    Yw_SubjectMark2Content mark2 = content as Yw_SubjectMark2Content;
                    string mark2Stem = mark2.Ysc_Content_Obj.Stem;
                    if (mark2.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
                    {
                        mark2Stem = ConfigurationManager.AppSettings["OssHostUrl"]
                            + mark2Stem;
                    }
                    string mark2Content = mark2.Ysc_Content_Obj.Content?
                        .Replace("/", string.Empty);
                    vm = new Mark2SubjectVm
                    {
                        KnowledgeId = 0,
                        Analysis = mark2.Ysc_Explain,
                        Answer = mark2.Ysc_Answer_Obj.Answers,
                        StemType = mark2.Ysc_Content_Obj.StemType,
                        Stem = mark2Stem,
                        SubjectType = mark2.Ysc_SubjectType,
                        Content = mark2Content,
                        Color = mark2.Ysc_Content_Obj.Color
                    };
                    break;

                default:
                    break;
            }

            return vm;
        }

        private static void RestoreUrl(List<SubjectOption> options, UeditorType type)
        {
            options.ForEach((o) =>
            {
                o.Text = UeditorContentFactory.RestoreUrl(o.Text, type);
            });
        }
    }
}