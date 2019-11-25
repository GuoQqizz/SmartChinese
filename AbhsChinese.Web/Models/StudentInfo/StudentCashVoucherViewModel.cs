using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.StudentInfo
{
    public class StudentCashVoucherViewModel
    {
        public int Ysv_Id { get; set; }

        public int Ysv_StudentId { get; set; }

        public int Ysv_CashVoucherId { get; set; }

        public string Ysv_VoucherNo { get; set; }

        public int Ysv_VoucherType { get; set; }

        public int Ycv_ExpireType { get; set; }

        public DateTime Ysv_ExpireDate { get; set; }

        public DateTime Ysv_TakenTime { get; set; }

        public string ExpireTime {
            get
            {
                if(Ycv_ExpireType==(int)ExpireTypeEnum.长期有效)
                {
                    return "长期有效";
                }
                return Ysv_TakenTime.ToString("yyyy-MM-dd") + " - " + Ysv_ExpireDate.ToString("yyyy-MM-dd");
            }
        }



        //现金券表
        public int Ycv_Id { get; set; }
        public string Ycv_Name { get; set; }
        public int Ycv_VoucherType { get; set; }
        public int Ycv_SchoolId { get; set; }
        public decimal Ycv_Amount { get; set; }
        public decimal Ycv_OrderAmountLimit { get; set; }
        public int Ycv_ApplyScopeType { get; set; }
        public int Ycv_ApplyGrade { get; set; }
        public int Ycv_CourseType { get; set; }
        public int Ycv_CourseId { get; set; }

        public string OrderAmountLimit
        {
            get
            {
                if (Ycv_OrderAmountLimit > 0)
                {
                    return "满￥" + Convert.ToInt32(Ycv_OrderAmountLimit) + "可用";
                }
                return "无限制";
            }
        }

        public string ApplyScoreType
        {
            get
            {
                if (Ycv_ApplyScopeType == (int)CashApplyScopeTypeEnum.全部课程)
                {
                    return "全部课程可用";
                }
                else if (Ycv_ApplyScopeType == (int)CashApplyScopeTypeEnum.指定类型)
                {
                    return "适用" + CustomEnumHelper.Parse(typeof(GradeEnum), Ycv_ApplyGrade) + "  " + CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycv_CourseType);
                }
                else if (Ycv_ApplyScopeType == (int)CashApplyScopeTypeEnum.指定课程)
                {
                    return "适用" + Ycs_Name;
                }
                return "";
            }
        }

        public string ApplySchool
        {
            get
            {
                if (Ycv_SchoolId > 0)
                {
                    return "适用" + Bsl_SchoolName;
                }
                else
                {
                    return "适用全国校区";
                }
            }
        }

        public string VoucherCategory
        {
            get
            {
                if (Ycv_SchoolId > 0)
                {
                    return "校区券";
                }
                else
                {
                    return "全国券";
                }
            }
        }


        //校区表
        public string Bsl_SchoolName { get; set; }

        //课程表
        public string Ycs_Name { get; set; }

        public string ExpireDate { get; set; }
        public string TakenTime { get; set; }
        public string UsedTime { get; set; }
    }
}