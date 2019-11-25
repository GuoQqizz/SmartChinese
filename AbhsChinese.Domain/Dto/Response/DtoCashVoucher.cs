using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoCashVoucher
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

        public string SchoolName { get; set; }

        public string Ycv_CourseKey { get; set; }

        public int Ycv_Status { get; set; }

        public DateTime Ycv_CreateTime { get; set; }

        public int Ycv_Creator { get; set; }

        public DateTime Ycv_UpdateTime { get; set; }

        public int Ycv_Editor { get; set; }

        /// <summary>
        /// 适用课程
        /// </summary>
        public string Ycs_Name { get; set; }

        /// <summary>
        /// 现金券类型
        /// </summary>
        public string VoucherType => CustomEnumHelper.Parse(typeof(VoucherTypeEnum), Ycv_VoucherType);
        /// <summary>
        /// 适用年级
        /// </summary>
        public string Grade
        {
            get
            {
                if (Ycv_ApplyGrade == 0)
                {
                    return "全部年级";
                }
                return CustomEnumHelper.Parse(typeof(GradeEnum), Ycv_ApplyGrade);
            }
        }
        /// <summary>
        /// 适用类型
        /// </summary>
        public string CourseType
        {
            get
            {
                if(Ycv_CourseType==0)
                {
                    return "全部类型";
                }
                return CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycv_CourseType);
            }
        }
        /// <summary>
        /// 有效期  日期范围
        /// </summary>
        public string ExpireDate => "截止至" + Ycv_ExpireDate.ToString("yyyy-MM-dd");

        public string AvailableType
        {
            get
            {
                if (Ycv_ApplyScopeType == (int)CashApplyScopeTypeEnum.指定类型)
                {
                    return CustomEnumHelper.Parse(typeof(GradeEnum), Ycv_ApplyGrade) + "-" + CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycv_CourseType);
                }
                else if (Ycv_ApplyScopeType == (int)CashApplyScopeTypeEnum.指定课程)
                {
                    return Ycs_Name;
                }
                return "全部课程";
            }
        }

        //（冗余）现金券明细页面需要
        public int UsedCount { get; set; }
    }
}
