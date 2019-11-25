using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.Subject;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace AbhsChinese.Domain.JsonTranslator
{
    public static class SubjectContentDtoBuilder
    {
        private static string fileUrlPrefix = ConfigurationManager.AppSettings["OssHostUrl"];

        public static DtoSubjectContent Create(Yw_SubjectContent content, Yw_Subject subject)
        {
            Check.IfNull(content, nameof(content));
            Check.IfNull(subject, nameof(subject));

            int kernalKnowledgeId = subject == null ? 0 : subject.Ysj_MainKnowledgeId;
            DtoSubjectContent dto = null;
            SubjectTypeEnum subjectType = (SubjectTypeEnum)content.Ysc_SubjectType;
            switch (subjectType)
            {
                case SubjectTypeEnum.选择题:
                    Yw_SubjectSelectContent select = content as Yw_SubjectSelectContent;
                    var random = select.Ysc_Content_Obj.Random;
                    var mOptions = StorageIfHasValue(select.Ysc_Content_Obj.Options);
                    if (random == 1)//随机显示
                    {
                        mOptions = mOptions.OrderBy(c => Guid.NewGuid()).ToList();
                    }
                    UeditorType optionType = (UeditorType)select.Ysc_Content_Obj.ContentType;
                    foreach (var item in mOptions)
                    {
                        item.Text = UeditorContentFactory.RestoreUrl(
                            item.Text, optionType);
                    }
                    dto = new DtoSelectSubjectContent
                    {
                        SubjectId = select.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = select.Ysc_Explain,
                        Answer = select.Ysc_Answer_Obj.Answers,
                        StemType = (UeditorType)select.Ysc_Content_Obj.StemType,
                        Stem = UeditorContentFactory.RestoreUrl(
                            select.Ysc_Content_Obj.Stem,
                            (UeditorType)select.Ysc_Content_Obj.StemType),
                        Options = mOptions,
                        OptionType = optionType,
                        SubjectType = (SubjectTypeEnum)select.Ysc_SubjectType,
                        Display = select.Ysc_Content_Obj.Display
                    };
                    break;

                case SubjectTypeEnum.判断题:
                    Yw_TrueFalseContent trueFalse = content as Yw_TrueFalseContent;
                    dto = new DtoTrueFalseSubjectContent
                    {
                        SubjectId = trueFalse.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = trueFalse.Ysc_Explain,
                        Answer = trueFalse.Ysc_Answer_Obj.Answer,
                        StemType = (UeditorType)trueFalse.Ysc_Content_Obj.StemType,
                        Stem = UeditorContentFactory.RestoreUrl(
                            trueFalse.Ysc_Content_Obj.Stem,
                            (UeditorType)trueFalse.Ysc_Content_Obj.StemType),
                        SubjectType = (SubjectTypeEnum)trueFalse.Ysc_SubjectType
                    };
                    break;

                case SubjectTypeEnum.填空题:
                    Yw_FillInBlankContent fillInBlank = content as Yw_FillInBlankContent;
                    string blankStem = UeditorContentFactory.Blank(
                        fillInBlank.Ysc_Content_Obj.Stem);
                    dto = new DtoFillInBlankSubjectContent
                    {
                        SubjectId = fillInBlank.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = fillInBlank.Ysc_Explain,
                        Answer = new
                        {
                            Perfect = fillInBlank.Ysc_Answer_Obj.Perfect,
                            Correct = fillInBlank.Ysc_Answer_Obj.Correct,
                            Other = fillInBlank.Ysc_Answer_Obj.Other
                        },
                        StemType = UeditorType.Text,
                        Stem = blankStem,
                        SubjectType = (SubjectTypeEnum)fillInBlank.Ysc_SubjectType
                    };
                    break;

                case SubjectTypeEnum.选择填空:
                    MultipleChoiceFillInBlankContent multipleChoiceFillInBlank
                        = content as MultipleChoiceFillInBlankContent;
                    List<SubjectOption> options = new List<SubjectOption>();
                    options.AddRange(multipleChoiceFillInBlank.Ysc_Content_Obj.SubjectOptions);
                    var gOptions = multipleChoiceFillInBlank.Ysc_Content_Obj.SubjectGOptions;
                    options.AddRange(gOptions);
                    options = StorageIfHasValue(options).ToList();
                    UeditorType contentType =
                        (UeditorType)multipleChoiceFillInBlank.Ysc_Content_Obj.ContentType;
                    RestoreUrl(options, contentType);
                    options = options.OrderBy(c => Guid.NewGuid()).ToList();
                    string mBlankStem = UeditorContentFactory.Blank(
                        multipleChoiceFillInBlank.Ysc_Content_Obj.Stem);
                    dto = new DtoSelectFillInBlankSubjectContent
                    {
                        SubjectId = multipleChoiceFillInBlank.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = multipleChoiceFillInBlank.Ysc_Explain,
                        Answer = multipleChoiceFillInBlank.Ysc_Answer_Obj.Answers,
                        StemType = UeditorType.Text,
                        Stem = mBlankStem,
                        Options = options,
                        OptionType = (UeditorType)multipleChoiceFillInBlank.Ysc_Content_Obj.ContentType,
                        SubjectType = (SubjectTypeEnum)multipleChoiceFillInBlank.Ysc_SubjectType
                    };
                    break;

                case SubjectTypeEnum.连线题:
                    Yw_MatchContent match = content as Yw_MatchContent;

                    var contentObj = match.Ysc_Content_Obj;

                    var leftOptions = new List<SubjectOption>();
                    leftOptions.AddRange(contentObj.LeftOptions);
                    leftOptions = StorageIfHasValue(leftOptions).ToList();
                    RestoreUrl(leftOptions, (UeditorType)contentObj.LeftOptionContentType);
                    leftOptions = leftOptions.OrderBy(c => Guid.NewGuid()).ToList();

                    var rightOptions = new List<SubjectOption>();
                    rightOptions.AddRange(contentObj.RightOptions);
                    rightOptions = StorageIfHasValue(rightOptions).ToList();
                    RestoreUrl(rightOptions, (UeditorType)contentObj.RightOptionContentType);
                    rightOptions = rightOptions.OrderBy(c => Guid.NewGuid()).ToList();
                    dto = new DtoMatchSubjectContent
                    {
                        SubjectId = match.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = match.Ysc_Explain,
                        Answer = match.Ysc_Answer_Obj.Answers,
                        StemType = (UeditorType)contentObj.StemType,
                        Stem = UeditorContentFactory.RestoreUrl(
                            contentObj.Stem,
                            (UeditorType)contentObj.StemType),
                        LeftOptions = leftOptions,
                        RightOptions = rightOptions,
                        SubjectType = (SubjectTypeEnum)match.Ysc_SubjectType,
                        LeftOptionType = (UeditorType)match.Ysc_Content_Obj.LeftOptionContentType,
                        RightOptionType = (UeditorType)match.Ysc_Content_Obj.RightOptionContentType
                    };
                    break;

                case SubjectTypeEnum.主观题:
                    Yw_SubjectFreeContent free = content as Yw_SubjectFreeContent;
                    string freeAnswer = free.Ysc_Answer_Obj.Answer;
                    string freeStandard = free.Ysc_Content_Obj.ScoreRules;

                    string freeStem = free.Ysc_Content_Obj.Stem;
                    if (free.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
                    {
                        freeStem = fileUrlPrefix + freeStem;
                    }
                    dto = new DtoFreeSubjectContent
                    {
                        SubjectId = free.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = free.Ysc_Explain,
                        Answer = freeAnswer,
                        StemType = (UeditorType)free.Ysc_Content_Obj.StemType,
                        Stem = freeStem,
                        SubjectType = (SubjectTypeEnum)free.Ysc_SubjectType,
                        Standard = freeStandard
                    };
                    break;

                case SubjectTypeEnum.圈点批注标色:
                    Yw_SubjectMarkContent mark = content as Yw_SubjectMarkContent;
                    string stem = mark.Ysc_Content_Obj.Stem;
                    if (mark.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
                    {
                        stem = fileUrlPrefix + stem;
                    }
                    string markContent = ReplacePlaceholder(mark.Ysc_Content_Obj.Content);

                    dto = new DtoMarkSubjectContent
                    {
                        Alignment=mark.Ysc_Content_Obj.Alignment,
                        SubjectId = mark.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = mark.Ysc_Explain,
                        Answer = mark.Ysc_Answer_Obj.Answers,
                        StemType = (UeditorType)mark.Ysc_Content_Obj.StemType,
                        Stem = stem,
                        SubjectType = (SubjectTypeEnum)mark.Ysc_SubjectType,
                        Content = markContent,
                        Color = mark.Ysc_Content_Obj.Color
                    };
                    break;

                case SubjectTypeEnum.圈点批注断句:
                    Yw_SubjectMark2Content mark2 = content as Yw_SubjectMark2Content;
                    string mark2Stem = mark2.Ysc_Content_Obj.Stem;
                    if (mark2.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
                    {
                        mark2Stem = fileUrlPrefix + mark2Stem;
                    }
                    string mark2Content = mark2.Ysc_Content_Obj.Content?
                        .Replace("/", string.Empty);
                    dto = new DtoMark2SubjectContent
                    {
                        Alignment=mark2.Ysc_Content_Obj.Alignment,
                        SubjectId = mark2.Ysc_SubjectId,
                        KnowledgeId = kernalKnowledgeId,
                        Analysis = mark2.Ysc_Explain,
                        Answer = mark2.Ysc_Answer_Obj.Answers,
                        StemType = (UeditorType)mark2.Ysc_Content_Obj.StemType,
                        Stem = mark2Stem,
                        SubjectType = (SubjectTypeEnum)mark2.Ysc_SubjectType,
                        Content = mark2Content,
                        Color = mark2.Ysc_Content_Obj.Color
                    };
                    break;

                default:
                    break;
            }

            return dto;
        }

        public static string ReplacePlaceholder(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return content;
            }

            Regex regex = new Regex(@"{:(\S+?)}");
            MatchCollection coll = regex.Matches(content);
            if (coll.Count > 0)
            {
                foreach (Match item in coll)
                {
                    string expression = item.Value;
                    string text = item.Groups[1].Value;
                    content = content.Replace(expression, text);
                }
            }
            return content;
        }

        private static IList<SubjectOption> StorageIfHasValue(IEnumerable<SubjectOption> options)
        {
            var result = new List<SubjectOption>();
            if (options != null)
            {
                foreach (var item in options)
                {
                    if (item.Text.HasValue())
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
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