using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 插入文字动作
    /// </summary>
    public class InsertText : StepAction
    {
        public InsertText() : base(LessonActionTypeEnum.InsertText) { }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; } = "";
        /// <summary>
        /// 尺寸(big/middle/small)
        /// </summary>
        public string Size { get; set; } = "niddle";
        /// <summary>
        /// 文字颜色
        /// </summary>
        public string Color { get; set; } = "#000000";
        /// <summary>
        /// 水平对齐(left/center/right)
        /// </summary>
        public string Align { get; set; } = "left";
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