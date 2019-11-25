using AbhsChinese.Domain.Entity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Domain.JsonEntity.Subject
{
    public class MatchContentObj
    {
        public IList<SubjectOption> LeftOptions { get; set; }
        public IList<SubjectOption> RightOptions { get; set; }
        public string Stem { set; get; }
        public int StemType { get; set; }
        public int LeftOptionContentType { get; set; }
        public int RightOptionContentType { get; set; }
    }
}