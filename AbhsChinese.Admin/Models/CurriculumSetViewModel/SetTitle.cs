using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 设置标题动作
    /// </summary>
    public class SetTitle : ActionBase
    {
        public SetTitle() : base("setTitle")
        {

        }
        /// <summary>
        /// 标题文本
        /// </summary>
        public string text { get; set; } = "";
        /// <summary>
        /// 文字尺寸(big/middle/small)
        /// </summary>
        public string size { get; set; } = "middle";
        /// <summary>
        /// 文字颜色
        /// </summary>
        public string color { get; set; } = "#000000";
        /// <summary>
        /// 文字对齐方式(left/center/right)
        /// </summary>
        public string align { get; set; } = "left";
        /// <summary>
        /// x定位点
        /// </summary>
        public double x { get; set; } = 0;
        /// <summary>
        /// y定位点
        /// </summary>
        public double y { get; set; } = 0;
        /// <summary>
        /// 进入方式(none/top/bottom/left/right)
        /// </summary>
        public string intype { get; set; } = "none";
    }
}