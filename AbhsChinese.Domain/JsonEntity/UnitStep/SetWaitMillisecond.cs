using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 等待动作
    /// </summary>
    public class SetWaitMillisecond : StepAction
    {
        public SetWaitMillisecond() : base(LessonActionTypeEnum.SetWaitMillisecond) { }
        /// <summary>
        /// 等待时长(毫秒)
        /// </summary>
        public int Stop { get; set; } = 1000;
    }
}