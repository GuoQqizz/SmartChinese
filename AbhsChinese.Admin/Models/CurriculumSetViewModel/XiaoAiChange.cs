using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 小艾变
    /// </summary>
    public class XiaoAiChange : ActionBase
    {
        public XiaoAiChange() : base("xiaoAiChange") { }
        /// <summary>
        /// 图片id
        /// </summary>
        public string imgId { get; set; } = "";
        /// <summary>
        /// 图片名称
        /// </summary>
        public string imgName { get; set; } = "";
        /// <summary>
        /// 图片资源路径
        /// </summary>
        public string src { get; set; } = "";
        /// <summary>
        /// 图片宽度
        /// </summary>
        public double width { get; set; } = 100;
        /// <summary>
        /// 图片高度
        /// </summary>
        public double height { get; set; } = 100;
        /// <summary>
        /// 水平对齐
        /// </summary>
        public string align { get; set; } = "left";
        /// <summary>
        /// 垂直对齐
        /// </summary>
        public string valign { get; set; } = "top";
        /// <summary>
        /// 横坐标
        /// </summary>
        public double x { get; set; } = 0;
        /// <summary>
        /// 纵坐标
        /// </summary>
        public double y { get; set; } = 0;
    }
}