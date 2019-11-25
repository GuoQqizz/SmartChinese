using AbhsChinese.Domain.JsonEntity.Answer;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Request.StudyTask
{
    public class StudyTaskPracticeResult
    {
        /// <summary>
        /// 答题结果对象
        /// </summary>
        public List<StudentAnswerBase> Answers { get; set; }

        /// <summary>
        /// 课程id
        /// </summary>
        public int CoruseId { get; set; }

        /// <summary>
        /// 课时id
        /// </summary>
        public int LessonId { get; set; }

        /// <summary>
        /// 课后任务id
        /// </summary>
        public int StudyTaskId { get; set; }

        /// <summary>
        /// 课时id
        /// </summary>
        public int UnitID { get; set; }

        /// <summary>
        /// 学习时间(秒)
        /// </summary>
        public int UseTime { get; set; }
    }
}