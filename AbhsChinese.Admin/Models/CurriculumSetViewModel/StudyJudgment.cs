﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 判断题
    /// </summary>
    public class StudyJudgment : ActionBase
    {
        public StudyJudgment() : base("studyJudgment") { }

        /// <summary>
        /// 问题id
        /// </summary>
        public string questionid { get; set; }
        /// <summary>
        /// 问题名称
        /// </summary>
        public string questionname { get; set; }
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
        /// 做题时长
        /// </summary>
        public int usetime { get; set; }
    }
}