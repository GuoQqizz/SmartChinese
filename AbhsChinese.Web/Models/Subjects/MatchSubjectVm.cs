using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Web.Models.Subjects
{
    public class MatchSubjectVm : SubjectVm
    {
        public IList<SubjectOption> LeftOptions { get; set; }
        public IList<SubjectOption> RightOptions { get; set; }
        public int LeftOptionType { get; set; }
        public int RightOptionType { get; set; }
    }
}