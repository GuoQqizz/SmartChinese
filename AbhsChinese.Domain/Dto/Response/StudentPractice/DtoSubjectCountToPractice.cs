namespace AbhsChinese.Domain.Dto.Response.StudentPractice
{
    public class DtoSubjectCountToPractice
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 课后练习题id
        /// </summary>
        public int SubjectId { get; set; }
        /// <summary>
        /// 练习过的题的数量
        /// </summary>
        public int SubjectCount { get; set; }
    }
}