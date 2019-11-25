using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.School
{
    public class DtoSchoolTeacherSearch : DtoSearch
    {
        public int SchoolId { get; set; }
        public int Grade { get; set; }
        public int Status { get; set; }
        public int TeacherId { get; set; }

        /// <summary>
        /// 按账号/姓名搜索
        /// </summary>
        public string SearchStr { get; set; }
        public DtoSchoolTeacherSearch()
        {

        }

        public DtoSchoolTeacherSearch(int schoolId)
        {
            this.SchoolId = schoolId;
        }
    }
}
