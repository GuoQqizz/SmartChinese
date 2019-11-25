using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 学习视频
    /// </summary>
    public class StudyVideo : ActionBase
    {
        public StudyVideo() : base("studyVideo") { }

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
        /// 视频路径
        /// </summary>
        public string videoSrc { get; set; } = "";
    }
}