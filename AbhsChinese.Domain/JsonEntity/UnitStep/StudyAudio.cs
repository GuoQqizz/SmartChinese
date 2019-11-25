using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 音频
    /// </summary>
    public class StudyAudio : StepAction
    {
        public StudyAudio() : base(LessonActionTypeEnum.StudyAudio) { }
        /// <summary>
        /// 媒体id
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 开场白
        /// </summary>
        public string KcbId { get; set; }
        /// <summary>
        /// 文字位置
        /// </summary>
        public string WordPosition { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Golds { get; set; }
    }
}