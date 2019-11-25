using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    public class StudyAudio : ActionBase
    {
        public StudyAudio() : base("studyAudio") { }
        /// <summary>
        /// 音频图片路径
        /// </summary>
        public string audioImgSrc { get; set; } = "";

        /// <summary>
        /// 音频路径
        /// </summary>
        public string audioSrc { get; set; } = "";

        /// <summary>
        /// 音频文本路径
        /// </summary>
        public string audioTextSrc { get; set; } = "";

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
        /// 文字位置
        /// </summary>
        public string wordPosition { get; set; } = "";
    }
}