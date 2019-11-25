using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.OwnOrder
{
    public class OrderDetailViewModel
    {
        private string gradeName;
        private string courseTypeName;
        private string payTimeStr = "";
        private string payTypeStr = "";
        private decimal baseVoucherAmount = 0M;
        private decimal schoolVoucherAmount = 0M;
        private string orderStatusStr = "";
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
        /// <summary>
        /// 教材版本（目前只有部编版）
        /// </summary>
        public string TextbookVersion { get; set; } = "部编版";

        public int OrderId { get; set; }
        public string OrderNo { get; set; }

        public decimal OrderAmount { get; set; }
        public decimal PayAmount { get; set; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.StudentOrderStatus"/>
        /// </summary>
        public int OrderStatus { get; set; }
        public DateTime OrderTime { get; set; }
        public string GradeName
        {
            set
            {
                gradeName = value;
            }
            get
            {
                return CustomEnumHelper.Parse(typeof(GradeEnum), Grade);
            }
        }

        public string CourseTypeName
        {
            set
            {
                courseTypeName = value;
            }
            get
            {
                return CustomEnumHelper.Parse(typeof(CourseCategoryEnum), CourseType);
            }
        }
        public int StudentId { get; set; }
        public string StudentAccount { get; set; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.CashPayTypeEnum"/>
        /// </summary>
        public int PayType { get; set; }
        public string PayTypeStr
        {
            set
            {
                payTypeStr = value;
            }
            get
            {
                switch (PayType)
                {
                    case (int)CashPayTypeEnum.微信:
                        payTypeStr = "微信支付";
                        break;
                }
                return payTypeStr;
            }
        }
        public DateTime PayTime { get; set; }
        public string PayTimeStr
        {
            set
            {
                payTimeStr = value;
            }
            get
            {
                return OrderStatus == (int)StudentOrderStatus.已支付 ? PayTime.ToString("yyyy-MM-dd HH:mm:ss") : "";
            }
        }

        /// <summary>
        /// 现金支付
        /// </summary>
        public OrderPayViewModel OrderCashPay { set; get; }
        /// <summary>
        /// 优惠券支付
        /// </summary>
        public List<OrderPayViewModel> OrderVoucherPayList { set; get; }
        public string SchoolName { get; set; }

        public decimal BaseVoucherAmount
        {
            get
            {
                return baseVoucherAmount;
            }

            set
            {
                baseVoucherAmount = value;
            }
        }

        public decimal SchoolVoucherAmount
        {
            get
            {
                return schoolVoucherAmount;
            }

            set
            {
                schoolVoucherAmount = value;
            }
        }

        public string OrderStatusStr
        {
            get
            {
                return CustomEnumHelper.Parse(typeof(StudentOrderStatus), OrderStatus);
            }

            set
            {
                orderStatusStr = value;
            }
        }
        /// <summary>
        /// 是否已购买过该课程，如果订单是已取消状态，且没有成功购买过此课程才可以继续购买
        /// </summary>
        public bool IsHaveThisCourse { set; get; }

    }
    public class OrderPayViewModel
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