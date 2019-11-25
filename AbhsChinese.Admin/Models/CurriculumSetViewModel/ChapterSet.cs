using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 课时设置
    /// </summary>
    public class ChapterSet
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int courseId { get; set; }
        /// <summary>
        /// 课时id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 章节名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 最后操作日期
        /// </summary>
        public string setTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 审批的id
        /// </summary>
        public int processid { get; set; }
        /// <summary>
        /// 课时讲义页
        /// </summary>
        public List<Page> pages { get; set; }
    }
}