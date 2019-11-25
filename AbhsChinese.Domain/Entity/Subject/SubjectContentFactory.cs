using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonTranslator;

namespace AbhsChinese.Domain.Entity.Subject
{
    public class SubjectContentFactory
    {
        public static Yw_SubjectContent Create(Yw_SubjectContent entity)
        {
            if (entity == null)
            {
                return null;
            }
            if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.选择题)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_SubjectSelectContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.主观题)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_SubjectFreeContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.判断题)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_TrueFalseContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.填空题)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_FillInBlankContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.选择填空)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, MultipleChoiceFillInBlankContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.连线题)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_MatchContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.主观题)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_SubjectFreeContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.圈点批注标色)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_SubjectMarkContent>(entity);
            }
            else if (entity.Ysc_SubjectType == (int)SubjectTypeEnum.圈点批注断句)
            {
                return TranslatorFactory.Translator<Yw_SubjectContent, Yw_SubjectMark2Content>(entity);
            }
            return null;
        }
    }
}