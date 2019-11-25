using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class StudentOrderViewModel
    {
        public int Yod_Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string Yod_OrderNo { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public int Yod_OrderType { get; set; }

        /// <summary>
        /// 关联订单Id
        /// </summary>
        public int Yod_ReferOrderId { get; set; }

        /// <summary>
        /// 学生Id
        /// </summary>
        public int Yod_StudentId { get; set; }

        /// <summary>
        /// 课程Id
        /// </summary>
        public int Yod_CourseId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Yod_Amount { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal Yod_PayAmount { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime Yod_OrderTime { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public DateTime Yod_PayTime { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int Yod_Status { get; set; }

        /// <summary>
        /// 支付批次
        /// </summary>
        public int Yod_PayBatchId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime Yod_UpdateTime { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string Status => CustomEnumHelper.Parse(typeof(StudentOrderStatus), Yod_Status);

        public string OrderTime => Yod_OrderTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}