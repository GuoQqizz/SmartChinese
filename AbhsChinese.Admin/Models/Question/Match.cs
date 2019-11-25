using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Question
{
    public class Match : QuestionInputModel
    {
        public UeditorType StemType { get; set; }
        public UeditorType TitleOptionContentType { get; set; }
        public UeditorType AnswertOptionContentType { get; set; }
        public IList<string> Title { get; set; }

        public IList<int> LinedAnswers { get; set; }
        public IList<string> Answer { get; set; }

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.连线题;
        }
    }
}