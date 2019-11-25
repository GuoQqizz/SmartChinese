using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 插入图片
    /// </summary>
    public class InsertImg : ActionBase
    {
        public InsertImg() : base("insertImg") { }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; } = "left";

        /// <summary>
        /// 图片高度
        /// </summary>
        public double height { get; set; } = 0;

        /// <summary>
        /// 图片id
        /// </summary>
        public string imgId { get; set; } = "";
        /// <summary>
        /// 进入方式
        /// </summary>
        public string intype { get; set; } = "none";
        /// <summary>
        /// 图片路径
        /// </summary>
        public string src { get; set; } = "";
        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        public string valign { get; set; } = "top";

        /// <summary>
        /// 图片宽度
        /// </summary>
        public double width { get; set; } = 0;
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