using AbhsChinese.Domain.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.School.Models.CashVoucher
{

    public class CashVoucherSearch : DtoSearch
    {
        public int SchoolId { get; set; }
        public int Id { get; set; }

        public string Name { get; set; }

        public int Status { get; set; }

        //public int CashType { get; set; }
    }
}