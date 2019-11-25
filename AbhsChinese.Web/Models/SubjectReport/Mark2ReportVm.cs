using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public class Mark2ReportVm:ReportVm
    {
        public SubjectColorEnum Color { get; set; }
        public string Content { get; set; }
        public int OptionType { get; set; }
    }
}