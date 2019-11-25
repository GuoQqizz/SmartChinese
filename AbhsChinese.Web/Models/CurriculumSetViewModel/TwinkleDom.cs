using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 闪烁
    /// </summary>
    public class TwinkleDom : ActionBase
    {
        public TwinkleDom() : base("twinkleDom") { }
        /// <summary>
        /// 执行时长(毫秒)
        /// </summary>
        public double duration { get; set; } = 1000;

        /// <summary>
        /// 执行次数
        /// </summary>
        public double num { get; set; } = 1;

        /// <summary>
        /// 对象id
        /// </summary>
        public string objectId { get; set; } = "";
    }
}