using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 插入文本
    /// </summary>
    public class InsertText : ActionBase
    {
        public InsertText() : base("insertText") { }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; } = "left";

        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; } = "#000";

        /// <summary>
        /// 进入方式
        /// </summary>
        public string intype { get; set; } = "none";

        /// <summary>
        /// 尺寸
        /// </summary>
        public string size { get; set; } = "middle";

        /// <summary>
        /// 文本
        /// </summary>
        public string text { get; set; } = "";
        /// <summary>
        /// x坐标
        /// </summary>
        public double x { get; set; } = 0;
        /// <summary>
        /// y坐标
        /// </summary>
        public double y { get; set; } = 0;
    }
}