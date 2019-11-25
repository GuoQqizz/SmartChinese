using AbhsChinese.Code.Json;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Response
{
    [JsonSerializeWithPrefix("Ycs")]
    public class DtoCourseListItem
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public int Ycs_Id { get; set; }
        /// <summary>
        /// 课程名称
        /// </summary>
        public string Ycs_Name { get; set; }
        /// <summary>
        /// 课程类型
        /// </summary>
        public int Ycs_CourseType { get; set; }
        /// <summary>
        /// 课程年级
        /// </summary>
        public int Ycs_Grade { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Ycs_OwnerName { get; set; }
        /// <summary>
        /// 课时数量
        /// </summary>
        public int Ycs_LessonCount { get; set; }
        /// <summary>
        /// 课程描述
        /// </summary>
        public string Ycs_Description { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Ycs_Status { get; set; }
        /// <summary>
        /// 已合格的课时数
        /// </summary>
        public int Ycs_ApprovalLesCount { get; set; }
        /// <summary>
        /// 课程封面
        /// </summary>
        public string Ycs_CoverImage { get; set; }
        public IDictionary<string, decimal> Ycs_Price { get; set; } = new Dictionary<string, decimal>();
    }
}