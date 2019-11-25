using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 朗读
    /// </summary>
    public class StudyRecitation : StepAction
    {
        public StudyRecitation() : base(LessonActionTypeEnum.StudyRecitation) { }

        /// <summary>
        /// 文本id
        /// </summary>
        public string TextId { get; set; }
        /// <summary>
        /// 开场白
        /// </summary>
        public string KcbId { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Golds { get; set; }
        /// <summary>
        /// 学习时间
        /// </summary>
        public int UseTime { get; set; }
    }
}