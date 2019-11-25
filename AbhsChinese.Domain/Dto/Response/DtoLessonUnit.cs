using AbhsChinese.Domain.JsonEntity.UnitStep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoLessonUnit
    {
        /// <summary>
        /// 单元(讲义)id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        public int CourseId { get; set; }
        /// <summary>
        /// 课时id
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// 单元序号
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 单元名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Screenshot { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 单元审批id
        /// </summary>
        public int ApproveId { get; set; }
        /// <summary>
        /// 审批状态
        /// </summary>
        public int ApproveStatus { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string Approve { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int Editor { get; set; }
        /// <summary>
        /// 步骤集合
        /// </summary>
        public List<Step> Steps { get; set; }
        /// <summary>
        /// 讲义金币总数
        /// </summary>
        public int Coins { get; set; }
    }
}

