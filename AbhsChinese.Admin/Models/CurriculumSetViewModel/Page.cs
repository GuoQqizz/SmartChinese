using AbhsChinese.Code.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AbhsChinese.Admin.Models.CurriculumSetViewModel
{
    /// <summary>
    /// 讲义页
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int courseId { get; set; }
        //课时id
        public int lessonId { get; set; }
        /// <summary>
        /// 讲义id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 讲义页号
        /// </summary>
        public int pageNum { get; set; }
        /// <summary>
        /// 讲义名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnail { get; set; }
        /// <summary>
        /// 步骤集合
        /// </summary>
        public List<Step> steps { get; set; }
        /// <summary>
        /// 审批结果类型
        /// </summary>
        public int approveType { get; set; }
        /// <summary>
        /// 审批意见
        /// </summary>
        public string approve { get; set; }

        /// <summary>
        /// 审批页面的加载链接
        /// </summary>
        public string approveAddress
        {
            get
            {
                return $"{ConfigurationManager.AppSettings["StudentClientDomain"]}LearningCenter/LessonApprove?ApproveKey={Encrypt.EncryptQueryString($"{new Random().Next(0, 10000)}_{id}_{lessonId}_{courseId}")}";
            }
        }
    }
}