using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 小艾变
    /// </summary>
    public class XiaoAiChange : StepAction
    {
        public XiaoAiChange() : base(LessonActionTypeEnum.XiaoAiChange) { }
        /// <summary>
        /// 图片名称
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
        /// 水平对齐
        /// </summary>
        public string Align { get; set; } = "left";
        /// <summary>
        /// 垂直对齐
        /// </summary>
        public string VAlign { get; set; } = "top";
        /// <summary>
        /// 横坐标
        /// </summary>
        public double X { get; set; } = 0;
        /// <summary>
        /// 纵坐标
        /// </summary>
        public double Y { get; set; } = 0;
    }
}