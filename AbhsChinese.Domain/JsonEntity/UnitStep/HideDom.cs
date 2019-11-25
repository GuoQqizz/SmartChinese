using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 隐藏元素动作
    /// </summary>
    public class HideDom : StepAction
    {
        public HideDom() : base(LessonActionTypeEnum.HideDom) { }
        /// <summary>
        /// 退出方式(none/top/left/right/bottom)
        /// </summary>
        public string OutType { get; set; } = "none";
        /// <summary>
        /// 隐藏元素id
        /// </summary>
        public string ObjectId { get; set; } = "";
    }
}