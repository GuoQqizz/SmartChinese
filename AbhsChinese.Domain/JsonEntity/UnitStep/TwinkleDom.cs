using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 闪烁
    /// </summary>
    public class TwinkleDom : StepAction
    {
        public TwinkleDom() : base(LessonActionTypeEnum.TwinkleDom) { }
        /// <summary>
        /// 闪烁间隔
        /// </summary>
        public int Duration { get; set; } = 1000;
        /// <summary>
        /// 闪烁次数
        /// </summary>
        public int Num { get; set; } = 1;
        /// <summary>
        /// 闪烁元素id
        /// </summary>
        public string ObjectId { get; set; } = "";
    }
}
