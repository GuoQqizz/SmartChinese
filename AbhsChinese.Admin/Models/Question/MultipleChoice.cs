using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Question
{
    public class MultipleChoice : QuestionInputModel
    {
        public int Random { get; set; }
        public UeditorType StemType { get; set; } 
        public UeditorType ContentType { get; set; }
        public List<int> Answers { get; set; }
        public int Display { get; set; }
        public IList<string> Options { get; set; } 

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.选择题;
        }
    }
}