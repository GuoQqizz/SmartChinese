using AbhsChinese.Domain.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Models.CashVoucher
{
    public class CahsVoucherDetailsSearch : DtoSearch
    {
        public int CashVoucherId { get; set; }

        public int Status { get; set; }

        public string UsedReferNo { get; set; }
    }
}