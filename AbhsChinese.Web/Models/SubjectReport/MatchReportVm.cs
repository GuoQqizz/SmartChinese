using AbhsChinese.Domain.Entity.Subject;
using AbhsChinese.Domain.JsonEntity.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public class MatchReportVm:ReportVm
    {
        public IList<SubjectOption> LeftOptions { get; set; }
        public IList<SubjectOption> RightOptions { get; set; }
        public int LeftOptionType { get; set; }
        public int RightOptionType { get; set; }
    }
}