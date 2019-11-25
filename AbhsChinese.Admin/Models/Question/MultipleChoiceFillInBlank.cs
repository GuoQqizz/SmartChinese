using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Question
{
    public class MultipleChoiceFillInBlank : QuestionInputModel
    {
        //public IList<int> Positions { get; set; }
        //public IList<int> GPositions { get; set; }
        public IList<int> AnswerIndexes { get; set; }
        public UeditorType ContentType { get; set; }
        /// <summary>
        /// 答案选项
        /// </summary>
        public IList<string> Options { get; set; }

        /// <summary>
        /// 干扰项
        /// </summary>
        public IList<string> Goptions { get; set; }

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.选择填空;
        }
    }
}