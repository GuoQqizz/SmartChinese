using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class StuOwnVoucherSearch
    {
        public int PageIndex { get; set; }
        
        public StudentCashVoucherStatusEnum Status { get; set; }
    }
}