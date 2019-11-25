using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.Subject;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using AbhsChinese.Domain.JsonEntity.Subject;
using AbhsChinese.Domain.JsonTranslator;
using AbhsChinese.Web.Models.Subjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public static class SubjectReportVmFactory
    {
        public static ReportVm Create(Yw_SubjectContent content, StudentAnswerBase studentAnswer, Yw_Subject subject)
        {
                DtoSubjectContent subjectContent = SubjectContentDtoBuilder.Create(content, subject);

                ReportVm vm = null;
                SubjectTypeEnum subjectType = (SubjectTypeEnum)studentAnswer.Type;
                switch (subjectType)
                {//选择、选择填空、连线
                    case SubjectTypeEnum.选择题:
                        StuSelectAnswer selectAnswer = studentAnswer as StuSelectAnswer;
                        DtoSelectSubjectContent selectContent = subjectContent as DtoSelectSubjectContent;
                        vm = new MultipleChoiceReportVm
                        {
                            SubjectId = selectAnswer.SubjectId,
                            Stem = selectContent.Stem,
                            StemType = (int)selectContent.StemType,
                            Answer = selectContent.Answer,
                            Options = selectContent.Options.OrderBy(s => Array.IndexOf(selectAnswer.OptionSequences.ToArray(), s.Key)).ToList(),
                            OptionType = (int)selectContent.OptionType,
                            SubjectType = (int)selectContent.SubjectType,
                            Analysis = selectContent.Analysis,

                            StudentAnswer = selectAnswer.Answers,
                            ResultStars = selectAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.判断题:
                        StuTrueFalseAnswer trueFalseAns = studentAnswer as StuTrueFalseAnswer;
                        DtoTrueFalseSubjectContent trueFalseCon = subjectContent as DtoTrueFalseSubjectContent;
                        vm = new TrueFalseReportVm
                        {
                            SubjectId = trueFalseAns.SubjectId,
                            Stem = trueFalseCon.Stem,
                            StemType = (int)trueFalseCon.StemType,
                            Answer = trueFalseCon.Answer,
                            SubjectType = (int)trueFalseCon.SubjectType,
                            Analysis = trueFalseCon.Analysis,

                            StudentAnswer = trueFalseAns.Answer,
                            ResultStars = trueFalseAns.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.填空题:
                        StuBlankAnswer blankAnswer = studentAnswer as StuBlankAnswer;
                        DtoFillInBlankSubjectContent fillInBlank = subjectContent as DtoFillInBlankSubjectContent;
                        vm = new FillInBlankReportVm
                        {
                            SubjectId = blankAnswer.SubjectId,
                            Stem = fillInBlank.Stem,
                            StemType = (int)fillInBlank.StemType,
                            Answer = fillInBlank.Answer,
                            SubjectType = (int)fillInBlank.SubjectType,
                            Analysis = fillInBlank.Analysis,

                            StudentAnswer = blankAnswer.Answers,
                            ResultStars = blankAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.选择填空:
                        StuSelectBlankAnswer selectBlankAnswer = studentAnswer as StuSelectBlankAnswer;

                    DtoSelectFillInBlankSubjectContent multipleChoiceFillInBlank = subjectContent as DtoSelectFillInBlankSubjectContent;
                    
                        vm = new MultipleChoiceFillInBlankReportVm
                        {
                            SubjectId = selectBlankAnswer.SubjectId,
                            Stem = multipleChoiceFillInBlank.Stem,
                            StemType = (int)multipleChoiceFillInBlank.StemType,
                            Answer = multipleChoiceFillInBlank.Answer,
                            Options = multipleChoiceFillInBlank.Options.OrderBy(s => Array.IndexOf(selectBlankAnswer.OptionSequences.ToArray(), s.Key)).ToList(),
                            OptionType = (int)multipleChoiceFillInBlank.OptionType,
                            SubjectType = (int)multipleChoiceFillInBlank.SubjectType,
                            Analysis = multipleChoiceFillInBlank.Analysis,

                            StudentAnswer = selectBlankAnswer.Answers,
                            ResultStars = selectBlankAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.连线题:
                        StuMatchAnswer matchAnswer = studentAnswer as StuMatchAnswer;

                        DtoMatchSubjectContent match = subjectContent as DtoMatchSubjectContent;
                       
                        vm = new MatchReportVm
                        {
                            SubjectId = matchAnswer.SubjectId,
                            Stem = match.Stem,
                            StemType = (int)match.StemType,
                            Answer = match.Answer,
                            SubjectType = (int)match.SubjectType,
                            LeftOptions = match.LeftOptions.OrderBy(s => Array.IndexOf(matchAnswer.LeftOptionSequences.ToArray(), s.Key)).ToList(),
                            RightOptions = match.RightOptions.OrderBy(s => Array.IndexOf(matchAnswer.RightOptionSequences.ToArray(), s.Key)).ToList(),
                            LeftOptionType = (int)match.LeftOptionType,
                            RightOptionType = (int)match.RightOptionType,
                            Analysis = match.Analysis,

                            StudentAnswer = matchAnswer.Answers,
                            ResultStars = matchAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.主观题:
                        StuFreeAnswer freeAnswer = studentAnswer as StuFreeAnswer;
                        DtoFreeSubjectContent free = subjectContent as DtoFreeSubjectContent;
                        
                        vm = new FreeReportVm
                        {
                            SubjectId = freeAnswer.SubjectId,
                            Stem = free.Stem,
                            StemType = (int)free.StemType,
                            Answer = free.Answer,
                            SubjectType = (int)free.SubjectType,
                            Analysis = free.Analysis,
                            Standard = free.Standard,

                            StudentAnswer = freeAnswer.Answer,
                            ResultStars = freeAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.圈点批注标色:
                        StuMarkColorAnswer markColorAnswer = studentAnswer as StuMarkColorAnswer;
                        DtoMarkSubjectContent mark = subjectContent as DtoMarkSubjectContent;
                    
                        vm = new MarkReportVm
                        {
                            SubjectId = markColorAnswer.SubjectId,
                            Stem = mark.Stem,
                            StemType = (int)mark.StemType,
                            Answer = mark.Answer,
                            SubjectType = (int)mark.SubjectType,
                            Analysis = mark.Analysis,
                            Content = mark.Content,
                            Color = mark.Color,

                            StudentAnswer = markColorAnswer.Answers,
                            ResultStars = markColorAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    case SubjectTypeEnum.圈点批注断句:
                        StuMarkCutAnswer markCutAnswer = studentAnswer as StuMarkCutAnswer;
                        DtoMark2SubjectContent mark2 = subjectContent as DtoMark2SubjectContent;
                    
                        vm = new Mark2ReportVm
                        {
                            SubjectId = markCutAnswer.SubjectId,
                            Stem = mark2.Stem,
                            StemType = (int)mark2.StemType,
                            Answer = mark2.Answer,
                            SubjectType = (int)mark2.SubjectType,
                            Analysis = mark2.Analysis,
                            Content = mark2.Content,
                            Color = mark2.Color,

                            StudentAnswer = markCutAnswer.Answers,
                            ResultStars = markCutAnswer.ResultStars,
                            Difficulty = subject.Ysj_Difficulty
                        };
                        break;
                    default:
                        break;
                }
            return vm;
        }
        #region 旧版
        //private static string fileUrlPrefix = ConfigurationManager.AppSettings["OssHostUrl"];
        //public static ReportVm Create(Yw_SubjectContent content, StudentAnswerBase studentAnswer, Yw_Subject subject)
        //{
        //    Check.IfNull(studentAnswer, nameof(studentAnswer));
        //    Check.IfNull(content, nameof(content));
        //    Check.IfNull(subject, nameof(subject));

        //    int kernalKnowledgeId = subject == null ? 0 : subject.Ysj_MainKnowledgeId;

        //    ReportVm vm = null;
        //    SubjectTypeEnum subjectType = (SubjectTypeEnum)studentAnswer.Type;
        //    switch (subjectType)
        //    {//选择、选择填空、连线
        //        case SubjectTypeEnum.选择题:
        //            StuSelectAnswer selectAnswer = studentAnswer as StuSelectAnswer;
        //            Yw_SubjectSelectContent selectContent = content as Yw_SubjectSelectContent;
        //            var selectOptions = selectContent.Ysc_Content_Obj.Options.OrderBy(s => Array.IndexOf(selectAnswer.OptionSequences.ToArray(), s.Key)).ToList();
        //            foreach (var item in selectOptions)
        //            {
        //                item.Text = UeditorContentFactory.RestoreUrl(
        //                    item.Text, (UeditorType)selectContent.Ysc_Content_Obj.ContentType);
        //            }
        //            vm = new MultipleChoiceReportVm
        //            {
        //                SubjectId = selectAnswer.SubjectId,
        //                Stem = UeditorContentFactory.RestoreUrl(
        //                    selectContent.Ysc_Content_Obj.Stem,
        //                    (UeditorType)selectContent.Ysc_Content_Obj.StemType),
        //                StemType = selectContent.Ysc_Content_Obj.StemType,
        //                Answer = selectContent.Ysc_Answer_Obj.Answers,
        //                Options = selectOptions,
        //                OptionType = selectContent.Ysc_Content_Obj.ContentType,
        //                SubjectType = selectContent.Ysc_SubjectType,
        //                Analysis = selectContent.Ysc_Explain,

        //                StudentAnswer = selectAnswer.Answers,
        //                ResultStars = selectAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.判断题:
        //            StuTrueFalseAnswer trueFalseAns = studentAnswer as StuTrueFalseAnswer;
        //            Yw_TrueFalseContent trueFalseCon = content as Yw_TrueFalseContent;
        //            vm = new TrueFalseReportVm
        //            {
        //                SubjectId = trueFalseAns.SubjectId,
        //                Stem = UeditorContentFactory.RestoreUrl(
        //                    trueFalseCon.Ysc_Content_Obj.Stem,
        //                    (UeditorType)trueFalseCon.Ysc_Content_Obj.StemType),
        //                StemType = trueFalseCon.Ysc_Content_Obj.StemType,
        //                Answer = trueFalseCon.Ysc_Answer_Obj.Answer,
        //                SubjectType = trueFalseCon.Ysc_SubjectType,
        //                Analysis = trueFalseCon.Ysc_Explain,

        //                StudentAnswer = trueFalseAns.Answer,
        //                ResultStars = trueFalseAns.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.填空题:
        //            StuBlankAnswer blankAnswer = studentAnswer as StuBlankAnswer;
        //            Yw_FillInBlankContent fillInBlank = content as Yw_FillInBlankContent;
        //            vm = new FillInBlankReportVm
        //            {
        //                SubjectId = blankAnswer.SubjectId,
        //                Stem = fillInBlank.Ysc_Content_Obj.Stem,
        //                StemType = (int)UeditorType.Text,
        //                Answer = new
        //                {
        //                    Perfect = fillInBlank.Ysc_Answer_Obj.Perfect,
        //                    Correct = fillInBlank.Ysc_Answer_Obj.Correct,
        //                    Other = fillInBlank.Ysc_Answer_Obj.Other
        //                },
        //                SubjectType = fillInBlank.Ysc_SubjectType,
        //                Analysis = fillInBlank.Ysc_Explain,

        //                StudentAnswer = blankAnswer.Answers,
        //                ResultStars = blankAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.选择填空:
        //            StuSelectBlankAnswer selectBlankAnswer = studentAnswer as StuSelectBlankAnswer;
        //            MultipleChoiceFillInBlankContent multipleChoiceFillInBlank = content as MultipleChoiceFillInBlankContent;
        //            List<SubjectOption> options = new List<SubjectOption>();
        //            options.AddRange(multipleChoiceFillInBlank.Ysc_Content_Obj.SubjectOptions);
        //            options.AddRange(multipleChoiceFillInBlank.Ysc_Content_Obj.SubjectGOptions);
        //            UeditorType contentType =
        //                (UeditorType)multipleChoiceFillInBlank.Ysc_Content_Obj.ContentType;
        //            RestoreUrl(options, contentType);

        //            string mBlankStem = UeditorContentFactory.Blank(
        //                multipleChoiceFillInBlank.Ysc_Content_Obj.Stem);
        //            vm = new MultipleChoiceFillInBlankReportVm
        //            {
        //                SubjectId = selectBlankAnswer.SubjectId,
        //                Stem = mBlankStem,
        //                StemType = (int)UeditorType.Text,
        //                Answer = multipleChoiceFillInBlank.Ysc_Answer_Obj.Answers,
        //                Options = options.OrderBy(s => Array.IndexOf(selectBlankAnswer.OptionSequences.ToArray(), s.Key)).ToList(),
        //                OptionType = multipleChoiceFillInBlank.Ysc_Content_Obj.ContentType,
        //                SubjectType = multipleChoiceFillInBlank.Ysc_SubjectType,
        //                Analysis = multipleChoiceFillInBlank.Ysc_Explain,

        //                StudentAnswer = selectBlankAnswer.Answers,
        //                ResultStars = selectBlankAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.连线题:
        //            StuMatchAnswer matchAnswer = studentAnswer as StuMatchAnswer;

        //            Yw_MatchContent match = content as Yw_MatchContent;
        //            var contentObj = match.Ysc_Content_Obj;

        //            var leftOptions = new List<SubjectOption>();
        //            leftOptions.AddRange(contentObj.LeftOptions);
        //            RestoreUrl(leftOptions, (UeditorType)contentObj.LeftOptionContentType);

        //            var rightOptions = new List<SubjectOption>();
        //            rightOptions.AddRange(contentObj.RightOptions);
        //            RestoreUrl(rightOptions, (UeditorType)contentObj.RightOptionContentType);
        //            vm = new MatchReportVm
        //            {
        //                SubjectId = matchAnswer.SubjectId,
        //                Stem = UeditorContentFactory.RestoreUrl(
        //                    contentObj.Stem,
        //                    (UeditorType)contentObj.StemType),
        //                StemType = match.Ysc_Content_Obj.StemType,
        //                Answer = match.Ysc_Answer_Obj.Answers,
        //                SubjectType = match.Ysc_SubjectType,
        //                LeftOptions = leftOptions.OrderBy(s => Array.IndexOf(matchAnswer.LeftOptionSequences.ToArray(), s.Key)).ToList(),
        //                RightOptions = rightOptions.OrderBy(s => Array.IndexOf(matchAnswer.RightOptionSequences.ToArray(), s.Key)).ToList(),
        //                LeftOptionType = match.Ysc_Content_Obj.LeftOptionContentType,
        //                RightOptionType = match.Ysc_Content_Obj.RightOptionContentType,
        //                Analysis = match.Ysc_Explain,

        //                StudentAnswer = matchAnswer.Answers,
        //                ResultStars = matchAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.主观题:
        //            StuFreeAnswer freeAnswer = studentAnswer as StuFreeAnswer;
        //            Yw_SubjectFreeContent free = content as Yw_SubjectFreeContent;
        //            string freeStem = free.Ysc_Content_Obj.Stem;
        //            if (free.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
        //            {
        //                freeStem = fileUrlPrefix + freeStem;
        //            }

        //            vm = new FreeReportVm
        //            {
        //                SubjectId = freeAnswer.SubjectId,
        //                Stem = freeStem,
        //                StemType = free.Ysc_Content_Obj.StemType,
        //                Answer = free.Ysc_Answer_Obj.Answer,
        //                SubjectType = free.Ysc_SubjectType,
        //                Analysis = free.Ysc_Explain,
        //                Standard = free.Ysc_Content_Obj.ScoreRules,

        //                StudentAnswer = freeAnswer.Answer,
        //                ResultStars = freeAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.圈点批注标色:
        //            StuMarkColorAnswer markColorAnswer = studentAnswer as StuMarkColorAnswer;
        //            Yw_SubjectMarkContent mark = content as Yw_SubjectMarkContent;

        //            string stem = mark.Ysc_Content_Obj.Stem;
        //            if (mark.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
        //            {
        //                stem = fileUrlPrefix + stem;
        //            }
        //            string markContent = ReplacePlaceholder(mark.Ysc_Content_Obj.Content);

        //            vm = new MarkReportVm
        //            {
        //                SubjectId = markColorAnswer.SubjectId,
        //                Stem = stem,
        //                StemType = mark.Ysc_Content_Obj.StemType,
        //                Answer = mark.Ysc_Answer_Obj.Answers,
        //                SubjectType = mark.Ysc_SubjectType,
        //                Analysis = mark.Ysc_Explain,
        //                Content = markContent,
        //                Color = mark.Ysc_Content_Obj.Color,

        //                StudentAnswer = markColorAnswer.Answers,
        //                ResultStars = markColorAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        case SubjectTypeEnum.圈点批注断句:
        //            StuMarkCutAnswer markCutAnswer = studentAnswer as StuMarkCutAnswer;
        //            Yw_SubjectMark2Content mark2 = content as Yw_SubjectMark2Content;

        //            string mark2Stem = mark2.Ysc_Content_Obj.Stem;
        //            if (mark2.Ysc_Content_Obj.StemType == (int)UeditorType.Image)
        //            {
        //                mark2Stem = fileUrlPrefix + mark2Stem;
        //            }
        //            string mark2Content = mark2.Ysc_Content_Obj.Content?
        //                .Replace("/", string.Empty);
        //            vm = new Mark2ReportVm
        //            {
        //                SubjectId = markCutAnswer.SubjectId,
        //                Stem = mark2Stem,
        //                StemType = mark2.Ysc_Content_Obj.StemType,
        //                Answer = mark2.Ysc_Answer_Obj.Answers,
        //                SubjectType = mark2.Ysc_SubjectType,
        //                Analysis = mark2.Ysc_Explain,
        //                Content = mark2Content,
        //                Color = mark2.Ysc_Content_Obj.Color,

        //                StudentAnswer = markCutAnswer.Answers,
        //                ResultStars = markCutAnswer.ResultStars,
        //                Difficulty = subject.Ysj_Difficulty
        //            };
        //            break;
        //        default:
        //            break;
        //    }
        //    return vm;
        //}

        //public static string ReplacePlaceholder(string content)
        //{
        //    if (string.IsNullOrWhiteSpace(content))
        //    {
        //        return content;
        //    }

        //    Regex regex = new Regex(@"{:(\S+?)}");
        //    MatchCollection coll = regex.Matches(content);
        //    if (coll.Count > 0)
        //    {
        //        foreach (Match item in coll)
        //        {
        //            string expression = item.Value;
        //            string text = item.Groups[1].Value;
        //            content = content.Replace(expression, text);
        //        }
        //    }
        //    return content;
        //}

        //private static void RestoreUrl(List<SubjectOption> options, UeditorType type)
        //{
        //    options.ForEach((o) =>
        //    {
        //        o.Text = UeditorContentFactory.RestoreUrl(o.Text, type);
        //    });
        //}
        #endregion
    }
}