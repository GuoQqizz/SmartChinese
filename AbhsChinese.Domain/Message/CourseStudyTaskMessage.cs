using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Message
{
    public class CourseStudyTaskMessage
    {
        public int StudentId
        {
            set; get;
        }

        public int CourseId
        {
            set; get;
        }

        public int LessonId
        {
            set; get;
        }

        public int StudySeconds
        {
            set; get;
        }

        public int GetCoins
        {
            set; get;
        }

        public int SubjectCount
        {
            set; get;
        }

        public DateTime AsOfDate
        {
            set; get;
        }
    }
}
