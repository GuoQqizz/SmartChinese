using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 快速阅读
    /// </summary>
    public class StudyFastRead : ActionBase
    {
        public StudyFastRead() : base("studyFastRead")
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
        /// whole表示整行，half表示半行
        /// </summary>
        public string showModel { get; set; } = "";
        /// <summary>
        /// 显示文字数量
        /// </summary>
        public int showNum { get; set; } = 5;

        /// <summary>
        /// 切换间隔
        /// </summary>
        public int speed { get; set; } = 1000;
    }
}