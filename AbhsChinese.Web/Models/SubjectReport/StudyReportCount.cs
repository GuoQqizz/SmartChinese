using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public class StudyReportCount
    {
        public int AllCount => LessonCount + TaskCount + PracticeCount;

        public int LessonCount { get; set; }

        public int TaskCount { get; set; }

        public int PracticeCount { get; set; }

        public int Active { get; set; }
    }
}