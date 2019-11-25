using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 快速阅读
    /// </summary>
    public class StudyFastRead : StepAction
    {
        public StudyFastRead() : base(LessonActionTypeEnum.StudyFastRead) { }

        /// <summary>
        /// 文本id
        /// </summary>
        public string TextId { get; set; }
        /// <summary>
        /// 开场白
        /// </summary>
        public string KcbId { get; set; }
        /// <summary>
        /// 切换速度
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// 显示文字数
        /// </summary>
        public int ShowNum { get; set; }

        /// <summary>
        /// 显示类型
        /// </summary>
        public string ShowModel { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Golds { get; set; }
    }
}