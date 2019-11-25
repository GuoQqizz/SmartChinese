using AbhsChinese.Domain.Dto.Response.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.LearningCenter
{
    public class StudentCourseInfoViewModel: DtoStudentCourseInfo
    {
        /// <summary>
        /// 已学过课程
        /// </summary>
        public List<DtoStudentLessonInfo> StudiedLesson { get; set; } = new List<DtoStudentLessonInfo>();
        /// <summary>
        /// 未学过课程
        /// </summary>
        public List<DtoStudentLessonInfo> NotStudyLesson { get; set; } = new List<DtoStudentLessonInfo>();
    }
}