using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Request.School
{
    public class DtoSchoolSearch : DtoSearch
    {
        public int Status { get; set; }

        public int SchoolId { get; set; }

        /// <summary>
        /// 按校区名/校长/手机号搜索
        /// </summary>
        public string SearchStr { get; set; }

        public int ProvId { get; set; }

        public int CityId { get; set; }

        public int CountyId { get; set; }
    }
}
