using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 插入文字动作
    /// </summary>
    public class InsertText : ActionBase
    {
        public InsertText() : base("insertText") { }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string text { get; set; } = "";
        /// <summary>
        /// 尺寸(big/middle/small)
        /// </summary>
        public string size { get; set; } = "niddle";
        /// <summary>
        /// 文字颜色
        /// </summary>
        public string color { get; set; } = "#000000";
        /// <summary>
        /// 水平对齐(left/center/right)
        /// </summary>
        public string align { get; set; } = "left";
        /// <summary>
        /// x坐标
        /// </summary>
        public double x { get; set; } = 0;
        /// <summary>
        /// y坐标
        /// </summary>
        public double y { get; set; } = 0;

        /// <summary>
        /// 进入动画(none/top/bottom/left/right)
        /// </summary>
        public string intype { get; set; } = "none";
    }
}