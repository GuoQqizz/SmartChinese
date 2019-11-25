using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.Course
{
    public class CourseInputModel
    {
        public string Name { get; set; }
        public CourseCategoryEnum CourseType { get; set; } = CourseCategoryEnum.专项课;
        public CourseStatusEnum Status { get; set; } = CourseStatusEnum.未定价;
        public int Grade { get; set; }
        public int LessonCount { get; set; }
        public int Owner { get; set; }
        public int ResourceGroupId { get; set; }
        public string Employees { get; set; }
        public string Description { get; set; }
        public IList<string> Lessons { get; set; } = new List<string>();
    }
}