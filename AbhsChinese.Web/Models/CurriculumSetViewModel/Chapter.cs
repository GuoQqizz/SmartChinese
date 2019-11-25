using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Web.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 课时数据
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// 是否用于审批
        /// </summary>
        public bool isApprove { get; set; } = false;
        /// <summary>
        /// 上次学习是否结束
        /// </summary>
        public bool lastTimeIsOver { get; set; } = true;
        /// <summary>
        /// 金币开始累加的页码
        /// </summary>
        public int startAddGoldsPageNum { get; set; } = 0;
        /// <summary>
        /// 开始执行的页码
        /// </summary>
        public int startPageNum { get; set; } = 0;
        /// <summary>
        /// 总页码数
        /// </summary>
        public int totalPageNum { get; set; } = 0;
        /// <summary>
        /// 课程id
        /// </summary>
        public int courseId { get; set; } = 0;
        /// <summary>
        /// 课时id
        /// </summary>
        public int lessonId { get; set; } = 0;

        /// <summary>
        /// 课时编号
        /// </summary>
        public int lessonIndex { get; set; } = 0;

        /// <summary>
        /// 课时学习进度id
        /// </summary>
        public int lessonProgressId { get; set; } = 0;

        /// <summary>
        /// 课时学习进度秘钥
        /// </summary>
        public string progressKey { get; set; } = "";
    }
}