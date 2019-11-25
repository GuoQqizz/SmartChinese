using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 等待动作
    /// </summary>
    public class SetWaitMillisecond : ActionBase
    {
        public SetWaitMillisecond() : base("setWaitMillisecond") { }
        /// <summary>
        /// 等待时长(毫秒)
        /// </summary>
        public int stop { get; set; } = 1000;
    }
}