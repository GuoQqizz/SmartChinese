using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoSelectCenterCourseDetailResult
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

        public decimal BaseVoucherPrice
        {
            set; get;
        }

        public decimal SchoolVoucherPrice
        {
            set; get;
        }
        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 课程详情
        /// </summary>
        public string Introduction
        {
            set; get;
        }
        /// <summary>
        /// 课程安排
        /// </summary>
        public string Arrange
        {
            set; get;
        }

        public bool IsHaveThisCourse { set; get; }
        public bool IsBindSchool { set; get; }

        public int StudentApplySchoolStatus { set; get; }

        public string StudentApplySchoolName { set; get; }
    }
}
