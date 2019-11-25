using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;

namespace AbhsChinese.Admin.Models.Course
{
    public class LessonViewModel
    {
        public int ID { get; set; }
        public int CourseId { get; set; }
        public int Producer { get; set; }
        public string ProducerName { get; set; }
        public int Approver { get; set; }
        public string ApproverName { get; set; }
        public int Creator { get; set; }
        public string CreatorName { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string StatusText
        {
            get
            {
                return ((LessonStatusEnum)Status).ToString();
            }
        }
        public DateTime UpdateTime { get; set; }
        public string UpdateTimeText { get { return UpdateTime.ToString(Clock.Format); } }
    }
}