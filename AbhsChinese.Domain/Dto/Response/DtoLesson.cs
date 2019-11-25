using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    /// <summary>
    /// 课时信息
    /// </summary>
    public class DtoLesson
    {

        /// <summary>
        /// 课时id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 课时名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 课时序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 课时状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 课时状态文本
        /// </summary>
        public string StatusStr
        {
            get
            {
                return ((LessonStatusEnum)Status).ToString();
            }
        }
        /// <summary>
        /// 状态审批流程id
        /// </summary>
        public int ProcessId { get; set; }
        /// <summary>
        /// 制作人
        /// </summary>
        public int Producer { get; set; }
        /// <summary>
        /// 制作人姓名
        /// </summary>
        public string ProducerName { get; set; }
        /// <summary>
        /// 审批人
        /// </summary>
        public int Approver { get; set; }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string ApproverName { get; set; }

        /// <summary>
        /// 课时单元(讲义)数量
        /// </summary>
        public int UnitCount { get; set; }
        /// <summary>
        /// 课时创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 课时创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatorName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public int Editor { get; set; }
        /// <summary>
        /// 更新人名称
        /// </summary>
        public string EditorName { get; set; }
    }
}
