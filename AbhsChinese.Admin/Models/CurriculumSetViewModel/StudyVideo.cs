using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 视频
    /// </summary>
    public class StudyVideo : ActionBase
    {
        public StudyVideo() : base("studyVideo") { }

        /// <summary>
        /// 媒体id
        /// </summary>
        public string mediaid { get; set; }
        /// <summary>
        /// 媒体名称
        /// </summary>
        public string medianame { get; set; }
        /// <summary>
        /// 媒体路径
        /// </summary>
        public string src { get; set; }
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
    }
}