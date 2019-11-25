using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 缩放元素动作
    /// </summary>
    public class ScaleDom : ActionBase
    {
        public ScaleDom() : base("scaleDom") { }
        /// <summary>
        /// 缩放间隔
        /// </summary>
        public int duration { get; set; } = 1000;
        /// <summary>
        /// 缩放元素id
        /// </summary>
        public string objectId { get; set; } = "";
        /// <summary>
        /// 缩放比例 1=100%
        /// </summary>
        public double ratio { get; set; } = 1;
        /// <summary>
        /// 缩放次数
        /// </summary>
        public int num { get; set; } = 1;
    }
}