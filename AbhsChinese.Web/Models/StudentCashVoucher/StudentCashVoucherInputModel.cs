using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models
{
    public class StudentCashVoucherInputModel
    {
        /// <summary>
        /// 学生Id
        /// </summary>
        public int StudentId { get; set; }

        /// <summary>
        /// 现金券Id
        /// </summary>
        public int CashVoucherId { get; set; }

        /// <summary>
        /// 现金券领取方式
        /// </summary>
        public VoucherGotTypeEnum GotType { get; set; }

        /// <summary>
        /// 现金券领取关联Id
        /// </summary>
        public int GotReferId { get; set; }
    }
}