using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 朗读
    /// </summary>
    public class StudyRecitation : ActionBase
    {
        public StudyRecitation() : base("studyRecitation") { }
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
        /// 金币数
        /// </summary>
        public int goldCoins { get; set; }
        /// <summary>
        /// 学习时间
        /// </summary>
        public int usetime { get; set; }
    }
}