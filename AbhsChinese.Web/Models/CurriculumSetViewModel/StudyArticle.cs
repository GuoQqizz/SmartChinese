using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 学习图文
    /// </summary>
    public class StudyArticle : ActionBase
    {
        public StudyArticle() : base("studyArticle")
        {
        }

        /// <summary>
        /// 图文图片路径
        /// </summary>
        public string articleImgSrc { get; set; } = "";

        /// <summary>
        /// 图文文字路径
        /// </summary>
        public string articleTextSrc { get; set; } = "";

        /// <summary>
        /// 金币数量
        /// </summary>
        public int goldCoins { get; set; } = 0;

        /// <summary>
        /// 开场白图片路径
        /// </summary>
        public string kcbImgSrc { get; set; } = "";

        /// <summary>
        /// 开场白路径
        /// </summary>
        public string kcbSrc { get; set; } = "";

        /// <summary>
        /// 开场白文字
        /// </summary>
        public string kcbTextSrc { get; set; } = "";

        /// <summary>
        /// 图文学习时间
        /// </summary>
        public int studyTime { get; set; } = 60000;

        /// <summary>
        /// 文字位置
        /// </summary>
        public string wordPosition { get; set; } = "top";
    }
}