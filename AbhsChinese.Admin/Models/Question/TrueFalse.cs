using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Admin.Models.Question
{
    public class TrueFalse : QuestionInputModel
    {
        public UeditorType StemType { get; set; }
        public int Answer { get; set; }

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.判断题;
        }
    }
}