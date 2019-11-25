using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    public class SetWaitMillisecond : ActionBase
    {
        public SetWaitMillisecond() : base("setWaitMillisecond") { }
        /// <summary>
        /// 暂停时间(毫秒)
        /// </summary>
        public int stop { get; set; } = 1000;
    }
}