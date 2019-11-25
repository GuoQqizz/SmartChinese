using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 步骤表
    /// </summary>
    public class Step
    {
        /// <summary>
        /// 步骤id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 步骤编号
        /// </summary>
        public int stepNum { get; set; }
        /// <summary>
        /// 动作集合
        /// </summary>
        public List<ActionBase> actions { get; set; }
    }
}