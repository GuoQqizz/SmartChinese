using AbhsChinese.Code.Json;
using AbhsChinese.Domain.Dto.Request;
using AbhsChinese.Domain.Entity.School;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Response
{
    [JsonSerializeWithPrefix("Ycs")]
    public class DtoCourseDetails
    {
        /// <summary>
        /// 已合格课时
        /// </summary>
        public IList<int> ApprovedLessons { get; set; } = new List<int>();
        /// <summary>
        /// 课程计划
        /// </summary>
        public string Arrange { get; set; }

        /// <summary>
        /// 课程类型
        /// </summary>
        public int CourseType { get; set; }

        /// <summary>
        /// 课程封面图
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// 课程描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 课程年级
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// 课程id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 课程详情
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// 课时数量
        /// </summary>
        public int LessonCount { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// 课程价格
        /// </summary>
        public IList<DtoPricing> Pricings { get; set; } = new List<DtoPricing>();

        /// <summary>
        /// 资源组
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 教研老师
        /// </summary>
        public string Teachers { get; set; }
    }
}