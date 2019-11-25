using AbhsChinese.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CashVoucher
{
    public class CashVoucherSearch:Search
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        public int CashType { get; set; }
    }
}