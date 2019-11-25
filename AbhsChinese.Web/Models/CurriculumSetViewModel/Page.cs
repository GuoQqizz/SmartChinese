using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    public class Page
    {
        /// <summary>
        /// 讲义id
        /// </summary>
        public int pageId { get; set; } = 0;

        /// <summary>
        /// 讲义名称
        /// </summary>
        public string pageName { get; set; } = "";

        /// <summary>
        /// 讲义编号
        /// </summary>
        public int pageNum { get; set; } = 0;

        /// <summary>
        /// 金币加密
        /// </summary>
        public string coinsKey { get; set; } = "";

        /// <summary>
        /// 步骤列表
        /// </summary>
        public List<Step> steps { get; set; } = new List<Step>();
        
    }
}