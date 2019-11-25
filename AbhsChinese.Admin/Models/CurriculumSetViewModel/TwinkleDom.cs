using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 闪烁
    /// </summary>
    public class TwinkleDom : ActionBase
    {
        public TwinkleDom() : base("twinkleDom") { }
        /// <summary>
        /// 闪烁间隔
        /// </summary>
        public int duration { get; set; } = 1000;
        /// <summary>
        /// 闪烁次数
        /// </summary>
        public int num { get; set; } = 1;
        /// <summary>
        /// 闪烁元素id
        /// </summary>
        public string objectId { get; set; } = "";
    }
}
