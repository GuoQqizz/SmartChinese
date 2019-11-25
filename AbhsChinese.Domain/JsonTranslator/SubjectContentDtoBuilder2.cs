using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Dto.Response.Subject;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;

namespace AbhsChinese.Domain.JsonTranslator
{
    public static class SubjectContentDtoBuilder2
    {
        private static readonly string ossUrl = ConfigurationManager.AppSettings["OssHostUrl"];

        public static DtoSubjectContent Create(Yw_SubjectContent content, Yw_Subject subject)
        {
            Check.IfNull(content, nameof(content));
            Check.IfNull(subject, nameof(subject));

            DtoSubjectContent dto = null;
            dynamic contentObj = null;
            UeditorType optionType = UeditorType.Text;
            SubjectTypeEnum subjectType = (SubjectTypeEnum)content.Ysc_SubjectType;
            switch (subjectType)
            {
                case SubjectTypeEnum.选择题:
                    Yw_SubjectSelectContent select = content as Yw_SubjectSelectContent;
                    contentObj = select.Ysc_Content_Obj;
                    optionType = (UeditorType)contentObj.ContentType;
                    AppendUrlIfIsImage(contentObj.Options, optionType);
                    dto = new DtoSelectSubjectContent
                    {
                        Random = contentObj.Random,
                        Answer = select.Ysc_Answer_Obj.Answers,
                        Options = contentObj.Options,
                        OptionType = optionType,
                        Display = contentObj.Display
                    };
                    break;

                case SubjectTypeEnum.判断题:
                    Yw_TrueFalseContent trueFalse = content as Yw_TrueFalseContent;
                    contentObj = trueFalse.Ysc_Content_Obj;
                    dto = new DtoTrueFalseSubjectContent
                    {
                        Answer = trueFalse.Ysc_Answer_Obj.Answer
                    };
                    break;

                case SubjectTypeEnum.填空题:
                    Yw_FillInBlankContent fillInBlank = content as Yw_FillInBlankContent;
                    contentObj = fillInBlank.Ysc_Content_Obj;
                    dto = new DtoFillInBlankSubjectContent
                    {
                        Answer = new
                        {
                            Perfect = fillInBlank.Ysc_Answer_Obj.Perfect,
                            Correct = fillInBlank.Ysc_Answer_Obj.Correct,
                            Other = fillInBlank.Ysc_Answer_Obj.Other
                        }
                    };
                    break;

                case SubjectTypeEnum.选择填空:
                    MultipleChoiceFillInBlankContent multipleChoiceFillInBlank
                        = content as MultipleChoiceFillInBlankContent;
                    contentObj = multipleChoiceFillInBlank.Ysc_Content_Obj;
                    optionType = (UeditorType)contentObj.ContentType;
                    AppendUrlIfIsImage(contentObj.SubjectOptions, optionType);
                    AppendUrlIfIsImage(contentObj.SubjectGOptions, optionType);
                    dto = new DtoSelectFillInBlankSubjectContent
                    {
                        Answer = multipleChoiceFillInBlank.Ysc_Answer_Obj.Answers,
                        //Options = options,
                        OptionType = optionType
                    };
                    break;

                case SubjectTypeEnum.连线题:
                    Yw_MatchContent match = content as Yw_MatchContent;
                    contentObj = match.Ysc_Content_Obj;

                    var leftOptionType = (UeditorType)contentObj.LeftOptionContentType;
                    AppendUrlIfIsImage(contentObj.LeftOptions, leftOptionType);

                    var rightOptionType = (UeditorType)contentObj.RightOptionContentType;
                    AppendUrlIfIsImage(contentObj.RightOptions, rightOptionType);

                    dto = new DtoMatchSubjectContent
                    {
                        Answer = match.Ysc_Answer_Obj.Answers,
                        LeftOptions = contentObj.LeftOptions,
                        RightOptions = contentObj.RightOptions,
                        LeftOptionType = leftOptionType,
                        RightOptionType = rightOptionType
                    };
                    break;

                case SubjectTypeEnum.主观题:
                    Yw_SubjectFreeContent free = content as Yw_SubjectFreeContent;
                    contentObj = free.Ysc_Content_Obj;
                    dto = new DtoFreeSubjectContent
                    {
                        Answer = free.Ysc_Answer_Obj.Answer,
                        Standard = contentObj.ScoreRules
                    };
                    break;

                case SubjectTypeEnum.圈点批注标色:
                    Yw_SubjectMarkContent mark = content as Yw_SubjectMarkContent;
                    contentObj = mark.Ysc_Content_Obj;
                    dto = new DtoMarkSubjectContent
                    {
                        Answer = mark.Ysc_Answer_Obj.Answers,
                        Content = contentObj.Content,
                        Color = mark.Ysc_Content_Obj.Color
                    };
                    break;

                case SubjectTypeEnum.圈点批注断句:
                    Yw_SubjectMark2Content mark2 = content as Yw_SubjectMark2Content;
                    contentObj = mark2.Ysc_Content_Obj;
                    dto = new DtoMark2SubjectContent
                    {
                        Answer = mark2.Ysc_Answer_Obj.Answers,
                        Content = contentObj.Content,
                        Color = contentObj.Color
                    };
                    break;

                default:
                    dto = new DtoDefaultSubjectContent();
                    contentObj = new ExpandoObject();
                    break;
            }
            dto.SubjectId = content.Ysc_SubjectId;
            dto.KnowledgeId = GetKnowledgeId(subject);
            dto.Analysis = content.Ysc_Explain;
            dto.Stem = contentObj.Stem;
            dto.StemType = (UeditorType)contentObj.StemType;
            dto.SubjectType = subjectType;

            return dto;
        }

        private static void AppendUrlIfIsImage(
            IEnumerable<SubjectOption> options,
            UeditorType type)
        {
            if (type == UeditorType.Image)
            {
                foreach (var item in options)
                {
                    item.Text = ossUrl + item.Text;
                }
            }
        }

        private static int GetKnowledgeId(Yw_Subject subject)
        {
            return subject == null ? 0 : subject.Ysj_MainKnowledgeId;
        }
    }
}