using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 隐藏元素
    /// </summary>
    public class HideDom : ActionBase
    {
        public HideDom() : base("hideDom") { }
        /// <summary>
        /// 隐藏对象
        /// </summary>
        public string objectId { get; set; } = "";

        /// <summary>
        /// 隐藏方式
        /// </summary>
        public string outtype { get; set; } = "none";
    }
}