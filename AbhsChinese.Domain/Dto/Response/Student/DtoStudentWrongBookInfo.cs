using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoStudentWrongBookInfo
    {
        public string CourseName { get; set; }
        public string CourseLessonName { get; set; }
        public int Ywb_StudentId { get; set; }
        public int Ywb_Id { get; set; }
        public int Ywb_CourseId { get; set; }
        public int Ywb_LessonId { get; set; }
        public int Ywb_LessonProgressId { get; set; }
        public int Ywb_StudyTaskId { get; set; }
        public int Yws_SchoolId { get; set; }
        public int Yws_ClassId { get; set; }
        public int Yws_Source { get; set; }
        public string SourceStr => CustomEnumHelper.Parse(typeof(StudyWrongSourceEnum), Yws_Source);
        public int Yws_WrongCount { get; set; }
        public int Yws_WrongKnowledgeCount { get; set; }
        public int Yws_RemoveCount { get; set; }
        public int Yws_Status { get; set; }
        public DateTime Yws_CreateTime { get; set; }
        public string CreateTimeStr => Yws_CreateTime.ToString("yyyy-MM-dd HH:mm");
    }
}
