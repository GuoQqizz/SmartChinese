using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Web.Models.StudyTask
{
    public class StudyTaskListViewModel
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime StartTime { get; set; }
        public string DisplayStartTime
        {
            get
            {
                return StartTime.ToString(Clock.Format);
            }
        }
        /// <summary>
        /// 对应课程
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StudentTaskStatusEnum Status { get; set; }
        /// <summary>
        /// 第几个课时
        /// </summary>
        public int LessonIndex { get; set; }
        /// <summary>
        /// 题目数量
        /// </summary>
        public int SubjectCount { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime FinishTime { get; set; }
        public string DisplayFinishTime
        {
            get
            {
                string time = string.Empty;
                if (FinishTime != Clock.MinValue)
                {
                    time = FinishTime.ToString(Clock.Format);
                }
                return time;
            }
        }
        public int StudyTaskId { get; set; }
        public int StudentTaskId { get; set; }
    }
}
