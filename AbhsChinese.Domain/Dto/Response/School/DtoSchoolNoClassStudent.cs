using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.School
{
    public class DtoSchoolNoClassStudent
    {
        /// <summary>
        /// 学生学习进度表ID
        /// </summary>
        public int Yps_Id { get; set; }
        public int Yps_StudentId { get; set; }
        public int Yps_OrderId { get; set; }
        public int Yps_CourseId { get; set; }
        /// <summary>
        /// 学号
        /// </summary>
        public string Bst_No { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Bst_Name { get; set; }
        /// <summary>
        /// 学生电话
        /// </summary>
        public string Bst_Phone { get; set; }
        public int Bst_Grade { get; set; }
        public string GradeStr => CustomEnumHelper.Parse(typeof(GradeEnum), Bst_Grade);
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Ycs_Name { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int Ycs_CourseType { get; set; }

        public string CourseType => CustomEnumHelper.Parse(typeof(CourseCategoryEnum), Ycs_CourseType);
        /// <summary>
        /// 订单号
        /// </summary>
        public string Yod_OrderNo { get; set; }
        public DateTime Yod_OrderTime { get; set; }

        public string OrderTime => Yod_OrderTime.ToString("yyyy-MM-dd hh:mm:ss");
    }
}
