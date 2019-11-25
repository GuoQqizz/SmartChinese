using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using AbhsChinese.Domain.JsonEntity.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response.Student
{
    public class DtoStudentWrongSubjectInfo
    {
        public string CourseName { get; set; }
        public string CourseLessonName { get; set; }
        public int Yws_StudentId { get; set; }
        public int Yws_Id { get; set; }
        public int Yws_CourseId { get; set; }
        public int Yws_LessonId { get; set; }
        public int Yws_WrongBookId { get; set; }
        public int Yws_Source { get; set; }
        public string SourceStr => CustomEnumHelper.Parse(typeof(StudyWrongSourceEnum), Yws_Source);
        public int Yws_WrongSubjectId { get; set; }
        public int Yws_SubjectType { get; set; }
        public int Yws_KnowledgeId { get; set; }
        public string Yws_StudentAnswer { get; set; }
        public int Yws_RemoveTryCount { get; set; }
        /// <summary>
        /// <see cref="StudyWrongStatusEnum"/>
        /// </summary>
        public int Yws_Status { get; set; }

        public DateTime Yws_CreateTime { get; set; }
        public string CreateTimeStr => Yws_CreateTime.ToString("yyyy-MM-dd");

        public DateTime BookCreateTime { get; set; }
        public string BookCreateTimeStr => BookCreateTime.ToString("yyyy-MM-dd");


        /// <summary>
        /// 当前错题本下的所有错题
        /// Yw_StudentWrongSubject 表主键Id
        /// </summary>
        public List<int> WrongSubjectIds { get; set; }
    }
}
