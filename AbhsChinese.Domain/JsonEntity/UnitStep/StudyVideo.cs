using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 视频
    /// </summary>
    public class StudyVideo : StepAction
    {
        public StudyVideo() : base(LessonActionTypeEnum.StudyVideo) { }

        /// <summary>
        /// 媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 开场白
        /// </summary>
        public string KcbId { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Golds { get; set; }
    }
}