using AbhsChinese.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CashVoucher
{
    public class CahsVoucherDetailsSearch:Search
    {
        public int CashVoucherId { get; set; }

        public int Status { get; set; }

        public string UsedReferNo { get; set; }
    }
}