using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 选择题
    /// </summary>
    public class StudyOption : ActionBase
    {
        public StudyOption() : base("studyOption")
        {
        }
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
        /// 问题id
        /// </summary>
        public int questionId { get; set; } = 0;
        /// <summary>
        /// 学习时长(秒)
        /// </summary>
        public int studyTime { get; set; } = 60000;
    }
}