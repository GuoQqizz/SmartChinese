using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class SubjectSelectAnswerObj
    {
        /// <summary>
        /// 选择题答案
        /// 0：A   1：B   2：C   3：D   
        /// </summary>
        public List<int> Answers { set; get; }
    }
}