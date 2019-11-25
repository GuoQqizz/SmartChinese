using AbhsChinese.Code.Common;
using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.SubjectReport
{
    public class ReportVm
    {
        /// <summary>
        /// 题目Id
        /// </summary>
        public int SubjectId { get; set; }
        /// <summary>
        /// 题干
        /// </summary>
        public string Stem { get; set; }
        /// <summary>
        /// 题干的类型（图片或文字）
        /// </summary>
        public int StemType { get; set; }
        /// <summary>
        /// 学生答案
        /// </summary>
        public object StudentAnswer { get; set; }
        /// <summary>
        /// 标准答案
        /// </summary>
        public object Answer { get; set; }
        /// <summary>
        /// 解析
        /// </summary>
        public string Analysis { get; set; }
        /// <summary>
        /// 题目类型
        /// </summary>
        public int SubjectType { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public int ResultStars { get; set; }
        /// <summary>
        /// 难度
        /// </summary>
        public int Difficulty { get; set; }

        public string DifficultyStr => CustomEnumHelper.Parse(typeof(DifficultyEnum), Difficulty);
    }
}