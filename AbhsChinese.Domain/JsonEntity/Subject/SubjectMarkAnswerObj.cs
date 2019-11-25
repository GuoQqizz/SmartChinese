using System;
using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    /// <summary>
    /// 圈点批注-标色题的答案
    /// </summary>
    public class SubjectMarkAnswerObj
    {
        /// <summary>
        /// 'Tuple<int, int>'的第一个int存储起始位置
        /// 第一个int存储起始答案的字符长度
        /// </summary>
        //public List<Tuple<int, int>> Answers { get; set; }
        public List<int[]> Answers { get; set; }
    }
}