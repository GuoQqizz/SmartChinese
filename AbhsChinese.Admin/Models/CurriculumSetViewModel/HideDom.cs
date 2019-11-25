using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 隐藏元素动作
    /// </summary>
    public class HideDom : ActionBase
    {
        public HideDom() : base("hideDom") { }
        /// <summary>
        /// 退出方式(none/top/left/right/bottom)
        /// </summary>
        public string outtype { get; set; } = "none";
        /// <summary>
        /// 隐藏元素id
        /// </summary>
        public string objectId { get; set; } = "";
    }
}