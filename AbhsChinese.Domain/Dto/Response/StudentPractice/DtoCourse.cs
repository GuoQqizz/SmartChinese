using AbhsChinese.Domain.Enum;

namespace AbhsChinese.Domain.Dto.Response.StudentPractice
{
    public class DtoCourse
    {
        public int CourseId { get; set; }

        /// <summary>
        /// 课程名字
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 课程类型
        /// </summary>
        public CourseCategoryEnum CourseType { get; set; }

        /// <summary>
        /// 下一个课时的序号
        /// </summary>
        public int NextLessonIndex { get; set; }
        public int Grade { get; set; }
        public string ClassName { get; set; }
        public string SchoolName { get; set; }
        public int LessonCount { get; set; }
    }
}