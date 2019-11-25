using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.Dto.Response
{
    public class DtoVoucherForUserCourse
    {
        public int VoucherType
        {
            set;get;
        }

        public decimal Amount
        {
            set;get;
        }

        public int CourseId
        {
            set;get;
        }
    }
}
