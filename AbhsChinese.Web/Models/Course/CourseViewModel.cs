using AbhsChinese.Code.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.Course
{
    public class CourseViewModel
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
    }

    public class CourseSet
    {
        public IEnumerable<CourseViewModel> DataList { get; set; }

        public bool HasMore { get; set; }

        public bool IncludePrice { get; set; }
    }
}