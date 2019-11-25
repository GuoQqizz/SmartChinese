using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    public class SetBackground : ActionBase
    {
        public SetBackground() : base("setBackground") {

        }
        /// <summary>
        /// 背景类型(color/image)
        /// </summary>
        public string bgType { get; set; } = "color";
        /// <summary>
        /// 背景内容(颜色数字或媒体id)
        /// </summary>
        public string bg { get; set; } = "#ffffff";
        /// <summary>
        /// 背景图片链接
        /// </summary>
        public string bgUrl { get; set; }
        /// <summary>
        /// 背景图片名称
        /// </summary>
        public string bgName { get; set; }
    }
}