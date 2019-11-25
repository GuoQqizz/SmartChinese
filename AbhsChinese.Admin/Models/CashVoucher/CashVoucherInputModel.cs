using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CashVoucher
{
    public class CashVoucherInputModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int VoucherType { get; set; }

        public int PublishCount { get; set; }

        public decimal Amount { get; set; }

        public int LimitByPerson { get; set; }

        public int OrderAmountLimit { get; set; }

        public int ExpireType { get; set; }
        public DateTime ExpireDate { get; set; }
        public int ExpireDay { get; set; }

        public int SchoolType { get; set; }
        public int SchoolId { get; set; }

        public int ApplyScopeType { get; set; }
        public int Grade { get; set; }
        public int CourseType { get; set; }
        public int CourseId { get; set; }

        public string Remark { get; set; }

        //成单赠券
        public int RelatedCourseId { get; set; }

        /// <summary>
        /// 可叠加赠券（暂无）
        /// </summary>
        public int UseWithVoucherType { get; set; }
    }
}