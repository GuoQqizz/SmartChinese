using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 缩放元素
    /// </summary>
    public class ScaleDom : ActionBase
    {
        public ScaleDom() : base("scaleDom") { }
        /// <summary>
        /// 操作时长(毫秒)
        /// </summary>
        public double duration { get; set; } = 1000;

        /// <summary>
        /// 次数
        /// </summary>
        public double num { get; set; } = 1;

        /// <summary>
        /// 缩放对象
        /// </summary>
        public string objectId { get; set; } = "";
        /// <summary>
        /// 缩放比例
        /// </summary>
        public double ratio { get; set; } = 1;
    }
}