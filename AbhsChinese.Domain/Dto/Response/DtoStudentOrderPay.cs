using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoStudentOrderPay
    {
        public int StudentId { set; get; }
        public int CourseId { set; get; }

        public int OrderId { set; get; }
        public int BatchId { set; get; }
        /// <summary>
        /// 如果PayType是现金支付，此字段代表交易流水号，否则代表优惠券领取记录id
        /// </summary>
        public string PayOuterId { set; get; }
        public decimal Amount { set; get; }
        public int Status { set; get; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.StudentOrderPaymnentTypeEnum"/>
        /// </summary>
        public int PayType { set; get; }
        public DateTime PayTime { set; get; }
    }
}
