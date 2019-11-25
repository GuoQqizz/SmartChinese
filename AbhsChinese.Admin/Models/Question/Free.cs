using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Admin.Models.Question
{
    public class Free : QuestionInputModel
    {
        public UeditorType StemType { get; set; }
        public string Answer { get; set; }
        public string ScoreRules { get; set; }

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.主观题;
        }
    }
}