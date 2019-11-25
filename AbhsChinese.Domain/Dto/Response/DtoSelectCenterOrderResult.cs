using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoSelectCenterOrderResult
    {
        public int StudentId
        {
            set; get;
        }
        public int CourseId
        {
            set; get;
        }

        public string CourseName
        {
            set; get;
        }

        public string CourseImage
        {
            set; get;
        }

        public int Grade
        {
            set; get;
        }

        public int CourseType
        {
            set; get;
        }

        public int LessonCount
        {
            set; get;
        }
        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }


        public int OrderId { get; set; }

        public decimal OrderAmount { get; set; }
        public decimal PayAmount { get; set; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.StudentOrderStatus"/>
        /// </summary>
        public int OrderStatus { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.CashPayTypeEnum"/>
        /// </summary>
        public int PayType { get; set; }
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 现金支付
        /// </summary>
        public DtoStudentOrderPay OrderCashPay { set; get; }
        /// <summary>
        /// 优惠券支付
        /// </summary>
        public List<DtoStudentOrderPay> OrderVoucherPayList { set; get; }
    }
}
