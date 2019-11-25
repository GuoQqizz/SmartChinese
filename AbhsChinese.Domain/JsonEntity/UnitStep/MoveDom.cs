using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 移动元素位置动作
    /// </summary>
    public class MoveDom : StepAction
    {
        public MoveDom() : base(LessonActionTypeEnum.MoveDom) { }
        /// <summary>
        /// 移动到的横坐标
        /// </summary>
        public double X { get; set; } = 0;
        /// <summary>
        /// 移动到到纵坐标
        /// </summary>
        public double Y { get; set; } = 0;
        /// <summary>
        /// 移动前位置
        /// </summary>
        public double OX { get; set; } = 0;
        /// <summary>
        /// 移动后位置
        /// </summary>
        public double OY { get; set; } = 0;
        /// <summary>
        /// 移动时长(毫秒)
        /// </summary>
        public int Duration { get; set; } = 1000;
        /// <summary>
        /// 移动元素id
        /// </summary>
        public string ObjectId { get; set; } = "";
    }
}
