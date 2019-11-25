using AbhsChinese.Domain.Enum;
using System;

namespace AbhsChinese.Domain.Dto.Request.StudyTask
{
    public class DtoStudyTaskListItem
    {
        /// <summary>
        /// 对应课程
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 第几个课时
        /// </summary>
        public int LessonIndex { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public StudentTaskStatusEnum Status { get; set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public int SubjectCount { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 课后任务id
        /// </summary>
        public int StudyTaskId { get; set; }
        /// <summary>
        /// 学生任务记录
        /// </summary>
        public int StudentTaskId { get; set; }

    }
}