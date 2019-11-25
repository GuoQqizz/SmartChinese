using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.Student
{
    /// <summary>
    /// 获取学生课程分页条件
    /// </summary>
    public class DtoStudentCourseSearch : DtoSearch
    {
        /// <summary>
        /// 学生id
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// 完成状态
        /// </summary>
        public bool IsFinished { get; set; }
    }
}
