using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Enum
{
    public enum StepActionEnum
    {
        /// <summary>
        /// 设置标题
        /// </summary>
        setTitle = 1,
        /// <summary>
        /// 设置背景
        /// </summary>
        setBackground,
        /// <summary>
        /// 小艾说
        /// </summary>
        xiaoAiSay,
        /// <summary>
        /// 小艾变
        /// </summary>
        xiaoAiChange,
        /// <summary>
        /// 插入图片
        /// </summary>
        insertImg,
        /// <summary>
        /// 插入文本
        /// </summary>
        insertText,
        /// <summary>
        /// 等待
        /// </summary>
        setWaitMillisecond,
        /// <summary>
        /// 移动元素
        /// </summary>
        moveDom,
        /// <summary>
        /// 缩放元素
        /// </summary>
        scaleDom,
        /// <summary>
        /// 闪烁元素
        /// </summary>
        twinkleDom,
        /// <summary>
        /// 隐藏元素
        /// </summary>
        hideDom,
        /// <summary>
        /// 音频
        /// </summary>
        studyAudio,
        /// <summary>
        /// 视频
        /// </summary>
        studyVideo,
        /// <summary>
        /// 图文
        /// </summary>
        studyArticle,
        /// <summary>
        /// 朗读
        /// </summary>
        studyRecitation,
        /// <summary>
        /// 快速阅读-简单
        /// </summary>
        studyFastReadEasy,
        /// <summary>
        /// 快速阅读
        /// </summary>
        studyFastRead,
        /// <summary>
        /// 圈点批注
        /// </summary>
        studyAnnotation,
        /// <summary>
        /// 判断题
        /// </summary>
        studyJudgment,
        /// <summary>
        /// 连线题
        /// </summary>
        studyLinking,
        /// <summary>
        /// 选择题图
        /// </summary>
        studyOption,
        /// <summary>
        /// 选择填空题
        /// </summary>
        studyOptionFill,
        /// <summary>
        /// 填空题
        /// </summary>
        studyFill,
        /// <summary>
        /// 主观题
        /// </summary>
        studySubjective,
        /// <summary>
        /// 田字格写字
        /// </summary>
        StudyCalligraphy
    }
}
