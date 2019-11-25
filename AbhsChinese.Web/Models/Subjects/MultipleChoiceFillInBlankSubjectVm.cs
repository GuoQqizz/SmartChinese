using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using System.Collections.Generic;

namespace AbhsChinese.Web.Models.Subjects
{
    public class MultipleChoiceFillInBlankSubjectVm : SubjectVm
    {
        public IList<SubjectOption> Options { get; set; }
        public int OptionType { get; set; }
    }
}