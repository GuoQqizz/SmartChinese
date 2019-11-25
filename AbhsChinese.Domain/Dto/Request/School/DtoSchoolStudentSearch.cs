using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.School
{
    public class DtoSchoolStudentSearch : DtoSearch
    {
        public int SchoolId { get; set; }
        /// <summary>
        /// 按账号/昵称/姓名搜索
        /// </summary>
        public string SearchStr { get; set; }

        public int StudentId { get; set; }

        public int Grade { get; set; }

        public int Status { get; set; }

        public int AccountType { get; set; }
        public string Bst_No { get; set; }
    }
}
