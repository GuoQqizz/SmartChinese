using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 插入图片动作
    /// </summary>
    public class InsertImg : StepAction
    {
        public InsertImg() : base(LessonActionTypeEnum.InsertImg) { }
        /// <summary>
        /// 图片id
        /// </summary>
        public string ImgId { get; set; } = "";
        /// <summary>
        /// 图片宽度
        /// </summary>
        public double Width { get; set; } = 100;
        /// <summary>
        /// 图片高度
        /// </summary>
        public double Height { get; set; } = 100;
        /// <summary>
        /// 水平对齐(left/center/right)
        /// </summary>
        public string Align { get; set; } = "left";
        /// <summary>
        /// 垂直对齐(top/middle/bottom)
        /// </summary>
        public string VAlign { get; set; } = "top";
        /// <summary>
        /// x坐标
        /// </summary>
        public double X { get; set; } = 0;
        /// <summary>
        /// y坐标
        /// </summary>
        public double Y { get; set; } = 0;
        /// <summary>
        /// 进入动画(none/top/bottom/left/right)
        /// </summary>
        public string InType { get; set; } = "none";
    }
}