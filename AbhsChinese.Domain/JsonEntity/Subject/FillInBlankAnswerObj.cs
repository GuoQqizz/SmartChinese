using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class FillInBlankAnswerObj
    {
        /// <summary>
        /// 正确答案1
        /// </summary>
        public IList<string> Correct { get; set; }

        /// <summary>
        /// 勉强答案1
        /// </summary>
        public IList<string> Other { get; set; }

        /// <summary>
        /// 最佳答案1
        /// </summary>
        public IList<string> Perfect { get; set; }
    }
}