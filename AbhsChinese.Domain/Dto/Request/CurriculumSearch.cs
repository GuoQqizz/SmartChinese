using AbhsChinese.Domain.Enum;
using System.Collections.Generic;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoCurriculumSearch : DtoSearch
    {
        /// <summary>
        /// 课程状态
        /// </summary>
        public IList<int> Status { get; set; }
        /// <summary>
        /// 负责人名字
        /// </summary>
        public string OwnerName { get; set; }
        public int Grade { get; set; }
        public CourseCategoryEnum? CourseType { get; set; }

        /// <summary>
        /// 课程编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }
    }
}