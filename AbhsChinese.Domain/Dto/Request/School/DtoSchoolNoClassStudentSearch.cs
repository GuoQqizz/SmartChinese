using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.School
{
    public class DtoSchoolNoClassStudentSearch:DtoSearch
    {
        /// <summary>
        /// 按姓名/手机号搜索
        /// </summary>
        public string SearchStr { get; set; }

        public int SchoolId { get; set; }

        public string StudentNo { get; set; }

        public int Grade { get; set; }

        public int CourseType { get; set; }

    }
}
