using AbhsChinese.Domain.JsonEntity.Answer;
using System.Collections.Generic;

namespace AbhsChinese.Web.Models.Subjects
{
    public class PracticeResult
    {
        public IList<StudentAnswerBase> Answer { get; set; }
        public int StudentId { get; set; }
        public int StudyTaskId { get; set; }
        public int UseTime { get; set; }
    }
}