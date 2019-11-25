using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    /// <summary>
    /// 学生学习课程信息
    /// </summary>
    public class DtoStudentLessonInfo
    {
        /// <summary>
        /// 课时id
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// 课时编号
        /// </summary>
        public int LessonIndex { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string LessonName { get; set; }
        /// <summary>
        /// 是否完成学习
        /// </summary>
        public bool IsStudyOver { get; set; }

        /// <summary>
        /// 是否开放学习
        /// </summary>
        public bool CanStudy { get; set; }
    }
}
