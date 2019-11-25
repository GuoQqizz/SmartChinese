using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request
{
    /// <summary>
    /// 根据学生信息获取
    /// </summary>
    public class DtoLessonUnitSearch : DtoSearch
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseID { get; set; }
        /// <summary>
        /// 课时id
        /// </summary>
        public int LessonID { get; set; }
        /// <summary>
        /// 学生id
        /// </summary>
        public int StudentID { get; set; }
    }
}
