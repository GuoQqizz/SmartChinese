using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;

namespace AbhsChinese.Web.Models.SubjectReport
{
    /// <summary>
    /// 选择题
    /// </summary>
    public class MultipleChoiceReportVm : ReportVm
    {
        public IList<SubjectOption> Options { get; set; }
        public int OptionType { get; set; }
    }
}