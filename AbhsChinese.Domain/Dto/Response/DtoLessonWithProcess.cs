using System;

namespace AbhsChinese.Domain.Dto.Response
{
    /// <summary>
    /// 课时信息(包含流程信息)
    /// </summary>
    public class DtoLessonWithProcess
    {
        /// <summary>
        /// 课时创建人
        /// </summary>
        public int Creator { get; set; }

        /// <summary>
        /// 课时序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 课时名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态审批流程
        /// </summary>
        public int Process { get; set; }

        /// <summary>
        /// 课时状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}