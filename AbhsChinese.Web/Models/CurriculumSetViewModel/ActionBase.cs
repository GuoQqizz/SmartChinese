using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 动作基类
    /// </summary>
    public class ActionBase
    {
        /// <summary>
        /// 根据动作类型创建对象
        /// </summary>
        /// <param name="type"></param>
        public ActionBase(string type)
        {
            this.type = type;
        }
        /// <summary>
        /// 动作id
        /// </summary>
        public string actionId { get; set; } = "";

        /// <summary>
        /// 动作编号
        /// </summary>
        public int actionNum { get; set; } = 0;
        /// <summary>
        /// 动作类型
        /// </summary>
        public string type { get; } = "";
    }
}