using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 设置标题
    /// </summary>
    public class SetTitle : ActionBase
    {
        /// <summary>
        /// 设置标题
        /// </summary>
        public SetTitle() : base("setTitle")
        {
        }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; } = "left";

        /// <summary>
        /// 文字颜色
        /// </summary>
        public string color { get; set; } = "#000";

        /// <summary>
        /// 进入方式
        /// </summary>
        public string intype { get; set; } = "none";

        /// <summary>
        /// 文字大小
        /// </summary>
        public string size { get; set; } = "middle";

        /// <summary>
        /// 标题文字
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