using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 移动元素位置动作
    /// </summary>
    public class MoveDom : ActionBase
    {
        public MoveDom() : base("moveDom") { }
        /// <summary>
        /// 移动到的横坐标
        /// </summary>
        public double x { get; set; } = 0;
        /// <summary>
        /// 移动到到纵坐标
        /// </summary>
        public double y { get; set; } = 0;
        /// <summary>
        /// 移动前位置
        /// </summary>
        public double ox { get; set; } = 0;
        /// <summary>
        /// 移动后位置
        /// </summary>
        public double oy { get; set; } = 0;
        /// <summary>
        /// 移动时长(毫秒)
        /// </summary>
        public int duration { get; set; } = 1000;
        /// <summary>
        /// 移动元素id
        /// </summary>
        public string objectId { get; set; } = "";
    }
}
