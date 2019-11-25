using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 步骤
    /// </summary>
    public class Step
    {
        /// <summary>
        /// 步骤动作
        /// </summary>
        public List<ActionBase> actions { get; set; } = new List<ActionBase>();
        /// <summary>
        /// 步骤编号
        /// </summary>
        public int stepNum { get; set; } = 0;
    }
}