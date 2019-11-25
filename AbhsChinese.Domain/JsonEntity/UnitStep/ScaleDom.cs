using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 缩放元素动作
    /// </summary>
    public class ScaleDom : StepAction
    {
        public ScaleDom() : base(LessonActionTypeEnum.ScaleDom) { }
        /// <summary>
        /// 缩放间隔
        /// </summary>
        public int Duration { get; set; } = 1000;
        /// <summary>
        /// 缩放元素id
        /// </summary>
        public string ObjectId { get; set; } = "";
        /// <summary>
        /// 缩放比例 1=100%
        /// </summary>
        public double Ratio { get; set; } = 1;
        /// <summary>
        /// 缩放次数
        /// </summary>
        public int Num { get; set; } = 1;
    }
}