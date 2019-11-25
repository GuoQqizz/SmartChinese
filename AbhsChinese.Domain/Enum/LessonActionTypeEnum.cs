using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    /// <summary>
    /// 课程动作类型
    /// </summary>
    public enum LessonActionTypeEnum
    {
        /// <summary>
        /// 设置标题
        /// </summary>
        SetTitle = 1,
        /// <summary>
        /// 设置背景
        /// </summary>
        SetBackground,
        /// <summary>
        /// 小艾说
        /// </summary>
        XiaoAiSay,
        /// <summary>
        /// 小艾变
        /// </summary>
        XiaoAiChange,
        /// <summary>
        /// 插入图片
        /// </summary>
        InsertImg,
        /// <summary>
        /// 插入文字
        /// </summary>
        InsertText,
        /// <summary>
        /// 设置等候时间
        /// </summary>
        SetWaitMillisecond,

        /// <summary>
        /// 移动元素
        /// </summary>
        MoveDom,
        /// <summary>
        /// 缩放元素
        /// </summary>
        ScaleDom,
        /// <summary>
        /// 闪烁元素
        /// </summary>
        TwinkleDom,
        /// <summary>
        /// 隐藏元素
        /// </summary>
        HideDom,

        /// <summary>
        /// 学习音频
        /// </summary>
        StudyAudio,
        /// <summary>
        /// 学习视频
        /// </summary>
        StudyVideo,
        /// <summary>
        /// 学习图文
        /// </summary>
        StudyArticle,
        /// <summary>
        /// 学习朗读
        /// </summary>
        StudyRecitation,
        /// <summary>
        /// 快速阅读-简单
        /// </summary>
        StudyFastReadEasy,
        /// <summary>
        /// 快速阅读
        /// </summary>
        StudyFastRead,
        /// <summary>
        /// 圈点批注-标色
        /// </summary>
        StudyAnnotation,
        /// <summary>
        /// 圈点批注-断句
        /// </summary>
        StudyAnnotation2,

        /// <summary>
        /// 判断题
        /// </summary>
        StudyJudgment,
        /// <summary>
        /// 连线题
        /// </summary>
        StudyLinking,
        /// <summary>
        /// 选择题
        /// </summary>
        StudyOption,
        /// <summary>
        /// 选择填空
        /// </summary>
        StudyOptionFill,
        /// <summary>
        /// 填空题
        /// </summary>
        StudyFill,
        /// <summary>
        /// 主观题
        /// </summary>
        StudySubjective,
        /// <summary>
        /// 田字格写字
        /// </summary>
        StudyCalligraphy,
    }
}
