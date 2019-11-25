using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 快速阅读
    /// </summary>
    public class StudyFastRead :ActionBase
    {
        public StudyFastRead() : base("studyFastRead") { }
        /// <summary>
        /// 文本id
        /// </summary>
        public string textid { get; set; }
        /// <summary>
        /// 文本内容
        /// </summary>
        public string textstr { get; set; }
        /// <summary>
        /// 开场白id
        /// </summary>
        public string kcbid { get; set; }
        /// <summary>
        /// 开场白路径
        /// </summary>
        public string kcbsrc { get; set; }
        /// <summary>
        /// 开场白文本
        /// </summary>
        public string kcbtext { get; set; }

        /// <summary>
        /// 切换速度
        /// </summary>
        public int speed { get; set; }
        /// <summary>
        /// 显示文字数
        /// </summary>
        public int showNum { get; set; }
        /// <summary>
        /// 显示类型
        /// </summary>
        public string showModel { get; set; }
        /// <summary>
        /// 金币数
        /// </summary>
        public int goldCoins { get; set; }
    }
}