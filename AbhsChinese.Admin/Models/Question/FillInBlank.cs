using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Admin.Models.Question
{
    public class BlankAnswer
    {
        public IList<string> Blanks { get; set; }
    }

    public class FillInBlank : QuestionInputModel
    {
        /// <summary>
        /// 正确答案
        /// </summary>
        public BlankAnswer Correct { get; set; } = new BlankAnswer();

        /// <summary>
        /// 勉强答案
        /// </summary>
        public BlankAnswer Other { get; set; } = new BlankAnswer();

        /// <summary>
        /// 最佳答案
        /// </summary>
        public BlankAnswer Perfect { get; set; } = new BlankAnswer();

        protected override int GetSubjectType()
        {
            return (int)SubjectTypeEnum.填空题;
        }
    }
}