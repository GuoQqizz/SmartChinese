using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request
{
    public class DtoApplyStudentSearch : DtoSearch
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string Bst_No { get; set; }
        /// <summary>
        /// 按账号/昵称/姓名搜索
        /// </summary>
        public string SearchStr { get; set; }

        public int Grade { get; set; }

        public int SchoolId { get; set; }

        //public int Status { get; set; }
    }
}
