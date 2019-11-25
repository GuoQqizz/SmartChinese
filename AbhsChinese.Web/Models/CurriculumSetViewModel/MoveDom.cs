using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    public class MoveDom : ActionBase
    {
        public MoveDom() : base("moveDom") { }
        /// <summary>
        /// 操作时长(毫秒)
        /// </summary>
        public double duration { get; set; } = 0;

        /// <summary>
        /// 移动对象
        /// </summary>
        public string objectId { get; set; } = "";
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