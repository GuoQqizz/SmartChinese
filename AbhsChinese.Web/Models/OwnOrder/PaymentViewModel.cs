using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.OwnOrder
{
    public class PaymentViewModel
    {
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

        public decimal OrderAmount { get; set; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.StudentOrderStatus"/>
        /// </summary>
        public int OrderStatus { get; set; }
        public DateTime OrderTime { get; set; }
        private string gradeName;
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

        private string courseTypeName;
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
    }
}