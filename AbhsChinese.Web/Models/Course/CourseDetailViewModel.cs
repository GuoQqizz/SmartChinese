using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Course
{
    public class CourseDetailViewModel
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

        public decimal CoursePrice
        {
            set; get;
        }

        public int SellCount
        {
            set; get;
        }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.GradeEnum"/>
        /// </summary>

        public int Grade
        {
            set; get;
        }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.CourseCategoryEnum"/> 
        /// </summary>
        public int CourseType
        {
            set; get;
        }

        public int LessonCount
        {
            set; get;
        }

        public decimal BaseVoucherPrice
        {
            set; get;
        }

        public decimal SchoolVoucherPrice
        {
            set; get;
        }


        public string Description { get; set; }
        /// <summary>
        /// 教材版本（目前只有部编版）
        /// </summary>
        public string TextbookVersion { get; set; } = "部编版";

        public string Introduction
        {
            set; get;
        }
        public string Arrange
        {
            set; get;
        }

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
        public bool IsHaveThisCourse { get; set; }
        public bool IsBindSchool { get; set; }
        /// <summary>
        /// <see cref="AbhsChinese.Domain.Enum.ApplyStatusEnum"/>
        /// </summary>
        public int StudentApplySchoolStatus { set; get; }

        public string StudentApplySchoolName { set; get; }

        /// <summary>
        /// 来源广告Id
        /// </summary>
        public int Ad_Id { get; set; }
    }
}