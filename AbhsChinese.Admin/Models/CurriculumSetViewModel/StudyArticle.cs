using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 图文动作
    /// </summary>
    public class StudyArticle : ActionBase
    {
        public StudyArticle() : base("studyArticle") { }
        /// <summary>
        /// 文本id
        /// </summary>
        public string textid { get; set; }
        /// <summary>
        /// 文本内容
        /// </summary>
        public string textstr { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        public string imgid { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string imgsrc { get; set; }
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
        /// 文本位置
        /// </summary>
        public string wordPosition { get; set; }
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