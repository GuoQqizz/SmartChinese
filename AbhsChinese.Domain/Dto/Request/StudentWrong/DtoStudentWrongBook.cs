using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.StudentWrong
{
    /// <summary>
    /// 学生错题
    /// </summary>
    public class DtoStudentWrongBook
    {
        public int StudentId { get; set; }
     
        public StudyWrongSourceEnum Source { get; set; }
        public int CourseId { get; set; }
        public int LessonId { get; set; }

        public int LessonProgressId { get; set; }

        public int StudyTaskId { get; set; }
    }
}
