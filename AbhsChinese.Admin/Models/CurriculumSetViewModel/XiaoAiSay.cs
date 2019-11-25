using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 小艾说
    /// </summary>
    public class XiaoAiSay : ActionBase
    {
        public XiaoAiSay() : base("xiaoAiSay") { }
        /// <summary>
        /// 媒体id
        /// </summary>
        public string mediaid { get; set; }
        /// <summary>
        /// 媒体名称
        /// </summary>
        public string medianame { get; set; }
        /// <summary>
        /// 媒体路径
        /// </summary>
        public string src { get; set; }
    }
}