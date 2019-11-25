using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 插入图片动作
    /// </summary>
    public class InsertImg : ActionBase
    {
        public InsertImg() : base("insertImg") { }
        /// <summary>
        /// 图片id
        /// </summary>
        public string imgId { get; set; } = "";
        /// <summary>
        /// 图片名称
        /// </summary>
        public string imgName { get; set; } = "";
        /// <summary>
        /// 图片路径
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
        /// 水平对齐(left/center/right)
        /// </summary>
        public string align { get; set; } = "left";
        /// <summary>
        /// 垂直对齐(top/middle/bottom)
        /// </summary>
        public string valign { get; set; } = "top";
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