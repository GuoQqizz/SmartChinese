using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.School
{
    public class DtoSchoolClassSearch : DtoSearch
    {
        /// <summary>
        /// 按班级名/班主任/课程名搜索
        /// </summary>
        public string SearchStr { get; set; }

        public int ClassId { get; set; }

        public int SchoolId { get; set; }

        public int Grade { get; set; }

        public int Status { get; set; }

        public int CourseType { get; set; }
    }
}
