using AbhsChinese.Code.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoOwnCashVoucher
    {
        /// <summary>
        /// 现金券Id
        /// </summary>
        public int Ycv_Id { get; set; }

        /// <summary>
        /// 现金券名称
        /// </summary>
        public string Ycv_Name { get; set; }

        /// <summary>
        /// 校区Id
        /// </summary>
        public int Ycv_SchoolId { get; set; }

        /// <summary>
        /// 现金券类型
        /// </summary>
        public int Ycv_VoucherType { get; set; }
        
        /// <summary>
        /// 面值
        /// </summary>
        public decimal Ycv_Amount { get; set; }

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
        /// 适用类型
        /// </summary>
        public int Ycv_ApplyScopeType { get; set; }

        /// <summary>
        /// 适用年级
        /// </summary>
        public int Ycv_ApplyGrade { get; set; }

        /// <summary>
        /// 适用课程类型
        /// </summary>
        public int Ycv_CourseType { get; set; }
        
        /// <summary>
        /// 可使用课程Id（名称）
        /// </summary>
        public int Ycv_CourseId { get; set; }
        
        /// <summary>
        /// 创建时间（有效期开始时间）
        /// </summary>
        public DateTime Ycv_CreateTime { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string Bsl_SchoolName { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Ycs_Name { get; set; }



        /// <summary>
        /// 学生现金券Id
        /// </summary>
        public int Ysv_Id { get; set; }
        /// <summary>
        /// 有效期（学生拥有的）
        /// </summary>
        public DateTime Ysv_ExpireDate { get; set; }

        /// <summary>
        /// 领取日期（学生拥有的）
        /// </summary>
        public DateTime Ysv_TakenTime { get; set; }

        /// <summary>
        /// 1：学生已拥有且可用的 2：学生可领取且可用的
        /// </summary>
        public int IsAvailable { get; set; }
    }
}
