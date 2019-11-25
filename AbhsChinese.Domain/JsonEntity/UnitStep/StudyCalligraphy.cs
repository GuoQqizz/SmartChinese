using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 田字格写字
    /// </summary>
    public class StudyCalligraphy : StepAction
    {
        public StudyCalligraphy() : base(LessonActionTypeEnum.StudyCalligraphy) { }
        /// <summary>
        /// 问题id
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// 开场白
        /// </summary>
        public string KcbId { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Golds { get; set; }
        /// <summary>
        /// 做题时长
        /// </summary>
        public int UseTime { get; set; }
    }
}