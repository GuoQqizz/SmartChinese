using AbhsChinese.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Domain.JsonEntity.UnitStep
{
    /// <summary>
    /// 图文动作
    /// </summary>
    public class StudyArticle : StepAction
    {
        public StudyArticle() : base(LessonActionTypeEnum.StudyArticle) { }

        /// <summary>
        /// 文本id
        /// </summary>
        public string TextId { get; set; }
        /// <summary>
        /// 图片id
        /// </summary>
        public string ImgId { get; set; }
        /// <summary>
        /// 开场白
        /// </summary>
        public string KcbId { get; set; }
        /// <summary>
        /// 文本位置
        /// </summary>
        public string WordPosition { get; set; }
        /// <summary>
        /// 金币数量
        /// </summary>
        public int Golds { get; set; }
        /// <summary>
        /// 学习时间
        /// </summary>
        public int UseTime { get; set; }
    }
}