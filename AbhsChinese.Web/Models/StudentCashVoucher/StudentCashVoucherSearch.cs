using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models
{
    public class StudentCashVoucherSearch
    {
        /// <summary>
        /// 使用金额要求
        /// </summary>
        public int OrderAmountLimit { get; set; }

        /// <summary>
        /// 适用年级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 适用课程类型
        /// </summary>
        public int CourseType { get; set; }

        /// <summary>
        /// 适用课程Id
        /// </summary>
        public int CourseId { get; set; }
    }
}