using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class MatchAnswerObj
    {
        /// <summary>
        /// key为leftoption的编号
        /// value为rightoption的编号
        /// </summary>
        //public List<Dictionary<int,int>> Answers { get; set; }
        //public Dictionary<int,int> Answers { get; set; }
        public IList<int[]> Answers { get; set; }
    }
}