using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public class MultipleChoiceFillInBlankReportVm:ReportVm
    {
        public IList<SubjectOption> Options { get; set; }
        public int OptionType { get; set; }
    }
}