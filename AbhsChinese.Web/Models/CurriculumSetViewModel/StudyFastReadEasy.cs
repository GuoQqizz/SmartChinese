using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 快速阅读-简单模式
    /// </summary>
    public class StudyFastReadEasy : ActionBase
    {
        public StudyFastReadEasy() : base("studyFastReadEasy")
        {
        }
        /// <summary>
        /// 快速阅读文本
        /// </summary>
        public string fastReadText { get; set; } = "";

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
        /// 显示文字数量
        /// </summary>
        public int showNum { get; set; } = 10;

        /// <summary>
        /// 切换间隔
        /// </summary>
        public int speed { get; set; } = 1000;
    }
}