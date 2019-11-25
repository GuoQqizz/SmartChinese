using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CashVoucher
{
    public class CashVoucherViewModel
    {
        public int Ycv_Id { get; set; }
        
        public string Ycv_Name { get; set; }

        public int Ycv_VoucherType { get; set; }

        public int Ycv_SchoolId { get; set; }

        public int Ycv_PublishCount { get; set; }

        public decimal Ycv_Amount { get; set; }

        public int Ycv_LimitByPerson { get; set; }

        public decimal Ycv_OrderAmountLimit { get; set; }

        public int Ycv_ExpireType { get; set; }

        public DateTime Ycv_ExpireDate { get; set; }

        public int Ycv_ExpireDayCount { get; set; }
        
        public int Ycv_ApplyScopeType { get; set; }

        public int Ycv_ApplyGrade { get; set; }

        public int Ycv_CourseType { get; set; }

        public int Ycv_CourseId { get; set; }

        public int Ycv_RelatedCourseId { get; set; }

        public int Ycv_UseWithVoucherType { get; set; }

        public string Ycv_Remark { get; set; }
       
        public int Ycv_TakenCount { get; set; }

        public int Ycv_Status { get; set; }

        public DateTime Ycv_CreateTime { get; set; }

        public string VoucherType => CustomEnumHelper.Parse(typeof(VoucherTypeEnum), Ycv_VoucherType);

        public string Grade
        {
            get
            {
                if(Ycv_ApplyGrade==0)
                {
                    return "全部年级";
                }
                return CustomEnumHelper.Parse(typeof(GradeEnum), Ycv_ApplyGrade);
            }
        }

        public string CourseType
        {
            get
            {
                if (Ycv_CourseType == 0)
                {
                    return "全部类型";
                }
                return CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycv_CourseType);
            }
        }

        public string CourseIds
        {
            get
            {
                if (Ycv_CourseType == 0)
                {
                    return "全部类型";
                }
                return CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycv_ApplyGrade);
            }
        }
    }
}