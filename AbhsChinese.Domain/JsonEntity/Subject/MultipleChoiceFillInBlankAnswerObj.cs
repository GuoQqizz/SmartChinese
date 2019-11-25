using System;
using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class MultipleChoiceFillInBlankAnswerObj
    {
        //public Dictionary<int,int> Answers { get; set; }
        /// <summary>
        /// 答案
        /// 格式为：[1,1] [2,2] [3,3]
        /// </summary>
        public IList<int[]> Answers { get; set; }
        //1,1；2,2，；3,3，
    }
}