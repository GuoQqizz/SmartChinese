using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 设置背景
    /// </summary>
    public class SetBackground : ActionBase
    {
        public SetBackground() : base("setBackground")
        {
        }
        /// <summary>
        /// 背景内容
        /// </summary>
        public string bg { get; set; } = "#fff";

        /// <summary>
        /// 背景类型
        /// </summary>
        public string bgType { get; set; } = "color";
    }
}