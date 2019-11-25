using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoAvailableVoucher
    {
        public int Ycv_Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Ycv_Name { get; set; }
        /// <summary>
        /// 现金券类型
        /// </summary>
        public int Ycv_VoucherType { get; set; }
        /// <summary>
        /// 适用校区
        /// </summary>
        public int Ycv_SchoolId { get; set; }
        /// <summary>
        /// 发行量
        /// </summary>
        public int Ycv_PublishCount { get; set; }
        /// <summary>
        /// 面值
        /// </summary>
        public decimal Ycv_Amount { get; set; }
        /// <summary>
        /// 每人限领数量
        /// </summary>
        public int Ycv_LimitByPerson { get; set; }
        /// <summary>
        /// 适用门槛
        /// </summary>
        public decimal Ycv_OrderAmountLimit { get; set; }
        /// <summary>
        /// 有效期类型
        /// </summary>
        public int Ycv_ExpireType { get; set; }
        /// <summary>
        /// 有效期类型  1：日期范围
        /// </summary>
        public DateTime Ycv_ExpireDate { get; set; }
        /// <summary>
        /// 有效期类型  2：固定天数
        /// </summary>
        public int Ycv_ExpireDayCount { get; set; }
        /// <summary>
        /// 可使用课程类型  1：全部类型 2：指定类型 3：指定课程
        /// </summary>
        public int Ycv_ApplyScopeType { get; set; }
        /// <summary>
        /// 适用年级
        /// </summary>
        public int Ycv_ApplyGrade { get; set; }
        /// <summary>
        /// 适用类型
        /// </summary>
        public int Ycv_CourseType { get; set; }
        /// <summary>
        /// 可使用课程Id（名称）
        /// </summary>
        public int Ycv_CourseId { get; set; }
        /// <summary>
        /// 可赠券课程
        /// </summary>
        public int Ycv_RelatedCourseId { get; set; }
        /// <summary>
        /// 可叠加券
        /// </summary>
        public int Ycv_UseWithVoucherType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Ycv_Remark { get; set; }
        /// <summary>
        /// 已领取数量
        /// </summary>
        public int Ycv_TakenCount { get; set; }

        public string Ycv_CourseKey { get; set; }

        public int Ycv_Status { get; set; }

        public DateTime Ycv_CreateTime { get; set; }

        public int Ycv_Creator { get; set; }

        public DateTime Ycv_UpdateTime { get; set; }

        public int Ycv_Editor { get; set; }

        public int Ysv_CashVoucherId { get; set; }
    }
}
